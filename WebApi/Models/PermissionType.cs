using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    [Table("TIPO_PERMISOS")]
    public class PermissionType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Descripcion { get; set; }

        // Navigation property for related Permisos
        public ICollection<Permission> ? Permisos { get; set; }
    }
}
