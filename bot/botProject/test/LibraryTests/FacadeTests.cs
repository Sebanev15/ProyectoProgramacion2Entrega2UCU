using System;
using System.Reflection;
using Moq;
using NUnit.Framework;
using Ucu.Poo.DiscordBot.Domain;

namespace Ucu.Poo.DiscordBot.Tests
{
    [TestFixture]
    public class FacadeTests
    {
        [Test]
        public void Instance_ReturnsSameSingletonInstance()
        {
            var f1 = Facade.Instance;
            var f2 = Facade.Instance;

            Assert.That(f1, Is.SameAs(f2));
        }

        [Test]
        public void GetUserInfo_WhenUserNotFound_ReturnsNotFoundMessage()
        {
            Mock<IUsersRepository> mockedRepository;
            var facade = CreateFacadeWithMockRepo(out mockedRepository);
            mockedRepository.Setup(r => r.Find("Test")).Returns((User)null);

            var actual = facade.GetUserInfo("Test");

            Assert.That(actual, Is.EqualTo("El usuario de Discord 'Test' no es usuario de esta aplicación."));
            mockedRepository.Verify(r => r.Find("Test"), Times.Once);
        }

        [Test]
        public void GetUserInfo_WhenUserExistsWithoutRoles_FormatsEmptyRoles()
        {
            Mock<IUsersRepository> mockedRepository;
            var facade = CreateFacadeWithMockRepo(out mockedRepository);
            var user = new User("Test"); // sin roles
            mockedRepository.Setup(r => r.Find("Test")).Returns(user);

            var actual = facade.GetUserInfo("Test");

            Assert.That(actual, Is.EqualTo("El usuario 'Test' tiene los roles '' en esta aplicación."));
            mockedRepository.Verify(r => r.Find("Test"), Times.Once);
        }

        [Test]
        public void GetUserInfo_WhenUserExistsWithRoles_ListsCommaSeparatedRoleNames()
        {
            Mock<IUsersRepository> mockedRepository;
            var facade = CreateFacadeWithMockRepo(out mockedRepository);
            var user = new User("Test");
            user.AddRole(Role.Admin);
            user.AddRole(Role.User);
            mockedRepository.Setup(r => r.Find("Test")).Returns(user);

            var actual = facade.GetUserInfo("Test");

            Assert.That(actual, Is.EqualTo("El usuario 'Test' tiene los roles 'Admin, User' en esta aplicación."));
            mockedRepository.Verify(r => r.Find("Test"), Times.Once);
        }

        [Test]
        public void GetUserInfo_UsesRoleToStringInJoin()
        {
            Mock<IUsersRepository> mockedRepository;
            var facade = CreateFacadeWithMockRepo(out mockedRepository);
            var user = new User("Test");
            user.AddRole(Role.User);
            mockedRepository.Setup(r => r.Find("Test")).Returns(user);

            var actual = facade.GetUserInfo("Test");

            Assert.That(actual, Does.Contain(Role.User.Name));
            mockedRepository.Verify(r => r.Find("Test"), Times.Once);
        }

        // Helper: crea una instancia de Facade inyectando un mock de UsersRepository
        private static Facade CreateFacadeWithMockRepo(out Mock<IUsersRepository> mockedRepository)
        {
            mockedRepository = new Mock<IUsersRepository>(MockBehavior.Strict);

            var facadeType = typeof(Facade);
            
            var instanceProp = facadeType.GetProperty("Instance", BindingFlags.Public | BindingFlags.Static);
            var facadeInstance = instanceProp.GetValue(null);

            if (facadeInstance == null)
            {
                facadeInstance = Facade.Instance;
            }

            var repoField = facadeType.GetField("usersRepository", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.That(repoField, Is.Not.Null, "No se encontró el campo privado usersRepository en Facade.");
            repoField.SetValue(facadeInstance, mockedRepository.Object);

            return (Facade)facadeInstance;
        }
    }
}
