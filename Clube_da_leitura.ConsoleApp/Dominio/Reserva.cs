using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clube_da_leitura.ConsoleApp.Dominio
{
    public class Reserva: EntidadeBase
    {
        public Amigo amigoReserva;
        public Revista revistaReserva;
        public DateTime dataCriacaoReserva;
        public DateTime dataVencimentoReserva;

        private static int idEstatico = 1;

        public bool statusFoiEmprestada;
        protected void AtribuirId()
        {
            this.id = idEstatico;
            idEstatico++;
        }
        public Reserva(int id)
        {
            this.id = id;
        }
        public Reserva(Amigo amigo, Revista rev)
        {
            this.statusFoiEmprestada = false;
            this.dataCriacaoReserva = DateTime.Now;
            this.dataVencimentoReserva = (DateTime.Now).AddDays(2);
            this.amigoReserva = amigo;
            this.revistaReserva = rev;
            AtribuirId();
        }
        public bool ReservaEstaVencida()
        {
            if (dataVencimentoReserva < DateTime.Now)
            {
                return true;
            }
            else return false;
        }
        public Emprestimo TransformarEmEmprestimo(DateTime dataVencimentoEmprestimo)
        {
            this.statusFoiEmprestada = true;

            Emprestimo emp = new Emprestimo(amigoReserva, revistaReserva, DateTime.Now, dataVencimentoEmprestimo);

            return emp;
        }

    }
}
