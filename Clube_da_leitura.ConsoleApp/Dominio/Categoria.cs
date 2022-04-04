using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clube_da_leitura.ConsoleApp.Dominio
{
    public class Categoria :EntidadeBase
    {
        private string _nomeCategoria;
        private int _quantidadeDias;

        private static int idEstatico = 1;

        public string NomeCategoria { get => _nomeCategoria;  }
        public int QuantidadeDias { get => _quantidadeDias;  }
        protected void AtribuirId()
        {
            this._id = idEstatico;
            idEstatico++;
        }
        public Categoria(string nome, int quantidadeDias)
        {
            this._nomeCategoria = nome;
            this._quantidadeDias = quantidadeDias;
            AtribuirId();
        }

        public Categoria(int id)
        {
            this._id = id;
        }

        internal void EditarCategoria(string nome, int numeroDias)
        {
            this._nomeCategoria = nome;
            this._quantidadeDias = numeroDias;
        }
    }
}
