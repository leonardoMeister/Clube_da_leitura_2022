using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clube_da_leitura.ConsoleApp.Dominio
{
    public  class Revista : EntidadeBase
    {
        public string tipoColecao;
        public int numeroEdicao;
        public DateTime AnoImpressao;
        public bool statusEmprestadaGuardada;

        public Caixa caixaOndeFicaGuardada;
        private static int idEstatico = 1;
        protected void AtribuirId()
        {
            this.id = idEstatico;
            idEstatico++;
        }
        public Revista(string tipoColecao, int numeroEdicao, DateTime anoImpressao, bool statusEmprestadaGuardada)
        {
            this.tipoColecao = tipoColecao;
            this.numeroEdicao = numeroEdicao;
            AnoImpressao = anoImpressao;
            this.statusEmprestadaGuardada = statusEmprestadaGuardada;
            AtribuirId();
        }

        public void AtribuirUmaCaixaDestino(Caixa caixa)
        {
            this.caixaOndeFicaGuardada = caixa;
        }
    }
}
