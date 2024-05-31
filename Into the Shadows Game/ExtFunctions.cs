using Spectre.Console;
using System.Diagnostics;
using System.Text;

namespace Into_the_Shadows_Game
{

    //                                          EXTERNAL FUNCTIONS                                          //
    //      Written all methods/functions that will be called several times throughout the MainGame.cs. This is to prevent having to rewrite parts of code endlessly and repeatedly.                         //

    internal class ExtFunctions
    {
        public static string ChosenOption = "";
        public static Rule HeadingTextUnderline = new Rule().LeftJustified().DoubleBorder();
        public static string HeadingTitle = "";

        public static string StopwatchHours;
        public static string StopwatchMinutes;
        public static string StopwatchSeconds;

        ////////////////////////////////////////////////////
        ///              CONSOLE FUNCTIONS               ///
        ////////////////////////////////////////////////////



        //      CLEANS THE CONSOLE OF TEXT AND OTHER THINGS.      //
        public static void CleanConsole()
        {
            if (StoryMode.PlayerIsMoving)
            {
                AnsiConsole.Status().Start("\n[white]Moving...[/]", loadingStatus =>
                {
                    Thread.Sleep(1000);

                    loadingStatus.Status($"[white]You are now moving from {StoryMode.CurrentLocation} to {StoryMode.NewLocation}...[/]");
                    loadingStatus.Spinner(Spinner.Known.Star2);
                    loadingStatus.SpinnerStyle(Style.Parse("green"));

                    Random timeToMove = new();
                    int timeToMoveInt = timeToMove.Next(1200, 3250);

                    Thread.Sleep(timeToMoveInt);

                    loadingStatus.Status("[green]Done![/]");
                    Thread.Sleep(350);

                    AnsiConsole.Clear();
                    Console.Clear();
                    StoryMode.PlayerIsMoving = false;
                });
            }

            else if (StoryMode.PlayerIsQuitting)
            {
                AnsiConsole.Status().Start("\n[white]Exiting the game...[/]", loadingStatus =>
                {
                    Thread.Sleep(1000);

                    loadingStatus.Status("[white]Please wait while the game is closing...[/]");
                    loadingStatus.Spinner(Spinner.Known.Star2);
                    loadingStatus.SpinnerStyle(Style.Parse("green"));
                    Thread.Sleep(2000);

                    loadingStatus.Status("[green]Done![/]");
                    Thread.Sleep(350);

                    AnsiConsole.Clear();
                    Console.Clear();
                    StoryMode.PlayerIsQuitting = false;
                });
            }

            else if (MainMenuOptions.PlayerIsReadingBio)
            {
                AnsiConsole.Status().Start("\n[white]Loading the bio...[/]", loadingStatus =>
                {
                    Thread.Sleep(1000);

                    loadingStatus.Status($"[white]Please wait while the {MainMenuOptions.EnemyBioName}'s bio is loading...[/]");
                    loadingStatus.Spinner(Spinner.Known.Star2);
                    loadingStatus.SpinnerStyle(Style.Parse("green"));
                    Thread.Sleep(2000);

                    loadingStatus.Status("[green]Done![/]");
                    Thread.Sleep(500);

                    AnsiConsole.Clear();
                    Console.Clear();
                });
            }

            else
            {
                AnsiConsole.Status().Start("\n[white]Cleaning the console...[/]", loadingStatus =>
                {
                    Thread.Sleep(1000);

                    loadingStatus.Status("[white]Please wait while the console is being cleaned...[/]");
                    loadingStatus.Spinner(Spinner.Known.Star2);
                    loadingStatus.SpinnerStyle(Style.Parse("green"));
                    Thread.Sleep(2000);

                    loadingStatus.Status("[green]Done![/]");
                    Thread.Sleep(350);

                    AnsiConsole.Clear();
                    Console.Clear();

                });
            }
        }

