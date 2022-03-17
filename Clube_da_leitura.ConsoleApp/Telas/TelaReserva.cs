using Clube_da_leitura.ConsoleApp.Controlador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clube_da_leitura.ConsoleApp.Telas
{
    public class TelaReserva : TelaBase, ICadastravel
    {
        public ControladorReserva controReserva;
        public ControladorAmigo controAmigo;
        public ControladorRevista controRevista;
        public ControladorEmprestimo controladorEmprestimo;
        public TelaReserva(string tit, ControladorReserva controReserva, ControladorAmigo controAmigo, ControladorRevista controRevista, ControladorEmprestimo controladorEmprestimo) : base(tit)
        {
            this.controReserva = controReserva;
            this.controAmigo = controAmigo;
            this.controRevista = controRevista;
            this.controladorEmprestimo = controladorEmprestimo;
        }

        public void EditarRegistro()
        {

        }

        public void ExcluirRegistro()
        {

        }

        public void InserirNovoRegistro()
        {

        }

        public void VisualizarRegistros()
        {

        }
    }
}
