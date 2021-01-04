using Xunit;
using MedBook_RazorPages.Models;
using MedBook_RazorPages.Resources;
using System.Linq;
using System.Collections.Generic;
using Xunit.Abstractions;
using MedBook_RazorPages.Pages;
using System;

namespace LoginRegisterTest

{
    public class LoginRegisterTest
    {
        private List<Users> users;

        private readonly ITestOutputHelper output;

        public LoginRegisterTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        void fillUp() {
            if (users == null) 
            {
                users = new List<Users>();
            }

            Users user1 = new Users();
            user1.FirstName = "Aioane";
            user1.LastName = "Dragos";
            user1.Email = "dragosaioane1998@gmail.com";
            user1.Password = BCrypt.Net.BCrypt.HashPassword("testPassword");

            Users user2 = new Users();
            user2.FirstName = "Gigiel";
            user2.LastName = "popa";
            user2.Email = "gigelpopa@gmail.com";
            user2.Password = BCrypt.Net.BCrypt.HashPassword("testPassword");

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

            description.Email = "draosaioane1999@gmail.com";
            description.FirstName = "Aioanei";
            description.LastName = "Dragos";
            description.Password = BCrypt.Net.BCrypt.HashPassword("testPassword");
            List<Users> list = verifying.getAllUsers(users, description);

            Assert.Equal("draosaioane1999@gmail.com", users.Where(user => user.FirstName == description.FirstName).Single().Email);
            clear();
        }

        [Fact]
        public void TestFailUserAdded()
        {
            
            fillUp();
            Users description = new Users();
            verifyingUsers verifying = new verifyingUsers();

            description.Email = "draosaioane1999@gmail.com";
            description.FirstName = "Aioanei";
            description.LastName = "Dragos";
            description.Password = BCrypt.Net.BCrypt.HashPassword("testPassword");
            users = verifying.getAllUsers(users, description);
            users = verifying.getAllUsers(users, description);
            int count = 0;

            foreach (var user in users) {
                if (user.Email == "draosaioane1999@gmail.com")
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

            description.FirstName = "Giovanni";
            description.LastName = "Andriasi";
            description.Email = "draosaioane1999@gmail.com";
            description.Password = BCrypt.Net.BCrypt.HashPassword("testPassword");
            
            users = verifying.getAllUsers(users, description);

            Assert.NotEqual("testPassword", users.Where(user => user.FirstName == description.FirstName).Single().Password);
            Assert.Equal(description.Password, users.Where(user => user.FirstName == description.FirstName).Single().Password);
            clear();
        }

        [Fact]
        public void changeStats() {

            fillUp();
            Users description = new Users();
            verifyingUsers verifying = new verifyingUsers();
            description.FirstName = "Aioanei";
            description.LastName = "Dragos";
            description.Password = BCrypt.Net.BCrypt.HashPassword("testPassword");
            description.Email = "dragosaioane1998@gmail.com";

            users = verifying.changeUserByEmail(users, description);

            Assert.Equal(description.FirstName, users.Where(user => user.FirstName == description.FirstName).Single().FirstName);
        }

        [Fact]
        public void StresEmailTest() {
            signupModel signup = new signupModel();
            for (int i = 0; i < 1; i++) {
                signup.SendVerificationLinkEmail("aioane.costin@yahoo.com", Guid.NewGuid().ToString(), "VerifyAccount");
            }
        }
    }
}