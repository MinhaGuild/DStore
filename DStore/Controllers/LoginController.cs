using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Database.Context;
using DStore.Helpers;
using DStore.Models;
using DStore.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ModelLibrary.Models;
using ModelLibrary.Services;

namespace DStore.Controllers
{
    [Route("login")]
    public class LoginController : Controller
    {
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("")]
        public IActionResult Index([FromBody]LoginModel model, [FromServices]DatabaseContext context, IOptions<EmailSettings> emailSettings)
        {
            if (!string.IsNullOrEmpty(model.Email))
            {
                try
                {
                    var user = context.Users.Include(x => x.Role).FirstOrDefault(x =>
                    x.Email.ToLower().Equals(model.Email.ToLower()) && x.Actived);

                    if (user != null)
                    {
                        var nPass = new ChangePassword(user);
                        context.ChangePassword.Add(nPass);
                        context.SaveChanges();

                        return Ok(new { status = "success", message = "Você receberá em breve um email com a nova senha para realizar login na aplicação." });
                    }
                    else
                    {
                        return NotFound(new { status = "not found", message = "Email não encontrado." });
                    }
                }
                catch (Exception ex)
                {
                    return NotFound(new { status = "error", exception = ex, message = "Ocorreu um erro. Tente novamente mais tarde." });
                }
            }
            else
            {
                try
                {
                    var enc = new Encrypter();

                    var user = context.Users.Include(x => x.Role).FirstOrDefault(x =>
                    x.Username.ToLower().Equals(model.Username.ToLower()) &&
                    x.Password.Equals(enc.Encrypt(model.Password)) &&
                    x.Actived);

                    if (user == null)
                        return NotFound(new { message = "Usuário ou senha inválidos." });

                    var token = TokenService.GenerateToken(user, AppSettings.Secret);

                    return Ok(new { status = "success", user = new { role = new { id = user.RoleId, name = user.Role.Name }, nickname = user.Nickname, name = user.Name }, token });
                }
                catch (Exception ex)
                {
                    return NotFound(new { status = "error", exception = ex, message = "Usuário ou senha inválidos." });
                }
            }
        }
    }
}