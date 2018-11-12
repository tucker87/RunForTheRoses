using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using RunForTheRoses.Models;

namespace RunForTheRoses.Repository
{
    public static class RunForTheRosesRepo
    {
        public static List<RunForTheRosesResult> LoadRosesResults(string fileName) //returns list of horses
        {
            List<RunForTheRosesResult> derbyResults;
            var serializer = new JsonSerializer();
            using (var reader = new StreamReader(fileName))
            using (var jsonReader = new JsonTextReader(reader))
                derbyResults = serializer.Deserialize<List<RunForTheRosesResult>>(jsonReader);

            return derbyResults; //returns list of the horse races
        }

        public static void Print(List<RunForTheRosesResult> runForTheRoses)
        {
            foreach (var runForTheRosesResult in runForTheRoses)
                Console.WriteLine(runForTheRosesResult.Horse);

            Console.Write(Environment.NewLine); //provides space after list of horses
        }

        public static List<RunForTheRosesResult> Shuffle(List<RunForTheRosesResult> runForTheRoses)
        {
            var random = new Random();
            return runForTheRoses.OrderBy(r => random.Next()).ToList();
        }
    }
}
