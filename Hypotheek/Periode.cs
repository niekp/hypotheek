using System;
namespace Hypotheek
{
    public class Periode
    {
        public int Jaar { get; set; }
        public int Maand { get; set; }
        private double huidig;
        private double rente;
        private double aflossen;

        public double Huidig
        {
            get
            {
                return Math.Round(this.huidig, 2);
            }
            set
            {
                this.huidig = value;

                if (this.huidig < 0)
                {
                    this.huidig = 0;
                }
            }
        }
        public double Rente
        {
            get
            {
                return Math.Round(this.rente, 2);
            }
            set
            {
                this.rente = value;
            }
        }

        public double Aflossen
        {
            get
            {
                return Math.Round(this.aflossen, 2);
            }
            set
            {
                this.aflossen = value;
            }
        }


    }
}
