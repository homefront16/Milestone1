using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MinesweeperClassLibrary
{
    public class DataHandling
    {
        /// <summary>
        /// Costructor. Instantiates a DataHandling object. Holds methods
        /// for writing to a json file and reading from a json file. 
        /// </summary>
        public DataHandling()
        {
           
        }

        /// <summary>
        /// Method takes a list of PlayerStats as a parameter. It takes that list
        /// and serializes it to a json file. 
        /// </summary>
        /// <param name="theList"></param>
        public void WriteToJSON(List<PlayerStats> theList)
        {
            
            string fileName = @"C:\Users\Raymond\Source\Repos\homefront16\Milestone1\MinesweeperGUI2\Data\PlayerStats.json";

            // serialize JSON to a string and then write string to a file
            File.WriteAllText(fileName, JsonConvert.SerializeObject(theList));
     

            // serialize JSON directly to a file
            using (StreamWriter file = File.CreateText(fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, theList);
            }
        }


        /// <summary>
        /// Method takes a filename. The file should be a json file using absolute path. 
        /// The method will read the file and deserialize the file in to a list of 
        /// PlayerStats objects. 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public List<PlayerStats> ReadJSONFile(string fileName)
        {
            List<PlayerStats> myStatList;
            using (StreamReader r = new StreamReader(fileName))
            {
                string json = r.ReadToEnd();
                
                myStatList = JsonConvert.DeserializeObject<List<PlayerStats>>(json);
            }

            return myStatList;
        }
    }
}
