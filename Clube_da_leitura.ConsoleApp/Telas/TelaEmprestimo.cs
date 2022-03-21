using Clube_da_leitura.ConsoleApp.Controlador;
using Clube_da_leitura.ConsoleApp.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clube_da_leitura.ConsoleApp.Telas
{
    public class TelaEmprestimo : TelaBase, ICadastravel
    {
        ControladorEmprestimo controladorEmprestimo;
        ControladorRevista controladorRevista;
        ControladorAmigo controladorAmigo;
        public TelaEmprestimo(string tit, ControladorEmprestimo controladorEmprestimo, ControladorAmigo controladorAmigo, ControladorRevista controladorRevista) : base(tit)
        {
            this.controladorEmprestimo = controladorEmprestimo;
            this.controladorRevista = controladorRevista;
            this.controladorAmigo = controladorAmigo;
        }

        public void EditarRegistro()
        {
            List<Emprestimo> lista = controladorEmprestimo.SelecionarTodosRegistros();
            if (lista.Count == 0) ImprimirFinalizacao("A lista não possui itens.\nTente novamente");
            else
            {
                Console.WriteLine("Informe o id do Emprestimo a Editar:");
                int id = Convert.ToInt32(Console.ReadLine());
                if (controladorEmprestimo.ExisteRegistroComEsteId(id))
                {
                    Emprestimo emp = controladorEmprestimo.SelecionarRegistroPorId(new Emprestimo(id));

                    Console.WriteLine("Informe a nova data de devolução do Emprestimo: [2022/02/20]");
                    string stringData = Console.ReadLine();
                    DateTime novaDataDevolucao = new DateTime(Convert.ToInt32(stringData.Substring(0, 4)), Convert.ToInt32(stringData.Substring(5, 2)), Convert.ToInt32(stringData.Substring(8, 2)));

                    emp.dataVencimento = novaDataDevolucao;
                    ImprimirFinalizacao("Emprestimo Editado Com Sucesso.", ConsoleColor.Green);
                }
                else
                {
                    ImprimirFinalizacao("Não existe Emprestimo com este ID");
                }

            }
        }
        public void ExcluirRegistro()
        {
            List<Emprestimo> lista = controladorEmprestimo.SelecionarTodosRegistros();
            if (lista.Count == 0) ImprimirFinalizacao("A lista não possui itens.\nTente novamente");
            else
            {
                Console.WriteLine("Informe o id do Emprestimo a devolver:");
                int id = Convert.ToInt32(Console.ReadLine());
                if (controladorEmprestimo.ExisteRegistroComEsteId(id))
                {
                    Emprestimo emp = controladorEmprestimo.SelecionarRegistroPorId(new Emprestimo(id));

                    emp.statusDevolucao = true;
                    emp.amigoEmprestimo.revistaEmprestada = null;
                    emp.revista.statusGuardada = true;
                    ImprimirFinalizacao("Emprestimo Devolvido,Revista guardada na caixa,Amigo Pode Emprestar Novamente!", ConsoleColor.Green);
                }
                else
                {
                    ImprimirFinalizacao("Não existe Emprestimo com este ID");
                }

            }
        }
        public void InserirNovoRegistro()
        {
            if (VerificarRegraExistirItensControladores()) return;
            Console.Clear();
            ImprimirColorido("Para Inserir um Emprestimo seleciona um Amigo e Revista Primeiro.", ConsoleColor.Yellow);

            Console.WriteLine("--------------------------------------------------------------------------------------\n");

            Console.WriteLine("Informe o Id do Amigo: ");
            int idAmigo = Convert.ToInt32(Console.ReadLine());
            Amigo amigo = controladorAmigo.SelecionarRegistroPorId(new Amigo(idAmigo));

            if (amigo.PodeEmprestarRevista()) ImprimirColorido("Amigo encontrado pode Emprestar revista!", ConsoleColor.Green);
            else
            {
                ImprimirFinalizacao("O Amigo Já possui uma revista emprestada.\nTente novamente.");
                return;
            }

            Console.WriteLine("\n\nInforme o Id da Revista: ");
            int idRevista = Convert.ToInt32(Console.ReadLine());
            Revista revista = controladorRevista.SelecionarRegistroPorId(new Revista(idRevista));
            if (revista.RevistaPodeSerEmprestada()) ImprimirColorido("Revista econtrada pode ser emprestada", ConsoleColor.Green);
            else
            {
                ImprimirFinalizacao("A revista está emprestada no momento.\nTente novamente.");
                return;
            }


            DateTime dataCriacao = DateTime.Now;

            Emprestimo emp = new Emprestimo(amigo, revista, dataCriacao);
            controladorEmprestimo.AdicionarRegistro(emp);
            amigo.revistaEmprestada = revista;
            revista.statusGuardada = false;
            ImprimirFinalizacao("Emprestimo Adicionado Com sucesso", ConsoleColor.Green);

        }
        public void VisualizarRegistros()
        {
            int opcaoFiltro = PegarOpcaoFiltroEmprestimos();

            switch (opcaoFiltro)
            {
                case 1:
                    ImprimirListaEmprestimos(controladorEmprestimo.SelecionarTodosRegistros());
                    break;
                case 2:
                    ImprimirListaEmprestimos(controladorEmprestimo.SelecionarTodosRegistrosAbertos());
                    break;
                case 3:
                    ImprimirListaEmprestimos(controladorEmprestimo.SelecionarTodosRegistrosFechados());
                    break;
                case 4:
                    Console.WriteLine("\nInforme O Mes desejado: Ex[3, 12, 5]");
                    int mes = Convert.ToInt32(Console.ReadLine());
                    ImprimirListaEmprestimos(controladorEmprestimo.SelecionarTodosRegistrosPorMes(mes));
                    break;
                case 5:
                    ImprimirListaEmprestimos(controladorEmprestimo.SelecionarTodosRegistrosVencidos());
                    break;
                default:
                    throw new ArgumentException();
            }
        }
        private void ImprimirListaEmprestimos(List<Emprestimo> listEmp)
        {
            if (listEmp.Count != 0)
            {
                string configuracaColunasTabela = "{0,-6} | {1,-15} | {2,-15} | {3,-20} | {4,-20} | {5,-10} ";

                MontarCabecalhoTabela(configuracaColunasTabela, "Id", "amigo", "Revista", "Data Emprestimo", "Dia Devolução", "Status");


                foreach (Emprestimo t in listEmp)
                {
                    string auxStatusDevolv = (t.statusDevolucao) ? "Devolvido" : "Emprestado";
                    Console.WriteLine(configuracaColunasTabela, t.id, t.amigoEmprestimo.nome, t.revista.tipoColecao, t.dataCriacao, t.dataVencimento, auxStatusDevolv);
                }
            }
            else
            {
                ImprimirFinalizacao("Não existem itens na lista.");
                return;
            }
            ImprimirFinalizacao();
        }
        private int PegarOpcaoFiltroEmprestimos()
        {
            int opcaoDesejada = 0;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Informe a opçao desejada de Visualizaçao de Emprestimos: ");
                Console.WriteLine("  [1] Todos\n  [2] Abertos\n  [3] Fechados\n  [4] Por Mês\n  [5] Vencidos com multa");
                opcaoDesejada = Convert.ToInt32(Console.ReadLine());
                if (opcaoDesejada == 1 || opcaoDesejada == 2 || opcaoDesejada == 3 || opcaoDesejada == 4 || opcaoDesejada == 5) return opcaoDesejada;

                ImprimirFinalizacao("Opção fora do escopo.\nTente novamente!");
            }
        }
        private bool VerificarRegraExistirItensControladores()
        {
            if (!controladorAmigo.ExisteRegistrosNaLista())
            {
                ImprimirFinalizacao("Para adicionar um Emprestimo precisa antes adicionar uma Revista!");
                return true;
            }
            else if (!controladorAmigo.ExisteRegistrosNaLista())
            {
                ImprimirFinalizacao("Para adicionar um Emprestimo precisa antes adicionar um Amigo!");
                return true;
            }
            return false;
        }
    }
}
