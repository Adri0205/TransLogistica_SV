using System;

namespace TransLogística_SV.Models
{
    public class Motocicleta : Vehiculo
    {
        public int Cilindraje { get; set; }

        public override string TipoVehiculo => "Motocicleta";

        // Fórmula: costo base reducido, recargo si cilindraje > 500cc, y factor por kilometraje
        public override double CalcularCostoMantenimiento()
        {
            double costoBase = 180.0;
            double recargoCilindraje = Cilindraje > 500 ? 120.0 : 0.0;
            double factorKilometraje = 0.005 * Kilometraje;

            return Math.Round(costoBase + recargoCilindraje + factorKilometraje, 2);
        } l
    }
}
