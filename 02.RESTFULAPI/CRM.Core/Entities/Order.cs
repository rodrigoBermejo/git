using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Core.Entities
{
    public class Order
    {
        [Key]
        public Guid IdOrder { get; set; }

        [ForeignKey("Customer")]
        public required Guid IdCustomer { get; set; }
        
        public required DateTime OrderDate { get; set; }
        
        public required DateTime DeliveryDate { get; set; }

        [Precision(18, 2)] 
        public required decimal TotalAmount { get; set; }
        
        public required EnumOrderStatus Status { get; set; }
        
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

    public enum EnumOrderStatus
    {
        Open,
        Pending,
        Canceled,
        Completed,
        Closed
    }
}
