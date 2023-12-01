class Storprojekt {
    static List<Dog> dogs = new List<Dog>(); // List to store Dog objects
    static double balance = 0; // Variable to store the balance

    static void Main() {
        dogs.Add(new Dog("Romen", 9, 67, "Greyhound", 73, "male", 34)); // Adding a new Dog object to the list
        dogs.Add(new Dog("Max", 8, 69, "Saluki", 72, "male", 23)); // Adding a new Dog object to the list
        dogs.Add(new Dog("Firare", 7, 71, "Doberman Pinscher", 72, "Male", 25)); // Adding a new Dog object to the list
        dogs.Add(new Dog("X", 11, 69, "Afghan Hound", 72, "Male", 29)); // Adding a new Dog object to the list
        Console.WriteLine("Welcome to The home of the fastest dogs in the world!");
        Console.WriteLine("Click enter to Enter the house!");
        Console.ReadLine();
        Console.Clear();
        bool validChoice = false;
        int choice = 0;
        while (!validChoice) {
            Console.WriteLine("OBS! Make the program fullscreen for an optimal trial");
            Console.WriteLine("Below, you'll find a list of options that you can choose from. Simply type the corresponding number of the option you desire to proceed!");
            Console.WriteLine("1- Start racing");
            Console.WriteLine("2- Simulate a race");
            Console.WriteLine("3- Charge your balance");
            Console.WriteLine("4- Add your own dog");
            Console.WriteLine("5- Get our dogs' information");
            Console.WriteLine("6- Information about the company");

            Console.Write("Enter your choice (1-6): ");
            int casess = 0;
            try {
                string cases = Console.ReadLine();
                casess = Convert.ToInt32(cases);
            }
            catch (Exception e) {
                Console.Clear();
                Console.WriteLine("Invalid choice. Please enter a number between 1 and 6.");
                Console.WriteLine("Press any key to go back to the main minue");
                Console.ReadLine();
                Console.Clear();
            }
            switch (casess) {
                case 1:
                    Console.Clear();
                    Console.WriteLine("Please note that Roman is currently the fastest dog in our races. If you place a bet on Roman and he wins, you will receive a payout of 1.2 times your original bet. For Max, the payout will be 1.2 times your bet amount. As for all the other dogs, the payout will be twice your bet amount.");
                    Console.WriteLine("You've chosen to bet on a dog. Here are the available dogs: ");
                    DisplayAvailableDogs(); // Displaying the available dogs
                    Console.Write("Enter the number of the dog you want to bet on: ");
                    string inputDog = Console.ReadLine();
                    int selectedDogNumber;
                    Console.WriteLine("Your current balance is: " + balance);
                    try {
                        selectedDogNumber = Convert.ToInt32(inputDog);

                        if (selectedDogNumber < 1 || selectedDogNumber > dogs.Count) {
                            Console.WriteLine("Invalid dog number. Please enter a number between 1 and " + dogs.Count + ".");
                            Console.WriteLine("Press any key to go back to the main menu");
                            Console.ReadLine();
                            Console.Clear();
                            break;
                        }
                    }
                    catch (FormatException e) {
                        Console.WriteLine("Invalid input. Please enter a valid number.");
                        Console.WriteLine("Press any key to go back to the main menu");
                        Console.ReadLine();
                        Console.Clear();
                        Console.WriteLine(e.Message);
                        break;
                    }

                    Console.Write("Enter the amount of money you want to bet (minimum $100): ");
                    string inputMoney = Console.ReadLine();
                    int betAmount;

                    try {
                        betAmount = Convert.ToInt32(inputMoney);

                        // Validate if the bet amount is greater than or equal to the minimum bet
                        if (betAmount < 100) {
                            Console.WriteLine("Minimum bet amount is $100. Please enter a valid amount.");
                            Console.WriteLine("Press any key to go back to the main menu");
                            Console.ReadLine();
                            Console.Clear();
                            break;
                        }

                        if (betAmount > balance) {
                            Console.WriteLine("Insufficient balance. Please enter a valid amount within your balance.");
                            Console.WriteLine("Press any key to go back to the main menu");
                            Console.ReadLine();
                            Console.Clear();
                            break;
                        }
                    }
                    catch (FormatException) {
                        Console.WriteLine("Invalid input. Please enter a valid number.");
                        Console.WriteLine("Press any key to go back to the main menu");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    }

                    int winningDogNumber = RaceAllDogs(); // Running the race and getting the winning dog number
                    Console.WriteLine("The race is over! The winning dog is: " + dogs[winningDogNumber].Name);

                    double multiplier = 1.0;
                    if (winningDogNumber == 0) {
                        multiplier = 1.2;
                    }
                    else if (winningDogNumber == 1) {
                        multiplier = 1.5;
                    }

                    if (selectedDogNumber - 1 == winningDogNumber) {
                        double winnings = betAmount * multiplier;
                        balance += winnings;
                        Console.WriteLine($"Congratulations! You won {winnings}$!");
                    }
                    if (selectedDogNumber - 1 != winningDogNumber) {
                        Console.WriteLine("Unfortunately, you lost your bet. Better luck next time!");
                        balance -= betAmount;
                    }

                    Console.WriteLine("Press any key to go back to the main menu");
                    Console.ReadLine();
                    Console.Clear();
                    break;
                case 2:
                    Console.Write("Here you can simulate how races will play out");
                    Console.ReadLine();
                    DisplayAvailableDogs(); // Displaying the available dogs
                    Console.Write("Chose One of the avilbale dogs here (OBS! wirte the dogs number): ");
                    Console.ReadLine();

                    winningDogNumber = RaceAllDogs(); // Running the race and getting the winning dog number

                    Console.WriteLine("The race is over! The winning dog is: " + dogs[winningDogNumber].Name);

                    Console.WriteLine("Press any key to go back to the main menu");
                    Console.ReadLine();
                    Console.Clear();
                    break;
                case 5:
                    DisplayAllDogsInfo(dogs); // Displaying information about all the dogs
                    Console.WriteLine("Press any key to go back to the main menu");
                    Console.ReadLine();
                    Console.Clear();
                    break;
                case 4:
                    AddDog(); // Adding a new dog to the list
                    break;
                case 6:
                    Console.Clear();
                    Console.WriteLine("In our company, you have the opportunity to participate in dog races by placing bets. If your bet is on the winning dog, you earn money; however, if your bet does not succeed, you will lose the wagered amount. It's as straightforward as that");
                    Console.WriteLine("Press any key to go back to the main menu");
                    Console.ReadLine();
                    Console.Clear();
                    break;
                case 3:
                    Console.Clear();
                    ChargeBalance(); // Charging the balance
                    Console.WriteLine("Press any key to go back to the main minue");
                    Console.ReadLine();
                    Console.Clear();
                    break;
                default:
                    Console.Clear();
                    break;
            }
        }
    }

    static int RaceAllDogs() {
        int dogIndex = 0;
        Dog winningDog = dogs[0];
        dogs[0].GenerateRaceSpeed(); // Generating race speed for the first dog
        for (int i = 1; i < dogs.Count; i++) {
            dogs[i].GenerateRaceSpeed(); // Generating race speed for the rest of the dogs
            if (dogs[i].RaceSpeed > winningDog.RaceSpeed) {
                winningDog = dogs[i];
                dogIndex = i;
            }
        }

        return dogIndex; // Returning the index of the winning dog
    }

    static void DisplayAvailableDogs() {
        for (int i = 0; i < dogs.Count; i++) {
            Console.WriteLine((i + 1) + " " + dogs[i].Name); // Displaying the number and name of each dog
        }
    }

    static void DisplayAllDogsInfo(List<Dog> dogList) {
        for (int i = 0; i < dogList.Count; i++) {
            dogList[i].PrintInfo(); // Printing information about each dog
        }
    }
    static void ChargeBalance() {
        Console.Write("How much do you want to charge? ");
        string amount = Console.ReadLine();

        int chargeAmount;
        try {
            chargeAmount = Convert.ToInt32(amount);
        }
        catch (FormatException) {
            Console.WriteLine("Invalid amount. Enter a number.");
            return;
        }

        balance += chargeAmount; // Updating the balance
        Console.WriteLine($"Your balance is now {balance}$");
    }

    static void AddDog() {
        Console.Write("Enter your dog's name: ");
        string name = Console.ReadLine();

        Console.Write("Enter your dog's age: ");
        int age = Convert.ToInt32(Console.ReadLine());
        if (age > 20 || age < 4) {
            Console.WriteLine("Invalid age. Age should be between 4 and 20");
            Console.WriteLine("Press any key to go back to the main minue");
            Console.ReadLine();
            Console.Clear();
            return;

        }

        Console.Write("Enter your dog's height in CM: ");
        int height = Convert.ToInt32(Console.ReadLine());
        if (height > 200 || height < 50) {
            Console.WriteLine("Invalid height. height should be abov 50 and below 200 CM");
            Console.WriteLine("Press any key to go back to the main minue");
            Console.ReadLine();
            Console.Clear();
            return;

        }

        Console.Write("Enter your dog's breed: ");
        string breed = Console.ReadLine();

        Console.Write("Enter your dog's max speed: ");
        int maxSpeed = Convert.ToInt32(Console.ReadLine());
        if (maxSpeed > 75 || maxSpeed < 50) {
            Console.WriteLine("Maximum speed cannot be belw 80 and abov 50 km/h");
            Console.WriteLine("Press any key to go back to the main minue");
            Console.ReadLine();
            Console.Clear();
            return;

        }
        Console.Write("Enter your dog's gender: ");
        string gender = Console.ReadLine();

        Console.Write("Enter your dog's weight: ");
        int weight = Convert.ToInt32(Console.ReadLine());
        if (weight > 50 || weight < 10) {
            Console.WriteLine("The weight should be abov 10 and below 30 kg.");
            Console.WriteLine("Press any key to go back to the main minue");
            Console.ReadLine();
            Console.Clear();
            return;

        }

        Dog newDog = new Dog(name, age, height, breed, maxSpeed, gender, weight);
        dogs.Add(newDog); // Adding the new dog to the list

        Console.WriteLine("Your dog has been added to the list!");
        Console.WriteLine("Press any key to go back to the main minue");
        Console.ReadLine();
    }

    class Dog {
        // Properties of the Dog class
        public string Name { get; set; }
        public int Age { get; set; }
        public int Height { get; set; }
        public string Breed { get; set; }
        public int MaxSpeed { get; set; }
        public int RaceSpeed { get; set; }
        public string Gender { get; set; }
        public int Weight { get; set; }

        // Constructor for the Dog class
        public Dog(string name, int age, int height, string breed, int maxSpeed, string gender, int weight) {
            Name = name;
            Age = age;
            Height = height;
            Breed = breed;
            MaxSpeed = maxSpeed;
            Gender = gender;
            Weight = weight;
        }

        // Method to print information about the dog
        public void PrintInfo() {
            Console.WriteLine("Name: " + Name + " Age: " + Age + " Height: " + Height + " Breed: " + Breed + " Max Speed: " + MaxSpeed + " Gender: " + Gender + " Weight: " + Weight);
        }

        // Method to generate race speed for the dog
        public void GenerateRaceSpeed() {
            Random rand = new Random();
            int i = rand.Next(1, 6);
            RaceSpeed = MaxSpeed - i;
        }
    }
}