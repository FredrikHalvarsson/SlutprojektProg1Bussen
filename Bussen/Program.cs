using System;
using System.Collections;
using System.Reflection;

namespace Bussen
{
    class Bus
    {
        public int totalAge;
        public Passenger[] passenger = new Passenger[25];
        public int passengerAmount = 0; //Antal passagerare
        public int realAndFakePsngr = 0; //Antal passagerare inkl. "Tomma platser"

        public void Run()   
        {
            Console.WriteLine("Welcome to the awesome Buss-simulator");
            int temp = 0;
            do
            {
                Console.WriteLine("Choose options: \n" +
                    "1: Add 1 passenger \n" +
                    "2: Show passenger list \n" +
                    "3: Calculate passenger total age \n" +
                    "4: Calculate passenger average age \n" +
                    "5: Find oldest passenger \n" +
                    "6: Find passengers within assigned age \n" +
                    "7: Sort passenger list \n" +
                    "8: Show list of Male/Female passengers \n" +
                    "9: Poke a passenger \n" +
                    " (Warning! Option not advised) \n" +
                    "10: Remove 1 passenger \n" +
                    "0: Exit program \n");

                temp = ExceptionManager(temp); //Kör metod för inmatning med undantagshantering
                switch (temp)
                {
                    case 1: AddPassenger();
                    break;
                    case 2: PrintBuss();
                    break;
                    case 3: CalcTotalAge();
                    break;
                    case 4: CalcAverageAge();
                    break;
                    case 5: MaxAge();
                    break;
                    case 6: FindAge();
                    break;
                    case 7: SortBus();
                    break;
                    case 8: PrintGender();
                    break;
                    case 9: Poke();
                    break;
                    case 10: PassengerDepart();
                    break;
                    case 11: System.Environment.Exit(1);
                        break;
                    case 0: Console.WriteLine("Exiting program");
                    break;
                    default:  
                        Console.Clear();
                        Console.WriteLine("Invalid input");
                        Task.Factory.StartNew(() => Console.ReadKey()).Wait(TimeSpan.FromSeconds(5.0));
                        Console.Clear();
                        break;
                }
            }while (temp != 0);
        }
        public int ExceptionManager(int temp)
        {
            bool intValid = false;
            while (!intValid)
            {
                try
                {
                    temp = Convert.ToInt32(Console.ReadLine());
                    intValid = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    Task.Factory.StartNew(() => Console.ReadKey()).Wait(TimeSpan.FromSeconds(5.0));
                    Console.Clear();
                    Run();
                }
            }
            return temp;
        }

        public void AddPassenger()
        {
            //Lägg till passagerare.
            Console.Clear();
            if (passengerAmount < 25) //Om bussen är full kan inte någon passagerare stiga på
            {
                for (int i = 0; i < 25; i++)
                {
                    if (passenger[i] == null||passenger[i].Age == 101)
                    {
                        Random slump = new Random();
                        int Age = slump.Next(1, 100);
                        string Gender = AssignGender();

                        passenger[i] = new Passenger(Age, Gender);
                        passengerAmount++;
                        if (realAndFakePsngr < 25)
                        {
                            realAndFakePsngr++;
                        }
                        Console.WriteLine($"A {Age} year old {Gender} boarded the bus. \n The bus now has {passengerAmount} passengers.");
                        Task.Factory.StartNew(() => Console.ReadKey()).Wait(TimeSpan.FromSeconds(5.0));
                        Console.Clear();
                        break;
                    }
                }
            }
            else
            {
                Console.WriteLine("The bus is full");
                Task.Factory.StartNew(() => Console.ReadKey()).Wait(TimeSpan.FromSeconds(5.0));
                Console.Clear();
            }

        }
        private static string AssignGender() //Denna metod bestämmer kön på passageraren
        {
            Random slump = new Random();
            int intGender = slump.Next(1, 3);
            string Gender = "Unknown";
            if (intGender == 1)
            {
                Gender = "Male";
            }
            if (intGender == 2)
            {
                Gender = "Female";
            }
            return Gender;
        }
        public void PrintBuss()
        {
            //Skriv ut alla värden ur vektorn. Alltså - skriv ut alla passagerare
            Console.Clear();
            for (int i = 0; i < passenger.Length; i++)
            {
                if (passenger[i] == null || passenger[i].Age==101)
                {
                    Console.WriteLine("Empty seat");
                }
                if (passenger[i] != null && passenger[i].Age!=101)
                {
                    Console.WriteLine("Age: " + passenger[i].Age + "" + passenger[i].Gender);
                }
            }
            Task.Factory.StartNew(() => Console.ReadKey()).Wait(TimeSpan.FromSeconds(5.0));
            Console.Clear();
        }
        public int CalcTotalAge() //Beräkna den totala åldern. 
        {
            Console.Clear();
            totalAge = 0;
            foreach (Passenger item in passenger)
            {
                if(item != null && item.Age != 101)
                {
                    totalAge += item.Age;
                }
            }
            Console.WriteLine("The total age is:");
            Console.WriteLine(totalAge);
            Task.Factory.StartNew(() => Console.ReadKey()).Wait(TimeSpan.FromSeconds(5.0));
            Console.Clear();
            return totalAge;
        }

