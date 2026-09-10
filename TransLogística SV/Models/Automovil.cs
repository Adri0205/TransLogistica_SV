using System;

namespace TransLogística_SV.Models
{
    public class Automovil : Vehiculo
    {
        // Tipo de combustible
        public string TipoCombustible { get; set; } = string.Empty;

        public override string TipoVehiculo => "Automóvil";

        // Fórmula: costo base + recargo si es Gasolina Premium + factor por kilometraje
        public override double CalcularCostoMantenimiento()
        {
            double costoBase = 400.0;
            double recargoPremium = TipoCombustible?.Equals("Gasolina Premium", StringComparison.OrdinalIgnoreCase) == true ? 150.0 : 0.0;
            double factorKilometraje = 0.01 * Kilometraje;

            return Math.Round(costoBase + recargoPremium + factorKilometraje, 2);
        }
    }
}
