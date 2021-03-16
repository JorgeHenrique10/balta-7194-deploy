using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shop.Data;
using Shop.Models;

namespace Shop.Controllers
{
    [Route("v1")]
    public class ShopController : ControllerBase
    {
        [HttpPost]
        [Route("")]
        public async Task<ActionResult<dynamic>> Post([FromServices] DataContext context)
        {
            var manager = new User { Id = 1, UserName = "batman", Password = "batman", Role = "manager" };
            var employee = new User { Id = 2, UserName = "robin", Password = "robin", Role = "employee" };
            var category = new Category { Id = 1, Title = "Informática" };
            var product = new Product { Id = 1, Category = category, Title = "Mouse", Description = "Mouse sem fio", Price = 200 };
            var product2 = new Product { Id = 2, Category = category, Title = "Teclado", Description = "Teclado sem fio", Price = 200 };

            try
            {
                context.Users.Add(employee);
                context.Users.Add(employee);
                context.Categories.Add(category);
                context.Products.Add(product);
                context.Products.Add(product2);

                await context.SaveChangesAsync();

                return Ok(new { message = "Dados configurados com sucesso!" });

            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "Não foi possível inserir os dados iniciais", exeption = ex.Message });
            }


        }
    }
}