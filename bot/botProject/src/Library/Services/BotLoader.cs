using System;
using System.Reflection;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Library;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Ucu.Poo.DiscordBot.Commands;
using Ucu.Poo.DiscordBot.Interfaces;
using Ucu.Poo.DiscordBot.Modals;

namespace Ucu.Poo.DiscordBot.Services
{
    public static class BotLoader
    {
        public static async Task LoadAsync()
        {
            var configuration = new ConfigurationBuilder()
                .AddUserSecrets<IBot>()
                .Build();

            var services = new ServiceCollection();

            var client = new DiscordSocketClient(new DiscordSocketConfig
            {
                GatewayIntents = GatewayIntents.AllUnprivileged | GatewayIntents.MessageContent,
                LogLevel = LogSeverity.Info
            });

            services
                .AddLogging(configure => configure.AddConsole())
                .AddSingleton<IConfiguration>(configuration)
                .AddSingleton(Fachada.GetInstancia())
                .AddSingleton(client)
                .AddSingleton(sp => new InteractionService(client.Rest, new InteractionServiceConfig
                {
                    LogLevel = LogSeverity.Info,
                    DefaultRunMode = RunMode.Async
                }))
                .AddSingleton<CommandsCrearEntidades>()
                .AddSingleton<IModalHandler, ClienteModals>()
                .AddScoped<IBot, Bot>();

            var serviceProvider = services.BuildServiceProvider();

            try
            {
                var interactions = serviceProvider.GetRequiredService<InteractionService>();

                client.Log += msg => { Console.WriteLine($"[Client Log] {msg}"); return Task.CompletedTask; };
                interactions.Log += msg => { Console.WriteLine($"[Interaction Log] {msg}"); return Task.CompletedTask; };

                var assembly = typeof(CommandsCrearEntidades).Assembly;
                Console.WriteLine($"Cargando módulos desde ensamblado: {assembly.FullName}");

                var modulesAdded = await interactions.AddModulesAsync(assembly, serviceProvider).ConfigureAwait(false);
                Console.WriteLine($"Módulos cargados: {modulesAdded.Count()}");
                foreach (var module in modulesAdded)
                {
                    Console.WriteLine($"  - Módulo: {module.Name}");
                    foreach (var cmd in module.SlashCommands)
                        Console.WriteLine($"    Slash Command: /{cmd.Name}");
                    foreach (var modal in module.ModalCommands)
                        Console.WriteLine($"    Modal Command: {modal.Name}");
                }

                interactions.InteractionExecuted += async (command, context, result) =>
                {
                    if (!result.IsSuccess)
                    {
                        Console.WriteLine($"Error ejecutando interacción: {result.ErrorReason}");
                        if (result.Error.HasValue)
                            Console.WriteLine($" - Tipo de error: {result.Error.Value}");
                    }
                };

                client.InteractionCreated += async interaction =>
                {
                    try
                    {
                        Console.WriteLine($"Interacción recibida: {interaction.Type}");
                        var ctx = new SocketInteractionContext(client, interaction);
                        await interactions.ExecuteCommandAsync(ctx, serviceProvider).ConfigureAwait(false);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Excepción en InteractionCreated: {ex}");
                    }
                };

                // Auto-detectar todos los handlers registrados
                var modalHandlers = serviceProvider.GetServices<IModalHandler>()
                    .ToDictionary(h => h.CustomId, h => h);

                Console.WriteLine($"Handlers de modals registrados: {modalHandlers.Count}");
                foreach (var handler in modalHandlers)
                {
                    Console.WriteLine($"  - Handler para: {handler.Key}");
                }

                client.ModalSubmitted += async modal =>
                {
                    Console.WriteLine($"Modal recibido: {modal.Data.CustomId}");

                    if (modalHandlers.TryGetValue(modal.Data.CustomId, out var handler))
                    {
                        await handler.HandleAsync(modal);
                    }
                    else
                    {
                        Console.WriteLine($"⚠️ No hay handler registrado para: {modal.Data.CustomId}");
                    }
                };

                var readyTaskSource = new TaskCompletionSource<bool>();

                client.Ready += async () =>
                {
                    Console.WriteLine("Cliente conectado y listo.");
                    var guildEnv = Environment.GetEnvironmentVariable("DISCORD_TEST_GUILD_ID");
                    if (ulong.TryParse(guildEnv, out var testGuildId))
                    {
                        Console.WriteLine($"Registrando comandos en el guild de prueba {testGuildId}...");
                        await interactions.RegisterCommandsToGuildAsync(testGuildId).ConfigureAwait(false);

                        var commands = interactions.SlashCommands;
                        Console.WriteLine($"Comandos registrados: {commands.Count}");
                        foreach (var cmd in commands)
                        {
                            Console.WriteLine($"  - Comando: /{cmd.Name}");
                        }
                        Console.WriteLine("Comandos listos para usar en Discord.");
                    }
                    else
                    {
                        Console.WriteLine("No se encontró `DISCORD_TEST_GUILD_ID`. Registrando comandos globalmente...");
                        await interactions.RegisterCommandsGloballyAsync().ConfigureAwait(false);
                        Console.WriteLine("Comandos globales registrados (pueden tardar hasta 1 hora en aparecer).");
                    }

                    readyTaskSource.SetResult(true);
                };

                IBot bot = serviceProvider.GetRequiredService<IBot>();
                await bot.StartAsync(serviceProvider).ConfigureAwait(false);

                Console.WriteLine("Esperando que el bot se conecte completamente...");
                await readyTaskSource.Task.ConfigureAwait(false);

                Console.WriteLine("Bot conectado. Presione 'q' para salir.");
                while (Console.ReadKey(true).Key != ConsoleKey.Q) { }

                await bot.StopAsync().ConfigureAwait(false);
                Console.WriteLine("\nFinalizado.");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                Environment.Exit(-1);
            }
        }
    }
}
