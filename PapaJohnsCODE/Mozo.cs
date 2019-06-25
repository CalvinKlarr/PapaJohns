using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PapaJohnsCODE
{
    public class Mozo
    {

        public int id { get; set; }

        public List<Mesa> mesas;


        public Mozo(Mesa meza)
        {
            this.mesas = new List<Mesa>();
            mesas.Add(meza);

        }

        public Mozo() { }

    }
}
