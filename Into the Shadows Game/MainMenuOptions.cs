using Spectre.Console;

namespace Into_the_Shadows_Game
{
    internal class MainMenuOptions
    {
        public static bool PlayerIsReadingBio = false;
        public static string EnemyBioName = "";
        public static string SelectedDevice = "";

        static string selectedLocation = "";
        static readonly StoryMode getStoryLocations = new();

        public static void Options()
        {
            static void soundOptions()
            {
                // While loop - if the player has not muted the background music, and has not muted the sound effects.
                while (StoryMode.PlayBGM && StoryMode.PlaySFX)
                {
                    ExtFunctions.CreateMenuPrompt(new[] { "Mute Background Music.", "Mute Sound Effects.", "Go back to the main menu." }, "Sound Options - Choose any options below.", true, false);

                    switch (ExtFunctions.ChosenOption)
                    {
                        case "Mute Background Music.":
                            AnsiConsole.Clear();
                            StoryMode.PlayBGM = false;
                            break;

                        case "Mute Sound Effects.":
                            AnsiConsole.Clear();
                            StoryMode.PlaySFX = false;
                            break;
                        
                        case "Go back to the main menu.":
                            ExtFunctions.CleanConsole();
                            SoundSystem.CheckBGMPlayer();
                            MainGame.Main();
                            break;
                    }
                }

                // While loop - if the player has muted the background music, and has not muted the sound effects.
                while (StoryMode.PlayBGM == false && StoryMode.PlaySFX)
                {
                    ExtFunctions.CreateMenuPrompt(new[] { "Unmute Background Music.", "Mute Sound Effects.", "Go back to the main menu." }, "Sound Options - Choose any options below.", true, false);

                    switch (ExtFunctions.ChosenOption)
                    {
                        case "Unmute Background Music.":
                            AnsiConsole.Clear();
                            StoryMode.PlayBGM = true;
                            break;

                        case "Mute Sound Effects.":
                            AnsiConsole.Clear();
                            StoryMode.PlaySFX = false;
                            break;

                        case "Go back to the main menu.":
                            ExtFunctions.CleanConsole();
                            MainGame.Main();
                            break;
                    }
                }

                while (StoryMode.PlayBGM && StoryMode.PlaySFX == false)
                {
                    ExtFunctions.CreateMenuPrompt(new[] { "Mute Background Music.", "Unmute Sound Effects.", "Go back to the main menu." }, "Sound Options - Choose any options below.", true, false);

                    switch (ExtFunctions.ChosenOption)
                    {
                        case "Mute Background Music.":
                            AnsiConsole.Clear();
                            StoryMode.PlayBGM = false;
                            break;

                        case "Unmute Sound Effects.":
                            AnsiConsole.Clear();
                            StoryMode.PlaySFX = true;
                            break;

                        case "Go back to the main menu.":
                            ExtFunctions.CleanConsole();
                            MainGame.Main();
                            break;
                    }
                }

                while (StoryMode.PlayBGM == false && StoryMode.PlaySFX == false)
                {
                    ExtFunctions.CreateMenuPrompt(new[] { "Unmute Background Music.", "Unmute Sound Effects.", "Go back to the main menu." }, "Sound Options - Choose any options below.", true, false);

                    switch (ExtFunctions.ChosenOption)
                    {
                        case "Unmute Background Music.":
                            AnsiConsole.Clear();
                            StoryMode.PlayBGM = true;
                            break;

                        case "Unmute Sound Effects.":
                            AnsiConsole.Clear();
                            StoryMode.PlaySFX = true;
                            break;

                        case "Go back to the main menu.":
                            ExtFunctions.CleanConsole();
                            MainGame.Main();
                            break;
                    }
                }
            }

            //Console.Title = "Into the Shadows: Main Menu - Options.";

            //AnsiConsole.MarkupLine("[underline][gray]Main Menu - Options[/][/]\n");

            ExtFunctions.CreateMenuPrompt(new[] { "Sound Options.", "Load Saved Data.", "Go back to the previous menu." }, "Options - Choose any options below.", true, false);

            switch (ExtFunctions.ChosenOption)
            {
                case "Sound Options.":
                    ExtFunctions.CleanConsole();
                    soundOptions();
                    break;

                case "Load Saved Data.":
                    ExtFunctions.CleanConsole();
                    ExtFunctions.LoadGame();
                    MainGame.Main();
                    break;

                case "Go back to the previous menu.":
                    ExtFunctions.CleanConsole();
                    MainGame.Main();
                    break;
            }
        }

