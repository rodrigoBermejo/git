using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.Core.Entities
{
    public class Activity
    {
        [Key]
        public Guid IdActivity { get; set; }
        
        [ForeignKey("Customer")]
        public required Guid IdCustomer { get; set; }
        
        [ForeignKey("Contact")]
        public required Guid IdContact { get; set; }
        
        public required EnumActivityType Type { get; set; }
        
        public required DateTime ActivityDate { get; set; }
        
        [StringLength(100)] 
        public required string Description { get; set; }
        
        public required EnumActivityStatus Status { get; set; }
        
        public DateTime? Created { get; set; }
        
        [ForeignKey("CreatedByUser")]
        public Guid? CreatedById { get; set; }
        
        [ForeignKey("UpdatedByUser")]
        public Guid? UpdatedById { get; set; }
        
        public DateTime? Updated { get; set; }
        
        // Navigation properties
        public Customer? Customer { get; set; }
        public Contact? Contact { get; set; }
        public User? CreatedByUser { get; set; }
        public User? UpdatedByUser { get; set; }
    }

    public enum EnumActivityType
    {
        Call,
        Meeting,
        Email,
        Task
    }

    public enum EnumActivityStatus
    {
        Open,
        Pending,
        Canceled,
        Closed
    }
}
