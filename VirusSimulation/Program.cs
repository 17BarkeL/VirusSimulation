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
        //REMOVE OLD INFECTED FROM DRAWING AGAIN
        public static Timer updateTimer;

        static void Main(string[] args)
        {
            Globals.Initialise();
            Globals.OutputPopulation();

            updateTimer = new Timer(Globals.updateInterval);
            updateTimer.Elapsed += Update;
            updateTimer.Start();

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
            Console.SetCursorPosition(Globals.infectedPercentageCoordinates[0], Globals.infectedPercentageCoordinates[1]);
            Console.Write("\b");
            Console.Write($" {((float)infectedNumber / (float)Globals.population.Count) * 100}%                             ");
            Globals.OutputPopulation();
        }
    }
}
