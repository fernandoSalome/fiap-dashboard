using Fiap.RH.Sistema.Contratacao.Dominio;
using Fiap.RH.Sistema.Contratacao.MCV.Web.ViewModels;
using Fiap.RH.Sistema.Contratacao.Persistencia.UnitsOfWork;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Fiap.RH.Sistema.Contratacao.MCV.Web.Controllers
{
    public class CandidatoController : Controller
    {
        #region FIELDS
        private UnitOfWork _unit = new UnitOfWork();
        #endregion

        #region GET
        [HttpGet]
        public ActionResult Buscar(string nomeCandidato, int? idFabrica)
        {
            var listar = _unit.CandidatoRepository.BuscarPor(a =>
                a.Nome.Contains(nomeCandidato) && (a.IdFabrica == idFabrica || idFabrica == null));

            return View(listar);
        }

        [HttpGet]
        public ActionResult Cadastrar(string mensagem)
        {
            var viewModel = new CandidatoViewModel()
            {
                Mensagem = mensagem,
                UnidadeSelectList =  ListarUnidades(),
                FabricaSelectList = ListarFabricas(),
                PerfilSelectList = ListarPerfil(),
                CursoSelectList = ListarCursos()
            };
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Listar()
        {
            var viewModel = new CandidatoViewModel()
            {
                UnidadeSelectList = ListarUnidades(),
                FabricaSelectList = ListarFabricas(),
                PerfilSelectList = ListarPerfil(),
                CursoSelectList = ListarCursos(),
                Candidatos = _unit.CandidatoRepository.Listar()
            };
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            Candidato candidato = null;
            var candidatoViewModel = new CandidatoViewModel();
            try{
                candidato = _unit.CandidatoRepository.BuscarPorId(id);
                candidatoViewModel.Id = candidato.Id;
                candidatoViewModel.Nome = candidato.Nome;
                candidatoViewModel.Publico = candidato.Publico;
                candidatoViewModel.Email = candidato.Email;
                candidatoViewModel.Residencial = candidato.Residencial;
                candidatoViewModel.Celular = candidato.Celular;
                candidatoViewModel.Cv = candidato.Cv;
                candidatoViewModel.Confirmacao = candidato.Confirmacao;
                candidatoViewModel.ProvaTecnica = candidato.ProvaTecnica;
                candidatoViewModel.Nota = candidato.Nota;
                candidatoViewModel.Rm = candidato.Rm;
                candidatoViewModel.UnidadeSelectList = ListarUnidades();
                candidatoViewModel.FabricaSelectList = ListarFabricas();
                candidatoViewModel.CursoSelectList = ListarCursos();
                candidatoViewModel.PerfilSelectList = ListarPerfil();

                candidatoViewModel.UnidadeId = candidato.IdUnidade;
                candidatoViewModel.FabricaId = candidato.IdFabrica;
                candidatoViewModel.CursoId = candidato.IdCurso;
                candidatoViewModel.PerfilId = candidato.IdPerfilProfissional;
            }
            catch(Exception e)
            {
                candidatoViewModel.Mensagem = "Erro ao buscar o candidato no banco de dados para editar: " + e.Message;
            }
            return View(candidatoViewModel);
        }
        #endregion

        #region POST
        [HttpPost]
        public ActionResult Cadastrar(CandidatoViewModel candidatoviewmodel)
        {
            if (ModelState.IsValid)
            {
                var candidato = new Candidato()
                {
                    Nome = candidatoviewmodel.Nome,
                    Publico = candidatoviewmodel.Publico,
                    Email = candidatoviewmodel.Email,
                    Residencial = candidatoviewmodel.Residencial,
                    Celular = candidatoviewmodel.Celular,
                    Cv = candidatoviewmodel.Cv,
                    Confirmacao = candidatoviewmodel.Confirmacao,
                    ProvaTecnica = candidatoviewmodel.ProvaTecnica,
                    Nota = candidatoviewmodel.Nota,
                    Rm = candidatoviewmodel.Rm,
                    IdUnidade = candidatoviewmodel.UnidadeId,
                    IdFabrica = candidatoviewmodel.FabricaId,
                    IdCurso = candidatoviewmodel.CursoId,
                    IdPerfilProfissional = candidatoviewmodel.PerfilId
                };
                if (FabricaController.IsCandidatoAprovado(candidatoviewmodel.Nota, candidatoviewmodel.FabricaId, _unit))
                {
                    AprovaCandidato(candidato, _unit);
                }
                else
                {
                    ReprovaCandidato(candidato, _unit);
                }

                _unit.CandidatoRepository.Cadastrar(candidato);

                try
                {
                    _unit.Save();
                }
                catch(Exception e)
                {
                    candidatoviewmodel.Mensagem = "Erro - " + e.Message;
                    candidatoviewmodel.UnidadeSelectList = ListarUnidades();
                    candidatoviewmodel.FabricaSelectList = ListarFabricas();
                    candidatoviewmodel.CursoSelectList = ListarCursos();
                    candidatoviewmodel.PerfilSelectList = ListarPerfil();
                    return View(candidatoviewmodel);
                }
                return RedirectToAction("Cadastrar", new { mensagem = "Candidato Cadastrado!" });
            }
            else
            {
                candidatoviewmodel.UnidadeSelectList = ListarUnidades();
                candidatoviewmodel.FabricaSelectList = ListarFabricas();
                candidatoviewmodel.CursoSelectList = ListarCursos();
                candidatoviewmodel.PerfilSelectList = ListarPerfil();
                return View(candidatoviewmodel);
            }
        }

        [HttpPost]
        public ActionResult Editar(CandidatoViewModel candidatoViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (IsCandidatoDuplicado(candidatoViewModel))
                    {
                        candidatoViewModel.Mensagem = "Já existe um candidato com este nome!";
                        return View(candidatoViewModel);
                    }

                    var candidato = _unit.CandidatoRepository.BuscarPorId(candidatoViewModel.Id);

                    candidato.Nome = candidatoViewModel.Nome;
                    candidato.Publico = candidatoViewModel.Publico;
                    candidato.Email = candidatoViewModel.Email;
                    candidato.Residencial = candidatoViewModel.Residencial;
                    candidato.Celular = candidatoViewModel.Celular;
                    candidato.Cv = candidatoViewModel.Cv;
                    candidato.Confirmacao = candidatoViewModel.Confirmacao;
                    candidato.ProvaTecnica = candidatoViewModel.ProvaTecnica;
                    candidato.Nota = candidatoViewModel.Nota;
                    candidato.Rm = candidatoViewModel.Rm;

                    candidato.IdUnidade = candidatoViewModel.UnidadeId;
                    candidato.IdCurso = candidatoViewModel.CursoId;
                    candidato.IdFabrica = candidatoViewModel.FabricaId;
                    candidato.IdPerfilProfissional = candidatoViewModel.PerfilId;

                    _unit.CandidatoRepository.Editar(candidato);
                    _unit.Save();
                }
                catch (Exception e)
                {
                    candidatoViewModel.Mensagem = "Erro ao editar o candidato: " + e.Message;
                    return View(candidatoViewModel);
                }
            }
            else return View(candidatoViewModel);

            return RedirectToAction("Listar");
        }

        [HttpPost]
        public ActionResult Excluir(int candidatoId)
        {
            _unit.CandidatoRepository.Remover(candidatoId);
            _unit.Save();
            return RedirectToAction("Listar", new { mensagem = "Candidato Excluido!" });
        }

        #endregion

        #region STATIC
        public static void AprovaCandidato(Candidato candidato, UnitOfWork unit)
        {
            AvaliacaoController.Cadastrar(candidato.Id, unit);
            RelatorioController.Cadastrar(candidato.Id, unit);
            candidato.Aprovado = true;
        }

        public static void ReprovaCandidato(Candidato candidato, UnitOfWork unit)
        {
            candidato.Aprovado = false;
        }

        public static void ReprovaCandidatoAprovado(Candidato candidato, UnitOfWork unit)
        {
            try
            {
                unit.CandidatoRepository.Remover(candidato.Id);
            }
            catch (Exception e)
            {
                throw new Exception("Não foi possível remover o candidato: " + e.Message);
            }
            AulaController.RemoverCandidatoAula(candidato.Id, unit);
            AvaliacaoController.Remover(candidato.Id, unit);
            RelatorioController.Remover(candidato.Id, unit);
            InverteStatusAprovacao(candidato);
        }

        public static void AprovaCandidatoReprovado(Candidato candidato, UnitOfWork unit)
        {
            AvaliacaoController.Cadastrar(candidato.Id, unit);
            RelatorioController.Cadastrar(candidato.Id, unit);
            InverteStatusAprovacao(candidato);
        }

        public static void InverteStatusAprovacao(Candidato candidato)
        {
            candidato.Aprovado = !candidato.Aprovado;
        }

        public static void RevalidaAprovacoes(decimal NovaNotaCorte, UnitOfWork unit)
        {
            var candidatos = unit.CandidatoRepository.Listar();
            foreach (var c in candidatos)
            {
                if (c.Nota < NovaNotaCorte && c.Aprovado == true)
                {
                    ReprovaCandidatoAprovado(c, unit);
                }
                else if (c.Nota >= NovaNotaCorte && c.Aprovado == false)
                {
                    AprovaCandidatoReprovado(c, unit);
                }
            }
        }

        #endregion

        #region OVERRIDE
        protected override void Dispose(bool disposing)
        {
            //devolve a conexao ao pool (Context) antes de destruir este controller
            _unit.Dispose();
            base.Dispose(disposing);
        }
        #endregion

        #region PRIVATE

        public bool IsCandidatoDuplicado(CandidatoViewModel candidatoViewModel)
        {
            var candidatos = _unit.CandidatoRepository.BuscarPor(c => c.Nome == candidatoViewModel.Nome);
            if (candidatos.Any() && !(candidatos.First().Id == candidatoViewModel.Id)) return true;
            return false;
        }
        private SelectList ListarFabricas()
        {
            var fabricas = _unit.FabricaRepository.Listar();
            return new SelectList(fabricas, "Id", "Nome");
        }

        private SelectList ListarUnidades()
        {
            var unidades = _unit.UnidadeRepository.Listar();
            return new SelectList(unidades, "Id", "Nome");
        }

        private SelectList ListarPerfil()
        {
            var perfilprof = _unit.PerfilProfissionalRepository.Listar();
            return new SelectList(perfilprof, "Id", "Descricao");
        }

        private SelectList ListarCursos()
        {
            var cursos = _unit.CursoRepository.Listar();
            return new SelectList(cursos, "Id", "Nome");
        }
   
  
                                              


        #endregion
    }
}