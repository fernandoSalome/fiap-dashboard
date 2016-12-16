using Fiap.RH.Sistema.Contratacao.Persistencia.UnitsOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fiap.RH.Sistema.Contratacao.MCV.Web.Controllers
{
    public class AulaController : Controller
    {
        #region FIELDS
        private UnitOfWork _unit = new UnitOfWork();
        #endregion

        // GET: Aula
        public ActionResult Index()
        {
            return View();
        }

        #region STATIC
        public static void RemoverCandidatoAula(int idCandidato, UnitOfWork unit)
        {
            try
            {
                unit.CandidatoAulaRepository.Remover(idCandidato);
            }
            catch (Exception e)
            {
                throw new Exception("Não foi possível remover a lista de presença do candidato " 
                                   + idCandidato + ": " + e.Message);
            }
        }
        #endregion


        #region OVERRIDE
        protected override void Dispose(bool disposing)
        {
            _unit.Dispose();
            base.Dispose(disposing);
        }
        #endregion
    }
}