        public double CalcAverageAge() //Beräkna den genomsnittliga åldern.
        {
            Console.Clear();
            CalcTotalAge(); //Börjar med att automatiskt köra metod för totalålder så att vi har något att räkna med.
            Console.WriteLine($"Divided by {passengerAmount} passengers equals:");
            double averageAge = totalAge / passengerAmount;
            Console.WriteLine(averageAge);
            Task.Factory.StartNew(() => Console.ReadKey()).Wait(TimeSpan.FromSeconds(5.0));
            Console.Clear();
            return averageAge;
        }

        public int MaxAge() //ta fram den passagerare med högst ålder.
        {
            Console.Clear();
            int maxAge = 0;
            foreach (Passenger item in passenger)
            {
                if (item != null && item.Age != 101)
                {
                    if (item.Age > maxAge)
                    {
                        maxAge = item.Age;
                    }
                }
            }
            Console.WriteLine(maxAge);
            Task.Factory.StartNew(() => Console.ReadKey()).Wait(TimeSpan.FromSeconds(5.0));
            Console.Clear();
            return maxAge;
        }

        public void FindAge() //Visa alla positioner med passagerare med en viss ålder
        {
            Console.Clear();
            int from = 0;
            int to = 0;
            Console.WriteLine("Find passengers within ages From:");
            from = ExceptionManager(from);
            Console.WriteLine("To:");
            to = ExceptionManager(to);

            foreach (Passenger item in passenger)
            {
                if (item != null && item.Age != 101)
                {
                    if (item.Age >= from && item.Age <= to)
                    {
                        Console.WriteLine("Age: " + item.Age + "" + item.Gender);
                    }
                }
            }
            Task.Factory.StartNew(() => Console.ReadKey()).Wait(TimeSpan.FromSeconds(5.0));
            Console.Clear();
        }

        public void SortBus() //Sortera bussen efter ålder.
        {
            Console.Clear();
            Passenger[] tempPassenger = new Passenger[realAndFakePsngr];
            for (int i = 0; i < tempPassenger.Length; i++)
            {
                tempPassenger[i] = passenger[i];
            }
            realAndFakePsngr = passengerAmount;
            //Skapar en ny array med endast riktiga passagerare och fejk "tomma platser" utan alla faktiskt tomma platser.
            Array.Sort(tempPassenger, new PassengerComparer());
            //Sorterar arrayen
            foreach (Passenger item in tempPassenger)
            {
                if(item.Age != 101)
                {
                    Console.WriteLine("Age: " + item.Age + "" + item.Gender);
                }
            } 
            //Skriver ut den sorterade listan
            Array.Clear(passenger);
            Array.Copy(tempPassenger, passenger, tempPassenger.Length);
            //Tömmer den gammla arrayen och fyller den med den nya.
            Task.Factory.StartNew(() => Console.ReadKey()).Wait(TimeSpan.FromSeconds(5.0));
            Console.Clear();
        }

