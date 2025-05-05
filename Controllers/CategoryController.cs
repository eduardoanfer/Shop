using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Necessário para SaveChangesAsync
using Shop.Data;
using Shop.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Shop.Controllers
{
    [Route("v1/categories")]
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
        [AllowAnonymous]
        [ResponseCache(VaryByHeader = "User-Agent", Location = ResponseCacheLocation.Any, Duration = 30)] 
        //duração de 30 minutos. 
        public async Task<ActionResult<List<Category>>> Get()
        {
          var categories = await _context.Categories.AsNoTracking().ToListAsync();
          return Ok(categories);
        }   
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Category>> GetbyId(int id,
        [FromServices]DataContext context)
        {
            var categories = await _context.Categories.AsNoTracking()
                .FirstOrDefaultAsync( x => x.Id == id);
          if (categories == null)
              return NotFound(new { message = "Categoria não encontrada" });
          return Ok(categories);
        }
        
        [HttpPost]
        [Route("")]
        [Authorize(Roles = "employee")]
        public async Task<ActionResult<Category>> Post(
            [FromBody] Category model, 
            [FromServices] DataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                //estou avizando que uma coisa mudou
                _context.Entry<Category>(model).State = EntityState.Added; 
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
        [Authorize(Roles = "employee")]
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
        public async Task<ActionResult<Category>> Delete(int id
            , [FromServices] DataContext context)
        {
            //x é uma categória
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (category == null)
                return NotFound(new { message = "Categoria não encontrada" });
            try
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
                return Ok(new {mensagem = "Categoria removido com sucesso" });
            }
            catch (Exception e)
            {
                return BadRequest(new{mensagem ="Não foi possível eliminar a Categoria: " + e.Message});
            }
        }
    }
}
