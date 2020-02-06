using ModelLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Database.Context
{
    public class DbInitializer
    {
        public static void Initialize(DatabaseContext context)
        {
            context.Database.EnsureCreated();

            if (!context.Roles.Any())
            {
                var admrole = new Role("Administrator", "Role apricada apenas para o administrador da aplicação.");
                var editorrole = new Role("Administrator", "Role apricada apenas para o administrador da aplicação.");
                var userrole = new Role("Administrator", "Role apricada apenas para o administrador da aplicação.");
                context.Roles.Add(admrole);
                context.Roles.Add(editorrole);
                context.Roles.Add(userrole);

                context.SaveChanges();
            }

            if (!context.Users.Any())
            {
                var user = new User(
                    "Luan",
                    "Fauth",
                    "luan.fauth@gmail.com",
                    "Usuário administrador controlado e criado para gerir a aplicação.",
                    "Delonge",
                    "luandelonge",
                    "Senh@1",
                    context.Roles.FirstOrDefault(x => x.Name.Equals("Administrator")).Id);

                context.Users.Add(user);

                context.SaveChanges();
            }
        }
    }
}
