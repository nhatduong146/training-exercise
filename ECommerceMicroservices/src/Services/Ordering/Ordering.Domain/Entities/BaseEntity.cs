using System.ComponentModel.DataAnnotations;

namespace Ordering.Domain.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }

        public bool IsDeleted { get; set; } = false;

        public string CreatedBy { get; set; }

        public DateTimeOffset? CreatedOn { get; set; } = DateTimeOffset.UtcNow;

        public string UpdatedBy { get; set; }

        public DateTimeOffset? UpdatedOn { get; set; }

        public string DeletedBy { get; set; }

        public DateTimeOffset? DeletedOn { get; set; }
    }
}
