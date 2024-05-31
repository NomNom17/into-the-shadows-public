using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Into_the_Shadows_Game
{
    internal class PlayerJournal
    {
        StringBuilder JournalEntries = new StringBuilder(string.Empty);

        public void DisplayJournal()
        {
            // Change variables to Static to access them in another class.
            
            if (StoryMode.LaundryRoomIllegibleWallTextRead == true)
            {
                JournalEntries.AppendLine("There is almost illegible writing on the wall, that says:\nThere is no escape once you have been trapped in Hell. They are everywhere; killing everyone who has committed sins, who is negative, who is depressed, who is classed as filth by having medical conditions. Who are They? And where did They come from...\nSeems like I am not the only one inside this place. Better be careful and keep an eye out."); // Quote - The Laundry Room. //
                JournalEntries.AppendLine("What is going on here? So much death everywhere..."); // Player quote - The Guard Post. //
            }

            //Console.Write(JournalEntries.ToString());

            Console.ForegroundColor = Color.SandyBrown;
            AnsiConsole.Markup(JournalEntries.ToString());

            ExtFunctions.ContinuePrompt();
            ExtFunctions.CleanConsole();
        }
    }
}
