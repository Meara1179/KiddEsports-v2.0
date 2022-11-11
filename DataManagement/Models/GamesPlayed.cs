using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManagement.Models
{
    public class GamesPlayed
    {
        public int GamesPlayedID { get; set; }
        public string GameName { get; set; }
        public string GameType { get; set; }

        public GamesPlayed()
        {

        }
        public GamesPlayed(int gamesPlayedID, string gameName)
        {
            GamesPlayedID = gamesPlayedID;
            GameName = gameName;
        }

        public GamesPlayed(int gamesPlayedID, string gameName, string gameType)
        {
            GamesPlayedID = gamesPlayedID;
            GameName = gameName;
            GameType = gameType;
        }
    }
}