        public static void HowToPlay()
        {
            //Console.Title = "Into the Shadows: Main Menu - How To Play.";

            //AnsiConsole.MarkupLine("[underline][gray]Main Menu - How To Play[/][/]\n");

            AnsiConsole.MarkupLine("[orange1]\nMost of the time you will spend on the game, you will be using:\n\n[green]Up or Down Arrow Keys.[/]\n[green]Enter Key.[/]\n\nThere will be situations in the game, where you need to [green][underline]manually input[/][/] values.\n\nYou, as the player, can receive [cyan]buffs[/] and [red]debuffs[/] depending on the circumstances. Keep an eye on your [red]Fear Meter[/].\n\nYou will be engaging in battles with easy to difficult enemies during the gameplay. Ensure that, you will take advantage of the consumables that you have found.\n\nYou can level up in the game, which allows you to strengthen yourself using a [cyan]stat[/] point on your Max HP, DEF [[Defence]] Rating, or ATK [[Attack]] Rating.[/]");

            AnsiConsole.Write(new Markup("\n\n[orange1][red]----------------------------------------------------------------------------------------[/]\n\nNOTE: [link=https://www.microsoft.com/store/productid/9N0DX20HK701?ocid=pdpshare]Windows Terminal[/] is recommended to use for this game, to enable few decorative features.\n[italic]CTRL + Left Click to open the link.[/]\n\n[red]----------------------------------------------------------------------------------------[/]\n[/]").Centered());

            ExtFunctions.ContinuePrompt();

            ExtFunctions.CleanConsole();
            MainGame.Main();
        }

        public static void About()
        {
            //Console.Title = "Into the Shadows: Main Menu - About";

            //AnsiConsole.Markup("[underline][gray]Main Menu - About[/][/]\n\n");

            ExtFunctions.CreateHeading("About the game and the creator.", true);

            AnsiConsole.Write(new Markup("[orange1]The game has been created by myself - NomNom. This game has been created for the Programming Foundations module, for my university course.[/]\n\n").Centered());

            ExtFunctions.CreateHeading("Libraies used", true);
            AnsiConsole.Write(new Markup("[orange1]The game has been created using the following libraries:\n\n- [link=https://spectreconsole.net/]Spectre.Console[/]\n- [link=https://github.com/naudio/NAudio]NAudio[/]\n\n[/]").Centered());

            ExtFunctions.CreateHeading("Music used from", true);
            AnsiConsole.Write(new Markup("[orange1]The game uses music and sound effects that are royal-free. These have been downloaded from the following sites:\n\n[link=https://pixabay.com/]- Pixabay[/]\n- [link=https://www.fesliyanstudios.com/]Fesliyan Studios[/][/]").Centered());

            ExtFunctions.ContinuePrompt();

            ExtFunctions.CleanConsole();

            SoundSystem.CheckBGMPlayer();

            MainGame.Main();
        }

