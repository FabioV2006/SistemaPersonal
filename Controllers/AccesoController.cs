using System.Security.Claims;
using SistemaWebDisbofar.Models; // Para acceder a DistribuidoraDbContext y Usuario
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Para usar FirstOrDefaultAsync

namespace CRUDCORE.Controllers
{
    public class AccesoController : Controller
    {
        private readonly DistribuidoraDbContext _context;

        // Inyectamos el DbContext
        public AccesoController(DistribuidoraDbContext context)
        {
            _context = context;
        }

        // --- MUESTRA LA PÁGINA DE LOGIN (GET) ---
        [HttpGet]
        public IActionResult Login()
        {
            // Si el usuario ya está logueado, lo mandamos al Home (Dashboard)
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // --- PROCESA EL INICIO DE SESIÓN (POST) ---
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string documento, string clave)
        {
            // 1. Validar en la base de datos (¡IMPORTANTE: Nunca guardes claves en texto plano!)
            //    Este código asume que la clave está en texto plano, lo cual es inseguro.
            //    En un proyecto real, deberías "hashear" la clave al guardarla y al compararla.
            var usuario = await _context.Usuarios
                                .Include(u => u.IdRolNavigation) // Carga el Rol asociado
                                .FirstOrDefaultAsync(u => u.Documento == documento && u.Clave == clave && u.Estado == true);

            if (usuario == null)
            {
                ViewData["Error"] = "Usuario o contraseña incorrectos, o usuario inactivo.";
                return View();
            }

            // 2. Crear los "Claims" (la "identificación" del usuario)
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuario.NombreCompleto),
                new Claim("Documento", usuario.Documento),
                new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString()),
                new Claim(ClaimTypes.Role, usuario.IdRolNavigation.Descripcion) // ¡Guardamos el Rol!
            };

            // 3. Crear la identidad y el principal
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                AllowRefresh = true,
                // IsPersistent = true, // Para la opción "Recordarme"
            };

            // 4. Iniciar Sesión (Esto crea la cookie encriptada)
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            // 5. Redirigir al Dashboard (HomeController)
            return RedirectToAction("Index", "Home");
        }

        // --- ACCIÓN DE CERRAR SESIÓN ---
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            // Borra la cookie de autenticación
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Acceso");
        }
    }
}