using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clube_da_leitura.ConsoleApp.Dominio
{
    public class Emprestimo: EntidadeBase
    {
        public Amigo amigoEmprestimo;
        public Revista revista;
        public DateTime dataCriacao;
        public DateTime dataVencimento;
        private static int idEstatico = 1;
        protected void AtribuirId()
        {
            this.id = idEstatico;
            idEstatico++;
        }
        public Emprestimo(Amigo amigoEmprestimo, Revista revista, DateTime dataCriacao, DateTime dataVencimento)
        {
            this.amigoEmprestimo = amigoEmprestimo;
            this.revista = revista;
            this.dataCriacao = dataCriacao;
            this.dataVencimento = dataVencimento;
            AtribuirId();
        }
                
    }
}
