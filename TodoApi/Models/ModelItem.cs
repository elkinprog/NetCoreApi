

using System;
using System.Threading.Tasks;

namespace TodoApi.Models
{
    public class ModelItem
    {

        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
    }
}
