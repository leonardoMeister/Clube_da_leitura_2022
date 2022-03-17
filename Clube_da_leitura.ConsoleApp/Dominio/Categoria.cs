using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clube_da_leitura.ConsoleApp.Dominio
{
    public class Categoria :EntidadeBase
    {
        public string nomeCategoria;
        public int quantidadeDias;

        private static int idEstatico = 1;

        protected void AtribuirId()
        {
            this.id = idEstatico;
            idEstatico++;
        }

        public Categoria(string nome, int quantidadeDias)
        {
            this.nomeCategoria = nome;
            this.quantidadeDias = quantidadeDias;
            AtribuirId();
        }

        public Categoria(int id)
        {
            this.id = id;
        }

    }
}
