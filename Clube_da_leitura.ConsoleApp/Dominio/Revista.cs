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
        public DateTime dataAnoImpressao;
        public bool statusGuardada;

        public Caixa caixaOndeFicaGuardada;
        private static int idEstatico = 1;
        protected void AtribuirId()
        {
            this.id = idEstatico;
            idEstatico++;
        }
        public bool RevistaPodeSerEmprestada()
        {
            if (statusGuardada) return true;
            return false;
        }
        public Revista(int id)
        {
            this.id = id;
        }
        public Revista(string tipoColecao, int numeroEdicao, DateTime anoImpressao)
        {
            this.tipoColecao = tipoColecao;
            this.numeroEdicao = numeroEdicao;
            dataAnoImpressao = anoImpressao;
            this.statusGuardada = true;
            AtribuirId();
        }

        public void AtribuirUmaCaixaDestino(Caixa caixa)
        {
            this.caixaOndeFicaGuardada = caixa;
        }
    }
}
