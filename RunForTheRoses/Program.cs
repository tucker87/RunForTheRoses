﻿using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Linq;
using System.Text;


namespace RunForTheRoses
{
    class Program
    {
        public static void Main(string[] args)
        {
            //Per requirements, the persisted data must be able to be recalled when the app opens. I am showing that last data here in the beginning & Welcoming the user to the app
            //1
            Console.WriteLine("Welcome to the Repository for the 2016 Kentucky Derby's Run for the Roses.");
            Console.Write(Environment.NewLine);
            
            //When initially running the app, no user data is available. For the purpose of this app, I created an initial default response
            //When the program is stoped, closed and ran again, it will then return the stored data from the user's answers. 
            Console.WriteLine("The following displays the last bet:");
            string path = "./HorseBet.txt";
            if (!File.Exists("./HorseBet.txt"))
            {
                // Create a file to write to.
                string createText = "Erica bet on Nyquist to win." + Environment.NewLine;
                File.WriteAllText(path, createText);
      
            }

            string text = File.ReadAllText("./HorseBet.txt");
            Console.WriteLine("The last bet was " + text);
            Console.Write(Environment.NewLine);

            Console.Write("Press enter to see the list of 2016 Kentucky Derby horses."); 
            Console.ReadKey(true);
            Console.Write(Environment.NewLine);
            Console.Clear();    //This method will clear the welcome line. //I didn't want the welcome line to be visible the entire ime the app was open

            //2. The user is then able to see the list of the 2016 Kentucky Derby horses that ran in the race
            string derbyDirectory = Directory.GetCurrentDirectory();
            DirectoryInfo derbydirectory = new DirectoryInfo(derbyDirectory);

            var fileName = Path.Combine(derbydirectory.FullName, "2016RunForTheRosesResults.json"); //reads from the list

            var runForTheRoses = DeserializeRunForTheRosesResults(fileName); //returns a list

            //3. writes the running horse of the derby to the console from 2016RunForTheRoses.cvs file to console
            Random random = new Random();
            for (int i = 19; i >= 0; i--)
            {
                var j = random.Next(0, i);
                Console.WriteLine(runForTheRoses[j].Horse);
                var holder = runForTheRoses[j];
                runForTheRoses[j] = runForTheRoses[i];
                runForTheRoses[i] = holder;
            }

            Console.Write(Environment.NewLine); //provides space after list of horses

            Console.WriteLine("Please enter your name to place a bet.");
            var userName = Console.ReadLine();
            Console.Write(Environment.NewLine);
            Console.Write("What horse did you bet to win the 2016 Derby?");
            Console.Write(Environment.NewLine);

            //4. This code will validate the user's input on the horse they bet on and will display what place they finished and if their horse is not a valid horse it will return null
            //user entry returned from the Console.ReadLine method will be stored in the horseBet variable
            //If user selects a horse that is not on the list or presses enter and no value is captured. User needs to be prompted to pick a horse from the list



            bool nullAnswer = true;
            while (nullAnswer)
            {
                string horseBet = Console.ReadLine();
                Console.Write(Environment.NewLine);


                var horseBetAnswer = runForTheRoses.FirstOrDefault(r => string.Equals(r.Horse, horseBet, StringComparison.InvariantCultureIgnoreCase));


                if (horseBetAnswer == null)
                {

                    Console.WriteLine("That horse didn't run in the 2016 Run for the Roses. Please pick a horse from the list.");

                }
                else
                {
                    Console.Clear();
                    var UserBetString = userName + ", " + horseBetAnswer.Horse + " came in " + horseBetAnswer.Place + " place.";
                    Console.WriteLine(UserBetString);
                    nullAnswer = false;//breaks out of loop
                    Console.Write(Environment.NewLine);

                    //display users and their last bet

                    string HorseBetString = userName + " bet " + horseBetAnswer.Horse + " to win.";
                    File.WriteAllText("./HorseBet.txt", HorseBetString);
                   
                }

                
               
            }

                                   
            //5.
            Console.WriteLine("The program will now close. Thanks!");


            Console.Read();


        }



        //The ReadFile method reads the entire file to the end of it's file
        public static string ReadFile(string fileName)
        {
            using (var reader = new StreamReader(fileName))
            {
                return reader.ReadToEnd();
            }
        }


        //Makes List of RunFortheRosesResults
        public static List<RunForTheRosesResults> ReadRunForTheRosesResults(string fileName)
        {
            var runForTheRosesResults = new List<RunForTheRosesResults>();
            using (var reader = new StreamReader(fileName))
            {
                string line = "";
                reader.ReadLine(); //reads next line in file and returns. Reads to end of file and returns null
                while ((line = reader.ReadLine()) != null) //reads file to end of line if not null
                {
                    string[] values = line.Split(',');
                    var raceResults = new RunForTheRosesResults { Race = values[1], Place = values[2], Horse = values[3] };
                    new RunForTheRosesResults().Add(new RunForTheRosesResults()); //adds values to list
                }
            }
            return runForTheRosesResults; //converts a list of the RunForTheRosesResults

        }

        public static List<RunForTheRosesResults> DeserializeRunForTheRosesResults(string fileName)//returns list of horses
        {
            var derbyResults = new List<RunForTheRosesResults>();
            var serializer = new JsonSerializer();
            using (var reader = new StreamReader(fileName))
            using (var jsonReader = new JsonTextReader(reader))
            {
                derbyResults = serializer.Deserialize<List<RunForTheRosesResults>>(jsonReader);
            }

            return derbyResults; //returns list of the horse races
        }

        

    }
}




        

 
