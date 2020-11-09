using Xunit;
using MedBook_RazorPages.Models;
using MedBook_RazorPages.Resources;
using System.Linq;
using System.Collections.Generic;
using Xunit.Abstractions;

namespace XUnitTestProject1

{
    public class UnitTest1
    {
        private List<Users> users;

        private readonly ITestOutputHelper output;

        public UnitTest1(ITestOutputHelper output)
        {
            this.output = output;
        }

        void fillUp() {
            if (users == null) 
            {
                users = new List<Users>();
            }

            Users user1 = new Users();
            user1.firstName = "Aioane";
            user1.lastName = "Dragos";
            user1.email = "dragosaioane1998@gmail.com";
            user1.password = BCrypt.Net.BCrypt.HashPassword("testPassword");

            Users user2 = new Users();
            user2.firstName = "Gigiel";
            user2.lastName = "popa";
            user2.email = "gigelpopa@gmail.com";
            user2.password = BCrypt.Net.BCrypt.HashPassword("testPassword");

            users.Add(user1);
            users.Add(user2);
        }

        void clear() {
            users.Clear();
        }

        [Fact]
        public void TestEmty() {
            fillUp();
            Assert.NotEmpty(users.ToList());
            clear();
        }

        [Fact]
        public void TestUserAdded() {
            fillUp();
            Users description = new Users();
            verifyingUsers verifying = new verifyingUsers();

            description.email = "draosaioane1999@gmail.com";
            description.firstName = "Aioanei";
            description.lastName = "Dragos";
            description.password = BCrypt.Net.BCrypt.HashPassword("testPassword");
            List<Users> list = verifying.getAllUsers(users, description);

            Assert.Equal("draosaioane1999@gmail.com", users.Where(user => user.firstName == description.firstName).Single().email);
            clear();
        }

        [Fact]
        public void TestFailUserAdded()
        {
            
            fillUp();
            Users description = new Users();
            verifyingUsers verifying = new verifyingUsers();

            description.email = "draosaioane1999@gmail.com";
            description.firstName = "Aioanei";
            description.lastName = "Dragos";
            description.password = BCrypt.Net.BCrypt.HashPassword("testPassword");
            users = verifying.getAllUsers(users, description);
            users = verifying.getAllUsers(users, description);
            int count = 0;

            foreach (var user in users) {
                if (user.email == "draosaioane1999@gmail.com")
                    count++;
            }

            Assert.Equal(1, count);
            clear();
        }

        [Fact]
        public void testHashPassword() {
            fillUp();
            Users description = new Users();
            verifyingUsers verifying = new verifyingUsers();

            description.firstName = "Giovanni";
            description.lastName = "Andriasi";
            description.email = "draosaioane1999@gmail.com";
            description.password = BCrypt.Net.BCrypt.HashPassword("testPassword");
            
            users = verifying.getAllUsers(users, description);

            Assert.NotEqual("testPassword", users.Where(user => user.firstName == description.firstName).Single().password);
            Assert.Equal(description.password, users.Where(user => user.firstName == description.firstName).Single().password);
            clear();
        }

        [Fact]
        public void changeStats() {

            fillUp();
            Users description = new Users();
            verifyingUsers verifying = new verifyingUsers();
            description.firstName = "Aioanei";
            description.lastName = "Dragos";
            description.password = BCrypt.Net.BCrypt.HashPassword("testPassword");
            description.email = "dragosaioane1998@gmail.com";

            users = verifying.changeUserByEmail(users, description);

            Assert.Equal(description.firstName, users.Where(user => user.firstName == description.firstName).Single().firstName);
        }
    }
}