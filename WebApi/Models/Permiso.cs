using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApi.Models;


namespace WebApi.Models
{
    [Table("PERMISOS")]
    public class Permiso
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string NombreEmpleado { get; set; }

        [Required]
        [MaxLength(100)]
        public string ApellidoEmpleado { get; set; }

        [ForeignKey("TipoPermisoNavigation")]
        public int TipoPermisoId { get; set; }

        [Required]
        public TipoPermiso TipoPermiso { get; set; }
        [Required]
        public DateTime FechaPermiso { get; set; }
    }
}