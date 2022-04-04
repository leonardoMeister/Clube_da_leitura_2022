using Clube_da_leitura.ConsoleApp.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clube_da_leitura.ConsoleApp.Controlador
{
    public class ControladorEmprestimo : Controlador<Emprestimo>
    {
        internal List<Emprestimo> SelecionarTodosRegistrosAbertos()
        {
            List<Emprestimo> list = new List<Emprestimo>(SelecionarTodosRegistros());
            List<Emprestimo> listNova = new List<Emprestimo>();

            foreach (Emprestimo emp in list)
            {
                if (emp.StatusDevolucao == false) listNova.Add(emp);
            }
            return listNova;
        }
        internal List<Emprestimo> SelecionarTodosRegistrosFechados()
        {
            List<Emprestimo> list = SelecionarTodosRegistros();
            List<Emprestimo> listNova = new List<Emprestimo>();
            foreach (Emprestimo emp in list)
            {
                if (emp.StatusDevolucao == true) listNova.Add(emp);
            }
            return listNova;
        }
        public bool QuitarDividaPorDevolucaoDeAmigo(Amigo amigo)
        {
            List<Emprestimo> lista = SelecionarTodosRegistros();

            foreach(Emprestimo emp in lista)
            {
                if(emp.StatusDevolucao == false && emp.AmigoEmprestimo._id == amigo._id)
                {
                    emp.FazerDevolucaoEmprestimo();
                    
                    return true;
                }
            }
            return false;
        }
        internal List<Emprestimo> SelecionarTodosRegistrosPorMes(int mes)
        {
            List<Emprestimo> list = new List<Emprestimo>(SelecionarTodosRegistros());
            List<Emprestimo> listNova = new List<Emprestimo>();

            foreach (Emprestimo emp in list)
            {
                if (emp.DataVencimento.Month == mes) listNova.Add(emp);
            }
            return listNova;
        }
        internal List<Emprestimo> SelecionarTodosRegistrosVencidos()
        {
            List<Emprestimo> list = new List<Emprestimo>(SelecionarTodosRegistros());
            List<Emprestimo> listNova = new List<Emprestimo>();

            foreach (Emprestimo emp in list)
            {
                if (emp.DataVencimento < DateTime.Now && emp.StatusDevolucao == false) listNova.Add(emp);
            }
            AplicarMultaEmRegistrosVencidos(listNova);
            return listNova;
        }

        internal void AplicarMultaEmRegistrosVencidos(List<Emprestimo> listaRegVencidos)
        {
            foreach (Emprestimo emp in listaRegVencidos)
            {
                emp.AmigoEmprestimo.StatusPossuiMulta = true;
            }
            return;
        }
    }
}
