using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Models;
using System; 
using Microsoft.AspNetCore.Authorization;
using System.Linq; 
using Shop.Services;

namespace Shop.Controllers;

[Route("v1/users")]
public class UserController : Controller
{
    [HttpGet]
    [Route("")]
    [Authorize(Roles = "manager")]
    public async Task<ActionResult<List<User>>> Get([FromServices] DataContext context)
    {
        var users = await context
            .Users
            .AsNoTracking()
            .ToListAsync();
        return users;
    }

    [HttpPost]
    [Route("")]
    [AllowAnonymous] //
    public async Task<ActionResult<User>> Post(
        [FromServices] DataContext context,
        [FromBody] User model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            model.Role = "employee"; // força o usuário sempre ser funcinário
            context.Users.Add(model); // se nao adiciona o usuario 
            await context.SaveChangesAsync(); // salva as mudanças
            model.Password = ""; // esconde a senha, mas é melhor incriptar 
            return model;
        }
        catch (Exception e) // se não puder criar um usuário 
        {
             return BadRequest(new { message = "Não foi possível criar o usuário." });
        }
    }

    [HttpPut]
    [Route("{id:int}")]
    [Authorize(Roles = "manager")]
    public async Task<ActionResult<User>> Put(
        [FromServices] DataContext context,
        int id,
        [FromBody] User model
    )
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (id != model.Id)
        {
            return NotFound(new { message = "Usuário não encontrada" }); 
        }

        try
        {
            context.Entry(model).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return model;
        }
        catch (Exception e)
        {
            return BadRequest(new { message = "Não foi possível criar o usuário" });
        }
    }

    [HttpPost]
    [Route("login")]
    public async Task<ActionResult<dynamic>> Authenticate( // as vezes retorna um usuario  e as vezes nada ( dynamic)
        [FromServices] DataContext context,
        [FromBody] User model)
    {
        var user = await context.Users
            .AsNoTracking() 
            .Where(p => p.Username == model.Username && p.Password == model.Password)
            .FirstOrDefaultAsync(); ; 
        //declarei que meu p -> seria o user ali para puxar ele na pesquisa
        if (user == null)
        {
            return NotFound(new { message = "Usuário ou senha inválidos" });
        }
        var token = TokenService.GenerateToken(user);
        return new
        {
            user = user,
            token = token
        };
    }
    [HttpGet]
    [Route("anonimo")]
    [AllowAnonymous] //Permite acesso anonimo a um metodo
    public string Anonimo() => "Anonimo";   
    
    
    [HttpGet]
    [Route("autenticado")]
    [Authorize] // somente usuario autentificado pode usar1
    public string Autenticado() => "Autenticado";
    
    [HttpGet]
    [Route("funcionario")]
    [Authorize(Roles = "employee")] // somente usuario autentificado pode usar1
    public string Funcionario() => "Funcionario";
    
    [HttpGet]
    [Route("gerente")]
    [Authorize(Roles = "manager")]
    public string Gerente() => "Gerente";   
    
  
    
}
