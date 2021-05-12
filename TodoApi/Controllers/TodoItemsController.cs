using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TodoApi.Context;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    /////////////////////////////////////////////////////////////////////
    public class TodoItemsController : ControllerBase
    {

        #region Implementar Context Dependency Injection
        private readonly TodoItemContext _context;

        public TodoItemsController(TodoItemContext context)
        {
            this._context = context;
        }
        #endregion
        /////////////////////////////////////////////////////////////////////


        /////////////////////////////////////////////////////////////////////
        #region MostrarTodo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ModelItem>>> GetAllItems()
        {
            return await _context.modelitem.ToListAsync();
        }
        #endregion
    //////////////////////////////////////////////////////////////////////



    /////////////////////////////////////////////////////////////////////
        #region MostrarUno
        [HttpGet("{id}")]
        public async Task<ActionResult<ModelItem>> GetItem(long id)
        {
            var contentItem = await _context.modelitem.FindAsync(id);

            if (contentItem == null)
            {
                return NotFound();
            }
            return contentItem;
        }
        #endregion



        /////////////////////////////////////////////////////////////////////
        #region Actualizar

        [HttpPut("{id}")]
        public async Task<IActionResult> PutItem(long id, ModelItem _modelItem)

        {
            if (id != _modelItem.Id)
            {
                return BadRequest();
            }
            _context.Entry(_modelItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ModelItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        private bool ModelItemExists(long id)
        {
            return _context.modelitem.Any(e => e.Id == id);
        }
        #endregion
    /////////////////////////////////////////////////////////////////////


    /////////////////////////////////////////////////////////////////////
        #region  Crear
        [HttpPost]
        public async Task<ActionResult<ModelItem>> PostItem(ModelItem modelItem)
        {
            _context.modelitem.Add(modelItem);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAllItems), new { id = modelItem.Id }, modelItem);
        }
        #endregion
        /////////////////////////////////////////////////////////////////////



        /////////////////////////////////////////////////////////////////////
        #region Eliminar
        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteItem(long id)
        {
            var contentItem = await _context.modelitem.FindAsync(id);

            if (contentItem == null)
            {
                return NotFound();
            }
            _context.modelitem.Remove(contentItem);
            await _context.SaveChangesAsync();
            return NoContent();
            #endregion
        }
    /////////////////////////////////////////////////////////////////////
    
    }
}