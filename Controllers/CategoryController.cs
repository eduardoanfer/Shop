using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Necessário para SaveChangesAsync
using Shop.Data;
using Shop.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.Controllers
{
    [Route("categories")]
    public class CategoryController : ControllerBase
    {
        private readonly DataContext _context;

        // Injetando o DataContext via construtor
        public CategoryController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Category>>> Get()
        {
            // Buscar do banco de dados
            var categories = await _context.Categories.ToListAsync();
            return Ok(categories);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Category>> Get(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
                return NotFound(new { message = "Categoria não encontrada" });

            return Ok(category);
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Category>> Post(
            [FromBody] Category model, 
            [FromServices] DataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _context.Entry<Category>(model).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return model;
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(new{mensagem 
                    = "Ocorreu um erro ao criar categoria: " + e.Message});
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<Category>> Put(int id,
            [FromBody] Category model, 
            [FromServices] DataContext context)
        {
            if (id != model.Id)
                return NotFound(new { message = "Categoria não encontrada" });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(model);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult<Category>> Delete(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
                return NotFound(new { message = "Categoria não encontrada" });

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return Ok(category);
        }
    }
}
