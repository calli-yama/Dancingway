using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Dalamud.Configuration;
using Dalamud.Plugin;
using Dalamud.Data;
using Lumina.Excel;
using Lumina.Excel.GeneratedSheets;


namespace Dancingway
{
    public class DanceList
    {
        private List<Dance> emoteList;
        private Random rnd;
        private List<uint> parsedSheet { get; init; }
        // took all the list of rowIDs from Godbert for the dances
        private List<uint> rowIDs = new List<uint> { 11, 101, 102, 103, 109, 118, 119, 120, 126, 129, 145, 149, 173, 174, 176, 185, 186, 192, 193, 197, 198, 212, 216, 217 };


        // Public external methods
        public void InitialiseDanceList()
        {
            emoteList = new List<Dance>();
            rnd = new Random();
        }

        public void BuildDanceList()
        {
            emoteList.Clear();
            foreach (var itemID in rowIDs)
            {
                string rawText = Service.DataManager.GetExcelSheet<Emote>()!.GetRow(itemID)!.TextCommand.ToString();
                // produces: "Lumina.Excel.GeneratedSheets.TextCommand#617" in test environment, 44 characters long

                // test
                //string emote = rawText.Remove(41);
                uint textCommandID = Convert.ToUInt32(Int32.Parse(rawText.Remove(0,41)));
                string emote = Service.DataManager.GetExcelSheet<TextCommand>()!.GetRow(textCommandID).Command.ToString();

                Dance newDance = new Dance(itemID, emote);
                emoteList.Add(newDance);
            }
        }

        public void TestBuild()
        {
            emoteList.Clear();

            AddDance("/hdance");
            AddDance("/dance");
            AddDance("/mdance");
            AddDance("/bdance");
            AddDance("/sdance");
            AddDance("/mogdance");
            AddDance("/sundance");
        }

        // Private internal methods
        private string Sanitise(string danceToClean)
        {
            // doesnt do anything, yet
            string sanitised = danceToClean;
            return sanitised;
        }

        // Public Getters
        public int getLength()
        {
            return emoteList.Count();
        }

        public string getRandomDance()
        {
            int index = rnd.Next(1, this.getLength());
            string chosenDance = getDanceCommand(index);

            return chosenDance;
        }

        public string getDanceCommand(int index)
        {
            string cleanEmote = Sanitise(emoteList[index].command);
            return cleanEmote;
        }

        // Public Setters
        public void AddDance(string newCommand)
        {
            string cleanDance = Sanitise(newCommand);
            string command = cleanDance; // use the name and command as the same thing for now
            Dance newDance = new Dance();
            newDance.command = cleanDance;
            newDance.name = cleanDance;
            newDance.enabled = true; // later on will add method to check if toon has this emote enabled

            emoteList.Add(newDance);
        }



    }
}
