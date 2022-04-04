using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clube_da_leitura.ConsoleApp.Dominio
{
    public class Caixa : EntidadeBase
    {
        private string _corCaixa;
        private string _etiqueta;
        private int _numero;
        private List<Revista> _listaRevistasNaCaixa;

        private static int idEstatico = 1;

        public string CorCaixa { get => _corCaixa;  }
        public string Etiqueta { get => _etiqueta;  }
        public int Numero { get => _numero;  }
        public List<Revista> ListaRevistasNaCaixa { get => _listaRevistasNaCaixa;  }

        protected void AtribuirId()
        {
            this._id = idEstatico;
            idEstatico++;
        }
        public Caixa(int id)
        {
            this._id = id;
        }
        public Caixa(string corCaixa, string etiqueta, int numero)
        {
            this._corCaixa = corCaixa;
            this._etiqueta = etiqueta;
            this._numero = numero;
            this._listaRevistasNaCaixa = new List<Revista>();
            AtribuirId();
        }
        public void AdicionarRevistaNaCaixa(Revista rev)
        {
            ListaRevistasNaCaixa.Add(rev);
        }
        public void RemoverRevistadaCaixa(Revista rev)
        {
            if(ListaRevistasNaCaixa.Count > 0)
            {
                ListaRevistasNaCaixa.Remove(rev);
            }
        }

        internal void EditarCaixa(string corCaixa, string etiqueta, int numero)
        {
            this._corCaixa = corCaixa;
            this._etiqueta = etiqueta;
            this._numero = numero;
        }
    }
}
