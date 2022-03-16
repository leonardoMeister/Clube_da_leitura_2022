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
                if (emp.statusDevolucao == false) listNova.Add(emp);
            }
            return listNova;
        }
        internal List<Emprestimo> SelecionarTodosRegistrosFechados()
        {
            List<Emprestimo> list = SelecionarTodosRegistros();
            List<Emprestimo> listNova = new List<Emprestimo>();
            foreach (Emprestimo emp in list)
            {
                if (emp.statusDevolucao == true) listNova.Add(emp);
            }
            return listNova;
        }
        internal List<Emprestimo> SelecionarTodosRegistrosPorMes(int mes)
        {
            List<Emprestimo> list = new List<Emprestimo>(SelecionarTodosRegistros());
            List<Emprestimo> listNova = new List<Emprestimo>();

            foreach (Emprestimo emp in list)
            {
                if (emp.dataVencimento.Month == mes) listNova.Add(emp);
            }
            return listNova;
        }
        internal List<Emprestimo> SelecionarTodosRegistrosVencidos()
        {
            List<Emprestimo> list = new List<Emprestimo>(SelecionarTodosRegistros());
            List<Emprestimo> listNova = new List<Emprestimo>();

            foreach (Emprestimo emp in list)
            {
                if (emp.dataVencimento < DateTime.Now && emp.statusDevolucao == false) listNova.Add(emp);
            }
            return listNova;
        }
    }
}
