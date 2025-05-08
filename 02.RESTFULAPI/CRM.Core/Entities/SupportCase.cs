using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.Core.Entities
{
    public class SupportCase
    {
        [Key]
        public Guid IdSupportCase { get; set; }
        
        [ForeignKey("Customer")]
        public required Guid IdCustomer { get; set; }

        [ForeignKey("Contact")]
        public required Guid IdContact { get; set; }
        
        [StringLength(100)] 
        public required string Title { get; set; }
        
        [StringLength(100)] 
        public required string Description { get; set; }
        
        public required DateTime StartDate { get; set; }
        
        public required DateTime EndDate { get; set; }
        
        public required EnumSupportCasePriority Priority { get; set; }
        
        public required EnumSupportCaseStatus Status { get; set; }
        
        public DateTime? Created { get; set; }
        
        public DateTime? Updated { get; set; }

        [ForeignKey("CreatedByUser")]
        public Guid? CreatedById { get; set; }

        [ForeignKey("UpdatedByUser")]
        public Guid? UpdatedById { get; set; }

        // Navigation properties
        public User? CreatedByUser { get; set; }
        public User? UpdatedByUser { get; set; }
        public Customer? Customer { get; set; }
        public Contact? Contact { get; set; }
    }

    public enum EnumSupportCasePriority
    {
        Low,
        Medium,
        High
    }

    public enum EnumSupportCaseStatus
    {
        Open,
        Pending,
        Canceled,
        Closed
    }
}
