using System.ComponentModel.DataAnnotations;

namespace TransLogística_SV.Models
{
    public class VehiculoViewModel
    {
        [Required(ErrorMessage = "El tipo de vehículo es obligatorio.")]
        public string Tipo { get; set; } = string.Empty;

        [Required(ErrorMessage = "La placa es obligatoria.")]
        [StringLength(20, ErrorMessage = "La placa no puede exceder 20 caracteres.")]
        public string Placa { get; set; } = string.Empty;

        [Required(ErrorMessage = "La marca es obligatoria.")]
        public string Marca { get; set; } = string.Empty;

        [Required(ErrorMessage = "El modelo es obligatorio.")]
        public string Modelo { get; set; } = string.Empty;

        [Range(1900, 2100, ErrorMessage = "Año inválido.")]
        public int Anio { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Kilometraje inválido.")]
        public double Kilometraje { get; set; }

        // Campos específicos opcionales según el tipo
        [Range(0, double.MaxValue, ErrorMessage = "Capacidad inválida.")]
        public double? CapacidadCargaToneladas { get; set; }

        // Opcional: solo aplicable para Automovil
        public string? TipoCombustible { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Cilindraje inválido.")]
        public int? Cilindraje { get; set; }
    }
}
