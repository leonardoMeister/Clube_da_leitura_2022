using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clube_da_leitura.ConsoleApp.Dominio
{
    public class Amigo: EntidadeBase
    {
        public string nome;
        public string nomeResponsavel;
        public string telefone;
        public string endereco;

        public Revista revistaEmprestada;

        private static int idEstatico = 1;
        protected void AtribuirId()
        {
            this.id = idEstatico;
            idEstatico++;
        }

        public bool PodeEmprestarRevista()
        {
            if (revistaEmprestada == null) return true;
            else return false;
            
        }

        public Amigo(string nome, string nomeResponsavel, string telefone, string endereco)
        {
            this.nome = nome;
            this.nomeResponsavel = nomeResponsavel;
            this.telefone = telefone;
            this.endereco = endereco;
            AtribuirId();
        }
        public Amigo(int id)
        {
            this.id = id;
        }

        public bool ContemUmaRevistaEmEmprestimo()
        {
            if (revistaEmprestada == null) return false;
            return true;
        }

    }
}
