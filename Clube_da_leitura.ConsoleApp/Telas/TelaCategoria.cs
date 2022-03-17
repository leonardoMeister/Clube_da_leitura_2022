using Clube_da_leitura.ConsoleApp.Controlador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clube_da_leitura.ConsoleApp.Telas
{
    public class TelaCategoria : TelaBase, ICadastravel
    {
        public ControladorCategoria controladorCategoria;

        public TelaCategoria(string tit, ControladorCategoria contro) : base(tit)
        {
            this.controladorCategoria = contro;
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
