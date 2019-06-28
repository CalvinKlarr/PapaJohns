using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PapaJohnsCODE
{
    public class Mesa
    {
        public int id { get; set; }

        
        private string estado; //libre o reservada.
        public string Estado { get { return estado; } set { this.estado = value; } }

        private DateTime reservacion;
        public DateTime Reservacion { get { return reservacion; } set { this.reservacion = value; } }

        private string cliente;
        public string Cliente { get { return cliente; } set { this.cliente = value; } }

        private string imageName;
        public string ImageName { get { return imageName; } set { this.imageName = value; } }

        private string tipoDeMesa;
        public string TipoDeMesa { get { return tipoDeMesa; } set { this.tipoDeMesa = value; } }

        private int consumo;
        public int Consumo { get { return consumo; } set { this.consumo = value; } }


        public Mozo mozito;

        public Mesa()
        {
            estado = "Libre";
            cliente = "Por el momento nadie";

        }

        public void update(DateTime toCheck)
        {
            if(this.reservacion.DayOfYear == toCheck.DayOfYear)
            {
                if(toCheck.Hour >= this.reservacion.Hour)
                {
                    estado = "Reservada";
                }
            }
            else
            {
                estado = "Libre";
            }

        }
        public override string ToString()
        {
            if (mozito != null)
            {
                return "Estado: " + estado + "\nCliente: " + cliente + "\nConsumo: " + consumo + "\nMozo: " + mozito.ToString();
            }
            else
            {
                return "Estado: " + estado + "\nCliente: " + cliente + "\nConsumo: " + consumo + "\nMozo: No hay mozo ";
            }
        }



    }
}
