using System;

namespace TransLogística_SV.Models
{
    public abstract class Vehiculo
    {
        public string Placa { get; set; } = string.Empty;
        public string Marca { get; set; } = string.Empty;
        public string Modelo { get; set; } = string.Empty;
        public int Anio { get; set; }
        public double Kilometraje { get; set; }

        // Propiedad para identificar el tipo concreto en vistas
        public virtual string TipoVehiculo => "Vehículo";

        public abstract double CalcularCostoMantenimiento();

        public virtual string ObtenerResumen()
        {
            return $"{Marca} {Modelo} ({Anio}) - Placa: {Placa}";
        }
    }
}
