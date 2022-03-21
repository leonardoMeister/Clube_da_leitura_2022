using Clube_da_leitura.ConsoleApp.Controlador;
using Clube_da_leitura.ConsoleApp.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clube_da_leitura.ConsoleApp.Telas
{
    public class TelaReserva : TelaBase, ICadastravel
    {
        public ControladorReserva controladorReserva;
        public ControladorAmigo controladorAmigo;
        public ControladorRevista controladorRevista;
        public ControladorEmprestimo controladorEmprestimo;
        public TelaReserva(string tit, ControladorReserva controReserva, ControladorAmigo controAmigo, ControladorRevista controRevista, ControladorEmprestimo controladorEmprestimo) : base(tit)
        {
            this.controladorReserva = controReserva;
            this.controladorAmigo = controAmigo;
            this.controladorRevista = controRevista;
            this.controladorEmprestimo = controladorEmprestimo;
        }

        public void EditarRegistro()
        {
            //SERVE PARA TORNAR UMA RESERVA EM EMPRESTIMO
            List<Reserva> lista = controladorReserva.SelecionarTodosRegistros();
            if (lista.Count == 0) ImprimirFinalizacao("A lista não possui itens.\nTente novamente");
            else
            {
                Console.WriteLine("Informe o id da Reserva a Editar:");
                int id = Convert.ToInt32(Console.ReadLine());
                if (controladorReserva.ExisteRegistroComEsteId(id))
                {
                    
                    Reserva rev = controladorReserva.SelecionarRegistroPorId(new Reserva(id));
                    if (rev.statusFoiEmprestada ==true)
                    {
                        ImprimirFinalizacao("Reserva já foi emprestada.\nTente novamente");
                        return;
                    }
                    Emprestimo emp = rev.TransformarEmEmprestimo(DateTime.Now.AddDays( rev.revistaReserva.categoria.quantidadeDias));
                    controladorEmprestimo.AdicionarRegistro(emp);
                    rev.statusFoiEmprestada = true;

                    ImprimirFinalizacao("Reserva transformada em emprestimo.", ConsoleColor.Green);
                }
                else
                {
                    ImprimirFinalizacao("Não existe Reserva com este ID");
                }

            }

        }
        public void ExcluirRegistro()
        {
            //SERVE PARA CANCELAR UMA RESERVA
            List<Reserva> lista = controladorReserva.SelecionarTodosRegistros();
            if (lista.Count == 0) ImprimirFinalizacao("A lista não possui itens.\nTente novamente");
            else
            {
                Console.WriteLine("Informe o id da Reserva a Excluir:");
                int id = Convert.ToInt32(Console.ReadLine());
                if (controladorReserva.ExisteRegistroComEsteId(id))
                {
                    Reserva rev = controladorReserva.SelecionarRegistroPorId(new Reserva(id));
                    if (rev.statusFoiEmprestada == true)
                    {
                        ImprimirFinalizacao("Reserva já foi emprestada.\nTente novamente");
                        return;
                    }
                    else if (rev.statusCancelada == true)
                    {
                        ImprimirFinalizacao("Reserva já foi Cancelada.\nTente novamente");
                        return;
                    }
                    rev.statusCancelada = true;
                    rev.amigoReserva.revistaEmprestada = null;
                    rev.revistaReserva.statusGuardada = true;

                    ImprimirFinalizacao("Reserva Cancelada com sucesso.", ConsoleColor.Green);
                }
                else
                {
                    ImprimirFinalizacao("Não existe Reserva com este ID");
                }

            }
        }
        public void InserirNovoRegistro()
        {
            if (VerificarRegraExistirItensControladores()) return;
            Console.Clear();
            ImprimirColorido("Para criar uma Reserva selecione um Amigo e Revista Primeiro.", ConsoleColor.Yellow);

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

            Reserva rese = new Reserva(amigo,revista);
            controladorReserva.AdicionarRegistro(rese);
            amigo.revistaEmprestada = revista;
            revista.statusGuardada = false;

            ImprimirFinalizacao("Reserva Adicionada Com sucesso", ConsoleColor.Green);
        }
        public void VisualizarRegistros()
        {
            Console.Clear();
            if (controladorReserva.ExisteRegistrosNaLista())
            {
                List<Reserva> lista = controladorReserva.SelecionarTodosRegistros();
                string configuracaColunasTabela = "{0,-10} | {1,-25} | {2,-25} | {3,-30}";

                MontarCabecalhoTabela(configuracaColunasTabela, "Id", "Amigo", "Revista", "Status Reserva");


                foreach (Reserva t in lista)
                {
                    string aux = t.PegarStatusReserva();
                    if (t.statusCancelada == true) aux = "Reserva Cancelada";
                    else if (t.statusFoiEmprestada == true) aux = "Reserva Foi Emprestada";
                    else  aux = "Reserva Ativa";
                    
                    Console.WriteLine(configuracaColunasTabela, t.id, t.amigoReserva.nome, t.revistaReserva.tipoColecao, aux);
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
