using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public required decimal Price { get; set; }
        public required string Description { get; set; }
        public required string Category { get; set; }
        public required string Image { get; set; }
        public required RatttingDto Rating { get; set; }
    }
    public class RatttingDto
    {
        public required double Rate { get; set; }
        public required int Count { get; set; }
    }

}