using Clube_da_leitura.ConsoleApp.Telas;
using System;


namespace Clube_da_leitura.ConsoleApp
{
    public class MenuPrincipal
    {
        TelaAmigo telaAmigo = new TelaAmigo("Tela Amigos");
        TelaCaixa telaCaixa = new TelaCaixa("Tela Caixas");
        TelaEmprestimo telaEmprestimo = new TelaEmprestimo("Tela Emprestimos");
        TelaRevista telaRevista = new TelaRevista("Tela Revista");

        public void IniciarMenuPrincipal()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Seja Bem Vindo Ao Menu Principal.");
                Console.WriteLine("\nInforme a opção desejada de tela:\n [1] Amigo\n [2] Revista\n [3] Caixa\n [4] Emprestimo");
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
                    return telaRevista;
                case 3:
                    return telaCaixa;
                case 4:
                    return telaEmprestimo;
            }
            return null;
        }
    }
}
