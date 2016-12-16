using Fiap.RH.Sistema.Contratacao.Dominio;
using Fiap.RH.Sistema.Contratacao.MCV.Web.ViewModels;
using Fiap.RH.Sistema.Contratacao.Persistencia.UnitsOfWork;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Fiap.RH.Sistema.Contratacao.MCV.Web.Controllers
{
    public class PerfilController : Controller
    {
        #region FIELDS
        private UnitOfWork _unit = new UnitOfWork();
        #endregion


        #region GET
        [HttpGet]
        public ActionResult Cadastrar(string mensagem)
        {
            var perfilViewModel = new PerfilViewModel()
            {
                Mensagem = mensagem
            };
            return View(perfilViewModel);
        }

        [HttpGet]
        public ActionResult Listar()
        {
            var perfilViewModel = new PerfilViewModel();
            try
            {
                perfilViewModel.Perfis = _unit.PerfilProfissionalRepository.Listar();
            }
            catch (Exception e)
            {
                perfilViewModel.Mensagem = "Erro ao buscar os perfis no banco de dados: " + e.Message;
            }
            return View(perfilViewModel);
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            PerfilProfissional perfil = null;
            var perfilViewModel = new PerfilViewModel();

            try
            {
                perfil = _unit.PerfilProfissionalRepository.BuscarPorId(id);
                perfilViewModel.Id = perfil.Id;
                perfilViewModel.Descricao = perfil.Descricao;
            }
            catch (Exception e)
            {
                perfilViewModel.Mensagem = "Erro ao buscar o perfil no banco de dados para editar: " + e.Message;
            }

            return View(perfilViewModel);
        }
        #endregion


        #region POST
        [HttpPost]
        public ActionResult Cadastrar(PerfilViewModel perfilViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (IsPerfilDuplicado(perfilViewModel))
                    {
                        perfilViewModel.Mensagem = "Já existe um perfil com este nome!";
                        return View(perfilViewModel);
                    }

                    var perfil = new PerfilProfissional()
                    {
                        Descricao = perfilViewModel.Descricao
                    };

                    _unit.PerfilProfissionalRepository.Cadastrar(perfil);
                    _unit.Save();
                }
                catch (Exception e)
                {
                    perfilViewModel.Mensagem = "Erro ao cadastrar perfil: " + e.Message;
                    return View(perfilViewModel);
                }
            }
            else return View(perfilViewModel);

            return RedirectToAction("Listar");
        }

        [HttpPost]
        public JsonResult Remover(int id)
        {
            try
            {
                _unit.PerfilProfissionalRepository.Remover(id);
                _unit.Save();
            }
            catch (Exception e)
            {
                return Json(new
                {
                    status = false,
                    mensagem = "Existe algum candidato associado a este perfil?"
                               + " Se sim deve trocar antes de remover."
                               + " Erro ao remover: " + e.Message
                });
            }
            return Json(new { status = true });
        }

        [HttpPost]
        public ActionResult Editar(PerfilViewModel perfilViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (IsPerfilDuplicado(perfilViewModel))
                    {
                        perfilViewModel.Mensagem = "Já existe um perfil com este nome!";
                        return View(perfilViewModel);
                    }

                    var perfil = _unit.PerfilProfissionalRepository.BuscarPorId(perfilViewModel.Id);
                    perfil.Descricao = perfilViewModel.Descricao;
                    _unit.PerfilProfissionalRepository.Editar(perfil);
                    _unit.Save();
                }
                catch (Exception e)
                {
                    perfilViewModel.Mensagem = "Erro ao editar o perfil: " + e.Message;
                    return View(perfilViewModel);
                }
            }
            else return View(perfilViewModel);

            return RedirectToAction("Listar");
        }
        #endregion


        #region PRIVATE
        public bool IsPerfilDuplicado(PerfilViewModel perfilViewModel)
        {
            var perfis = _unit.PerfilProfissionalRepository.BuscarPor(p => p.Descricao == perfilViewModel.Descricao);
            if (perfis.Any() && !(perfis.First().Id == perfilViewModel.Id)) return true;
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