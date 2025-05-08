using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;

namespace CRM.Core.Entities
{
    public class SalesOpportunity
    {
        [Key]
        public Guid IdSalesOpportunity { get; set; }

        [ForeignKey("Customer")]
        public required Guid IdCustomer { get; set; }

        [StringLength(100)]
        public required string Description { get; set; }
        
        public required DateTime BeginingDate { get; set; }
        
        public required DateTime EndDate { get; set; }

        [Precision(18, 2)] 
        public required decimal Amount { get; set; }
        
        public required EnumOpportunityStatus Status { get; set; }
        
        public required EnumOpportunityStage Stage { get; set; }
        
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
    }

    public enum EnumOpportunityStatus
    {
        Open,
        Closed,
        Win,
        Lost
    }

    public enum EnumOpportunityStage
    {
        Discovery,
        Proposal,
        Negotiation,
        Closed
    }
}
