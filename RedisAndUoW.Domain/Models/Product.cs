using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisAndUoW.Domain.Models
{
    public partial class Product
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? PicUrl { get; set; }
        public string Status { get; set; } = null!;
        public double SellingPrice { get; set; }
        public double DiscountPrice { get; set; }
        public double TruePrice { get; set; }
        public Guid CategoryId { get; set; }

        public virtual Category Category { get; set; } = null!;
    }
}
