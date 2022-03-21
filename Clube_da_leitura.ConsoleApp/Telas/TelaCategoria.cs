using Clube_da_leitura.ConsoleApp.Controlador;
using Clube_da_leitura.ConsoleApp.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clube_da_leitura.ConsoleApp.Telas
{
    public class TelaCategoria : TelaBase, ICadastravel
    {
        public ControladorCategoria controladorCategoria;

        public TelaCategoria(string tit, ControladorCategoria contro) : base(tit)
        {
            this.controladorCategoria = contro;
        }
        public void EditarRegistro()
        {
            Console.Clear();
            if (controladorCategoria.ExisteRegistrosNaLista())
            {
                Console.WriteLine("Informe o Id da Categoria que deseja editar:");
                int id = Convert.ToInt32(Console.ReadLine());

                if (controladorCategoria.ExisteRegistroComEsteId(id))
                {
                    Console.WriteLine("Digite o Nome da Categoria: ");
                    string nome = Console.ReadLine();
                    Console.WriteLine("Digite o Numero de dias limite da categoria");
                    int numeroDias = Convert.ToInt32(Console.ReadLine());

                    Categoria cat = controladorCategoria.SelecionarRegistroPorId(new Categoria(id));

                    cat.nomeCategoria = nome;
                    cat.quantidadeDias = numeroDias;
                    ImprimirFinalizacao("Categoria Editada Com Sucesso.", ConsoleColor.Green);
                }
                else ImprimirFinalizacao("Não existe registro com este ID");

            }
            else ImprimirFinalizacao("Não existem itens na lista");

        }
        public void ExcluirRegistro()
        {
            Console.Clear();
            if (controladorCategoria.ExisteRegistrosNaLista())
            {
                Console.WriteLine("Informe o Id da Categoria que deseja Remover:");
                int id = Convert.ToInt32(Console.ReadLine());

                if (controladorCategoria.ExisteRegistroComEsteId(id))
                {
                    controladorCategoria.ExcluirRegistro(id);
                    ImprimirFinalizacao("Categoria Excluida com sucesso.",ConsoleColor.Green);
                }
                else ImprimirFinalizacao("Não existe registro com este ID");

            }
            else ImprimirFinalizacao("Não existem itens na lista");
        } 
        public void InserirNovoRegistro()
        {
            Console.Clear();

            Console.WriteLine("Digite o Nome da Categoria: ");
            string nome = Console.ReadLine();
            Console.WriteLine("Digite o Numero de dias limite da categoria");
            int numeroDias = Convert.ToInt32(Console.ReadLine());

            Categoria cat = new Categoria(nome, numeroDias);

            controladorCategoria.AdicionarRegistro(cat);

            ImprimirFinalizacao("Categoria Adicionada com sucesso.", ConsoleColor.Green);
        }
        public void VisualizarRegistros()
        {
            Console.Clear();
            if (controladorCategoria.ExisteRegistrosNaLista())
            {
                List<Categoria> lista = controladorCategoria.SelecionarTodosRegistros();
                string configuracaColunasTabela = "{0,-10} | {1,-25} | {2,-35}";

                MontarCabecalhoTabela(configuracaColunasTabela, "Id", "Nome", "Numero Dias Limite");


                foreach (Categoria t in lista)
                {
                    Console.WriteLine(configuracaColunasTabela, t.id, t.nomeCategoria, t.quantidadeDias);
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
