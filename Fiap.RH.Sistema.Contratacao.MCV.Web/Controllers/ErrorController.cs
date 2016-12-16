using System.Web.Mvc;

namespace Fiap.RH.Sistema.Contratacao.MCV.Web.Controllers
{
    public class ErrorController : Controller
    {    
        public ActionResult Default()
        {
            return View();
        }
        public ActionResult NotFound()
        {
            return View();
        }
    }
}