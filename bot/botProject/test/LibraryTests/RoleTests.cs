using System;
using NUnit.Framework;
using Ucu.Poo.DiscordBot.Domain;

namespace Ucu.Poo.DiscordBot.Tests
{
    [TestFixture]
    public class RoleTests
    {
        [Test]
        public void Constructor_WithValidName_SetsProperty()
        {
            var role = new Role("Moderator");
            
            Assert.That(role.Name, Is.EqualTo("Moderator"));
        }

        [Test]
        public void Constructor_WithNullName_ThrowsArgumentNullException()
        {
            Assert.That(() => new Role(null), Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void Constructor_WithEmptyName_ThrowsArgumentNullException()
        {
            Assert.That(() => new Role(string.Empty), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void Admin_StaticRole_IsSingletonWithCorrectName()
        {
            var admin1 = Role.Admin;
            var admin2 = Role.Admin;

            Assert.That(admin1, Is.SameAs(admin2));              
            Assert.That(admin1.Name, Is.EqualTo("Admin"));       
            Assert.That(admin1.ToString(), Is.EqualTo("Admin"));
        }

        [Test]
        public void User_StaticRole_IsSingletonWithCorrectName()
        {
            var user1 = Role.User;
            var user2 = Role.User;

            Assert.That(user1, Is.SameAs(user2));
            Assert.That(user1.Name, Is.EqualTo("User"));
            Assert.That(user1.ToString(), Is.EqualTo("User"));
        }

        [Test]
        public void ToString_ReturnsRoleName()
        {
            var role = new Role("Reader");
            Assert.That(role.ToString(), Is.EqualTo("Reader"));
        }

        [Test]
        public void DifferentInstances_WithSameName_AreNotReferenceEqual()
        {
            var r1 = new Role("SameName");
            var r2 = new Role("SameName");

            Assert.That(r1, Is.Not.SameAs(r2));
            Assert.That(r1.Name, Is.EqualTo(r2.Name));
        }

        [Test]
        public void Name_IsImmutable()
        {
            var role = new Role("Immutable");
            Assert.That(() => { var _ = role.Name; }, Throws.Nothing);
        }
    }
}
