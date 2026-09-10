using System;

namespace TransLogística_SV.Models
{
    public class Camion : Vehiculo
    {
        public double CapacidadCargaToneladas { get; set; }

        public override string TipoVehiculo => "Camión";

        // Ejemplo de fórmula: costo base + recargo por tonelada + factor por kilometraje
        public override double CalcularCostoMantenimiento()
        {
            double costoBase = 1200.0;
            double recargoPorTonelada = 250.0 * CapacidadCargaToneladas;
            double factorKilometraje = 0.02 * Kilometraje; // incremento por km

            return Math.Round(costoBase + recargoPorTonelada + factorKilometraje, 2);
        }

        // Sobrescribe ObtenerResumen incorporando la capacidad y la implementación base
        public override string ObtenerResumen()
        {
            return base.ObtenerResumen() + $" - Capacidad: {CapacidadCargaToneladas} t";
        }
    }
}