        public static void EnemyBios()
        {
            void ignoreBiosWarningCheck(ref bool ignoreWarning)
            {
                if (ignoreWarning == false)
                {
                    AnsiConsole.MarkupLine("You have chosen to ignore the warning. Would you like to disable this warning?");

                    ExtFunctions.CreateMenuPrompt(new[] { "Yes.", "No." }, "Choose any of the options listed below.", true, false);

                    switch (ExtFunctions.ChosenOption)
                    {
                        case "Yes.":
                            ignoreWarning = true;
                            break;

                        case "No.":
                            break;
                    }
                }
            }
            void checkPlayerReadAllBios(bool[] enemyBiosRead)
            {
                bool allBiosRead = enemyBiosRead.All(booleans => booleans);

                if (allBiosRead)
                {
                    AnsiConsole.MarkupLine("[green]You have read all the bios. Would you want to go back to the main menu?[/]");

                    ExtFunctions.CreateMenuPrompt(new[] { "Yes.", "No." }, "Choose any of the options listed below.", true, false);

                    switch (ExtFunctions.ChosenOption)
                    {
                        case "Yes.":
                            ExtFunctions.CleanConsole();
                            MainGame.Main();
                            break;

                        case "No.":
                            EnemyBios();
                            break;
                    }
                }
            }
            static void displayEnemyStats()
            {
                int enemyHP = 0;
                float enemyDefence = 0;
                float enemyAttack = 0;

                Grid enemyStatsGrid = new();
                Rule underline = new Rule().Centered().DoubleBorder();
                Rule underlineEnd = new Rule().Centered().AsciiBorder();

                switch (EnemyBioName)
                {
                    case "Engorged Beetles":
                        underline.Title = $"Engorged Beetles";
                        enemyHP = 65;
                        enemyDefence = 1.25f;
                        enemyAttack = 2.35f;
                        break;

                        case "Slitherman":
                        underline.Title = $"{BattleSystem.EncounteredEnemy[1]}";
                        enemyHP = 125;
                        enemyDefence = 1.75f;
                        enemyAttack = 4.25f;
                        break;

                        case "The Woodchopper":
                        underline.Title = $"{BattleSystem.EncounteredEnemy[2]}";
                        enemyHP = 200;
                        enemyDefence = 2.25f;
                        enemyAttack = 7.75f;
                        break;

                        case "???: Guard":
                        underline.Title = $"{BattleSystem.EncounteredEnemy[3]}";
                        enemyHP = 175;
                        enemyDefence = 3.75f;
                        enemyAttack = 4.25f;
                        break;
                }

                /*------------------------------------------------Text------------------------------------------------*/

                // Text allows us to apply style where it cannot be possible in any other way in a specific use.
                Text enemyHPHeading = new Text("HP [Health Points]", new Style(Color.Red)).Centered();
                Text enemyDefHeading = new Text("DEF Rating [Defence]", new Style(Color.Red)).Centered();
                Text enemyATKHeading = new Text("ATK Rating [Attack]", new Style(Color.Red)).Centered();


                Text enemyHPDisplay = new Text(enemyHP.ToString() + " HP", new Style()).Centered();
                Text enemyDefDisplay = new Text(enemyDefence.ToString(), new Style()).Centered();
                Text enemyATKDisplay = new Text(enemyAttack.ToString(), new Style()).Centered();

                /*-----------------------------------------------Padding----------------------------------------------*/

                Padder enemyHPHeadingPadder = new Padder(enemyHPHeading).PadLeft(9).PadRight(1).PadBottom(0).PadTop(0);
                Padder enemyDefHeadingPadder = new Padder(enemyDefHeading).PadLeft(9).PadRight(1).PadBottom(0).PadTop(0);
                Padder enemyATKHeadingPadder = new Padder(enemyATKHeading).PadLeft(9).PadRight(1).PadBottom(0).PadTop(0);

                Padder enemyHPDisplayPadder = new Padder(enemyHPDisplay).PadLeft(9).PadRight(1).PadBottom(0).PadTop(0);
                Padder enemyDefDisplayPadder = new Padder(enemyDefDisplay).PadLeft(9).PadRight(1).PadBottom(0).PadTop(0);
                Padder enemyATKDisplayPadder = new Padder(enemyATKDisplay).PadLeft(9).PadRight(1).PadBottom(0).PadTop(0);

                underline.Style = Style.Parse("red");
                underlineEnd.Style = Style.Parse("red");
                AnsiConsole.Write(underline);

                AnsiConsole.WriteLine();

                enemyStatsGrid.AddColumn();
                enemyStatsGrid.AddColumn();
                enemyStatsGrid.AddColumn();
                enemyStatsGrid.AddRow(enemyHPHeadingPadder, enemyDefHeadingPadder, enemyATKHeadingPadder);
                enemyStatsGrid.AddRow(enemyHPDisplayPadder, enemyDefDisplayPadder, enemyATKDisplayPadder);

                AnsiConsole.Write(enemyStatsGrid);
                AnsiConsole.WriteLine();
                AnsiConsole.Write(underlineEnd);
                AnsiConsole.WriteLine("\n\n");
            }

            bool[] enemyBiosRead = new bool[3];
            bool ignoreBiosReadWarning = false;

            checkPlayerReadAllBios(enemyBiosRead);

            //Console.Title = "Into the Shadows: Main Menu - Enemy Bios";

            AnsiConsole.Markup("[underline][gray]Main Menu - Enemy Bios[/][/]\n");

            ExtFunctions.CreateMenuPrompt(new[] { "Engorged Beetles.", "Slitherman.", "The Woodchopper.", "???: Guard.", "Return to Main Menu." }, "Choose any of the options listed below.", true, false);

            switch (ExtFunctions.ChosenOption)
            {
                case "Engorged Beetles.":
                    switch (enemyBiosRead[0])
                    {
                        case false:
                            PlayerIsReadingBio = true;
                            EnemyBioName = "Engorged Beetles";
                            ExtFunctions.CleanConsole();
                            enemyBiosBeetles(ref enemyBiosRead);
                            break;

                        case true:
                            if (ignoreBiosReadWarning == false)
                            {
                                AnsiConsole.MarkupLine("[orange1]You have read this enemy's bio already. Would you like to read the bio again?[/]");

                                ExtFunctions.CreateMenuPrompt(new[] { "Yes.", "No." }, "Choose any of the options listed below.", true, false);

                                switch (ExtFunctions.ChosenOption)
                                {
                                    case "Yes.":
                                       ignoreBiosWarningCheck(ref ignoreBiosReadWarning);
                                        PlayerIsReadingBio = true;
                                        EnemyBioName = "Engorged Beetles";
                                        enemyBiosBeetles(ref enemyBiosRead);
                                        break;

                                    case "No.":
                                        PlayerIsReadingBio = true;
                                        EnemyBioName = "Engorged Beetles";                                        ExtFunctions.CleanConsole();
                                        enemyBiosBeetles(ref enemyBiosRead);
                                        break;
                                }
                            }

                            else
                            {
                                PlayerIsReadingBio = true;
                                EnemyBioName = "Engorged Beetles";
                                ExtFunctions.CleanConsole();
                                enemyBiosBeetles(ref enemyBiosRead);
                            }
                            break;
                    }
                    break;

                case "Slitherman.":
                    switch (enemyBiosRead[1])
                    {
                        case false:
                            EnemyBioName = "Slitherman";
                            PlayerIsReadingBio = true;
                            ExtFunctions.CleanConsole();
                            enemyBiosSlitherman(ref enemyBiosRead);
                            break;

                        case true:
                            if (ignoreBiosReadWarning == false)
                            {
                                AnsiConsole.MarkupLine("You have read this enemy's bio already. Would you like to read the bio again?");

                                ExtFunctions.CreateMenuPrompt(new[] { "Yes.", "No." }, "Choose any of the options listed below.", true, false);

                                switch(ExtFunctions.ChosenOption)
                                {
                                        case "Yes.":
                                        ignoreBiosWarningCheck(ref ignoreBiosReadWarning);

                                        PlayerIsReadingBio = true;
                                        EnemyBioName = "Slitherman";
                                        ExtFunctions.CleanConsole();
                                        
                                        enemyBiosSlitherman(ref enemyBiosRead);
                                        break;

                                        case "No.":
                                        PlayerIsReadingBio = true;
                                        EnemyBioName = "Slitherman";
                                        ExtFunctions.CleanConsole();
                                        enemyBiosSlitherman(ref enemyBiosRead);
                                        break;
                                }
                            }

                            else
                            {
                                PlayerIsReadingBio = true;
                                EnemyBioName = "Slitherman";
                                ExtFunctions.CleanConsole();
                                enemyBiosSlitherman(ref enemyBiosRead);
                            }
                            break;
                    }
                    break;

                case "The Woodchopper.":

                    switch (enemyBiosRead[2])
                    {
                        case false:
                            PlayerIsReadingBio = true;
                            EnemyBioName = "The Woodchopper";
                            ExtFunctions.CleanConsole();
                            enemyBiosTheWoodchopper(ref enemyBiosRead);
                            break;

                        case true:
                            if (ignoreBiosReadWarning == false)
                            {
                                AnsiConsole.MarkupLine("You have read this enemy's bio already. Would you like to read the bio again?");

                                ExtFunctions.CreateMenuPrompt(new[] { "Yes.", "No." }, "Choose any of the options listed below.", true, false);

                                switch (ExtFunctions.ChosenOption)
                                {
                                    case "Yes.":
                                        ignoreBiosWarningCheck(ref ignoreBiosReadWarning);
                                        PlayerIsReadingBio = true;
                                        EnemyBioName = "The Woodchopper";
                                        ExtFunctions.CleanConsole();
                                        enemyBiosTheWoodchopper(ref enemyBiosRead);
                                        break;

                                    case "No.":
                                        PlayerIsReadingBio = true;
                                        EnemyBioName = "The Woodchopper";
                                        ExtFunctions.CleanConsole();
                                        enemyBiosTheWoodchopper(ref enemyBiosRead);
                                        break;
                                }
                            }

                            else
                            {
                                PlayerIsReadingBio = true;
                                EnemyBioName = "The Woodchopper";
                                ExtFunctions.CleanConsole();
                                enemyBiosTheWoodchopper(ref enemyBiosRead);
                            }
                            break;
                    }
                    break;

                case "???: Guard.":
                    switch (enemyBiosRead[3])
                    {
                        case false:
                            PlayerIsReadingBio = true;
                            EnemyBioName = "???: Guard";
                            ExtFunctions.CleanConsole();
                            enemyBiosUnknownEnemy(ref enemyBiosRead);
                            break;

                        case true:
                            if (ignoreBiosReadWarning == false)
                            {
                                AnsiConsole.MarkupLine("You have read this enemy's bio already. Would you like to read the bio again?");

                                ExtFunctions.CreateMenuPrompt(new[] { "Yes.", "No." }, "Choose any of the options listed below.", true, false);

                                switch (ExtFunctions.ChosenOption)
                                {
                                    case "Yes.":
                                        ignoreBiosWarningCheck(ref ignoreBiosReadWarning);
                                        PlayerIsReadingBio = true;
                                        EnemyBioName = "???: Guard";
                                        ExtFunctions.CleanConsole();
                                        enemyBiosUnknownEnemy(ref enemyBiosRead);
                                        break;

                                    case "No.":
                                        PlayerIsReadingBio = true;
                                        EnemyBioName = "???: Guard";
                                        ExtFunctions.CleanConsole();
                                        enemyBiosUnknownEnemy(ref enemyBiosRead);
                                        break;
                                }
                            }

                            else
                            {
                                PlayerIsReadingBio = true;
                                EnemyBioName = "???: Guard";
                                ExtFunctions.CleanConsole();
                                enemyBiosUnknownEnemy(ref enemyBiosRead);
                            }
                            break;
                    }
                    break;
            }

            static void enemyBiosBeetles(ref bool[] enemyBiosRead)
            {
                enemyBiosRead[0] = true;

                //Console.Title = "Into the Shadows: Main Menu - Enemy Bios [Engorged Beetles]";

                AnsiConsole.Write(new Markup("[underline][gray]Main Menu - Enemy Bios [[Engorged Beetles]][/][/]\n").Centered());
                AnsiConsole.Write(new Markup("[orange1]Engorged Beetles are unusual creature that will attack you if provoked. The Origins of these beetles are unknown, however, they appear from within the darkness.\n----------------------------------------------------------------------------------------[/]").Centered());
                AnsiConsole.Write(new Markup("[orange1][underline]Type of Attacks:[/]\n\n- Bite; Bites you, deals mild damage.\n- Flies and Hisses; Scares you, trying to increase your fear meter.\n- Sprinkle Some Poop; Flies around you and drops some dropplings that you insinuate that it may be some chocolate.\n\n[italic][gray]Press any key to return to the previous menu.[/][/][/]\n").Centered());

                displayEnemyStats();

                ExtFunctions.ContinuePrompt();

                EnemyBios();
            }

            static void enemyBiosSlitherman(ref bool[] enemyBiosRead)
            {
                enemyBiosRead[1] = true;

                //Console.Title = "Into the Shadows: Main Menu - Enemy Bios [Slitherman]";

                AnsiConsole.Write(new Markup("[underline][gray]Main Menu - Enemy Bios [[Slitherman]][/][/]\n").Centered());
                AnsiConsole.Write(new Markup("[orange1]A monster, with a human body. Both of its legs are attached to each other, looks like they were sewn together. It slithers using its legs and can possibly lunge at you. It has large claws on its hands, that can be fatal for you.\n----------------------------------------------------------------------------------------[/]").Centered());
                AnsiConsole.Write(new Markup("[orange1][underline]Type of Attacks:[/]\n\n- Scratch; Uses its long metal claws to attack you. Can make you to bleed.\n- Lunge; Lunges at you viciously, causing you to get knocked down. Deals a lot of damage.\n- Wail; Wails out loud, restoring few HP and frightens you. Can make you to be dazed for a turn.\n\n[italic][gray]Press any key to return to the previous menu.[/][/][/]\n").Centered());

                displayEnemyStats();

                ExtFunctions.ContinuePrompt();

                EnemyBios();
            }

            static void enemyBiosTheWoodchopper(ref bool[] enemyBiosRead)
            {
                enemyBiosRead[2] = true;

                //Console.Title = "Into the Shadows: Main Menu - Enemy Bios [The Woodchopper]";

                AnsiConsole.Write(new Markup("[underline][gray]Main Menu - Enemy Bios [[The Woodchopper]][/][/]\n").Centered());
                AnsiConsole.Write(new Markup("[orange1]Another monster, walks and limps, has a human body without a head. The head is replaced with an axe's head but larger in width, and looks like skin has been stretched out to cover it.\n----------------------------------------------------------------------------------------[/]").Centered());
                AnsiConsole.Write(new Markup("[orange][underline]Type of Attacks:[/]\n\n- Single Slice; Swipes its sharp saw head at you. Can make you bleed.\n- Double Slice; Swipes its sharp saw head at you, twice. Deals a lot of damage.\n- Grapple; Attempts to grab you to perform its instant kill. Slightly damages you.\n- Chop Chop; Attempts to power up its attack to instant kill you, by chopping your torso in half. Will damage your significantly.\n\n[italic][gray]Press any key to return to the previous menu.[/][/][/]\n").Centered());

                displayEnemyStats();

                ExtFunctions.ContinuePrompt();

                EnemyBios();
            }

            static void enemyBiosUnknownEnemy(ref bool[] enemyBiosRead)
            {
                enemyBiosRead[3] = true;

                //Console.Title = "Into the Shadows: Main Menu - Enemy Bios [???: Guard]";

                AnsiConsole.Write(new Markup("[underline][gray]Main Menu - Enemy Bios [[???: Guard]][/][/]\n").Centered());
                AnsiConsole.Write(new Markup("[gray]No information can be provided.\n----------------------------------------------------------------------------------------[/]").Centered());
                AnsiConsole.Write(new Markup("[bold][underline]Type of Attacks:[/][/]\n\n- Melee; ???\n- Shoot; ???\n\n[italic][gray]Press any key to return to the previous menu.[/][/]\n").Centered());

                displayEnemyStats();

                ExtFunctions.ContinuePrompt();

                EnemyBios();
            }
        }

