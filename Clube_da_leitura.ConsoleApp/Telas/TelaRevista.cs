using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clube_da_leitura.ConsoleApp.Telas
{
    public class TelaRevista : TelaBase, ICadastravel
    {
        public TelaRevista(string tit) : base(tit)
        {
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

        public bool VisualizarRegistros()
        {
            return true;
        }
    }
}
