using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Models;

namespace Shop.Controllers;
[Route("Products")]
public class ProductController : ControllerBase
{
    [HttpGet]
    [Route("")]
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

    [Route("{id}")]
    public async Task<ActionResult<Product>> GetProducst(int id)
    {
        
    }
    
}