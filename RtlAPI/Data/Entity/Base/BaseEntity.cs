using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RtlAPI.Data.Entity.Base
{
    public abstract class BaseEntity<TKeyType>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public TKeyType Id { get; set; }

        [Column]
        public bool IsDeleted { get; set; } = false;

        [Column]
        public DateTime CreatedDate { get; set; }

        [Column]
        public DateTime UpdatedDate { get; set; }
    }
}