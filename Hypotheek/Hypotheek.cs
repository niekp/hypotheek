using System;
using System.Collections.Generic;

namespace Hypotheek
{
    public class Hypotheek
    {
        private double hypotheek = 0;
        private int maandbedrag = 0;
        private double rente = 0;

        private List<Periode> periodes = new List<Periode>();

        public void main()
        {

            if (getInputs())
            {
                if (calculateData())
                {
                    displayData();
                }
            }

        }

        private bool getInputs()
        {
            hypotheek = 0;
            maandbedrag = 0;
            rente = 0;

            try
            {
                Console.WriteLine("Hypotheek?");
                hypotheek = double.Parse(Console.ReadLine().Replace(",", "."));
                Console.WriteLine("Maandbedrag?");
                maandbedrag = int.Parse(Console.ReadLine());
                Console.WriteLine("Rente percentage?");
                rente = double.Parse(Console.ReadLine().Replace(".", ","));

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Foute input");
                Console.WriteLine(ex.InnerException);

                return false;
            }
        }

        private bool calculateData()
        {
            int jaar = DateTime.Now.Year;
            int maand = DateTime.Now.Month;

            while (hypotheek > 0)
            {

                try
                {

                    double rentebedrag = (hypotheek / 100 * rente) / 12;
                    double aflossing = maandbedrag - rentebedrag;
                    hypotheek -= aflossing;

                    periodes.Add(new Periode() { Jaar = jaar, Maand = maand, Huidig = hypotheek, Rente = rentebedrag, Aflossen = aflossing });

                    maand++;
                    if (maand > 12)
                    {
                        maand = 1;
                        jaar++;
                    }
                    if (aflossing <= 0)
                    {
                        Console.WriteLine(String.Format("Maandbedrag is te laag (rentebedrag: {0})", rentebedrag));
                        return false;
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException);
                }

            }
            return true;

        }

        private string PadNumber(double input, int size)
        {
            string output = input.ToString();
            for (int n = output.Length; n < size; n++)
            {
                output = " " + output;
            }
            return output;
        }

        private void displayData()
        {
            string regel = "|| MM-JJJJ | Aflossen |    Rente |  Restant ||";
            Console.WriteLine(regel);
            int jaarVorig = 0;
            double jaarAfgelost = 0;
            double jaarRente = 0;
            int jaarMaanden = 0;
            foreach (Periode p in periodes)
            {

                if (jaarVorig > 0 && jaarVorig != p.Jaar)
                {
                    regel = "|| Samenvatting " + jaarVorig + System.Environment.NewLine;
                    regel += "|| Aflossen: " + jaarAfgelost.ToString("C") + " (" + Math.Round(jaarAfgelost / jaarMaanden, 2).ToString("C") + " p.m.)" + System.Environment.NewLine;
                    regel += "|| Rente   : " + jaarRente.ToString("C") + " (" + Math.Round(jaarRente / jaarMaanden, 2).ToString("C") + " p.m.)" + System.Environment.NewLine;
                    Console.WriteLine(regel);
                    jaarRente = 0;
                    jaarAfgelost = 0;
                    jaarMaanden = 0;
                }

                regel = "";
                regel += "|| " + p.Maand.ToString("D2") + "-" + p.Jaar + " | ";
                regel += PadNumber(p.Aflossen, 8) + " | ";
                regel += PadNumber(p.Rente, 8) + " | ";
                regel += PadNumber(p.Huidig, 8) + " ||";

                Console.WriteLine(regel);

                jaarAfgelost += p.Aflossen;
                jaarRente += p.Rente;
                jaarVorig = p.Jaar;
                jaarMaanden++;
            }
        }
    }
}
