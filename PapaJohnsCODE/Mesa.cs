using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PapaJohnsCODE
{
    class Mesa
    {
        public int id { get { return id; } set { this.id = value;  } }

        private List<bool> silla; //cantidad y disponibilidad(depende de la cantidad de nodos q tenga es la cantidad y la disponibilidad de si es true o false.
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
