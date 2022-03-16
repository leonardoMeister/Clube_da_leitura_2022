using Clube_da_leitura.ConsoleApp.Controlador;
using Clube_da_leitura.ConsoleApp.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clube_da_leitura.ConsoleApp.Telas
{
    public class TelaAmigo : TelaBase, ICadastravel
    {
        private ControladorAmigo controladorAmigo;
        public TelaAmigo(string tit, ControladorAmigo controladorAmigo) : base(tit)
        {
            this.controladorAmigo = controladorAmigo;
        }

        public void EditarRegistro()
        {
            Console.WriteLine("Informe o Id do Amigo que deseja editar:");
            int id = Convert.ToInt32(Console.ReadLine());
            if (controladorAmigo.ExisteRegistroComEsteId(id))
            {
                ImprimirFinalizacao("Item localizado.", ConsoleColor.Green);

                Console.WriteLine("Informa o nome do amigo:");
                string nome = Console.ReadLine();
                Console.WriteLine("Informe o nome do responsavel:");
                string nomeResponsavel = Console.ReadLine();
                Console.WriteLine("Informe o Telefone para cotato:");
                string telefone = Console.ReadLine();
                Console.WriteLine("Informe o Endereço(Cidade/Rua):");
                string endereco = Console.ReadLine();

                Amigo amigo = controladorAmigo.SelecionarRegistroPorId(new Amigo(id));

                amigo.nomeResponsavel = nomeResponsavel;
                amigo.telefone = telefone;
                amigo.nome = nome;
                amigo.endereco = endereco;

                ImprimirFinalizacao("Item Editado com Sucesso!", ConsoleColor.Green);
            }
            else
            {
                ImprimirFinalizacao("Nenhum item localizado na lista com o id informado.\nTente novamente");
            }
        }
        public void ExcluirRegistro()
        {
            Console.WriteLine("Informe o Id do Amigo que deseja remover:");
            int id = Convert.ToInt32(Console.ReadLine());
            if (controladorAmigo.ExisteRegistroComEsteId(id))
            {
                Amigo amigo = controladorAmigo.SelecionarRegistroPorId(new Amigo(id));

                //PRECISA IMPEDIR DE DELETAR AMIGO SE TIVER EM EMPRESTIMO

                controladorAmigo.ExcluirRegistro(id);
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

            Console.WriteLine("Informa o nome do amigo:");
            string nome = Console.ReadLine();
            Console.WriteLine("Informe o nome do responsavel:");
            string nomeResponsavel = Console.ReadLine();
            Console.WriteLine("Informe o Telefone para cotato:");
            string telefone = Console.ReadLine();
            Console.WriteLine("Informe o Endereço(Cidade/Rua):");
            string endereco = Console.ReadLine();

            Amigo amigo = new Amigo(nome, nomeResponsavel, telefone, endereco);
            controladorAmigo.AdicionarRegistro(amigo);

            ImprimirFinalizacao("Amigo Adicionada com sucesso!", ConsoleColor.Green);
        }
        public void VisualizarRegistros()
        {
            Console.Clear();
            if (controladorAmigo.ExisteRegistrosNaLista())
            {
                List<Amigo> lista = controladorAmigo.SelecionarTodosRegistros();
                string configuracaColunasTabela = "{0,-10} | {1,-25} | {2,-25} | {3,-25} | {4,-25}";

                MontarCabecalhoTabela(configuracaColunasTabela, "Id", "Nome", "Responsavel", "Telefone", "Endereco");


                foreach (Amigo t in lista)
                {
                    Console.WriteLine(configuracaColunasTabela, t.id, t.nome, t.nomeResponsavel, t.telefone, t.endereco);
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
