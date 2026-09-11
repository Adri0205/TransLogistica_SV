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
            return View(new TransLogística_SV.Models.VehiculoViewModel());
        }

        // POST: Vehiculos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TransLogística_SV.Models.VehiculoViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            Vehiculo vehiculo;

            if (vm.Tipo == "Camion")
            {
                vehiculo = new Camion
                {
                    Placa = vm.Placa,
                    Marca = vm.Marca,
                    Modelo = vm.Modelo,
                    Anio = vm.Anio,
                    Kilometraje = vm.Kilometraje,
                    CapacidadCargaToneladas = vm.CapacidadCargaToneladas ?? 0.0
                };
            }
            else if (vm.Tipo == "Automovil")
            {
                vehiculo = new Automovil
                {
                    Placa = vm.Placa,
                    Marca = vm.Marca,
                    Modelo = vm.Modelo,
                    Anio = vm.Anio,
                    Kilometraje = vm.Kilometraje,
                    TipoCombustible = vm.TipoCombustible
                };
            }
            else if (vm.Tipo == "Motocicleta")
            {
                vehiculo = new Motocicleta
                {
                    Placa = vm.Placa,
                    Marca = vm.Marca,
                    Modelo = vm.Modelo,
                    Anio = vm.Anio,
                    Kilometraje = vm.Kilometraje,
                    Cilindraje = vm.Cilindraje ?? 0
                };
            }
            else
            {
                ModelState.AddModelError("Tipo", "Debe seleccionar un tipo de vehículo.");
                return View(vm);
            }

            if (!_repositorio.Agregar(vehiculo))
            {
                ModelState.AddModelError(
                    "Placa",
                    "Ya existe un vehículo registrado con esa placa."
                );

                return View(vm);
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

            var vm = new TransLogística_SV.Models.VehiculoViewModel
            {
                Placa = vehiculo.Placa,
                Marca = vehiculo.Marca,
                Modelo = vehiculo.Modelo,
                Anio = vehiculo.Anio,
                Kilometraje = vehiculo.Kilometraje
            };

            if (vehiculo is Camion c)
            {
                vm.Tipo = "Camion";
                vm.CapacidadCargaToneladas = c.CapacidadCargaToneladas;
            }
            else if (vehiculo is Automovil a)
            {
                vm.Tipo = "Automovil";
                vm.TipoCombustible = a.TipoCombustible;
            }
            else if (vehiculo is Motocicleta m)
            {
                vm.Tipo = "Motocicleta";
                vm.Cilindraje = m.Cilindraje;
            }

            return View(vm);
        }

        // POST: Vehiculos/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string placaOriginal, TransLogística_SV.Models.VehiculoViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var vehiculoActual = _repositorio.ObtenerPorPlaca(placaOriginal);

            if (vehiculoActual == null)
            {
                return NotFound();
            }

            Vehiculo vehiculo;

            if (vm.Tipo == "Camion")
            {
                vehiculo = new Camion
                {
                    Placa = vm.Placa,
                    Marca = vm.Marca,
                    Modelo = vm.Modelo,
                    Anio = vm.Anio,
                    Kilometraje = vm.Kilometraje,
                    CapacidadCargaToneladas = vm.CapacidadCargaToneladas ?? 0.0
                };
            }
            else if (vm.Tipo == "Automovil")
            {
                vehiculo = new Automovil
                {
                    Placa = vm.Placa,
                    Marca = vm.Marca,
                    Modelo = vm.Modelo,
                    Anio = vm.Anio,
                    Kilometraje = vm.Kilometraje,
                    TipoCombustible = vm.TipoCombustible
                };
            }
            else
            {
                vehiculo = new Motocicleta
                {
                    Placa = vm.Placa,
                    Marca = vm.Marca,
                    Modelo = vm.Modelo,
                    Anio = vm.Anio,
                    Kilometraje = vm.Kilometraje,
                    Cilindraje = vm.Cilindraje ?? 0
                };
            }

            if (placaOriginal != vm.Placa &&
                _repositorio.ObtenerPorPlaca(vm.Placa) != null)
            {
                ModelState.AddModelError(
                    "Placa",
                    "La nueva placa ya está registrada."
                );

                return View(vm);
            }

            if (placaOriginal != vm.Placa)
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