using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
namespace MinesweeperClassLibrary
{
    public class PlayerStats : IComparable<PlayerStats>
    {
        public String Name { get; set; }
        public int Score { get; set; }
        public int Difficulty { get; set; }
        public int Time { get; set; }

    
        /// <summary>
        /// Empty constructor to allow instaniating PlayerStats object without
        /// and parameters. 
        /// </summary>
        public PlayerStats()
        {

        }

        public PlayerStats(string name, int difficulty, int time)
        {
            Name = name;
            Difficulty = difficulty;
            Time = time;
            Score = time / difficulty;
        }
        
        /// <summary>
        /// Override the ToString method to show player name, score, difficulty, and Time.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "Player: " + Name + " Score: " + Score + " Difficulty: " + Difficulty + " Time " + Time;
        }

        /// <summary>
        /// Override the compareTo method sorting by score and then by 
        /// difficulty in the event of scores being equal. 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(PlayerStats other)
        {
            if(this.Score == other.Score)
            {
                return other.Difficulty.CompareTo(this.Difficulty);
            }

            return other.Score.CompareTo(this.Score);
        }

        /// <summary>
        /// Overriding equals method standard formatting. 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return obj is PlayerStats stats &&
                   Name == stats.Name &&
                   Score == stats.Score &&
                   Difficulty == stats.Difficulty &&
                   Time == stats.Time;
        }

        /// <summary>
        /// Overriding get hash code standard formatting. 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            int hashCode = 1647062255;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + Score.GetHashCode();
            hashCode = hashCode * -1521134295 + Difficulty.GetHashCode();
            hashCode = hashCode * -1521134295 + Time.GetHashCode();
            return hashCode;
        }

     
    }
}
