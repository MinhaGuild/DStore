using ModelLibrary.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ModelLibrary.Models
{
    public class ChangePassword
    {
        [Key]
        public int Id { get; private set; }
        public string Token { get; private set; }
        public string Email { get; private set; }

        public ChangePassword(User user)
        {
            var enc = new Encrypter();

            Email = user.Email;
            Token = enc.Encrypt(string.Format("{0}.{1}--{2}.{3}", user.Name, user.Nickname, user.Role.Name, user.Email).Replace(" ", ""));
        }
    }
}
