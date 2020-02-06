using ModelLibrary.Services;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ModelLibrary.Models
{
    /// <summary>
    /// Classe de usuário contendo informações básicas para o login e autenticação desta aplicação.
    /// </summary>
    public class User : ModelBase
    {
        [MinLength(4, ErrorMessage = "Este campo precisa ter entre 4 e 50 caracteres.")]
        [MaxLength(50, ErrorMessage = "Este campo precisa ter entre 4 e 50 caracteres.")]
        public string Username { get; set; }

        [MinLength(8, ErrorMessage = "Este campo precisa ter no mínimo 8 caracteres.")]
        public string Password { get; private set; }

        [DefaultValue(true)]
        public bool Actived { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        public int RoleId { get; set; }
        public Role Role { get; set; }
        [MinLength(3, ErrorMessage = "Este campo precisa ter no mínimo 3 caracteres.")]
        public string LastName { get; set; }

        [EmailAddress(ErrorMessage = "Email inválido.")]
        [Required(ErrorMessage = "Este campo é obrigatório.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [MinLength(3, ErrorMessage = "Este campo precisa ter no mínimo 3 caracteres.")]
        public string Nickname { get; set; }

        public User(string name, string lastName, string description, string email, string nickname, string username, string password, int roleId)
        {
            var enc = new Encrypter();

            Name = name;
            LastName = lastName;
            Description = description;
            Email = email;
            Nickname = nickname;
            Username = username;
            Password = enc.Encrypt(password);
            RoleId = roleId;
            Actived = true;
            Updated = DateTime.UtcNow;
            if (Id == 0)
                Created = DateTime.UtcNow;
        }

        public void SetPassword(string newpass)
        {
            var enc = new Encrypter();
            Password = enc.Encrypt(newpass);
        }
    }
}
