using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.Core.Entities
{
    public class Contact
    {
        [Key]
        public Guid IdContact { get; set; }
        
        [ForeignKey("Customer")] 
        
        public required Guid IdCustomer { get; set; }
        
        [StringLength(100)]
        public required string Name { get; set; }
        
        [StringLength(100)]
        public required string Position { get; set; }
        
        [StringLength(100)]
        public required string Email { get; set; }
        
        [StringLength(15)]
        public required string Phone { get; set; }
        
        [StringLength(500)]
        public string? Notes { get; set; }
        
        public DateTime? Created { get; set; }
        
        public DateTime? Updated { get; set; }

        [ForeignKey("CreatedByUser")]
        public Guid? CreatedById { get; set; }

        [ForeignKey("UpdatedByUser")]
        public Guid? UpdatedById { get; set; }

        // Navigation properties
        public Customer? Customer { get; set; }
        public User? CreatedByUser { get; set; }
        public User? UpdatedByUser { get; set; }
    }
}
