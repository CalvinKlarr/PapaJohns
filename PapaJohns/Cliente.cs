using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PapaJohns
{
    class Cliente
    {

        public int id { get; set; }
        private int consumos;
        public int Consumos { get { return consumos; } set { this.consumos += value;  } }

        public Cliente()
        {

        }
    }
}
