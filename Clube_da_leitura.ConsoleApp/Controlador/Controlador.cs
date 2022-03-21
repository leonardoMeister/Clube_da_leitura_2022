using Clube_da_leitura.ConsoleApp.Dominio;
using System.Collections.Generic;

namespace Clube_da_leitura.ConsoleApp.Controlador
{
    public class Controlador<T> where T : EntidadeBase
    {
        public List<T> lista = new List<T>();

        public bool ExcluirRegistro(int id)
        {
            lista.RemoveAt(lista.FindIndex(x => x.id == id));
            return true;
        }
        public bool ExisteRegistroComEsteId(int id)
        {
            foreach (T itens in lista) if (itens.id == id) return true;
            return false;
        }

        public void AdicionarRegistro(T item)
        {
            lista.Add(item);
        }

        public void EditarRegistro(int id, T item)
        {
            lista[lista.FindIndex(x => x.id == id)] = item;
        }

        public List<T> SelecionarTodosRegistros()
        {
            return lista;
        }

        public bool ExisteRegistrosNaLista()
        {
            if (lista.Count > 0)
            {
                return true;
            }else return false;
        }

        public T SelecionarRegistroPorId(T item)
        {
            return lista[lista.FindIndex(x => x.id == item.id)];
        }
    }
}
