using System;
using System.Collections.ObjectModel;
using NUnit.Framework;
using Ucu.Poo.DiscordBot.Domain;

namespace Ucu.Poo.DiscordBot.Tests
{
    [TestFixture]
    public class UserTests
    {
        [Test]
        public void Constructor_WithValidUserName_SetsProperty()
        {
            var user = new User("Test");
            Assert.That(user.UserName, Is.EqualTo("Test"));
        }

        [Test]
        public void Constructor_WithNullUserName_ThrowsArgumentNullException()
        {
            Assert.That(() => new User(null), Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void Constructor_WithEmptyUserName_ThrowsArgumentNullException()
        {
            Assert.That(() => new User(string.Empty), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void Constructor_Roles_InitiallyEmpty()
        {
            var user = new User("Test");
            Assert.That(user.Roles, Is.Not.Null);
            Assert.That(user.Roles.Count, Is.EqualTo(0));
        }

        [Test]
        public void AddRole_WithNullRole_ThrowsArgumentNullException()
        {
            var user = new User("Test");
            Assert.That(() => user.AddRole(null), Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void AddRole_WhenRoleIsValid_AddsRoleToUser()
        {
            var user = new User("Test");

            user.AddRole(Role.Admin);

            Assert.That(user.HasRole(Role.Admin), Is.True);
            Assert.That(user.Roles, Contains.Item(Role.Admin));
            Assert.That(user.Roles.Count, Is.EqualTo(1));
        }

        [Test]
        public void AddRole_WhenRoleAlreadyExists_IgnoresButDoesNotFail()
        {
            var user = new User("Test");

            user.AddRole(Role.User);
            user.AddRole(Role.User);

            Assert.That(user.Roles.Count, Is.EqualTo(1));
            Assert.That(user.HasRole(Role.User), Is.True);
        }

        [Test]
        public void HasRole_WhenRoleNotPresent_ReturnsFalse()
        {
            var user = new User("Test");

            Assert.That(user.HasRole(Role.User), Is.False);
        }

        [Test]
        public void RemoveRole_WithNullRole_ThrowsArgumentNullException()
        {
            var user = new User("Test");
            Assert.That(() => user.RemoveRole(null), Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void RemoveRole_WhenUserDoesNotHaveRole_ThrowsInvalidOperationExceptionWithMessage()
        {
            var user = new User("Test");

            Assert.Throws<InvalidOperationException>(() => user.RemoveRole(Role.User));
        }

        [Test]
        public void RemoveRole_WhenRoleIsValid_RemovesExistingRole()
        {
            var user = new User("Test");

            user.AddRole(Role.User);
            Assert.That(user.HasRole(Role.User), Is.True);
            Assert.That(user.Roles.Count, Is.EqualTo(1));

            user.RemoveRole(Role.User);

            Assert.That(user.HasRole(Role.User), Is.False);
            Assert.That(user.Roles.Count, Is.EqualTo(0));
        }

        [Test]
        public void Roles_IsReadOnlyCopy_NotModifiableFromOutside()
        {
            var user = new User("Test");

            user.AddRole(Role.User);

            var readOnly = user.Roles;
            Assert.That(() =>
            {
                var asReadOnly = (ReadOnlyCollection<Role>)readOnly;
            }, Throws.Nothing);

            var snapshot1 = user.Roles;
            var snapshot2 = user.Roles;
            Assert.That(snapshot1, Is.Not.SameAs(snapshot2));
        }
    }
}