        public static void DebugMenu()
        {
            string[] listOfLocations = { "Basement 1", "Laundry Room 1", "Basement 2", "Guard Post", "Laundry Room 2", "Canteen", "Visitor Centre", "Entrance 1", "Laundry Room 3", "Corridor 1", "Infirmary", "Cell Block A", "Cell Block B", "Shower Room", "Solitary 1", "Execution Room", "Torture Room", "Corridor 2", "Rec Room", "Solitary 2", "Entrance 2", "Weapons Room" };

            static void jumpLocations()
            {
                switch (selectedLocation)
                {
                    case "Basement 1":
                        ExtFunctions.CleanConsole();
                        StoryMode.Story_Basement();
                        break;

                    case "Laundry Room 1":
                        ExtFunctions.CleanConsole();
                        getStoryLocations.Story_LaundryRoom1();
                        break;
                    
                        case "Basement 2":
                        ExtFunctions.CleanConsole();
                        getStoryLocations.Story_Basement2();
                        break;

                        case "Guard Post":
                        ExtFunctions.CleanConsole();
                        getStoryLocations.Story_GuardPost1();
                        break;

                        case "Laundry Room 2":
                        ExtFunctions.CleanConsole();
                        getStoryLocations.Story_LaundryRoom2();
                        break;

                        case "Canteen":
                        ExtFunctions.CleanConsole();
                        getStoryLocations.Story_Canteen();
                        break;

                    case "Visitor Centre":
                        ExtFunctions.CleanConsole();
                        getStoryLocations.Story_VisitorCentre();
                        break;

                        case "Entrance 1":
                        ExtFunctions.CleanConsole();
                        getStoryLocations.Story_Entrance1();
                        break;

                        case "Laundry Room 3":
                        ExtFunctions.CleanConsole();
                        getStoryLocations.Story_LaundryRoom3();
                        break;

                        case "Corridor 1":
                        ExtFunctions.CleanConsole();
                        getStoryLocations.Story_Corridor1();
                        break;

                        case "Infirmary":
                        ExtFunctions.CleanConsole();
                        getStoryLocations.Story_Infirmary();
                        break;

                        case "Cell Block A":
                        ExtFunctions.CleanConsole();
                        getStoryLocations.Story_CellBlockA();
                        break;

                        case "Cell Block B":
                        ExtFunctions.CleanConsole();
                        getStoryLocations.Story_CellBlockB();
                        break;

                        case "Shower Room":
                        ExtFunctions.CleanConsole();
                        getStoryLocations.Story_Showers();
                        break;

                        case "Solitary 1":
                        ExtFunctions.CleanConsole();
                        getStoryLocations.Story_Solitary1();
                        break;

                        case "Execution Room":
                            ExtFunctions.CleanConsole();
                        getStoryLocations.Story_ExecutionRoom();
                        break;

                        case "Torture Room":
                        ExtFunctions.CleanConsole();
                        getStoryLocations.Story_TortureRoom();
                        break;

                        case "Corridor 2":
                        ExtFunctions.CleanConsole();
                        getStoryLocations.Story_Corridor2();
                        break;

                        case "Rec Room":
                        ExtFunctions.CleanConsole();
                        getStoryLocations.Story_RecRoom();
                            break;

                        case "Solitary 2":
                        ExtFunctions.CleanConsole();
                        getStoryLocations.Story_Solitary2();
                        break;

                        case "Entrance 2":
                        ExtFunctions.CleanConsole();
                        getStoryLocations.Story_Entrance2();
                        break;

                        case "Weapons Room":
                        ExtFunctions.CleanConsole();
                        getStoryLocations.Story_WeaponsRoom();
                        break;

                }
            }

            // It is best to see if there is a saved data that can be loaded in, else the game could freak out or act weird, if the variables aren't updated to the correct values once the player gets far into the game - if that makes sense.
            ExtFunctions.CheckForSaveData();

            AnsiConsole.MarkupLine("[sandybrown]Here are the locations you can jump across to.\nThis will skip the introduction and disclaimer information.[/]\n");

            for (int i = 0; i < listOfLocations.Length; i++)
            {
                AnsiConsole.MarkupLine($"[sandybrown]{listOfLocations[i]}.[/]");
            }

            ExtFunctions.CreateMenuPrompt(listOfLocations, "Choose any of the locations listed below.", true, false);

            switch (ExtFunctions.ChosenOption)
            {
                case "Basement 1":
                    selectedLocation = "Basement 1";
                    break;

                case "Laundry Room 1":
                    selectedLocation = "Laundry Room 1";
                    break;

                case "Basement 2":
                    selectedLocation = "Basement 2";
                    break;

                case "Guard Post":
                    selectedLocation = "Guard Post";
                    break;

                case "Laundry Room 2":
                    selectedLocation = "Laundry Room 2";
                    break;

                case "Canteen":
                    selectedLocation = "Canteen";
                    break;

                case "Visitor Center":
                    selectedLocation = "Visitor Center";
                    break;

                case "Entrance 1":
                    selectedLocation = "Entrance 1";
                    break;

                case "Laundry Room 3":
                    selectedLocation = "Laundry Room 3";
                    break;

                case "Corridor 1":
                    selectedLocation = "Corridor 1";
                    break;

                case "Infirmary":
                    selectedLocation = "Infirmary";
                    break;

                case "Cell Block A":
                    selectedLocation = "Cell Block A";
                    break;

                case "Cell Block B":
                    selectedLocation = "Cell Block B";
                    break;

                case "Shower Room":
                    selectedLocation = "Shower Room";
                    break;

                case "Solitary 1":
                    selectedLocation = "Solitary 1";
                    break;

                case "Execution Room":
                    selectedLocation = "Execution Room";
                    break;

                case "Torture Room":
                    selectedLocation = "Torture Room";
                    break;

                case "Corridor 2":
                    selectedLocation = "Corridor 2";
                    break;

                case "Rec Room":
                    selectedLocation = "Rec Room";
                    break;

                case "Solitary 2":
                    selectedLocation = "Solitary 2";
                    break;

                case "Entrance 2":
                    selectedLocation = "Entrance 2";
                    break;

                case "Weapons Room":
                    selectedLocation = "Weapons Room";
                    break;
            }

            jumpLocations();
        }
    }
}
