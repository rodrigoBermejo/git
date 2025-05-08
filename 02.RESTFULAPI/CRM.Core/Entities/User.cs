using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.Core.Entities
{
    public class User
    {
        [Key] 
        public Guid IdUser { get; set; }

        [StringLength(100)] 
        public required string UserName { get; set; }

        [StringLength(100)] 
        public required string UserDisplayName { get; set; }

        [StringLength(100)] 
        public required string Email { get; set; }

        public required EnumUserRole Role { get; set; }

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

    public enum EnumUserRole
    {
        Admin,
        Sales,
        Support,
        Manager,
        User
    }
}
