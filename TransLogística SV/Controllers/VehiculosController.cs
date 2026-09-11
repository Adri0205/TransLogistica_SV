using Microsoft.AspNetCore.Mvc;
using TransLogística_SV.Models;
using TransLogística_SV.Repositories;

namespace TransLogística_SV.Controllers
{
    public class VehiculosController : Controller
    {
        private readonly VehiculoRepositorio _repositorio;

        public VehiculosController()
        {
            _repositorio = new VehiculoRepositorio();
        }

        // GET: Vehiculos
        public IActionResult Index()
        {
            var vehiculos = _repositorio.ObtenerTodos();
            return View(vehiculos);
        }

        // GET: Vehiculos/Details/ABC123
        public IActionResult Details(string placa)
        {
            if (string.IsNullOrEmpty(placa))
            {
                return NotFound();
            }

            var vehiculo = _repositorio.ObtenerPorPlaca(placa);

            if (vehiculo == null)
            {
                return NotFound();
            }

            return View(vehiculo);
        }

        // GET: Vehiculos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vehiculos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(
            string tipo,
            string placa,
            string marca,
            string modelo,
            int anio,
            double kilometraje,
            double capacidadCargaToneladas,
            string tipoCombustible,
            int cilindraje)
        {
            Vehiculo vehiculo;

            if (tipo == "Camion")
            {
                vehiculo = new Camion
                {
                    Placa = placa,
                    Marca = marca,
                    Modelo = modelo,
                    Anio = anio,
                    Kilometraje = kilometraje,
                    CapacidadCargaToneladas = capacidadCargaToneladas
                };
            }
            else if (tipo == "Automovil")
            {
                vehiculo = new Automovil
                {
                    Placa = placa,
                    Marca = marca,
                    Modelo = modelo,
                    Anio = anio,
                    Kilometraje = kilometraje,
                    TipoCombustible = tipoCombustible
                };
            }
            else if (tipo == "Motocicleta")
            {
                vehiculo = new Motocicleta
                {
                    Placa = placa,
                    Marca = marca,
                    Modelo = modelo,
                    Anio = anio,
                    Kilometraje = kilometraje,
                    Cilindraje = cilindraje
                };
            }
            else
            {
                ModelState.AddModelError("", "Debe seleccionar un tipo de vehículo.");
                return View();
            }

            if (!_repositorio.Agregar(vehiculo))
            {
                ModelState.AddModelError(
                    "Placa",
                    "Ya existe un vehículo registrado con esa placa."
                );

                return View();
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Vehiculos/Edit/ABC123
        public IActionResult Edit(string placa)
        {
            if (string.IsNullOrEmpty(placa))
            {
                return NotFound();
            }

            var vehiculo = _repositorio.ObtenerPorPlaca(placa);

            if (vehiculo == null)
            {
                return NotFound();
            }

            return View(vehiculo);
        }

        // POST: Vehiculos/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(
            string placaOriginal,
            string placa,
            string marca,
            string modelo,
            int anio,
            double kilometraje,
            double capacidadCargaToneladas,
            string tipoCombustible,
            int cilindraje)
        {
            var vehiculoActual = _repositorio.ObtenerPorPlaca(placaOriginal);

            if (vehiculoActual == null)
            {
                return NotFound();
            }

            Vehiculo vehiculo;

            if (vehiculoActual is Camion)
            {
                vehiculo = new Camion
                {
                    Placa = placa,
                    Marca = marca,
                    Modelo = modelo,
                    Anio = anio,
                    Kilometraje = kilometraje,
                    CapacidadCargaToneladas = capacidadCargaToneladas
                };
            }
            else if (vehiculoActual is Automovil)
            {
                vehiculo = new Automovil
                {
                    Placa = placa,
                    Marca = marca,
                    Modelo = modelo,
                    Anio = anio,
                    Kilometraje = kilometraje,
                    TipoCombustible = tipoCombustible
                };
            }
            else
            {
                vehiculo = new Motocicleta
                {
                    Placa = placa,
                    Marca = marca,
                    Modelo = modelo,
                    Anio = anio,
                    Kilometraje = kilometraje,
                    Cilindraje = cilindraje
                };
            }

            if (placaOriginal != placa &&
                _repositorio.ObtenerPorPlaca(placa) != null)
            {
                ModelState.AddModelError(
                    "Placa",
                    "La nueva placa ya está registrada."
                );

                return View(vehiculo);
            }

            if (placaOriginal != placa)
            {
                _repositorio.Eliminar(placaOriginal);
                _repositorio.Agregar(vehiculo);
            }
            else
            {
                _repositorio.Actualizar(placaOriginal, vehiculo);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Vehiculos/Delete/ABC123
        public IActionResult Delete(string placa)
        {
            if (string.IsNullOrEmpty(placa))
            {
                return NotFound();
            }

            var vehiculo = _repositorio.ObtenerPorPlaca(placa);

            if (vehiculo == null)
            {
                return NotFound();
            }

            return View(vehiculo);
        }

        // POST: Vehiculos/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string placa)
        {
            _repositorio.Eliminar(placa);

            return RedirectToAction(nameof(Index));
        }
    }
}