using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirusSimulation
{
    class Program
    {
        static Random random = new Random();
        static int outputLineLength = 10;
        public static List<Person> population = new List<Person>();
        static int populationNumber = 100;
        static int infectedNumber = 1;
        static int transmissionSpeed = 1;

        static void Main(string[] args)
        {
            /*Initialise();
            OutputPopulation();

            Console.ReadLine();*/
            NumberAround(25);
        }

        public static void Initialise()
        {
            population.Clear();

            for (int i = 0; i < populationNumber; i++)
            {
                Person newPerson = new Person();
                newPerson.id = i;
                population.Add(newPerson);

            }

            for (int i = 0; i < infectedNumber; i++)
            {
                Person selectedPerson = population[random.Next(0, population.Count)];

                if (selectedPerson.infected == false)
                {
                    selectedPerson.infected = true;
                }

                else
                {
                    i--;
                }
            }
        }

        public static void OutputPopulation()
        {
            foreach (Person person in population)
            {
                if (person.infected)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }

                Console.Write("■");

                if ((population.IndexOf(person) + 1) % outputLineLength == 0)
                {
                    Console.Write("\n");
                }

                else
                {
                    Console.Write(" ");
                }

                Console.ResetColor();
            }
        }

        public static void SpreadVirus()
        {

        }

        public static void GetPeopleAround(Person person)
        {
            // s
        }

        public static List<int> NumberAround(int number)
        {
            List<int> numbersAround = new List<int>();

            numbersAround.Add(number - 1);
            numbersAround.Add(number + 1);
        }
    }
}
