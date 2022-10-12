using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

public static class Globals
{
    // CANNOT CHOOSEONE ALREADY INFECTED WHICH REDUCES CHANCE FOR CORNERS TO GET INFECTED IN AROUND FUCNTION REMOVEONES THAT ARE INFECTED TO CHOOSE FROM
    // CANNOT CHOOSEONE ALREADY INFECTED WHICH REDUCES CHANCE FOR CORNERS TO GET INFECTED IN AROUND FUCNTION REMOVEONES THAT ARE INFECTED TO CHOOSE FROM
    // CANNOT CHOOSEONE ALREADY INFECTED WHICH REDUCES CHANCE FOR CORNERS TO GET INFECTED IN AROUND FUCNTION REMOVEONES THAT ARE INFECTED TO CHOOSE FROM
    public static Random random = new Random();
    public static List<Person> population = new List<Person>();
    public static List<Person> peopleToEdit = new List<Person>();
    public static Timer simulationTimer;
    public static int outputLineLength = 10;
    public static int populationNumber = outputLineLength * 10;
    public static int updateInterval = 1000;
    public static int infectedNumber = 3;
    public static int transmissionChance = 10;

    public static void Initialise()
    {
        Console.CursorVisible = false;
        simulationTimer = new Timer();

        population.Clear();

        for (int i = 0; i < populationNumber; i++)
        {
            Person newPerson = new Person();
            newPerson.id = i;
            newPerson.X = newPerson.id - (outputLineLength * (newPerson.id / outputLineLength));
            newPerson.Y = newPerson.id / outputLineLength;
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

            foreach (Person person in population)
            {
                if (person.infected)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }

                Console.Write("■");
                person.X = Console.CursorLeft;
                person.Y = Console.CursorTop;

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
    }

    public static void OutputPopulation()
    {
        /*foreach (Person person in population)
        {
            if (person.infected)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }

            Console.Write("■");

            if ((population.IndexOf(person) + 1) % outputLineLength == 0) //person.id
            {
                Console.Write("\n");
            }

            else
            {
                Console.Write(" ");
            }

            Console.ResetColor();
        }*/

        foreach (Person personToEdit in peopleToEdit)
        {
            Console.SetCursorPosition(personToEdit.X, personToEdit.Y);

            Console.Write("\b");

            if (personToEdit.infected)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }

            Console.Write("■");

            if ((personToEdit.id + 1) % outputLineLength == 0) //person.id
            {
                Console.Write("\n");
            }

            else
            {
                Console.Write(" ");
            }

            Console.ResetColor();
        }

        peopleToEdit.Clear();
    }

    public static void SpreadVirus()
    {
        List<Person> currentPopulation = population;
        List<Person> newInfected = new List<Person>();

        foreach (Person person in currentPopulation)
        {
            if (!person.infected)
            {
                List<Person> peopleAround = GetPeopleAround(person);

                foreach (Person possible in peopleAround)
                {
                    int transmissionNumber = random.Next(1, transmissionChance + 1);

                    if (transmissionNumber == 1)
                    {
                        newInfected.Add(possible);
                    }
                }
                // (1,0.5), (2,0.75), (3, 0.875) CHANCE MULTIPLIER
            }
        }

        foreach (Person newInfectedP in newInfected)
        {
            newInfectedP.infected = true;
            peopleToEdit.Add(newInfectedP);
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

        return peopleAround;
    }
}
