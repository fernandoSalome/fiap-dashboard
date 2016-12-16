using Fiap.RH.Sistema.Contratacao.Dominio;
using Fiap.RH.Sistema.Contratacao.Persistencia.UnitsOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fiap.RH.Sistema.Contratacao.MCV.Web.Controllers
{
    public class AvaliacaoController : Controller
    {
        #region FIELDS
        private UnitOfWork _unit = new UnitOfWork();
        #endregion

        //GET Listar todos

        //GET Buscar (por nome e fabrica)

        //GET Editar

        //POST Editar


        #region STATIC
        public static void Cadastrar(int id, UnitOfWork unit)
        {
            var avaliacaoComportamental = new AvaliacaoComportamental()
            {
                Id = id
            };
            try
            {
                unit.AvaliacaoComportamentalRepository.Cadastrar(avaliacaoComportamental);
            }
            catch (Exception e)
            {
                throw new Exception("Não foi possível inserir uma avaliação comportamental" + e.Message);
            }
        }

        public static void Remover(int id, UnitOfWork unit)
        {
            try
            {
                unit.AvaliacaoComportamentalRepository.Remover(id);
            }
            catch (Exception e)
            {
                throw new Exception("Não foi possível remover a Avaliação Comportamental: " + e.Message);
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