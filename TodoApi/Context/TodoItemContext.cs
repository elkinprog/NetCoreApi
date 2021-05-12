using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.Context
{
    public class TodoItemContext: DbContext
    {

        public TodoItemContext(DbContextOptions<TodoItemContext> options )  
            : base (options)
        {
                
        }

        public DbSet<ModelItem> modelitem{ get; set; }

    }
}
