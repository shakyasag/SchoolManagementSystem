using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using SchoolnManagementSystem.Domain.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SchoolManagementSystem.Presistance
{
    public   class Seeder
    {
        public static void seedusers(UserManager<User> usermanager, RoleManager<Role> rolemanager)
        {
            if (!usermanager.Users.Any())
            {
                var userdata = File.ReadAllText("../SchoolManagementSystem.Presistance/user.json");
                var users = JsonConvert.DeserializeObject<List<User>>(userdata);
                foreach (var user in users)
                {
                    usermanager.CreateAsync(user, "password").Wait();
                    usermanager.AddToRoleAsync(user, "member");
                }
                var role = new List<Role>
                {
                    new Role{Name="Admin" },
                    new Role{Name="User" }
                   

                };
                foreach (var roles in role)
                {
                    rolemanager.CreateAsync(roles).Wait();
                }
                var admin = new User
                {
                    UserName = "admin"
                };

                var result = usermanager.CreateAsync(admin, "password").Result;
                if (result.Succeeded)
                {
                    var admins = usermanager.FindByNameAsync("admin").Result;
                    usermanager.AddToRolesAsync(admins, new[] { "admin", "member" });
                }

            }
        }
    }
}
