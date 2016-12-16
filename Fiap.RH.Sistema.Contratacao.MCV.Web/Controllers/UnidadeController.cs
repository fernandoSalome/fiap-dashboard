using Fiap.RH.Sistema.Contratacao.Dominio;
using Fiap.RH.Sistema.Contratacao.MCV.Web.ViewModels;
using Fiap.RH.Sistema.Contratacao.Persistencia.UnitsOfWork;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Fiap.RH.Sistema.Contratacao.MCV.Web.Controllers
{
    public class UnidadeController : Controller
    {
        #region FIELDS
        private UnitOfWork _unit = new UnitOfWork();
        #endregion


        #region GET
        [HttpGet]
        public ActionResult Cadastrar(string mensagem)
        {
            var unidadeViewModel = new UnidadeViewModel()
            {
                Mensagem = mensagem
            };
            return View(unidadeViewModel);
        }

        [HttpGet]
        public ActionResult Listar()
        {
            var unidadeViewModel = new UnidadeViewModel();
            try
            {
                unidadeViewModel.Unidades = _unit.UnidadeRepository.Listar();
            }
            catch (Exception e)
            {
                unidadeViewModel.Mensagem = "Erro ao buscar as unidades no banco de dados: " + e.Message;
            }
            return View(unidadeViewModel);
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            Unidade unidade = null;
            var unidadeViewModel = new UnidadeViewModel();

            try
            {
                unidade = _unit.UnidadeRepository.BuscarPorId(id);
                unidadeViewModel.Id = unidade.Id;
                unidadeViewModel.Nome = unidade.Nome;
            }
            catch (Exception e)
            {
                unidadeViewModel.Mensagem = "Erro ao buscar a unidade no banco de dados para editar: " + e.Message;
            }

            return View(unidadeViewModel);
        }
        #endregion


        #region POST
        [HttpPost]
        public ActionResult Cadastrar(UnidadeViewModel unidadeViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (IsUnidadeDuplicada(unidadeViewModel))
                    {
                        unidadeViewModel.Mensagem = "Já existe uma unidade com este nome!";
                        return View(unidadeViewModel);
                    }

                    var unidade = new Unidade()
                    {
                        Nome = unidadeViewModel.Nome
                    };

                    _unit.UnidadeRepository.Cadastrar(unidade);
                    _unit.Save();
                }
                catch (Exception e)
                {
                    unidadeViewModel.Mensagem = "Erro ao cadastrar unidade: " + e.Message;
                    return View(unidadeViewModel);
                }
            }
            else return View(unidadeViewModel);

            return RedirectToAction("Listar");
        }

        [HttpPost]
        public JsonResult Remover(int id)
        {
            try
            {
                _unit.UnidadeRepository.Remover(id);
                _unit.Save();
            }
            catch (Exception e)
            {
                return Json(new
                {
                    status = false,
                    mensagem = "Existe algum candidato associado a esta unidade?"
                               + " Se sim deve trocar antes de remover."
                               + " Erro ao remover: " + e.Message
                });
            }
            return Json(new { status = true });
        }

        [HttpPost]
        public ActionResult Editar(UnidadeViewModel unidadeViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (IsUnidadeDuplicada(unidadeViewModel))
                    {
                        unidadeViewModel.Mensagem = "Já existe uma unidade com este nome!";
                        return View(unidadeViewModel);
                    }

                    var unidade = _unit.UnidadeRepository.BuscarPorId(unidadeViewModel.Id);
                    unidade.Nome = unidadeViewModel.Nome;
                    _unit.UnidadeRepository.Editar(unidade);
                    _unit.Save();
                }
                catch (Exception e)
                {
                    unidadeViewModel.Mensagem = "Erro ao editar a unidade: " + e.Message;
                    return View(unidadeViewModel);
                }
            }
            else return View(unidadeViewModel);

            return RedirectToAction("Listar");
        }
        #endregion


        #region PRIVATE
        public bool IsUnidadeDuplicada(UnidadeViewModel unidadeViewModel)
        {
            var unidades = _unit.UnidadeRepository.BuscarPor(a => a.Nome == unidadeViewModel.Nome);
            if (unidades.Any() && !(unidades.First().Id == unidadeViewModel.Id)) return true;
            return false;
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