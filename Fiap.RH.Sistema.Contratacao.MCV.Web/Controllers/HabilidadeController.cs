using System;
using System.Linq;
using System.Web.Mvc;
using Fiap.RH.Sistema.Contratacao.Persistencia.UnitsOfWork;
using Fiap.RH.Sistema.Contratacao.MCV.Web.ViewModels;
using Fiap.RH.Sistema.Contratacao.Dominio;


namespace Fiap.RH.Sistema.Contratacao.MCV.Web.Controllers
{
    public class HabilidadeController : Controller
    {
        #region FIELDS
        private UnitOfWork _unit = new UnitOfWork();
        #endregion


        #region GET
        [HttpGet]
        public ActionResult Cadastrar(string mensagem)
        {
            var habilidadeViewModel = new HabilidadeViewModel()
            {
                Mensagem = mensagem
            };
            return View(habilidadeViewModel);
        }

        [HttpGet]
        public ActionResult Listar()
        {
            var habilidadeViewModel = new HabilidadeViewModel();
            try
            {
                habilidadeViewModel.Habilidades = _unit.HabilidadeRepository.Listar();
            }
            catch (Exception e)
            {
                habilidadeViewModel.Mensagem = "Erro ao buscar as habilidades no banco de dados: " + e.Message;
            }
            return View(habilidadeViewModel);
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            Habilidade habilidade = null;
            var habilidadeViewModel = new HabilidadeViewModel();

            try
            {
                habilidade = _unit.HabilidadeRepository.BuscarPorId(id);
                habilidadeViewModel.Id = habilidade.Id;
                habilidadeViewModel.Nome = habilidade.Nome;
            }
            catch (Exception e)
            {
                habilidadeViewModel.Mensagem = "Erro ao buscar a habilidade no banco de dados para editar: " + e.Message;
            }

            return View(habilidadeViewModel);
        }
        #endregion


        #region POST
        [HttpPost]
        public ActionResult Cadastrar(HabilidadeViewModel habilidadeViewModel)
        {
            if (ModelState.IsValid)
            {
               try
               {
                    if (IsHabilidadeDuplicada(habilidadeViewModel))
                    {
                        habilidadeViewModel.Mensagem = "Já existe uma habilidade com este nome!";
                        return View(habilidadeViewModel);
                    }

                    var habilidade = new Habilidade()
                    {
                        Nome = habilidadeViewModel.Nome
                    };

                    _unit.HabilidadeRepository.Cadastrar(habilidade);
                    _unit.Save();
               }
               catch (Exception e)
               {
                    habilidadeViewModel.Mensagem = "Erro ao cadastrar habilidade: " + e.Message;
                    return View(habilidadeViewModel);
               }
            }
            else return View(habilidadeViewModel);

            return RedirectToAction("Listar");
        }

        [HttpPost]
        public JsonResult Remover(int id)
        {
            try
            {
                _unit.HabilidadeRepository.Remover(id);
                _unit.Save();
            }
            catch (Exception e)
            {
                return Json(new
                {
                    status = false,
                    mensagem = "Existe algum relatório de candidato associado a esta habilidade?"
                               + " Se sim deve trocar antes de remover."
                               + " Erro ao remover: " + e.Message
                });
            }
            return Json(new { status = true });
        }

        [HttpPost]
        public ActionResult Editar(HabilidadeViewModel habilidadeViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (IsHabilidadeDuplicada(habilidadeViewModel))
                    {
                        habilidadeViewModel.Mensagem = "Já existe uma habilidade com este nome!";
                        return View(habilidadeViewModel);
                    }

                    var habilidade = _unit.HabilidadeRepository.BuscarPorId(habilidadeViewModel.Id);
                    habilidade.Nome = habilidadeViewModel.Nome;
                    _unit.HabilidadeRepository.Editar(habilidade);
                    _unit.Save();
                }
                catch (Exception e)
                {
                    habilidadeViewModel.Mensagem = "Erro ao editar a habilidade: " + e.Message;
                    return View(habilidadeViewModel);
                }
            }
            else return View(habilidadeViewModel);

            return RedirectToAction("Listar");
        }
        #endregion


        #region PRIVATE
        public bool IsHabilidadeDuplicada(HabilidadeViewModel habilidadeViewModel)
        {
            var habilidades = _unit.HabilidadeRepository.BuscarPor(a => a.Nome == habilidadeViewModel.Nome);
            if (habilidades.Any() && !(habilidades.First().Id == habilidadeViewModel.Id)) return true;
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
