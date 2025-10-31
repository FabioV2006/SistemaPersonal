using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaWebDisbofar.Models;

namespace SistemaWebDisbofar.Controllers
{
    [Authorize] // <-- ¡ESTO ES CLAVE! Requiere login para TODO este controlador
    public class HomeController : Controller
    {
        // Cambiamos el DbContext al de la Distribuidora
        private readonly DistribuidoraDbContext _DBContext;

        public HomeController(DistribuidoraDbContext context)
        {
            _DBContext = context;
        }

        // Esta acción ahora es tu DASHBOARD
        public IActionResult Index()
        {
            // TODO: Aquí puedes cargar datos para el Dashboard y enviarlos a la vista
            // Ejemplo:
            // var ventasMes = _DBContext.Ventas
            //     .Where(v => v.FechaRegistro.Value.Month == DateTime.Now.Month && v.FechaRegistro.Value.Year == DateTime.Now.Year)
            //     .Sum(v => v.MontoTotal);

            // ViewBag.VentasMes = ventasMes;
            // ViewBag.ClientesActivos = _DBContext.Clientes.Count(c => c.Estado == true);

            return View();
        }

        // --- ¡ELIMINA LAS ACCIONES ANTIGUAS! ---
        // Elimina: Empleado_Detalle (GET y POST)
        // Elimina: Eliminar (GET y POST)

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
