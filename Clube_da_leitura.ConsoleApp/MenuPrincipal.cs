using Clube_da_leitura.ConsoleApp.Controlador;
using Clube_da_leitura.ConsoleApp.Dominio;
using Clube_da_leitura.ConsoleApp.Telas;
using System;


namespace Clube_da_leitura.ConsoleApp
{
    public class MenuPrincipal
    {

        ControladorCaixa controladorCaixa;
        ControladorAmigo controladorAmigo;
        ControladorRevista controladorRevista;
        ControladorEmprestimo controladorEmprestimo;
        ControladorCategoria controladorCategoria;
        ControladorReserva controladorReserva;

        TelaAmigo telaAmigo;
        TelaCaixa telaCaixa;
        TelaEmprestimo telaEmprestimo;
        TelaRevista telaRevista;
        TelaCategoria telaCategoria;
        TelaReserva telaReserva;

        public MenuPrincipal()
        {
            controladorAmigo = new ControladorAmigo();
            controladorCaixa = new ControladorCaixa();
            controladorEmprestimo = new ControladorEmprestimo();
            controladorRevista = new ControladorRevista();
            controladorReserva = new ControladorReserva();
            controladorCategoria = new ControladorCategoria();

            AdicionarItensDeTeste();

            telaCategoria = new TelaCategoria("Tela Categoria", controladorCategoria);
            telaReserva = new TelaReserva("Tela Reserva", controladorReserva, controladorAmigo, controladorRevista, controladorEmprestimo);
            telaRevista = new TelaRevista("Tela Revista", controladorRevista, controladorCaixa, controladorCategoria);

            telaAmigo = new TelaAmigo("Tela Amigos", controladorAmigo,controladorEmprestimo);
            telaCaixa = new TelaCaixa("Tela Caixas", controladorCaixa);
            telaEmprestimo = new TelaEmprestimo("Tela Emprestimos", controladorEmprestimo, controladorAmigo, controladorRevista);


        }

        public void AdicionarItensDeTeste()
        {

            Categoria cat1 = new Categoria("Exclusividade", 1);
            Categoria cat2 = new Categoria("Premium", 3);
            Categoria cat3 = new Categoria("Rara", 5);
            Categoria cat4 = new Categoria("Normal", 15);
            Categoria cat5 = new Categoria("Livre", 30);

            controladorCategoria.AdicionarRegistro(cat1);
            controladorCategoria.AdicionarRegistro(cat2);
            controladorCategoria.AdicionarRegistro(cat3);
            controladorCategoria.AdicionarRegistro(cat4);
            controladorCategoria.AdicionarRegistro(cat5);

            Amigo m1 = new Amigo("leonardo1", "Veronica1", "(47) 9 9239-8644", "Estrada Nova");
            Amigo m2 = new Amigo("leonardo2", "Veronica2", "(55) 9 9239-8644", "Estrada Velha");
            Amigo m3 = new Amigo("leonardo3", "Veronica3", "(66) 9 9239-8644", "Estrada Reformada");

            controladorAmigo.AdicionarRegistro(m1);
            controladorAmigo.AdicionarRegistro(m2);
            controladorAmigo.AdicionarRegistro(m3);

            Caixa c1 = new Caixa("Amarela", "Etiqueta n1", 1);
            Caixa c2 = new Caixa("Azul", "Etiqueta n2", 2);

            Revista r1 = new Revista("Batman", 1994, new DateTime(2001, 03, 20), cat5);
            controladorRevista.AdicionarRegistro(r1);
            Revista r2 = new Revista("Robim", 1999, new DateTime(2003, 04, 09), cat5);
            controladorRevista.AdicionarRegistro(r2);
            Revista r3 = new Revista("SuperMan", 1980, new DateTime(1990, 04, 09), cat3);
            controladorRevista.AdicionarRegistro(r3);


            c1.AdicionarRevistaNaCaixa(r1);
            c1.AdicionarRevistaNaCaixa(r2);
            c1.AdicionarRevistaNaCaixa(r3);
            controladorCaixa.AdicionarRegistro(c1);
            controladorCaixa.AdicionarRegistro(c2);


            Emprestimo emp1 = new Emprestimo(m1, r2, new DateTime(2021, 3, 27));
            emp1.StatusDevolucao = true;

            Emprestimo emp2 = new Emprestimo(m2, r1, DateTime.Now);
            emp2.StatusDevolucao = true;

            Emprestimo emp3 = new Emprestimo(m3, r3, new DateTime(2020, 6, 27));
            m3.SetRevista(r3);
            r3.StatusGuardada = false;

            controladorEmprestimo.AdicionarRegistro(emp1);
            controladorEmprestimo.AdicionarRegistro(emp2);
            controladorEmprestimo.AdicionarRegistro(emp3);

            Reserva rev1 = new Reserva(m2, r2);
            m2.SetRevista(r2);
            r2.StatusGuardada = false;


            Reserva rev2 = new Reserva(m3, r1);
            rev2.StatusCancelada = true;

            Reserva rev3 = new Reserva(m2, r3);
            rev3.StatusCancelada = true;

            controladorReserva.AdicionarRegistro(rev1);
            controladorReserva.AdicionarRegistro(rev2);
            controladorReserva.AdicionarRegistro(rev3);

            controladorEmprestimo.SelecionarTodosRegistrosVencidos();
        }
        public void IniciarMenuPrincipal()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Seja Bem Vindo Ao Menu Principal.");
                Console.WriteLine("\nInforme a opção desejada de tela:\n [1] Amigo\n [2] Categoria\n [3] Caixa\n [4] Revista\n [5] Reserva\n [6] Emprestimo");
                int opcao = Convert.ToInt32(Console.ReadLine());

                ICadastravel tela = PegarTelaDesejada(opcao);
                
                bool deveContinuar = true;

                while (deveContinuar)
                {
                    if (tela == null) continue;
                    deveContinuar = OpcaoSubmenu(tela);
                }

            }
        }
        private bool OpcaoSubmenu(ICadastravel tela)
        {
            Console.Clear();
            ((TelaBase)tela).ConfigurarTela();

            string opcaoStr = ((TelaBase)tela).ObterOpcao();

            switch (opcaoStr.ToLower())
            {
                case "1":
                    tela.InserirNovoRegistro();
                    break;
                case "2":
                    tela.VisualizarRegistros();
                    break;
                case "3":
                    tela.ExcluirRegistro();
                    break;
                case "4":
                    tela.EditarRegistro();
                    break;
                case "s":
                    return false;
            }

            return true;
        }
        private ICadastravel PegarTelaDesejada(int opcao)
        {
            switch (opcao)
            {
                case 1:
                    return telaAmigo;
                case 2:
                    return telaCategoria;
                case 3:
                    return telaCaixa;
                case 4:
                    return telaRevista;
                case 5:
                    return telaReserva;
                case 6:
                    return telaEmprestimo;
            }
            return null;
        }
    }
}
