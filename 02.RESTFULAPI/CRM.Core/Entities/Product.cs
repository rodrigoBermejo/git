using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;

namespace CRM.Core.Entities
{
    public class Product
    {
        [Key]
        public Guid IdProduct { get; set; }
        
        [StringLength(100)] 
        public required string Name { get; set; }
        
        [StringLength(100)]
        public required string Description { get; set; }

        [Precision(18, 2)] 
        public required decimal Price { get; set; }
        
        public required int Stock { get; set; }
        
        public required EnumProductCategory Category { get; set; }
        
        public DateTime? Created { get; set; }
        
        public DateTime? Updated { get; set; }

        [ForeignKey("CreatedByUser")]
        public Guid? CreatedById { get; set; }

        [ForeignKey("UpdatedByUser")]
        public Guid? UpdatedById { get; set; }

        // Navigation properties
        public User? CreatedByUser { get; set; }
        public User? UpdatedByUser { get; set; }
    }

    public enum EnumProductCategory
    {
        Service,
        Product
    }
}
