using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clube_da_leitura.ConsoleApp.Dominio
{
    public class Reserva: EntidadeBase
    {
        private Amigo _amigoReserva;
        private Revista _revistaReserva;
        private DateTime _dataCriacaoReserva;
        private DateTime _dataVencimentoReserva;
        private bool _statusCancelada;
        private bool _statusFoiEmprestada;

        private static int idEstatico = 1;

        public Amigo AmigoReserva { get => _amigoReserva; }
        public Revista RevistaReserva { get => _revistaReserva; }
        public DateTime DataCriacaoReserva { get => _dataCriacaoReserva;}
        public DateTime DataVencimentoReserva { get => _dataVencimentoReserva;}
        public bool StatusCancelada { get => _statusCancelada; set => _statusCancelada = value; }
        public bool StatusFoiEmprestada { get => _statusFoiEmprestada;}

        protected void AtribuirId()
        {
            this._id = idEstatico;
            idEstatico++;
        }
        public Reserva(int id)
        {
            this._id = id;
        }
        public Reserva(Amigo amigo, Revista rev)
        {
            this._statusFoiEmprestada = false;
            this._statusCancelada = false;
            this._dataCriacaoReserva = DateTime.Now;
            this._dataVencimentoReserva = (DateTime.Now).AddDays(2);
            this._amigoReserva = amigo;
            this._revistaReserva = rev;
            AtribuirId();
        }
        public string PegarStatusReserva()
        {
            return "";
        }
        public bool ReservaEstaVencida()
        {
            if (DataVencimentoReserva < DateTime.Now)
            {
                return true;
            }
            else return false;
        }
        public Emprestimo TransformarEmEmprestimo(DateTime dataVencimentoEmprestimo)
        {
            this._statusFoiEmprestada = true;

            Emprestimo emp = new Emprestimo(AmigoReserva, RevistaReserva, DateTime.Now);

            return emp;
        }

        internal void DarBaixaReserva(bool statusCancelada = true, bool statusRevistaGuardada =true)
        {
            _statusCancelada = statusCancelada;
            AmigoReserva.SetRevista(null);
            RevistaReserva.StatusGuardada = statusRevistaGuardada;
        }

        internal void SetarDadosAmigoRevista(Revista revista, bool statusRevistaGuardada = false)
        {
            _amigoReserva.SetRevista(revista);
            revista.StatusGuardada = statusRevistaGuardada;
        }
    }
}