        //      DISPLAYS THE SYSTEM INFO      //
        public static void SysInfo()
        {
            // StringBuilder is a collection of strings that you can keep adding on to dynamically.
            StringBuilder systemInfo = new(string.Empty);

            systemInfo.AppendFormat("Machine Name: {0}\n", Environment.MachineName);
            //systemInfo.AppendFormat("User Name: {0}\n", Environment.UserName);
            systemInfo.AppendFormat("Processor Architecture: {0}\n", Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE"));
            systemInfo.AppendFormat("Processor Model: {0}\n", Environment.GetEnvironmentVariable("PROCESSOR_IDENTIFIER"));
            //systemInfo.AppendFormat("Processor Level:  {0}\n", Environment.GetEnvironmentVariable("PROCESSOR_LEVEL"));
            systemInfo.AppendFormat("CPU Core Count: {0} Cores\n", Environment.ProcessorCount);

            Console.ForegroundColor = ConsoleColor.Red;
            AnsiConsole.Write(new Markup(systemInfo.ToString() + "\n----------------------------------------------------------------------------------------").Centered());
            Console.ForegroundColor= ConsoleColor.White;

            /*
             * string UserMachineName = Environment.MachineName;
            string UserMachineOSVer = Environment.OSVersion.ToString();

            AnsiConsole.Write(new Markup($"\nYour System Info\nOS: {UserMachineOSVer}\nMachine Name: {UserMachineName}").Centered());
            */
        }

        //      DISPLAYS THE CONTINUE PROMPT TO LET USERS KNOW WHEN TO PRESS A KEY TO CONTINUE      //
        public static void ContinuePrompt()
        {
            AnsiConsole.Write(new Markup("[gray][italic][slowblink]\nPress any key to continue...\n[/][/][/]"));

            Console.ReadKey();
        }

        ////////////////////////////////////////////////////
        ///           STORY-RELATED FUNCTIONS            ///
        ////////////////////////////////////////////////////

        //      RANDOMISE ENEMY SPAWNING      //
        public static void EnemySpawnRandomiser(bool escapedFromTortureChamber)
        {
            if (escapedFromTortureChamber)
            {
                Random enemyTypeRND = new();
                int enemyTypeRNDInt = enemyTypeRND.Next(1, 5);
                
                switch (enemyTypeRNDInt)
                {
                    case 1:
                        BattleSystem.EncounteredEnemy = BattleSystem.EnemyList[0];

                        BattleSystem.EnemyStatSetup();
                        break;

                    case 2:
                        BattleSystem.EncounteredEnemy = BattleSystem.EnemyList[1];

                        BattleSystem.EnemyStatSetup();
                        break;

                    case 3:
                        BattleSystem.EncounteredEnemy = BattleSystem.EnemyList[2];

                        BattleSystem.EnemyStatSetup();
                        break;

                    case 4:
                        BattleSystem.EncounteredEnemy = BattleSystem.EnemyList[3];

                        BattleSystem.EnemyStatSetup();
                        break;
                }
            }
        }

        //      DISPLAYS THE CURRENT OBJECTIVE AND LOCATION      //
        public static void DisplayCurrentLocation(string currentLocation)
        {
            //Console.Title = $"Location: {currentLocation}";

            CreateHeading("Current Location", true);

            AnsiConsole.Write(new Markup($"[orange1]----------------------------------[/]\n[orange1][underline]Location:[/]\n {currentLocation}.[/][orange1]\n----------------------------------\n[/]").Centered());
        }

        public static void RandomisePuzzleSolutions(string currentLocation, string[] puzzleSolutions)
        {

        }

        ////////////////////////////////////////////////////
        ///         CONSOLE PRINTOUT FUNCTIONS           ///
        ////////////////////////////////////////////////////
        //      CREATES HEADING WITH A THICC LINE      //
        // STUPID THING RETURNS: "SPECTRE.CONSOLE.RULE"
        // WAIT NO, TURNS OUT I CAN'T WRITE "ANSICONSOLE.WRITE(HEADINGTEXTUNDERLINE + "\n\n)" BECAUSE IT'S A RULE, NOT A STRING. I NEED TO CONVERT IT TO A STRING FIRST WHICH I CAN'T SO I NEED TO EMBED NEW LINES WITHIN THE HEADINGTEXT STRING ITSELF. BUT THIS ISN'T REQUIRED BECAUSE \n\n DOES NOT DO ANYTHING TO "RULE".
        // SPECIFYING A STRING INCLUDING A RULE, WILL FORCE THE RULE TO BE CONVERTED TO A STRING, THAT IS WHY "SPECTRE.CONSOLE.RULE" IS RETURNED.
        // 40 MINUTES WASTED UGH. I NEED SLEEP. 😭
        public static void CreateHeading(string headingText, bool centreHeading)
        {
            HeadingTitle = headingText;
            
            if (centreHeading)
            {
                HeadingTextUnderline.Centered();
            }

            HeadingTextUnderline.Title = headingText;
            HeadingTextUnderline.Style = Style.Parse("orange1");
            AnsiConsole.Write(HeadingTextUnderline);
        }
        
        //      CREATES A MENU PROMPT      //
        public static void CreateMenuPrompt(string[] selectableOptions, string menuTitle, bool createHeading, bool centreHeading)
        {
            if (createHeading)
            {
                CreateHeading(menuTitle, centreHeading);
            }

            var userSelection = AnsiConsole.Prompt(new SelectionPrompt<string>()
            .PageSize(4)
            .AddChoices(selectableOptions)
            .MoreChoicesText("[grey][italic](Move up or down to reveal more options.)[/][/]")
            .HighlightStyle(Style.WithDecoration(Decoration.RapidBlink).Foreground(Color.Orange1))
            );

            ChosenOption = userSelection;
        }

        ////////////////////////////////////////////////////
        ///               OTHER FUNCTIONS                ///
        ////////////////////////////////////////////////////

        public static void DisplayCurrentTime()
        {
            while (true)
            {
                // Display the current time.
                Console.Title = "Current Time: " + DateTime.Now.ToString("HH:mm:ss");

                // Sleep for a short interval (e.g., 1 second).
                Thread.Sleep(1000);
            }
        }

        public static void ElapsedGameTime()
        {
            // Create a Stopwatch instance and start it.
            Stopwatch stopwatch = new();
            stopwatch.Start();

            while(true)
            {
                StopwatchHours = stopwatch.Elapsed.Hours.ToString("00");
                StopwatchMinutes = stopwatch.Elapsed.Minutes.ToString("00");
                StopwatchSeconds = stopwatch.Elapsed.Seconds.ToString("00");
        
                // Display the elapsed time.
                Console.Title = $"Elapsed Time: {StopwatchHours}:{StopwatchMinutes}:{StopwatchSeconds}";
                Thread.Sleep(100); // Adjust the refresh rate as needed.
            }
        }

        public static void RandomRewardFound()
        {
            Random rewardRND = new();
            int rewardRNDInt = rewardRND.Next(1, 10);

            switch (rewardRNDInt)
            {
                case 1:
                    Player.Bandages++;

                    AnsiConsole.MarkupLine("[green]You found a single bandage![/]");
                    ContinuePrompt();
                    break;

                case 2:
                    Player.ExpiredPacketOfMalteasers++;
                    Player.BottleOfWaterLabelledHolyWater++;

                    AnsiConsole.MarkupLine("[green]You found some expired Malteasers, and a bottle of water labelled 'Holy Water'![/]");
                    ContinuePrompt();
                    break;


                case 3:
                    Player.BottleOfWaterLabelledHolyWater++;

                    AnsiConsole.MarkupLine("[green]You found a bottle of water labelled 'Holy Water'![/]");
                    ContinuePrompt();
                    break;

                case 4:
                    Player.BoxofDoughnuts++;

                    AnsiConsole.MarkupLine("[green]You found a box of doughnuts![/]");
                    ContinuePrompt();
                    break;

                case 5:
                    Player.BottleOfAgedWater++;

                    AnsiConsole.MarkupLine("[green]You found a bottle of aged water![/]");
                    ContinuePrompt();
                break;
                
                case 6:
                    Player.BoxofFerreroRocher++;
                    Player.ExpiredPacketOfMalteasers++;
                    Player.BottleOfAgedWater++;

                    AnsiConsole.MarkupLine("[green]You found a box of Ferrero Rocher, an expired packet of Malteasers and a bottle of aged water![/]");
                    ContinuePrompt();
                    break;

                case 7:
                    Player.BottleOfAgedWater++;

                    AnsiConsole.MarkupLine("[green]You found a bottle of aged water![/]");
                    ContinuePrompt();
                    break;

                case 8:
                    Player.CrunchyChick += 2;
                    Player.BottleOfWaterLabelledHolyWater++;

                    AnsiConsole.MarkupLine("[green]You found 2 Crunchy Chicks, and a bottle of water labelled 'Holy Water'![/]");
                    ContinuePrompt();
                    break;

                case 9:
                    Player.CrunchyChick += 2;
                    Player.Bandages++;
                    Player.BoxofDoughnuts++;
                    Player.BottleOfAgedWater++;

                    AnsiConsole.MarkupLine("[green]You found 2 Crunchy Chicks, a bandage, a box of doughnuts, and a bottle of aged water![/]");
                    ContinuePrompt();
                    break;

                    case 10:
                    Player.Bandages += 4;
                    Player.BoxofDoughnuts++;

                    AnsiConsole.MarkupLine("[green]You found 4 bandages and a box of doughnuts![/]");
                    ContinuePrompt();
                    break;
            }
        }

        ////////////////////////////////////////////////////
        ///             SAVE/LOAD FUNCTIONS              ///
        ////////////////////////////////////////////////////
        public static void SaveGame()
        {
            bool gameSaving = true;

            while (gameSaving == true)
            {
                // Create a folder for the save data.
                // Got to reference System.IO.Directory because the IDE is thinking of another class named Dictionary. Until I found out that System.Collection.Generic was being reference, so removing the reference to that, has helped.
                
                if (!Directory.Exists("SaveGame"))
                {
                    CreateMenuPrompt(new[] {"Yes.", "No."}, "Save folder does not exist. Would you like to create one now?", true, false);

                    switch (ChosenOption)
                    {
                        case "Yes.":
                            AnsiConsole.Status().Start("\n[white]Creating a folder...[/]", loadingStatus =>
                            {
                                Thread.Sleep(1000);

                                loadingStatus.Status($"[orange1]A folder is being created, please wait...[/]");
                                loadingStatus.Spinner(Spinner.Known.Star2);
                                loadingStatus.SpinnerStyle(Style.Parse("orange1"));

                                Thread.Sleep(1500);

                                Directory.CreateDirectory("SaveGame");

                                loadingStatus.Status("[green]Done![/]");
                                loadingStatus.SpinnerStyle(Style.Parse("green"));
                                Thread.Sleep(750);

                                AnsiConsole.Clear();
                                Console.Clear();
                            });
                            break;

                        case "No.":
                            AnsiConsole.MarkupLine("[red]Save folder was not created. Your progress will not be saved.[/]");
                            ContinuePrompt();
                            gameSaving = false;
                            break;
                    }
                }                        

                using StreamWriter saveGameToFile = new("SaveGame\\SaveGame.intotheshadows_savegame");

                // Can I get an award for being the most forgetful person ever? Cos I forgot that arrays cannot contain values that are continuously being updated throughout the game, therefore, I have used them to save variables that change over time.

                saveGameToFile.WriteLine("// Player Variables");
                saveGameToFile.WriteLine($"PlayerCurrentLocation = {StoryMode.CurrentLocation}");
                saveGameToFile.WriteLine($"PlayerName = {StoryMode.PlayerNameFinalised}");
                saveGameToFile.WriteLine($"PlayerHealth = {Player.PlayerHealth}");
                saveGameToFile.WriteLine($"PlayerMaxHealth = {Player.PlayerMaxHealth}");
                saveGameToFile.WriteLine($"PlayerATKRating = {Player.PlayerATKRating}");
                saveGameToFile.WriteLine($"PlayerDEFRating = {Player.PlayerDEFRating}");
                saveGameToFile.WriteLine($"PlayerEXP = {Player.PlayerEXP}");
                saveGameToFile.WriteLine($"PlayerEXPRequired = {Player.PlayerEXPRequired}");
                saveGameToFile.WriteLine($"PlayerLevel = {Player.PlayerLevel}");
                saveGameToFile.WriteLine($"PlayerFear = {Player.PlayerFear}");
                saveGameToFile.WriteLine($"PlayerFearMax = {Player.PlayerMaxFear}");
                saveGameToFile.WriteLine($"PlayerIsBleeding = {Player.PlayerIsBleeding}");
                saveGameToFile.WriteLine($"PlayerIsBleedingTurns = {BattleSystem.BleedDMGDuration}");
                saveGameToFile.WriteLine($"PlayerFeared = {Player.PlayerFeared}");
                saveGameToFile.WriteLine($"PlayerIsDazed = {BattleSystem.PlayerIsDazed}");

                saveGameToFile.WriteLine("// Player Inventory");
                saveGameToFile.WriteLine($"ExpiredPacketOfMalteasers = {Player.ExpiredPacketOfMalteasers}");
                saveGameToFile.WriteLine($"BottleOfWaterLabelledHolyWater = {Player.BottleOfWaterLabelledHolyWater}");
                saveGameToFile.WriteLine($"BottleOfAgedWater = {Player.BottleOfAgedWater}");
                saveGameToFile.WriteLine($"BoxofDoughnuts = {Player.BoxofDoughnuts}");
                saveGameToFile.WriteLine($"BoxofFerreroRocher = {Player.BoxofFerreroRocher}");
                saveGameToFile.WriteLine($"CrunchyChick = {Player.CrunchyChick}");
                saveGameToFile.WriteLine($"PlayerBandages = {Player.Bandages}");

                saveGameToFile.WriteLine($"// Story Related Variables");
                saveGameToFile.WriteLine($"CanteenOfficerMet = {StoryMode.CanteenOfficerMet}");
                saveGameToFile.WriteLine($"CellA1Opened = {StoryMode.CellA1Opened}");
                saveGameToFile.WriteLine($"CellA2Opened = {StoryMode.CellA2Opened}");
                saveGameToFile.WriteLine($"CellA3Opened = {StoryMode.CellA3Opened}");
                saveGameToFile.WriteLine($"CellA4Opened = {StoryMode.CellA4Opened}");
                saveGameToFile.WriteLine($"CellA5Opened = {StoryMode.CellA5Opened}");
                saveGameToFile.WriteLine($"CellA6Opened = {StoryMode.CellA6Opened}");
                saveGameToFile.WriteLine($"CellB1Opened = {StoryMode.CellB1Opened}");
                saveGameToFile.WriteLine($"CellB2Opened = {StoryMode.CellB2Opened}");
                saveGameToFile.WriteLine($"CellB3Opened = {StoryMode.CellB3Opened}");
                saveGameToFile.WriteLine($"CellB4Opened = {StoryMode.CellB4Opened}");
                saveGameToFile.WriteLine($"CellB5Opened = {StoryMode.CellB5Opened}");
                saveGameToFile.WriteLine($"CellB6Opened = {StoryMode.CellB6Opened}");
                saveGameToFile.WriteLine($"EntranceVisited = {StoryMode.EntranceVisited}");
                saveGameToFile.WriteLine($"EnteringKeyPadCode = {StoryMode.EnteringKeyPadCode}");
                saveGameToFile.WriteLine($"EntranceNoPower = {StoryMode.EntranceNoPower}");
                saveGameToFile.WriteLine($"EntranceMissingKeyItems = {StoryMode.EntranceMissingKeyItems}");
                saveGameToFile.WriteLine($"ExecutionRoomFriendVisited = {StoryMode.ExecutionRoomFriendVisited}");
                saveGameToFile.WriteLine($"ExecutionRoomVisited = {StoryMode.ExecutionRoomVisited}");
                saveGameToFile.WriteLine($"EscapedFromTortureChamber = {StoryMode.EscapedFromTortureChamber}");
                saveGameToFile.WriteLine($"GuardsPostBodyChecked = {StoryMode.GuardsPostBodyChecked}");
                saveGameToFile.WriteLine($"GuardsPostVisited = {StoryMode.GuardsPostVisited}");
                saveGameToFile.WriteLine($"GuardsPostSwitchPulled = {StoryMode.GuardsPostSwitchPulled}");
                saveGameToFile.WriteLine($"GuardsPostObserved = {StoryMode.GuardsPostObserved}");
                saveGameToFile.WriteLine($"JournalDiscovered = {StoryMode.JournalDiscovered}");
                saveGameToFile.WriteLine($"LaundryRoom3CanteenVisited = {StoryMode.LaundryRoom3CanteenVisited}");
                saveGameToFile.WriteLine($"LaundryRoom1BeetlesDefeated = {StoryMode.LaundryRoom1BeetlesDefeated}");
                saveGameToFile.WriteLine($"LaundryRoomDeadGuardRadioAlerted = {StoryMode.LaundryRoomDeadGuardRadioAlerted}");
                saveGameToFile.WriteLine($"LaundryRoomGuardAlerted = {StoryMode.LaundryRoomGuardAlerted}");
                saveGameToFile.WriteLine($"LaundryRoomGuardExamined = {StoryMode.LaundryRoomGuardExamined}");
                saveGameToFile.WriteLine($"LaundryRoomGuardKilled = {StoryMode.LaundryRoomGuardKilled}");
                saveGameToFile.WriteLine($"LaundryRoomGuardRelocated = {StoryMode.LaundryRoomGuardRelocated}");
                saveGameToFile.WriteLine($"LaundryRoomIllegibleWallTextRead = {StoryMode.LaundryRoomIllegibleWallTextRead}");
                saveGameToFile.WriteLine($"LaundryRoomMonsterNoiseMade = {StoryMode.LaundryRoomMonsterNoiseMade}");
                saveGameToFile.WriteLine($"LockedWeaponsRoomDoorChecked = {StoryMode.LockedWeaponsRoomDoorChecked}");
                saveGameToFile.WriteLine($"ObtainedBandages = {StoryMode.ObtainedBandages}");
                saveGameToFile.WriteLine($"ObtainedFirstAid = {StoryMode.ObtainedFirstAid}");
                saveGameToFile.WriteLine($"ObtainedFuel = {StoryMode.ObtainedFuel}");
                saveGameToFile.WriteLine($"ObtainedKeycardLevel1 = {StoryMode.ObtainedKeycardLevel1}");
                saveGameToFile.WriteLine($"ObtainedKeycardLevel2 = {StoryMode.ObtainedKeycardLevel2}");
                saveGameToFile.WriteLine($"ObtainedKeycardLevel3 = {StoryMode.ObtainedKeycardLevel3}");
                saveGameToFile.WriteLine($"ObtainedKeyPadCode = {StoryMode.ObtainedKeyPadCode}");
                saveGameToFile.WriteLine($"ObtainedKeyPadWire1 = {StoryMode.ObtainedKeyPadWire1}");
                saveGameToFile.WriteLine($"ObtainedKeyPadWire2 = {StoryMode.ObtainedKeyPadWire2}");
                saveGameToFile.WriteLine($"ObtainedKeyPadWire3 = {StoryMode.ObtainedKeyPadWire3}");
                saveGameToFile.WriteLine($"ObtainedMap = {StoryMode.ObtainedMap}");
                saveGameToFile.WriteLine($"ObtainedNoiseMaker = {StoryMode.ObtainedNoiseMaker}");
                saveGameToFile.WriteLine($"NoiseMakerUsed = {StoryMode.NoiseMakerUsed}");
                saveGameToFile.WriteLine($"PlayerLeftSolitary = {StoryMode.PlayerLeftSolitary}");
                saveGameToFile.WriteLine($"PowerTurnedOn = {StoryMode.PowerTurnedOn}");
                saveGameToFile.WriteLine($"RecRoomVisited = {StoryMode.RecRoomVisited}");
                saveGameToFile.WriteLine($"RescuedFriend1 = {StoryMode.RescuedFriend1}");
                saveGameToFile.WriteLine($"RescuedFriend2 = {StoryMode.RescuedFriend2}");
                saveGameToFile.WriteLine($"ShowerRoomGuardKilled = {StoryMode.ShowerRoomGuardKilled}");
                saveGameToFile.WriteLine($"ShowerRoomBodyExamined = {StoryMode.ShowerRoomBodyExamined}");
                saveGameToFile.WriteLine($"SirensPlayed = {StoryMode.SirensPlayed}");
                saveGameToFile.WriteLine($"SolitaryVisited = {StoryMode.SolitaryVisited}");
                saveGameToFile.WriteLine($"Solitary2GuardKilled = {StoryMode.Solitary2GuardKilled}");
                saveGameToFile.WriteLine($"WeaponsRoomDoorWindowChecked = {StoryMode.WeaponsRoomDoorWindowChecked}");
                saveGameToFile.WriteLine($"WeaponRoomDoorOpened = {StoryMode.WeaponRoomDoorOpened}");
                saveGameToFile.WriteLine($"UnidentifiedPeopleIdentified = {StoryMode.UnidentifiedPeopleIdentified}");

                saveGameToFile.WriteLine($"// Player Options");
                saveGameToFile.WriteLine($"PlayBGM = {StoryMode.PlayBGM}");
                saveGameToFile.WriteLine($"PlaySFX = {StoryMode.PlaySFX}");

                saveGameToFile.WriteLine("// Game Statistics");
                saveGameToFile.WriteLine($"stopwatchHours = {StopwatchHours}");
                saveGameToFile.WriteLine($"stopwatchMinutes = {StopwatchMinutes}");
                saveGameToFile.WriteLine($"stopwatchSeconds = {StopwatchSeconds}");

                AnsiConsole.MarkupLine("\n[green]Your progress has been saved![/]\n");
                ContinuePrompt();
                gameSaving = false;
            }
        }

        // Got a little help from StaticOverflow for this one. But I understood what is being done after reading and executing the code.
        public static void LoadGame()
        {
            void DisplaySaveData()
            {
                // Read each line from the file and print it
                using (StreamReader saveReader = new("SaveGame\\SaveGame.intotheshadows_savegame"))
                {
                    while (!saveReader.EndOfStream)
                    {
                        string currentLine = saveReader.ReadLine();

                        if (currentLine.Contains("// Player Variables"))
                        {
                            currentLine.Replace("// Player Variables", "Player Stats").Trim();
                        }

                        if (currentLine.Contains("// Player Inventory"))
                        {
                            currentLine.Replace("// Player Inventory", "Player Inventory").Trim();
                        }

                        if (currentLine.Contains("// Story Related Variables"))
                        {
                            currentLine.Replace("// Story Related Variables", "Story Related Variables").Trim();
                        }

                        if (currentLine.Contains("// Player Options"))
                        {
                            currentLine.Replace("// Player Options", "Player Options").Trim();
                        }

                        if (currentLine.Contains("// Game Statistics"))
                        {
                            currentLine.Replace("// Game Statistics", "Game Statistics").Trim();
                        }

                        Console.WriteLine(currentLine);

                    }
                }
            }
            void LoadGameFromFile()
            {
                // currentLine will be used to allow the SteamReader to assign the "currently" read line to this string.
                string currentLine;

                // StreamReader is used to read the file, specified within the parenthesis.
                StreamReader loadGameFromFile = new StreamReader("SaveGame\\SaveGame.intotheshadows_savegame");

                // Loop to check if the current line that has been read, is not empty/null.
                while ((currentLine = loadGameFromFile.ReadLine()) != null)
                {
                    // Checks if the currently read line, contains the specified string.
                    if (currentLine.Contains("PlayerCurrentLocation"))
                    {
                        // Update the correpsonding variable with the value that is read from the file.
                        // Replace the currentLine's string with anything that is leftover on the same line, and trim the string to remove any spaces.
                        // In this instance, the string contains "PlayerCurrentLocation = " and we want to remove that, and only keep the location name. The location name is appended after the "=".
                        StoryMode.CurrentLocation = currentLine.Replace("PlayerCurrentLocation = ", "").Trim();
                    }

                    if (currentLine.Contains("PlayerName"))
                    {
                        StoryMode.PlayerNameFinalised = currentLine.Replace("PlayerName = ", "").Trim();
                    }

                    if (currentLine.Contains("PlayerHealth"))
                    {
                        Player.PlayerHealth = float.Parse(currentLine.Replace("PlayerHealth = ", "").Trim());
                    }


                    if (currentLine.Contains("PlayerMaxHealth"))
                    {
                        Player.PlayerMaxHealth = Convert.ToInt16(currentLine.Replace("PlayerMaxHealth = ", "").Trim());
                    }

                    if (currentLine.Contains("PlayerATKRating"))
                    {
                        Player.PlayerATKRating = float.Parse(currentLine.Replace("PlayerATKRating = ", "").Trim());
                    }

                    if (currentLine.Contains("PlayerDEFRating"))
                    {
                        Player.PlayerDEFRating = float.Parse(currentLine.Replace("PlayerDEFRating = ", "").Trim());
                    }

                    // Might accidentally load up the value of PlayerEXPRequired instead, since that is PlayerEXP is contained with PlayerEXPRequired.
                    if (currentLine.Contains("PlayerEXP") && !currentLine.Contains("PlayerEXPRequired"))
                    {
                        Player.PlayerEXP = float.Parse(currentLine.Replace("PlayerEXP = ", "").Trim());
                    }

                    if (currentLine.Contains("PlayerEXPRequired"))
                    {
                        Player.PlayerEXP = float.Parse(currentLine.Replace("PlayerEXPRequired = ", "").Trim());
                    }

                    if (currentLine.Contains("PlayerLevel"))
                    {
                        Player.PlayerLevel = Convert.ToInt16(currentLine.Replace("PlayerLevel = ", "").Trim());
                    }

                    if (currentLine.Contains("PlayerFear") && !currentLine.Contains("PlayerFearMax") && !currentLine.Contains("PlayerFeared"))
                    {
                        Player.PlayerFear = Convert.ToInt16(currentLine.Replace("PlayerFear = ", "").Trim());
                    }

                    if (currentLine.Contains("PlayerFearMax") && !currentLine.Contains("PlayerFeared"))
                    {
                        Player.PlayerMaxFear = Convert.ToInt16(currentLine.Replace("PlayerFearMax = ", "").Trim());
                    }

                    if (currentLine.Contains("PlayerIsBleeding") && !currentLine.Contains("PlayerIsBleedingTurns"))
                    {
                        Player.PlayerIsBleeding = Convert.ToBoolean(currentLine.Replace("PlayerIsBleeding = ", "").Trim());
                    }

                    if (currentLine.Contains("PlayerIsBleedingTurns"))
                    {
                        BattleSystem.BleedDMGDuration = Convert.ToInt16(currentLine.Replace("PlayerIsBleedingTurns = ", "").Trim());
                    }

                    if (currentLine.Contains("PlayerFeared"))
                    {
                        Player.PlayerFeared = Convert.ToBoolean(currentLine.Replace("PlayerFeared = ", "").Trim());
                    }

                    if (currentLine.Contains("PlayerIsDazed"))
                    {
                        BattleSystem.PlayerIsDazed = Convert.ToBoolean(currentLine.Replace("PlayerIsDazed = ", "").Trim());
                    }

                    if (currentLine.Contains("ExpiredPacketOfMalteasers"))
                    {
                        Player.ExpiredPacketOfMalteasers = Convert.ToInt16(currentLine.Replace("ExpiredPacketOfMalteasers = ", "").Trim());
                    }

                    if (currentLine.Contains("BottleOfWaterLabelledHolyWater"))
                    {
                        Player.BottleOfWaterLabelledHolyWater = Convert.ToInt16(currentLine.Replace("BottleOfWaterLabelledHolyWater = ", "").Trim());
                    }

                    if (currentLine.Contains("BottleOfAgedWater"))
                    {
                        Player.BottleOfAgedWater = Convert.ToInt16(currentLine.Replace("BottleOfAgedWater = ", "").Trim());
                    }

                    if (currentLine.Contains("BoxofDoughnuts"))
                    {
                        Player.BoxofDoughnuts = Convert.ToInt16(currentLine.Replace("BoxofDoughnuts = ", "").Trim());
                    }

                    if (currentLine.Contains("BoxofFerreroRocher"))
                    {
                        Player.BoxofFerreroRocher = Convert.ToInt16(currentLine.Replace("BoxofFerreroRocher = ", "").Trim());
                    }

                    if (currentLine.Contains("CrunchyChick"))
                    {
                        Player.CrunchyChick = Convert.ToInt16(currentLine.Replace("CrunchyChick = ", "").Trim());
                    }

                    if (currentLine.Contains("PlayerBandages"))
                    {
                        Player.Bandages = Convert.ToInt16(currentLine.Replace("PlayerBandages = ", "").Trim());
                    }

                    if (currentLine.Contains("CanteenOfficerMet"))
                    {
                        StoryMode.CanteenOfficerMet = Convert.ToBoolean(currentLine.Replace("CanteenOfficerMet = ", "").Trim());
                    }

                    if (currentLine.Contains("CellA1Opened"))
                    {
                        StoryMode.CellA1Opened = Convert.ToBoolean(currentLine.Replace("CellA1Opened = ", "").Trim());
                    }

                    if (currentLine.Contains("CellA2Opened"))
                    {
                        StoryMode.CellA2Opened = Convert.ToBoolean(currentLine.Replace("CellA2Opened = ", "").Trim());
                    }

                    if (currentLine.Contains("CellA3Opened"))
                    {
                        StoryMode.CellA3Opened = Convert.ToBoolean(currentLine.Replace("CellA3Opened = ", "").Trim());
                    }

                    if (currentLine.Contains("CellA4Opened"))
                    {
                        StoryMode.CellA4Opened = Convert.ToBoolean(currentLine.Replace("CellA4Opened = ", "").Trim());
                    }

                    if (currentLine.Contains("CellA5Opened"))
                    {
                        StoryMode.CellA5Opened = Convert.ToBoolean(currentLine.Replace("CellA5Opened = ", "").Trim());
                    }

                    if (currentLine.Contains("CellA6Opened"))
                    {
                        StoryMode.CellA6Opened = Convert.ToBoolean(currentLine.Replace("CellA6Opened = ", "").Trim());
                    }

                    if (currentLine.Contains("CellB1Opened"))
                    {
                        StoryMode.CellB1Opened = Convert.ToBoolean(currentLine.Replace("CellB1Opened = ", "").Trim());
                    }

                    if (currentLine.Contains("CellB2Opened"))
                    {
                        StoryMode.CellB2Opened = Convert.ToBoolean(currentLine.Replace("CellB2Opened = ", "").Trim());
                    }

                    if (currentLine.Contains("CellB3Opened"))
                    {
                        StoryMode.CellB3Opened = Convert.ToBoolean(currentLine.Replace("CellB3Opened = ", "").Trim());
                    }

                    if (currentLine.Contains("CellB4Opened"))
                    {
                        StoryMode.CellB4Opened = Convert.ToBoolean(currentLine.Replace("CellB4Opened = ", "").Trim());
                    }

                    if (currentLine.Contains("CellB5Opened"))
                    {
                        StoryMode.CellB5Opened = Convert.ToBoolean(currentLine.Replace("CellB5Opened = ", "").Trim());
                    }

                    if (currentLine.Contains("CellB6Opened"))
                    {
                        StoryMode.CellB6Opened = Convert.ToBoolean(currentLine.Replace("CellB6Opened = ", "").Trim());
                    }

                    if (currentLine.Contains("EntranceVisited"))
                    {
                        StoryMode.EntranceVisited = Convert.ToBoolean(currentLine.Replace("EntranceVisited = ", "").Trim());
                    }

                    if (currentLine.Contains("EnteringKeyPadCode"))
                    {
                        StoryMode.EnteringKeyPadCode = Convert.ToBoolean(currentLine.Replace("EnteringKeyPadCode = ", "").Trim());
                    }

                    if (currentLine.Contains("EntranceNoPower"))
                    {
                        StoryMode.EntranceNoPower = Convert.ToBoolean(currentLine.Replace("EntranceNoPower = ", "").Trim());
                    }

                    if (currentLine.Contains("EntranceMissingKeyItems"))
                    {
                        StoryMode.EntranceMissingKeyItems = Convert.ToBoolean(currentLine.Replace("EntranceMissingKeyItems = ", "").Trim());
                    }

                    if (currentLine.Contains("ExecutionRoomFriendVisited"))
                    {
                        StoryMode.ExecutionRoomFriendVisited = Convert.ToBoolean(currentLine.Replace("ExecutionRoomFriendVisited = ", "").Trim());
                    }

                    if (currentLine.Contains("ExecutionRoomVisited"))
                    {
                        StoryMode.ExecutionRoomVisited = Convert.ToBoolean(currentLine.Replace("ExecutionRoomVisited = ", "").Trim());
                    }

                    if (currentLine.Contains("EscapedFromTortureChamber"))
                    {
                        StoryMode.EscapedFromTortureChamber = Convert.ToBoolean(currentLine.Replace("EscapedFromTortureChamber = ", "").Trim());
                    }

                    if (currentLine.Contains("GuardsPostBodyChecked"))
                    {
                        StoryMode.GuardsPostBodyChecked = Convert.ToBoolean(currentLine.Replace("GuardsPostBodyChecked = ", "").Trim());
                    }

                    if (currentLine.Contains("GuardsPostVisited"))
                    {
                        StoryMode.GuardsPostVisited = Convert.ToBoolean(currentLine.Replace("GuardsPostVisited = ", "").Trim());
                    }

                    if (currentLine.Contains("GuardsPostSwitchPulled"))
                    {
                        StoryMode.GuardsPostSwitchPulled = Convert.ToBoolean(currentLine.Replace("GuardsPostSwitchPulled = ", "").Trim());
                    }

                    if (currentLine.Contains("GuardsPostObserved"))
                    {
                        StoryMode.GuardsPostObserved = Convert.ToBoolean(currentLine.Replace("GuardsPostObserved = ", "").Trim());
                    }

                    if (currentLine.Contains("JournalDiscovered"))
                    {
                        StoryMode.JournalDiscovered = Convert.ToBoolean(currentLine.Replace("JournalDiscovered = ", "").Trim());
                    }

                    if (currentLine.Contains("LaundryRoom1BeetlesDefeated"))
                    {
                        StoryMode.LaundryRoom1BeetlesDefeated = Convert.ToBoolean(currentLine.Replace("LaundryRoom1BeetlesDefeated = ", "").Trim());
                    }

                    if (currentLine.Contains("LaundryRoom3CanteenVisited"))
                    {
                        StoryMode.LaundryRoom3CanteenVisited = Convert.ToBoolean(currentLine.Replace("LaundryRoom3CanteenVisited = ", "").Trim());
                    }

                    if (currentLine.Contains("LaundryRoomDeadGuardRadioAlerted"))
                    {
                        StoryMode.LaundryRoomDeadGuardRadioAlerted = Convert.ToBoolean(currentLine.Replace("LaundryRoomDeadGuardRadioAlerted = ", "").Trim());
                    }

                    if (currentLine.Contains("LaundryRoomGuardAlerted"))
                    {
                        StoryMode.LaundryRoomGuardAlerted = Convert.ToBoolean(currentLine.Replace("LaundryRoomGuardAlerted = ", "").Trim());
                    }

                    if (currentLine.Contains("LaundryRoomGuardExamined"))
                    {
                        StoryMode.LaundryRoomGuardExamined = Convert.ToBoolean(currentLine.Replace("LaundryRoomGuardExamined = ", "").Trim());
                    }

                    if (currentLine.Contains("LaundryRoomGuardKilled"))
                    {
                        StoryMode.LaundryRoomGuardKilled = Convert.ToBoolean(currentLine.Replace("LaundryRoomGuardKilled = ", "").Trim());
                    }

                    if (currentLine.Contains("LaundryRoomGuardRelocated"))
                    {
                        StoryMode.LaundryRoomGuardRelocated = Convert.ToBoolean(currentLine.Replace("LaundryRoomGuardRelocated = ", "").Trim());
                    }

                    if (currentLine.Contains("LaundryRoomIllegibleWallTextRead"))
                    {
                        StoryMode.LaundryRoomIllegibleWallTextRead = Convert.ToBoolean(currentLine.Replace("LaundryRoomIllegibleWallTextRead = ", "").Trim());
                    }

                    if (currentLine.Contains("LaundryRoomMonsterNoiseMade"))
                    {
                        StoryMode.LaundryRoomMonsterNoiseMade = Convert.ToBoolean(currentLine.Replace("LaundryRoomMonsterNoiseMade = ", "").Trim());
                    }

                    if (currentLine.Contains("LockedWeaponsRoomDoorChecked"))
                    {
                        StoryMode.LockedWeaponsRoomDoorChecked = Convert.ToBoolean(currentLine.Replace("LockedWeaponsRoomDoorChecked = ", "").Trim());
                    }

                    if (currentLine.Contains("ObtainedBandages"))
                    {
                        StoryMode.ObtainedBandages = Convert.ToBoolean(currentLine.Replace("ObtainedBandages = ", "").Trim());
                    }

                    if (currentLine.Contains("ObtainedFirstAid"))
                    {
                        StoryMode.ObtainedFirstAid = Convert.ToBoolean(currentLine.Replace("ObtainedFirstAid = ", "").Trim());
                    }

                    if (currentLine.Contains("ObtainedFuel"))
                    {
                        StoryMode.ObtainedFuel = Convert.ToBoolean(currentLine.Replace("ObtainedFuel = ", "").Trim());
                    }

                    if (currentLine.Contains("ObtainedKeycardLevel1"))
                    {
                        StoryMode.ObtainedKeycardLevel1 = Convert.ToBoolean(currentLine.Replace("ObtainedKeycardLevel1 = ", "").Trim());
                    }

                    if (currentLine.Contains("ObtainedKeycardLevel2"))
                    {
                        StoryMode.ObtainedKeycardLevel2 = Convert.ToBoolean(currentLine.Replace("ObtainedKeycardLevel2 = ", "").Trim());
                    }

                    if (currentLine.Contains("ObtainedKeycardLevel3"))
                    {
                        StoryMode.ObtainedKeycardLevel3 = Convert.ToBoolean(currentLine.Replace("ObtainedKeycardLevel3 = ", "").Trim());
                    }

                    if (currentLine.Contains("ObtainedKeyPadCode"))
                    {
                        StoryMode.ObtainedKeyPadCode = Convert.ToBoolean(currentLine.Replace("ObtainedKeyPadCode = ", "").Trim());
                    }

                    if (currentLine.Contains("ObtainedKeyPadWire1"))
                    {
                        StoryMode.ObtainedKeyPadWire1 = Convert.ToBoolean(currentLine.Replace("ObtainedKeyPadWire1 = ", "").Trim());
                    }

                    if (currentLine.Contains("ObtainedKeyPadWire2"))
                    {
                        StoryMode.ObtainedKeyPadWire2 = Convert.ToBoolean(currentLine.Replace("ObtainedKeyPadWire2 = ", "").Trim());
                    }

                    if (currentLine.Contains("ObtainedKeyPadWire3"))
                    {
                        StoryMode.ObtainedKeyPadWire3 = Convert.ToBoolean(currentLine.Replace("ObtainedKeyPadWire3 = ", "").Trim());
                    }

                    if (currentLine.Contains("ObtainedMap"))
                    {
                        StoryMode.ObtainedMap = Convert.ToBoolean(currentLine.Replace("ObtainedMap = ", "").Trim());
                    }

                    if (currentLine.Contains("ObtainedNoiseMaker"))
                    {
                        StoryMode.ObtainedNoiseMaker = Convert.ToBoolean(currentLine.Replace("ObtainedNoiseMaker = ", "").Trim());
                    }

                    if (currentLine.Contains("NoiseMakerUsed"))
                    {
                        StoryMode.NoiseMakerUsed = Convert.ToBoolean(currentLine.Replace("NoiseMakerUsed = ", "").Trim());
                    }

                    if (currentLine.Contains("PlayerLeftSolitary"))
                    {
                        StoryMode.PlayerLeftSolitary = Convert.ToBoolean(currentLine.Replace("PlayerLeftSolitary = ", "").Trim());
                    }

                    if (currentLine.Contains("PowerTurnedOn"))
                    {
                        StoryMode.PowerTurnedOn = Convert.ToBoolean(currentLine.Replace("PowerTurnedOn = ", "").Trim());
                    }

                    if (currentLine.Contains("RecRoomVisited"))
                    {
                        StoryMode.RecRoomVisited = Convert.ToBoolean(currentLine.Replace("RecRoomVisited = ", "").Trim());
                    }

                    if (currentLine.Contains("RescuedFriend1"))
                    {
                        StoryMode.RescuedFriend1 = Convert.ToBoolean(currentLine.Replace("RescuedFriend1 = ", "").Trim());
                    }

                    if (currentLine.Contains("RescuedFriend2"))
                    {
                        StoryMode.RescuedFriend2 = Convert.ToBoolean(currentLine.Replace("RescuedFriend2 = ", "").Trim());
                    }

                    if (currentLine.Contains("ShowerRoomGuardKilled"))
                    {
                        StoryMode.ShowerRoomGuardKilled = Convert.ToBoolean(currentLine.Replace("ShowerRoomGuardKilled = ", "").Trim());
                    }

                    if (currentLine.Contains("ShowerRoomBodyExamined"))
                    {
                        StoryMode.ShowerRoomBodyExamined = Convert.ToBoolean(currentLine.Replace("ShowerRoomBodyExamined = ", "").Trim());
                    }

                    if (currentLine.Contains("SirensPlayed"))
                    {
                        StoryMode.SirensPlayed = Convert.ToBoolean(currentLine.Replace("SirensPlayed = ", "").Trim());
                    }

                    if (currentLine.Contains("SolitaryVisited"))
                    {
                        StoryMode.SolitaryVisited = Convert.ToBoolean(currentLine.Replace("SolitaryVisited = ", "").Trim());
                    }

                    if (currentLine.Contains("Solitary2GuardKilled"))
                    {
                        StoryMode.Solitary2GuardKilled = Convert.ToBoolean(currentLine.Replace("Solitary2GuardKilled = ", "").Trim());
                    }

                    if (currentLine.Contains("WeaponsRoomDoorWindowChecked"))
                    {
                        StoryMode.WeaponsRoomDoorWindowChecked = Convert.ToBoolean(currentLine.Replace("WeaponsRoomDoorWindowChecked = ", "").Trim());
                    }

                    if (currentLine.Contains("WeaponRoomDoorOpened"))
                    {
                        StoryMode.WeaponRoomDoorOpened = Convert.ToBoolean(currentLine.Replace("WeaponRoomDoorOpened = ", "").Trim());
                    }

                    if (currentLine.Contains("UnidentifiedPeopleIdentified"))
                    {
                        StoryMode.UnidentifiedPeopleIdentified = Convert.ToBoolean(currentLine.Replace("UnidentifiedPeopleIdentified = ", "").Trim());
                    }

                    if (currentLine.Contains("PlayBGM"))
                    {
                        StoryMode.PlayBGM = Convert.ToBoolean(currentLine.Replace("PlayBGM = ", "").Trim());
                    }

                    if (currentLine.Contains("PlaySFX"))
                    {
                        StoryMode.PlaySFX = Convert.ToBoolean(currentLine.Replace("PlaySFX = ", "").Trim());
                    }

                    if (currentLine.Contains("stopwatchHours"))
                    {
                        short hours = short.Parse(currentLine.Replace("stopwatchHours = ", "").Trim());

                        StopwatchHours = hours.ToString("00");

                    }

                    if (currentLine.Contains("stopwatchMinutes"))
                    {
                        short minutes = short.Parse(currentLine.Replace("stopwatchMinutes = ", "").Trim());

                        StopwatchMinutes = minutes.ToString("00");
                    }

                    if (currentLine.Contains("stopwatchSeconds"))
                    {
                        short seconds = short.Parse(currentLine.Replace("stopwatchSeconds = ", "").Trim());

                        StopwatchSeconds = seconds.ToString("00");
                    }
                }

                // Close the file after reading.
                loadGameFromFile.Close();

                AnsiConsole.MarkupLine("[green]Your save data has been loaded![/]");

                ContinuePrompt();
            }

            // Check if the file exists. Specified the exact file name, in the exact folder.
            if (File.Exists("SaveGame\\SaveGame.intotheshadows_savegame"))
            {
                // Create a menu prompt to let the user know that the save file has been found.
                CreateMenuPrompt(new[] { "Show the data saved to the save file.", "Load the save data." }, "A save file has been found...", true, false);

                switch (ChosenOption)
                {
                    case "Show the data saved to the save file.":
                        DisplaySaveData();
                        ContinuePrompt();
                        LoadGame();
                        break;

                    case "Load the save data.":
                        LoadGameFromFile();
                        break;
                }
            }

            else
            {
                AnsiConsole.MarkupLine("[red]No save file found.[/]");
                ContinuePrompt();
            }
        }

        public static void CheckForSaveData()
        {
            if (File.Exists("SaveGame\\SaveGame.intotheshadows_savegame"))
            {
                AnsiConsole.MarkupLine("\n[green]Wait! Before continuing, you already have an existing save data.[/]\n");

                CreateMenuPrompt(new[] { "Yes.", "No." }, "Would you like to load it?", true, false);

                switch (ChosenOption)
                {
                    case "Yes.":
                        LoadGame();
                        break;

                    case "No.":
                        AnsiConsole.MarkupLine("[red]Save file will not be loaded. This data will be overwritten at the next save point.[/]");
                        ContinuePrompt();
                        break;
                }
            }
        }
    }
}