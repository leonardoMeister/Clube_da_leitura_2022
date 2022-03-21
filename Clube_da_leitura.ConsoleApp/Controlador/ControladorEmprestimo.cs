﻿using Clube_da_leitura.ConsoleApp.Dominio;
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
        public bool QuitarDividaPorDevolucaoDeAmigo(Amigo amigo)
        {
            List<Emprestimo> lista = SelecionarTodosRegistros();

            foreach(Emprestimo emp in lista)
            {
                if(emp.statusDevolucao == false && emp.amigoEmprestimo.id == amigo.id)
                {
                    emp.statusDevolucao = true;
                    emp.amigoEmprestimo.revistaEmprestada = null;
                    emp.revista.statusGuardada = true;
                    
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
            AplicarMultaEmRegistrosVencidos(listNova);
            return listNova;
        }

        internal void AplicarMultaEmRegistrosVencidos(List<Emprestimo> listaRegVencidos)
        {
            foreach (Emprestimo emp in listaRegVencidos)
            {
                emp.amigoEmprestimo.statusPossuiMulta = true;
            }
            return;
        }
    }
}
