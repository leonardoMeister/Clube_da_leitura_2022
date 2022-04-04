using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clube_da_leitura.ConsoleApp.Dominio
{
    public class Emprestimo : EntidadeBase
    {
        private Amigo _amigoEmprestimo;
        private Revista _revista;
        private DateTime _dataCriacao;
        private DateTime _dataVencimento;
        private bool _statusDevolucao;

        private static int idEstatico = 1;

        public Amigo AmigoEmprestimo { get => _amigoEmprestimo; }
        public Revista Revista { get => _revista; }
        public DateTime DataCriacao { get => _dataCriacao; }
        public DateTime DataVencimento { get => _dataVencimento; }
        public bool StatusDevolucao { get => _statusDevolucao; set => _statusDevolucao = value; }

        protected void AtribuirId()
        {
            this._id = idEstatico;
            idEstatico++;
        }
        public Emprestimo(int id)
        {
            this._id = id;
        }
        public Emprestimo(Amigo amigoEmprestimo, Revista revista, DateTime dataCriacao)
        {
            this._amigoEmprestimo = amigoEmprestimo;
            this._revista = revista;
            this._dataCriacao = dataCriacao;
            this._dataVencimento = dataCriacao.AddDays(revista.Categoria.QuantidadeDias);
            this._statusDevolucao = false;
            AtribuirId();
        }

        internal void EditarEmprestimo(DateTime novaDataDevolucao)
        {
            this._dataVencimento = novaDataDevolucao;
        }

        internal void FazerDevolucaoEmprestimo(bool statusDevolucao = true, bool statusRevistaGuardada = true)
        {
            this._statusDevolucao = statusDevolucao;
            this._amigoEmprestimo.SetRevista(null);
            this._revista.StatusGuardada = statusRevistaGuardada;
        }
    }
}
