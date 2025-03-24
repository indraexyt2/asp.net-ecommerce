using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;

namespace Domain.Entity.Product
{
    [Table("Products")]
    public class Product : BaseEntity
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }

        public List<Category> Category { get; set; } = new List<Category>();
    }
}
