﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clube_da_leitura.ConsoleApp.Dominio
{
    public class Caixa : EntidadeBase
    {
        public string corCaixa;
        public string etiqueta;
        public int numero;

        public List<Revista> listaRevistasNaCaixa;
        private static int idEstatico = 1;
        protected void AtribuirId()
        {
            this.id = idEstatico;
            idEstatico++;
        }
        public Caixa(string corCaixa, string etiqueta, int numero)
        {
            this.corCaixa = corCaixa;
            this.etiqueta = etiqueta;
            this.numero = numero;
            this.listaRevistasNaCaixa = new List<Revista>();
            AtribuirId();
        }
    }
}
