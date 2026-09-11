using System.Collections.Generic;
using System.Linq;
using TransLogística_SV.Models;

namespace TransLogística_SV.Repositories
{
    public class VehiculoRepositorio
    {
        private static Dictionary<string, Vehiculo> _vehiculos =
            new Dictionary<string, Vehiculo>();

        // Agregar un vehículo
        public bool Agregar(Vehiculo vehiculo)
        {
            if (_vehiculos.ContainsKey(vehiculo.Placa))
            {
                return false;
            }

            _vehiculos.Add(vehiculo.Placa, vehiculo);
            return true;
        }

        // Para obtener todos los vehículos
        public List<Vehiculo> ObtenerTodos()
        {
            return _vehiculos.Values.ToList();
        }

        // Para obtener un vehículo por placa
        public Vehiculo? ObtenerPorPlaca(string placa)
        {
            if (_vehiculos.ContainsKey(placa))
            {
                return _vehiculos[placa];
            }

            return null;
        }

        // Actualiza un vehículo
        public bool Actualizar(string placa, Vehiculo vehiculo)
        {
            if (!_vehiculos.ContainsKey(placa))
            {
                return false;
            }

            _vehiculos[placa] = vehiculo;
            return true;
        }

        // Eliminar un vehículo
        public bool Eliminar(string placa)
        {
            if (!_vehiculos.ContainsKey(placa))
            {
                return false;
            }

            _vehiculos.Remove(placa);
            return true;
        }
    }
}
