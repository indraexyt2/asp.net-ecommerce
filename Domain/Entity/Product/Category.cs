using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;

namespace Domain.Entity.Product
{
    [Table("Categories")]
    public class Category : BaseEntity
    {
        public required string? Name { get; set; }

        public List<Product> Products { get; set;} = new List<Product>();
    }
}
