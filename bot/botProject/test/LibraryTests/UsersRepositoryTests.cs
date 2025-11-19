// Filename: UsersRepositoryTests.cs

using System.Linq;
using NUnit.Framework;
using Ucu.Poo.DiscordBot.Domain;

namespace Ucu.Poo.DiscordBot.Tests
{
    [TestFixture]
    public class UsersRepositoryTests
    {
        [Test]
        public void Constructor_SeedsFirstAdminUser()
        {
            var repository = new UsersRepository();

            User admin = null;
            foreach (User user in repository.AllUsers)
            {
                if (user.HasRole(Role.Admin))
                {
                    admin = user;
                    break;
                }
            }
                
            Assert.That(admin, Is.Not.Null);
        }

        [Test]
        public void Find_WhenUserExists_ReturnsUser()
        {
            var repository = new UsersRepository();
            User user = repository.AllUsers.FirstOrDefault();
            
            var actual = repository.Find(user.UserName);
            
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual, Is.EqualTo(user));
        }

        [Test]
        public void Find_WhenUserDoesNotExist_ReturnsNull()
        {
            var repo = new UsersRepository();

            var actual = repo.Find("no-existe");
            
            Assert.That(actual, Is.Null);
        }

        [Test]
        public void Find_WithNullUserName_ReturnsNull()
        {
            var repo = new UsersRepository();

            var actual = repo.Find(null);
            
            Assert.That(actual, Is.Null);
        }

        [Test]
        public void Find_WithEmptyUserName_ReturnsNull()
        {
            var repo = new UsersRepository();

            var actual = repo.Find(string.Empty);
            
            Assert.That(actual, Is.Null);
        }
    }
}
