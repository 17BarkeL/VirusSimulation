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
            OutputPopulation();*/
            List<int> numbers = NumberAround(1);

            for (int i = 0; i < numbers.Count; i++)
            {
                Console.WriteLine(numbers[i]);
            }

            Console.ReadLine();
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
            int numberAbove = number - outputLineLength;
            int numberBelow = number + outputLineLength;
            // One list and remove negatives
            // broken with 0
            //14, 15, 16, 24, 25, 26, 34, 35, 36

            numbersAround.Add(numberAbove - 1);
            numbersAround.Add(numberAbove);
            numbersAround.Add(numberAbove + 1);
            numbersAround.Add(number - 1);
            numbersAround.Add(number + 1);
            numbersAround.Add(numberBelow - 1);
            numbersAround.Add(numberBelow);
            numbersAround.Add(numberBelow + 1);

            foreach (int num in numbersAround.ToArray())
            {
                if (num < 0)
                {
                    numbersAround.Remove(num);
                }
            }

            return numbersAround;
        }
    }
}
