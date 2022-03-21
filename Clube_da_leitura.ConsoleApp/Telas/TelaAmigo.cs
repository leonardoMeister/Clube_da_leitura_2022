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
        private ControladorEmprestimo controladorEmprestimo;
        public TelaAmigo(string tit, ControladorAmigo controladorAmigo, ControladorEmprestimo controladorEmprestimo) : base(tit)
        {
            this.controladorAmigo = controladorAmigo;
            this.controladorEmprestimo = controladorEmprestimo;
        }

        public void EditarRegistro()
        {

            int opcao = PegarOpcaoEdicaoAmigo();

            switch (opcao)
            {
                case 1:
                    EdicaoAmigo();
                    break;
                case 2:
                    QuitarMultas();
                    break;
            }
        }

        private void QuitarMultas()
        {
            Console.WriteLine("Informe o Id do Amigo que deseja Quitar Dividas:");
            int id = Convert.ToInt32(Console.ReadLine());
            if (controladorAmigo.ExisteRegistroComEsteId(id))
            {
                Amigo amigo = controladorAmigo.SelecionarRegistroPorId(new Amigo(id));

                if(amigo.statusPossuiMulta == false)
                {
                    ImprimirFinalizacao("Esse amigo não possui multa.\nTente novamente.");
                }
                ImprimirFinalizacao("Item localizado.", ConsoleColor.Green);

                Console.WriteLine("Para Quitar uma multa o amigo deve devolver a revista.");
                Console.WriteLine("Para Quitar uma multa o amigo deve Pagar a multa.");
                Console.WriteLine("Para Quitar uma multa o amigo deve assinar um termo que não vai mais atrasar.");
                Console.WriteLine("Para Quitar uma multa o amigo deve conquistar a confiança novamente.\n");
                Console.WriteLine("Para confirmar a quitação de divida informe 'CONFIRMAR', se o amigo seguiu o protocolo de quitação de dividas.");
                Console.WriteLine("Informe 'CONFIRMAR' em caixa alta: ");
                string auxConfirmar = Console.ReadLine();
                if (auxConfirmar == "CONFIRMAR")
                {
                    if (controladorEmprestimo.QuitarDividaPorDevolucaoDeAmigo(amigo))
                    {
                        amigo.statusPossuiMulta = false;
                    }

                    ImprimirFinalizacao("Divida quitada com sucesso", ConsoleColor.Green);
                }else
                {
                    ImprimirFinalizacao("Codigo de confirmação invalido.\nTente novamente.");
                    return;
                }

                ImprimirFinalizacao("Divida Quitada!", ConsoleColor.Green);
            }
            else
            {
                ImprimirFinalizacao("Nenhum item localizado na lista com o id informado.\nTente novamente");
            }
        }

        private int PegarOpcaoEdicaoAmigo()
        {
            Console.Clear();

            while (true)
            {
                Console.WriteLine("Informe A opção desejada de edição de amigo:");

                Console.WriteLine("[1] Edição dados.");
                Console.WriteLine("[2] Quitar multas.");
                int opcao = Convert.ToInt32(Console.ReadLine());

                if (opcao == 1 || opcao == 2)
                {
                    return opcao;
                }
                else ImprimirFinalizacao("Opcao invalida.\nTente novamente.");
            }
        }

        private void EdicaoAmigo()
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
            controladorEmprestimo.AplicarMultaEmRegistrosVencidos(controladorEmprestimo.SelecionarTodosRegistrosVencidos());

            Console.Clear();
            if (controladorAmigo.ExisteRegistrosNaLista())
            {
                List<Amigo> lista = controladorAmigo.SelecionarTodosRegistros();
                string configuracaColunasTabela = "{0,-10} | {1,-15} | {2,-15} | {3,-25} | {4,-20} | {5,-15}";

                MontarCabecalhoTabela(configuracaColunasTabela, "Id", "Nome", "Responsavel", "Telefone", "Endereco", "Multas");


                foreach (Amigo t in lista)
                {
                    string auxMulta = (t.statusPossuiMulta) ? "Com Multa" : "Sem Multa";
                    Console.WriteLine(configuracaColunasTabela, t.id, t.nome, t.nomeResponsavel, t.telefone, t.endereco, auxMulta);
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
