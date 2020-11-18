using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MinesweeperClassLibrary
{
    public class DataHandling
    {
        //List<PlayerStats> PlayerStatsList;
        
        public DataHandling()
        {
           
        }

        public void WriteToJSON(List<PlayerStats> theList)
        {
            
            string fileName = @"D:\Users\Raymond\Documents\PlayerStats.json";

            // serialize JSON to a string and then write string to a file
            File.WriteAllText(fileName, JsonConvert.SerializeObject(theList));
     

            // serialize JSON directly to a file
            using (StreamWriter file = File.CreateText(fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, theList);
            }
        }

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
