using MedBook_RazorPages.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedBook_RazorPages.Resources
{
    public class verifyingUsers
    {
        public Users getUserById(List<Users> users, int id) {
            return users.Where(user => user.id == id).FirstOrDefault();
        }

        public List<Users> getAllUsers(List<Users> users, Users description) {
            bool check = true;
            foreach (Users user in users)
            {
                //check = true;
                if (description.Email == default(string) || description.Email == user.Email)
                    check = false;
            }

            if (check)
                users.Add(description);
            return users;
        }

        public List<Users> changeUserByEmail(List<Users> users, Users description)
        {
            for (int i = 0; i < users.Count; i++)
                if (users[i].Email == description.Email)
                {
                    users[i] = description;
                }
            return users;
        }
    }
}
