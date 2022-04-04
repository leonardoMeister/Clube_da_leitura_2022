using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clube_da_leitura.ConsoleApp.Dominio
{
    public class Amigo : EntidadeBase
    {
        private string _nome;
        private string _nomeResponsavel;
        private string _telefone;
        private string _endereco;
        private bool _statusPossuiMulta;
        private Revista _revistaEmprestada;

        private static int idEstatico = 1;

        public Revista RevistaEmprestada { get => _revistaEmprestada;}
        public bool StatusPossuiMulta { get => _statusPossuiMulta; set => _statusPossuiMulta = value; }
        public string Nome { get => _nome;  }
        public string NomeResponsavel { get => _nomeResponsavel; }
        public string Telefone { get => _telefone;  }
        public string Endereco { get => _endereco;  }

        protected void AtribuirId()
        {
            this._id = idEstatico;
            idEstatico++;
        }
        public bool PodeEmprestarRevista()
        {
            if (RevistaEmprestada == null && StatusPossuiMulta == false) return true;
            else return false;

        }

        public Amigo(string nome, string nomeResponsavel, string telefone, string endereco)
        {
            this._nome = nome;
            this._nomeResponsavel = nomeResponsavel;
            this._telefone = telefone;
            this._endereco = endereco;
            this._statusPossuiMulta = false;
            AtribuirId();
        }
        public void EditarAmigo(string nome, string nomeResponsavel, string telefone, string endereco)
        {
            this._nome = nome;
            this._nomeResponsavel = nomeResponsavel;
            this._telefone = telefone;
            this._endereco = endereco;
        }

        public Amigo(int id)
        {
            this._id = id;
        }
        public bool ContemUmaRevistaEmEmprestimo()
        {
            if (RevistaEmprestada == null) return false;
            return true;
        }

        internal void SetRevista(Revista revista)
        {
            this._revistaEmprestada = revista;
        }
    }
}
