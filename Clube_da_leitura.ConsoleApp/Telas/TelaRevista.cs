using Clube_da_leitura.ConsoleApp.Controlador;
using Clube_da_leitura.ConsoleApp.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clube_da_leitura.ConsoleApp.Telas
{
    public class TelaRevista : TelaBase, ICadastravel
    {
        ControladorRevista controladorRevista;
        ControladorCaixa controladorCaixa;
        public TelaRevista(string tit, ControladorRevista controladorRevista, ControladorCaixa controladorCaixa) : base(tit)
        {
            this.controladorRevista = controladorRevista;
            this.controladorCaixa = controladorCaixa;
        }
        private bool VerificarRegraExistirItensControladores()
        {
            if (!controladorCaixa.ExisteRegistrosNaLista())
            {
                ImprimirFinalizacao("Para adicionar/editar uma revista precisa antes adicionar uma caixa!");
                return true;
            }
            return false;
        }
        public void EditarRegistro()
        {
            Console.Clear();
            if (VerificarRegraExistirItensControladores()) return;

            Console.WriteLine("Informe o Id da revista que deseja editar:");
            int id = Convert.ToInt32(Console.ReadLine());
            if (controladorRevista.ExisteRegistroComEsteId(id))
            {
                ImprimirFinalizacao("Item localizado.", ConsoleColor.Green);


                Console.Write("Digite a coleção da revista: ");
                string colecao = Console.ReadLine();

                Console.Write("Digite o numero de edição da revista: ");
                int numeroEdicao = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Informe a Data defabricação da revista: [2022/02/20]");
                string stringData = Console.ReadLine();
                DateTime data = new DateTime(Convert.ToInt32(stringData.Substring(0, 4)), Convert.ToInt32(stringData.Substring(5, 2)), Convert.ToInt32(stringData.Substring(8, 2)));

                Revista rev = controladorRevista.SelecionarRegistroPorId(new Revista(id));

                rev.tipoColecao = colecao;
                rev.numeroEdicao = numeroEdicao;
                rev.dataAnoImpressao = data;

                ImprimirFinalizacao("Item Editado com Sucesso!", ConsoleColor.Green);
            }
            else
            {
                ImprimirFinalizacao("Nenhum item localizado na lista com o id informado.\nTente novamente");
            }

        }
        public void ExcluirRegistro()
        {
            Console.Clear();
            if (VerificarRegraExistirItensControladores()) return;

            Console.WriteLine("Informe o Id da Revista que deseja remover:");
            int id = Convert.ToInt32(Console.ReadLine());
            if (controladorRevista.ExisteRegistroComEsteId(id))
            {
                controladorRevista.ExcluirRegistro(id);
                ImprimirFinalizacao("Item Removio Com sucesso!", ConsoleColor.Green);
            }
            else
            {
                ImprimirFinalizacao("Não foi possivel localizar um item com o id Informado.\nTente Novamente.");
            }
        }
        public void InserirNovoRegistro()
        {
            Console.Clear();
            if (VerificarRegraExistirItensControladores()) return;

            Console.Write("Digite a coleção da revista: ");
            string colecao = Console.ReadLine();

            Console.Write("Digite o numero de edição da revista: ");
            int numeroEdicao = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Informe a Data defabricação da revista: [2022/02/20]");
            string stringData = Console.ReadLine();
            DateTime data = new DateTime(Convert.ToInt32(stringData.Substring(0, 4)), Convert.ToInt32(stringData.Substring(5, 2)), Convert.ToInt32(stringData.Substring(8, 2)));
            
                Caixa caixa;
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("Agora Informe o Id da caixa para guardar a Revista");
                    int id = Convert.ToInt32(Console.ReadLine());
                    if (!controladorCaixa.ExisteRegistroComEsteId(id))
                    {
                        ImprimirFinalizacao("Nenhuma Caixa localizada com esse id.\nTente Novamente!");
                        continue;
                    }
                    caixa = controladorCaixa.SelecionarRegistroPorId(new Caixa(id));
                    break;
                }

                Revista rev = new Revista(colecao, numeroEdicao, data);
                caixa.AdicionarRevistaNaCaixa(rev);
                controladorRevista.AdicionarRegistro(rev);
                ImprimirFinalizacao("Revista Adicionada Com Sucesso!", ConsoleColor.Green);
            
        }
        public void VisualizarRegistros()
        {
            Console.Clear();
            if (controladorRevista.ExisteRegistrosNaLista())
            {
                List<Revista> lista = controladorRevista.SelecionarTodosRegistros();
                string configuracaColunasTabela = "{0,-10} | {1,-25} | {2,-25} | {3,-25} | {4,-25}";

                MontarCabecalhoTabela(configuracaColunasTabela, "Id", "Coleção", "Numero", "Data Impressao", "Status");


                foreach (Revista t in lista)
                {
                    string auxStatus = (t.statusGuardada) ? "Guardada" : "Emprestada";
                    Console.WriteLine(configuracaColunasTabela, t.id, t.tipoColecao, t.numeroEdicao, t.dataAnoImpressao, auxStatus);
                }
            }
            else
            {
                ImprimirFinalizacao("Não existem itens na lista.");
                return;
            }
            ImprimirFinalizacao();
            return;
        }
    }
}
