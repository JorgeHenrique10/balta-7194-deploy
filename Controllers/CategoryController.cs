
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Models;

[Route("v1/categories")]
public class CategoryController : ControllerBase
{
    [HttpGet]
    [Route("{id:int}")]
    [AllowAnonymous]
    [ResponseCache(VaryByHeader = "User-Agent", Location = ResponseCacheLocation.Any, Duration = 30)] // Para colocar cach só na action especifica
    //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.none, NoStore = true)] //Se a aplicação estiver com cach e não colocar essa action com cach
    public async Task<ActionResult<Category>> GetById(int id, [FromServices] DataContext context)
    {
        try
        {
            var category = await context.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return Ok(category);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
    [HttpGet]
    [Route("")]
    [AllowAnonymous]
    public async Task<ActionResult<List<Category>>> Get([FromServices] DataContext context)
    {
        try
        {
            var categories = await context.Categories.AsNoTracking().ToListAsync();
            return Ok(categories);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpPost]
    [Route("")]
    [Authorize(Roles = "employee")]
    public async Task<ActionResult<List<Category>>> Post([FromBody] Category model, [FromServices] DataContext context)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            context.Categories.Add(model);
            await context.SaveChangesAsync();
            return Ok(model);

        }
        catch (Exception ex)
        {
            return BadRequest(new { message = "Não foi possivel inserir a categoria", exception = ex.Message });
        }
    }

    [HttpPut]
    [Route("{id:int}")]
    [Authorize(Roles = "employee")]
    public async Task<ActionResult<List<Category>>> Put(int id, [FromBody] Category model, [FromServices] DataContext context)
    {
        if (model.Id != id)
            return NotFound(new { message = "Categoria não encontrada" });

        if (!ModelState.IsValid)
            return BadRequest(new { message = ModelState });

        try
        {
            context.Entry<Category>(model).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return Ok(model);

        }
        catch (DbUpdateConcurrencyException)
        {
            return BadRequest(new { message = "Categoria já foi atualizada!" });
        }
        catch (Exception)
        {

            return BadRequest(new { message = "Erro ao atualizar a categoria!" });

        }

    }

    [HttpDelete]
    [Route("{id:int}")]
    [Authorize(Roles = "employee")]
    public async Task<ActionResult<List<Category>>> Delete(int id, [FromServices] DataContext context)
    {
        var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);

        if (category == null)
            return NotFound(new { message = "Categoria não encontrada!" });

        try
        {
            context.Categories.Remove(category);
            await context.SaveChangesAsync();
            return Ok(new { message = "Categoria removida com sucesso!" });
        }
        catch (System.Exception)
        {
            return BadRequest(new { message = "Não foi possível remover a Categoria" });
        }
    }
}