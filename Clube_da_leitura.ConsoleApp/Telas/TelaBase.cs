using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clube_da_leitura.ConsoleApp.Telas
{
    public abstract class TelaBase
    {
        private string titulo;

        public string Titulo { get { return titulo; } }

        public TelaBase(string tit)
        {
            titulo = tit;
        }
        public virtual string ObterOpcao()
        {
            Console.WriteLine("Digite 1 para inserir novo registro");
            Console.WriteLine("Digite 2 para visualizar registros");
            Console.WriteLine("Digite 3 para excluir um registro");
            Console.WriteLine("Digite 4 para editar um registro");
            Console.WriteLine("Digite S para Voltar");
            Console.WriteLine();

            Console.Write("Opção: ");
            string opcao = Console.ReadLine();

            return opcao;
        }

        public virtual void ImprimirFinalizacao(string text = "", ConsoleColor cor = ConsoleColor.Red)
        {
            Console.WriteLine(); 
            Console.ForegroundColor = cor;
            if (text != "") Console.WriteLine(text);
            Console.ResetColor();
            Console.WriteLine("PRESS ENTER TO CONTINUE...");
            Console.ReadKey();
            Console.Clear();
        }

        public virtual void ImprimirColorido(string text, ConsoleColor cor)
        {
            Console.ForegroundColor = cor;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        public void MontarCabecalhoTabela(string configuracaoColunasTabela, params object[] colunas)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine(configuracaoColunasTabela, colunas);

            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------");

            Console.ResetColor();
        }
        public void ConfigurarTela()
        {
            Console.Clear();

            Console.WriteLine(titulo);

            Console.WriteLine();

            //Console.WriteLine(subtitulo);

            //Console.WriteLine();
        }
    }
}
