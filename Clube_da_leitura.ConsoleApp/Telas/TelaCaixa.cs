using Clube_da_leitura.ConsoleApp.Controlador;
using Clube_da_leitura.ConsoleApp.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clube_da_leitura.ConsoleApp.Telas
{
    public class TelaCaixa : TelaBase, ICadastravel
    {
        ControladorCaixa controladorCaixa;
        public TelaCaixa(string tit, ControladorCaixa controladorCaixa) : base(tit)
        {
            this.controladorCaixa = controladorCaixa;
        }

        public void EditarRegistro()
        {
            Console.WriteLine("Informe o Id da Caixa que deseja editar:");
            int id = Convert.ToInt32(Console.ReadLine());
            if (controladorCaixa.ExisteRegistroComEsteId(id))
            {
                ImprimirFinalizacao("Item localizado.", ConsoleColor.Green);

                Console.WriteLine("Informa a cor da Caixa:");
                string corCaixa = Console.ReadLine();
                Console.WriteLine("Informe a etiqueta da Caixa:");
                string etiquetaCaixa = Console.ReadLine();
                Console.WriteLine("Informe o numero da Caixa:");
                int numeroCaixa = Convert.ToInt32(Console.ReadLine());

                Caixa caixa = controladorCaixa.SelecionarRegistroPorId(new Caixa(id));

                caixa.corCaixa = corCaixa;
                caixa.etiqueta = etiquetaCaixa;
                caixa.numero = numeroCaixa;

                ImprimirFinalizacao("Item Editado com Sucesso!", ConsoleColor.Green);
            }
            else
            {
                ImprimirFinalizacao("Nenhum item localizado na lista com o id informado.\nTente novamente");
            }
        }
        public void ExcluirRegistro()
        {
            Console.WriteLine("Informe o Id da Caixa que deseja remover:");
            int id = Convert.ToInt32(Console.ReadLine());
            if (controladorCaixa.ExisteRegistroComEsteId(id))
            {
                Caixa caixa = controladorCaixa.SelecionarRegistroPorId(new Caixa(id));
                if (caixa.listaRevistasNaCaixa.Count > 0)
                {
                    ImprimirFinalizacao("Não é possivel remover uma caixa com Revistas Dentro.\nTente novamente");
                    return;
                }
                controladorCaixa.ExcluirRegistro(id);
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

            Console.WriteLine("Informa a cor da Caixa:");
            string corCaixa = Console.ReadLine();
            Console.WriteLine("Informe a etiqueta da Caixa:");
            string etiquetaCaixa = Console.ReadLine();
            Console.WriteLine("Informe o numero da Caixa:");
            int numeroCaixa = Convert.ToInt32(Console.ReadLine());

            Caixa caixa = new Caixa(corCaixa, etiquetaCaixa, numeroCaixa);
            controladorCaixa.AdicionarRegistro(caixa);

            ImprimirFinalizacao("Caixa Adicionada com sucesso!", ConsoleColor.Green);
        }
        public void VisualizarRegistros()
        {
            Console.Clear();
            if (controladorCaixa.ExisteRegistrosNaLista())
            {
                List<Caixa> lista = controladorCaixa.SelecionarTodosRegistros();
                string configuracaColunasTabela = "{0,-10} | {1,-25} | {2,-25} | {3,-25}";

                MontarCabecalhoTabela(configuracaColunasTabela, "Id", "Cor", "Etiqueta", "Numero");


                foreach (Caixa t in lista)
                {
                    Console.WriteLine(configuracaColunasTabela, t.id, t.corCaixa, t.etiqueta, t.numero);
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
