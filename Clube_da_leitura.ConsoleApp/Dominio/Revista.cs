using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clube_da_leitura.ConsoleApp.Dominio
{
    public  class Revista : EntidadeBase
    {
        private string _tipoColecao;
        private int _numeroEdicao;
        private DateTime _dataAnoImpressao;
        private bool statusGuardada;
        private Categoria _categoria;
        private Caixa _caixaOndeFicaGuardada;

        private static int idEstatico = 1;

        public string TipoColecao { get => _tipoColecao; }
        public int NumeroEdicao { get => _numeroEdicao; }
        public DateTime DataAnoImpressao { get => _dataAnoImpressao; }
        public Categoria Categoria { get => _categoria; }
        public Caixa CaixaOndeFicaGuardada { get => _caixaOndeFicaGuardada; }
        public bool StatusGuardada { get => statusGuardada; set => statusGuardada = value; }

        protected void AtribuirId()
        {
            this._id = idEstatico;
            idEstatico++;
        }
        public bool RevistaPodeSerEmprestada()
        {
            if (StatusGuardada) return true;
            return false;
        }
        public Revista(int id)
        {
            this._id = id;
        }
        public Revista(string tipoColecao, int numeroEdicao, DateTime anoImpressao, Categoria cate)
        {
            this._tipoColecao = tipoColecao;
            this._numeroEdicao = numeroEdicao;
            this._dataAnoImpressao = anoImpressao;
            this._categoria = cate;
            this.StatusGuardada = true;
            AtribuirId();
        }

        public void AtribuirUmaCaixaDestino(Caixa caixa)
        {
            this._caixaOndeFicaGuardada = caixa;
        }

        internal void EditarRevista(string colecao, int numeroEdicao, DateTime data)
        {
            this._tipoColecao = colecao;
            this._numeroEdicao = numeroEdicao;
            this._dataAnoImpressao = data;
        }
    }
}