        public void PrintGender() //Skriv ut vilka positioner som har manliga respektive kvinnliga passagerare.
        {
            Console.Clear();
            int temp = 0;
            do
            {
                Console.WriteLine("Print list for: \n" +
                    "1: All male passengers \n" +
                    "2: All Female passengers \n" +
                    "0: Cancel");
                temp = ExceptionManager(temp);
                switch (temp)
                {
                    case 1:
                        foreach (Passenger item in passenger)
                        {
                            if (item.Age != 101 && item != null)
                            {
                                if (item.Gender == "Male")
                                {
                                    Console.WriteLine("Age: " + item.Age + "" + item.Gender);
                                }
                            }
                        }
                        Task.Factory.StartNew(() => Console.ReadKey()).Wait(TimeSpan.FromSeconds(5.0));
                        Console.Clear();
                        break;
                    case 2:
                        foreach (Passenger item in passenger)
                        {
                            if (item.Age != 101 && item != null)
                            {
                                if (item.Gender == "Female")
                                {
                                    Console.WriteLine("Age: " + item.Age + "" + item.Gender);
                                }
                            }
                        }
                        Task.Factory.StartNew(() => Console.ReadKey()).Wait(TimeSpan.FromSeconds(5.0));
                        Console.Clear();
                        break;
                    case 0:
                        Console.Clear();
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Invalid input");
                        Task.Factory.StartNew(() => Console.ReadKey()).Wait(TimeSpan.FromSeconds(5.0));
                        Console.Clear();
                        break;
                }
            } while (temp != 0);
        }
        public void Poke() //Peta på passagerare
        {
            Console.Clear();
            Console.WriteLine("Are you sure you want to poke a passenger? (y/n) \n" +
                "(The awesome buss-simulator will not be held accountable for any following legal actions)");
            if (Console.ReadLine() == "y")
            {
                bool isReal = false;
                while (!isReal)
                {
                    if (passengerAmount == 0)
                    {
                        Console.WriteLine("There are no passengers to poke");
                        isReal= true;
                    }
                    if (passengerAmount > 0)
                    {
                        Random slump = new Random();
                        int pokedNr = slump.Next(0, realAndFakePsngr);
                        int outcome = slump.Next(1, 4);
                        Passenger pokedPassenger = passenger[pokedNr];
                        if (pokedPassenger != null)
                        {
                            if (pokedPassenger.Age != 101)
                            {
                                Console.WriteLine($"You poked a {pokedPassenger.Age} year old {pokedPassenger.Gender} passenger");
                                if (pokedPassenger.Gender == "Female" && pokedPassenger.Age < 18)
                                {
                                    switch (outcome)
                                    {
                                        case 1:
                                            Console.WriteLine("What´s wrong with you?");
                                            break;
                                        case 2:
                                            Console.WriteLine("Her father got angry");
                                            break;
                                        case 3:
                                            Console.WriteLine("Everyone got creeped out by your behavior and left");
                                            Array.Clear(passenger);
                                            passengerAmount = 0;
                                            realAndFakePsngr = 0;
                                            break;
                                    }
                                }
                                if (pokedPassenger.Gender == "Male" && pokedPassenger.Age < 18)
                                {
                                    switch (outcome)
                                    {
                                        case 1:
                                            Console.WriteLine("The child started crying");
                                            break;
                                        case 2:
                                            Console.WriteLine("His mother got annoyed and asked to speak with the manager");
                                            break;
                                        case 3:
                                            Console.WriteLine("Sir! you are poking a child. Stop that!");
                                            break;
                                    }
                                }
                                if (pokedPassenger.Gender == "Male" && pokedPassenger.Age >= 18 && pokedPassenger.Age <= 65)
                                {
                                    switch (outcome)
                                    {
                                        case 1:
                                            Console.WriteLine("He told you to stop");
                                            break;
                                        case 2:
                                            Console.WriteLine("That's no man! Thats a space station!");
                                            break;
                                        case 3:
                                            Console.WriteLine("He was a terrorist and now he's going to blow up the bus!");
                                            Thread.Sleep(1000);
                                            Console.Clear();
                                            Console.WriteLine("The bomb goes of in. \n 3");
                                            Thread.Sleep(1000);
                                            Console.Clear();
                                            Console.WriteLine("The bomb goes of in. \n 2");
                                            Thread.Sleep(1000);
                                            Console.Clear();
                                            Console.WriteLine("The bomb goes of in. \n 1");
                                            Thread.Sleep(1000);
                                            Environment.Exit(1);
                                            break;
                                    }
                                }
                                if (pokedPassenger.Gender == "Female" && pokedPassenger.Age >= 18 && pokedPassenger.Age <= 65)
                                {
                                    switch (outcome)
                                    {
                                        case 1:
                                            Console.WriteLine("Everyone suddenly stood up and rearanged themself in order of age \n" +
                                                "Strange...");
                                            Thread.Sleep(2000);
                                            SortBus();
                                            break;
                                        case 2:
                                            Console.WriteLine("She got annoyed and asked to speak with the manager");
                                            break;
                                        case 3:
                                            Console.WriteLine("She pepper sprayed you and ran off");
                                            passenger[pokedNr].Age = 101;
                                            passengerAmount--;
                                            break;
                                    }
                                }
                                if (pokedPassenger.Gender == "Male" && pokedPassenger.Age > 65 && pokedPassenger.Age < 101)
                                {
                                    switch (outcome)
                                    {
                                        case 1:
                                            Console.WriteLine("He´s asleap");
                                            break;
                                        case 2:
                                            Console.WriteLine("Why?");
                                            break;
                                        case 3:
                                            Console.WriteLine("He started calculating the average age of all the passengers");
                                            Thread.Sleep(1000);
                                            CalcAverageAge();
                                            break;
                                    }
                                }
                                if (pokedPassenger.Gender == "Female" && pokedPassenger.Age > 65 && pokedPassenger.Age < 101)
                                {
                                    switch (outcome)
                                    {
                                        case 1:
                                            Console.WriteLine("It´s super effective! \n" +
                                                "The lady is confused");
                                            break;
                                        case 2:
                                            Console.WriteLine("The lady gave you a cookie");
                                            break;
                                        case 3:
                                            Console.WriteLine("The old lady was startled, she had a heart attack and died");
                                            passenger[pokedNr].Age = 101;
                                            passengerAmount--;
                                            break;
                                    }
                                }
                                isReal = true;   
                            }
                        }
                    }
                }
            }
            Task.Factory.StartNew(() => Console.ReadKey()).Wait(TimeSpan.FromSeconds(5.0));
            Console.Clear();
        }

