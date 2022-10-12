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
            // Console optimisation: only change new infected people not re-draw the whole console
            Globals.Initialise();
            Globals.OutputPopulation();

            /*Console.SetCursorPosition(Globals.population[99].X, Globals.population[99].Y);
            Console.Write("\b");
            Console.Write("@");*/

            updateTimer = new Timer(Globals.updateInterval);
            updateTimer.Elapsed += Update;
            updateTimer.Start();

            Console.ReadLine();
        }

        public static void Update(object sender, EventArgs e)
        {
            Globals.SpreadVirus();
            //Console.Clear();

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
