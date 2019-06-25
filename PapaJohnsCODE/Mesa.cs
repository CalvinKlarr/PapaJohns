using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PapaJohnsCODE
{
    public class Mesa
    {
        public int id { get { return id; } set { this.id = value;  } }

        
        private string estado; //libre, ocupada o reservada.
        public string Estado { get { return estado; } set { this.estado = value; } }

        private string cliente;
        public string Cliente { get { return cliente; } set { this.cliente = value; } }

        private int consumo;
        public int Consumo { get { return consumo; } set { this.consumo = value; } }

        public Mozo mozito;

        public Mesa()
        {

        }



    }
}
