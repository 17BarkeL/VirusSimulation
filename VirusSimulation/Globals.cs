using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

public static class Globals
{
    public static Random random = new Random();
    public static List<Person> population = new List<Person>();
    public static Timer simulationTimer;
    public static int outputLineLength = 10;
    public static int populationNumber = outputLineLength * 10;
    public static int updateInterval = 500;
    public static int infectedNumber = 3;
    public static int transmissionChance = 1;

    public static void Initialise()
    {
        Console.CursorVisible = false;
        simulationTimer = new Timer();

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
        List<Person> currentPopulation = population;
        List<Person> newInfected = new List<Person>();

        foreach (Person person in currentPopulation)
        {
            if (person.infected)
            {
                List<Person> possibleInfected = GetPeopleAround(person);

                foreach (Person possible in possibleInfected)
                {
                    if (possible.infected)
                    {
                        continue;
                    }

                    int transmissionNumber = random.Next(1, transmissionChance + 1);

                    if (transmissionNumber == 1)
                    {
                        newInfected.Add(possible);
                    }
                }
            }
        }

        foreach (Person newInfectedP in newInfected)
        {
            newInfectedP.infected = true;
        }
    }

    public static List<Person> GetPeopleAround(Person person)
    {
        int personId = person.id;
        List<Person> peopleAround = new List<Person>();
        List<int> idsAround = new List<int>();
        int idAbove = personId - outputLineLength;
        int idBelow = personId + outputLineLength;
        bool onTop = false;
        bool onBottom = false;
        bool onLeft = false;
        bool onRight = false;

        idsAround.Add(idAbove - 1);
        idsAround.Add(idAbove);
        idsAround.Add(idAbove + 1);
        idsAround.Add(personId - 1);
        idsAround.Add(personId + 1);
        idsAround.Add(idBelow - 1);
        idsAround.Add(idBelow);
        idsAround.Add(idBelow + 1);

        foreach (int num in idsAround.ToArray())
        {
            if (num < 0)
            {
                idsAround.Remove(num);
            }

            if (personId % outputLineLength == 0) { onLeft = true; }
            if ((personId + 1) % outputLineLength == 0) { onRight = true; }
            if (personId < outputLineLength) { onTop = true; }
            if (personId >= populationNumber - outputLineLength) { onBottom = true; }

            if (onLeft)
            {
                idsAround.Remove(idAbove - 1);
                idsAround.Remove(personId - 1);
                idsAround.Remove(idBelow - 1);
            }

            if (onRight)
            {
                idsAround.Remove(idAbove + 1);
                idsAround.Remove(personId + 1);
                idsAround.Remove(idBelow + 1);
            }

            if (onTop)
            {
                idsAround.Remove(idAbove - 1);
                idsAround.Remove(idAbove);
                idsAround.Remove(idAbove + 1);
            }

            if (onBottom)
            {
                idsAround.Remove(idBelow - 1);
                idsAround.Remove(idBelow);
                idsAround.Remove(idBelow + 1);
            }
        }

        foreach (int id in idsAround)
        {
            peopleAround.Add(population[id]);
        }

        foreach (Person persond in peopleAround)
        {
            //Console.Write($"{persond.id}, ");
        }

        return peopleAround;
    }

    public static List<int> NumberAround(int number)
    {
        List<int> numbersAround = new List<int>();
        int numberAbove = number - outputLineLength;
        int numberBelow = number + outputLineLength;
        bool onTop = false;
        bool onBottom = false;
        bool onLeft = false;
        bool onRight = false;

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

            if (number % outputLineLength == 0) { onLeft = true; }
            if ((number + 1) % outputLineLength == 0) { onRight = true; }
            if (number < outputLineLength) { onTop = true; }
            if (number >= populationNumber - outputLineLength) { onBottom = true; }

            if (onLeft)
            {
                numbersAround.Remove(numberAbove - 1);
                numbersAround.Remove(number - 1);
                numbersAround.Remove(numberBelow - 1);
            }

            if (onRight)
            {
                numbersAround.Remove(numberAbove + 1);
                numbersAround.Remove(number + 1);
                numbersAround.Remove(numberBelow + 1);
            }

            if (onTop)
            {
                numbersAround.Remove(numberAbove - 1);
                numbersAround.Remove(numberAbove);
                numbersAround.Remove(numberAbove + 1);
            }

            if (onBottom)
            {
                numbersAround.Remove(numberBelow - 1);
                numbersAround.Remove(numberBelow);
                numbersAround.Remove(numberBelow + 1);
            }
        }

        Console.WriteLine($"Left: {onLeft}, Right: {onRight}, Top: {onTop}, Bottom: {onBottom}");

        return numbersAround;
    }
}
