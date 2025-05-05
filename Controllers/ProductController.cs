using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Shop.Data;
using Shop.Models;
using Shop.ViewModels;

namespace Shop.Controllers;

[Route("v1/Products")]
public class ProductController : ControllerBase
{
    [HttpGet]
    [Route("")]
    [AllowAnonymous]
    public async Task<ActionResult<List<Product>>> Get([FromServices] DataContext context)
    {
        var products = await context
            .Products
            .Include(p => p.Category)
            .AsNoTracking()
            .ToListAsync();
        //o include da um joyn na tabela de category e puxa aqui
        if (products == null)
        {
            return NotFound();
        }

        return products;
    }

    [HttpGet]
    [Route("{id:int}")]
    [AllowAnonymous]
    public async Task<ActionResult<Product>>
        Get([FromServices]
            DataContext context,
            int id)
    {
        var product = await context
            .Products
            .Include(x => x.Category)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
        return product;
    } 
    [HttpGet]
    [Route("categories/{id:int}")] //products/categories/1 por exempo
    [AllowAnonymous]
    public async Task<ActionResult<List<Product>>> GetByCategory([FromServices] DataContext context, int id)
    {
        var products = await context
            .Products
            .Include(x => x.Category)
            .AsNoTracking()
            .Where(x => x.CategoryId == id)
            .ToListAsync();
        return products;
        // processar todos os produtos onde o categoryID for igual ao id  e o tolist so usamos no final para listar.
    }

    [HttpPost]
    [Route("")]
    [Authorize(Roles = "employee")] // um funcionario pode criar produtos 
    public async Task<ActionResult<Product>> Post(
        [FromServices] DataContext context,
        [FromBody] ProductViewModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var product = new Product
        {
            Title = model.Title,
            Description = model.Description,
            Price = model.Price,
            CategoryId = model.CategoryId
        };

        context.Products.Add(product);
        await context.SaveChangesAsync();
        return Ok(product);
    }


}    