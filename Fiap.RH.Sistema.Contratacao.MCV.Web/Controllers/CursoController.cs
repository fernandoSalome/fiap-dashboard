using Fiap.RH.Sistema.Contratacao.Dominio;
using Fiap.RH.Sistema.Contratacao.MCV.Web.ViewModels;
using Fiap.RH.Sistema.Contratacao.Persistencia.UnitsOfWork;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Fiap.RH.Sistema.Contratacao.MCV.Web.Controllers
{
    public class CursoController : Controller
    {
        #region FIELDS
        private UnitOfWork _unit = new UnitOfWork();
        #endregion


        #region GET
        [HttpGet]
        public ActionResult Cadastrar(string mensagem)
        {
            var cursoViewModel = new CursoViewModel()
            {
                Mensagem = mensagem
            };
            return View(cursoViewModel);
        }

        [HttpGet]
        public ActionResult Listar()
        {
            var cursoViewModel = new CursoViewModel();
            try
            {
                cursoViewModel.Cursos = _unit.CursoRepository.Listar();
            }
            catch (Exception e)
            {
                cursoViewModel.Mensagem = "Erro ao buscar os cursos no banco de dados: " + e.Message;
            }

            return View(cursoViewModel);
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            Curso curso = null;
            var cursoViewModel = new CursoViewModel();

            try
            {
                curso = _unit.CursoRepository.BuscarPorId(id);
                cursoViewModel.Id = curso.Id;
                cursoViewModel.Nome = curso.Nome;
            }
            catch (Exception e)
            {
                cursoViewModel.Mensagem = "Erro ao buscar o curso no banco de dados para editar: " + e.Message;
            }

            return View(cursoViewModel);
        }
        #endregion


        #region POST
        [HttpPost]
        public ActionResult Cadastrar(CursoViewModel cursoViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (IsCursoDuplicado(cursoViewModel))
                    {
                        cursoViewModel.Mensagem = "Já existe um curso com este nome!";
                        return View(cursoViewModel);
                    }

                    var curso = new Curso()
                    {
                        Nome = cursoViewModel.Nome
                    };

                    _unit.CursoRepository.Cadastrar(curso);
                    _unit.Save();
                }
                catch (Exception e)
                {
                    cursoViewModel.Mensagem = "Erro ao cadastrar curso: " + e.Message;
                    return View(cursoViewModel);
                }
            }
            else return View(cursoViewModel);

            return RedirectToAction("Listar");
        }

        [HttpPost]
        public JsonResult Remover(int id)
        {
            try
            {
                _unit.CursoRepository.Remover(id);
                _unit.Save();
            }
            catch (Exception e)
            {
                return Json(new { status = false, mensagem = "Existe algum candidato associado a este curso?"
                                                           + " Se sim deve trocar antes de remover."
                                                           + " Erro ao remover: " + e.Message });
            }
            return Json(new { status = true });
        }

        [HttpPost]
        public ActionResult Editar(CursoViewModel cursoViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (IsCursoDuplicado(cursoViewModel))
                    {
                        cursoViewModel.Mensagem = "Já existe um curso com este nome!";
                        return View(cursoViewModel);
                    }

                    var curso = _unit.CursoRepository.BuscarPorId(cursoViewModel.Id);
                    curso.Nome = cursoViewModel.Nome;
                    _unit.CursoRepository.Editar(curso);
                    _unit.Save();
                }
                catch (Exception e)
                {
                    cursoViewModel.Mensagem = "Erro ao editar o curso: " + e.Message;
                    return View(cursoViewModel);
                }
            }
            else return View(cursoViewModel);

            return RedirectToAction("Listar");
        }
        #endregion


        #region PRIVATE
        public bool IsCursoDuplicado(CursoViewModel cursoViewModel)
        {
            var cursos = _unit.CursoRepository.BuscarPor(c => c.Nome == cursoViewModel.Nome);
            if (cursos.Any() && !(cursos.First().Id == cursoViewModel.Id)) return true;
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