using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Models;
using Shop.Services;

namespace Shop.Controllers
{
    [Route("v1/users")]
    public class UserController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        [Authorize(Roles = "manager")]
        public async Task<ActionResult<List<User>>> Get([FromServices] DataContext context)
        {
            try
            {
                var users = await context.Categories.AsNoTracking().ToListAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost]
        [Route("")]
        [AllowAnonymous]
        public async Task<ActionResult<User>> Post([FromBody] User model, [FromServices] DataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                model.Role = "employee";
                context.Users.Add(model);
                await context.SaveChangesAsync();

                model.Password = "";
                return Ok(model);
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possivel adicionar o usuário" });
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        [Authorize(Roles = "manager")]
        public async Task<ActionResult<User>> Put(int id, [FromBody] User model, [FromServices] DataContext context)
        {
            if (model.Id != id)
                return NotFound(new { message = "Usuario não encontrada" });

            if (!ModelState.IsValid)
                return BadRequest(new { message = ModelState });

            try
            {
                context.Entry<User>(model).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return Ok(model);
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest(new { message = "Usuario já foi atualizada!" });
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Erro ao atualizar o usuario!" });
            }

        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<dynamic>> Login([FromBody] User model, [FromServices] DataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var user = await context.Users.AsNoTracking().Where(u => u.UserName == model.UserName && u.Password == model.Password).FirstOrDefaultAsync();

                if (user == null)
                    return NotFound(new { message = "Usuario ou Senha invalidos!" });

                var token = TokenService.GenerateToken(user);
                user.Password = "";
                return Ok(new { usuario = user, token = token });

            }
            catch (System.Exception)
            {
                return BadRequest(new { message = "Não foi possivel efetuar o login" });
            }

        }
    }
}