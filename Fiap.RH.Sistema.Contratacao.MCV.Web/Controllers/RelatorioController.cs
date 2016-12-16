using Fiap.RH.Sistema.Contratacao.Dominio;
using Fiap.RH.Sistema.Contratacao.MCV.Web.ViewModels;
using Fiap.RH.Sistema.Contratacao.Persistencia.UnitsOfWork;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Fiap.RH.Sistema.Contratacao.MCV.Web.Controllers
{
    public class RelatorioController : Controller
    {
        #region FIELDS
        private UnitOfWork _unit = new UnitOfWork();
        #endregion


        #region GET
        [HttpGet]
        public ActionResult Gerar(int id, string mensagem)
        {
            Relatorio relatorio = null;
            try
            {
                relatorio = _unit.RelatorioRepository.BuscarPorId(id);
                relatorio.Faltas = CalculaFaltas(id);
                _unit.Save(); //atualiza as faltas no sistema, apesar de sempre calcular para exibir
            }
            catch (Exception e)
            {
                ViewBag.Mensagem = "Erro ao gerar relatório: " + e.Message;
                return View("Gerar");
            }

            ViewBag.Mensagem = mensagem;
            return View(relatorio);
        }

        [HttpGet]
        public ActionResult Editar(int id, string mensagem)
        {
            Relatorio relatorio = null;
            RelatorioViewModel relatorioViewModel = new RelatorioViewModel();

            try
            {
                relatorio = _unit.RelatorioRepository.BuscarPorId(id);
                relatorioViewModel.Id = relatorio.Id;
                relatorioViewModel.AderenciaComportamental = relatorio.AderenciaComportamental;
                relatorioViewModel.Candidato = relatorio.Candidato;
                relatorioViewModel.DesempenhoNota = relatorio.DesempenhoNota;
                relatorioViewModel.HabilidadesDoCandidato = relatorio.RelatorioHabilidades;

                var habilidades = _unit.HabilidadeRepository.Listar();
                relatorioViewModel.Habilidades = new SelectList(habilidades, "Id", "Nome");
            }
            catch (Exception e)
            {
                relatorioViewModel.Mensagem = "Erro ao gerar relatório para alterações: " + e.Message;
            }

            relatorioViewModel.Mensagem = mensagem;
            return View(relatorioViewModel);
        }

        [HttpGet]
        public ActionResult AdicionarHabilidade(int id)
        {
            RelatorioViewModel relatorioViewModel = new RelatorioViewModel();
            try
            {
                ICollection<Habilidade> habilidades = _unit.HabilidadeRepository.Listar();
                relatorioViewModel.Habilidades = new SelectList(habilidades, "Id", "Nome");
                relatorioViewModel.Id = id;
                relatorioViewModel.Candidato = _unit.CandidatoRepository.BuscarPorId(id);
            }
            catch (Exception e)
            {
                relatorioViewModel.Mensagem = "Erro ao buscar as habilidades no banco de dados para seleção: " + e.Message;
            }
            return View(relatorioViewModel);
        }
        #endregion


        #region POST
        [HttpPost]
        public ActionResult Editar(RelatorioViewModel relatorioViewModel)
        {
            Relatorio relatorio = null;
            try
            {
                relatorio = _unit.RelatorioRepository.BuscarPorId(relatorioViewModel.Id);
                relatorio.AderenciaComportamental = relatorioViewModel.AderenciaComportamental;
                relatorio.DesempenhoNota = relatorioViewModel.DesempenhoNota;
                _unit.RelatorioRepository.Editar(relatorio);
                _unit.Save();
            }
            catch (Exception e)
            {
                relatorioViewModel.Mensagem = "Erro ao salvar as alterações do relatório: " + e.Message;
                return View(relatorioViewModel);
            }

            return RedirectToAction("Gerar", new { id = relatorio.Id, mensagem = "Relatório alterado com sucesso!"});
        }

        
        [HttpPost]
        public ActionResult AdicionarHabilidade(RelatorioViewModel relatorioViewModel)
        {
            if (ModelState.IsValidField("NovaHabilidadeId"))
            {
                RelatorioHabilidade novaHabilidade = new RelatorioHabilidade()
                {
                    IdRelatorio = relatorioViewModel.Id,
                    IdHabilidade = relatorioViewModel.NovaHabilidadeId,
                };

                try
                {
                    _unit.RelatorioHabilidadeRepository.Cadastrar(novaHabilidade);
                    _unit.Save();
                }
                catch (Exception e)
                {
                    relatorioViewModel.Mensagem = "Erro ao adicionar nova habilidade: " + e.Message;
                    return View(relatorioViewModel);
                }
            }
            else return View(relatorioViewModel);

            return RedirectToAction("Editar", new { id = relatorioViewModel.Id, mensagem = "Habilidade adicionada com sucesso!" });
        }

        [HttpPost]
        public JsonResult RemoverHabilidade(int id)
        {
            try
            {
                _unit.RelatorioHabilidadeRepository.RemoverPelaOrdem(id);
                _unit.Save();
            }
            catch(Exception e)
            {
                return Json(new { status = false, mensagem = "Erro ao remover a habilidade do relatório: " + e.Message });
            }
            return Json(new { status = true });
        }

        [HttpPost]
        public JsonResult AtualizarHabilidade(int id, int idHabilidade)
        {
            try
            {
                RelatorioHabilidade relatorioHabilidade = _unit.RelatorioHabilidadeRepository.BuscaPelaOrdem(id);
                relatorioHabilidade.IdHabilidade = idHabilidade;
                _unit.RelatorioHabilidadeRepository.Editar(relatorioHabilidade);
                _unit.Save();
            }
            catch (Exception e)
            {
                return Json(new { status = false, mensagem = "Erro ao atualizar a habilidade do relatório: " + e.Message });
            }
            return Json(new { status = true });
        }

        [HttpPost]
        public ActionResult Aprovar(int id)
        {
            Relatorio relatorio = null;
            try
            {
                relatorio = _unit.RelatorioRepository.BuscarPorId(id);
                relatorio.Status = "Aprovado";
                relatorio.LoginId = 1; //TODO !! recuperar id do login de usuário!
                _unit.RelatorioRepository.Editar(relatorio);
                _unit.Save();
            }
            catch (Exception e)
            {
                return RedirectToAction("Gerar", new { id = id, mensagem = "Erro ao aprovar candidato: " + e.Message });
            }

            return RedirectToAction("Gerar", new { id = id, mensagem = "Candidato aprovado com sucesso!"});
        }

        [HttpPost]
        public ActionResult Reprovar(int id)
        {
            Relatorio relatorio = null;
            try
            {
                relatorio = _unit.RelatorioRepository.BuscarPorId(id);
                relatorio.Status = "Reprovado";
                relatorio.LoginId = 1; //TODO !! recuperar id do login de usuário!
                _unit.RelatorioRepository.Editar(relatorio);
                _unit.Save();
            }
            catch (Exception e)
            {
                return RedirectToAction("Gerar", new { id = id, mensagem = "Erro ao reprovar candidato: " + e.Message });
            }

            return RedirectToAction("Gerar", new { id = id, mensagem = "Candidato foi corretamente reprovado" });
        }
        #endregion


        #region PRIVATE
        private int CalculaFaltas(int id)
        {
            ICollection<CandidatoAula> aulas = null;
            try
            {
                aulas = _unit.CandidatoAulaRepository.BuscarPor(ca => ca.IdCandidato == id);
            }
            catch (Exception e)
            {
                throw new Exception("Não foi possível encontrar as aulas que o candidato participou: " + e.Message);
            }
            int faltas = 0;
            foreach (var ca in aulas) if (ca.Presenca == false) faltas++;
            return faltas;
        }
        #endregion


        #region STATIC
        public static void Cadastrar(int id, UnitOfWork unit)
        {
            var relatorio = new Relatorio()
            {
                Id = id,
                AderenciaComportamental = 0,
                DesempenhoNota = 0,
                Faltas = 0,
                Status = "Indefinido"
            };

            try
            {
                Login login = RecuperaLoginDoAdm(unit);
                relatorio.Id = login.Id;
                unit.RelatorioRepository.Cadastrar(relatorio);
            }
            catch (Exception e)
            {
                throw new Exception("Não foi possível criar o relatório individual do candidato: " + e.Message);
            }
        }

        public static Login RecuperaLoginDoAdm(UnitOfWork unit)
        {
            var logins = unit.LoginRepository.BuscarPor(l => l.Nome == "Adm");
            Login login = null;
            if (logins.GetEnumerator().MoveNext())
            {
                login = logins.GetEnumerator().Current;
            }
            else
            {
                login = CriaLoginDoAdm(unit);
            }

            return login;
        }

        public static Login CriaLoginDoAdm(UnitOfWork unit)
        {
            Login login = new Login()
            {
                Nome = "Adm",
                Email = "adm@fiap.com.br",
                Senha = "adm"
            };
            unit.LoginRepository.Cadastrar(login);
            return login;
        }

        public static void Remover(int id, UnitOfWork unit)
        {
            try
            {
                unit.RelatorioRepository.Remover(id);
            }
            catch (Exception e)
            {
                throw new Exception("Não foi possível remover o relatório individual do candidato: " + e.Message);
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