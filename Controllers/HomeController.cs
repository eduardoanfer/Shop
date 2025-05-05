using System.Threading.Tasks;
using Azure.Identity;
using Microsoft.AspNetCore.Mvc;
using Shop.Data;
using Shop.Models;


namespace Shop.Controllers;

[Route("v1")]
public class HomeController : Controller
{
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<dynamic>> Get([FromServices] DataContext context)
        {
            var employee = new User { Id = 1, Username = "robin", Password = "robin" };
            var manager = new User { Id = 2, Username = "batman", Password = "batman" };

            var category = new Category { Id = 1, Title = "Informática" };
            var product = new Product
            {
                Id= 1,
                Category = category,
                Title = "Mouse",
                Price = 299,
                Description = "Mouse Gamer"
            };

            context.Users.Add(employee);
            context.Users.Add(manager);
            context.Categories.Add(category);
            context.Products.Add(product);
            await context.SaveChangesAsync(); // ára salvar tudo

            return Ok(new
            {
                message = "Dados configurados"
            });
        }
    
}