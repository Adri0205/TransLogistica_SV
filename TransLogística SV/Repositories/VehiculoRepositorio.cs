using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using TransLogística_SV.Models;

namespace TransLogística_SV.Repositories
{
    public class VehiculoRepositorio
    {
        private static ConcurrentDictionary<string, Vehiculo> _vehiculos =
            new ConcurrentDictionary<string, Vehiculo>();

        // Agregar un vehículo
        public bool Agregar(Vehiculo vehiculo)
        {
            return _vehiculos.TryAdd(vehiculo.Placa, vehiculo);
        }

        // Para obtener todos los vehículos
        public List<Vehiculo> ObtenerTodos()
        {
            return _vehiculos.Values.ToList();
        }

        // Para obtener un vehículo por placa
        public Vehiculo? ObtenerPorPlaca(string placa)
        {
            if (_vehiculos.TryGetValue(placa, out var v))
            {
                return v;
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

            _vehiculos[placa] = vehiculo; // indexer es seguro en ConcurrentDictionary
            return true;
        }

        // Eliminar un vehículo
        public bool Eliminar(string placa)
        {
            return _vehiculos.TryRemove(placa, out _);
        }
    }
}
