using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace VirusSimulation
{
    class Program
    {
        public static Timer updateTimer;

        static void Main(string[] args)
        {
            /*Globals.Initialise();
            Globals.OutputPopulation();

            updateTimer = new Timer(Globals.updateInterval);
            updateTimer.Elapsed += Update;
            updateTimer.Start();*/

            float transmissionChance = 4;
            float peopleAround = 2;
            float calc;

            calc = ((float)Math.Pow(transmissionChance, peopleAround) - 1) / (float)Math.Pow(transmissionChance, peopleAround);
            Console.WriteLine(calc);
            Console.ReadLine();
        }

        public static void Update(object sender, EventArgs e)
        {
            Globals.SpreadVirus();

            int infectedNumber = 0;

            foreach (Person person in Globals.population)
            {
                if (person.infected)
                {
                    infectedNumber++;
                }
            }

            //Console.WriteLine($"Infected: {((float)infectedNumber / (float)Globals.population.Count) * 100}%");
            Globals.OutputPopulation();
        }
    }
}