        public void PassengerDepart() //En passagerare kan stiga av
        {
            Console.Clear();
            bool isReal = false;
            Passenger departee;
            int departingPassenger;
            Random slump = new Random();
            while (isReal== false)
            {
                if (passengerAmount > 0)
                {
                    departingPassenger = slump.Next(0, realAndFakePsngr);
                    departee = passenger[departingPassenger];
                    if (departee != null)
                    {
                        if (departee.Age != 101)
                        {
                            Console.WriteLine($"A {departee.Age} year old {departee.Gender} departed");
                            passenger[departingPassenger].Age = 101;
                            passengerAmount--;
                            isReal = true;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("The bus is empty");
                    isReal = true;
                }
            }
            Console.WriteLine($"The bus now has {passengerAmount} passengers");            
            Task.Factory.StartNew(() => Console.ReadKey()).Wait(TimeSpan.FromSeconds(5.0));
            Console.Clear();
        }
    }

    class Passenger
    {
        public int Age
        {
            get;
            set;
        }
        public string Gender
        {
            get;
            set;
        }

        public Passenger(int age, string gender) //Konstruktor
        {
            Age = age;
            Gender = gender;
        }
    }
    class PassengerComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            return (new CaseInsensitiveComparer()).Compare(((Passenger)x).Age,((Passenger)y).Age);
        }
    }
    class Program
    {
        public static void Main(string[] args)
        {
            var myBus = new Bus();
            myBus.Run();
            Console.Write("Press any key to continue . . . ");
            Console.ReadKey(true);
        }
    }
}