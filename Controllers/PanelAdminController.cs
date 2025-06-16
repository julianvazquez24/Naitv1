using Microsoft.AspNetCore.Mvc;
using Naitv1.Helpers;

namespace Naitv1.Controllers
{
    public class PanelAdminController : Controller
    {
        private readonly ServicioDashboard _servicioDashboard;

        public PanelAdminController(ServicioDashboard servicioDashboard)
        {
            _servicioDashboard = servicioDashboard;
        }

        public IActionResult Index()
        {
            if ( UsuarioLogueado.esAdmin(HttpContext.Session))
            {
                DashboardData datos = _servicioDashboard.ObtenerMetrics();
                return View("Dashboard", datos);
            }
            else {
                return RedirectToAction("Index", "Home");
            }
            
        }
    }
}