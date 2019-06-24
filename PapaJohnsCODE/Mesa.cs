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

        private int tamaño;
        public int Tamaño { get { return tamaño; } set { this.tamaño = value;  } }

        private string forma; //redonda, cuadrada o rectangular
        public string Forma { get { return forma; } set { this.forma = value; } }

        private List<bool> silla; //cantidad y disponibilidad(depende de la cantidad de nodos q tenga es la cantidad y la disponibilidad de si es true o false.
        private string estado; //libre, ocupada o reservada.
        public string Estado { get { return estado; } set { this.estado = value; } }

        public Mozo mozito;

        public Mesa()
        {

        }



    }
}
