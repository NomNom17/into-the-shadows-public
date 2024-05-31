using Spectre.Console;

namespace Into_the_Shadows_Game
{
    internal class MainGame
    {
        public static bool FirstTimeSetup = false;

        public static void Main()
        {
            // For the main menu only, I will be specifying the file name of the sound, as it is the only place where I will be using the sound.
            string fileName = "StoryMode_Sound_Files\\BGM\\main-menu-bgm.mp3";
            
            // CheckBGMPlayer() method is used to check whether the BGMPlayer is currently playing any music, if it is, it will stop the music to prevent an exception from being thrown.
            SoundSystem.CheckBGMPlayer();
            
            // StoryBGMPlayer() method is used to play the music, and it takes in two parameters, the file name of the sound, and whether the sound should be played or not.
            SoundSystem.StoryBGMPlayer(fileName, StoryMode.PlayBGM);

            AnsiConsole.Write(new FigletText("Into the Shadows").Color(Color.Red).Centered());
            AnsiConsole.Write(new Markup("[red]\nA Console-Based Horror Adventure Game.\nCreated by: NomNom.\n\n----------------------------------------------------------------------------------------\n[/]").Centered());
            AnsiConsole.Write(new Markup("[orange1]NOTE: [link=https://www.microsoft.com/store/productid/9N0DX20HK701?ocid=pdpshare]Windows Terminal[/] is recommended to be used for this game. This is to enable a few decorative features.\n[italic]CTRL + Left Click to open the link.[/]\n\n[red]----------------------------------------------------------------------------------------[/]\n[/]").Centered());

            // Define what are the selectable options are.
            string[] MainMenuSelection = { "Play Game.", "Debug Menu." , "Options.", "How to Play?", "Enemy Bios.", "About.", "Exit the game." };

            // Create the menu prompt, specifying the array of strings that will be selectable by the user, the title of the heading, whether a heading should be created, and whether the heading text should be centred.
            ExtFunctions.CreateMenuPrompt(MainMenuSelection, "Choose any option below to continue. Use the Up and Down arrow keys to navigate.", true, true);

            // Create a switch statement to handle the user's selection.
            switch (ExtFunctions.ChosenOption)
            {
                case "Play Game.":
                    
                    // Display a small prompt, asking the user whether they want to read the disclaimer or not.
                    AnsiConsole.MarkupLine("[orange1]\nWould you want to read the Disclaimer information before getting into the game?[/]\n");

                    ExtFunctions.CreateMenuPrompt(new[] { "Yes.", "No." }, "Choose any option below to continue. Use the Up and Down arrow keys to navigate.", true, false);

                    switch (ExtFunctions.ChosenOption)
                    {
                        case "Yes.":
                            // We'll check if any music is playing, if it is, we'll stop it to stop the game throwing an exception.
                            SoundSystem.CheckBGMPlayer();
                            // Sweep sweep the console from the previous menu.
                            ExtFunctions.CleanConsole();
                            // Call the method that displays the disclaimer information.
                            StoryMode.DisclaimerInfo();
                            break;

                        case "No.":
                            SoundSystem.CheckBGMPlayer();
                            ExtFunctions.CheckForSaveData();
                            ExtFunctions.CleanConsole();
                            StoryMode.Introduction();
                            break;
                    }
                    break;

                case "Debug Menu.":
                    MainMenuOptions.DebugMenu();
                    break;

                case "Options.":
                    MainMenuOptions.Options();
                    break;

                case "How to Play?":
                    MainMenuOptions.HowToPlay();
                    break;

                case "About.":
                    MainMenuOptions.About();
                    break;

                case "Enemy Bios.":
                    MainMenuOptions.EnemyBios();
                    break;

                case "Exit the game.":
                    // This boolean is used to enable CleanConsole() method to detect that the player is quitting the game, so the appropriate text is displayed.
                    StoryMode.PlayerIsQuitting = true;
                    ExtFunctions.CleanConsole();
                    Environment.Exit(666);
                    break;
            }
        }
    }

    internal class StoryMode
    {
        //      STORY RELATED       //
        public static bool CanteenOfficerMet = false;
        public static bool EntranceVisited = false;
        public static bool EnteringKeyPadCode = false;
        public static bool ExecutionRoomVisited = false;
        public static bool GuardsPostObserved = false;
        public static bool GuardsPostSwitchPulled = false;
        public static bool JournalDiscovered = false;
        public static bool LaundryRoom3CanteenVisited = false;
        public static bool LaundryRoomGuardAlerted = false;
        public static bool LaundryRoomGuardKilled = false;
        public static bool LaundryRoomGuardRelocated = false;
        public static bool LaundryRoomDeadGuardRadioAlerted = false;
        public static bool LaundryRoomMonsterNoiseMade = false;
        public static bool ObtainedBandages = false;
        public static bool ObtainedFirstAid = false;
        public static bool ObtainedFuel = false;
        public static bool ObtainedKeycardLevel1 = true;
        public static bool ObtainedKeycardLevel2 = false;
        public static bool ObtainedKeycardLevel3 = false;
        public static bool ObtainedKeyPadCode = false;
        public static bool ObtainedKeyPadWire1 = false;
        public static bool ObtainedKeyPadWire2 = false;
        public static bool ObtainedKeyPadWire3 = false;
        public static bool ObtainedMap = false;
        public static bool ObtainedNoiseMaker = false;
        public static bool PowerTurnedOn = false;
        public static bool RecRoomVisited = false;
        public static bool ShowerRoomGuardKilled = false;
        public static bool SirensPlayed = false;
        public static bool Solitary2GuardKilled = false;
        public static bool WeaponRoomDoorOpened = false;
        public static string CurrentObjective = "";

        //      STORY PUZZLES       //
        public Random KeyPadCombinationRND = new();

        public int[] KeyPadCombination1 = { 7, 9, 1, 7, 5, 4 };
        public int KeyPadCombination2 = 595920;

        public Random InfirmaryFoodChainPuzzle = new();

        public string[] FoodChainPuzzle1 = { "Bear.", "Wolf.", "Snake." };

        //      STORY EVENTS        //
        public static Random ChanceToSneakBeetlesRND = new();
        public static int ChanceToSneakBeetles;
        
        public Random ChanceToSneakGuardRnd = new();
        public int ChanceToSneakGuard;
        public Random ChanceToSneakAlertedGuardRND = new();
        public int ChanceToSneakAlertedGuard;

        public Random CellBlockARND = new();
        public int WireCellBlockA;

        public Random CellBlockBRND = new();
        public int WireCellBlockB;

        public static bool EscapedFromTortureChamber = false;
        public static bool NoiseMakerUsed = false;
        public static bool PlayerLeftSolitary = false;
        public static bool RescuedFriend1 = false;
        public static bool RescuedFriend2 = false;
        public static bool SolitaryVisited = false;
        public static bool ExecutionRoomFriendVisited = false;


        //      POINTS OF INTEREST      //
        public static bool CellA1Opened = false;
        public static bool CellA2Opened = false;
        public static bool CellA3Opened = false;
        public static bool CellA4Opened = false;
        public static bool CellA5Opened = false;
        public static bool CellA6Opened = false;
        public static bool CellB1Opened = false;
        public static bool CellB2Opened = false;
        public static bool CellB3Opened = false;
        public static bool CellB4Opened = false;
        public static bool CellB5Opened = false;
        public static bool CellB6Opened = false;
        public static bool EntranceNoPower = false;
        public static bool EntranceMissingKeyItems = false;
        public static bool GuardsPostBodyChecked = false;
        public static bool GuardsPostVisited = false;
        public static bool LaundryRoomGuardExamined = false;
        public static bool LaundryRoomIllegibleWallTextRead = false;
        public static bool LockedWeaponsRoomDoorChecked = false;
        public static bool ShowerRoomBodyExamined = false;
        public static bool VisitorCenterLeafletObtained = false;
        public static bool WeaponsRoomDoorWindowChecked = false;
        public static bool UnidentifiedPeopleIdentified = false;

        //      PLAYER CHECKS       //
        public bool inSolitary2 = false;
        public static bool PlayerIsMoving = false;
        public static bool PlayerIsQuitting = false;
        public static string PreviousLocation = "";
        public static string CurrentLocation = "";
        public static string NewLocation = "";

        //      PLAYER VARIABLES        //
        public static string PlayerNameFinalised = "";

        //      ENEMY CHECKS        //
        public static bool LaundryRoom1BeetlesDefeated = false;

        //      RANDOM REWARDS      //
        public Random LootableContainerRND = new();
        public int LootableContainerReward;

        //      INSTANTIATE CLASSES     //
        public readonly static PlayerJournal PlayerJournal = new();

        //      SOUND RELATED           //
        //      ALL SOUNDS ARE ROYALTY FREE AND ARE FROM PIXABAY.COM     //
        public static bool PlayBGM = true;
        public static bool PlaySFX = true;
        public static string SoundFileName = "";

        public static void DisclaimerInfo()
        {
            SoundSystem.CheckBGMPlayer();
            SoundSystem.StoryBGMPlayer(SoundSystem.StoryModeBGMFileNames[0], PlayBGM);

            // ElapsedGameTime() method is used to display the elapsed time of the game, in the top right corner of the console window. So we cannot change the Console.Title.
            //Console.Title = "Into the Shadows: Disclaimer";

            AnsiConsole.MarkupLine("[orange1]The game that you are about to play, contains [red]gruesome details[/] that may disturb you.\n\nThis game features background ambience and sound effects to enhance the gameplay experience.\n\nRegardless of this; it is recommended for you to play this game with some headphones, volume up and lights off.[/]\n");

            // We'll make the player be aware of sound options that exists within the game. This enables the player to disable the sounds if they wish to do so.
            AnsiConsole.Write(new Markup("[italic][grey]Sounds can be disabled from the main menu.\n\nWould you like to go back to the main menu?[/][/]\n"));

            ExtFunctions.CreateMenuPrompt(new[] { "Yes.", "No." }, "Choose any option below to continue. Use the Up and Down arrow keys to navigate.", true, false);

            switch (ExtFunctions.ChosenOption) 
            {
                case "Yes.":
                    // CheckBGMPlayer() method is used to check whether the BGMPlayer is currently playing any music, if it is, it will stop the music to prevent an exception from being thrown.
                    SoundSystem.CheckBGMPlayer();
                    // Clean the console, with some fancy effects that can be annoying.
                    ExtFunctions.CleanConsole();
                    MainGame.Main();
                    break;

                case "No.":
                    ExtFunctions.CleanConsole();
                    // Check if the player already has a save data, if so, we'll load the game, else, we'll start a new game.
                    ExtFunctions.CheckForSaveData();
                    Introduction();
                    break;
            }
        }

        public static void Introduction()
        {
            Thread currentTimeDisplayThread = new(ExtFunctions.ElapsedGameTime);
            
            currentTimeDisplayThread.Start();

            // This is to check whether the player has loaded a save data, as to which this variable wouldn't be empty.
            // Of course, I could've used a boolean that returns true if the player has loaded a save data, but I wanted to use a string instead because I'm very smart.
            if (PlayerNameFinalised == "")
            {
                // Ask the player for their name. Doesn't matter if they don't enter a name as AnsiConsole.Ask refuses empty responses.
                var PlayerName = AnsiConsole.Ask<string>("[orange1]What is your [green]name[/]?[/]");

                // PlayerName Finalised will be used to display the player's name in the game as PlayerName is a local variable.
                PlayerNameFinalised = PlayerName;
            }

            AnsiConsole.MarkupLine($"\n[orange1]The story takes place in an apocalyptic dystopian era, where the humanity is near at its' end and the remains of humanity became savages.\n\nEveryone, who survived the atomic bombs that made a fallout and shaped the world into a wasteland, trusts no one, aside from themselves and their family or their closest friends.\n\nYou; {PlayerNameFinalised} were hunting for food and anything worth scavenging outdoors, in the dark, as you and your group were starving. You and your group have not ate any food for countless days.\n\nWhile you were scavenging, you came across a tent in a dead forest, with no signs of savages. You, decided to investigate the camp, until, you got caught in a snare trap, pulling you up to a tree branch.\n\nA few seconds later, you got knocked out by an unidentified person. You woke up in an abandoned building. Floor creaking, ceiling crumbling, and dust surrounding the room. You find yourself tied up, so you have started to analyse the room to see what you can use to cut the rope ties.\n\nAfter a minute, you find a sharp but broken metal railing that you use to cut the ties and attempted to make your way out....[/]");

            // Continue prompt, that tells the player to hit any key to continue. Else, the player may be confused as to what to do next.
            ExtFunctions.ContinuePrompt();

            ExtFunctions.CleanConsole();

            Story_Basement();
        }

        public static void Story_Basement()
        {
            bool inBasement = true;
            bool exploredBasement = false; // Can't let the player cheat with unless number of rewards. :P

            SoundSystem.CheckBGMPlayer();
            SoundSystem.StoryBGMPlayer(SoundSystem.StoryModeBGMFileNames[1], PlayBGM);

            //CurrentObjective = "Get out.";

            CurrentLocation = "Basement";
            ExtFunctions.DisplayCurrentLocation(CurrentLocation);
            ExtFunctions.SaveGame(); // Saves the game.

            while (inBasement)
            {
                AnsiConsole.MarkupLine("[orange1]As you look around the room, you have found a [green]metal rusty pipe[/].[/]\n");

                ExtFunctions.CreateMenuPrompt(new[] { "Look around the room.", "Go up the stairs." }, "What do you want to do?", true, false);

                switch (ExtFunctions.ChosenOption)
                {
                    case "Look around the room.":

                        // Random reward.
                        Random chanceToFindSomethingRND = new();
                        int chanceToFindSomething = chanceToFindSomethingRND.Next(1, 10);

                        if (chanceToFindSomething > 3)
                        {
                            if (exploredBasement == false)
                            {
                                ExtFunctions.RandomRewardFound();
                                exploredBasement = true;
                            }

                            else
                            {
                                // No cheating.
                                AnsiConsole.MarkupLine("[red]You cannot cheat in this game. If you think you can, you've came to the wrong place.[/]");
                                ExtFunctions.ContinuePrompt();
                            }
                        }

                        else if (chanceToFindSomething < 5 && chanceToFindSomething > 3)
                        {
                            if (exploredBasement == false)
                            {
                                AnsiConsole.MarkupLine("[orange1]You have found a [green]banadage[/] lying around![/]\n");

                                Player.Bandages += 1;
                                exploredBasement = true;
                                ExtFunctions.ContinuePrompt();
                            }

                            else
                            {
                                AnsiConsole.MarkupLine("[red]You cannot cheat in this game. If you think you can, you have came to the wrong place.[/]");
                                ExtFunctions.ContinuePrompt();
                            }
                        }

                        else
                        {
                            if (exploredBasement)
                            {
                                AnsiConsole.MarkupLine("[red]You cannot cheat in this game. If you think you can, you have came to the wrong place.[/]");
                                ExtFunctions.ContinuePrompt();
                            }

                            AnsiConsole.MarkupLine("[orange1]You have found nothing.[/]\n");
                            ExtFunctions.ContinuePrompt();
                            goto case "Go up the stairs.";
                        }
                        break;

                    case "Go up the stairs.":
                        // NewLocation and PlayerIsMoving is used by CleanConsole() to determine whether the player is moving to a new location or not, and if so, what is the new location that they've transitioning.
                        NewLocation = "Laundry Room";
                        PlayerIsMoving = true;

                        ExtFunctions.CleanConsole();

                        // Had to instantiate the class, as the method is not static.
                        StoryMode laundryRoom1 = new();
                        laundryRoom1.Story_LaundryRoom1();
                        inBasement = false; // Breaks the loop.
                        break;
                }
            }
        }

        public void Story_LaundryRoom1()
        {
            // Stack of sounds waiting to be played in a thread.
            void laundryRoom1SFXSounds()
            {
                SoundSystem.CheckSFXPlayer();

                // Play a sound first.
                SoundSystem.SFXSoundPlayer(SoundSystem.SFXSoundFileNames[10], PlaySFX);

                // Wait until the sound is finished, approximately 3 seconds long.
                Thread.Sleep(3000);

                SoundSystem.CheckSFXPlayer();

                // Play the second sound.
                SoundSystem.SFXSoundPlayer(SoundSystem.SFXSoundFileNames[9], PlaySFX);
            }

            //CurrentObjective = "Find a way out.";
            CurrentLocation = "Laundry Room";
            ExtFunctions.DisplayCurrentLocation(CurrentLocation);
            ExtFunctions.SaveGame();

            if (LaundryRoomIllegibleWallTextRead == false && GuardsPostVisited == false)
            {
                // Threads are very useful, in both optimisation of code, by utilising multiple threads and executing multiple methods at once.
                // Thread is my favourite. :)
                ThreadStart playLaundryRoom1SFXSounds = new(laundryRoom1SFXSounds);
                Thread playSounds = new(playLaundryRoom1SFXSounds);

                AnsiConsole.MarkupLine("[orange1]As you were walking up the stairs, you heard painful screams and wild animal-like noise. As you get to the top, you briefly see a bloodied man being dragged by a four legged creature, who briefly looked like a human. You see a detached arm with three claw marks, under it, is a pool of blood. You can almost here, someone whispering, but you cannot see who...\n\nThere are washing machines around the room, with clothing tosses everywhere, mostly bloody.\n\nA sign above you, states you are currently in the [cyan]Laundry Room[/]; which you insinuates that this may be some sort of prison.[/]");

                playSounds.Start();

                ExtFunctions.ContinuePrompt();

                // Check to see if the thread isn't alive, if so, we'll check the BGMPlayer and tell it to play another sound.
                if (playSounds.IsAlive != true)
                {
                    SoundSystem.CheckBGMPlayer();
                    SoundSystem.StoryBGMPlayer(SoundSystem.StoryModeBGMFileNames[2], PlayBGM);
                }

                ExtFunctions.CreateMenuPrompt(new[] { "Go to the Guard Post.", "Read the near-illegible bloody writing on the wall." }, "What do you want to do?", true, false);

                switch (ExtFunctions.ChosenOption)
                {
                    case "Go to the Guard Post.":
                        
                        // BGM should be looping, but adding this just in case it does not.
                        SoundSystem.CheckBGMPlayer();

                        NewLocation = "Guard Post";
                        PlayerIsMoving = true;
                        ExtFunctions.CleanConsole();
                        Story_GuardPost1();
                        break;

                    case "Read the near-illegible bloody writing on the wall.":
                        

                        LaundryRoomIllegibleWallTextRead = true;

                        AnsiConsole.MarkupLine("\nThere is an almost illegible writing on the wall, that says:\n[red]There is no escape once you have been trapped in Hell. They are everywhere; killing everyone who has committed sins, who is negative, who is depressed, who is classed as filth by having conditions. Who are They? And where did They come from...[/]\n\n");

                        ExtFunctions.ContinuePrompt();

                        SoundSystem.CheckBGMPlayer();
                        SoundSystem.StoryBGMPlayer(SoundSystem.SFXSoundFileNames[11], PlayBGM);
    
                        AnsiConsole.MarkupLine("[orange1]Out of nowhere, the [red]sirens[/] are going off. You did not know what this means, so, as a precaution, you have went back to the basement...[/]\n");

                        ExtFunctions.ContinuePrompt();


                        PreviousLocation = "Laundry Room 1";
                        NewLocation = "Basement";
                        ExtFunctions.CleanConsole();
                        Story_Basement2();
                        return;
                }
            }

            else if (LaundryRoomIllegibleWallTextRead && SirensPlayed && GuardsPostVisited == false)
            {
                SoundSystem.CheckBGMPlayer();
                SoundSystem.StoryBGMPlayer(SoundSystem.StoryModeBGMFileNames[3], PlayBGM);

                ExtFunctions.DisplayCurrentLocation(CurrentLocation);
                
                AnsiConsole.MarkupLine("[orange1]As you came back up, everything looked very dark. The smell of death is in the air...[/]");
                
                ExtFunctions.ContinuePrompt();

                AnsiConsole.MarkupLine("[orange1]\nAs you took few steps forward, you heard some sort of creature moving and flying. You could not tell what it was as you barely could see what it is...\n\nAs you moved slightly forward, you could see that there were a bunch of large beetles, surrounding a dead body, trying to nibble at it... They do not notice that you are in a close proximity...\n\n[/]");

                ExtFunctions.CreateMenuPrompt(new[] { "Sneak past them to the Guards Post.", "Sneak attack them." }, "What do you want to do?", true, false);

                switch (ExtFunctions.ChosenOption)
                {
                    case "Sneak past them to the Guards Post.":

                        ChanceToSneakBeetles = ChanceToSneakBeetlesRND.Next(1, 10);

                        if (ChanceToSneakBeetles >= 5)
                        {

                            SoundSystem.CheckSFXPlayer();
                            SoundSystem.SFXSoundPlayer(SoundSystem.SFXSoundFileNames[2], PlaySFX);

                            AnsiConsole.MarkupLine("[red]\nThe Beetles were alerted by your loud footsteps! They are coming to attack you![/]");

                            ExtFunctions.ContinuePrompt();

                            CurrentLocation = "Laundry Room 1";

                            BattleSystem.EncounteredEnemy = "Engorged Beetles";
                            BattleSystem.EnemyStatSetup();

                            LaundryRoom1BeetleDefeated();
                        }

                        else
                        {

                            AnsiConsole.MarkupLine("[orange1]\nYou have successfully snuck past the Beetles!\n[/]");
                            
                            ExtFunctions.ContinuePrompt();

                            NewLocation = "Guard's Post";
                            PlayerIsMoving = true;

                            ExtFunctions.CleanConsole();
                            
                            Story_GuardPost1();
                        }
                        break;

                    case "Sneak attack them.":

                        // This bool is used to determine whether the player is sneak attacking the enemy or not. This will be used in the BattleSystem class to call the corresponding method and will deduct a random amount of HP from the enemy.
                        BattleSystem.PlayerSneakAttack = true;
                        
                        // We handle this in BattleSystem.SneakAttack() method.
                        //AnsiConsole.MarkupLine("[green]You have successfully sneak attacked the Beetles and have taken 'damage' HP from them![/]");

                        ExtFunctions.ContinuePrompt();
                        
                        CurrentLocation = "Laundry Room 1";
                        
                        // Initiates the battle.
                        BattleSystem.EncounteredEnemy = "Engorged Beetles";
                        // After calling the method, the BattleSystem class will handle the battle entirely.
                        BattleSystem.EnemyStatSetup();
                        
                        // Got to revert the value of this bool back to false, else the player would be sneak attacking everyone.
                        BattleSystem.PlayerSneakAttack = false;

                        // Jump to this method, as the battle has ended. This will also aid with making the code presentable.
                        LaundryRoom1BeetleDefeated();
                        break;

                        /*  NO MORE JOUNRAL.    //
                    case "View your Journal.":
                        // Should create a while loop in PlayerJournal class, to prevent the player from going back to the previous location.
                        // This requires a loopy loop.
                        ExtFunctions.CleanConsole();
                        PlayerJournal.DisplayJournal();
                        Story_LaundryRoom1();
                        break;
                        */
                }
            }

            // Failsafe check to ensure the game would continue, if the player loads their saved data.
            else if (LaundryRoom1BeetlesDefeated)
            {
                LaundryRoom1BeetleDefeated();
            }

             void LaundryRoom1BeetleDefeated()
             {
                LaundryRoom1BeetlesDefeated = true;

                ExtFunctions.SaveGame();

                SoundSystem.CheckBGMPlayer();
                SoundSystem.StoryBGMPlayer(SoundSystem.StoryModeBGMFileNames[2], PlayBGM);

                AnsiConsole.MarkupLine("[green]\nThe Beetles has been defeated! Now you can move forward!\n[/]");

                ExtFunctions.ContinuePrompt();

                AnsiConsole.MarkupLine("[orange1]As you looked around, there is another area available for you to go. The signs are giving you the direction and says 'Guard's Post' is there. So, that is where you will go now.\n[/]");

                ExtFunctions.ContinuePrompt();

                NewLocation = "Guard's Post";
                PlayerIsMoving = true;
                ExtFunctions.CleanConsole();
                Story_GuardPost1();
             }
        }

        public void Story_Basement2()
        {
            CurrentLocation = "Basement";

            ExtFunctions.DisplayCurrentLocation(CurrentLocation);
            ExtFunctions.SaveGame();

            // Play the sirens, lower the volume of the sirens to mimic distance has been made between the sirens and player.
            SirensPlayed = true;
            SoundSystem.BGMPlayer.Volume = 0.71f;

            AnsiConsole.MarkupLine("[orange1]As you made it back to the basement, the sirens continue to play. You continue to wonder what the sirens mean; a warning? Or possible danger?...[/]");

            ExtFunctions.ContinuePrompt();

            AnsiConsole.MarkupLine("[orange1]As the sirens are being played, unknown darkness is overtaking the walls. Environmentally, making everything look dull, rotten, and damaged. It made everything look like death itself.[/]");

            ExtFunctions.ContinuePrompt();

            // Stops the sirens from being played.
            SoundSystem.CheckBGMPlayer();
            SoundSystem.CheckSFXPlayer();
            SoundSystem.BGMPlayer.Volume = 1f; // Revert the volume back to normal.

            AnsiConsole.MarkupLine("[orange1]The sirens has stopped. Everything sounds very quiet, as if nothing has happened. So, you decided to back up to the Laundry Room...[/]");

            ExtFunctions.ContinuePrompt();

            PlayerIsMoving = true;
            NewLocation = "Laundry Room";
            ExtFunctions.CleanConsole();

            if (PreviousLocation == "Laundry Room 1")
            {
                Story_LaundryRoom1();
            }

            else if (PreviousLocation == "Laundry Room 2")
            {
                Story_LaundryRoom2();
            }
        }

        public void Story_GuardPost1()
        {
            // Again, we don't need to check the BGMPlayer to see if its playing anything, as we're not initiating another sound to be played.

            CurrentLocation = "Guard Post";
            //CurrentObjective = "Look around for clues on how to get out.";

            ExtFunctions.DisplayCurrentLocation(CurrentLocation);
            ExtFunctions.SaveGame();

            AnsiConsole.MarkupLine("[orange1]You are now in The Guard Post. As you thought, there is nobody here, nobody that could help you or explain what is going on or even, how did you wind up in this place.\n\nBut there are [red]dead bodies[/] everywhere. Trails of blood everywhere, but one trail of blood directs your attention, to what is called the [cyan]'Weapons Room'[/], as clearly labelled on top of the door.[/]\n");

            GuardsPostVisited = true;

            ExtFunctions.ContinuePrompt();

            if (ObtainedKeycardLevel3 == false && LockedWeaponsRoomDoorChecked == false && GuardsPostBodyChecked == false && GuardsPostObserved == false)
            {
                ExtFunctions.DisplayCurrentLocation(CurrentLocation);

                ExtFunctions.CreateMenuPrompt(new[] { "Go to the Weapons Room.", "Check the bodies for the keycard.", "Look around the room." }, "What do you want to do?", true, false);

                switch (ExtFunctions.ChosenOption)
                {
                    case "Go to the Weapons Room.":
                        // Plays the beep beep sound.
                        SoundSystem.CheckSFXPlayer();
                        SoundSystem.SFXSoundPlayer(SoundSystem.SFXSoundFileNames[3], PlaySFX);

                        AnsiConsole.MarkupLine("\n[orange1]The door to the Weapons Room is locked. There is a card reader next by the door that beeps and displays [bold][cyan]'Level 3 Clearance Keycard Required'[/][/] when you attempt to open the door.\nYou will need the [bold][cyan]Level 3 Clearance Keycard[/][/] to get through. As you sigh in frustration, you realise that the bottom of the door has skull bone fragments, indicating someone's skull has practically exploded when the door was forced closed onto their skull. The thought horrifies you.[/]");

                        LockedWeaponsRoomDoorChecked = true;

                        ExtFunctions.ContinuePrompt();

                        // Increase FEAR.
                        Player.PlayerFear += 10;

                        ExtFunctions.CleanConsole();

                        Story_GuardPost1();
                        
                        break;

                    case "Check the bodies for the keycard.":
                        // Random reward.
                        GuardsPostBodyChecked = true;

                        AnsiConsole.MarkupLine("\n[orange1]There were no [cyan]keycard[/] on any of the bodies, however...[/]");

                        ExtFunctions.RandomRewardFound();

                        Story_GuardPost1();
                        break;

                    case "Look around the room.":
                        GuardsPostSwitchPulled = true;

                        AnsiConsole.MarkupLine("\n[orange1]As you were looking around the room, you faintly see a [cyan]switch[/], so you decided to pull the switch to see what it will do.[/]");

                        // Play sound.
                        SoundSystem.CheckSFXPlayer();
                        SoundSystem.SFXSoundPlayer(SoundSystem.SFXSoundFileNames[13], PlaySFX);

                        AnsiConsole.MarkupLine("[orange1]Sounds like a door has opened in the [cyan]Laundry Room...[/][/]\n");

                        ExtFunctions.CreateMenuPrompt(new[] { "Yes.", "No." }, "Would you like to go to the Laundry Room to check out what has opened?", true, false);

                        switch (ExtFunctions.ChosenOption)
                        {
                            case "Yes.":
                                NewLocation = "Laundry Room";
                                PlayerIsMoving = true;
                                ExtFunctions.CleanConsole();
                                Story_LaundryRoom2();
                                break;

                            case "No.":
                                ExtFunctions.CleanConsole();
                                Story_GuardPost1();
                                break;
                        }
                        break;
                }
            }

            else if (ObtainedKeycardLevel3 == false && LockedWeaponsRoomDoorChecked == false && GuardsPostBodyChecked && GuardsPostObserved == false)
            {
                ExtFunctions.DisplayCurrentLocation(CurrentLocation);

                ExtFunctions.CreateMenuPrompt(new[] { "Go to the Weapons Room.", "Look around the room." }, "What do you want to do?", true, false);

                switch (ExtFunctions.ChosenOption)
                {
                    case "Go to the Weapons Room.":

                        // Plays beep sound.
                        SoundSystem.CheckSFXPlayer();
                        SoundSystem.SFXSoundPlayer(SoundSystem.SFXSoundFileNames[3], PlaySFX);

                        AnsiConsole.MarkupLine("\n[orange1]The door to the Weapons Room is locked. There is a card reader next by the door that beeps and displays [bold][cyan]'Level 3 Clearance Keycard Required'[/][/] when you attempt to open the door.\nYou will need the [bold][cyan]Level 3 Clearance Keycard[/][/] to get through. As you sigh in frustration, you realise that the bottom of the door has skull bone fragments, indicating someone's skull has practically exploded when the door was forced closed onto their skull. The thought horrifies you.[/]");

                        LockedWeaponsRoomDoorChecked = true;

                        // Increase FEAR.
                        Player.PlayerFear += 10;

                        ExtFunctions.ContinuePrompt();
                        ExtFunctions.CleanConsole();

                        Story_GuardPost1();
                        break;


                    case "Look around the room.":

                        GuardsPostSwitchPulled = true;

                        AnsiConsole.MarkupLine("\n[orange1]As you were looking around the room, you faintly see a [cyan]switch[/], so you decided to pull the switch to see what it will do.[/]");

                        // Play sound.
                        SoundSystem.CheckSFXPlayer();
                        SoundSystem.SFXSoundPlayer(SoundSystem.SFXSoundFileNames[13], PlaySFX);

                        AnsiConsole.MarkupLine("[orange1]Sounds like a door has opened in the [cyan]Laundry Room...[/]\nWould you like to go to the Laundry Room to check out what has opened?[/]");

                        ExtFunctions.CreateMenuPrompt(new[] { "Yes.", "No." }, "Choose any options below to continue.", true, false);

                        switch (ExtFunctions.ChosenOption)
                        {
                            case "Yes.":
                                NewLocation = "Laundry Room";
                                PlayerIsMoving = true;
                                ExtFunctions.CleanConsole();
                                Story_LaundryRoom2();
                                break;

                            case "No.":
                                ExtFunctions.CleanConsole();
                                Story_GuardPost1();
                                break;
                        }
                        break;
                }
            }

            else if (ObtainedKeycardLevel3 == false && LockedWeaponsRoomDoorChecked && WeaponsRoomDoorWindowChecked == false && GuardsPostBodyChecked == false && GuardsPostObserved == false)
            {
                ExtFunctions.CreateMenuPrompt(new[] { "Peep through the Weapons Room Door Window.", "Check the bodies for the keycard.", "Look around the room." }, "Choose any option below to continue.", true, false);

                switch (ExtFunctions.ChosenOption)
                {
                    case "Peep through the Weapons Room Door Window.":

                        AnsiConsole.MarkupLine("[orange1]The light inside the room is flickering, so you cannot see what is inside the room. But there seem to be a [cyan]wire[/] on a table.[/]");

                        WeaponsRoomDoorWindowChecked = true;

                        ExtFunctions.ContinuePrompt();
                        ExtFunctions.CleanConsole();

                        Story_GuardPost1();
                        break;

                    case "Check the bodies for the keycard.":
                        // Random reward.

                        AnsiConsole.MarkupLine("\n[orange1]There were no keycard on any of the bodies, however...[/]");
                        
                        ExtFunctions.RandomRewardFound();
                        
                        GuardsPostBodyChecked = true;

                        Console.ReadKey();

                        Story_GuardPost1();
                        break;

                    case "Look around the room.":
                        GuardsPostSwitchPulled = true;

                        AnsiConsole.MarkupLine("[orange1]\nAs you were looking around the room, you faintly see a [cyan]switch[/], so you decided to pull the switch to see what it will do.[/]");
                                                
                        // Play sound.
                        SoundSystem.CheckSFXPlayer();
                        SoundSystem.SFXSoundPlayer(SoundSystem.SFXSoundFileNames[13], PlaySFX);

                        ExtFunctions.ContinuePrompt();

                        AnsiConsole.MarkupLine("[orange1]Sounds like a door has opened in the Laundry Room...\nWould you like to go to the Laundry Room to check out what has opened?[/]");

                        ExtFunctions.CreateMenuPrompt(new[] { "Yes.", "No." }, "Choose any option below to continue.", true, false);

                        switch (ExtFunctions.ChosenOption)
                        {
                            case "Yes.":
                                NewLocation = "Laundry Room";
                                PlayerIsMoving = true;
                                ExtFunctions.CleanConsole();
                                Story_LaundryRoom2();
                                break;

                            case "No.":
                                ExtFunctions.CleanConsole();
                                Story_GuardPost1();
                                break;
                        }
                        break;
                }

            }

            else if (ObtainedKeycardLevel3 == false && LockedWeaponsRoomDoorChecked && WeaponsRoomDoorWindowChecked && GuardsPostBodyChecked == false && GuardsPostObserved == false)
            {
                ExtFunctions.CreateMenuPrompt(new[] { "Check the bodies for the keycard.", "Look around the room." }, "Choose any option below to continue.", true, false);

                switch (ExtFunctions.ChosenOption)
                {
                    case "Check the bodies for the keycard.":
                        // Random reward.

                        AnsiConsole.MarkupLine("\n[orange1]There were no keycard on any of the bodies, however...[/]");

                        ExtFunctions.RandomRewardFound();

                        GuardsPostBodyChecked = true;

                        Story_GuardPost1();
                        break;

                    case "Look around the room.":
                        GuardsPostSwitchPulled = true;

                        AnsiConsole.MarkupLine("[orange1]\nAs you were looking around the room, you faintly see a [cyan]switch[/], so you decided to pull the switch to see what it will do.[/]");

                        // Play sound.
                        SoundSystem.CheckSFXPlayer();
                        SoundSystem.SFXSoundPlayer(SoundSystem.SFXSoundFileNames[13], PlaySFX);

                        ExtFunctions.ContinuePrompt();

                        AnsiConsole.MarkupLine("[orange1]Sounds like a door has opened in the Laundry Room...\nWould you like to go to the Laundry Room to check out what has opened?[/]");

                        ExtFunctions.CreateMenuPrompt(new[] { "Yes.", "No." }, "Choose any option below to continue.", true, false);

                        switch (ExtFunctions.ChosenOption)
                        {
                            case "Yes.":
                                PreviousLocation = "Guard's Post";
                                NewLocation = "Laundry Room";
                                PlayerIsMoving = true;
                                ExtFunctions.CleanConsole();
                                Story_LaundryRoom2();
                                break;

                            case "No.":
                                ExtFunctions.CleanConsole();
                                Story_GuardPost1();
                                break;
                        }
                        break;
                }
            }

            else if (ObtainedKeycardLevel3 == false && LockedWeaponsRoomDoorChecked && WeaponsRoomDoorWindowChecked == false && GuardsPostBodyChecked && GuardsPostObserved == false)
            {
                ExtFunctions.CreateMenuPrompt(new[] { "Peep through the Weapons Room Door Window.", "Look around the room." }, "Choose any option below to continue.", true, false);

                switch (ExtFunctions.ChosenOption)
                {
                    case "Peep through the Weapons Room Door Window.":
                        AnsiConsole.Write(new Markup("\n[orange1]There seems to be empty ammo boxes on the floor, and from the looks of it; a handgun. However, you cannot be sure, as the light is rapidly flickering.[/]"));

                        WeaponsRoomDoorWindowChecked = true;

                        ExtFunctions.ContinuePrompt();
                        ExtFunctions.CleanConsole();

                        Story_GuardPost1();
                        break;

                    case "Look around the room.":
                        GuardsPostSwitchPulled = true;

                        AnsiConsole.MarkupLine("[orange1]\nAs you were looking around the room, you faintly see a [cyan]switch[/], so you decided to pull the switch to see what it will do.[/]");

                        // Play sound.
                        SoundSystem.CheckSFXPlayer();
                        SoundSystem.SFXSoundPlayer(SoundSystem.SFXSoundFileNames[13], PlaySFX);

                        ExtFunctions.ContinuePrompt();

                        AnsiConsole.MarkupLine("[orange1]Sounds like a door has opened in the Laundry Room...\nWould you like to go to the Laundry Room to check out what has opened?[/]");

                        ExtFunctions.CreateMenuPrompt(new[] { "Yes.", "No." }, "Choose any option below to continue.", true, false);

                        switch (ExtFunctions.ChosenOption)
                        {
                            case "Yes.":
                                NewLocation = "Laundry Room";
                                PlayerIsMoving = true;
                                ExtFunctions.CleanConsole();
                                Story_LaundryRoom2();
                                break;

                            case "No.":
                                ExtFunctions.CleanConsole();
                                Story_GuardPost1();
                                break;
                        }
                        break;
                }

            }

            else if (ObtainedKeycardLevel3 == false && LockedWeaponsRoomDoorChecked && WeaponsRoomDoorWindowChecked == false && GuardsPostBodyChecked == false && GuardsPostObserved)
            {
                ExtFunctions.CreateMenuPrompt(new[] { "Peep through the Weapons Room Door Window.", "Check the bodies for the keycard.", "Look around the room." }, "Choose any option below to continue.", true, false);

                switch (ExtFunctions.ChosenOption)
                {
                    case "Peep through the Weapons Room Door Window.":
                        AnsiConsole.MarkupLine("\n[orange1]There seems to be empty ammo boxes on the floor, and from the looks of it; a handgun. However, you cannot be sure, as the light is rapidly flickering.[/]");

                        WeaponsRoomDoorWindowChecked = true;

                        ExtFunctions.ContinuePrompt();
                        ExtFunctions.CleanConsole();

                        Story_GuardPost1();
                        break;

                    case "Check the bodies for the keycard.":
                        // Random reward.

                        AnsiConsole.MarkupLine("\nThere were no keycard on any of the bodies, however...");
                        ExtFunctions.RandomRewardFound();
                        GuardsPostBodyChecked = true;

                        Story_GuardPost1();
                        break;
                }

            }

            else if (ObtainedKeycardLevel3 == false && LockedWeaponsRoomDoorChecked && WeaponsRoomDoorWindowChecked && GuardsPostBodyChecked && GuardsPostObserved == false)
            {
                ExtFunctions.CreateMenuPrompt(new[] { "Look around the room." }, "Choose any option below to continue.", true, false);

                if (ExtFunctions.ChosenOption == "Look around the room.")
                {
                    GuardsPostSwitchPulled = true;

                    AnsiConsole.MarkupLine("\n[orange1]As you were looking around the room, you faintly see a [cyan]switch[/], so you decided to pull the switch to see what it will do.[/]");

                    // Play sound.
                    SoundSystem.CheckSFXPlayer();
                    SoundSystem.SFXSoundPlayer(SoundSystem.SFXSoundFileNames[13], PlaySFX);

                    ExtFunctions.ContinuePrompt();

                    AnsiConsole.MarkupLine("[orange1]Sounds like a door has opened in the Laundry Room...\nWould you like to go to the Laundry Room to check out what has opened?[/]");

                    ExtFunctions.CreateMenuPrompt(new[] { "Yes.", "No." }, "Choose any option below to continue.", true, false);

                    switch (ExtFunctions.ChosenOption)
                    {
                        case "Yes.":
                            NewLocation = "Laundry Room";
                            PlayerIsMoving = true;
                            ExtFunctions.CleanConsole();
                            Story_LaundryRoom2();
                            break;

                        case "No.":
                            ExtFunctions.CleanConsole();
                            Story_GuardPost1();
                            break;
                    }
                }
            }

            else if (ObtainedKeycardLevel3 == false && LockedWeaponsRoomDoorChecked && WeaponsRoomDoorWindowChecked && GuardsPostBodyChecked && GuardsPostObserved)
            {
                AnsiConsole.WriteLine("[orange1]There is nothing else for you to do in this room! Therefore, you make your way to the Laundry Room.[/]");

                ExtFunctions.ContinuePrompt();

                PreviousLocation = "Guard's Post";
                NewLocation = "Laundry Room";
                PlayerIsMoving = true;
                ExtFunctions.CleanConsole();
                Story_LaundryRoom2();
            }
        }

        public void Story_LaundryRoom2()
        {
            CurrentLocation = "Laundry Room";

            ExtFunctions.DisplayCurrentLocation(CurrentLocation);
            ExtFunctions.SaveGame();

            /*
            if (GuardsPostSwitchPulled == true)
            {
                CurrentObjective = "Figure out where to go...";
            }

            else
            {
                CurrentObjective = "Find a way out.";
            }
            */

            if (LaundryRoomIllegibleWallTextRead == false && GuardsPostVisited && SirensPlayed == false)
            {
                // Plays the sirens
                SoundSystem.CheckSFXPlayer();
                SoundSystem.SFXSoundPlayer(SoundSystem.SFXSoundFileNames[11], PlaySFX);

                AnsiConsole.MarkupLine("[orange1]\nAs you have returned from the Guard's Post, the [red]sirens[/] are going off. You did not know what this means, so, as a precaution, you went back to the basement...[/]");

                NewLocation = "Basement";
                PreviousLocation = "Laundry Room 2";
                PlayerIsMoving = true;
                ExtFunctions.CleanConsole();
                Story_Basement2();
            }

            else if (GuardsPostVisited && SirensPlayed && LaundryRoom1BeetlesDefeated == false)
            {
                SoundSystem.CheckBGMPlayer();
                SoundSystem.StoryBGMPlayer(SoundSystem.StoryModeBGMFileNames[3], PlayBGM);

                CurrentObjective = "Decide what to do about the Beetles...";
                AnsiConsole.MarkupLine("[orange1]\nAs you came back up, everything looked very dark. The smell of death is in the air...[/]");
                ExtFunctions.ContinuePrompt();

                AnsiConsole.MarkupLine("[orange1]\nAs you took few steps forward, you heard some sort of creature moving and flying. You could not tell what it was as you barely could see what it is...\nAs you moved slightly forward, you could see that there were a bunch of large beetles, surrounding a dead body, trying to nibble at it... They do not notice that you are in a close proximity...[/]");

                ExtFunctions.CreateMenuPrompt(new[] { "Sneak past them to the Guards Post.", "Sneak past them to the Visitor Centre.", "Sneak attack them." }, "What do you want to do?", true, false);

                switch (ExtFunctions.ChosenOption)
                {
                    case "Sneak past them to the Guards Post.":
                        ChanceToSneakBeetles = ChanceToSneakBeetlesRND.Next(1, 10);

                        if (ChanceToSneakBeetles >= 5)
                        {
                            AnsiConsole.MarkupLine("[red]\nThe Beetles were alerted from your loud footsteps! They are determined to attack you![/]");

                            SoundSystem.CheckSFXPlayer();
                            SoundSystem.SFXSoundPlayer(SoundSystem.SFXSoundFileNames[2], PlaySFX);

                            ExtFunctions.ContinuePrompt();
                            // Initiate fight with the beetles
                            BattleSystem.EncounteredEnemy = "Engorged Beetles";
                            BattleSystem.EnemyStatSetup();

                            LaundryRoom1BeetlesDefeated = true;

                            laundryRoomBeetlesDefeated();
                        }

                        else
                        {
                            AnsiConsole.MarkupLine("[orange1]\nYou have successfully snuck past the Beetles![/]\n");
                            ExtFunctions.ContinuePrompt();

                            NewLocation = "Canteen";
                            PlayerIsMoving = true;
                            ExtFunctions.CleanConsole();
                            Story_Canteen();
                        }
                        break;

                    case "Sneak past them to the Visitor Centre.":
                        ChanceToSneakBeetles = ChanceToSneakBeetlesRND.Next(1, 10);

                        if (ChanceToSneakBeetles >= 5)
                        {
                            AnsiConsole.MarkupLine("[red]\nThe Beetles were alerted from your loud footsteps! They are determined to attack you![/]");

                            SoundSystem.CheckSFXPlayer();
                            SoundSystem.SFXSoundPlayer(SoundSystem.SFXSoundFileNames[2], PlaySFX);

                            ExtFunctions.ContinuePrompt();

                            // Initiate fight with the beetles
                            BattleSystem.EncounteredEnemy = "Engorged Beetles";
                            BattleSystem.EnemyStatSetup();

                            LaundryRoom1BeetlesDefeated = true;
                            laundryRoomBeetlesDefeated();


                        }

                        else
                        {
                            AnsiConsole.MarkupLine("[orange1]\nYou have successfully sneaked past the Beetles![/]\n");
                            ExtFunctions.ContinuePrompt();

                            NewLocation = "The Visitor Centre";
                            PlayerIsMoving = true;
                            ExtFunctions.CleanConsole();
                            Story_VisitorCentre();
                        }

                        break;

                    case "Sneak attack them.":
                        // Reduce the enemy's health by a random amount.

                        //AnsiConsole.MarkupLine("[green]\nYou have successfully sneak attacked the Beetles and have taken 'damage' HP from them![/]");

                        // Initiates battle.
                        BattleSystem.PlayerSneakAttack = true;
                        BattleSystem.EncounteredEnemy = "Engorged Beetles";
                        BattleSystem.EnemyStatSetup();

                        LaundryRoom1BeetlesDefeated = true;

                        laundryRoomBeetlesDefeated();

                        break;
                }
            }

            else if (LaundryRoom1BeetlesDefeated)
            {
                laundryRoomBeetlesDefeated();
            }

            void laundryRoomBeetlesDefeated()
            {
                if (LaundryRoomIllegibleWallTextRead == false && LaundryRoom1BeetlesDefeated && GuardsPostVisited)
                {
                    //AnsiConsole.MarkupLine("[green]The Beetles has been defeated![/]");

                    //ExtFunctions.ContinuePrompt();

                    SoundSystem.CheckSFXPlayer();
                    SoundSystem.SFXSoundPlayer(SoundSystem.SFXSoundFileNames[12], PlaySFX);

                    AnsiConsole.MarkupLine("[orange1]\nYou hear something growl, while producing an irritating and sensitive sound, as if someone is [red]scratching a metal fence with nails.[/]\nIt also seems like the large doors leading to the Canteen and Visitor Centre has been opened.[/]");

                    ExtFunctions.ContinuePrompt();

                    ExtFunctions.CreateMenuPrompt(new[] { "Read the near-illegible bloody writing on the wall.", "Go to the Canteen.", "Go to the Visitor Centre." }, "What do you want to do?", true, false);

                    switch (ExtFunctions.ChosenOption)
                    {
                        case "Read the near-illegible bloody writing on the wall.":

                            LaundryRoomIllegibleWallTextRead = true;
                            AnsiConsole.MarkupLine("\n[orange1]There is an almost illegible writing on the wall, that says:\n[red]There is no escape once you have been trapped in Hell. They are everywhere; killing everyone who has committed sins, who is negative, who is depressed, who is classed as filth by having conditions. Who are They? And where did They come from...[/][/]\n\n");

                            ExtFunctions.ContinuePrompt();

                            /*
                            if (JournalDiscovered == false)
                            {
                                AnsiConsole.MarkupLine("[orange1]You have now unlocked access to the [cyan]Journal[/]. This will allow you to see clues, quotes, and thoughts, all in one place.[/]");
                            }

                            else
                            {
                                AnsiConsole.MarkupLine("[orange1]The writing on your wall, has been added to your [cyan]Journal[/].[/]");
                            }
                            */

                            Story_LaundryRoom2();
                            break;

                        case "Go to the Canteen.":
                            NewLocation = "Canteen";
                            PlayerIsMoving = true;
                            ExtFunctions.CleanConsole();
                            Story_Canteen();
                            break;

                        case "Go to the Visitor Centre.":
                            NewLocation = "The Visitor Centre";
                            PlayerIsMoving = true;
                            ExtFunctions.CleanConsole();
                            Story_VisitorCentre();
                            break;
                    }
                }

                else if (LaundryRoomIllegibleWallTextRead && LaundryRoom1BeetlesDefeated && GuardsPostVisited && LaundryRoom1BeetlesDefeated && LaundryRoomMonsterNoiseMade == false)
                {
                    //AnsiConsole.MarkupLine("[green]The Beetles has been defeated![/]");

                    //ExtFunctions.ContinuePrompt();

                    SoundSystem.CheckSFXPlayer();
                    SoundSystem.SFXSoundPlayer(SoundSystem.SFXSoundFileNames[12], PlaySFX);

                    AnsiConsole.MarkupLine("[orange1]\nYou hear something growl, while producing an irritating and sensitive sound, as if someone is [red]scratching a metal fence with nails.[/]\nIt also seems like the large doors leading to the Canteen and Visitor Centre has been opened.[/]");

                    ExtFunctions.ContinuePrompt();

                    ExtFunctions.CreateMenuPrompt(new[] { "Go to the Canteen.", "Go to the Visitor Centre." }, "What do you want to do?", true, false);

                    switch (ExtFunctions.ChosenOption)
                    {
                        case "Go to the Canteen.":
                            NewLocation = "Canteen";
                            PlayerIsMoving = true;
                            ExtFunctions.CleanConsole();
                            Story_Canteen();
                            break;

                        case "Go to the Visitor Centre.":
                            NewLocation = "The Visitor Centre";
                            PlayerIsMoving = true;
                            ExtFunctions.CleanConsole();
                            Story_VisitorCentre();
                            break;
                    }
                }

                else if (CanteenOfficerMet)
                {
                    ExtFunctions.CreateMenuPrompt(new[] { "Go to the Canteen.", "Go to the Visitor Centre." }, "What do you want to do?", true, false);
                }
                else
                {
                    AnsiConsole.MarkupLine("[orange1]You are in the [cyan]Laundry Room[/] again, and it seems like the large doors leading to the [cyan]Canteen[/] and [cyan]Visitor Centre[/] has been opened.[/]");

                    ExtFunctions.ContinuePrompt();

                    ExtFunctions.CreateMenuPrompt(new[] { "Go to the Canteen.", "Go to the Visitor Centre." }, "What do you want to do?", true, false);

                    switch (ExtFunctions.ChosenOption)
                    {
                        case "Go to the Canteen.":
                            PlayerIsMoving = true;
                            NewLocation = "Canteen";
                            ExtFunctions.CleanConsole();
                            Story_Canteen();
                            break;

                        case "Go to the Visitor Centre.":
                            PlayerIsMoving = true;
                            NewLocation = "Visitor Centre";
                            ExtFunctions.CleanConsole();
                            Story_VisitorCentre();
                            break;
                    }
                }
            }
        }

        public void Story_Canteen()
        {
            static void canteen_PlaySounds()
            {
                SoundSystem.CheckSFXPlayer();
                SoundSystem.SFXSoundPlayer(SoundSystem.SFXSoundFileNames[14], PlaySFX);

                // We wait until the sound has finished playing.
                Thread.Sleep(4550);

                SoundSystem.CheckSFXPlayer();
                SoundSystem.SFXSoundPlayer(SoundSystem.SFXSoundFileNames[15], PlaySFX);
            }

            ThreadStart threadStartPlaySounds = new ThreadStart(canteen_PlaySounds);
            Thread queueSounds = new(threadStartPlaySounds);

            CurrentLocation = "Canteen";
            //CurrentObjective = "Investigate the noise";

            ExtFunctions.DisplayCurrentLocation(CurrentLocation);
            ExtFunctions.SaveGame();

            SoundSystem.CheckBGMPlayer();
            SoundSystem.StoryBGMPlayer(SoundSystem.StoryModeBGMFileNames[10], PlayBGM);

            AnsiConsole.MarkupLine("[orange1]You have entered the Canteen, but you hear someone calling for help...[/]\n");

            ExtFunctions.CreateHeading("Start of dialogue.", true);

            AnsiConsole.Write(new Markup("[italic][gray]As each character's voice lines are printed out, hit any key to continue.[/][/]\n").Centered());

            CanteenOfficerMet = true;

            AnsiConsole.Write(new Markup("[bold][red]> ???:[/][/] Help...\n"));

            Console.ReadKey();

            AnsiConsole.Write(new Markup($"[bold][red]> {PlayerNameFinalised}:[/][/] What the hell has happened to you?\n"));

            Console.ReadKey();

            AnsiConsole.Write(new Markup("[bold][red]> ???:[/][/] *wheezes* Have they seen you?\n"));

            Console.ReadKey();

            AnsiConsole.Write(new Markup($"[bold][red]> {PlayerNameFinalised}:[/][/] No, who are you referring to? Who are you?!\n"));

            Console.ReadKey();

            AnsiConsole.Write(new Markup("[bold][red]> Officer:[/][/] There is a... cult in the Prison. They... they trapped me with these monsters, who did this to me...\nI was an officer, stationed at this prison...\n"));

            Console.ReadKey();

            AnsiConsole.Write(new Markup($"[bold][red]> {PlayerNameFinalised}:[/][/] Is there any way to get out of here?\n"));

            Console.ReadKey();

            AnsiConsole.Write(new Markup("[bold][red]> Officer:[/][/] Entrance... code... [cyan]seven... nine one seven five four....[/]\n"));

            ObtainedKeyPadCode = true;

            Console.ReadKey();

            // Plays sound

            queueSounds.Start();

            AnsiConsole.MarkupLine("[orange1]A [red]monster[/] came out of nowhere and has killed the officer!\n\nYou will need to eliminate the monster to continue![/]");

            // -> Enemy unexpectedly, sneaks up behind and kills the officer, the Level 1 Clearance Key card drops on the floor but requires player to defeat the enemy.
            // Battle needs to be initiated here.

            ExtFunctions.ContinuePrompt();

            BattleSystem.EncounteredEnemy = "Slitherman";

            BattleSystem.EnemyStatSetup();

            // Battle ends.

            ObtainedKeycardLevel1 = true;

            CanteenOfficerMet = true;

            AnsiConsole.MarkupLine("\n[orange1]Upon defeating the monster, you can see the officer has dropped the [cyan]Level 1 Clearance Keycard[/], so you have picked it up.[/]\n\n");

            ExtFunctions.ContinuePrompt();

            if (PreviousLocation == "Laundry Room 3")
            {
                ExtFunctions.CreateMenuPrompt(new[] { "Go back to the Laundry Room.", "Go to the Visitor Centre." }, "What do you want to do?", true, false);

                switch (ExtFunctions.ChosenOption)
                {
                    case "Go back to the Laundry Room.":
                        LaundryRoom3CanteenVisited = true;
                        PlayerIsMoving = true;
                        NewLocation = "Laundry Room";
                        ExtFunctions.CleanConsole();
                        Story_LaundryRoom3();
                        break;
                }
            }

            AnsiConsole.WriteLine();

            // Subsituted the menu prompt with a simple automated continue action, as the menu prompt freaks out when there only a single option available.
            AnsiConsole.MarkupLine("[orange1]There is nothing else to do in the Canteen, so you decided to go to the Visitor Centre.[/]");

            ExtFunctions.ContinuePrompt();

            PlayerIsMoving = true;
            NewLocation = "Visitor Centre";
            ExtFunctions.CleanConsole();
            Story_VisitorCentre();
        }

        public void Story_VisitorCentre()
        {
            SoundSystem.CheckBGMPlayer();
            SoundSystem.StoryBGMPlayer(SoundSystem.StoryModeBGMFileNames[3], PlayBGM);

            CurrentLocation = "Visitor Centre";
            ExtFunctions.DisplayCurrentLocation(CurrentLocation);
            ExtFunctions.SaveGame();

            if (EscapedFromTortureChamber)
            {
                AnsiConsole.MarkupLine("[orange1]\nYou are back in the [cyan]Visitor Centre[/]. As there is not anything else to do here, you will move towards the Entrance...[/]");

                ExtFunctions.ContinuePrompt();

                NewLocation = "Entrance";
                PlayerIsMoving = true;
                ExtFunctions.CleanConsole();
                Story_Entrance2();
            }

            AnsiConsole.MarkupLine("[orange1]\nThere does not seem to be much to do in the Visitor Centre... However, there is a sign: [cyan]'Entrance ->'[/].[/]\n");

            ExtFunctions.ContinuePrompt();

            if (VisitorCenterLeafletObtained == false)
            {
                ExtFunctions.CreateMenuPrompt(new[] { "Follow the sign to the Entrance.", "Look around the Visitor Centre.", "Go back to the Laundry Room." }, "What do you want to do?", true, false);

                switch (ExtFunctions.ChosenOption)
                {
                    case "Follow the sign to the Entrance.":
                        NewLocation = "Entrance";
                        PlayerIsMoving = true;
                        ExtFunctions.CleanConsole();
                        Story_Entrance1();
                        break;

                    case "Look around the Visitor Centre.":
                        
                        VisitorCenterLeafletObtained = true;

                        AnsiConsole.MarkupLine("[orange1]On a table, you have found a leaflet. The contents of the leaflet is, as follows:[/]\n");
                        AnsiConsole.Write(new Markup("[red][underline]Our Objective[/]\n----------------\nWhile there are those beings are out there, filled with sin, filth, and disgust, we will continue to see God punishing us.\nWe must do anything and everything that we can, to purge the world from sin and recruit anyone who is a believer of our faith.\n\nWe need to hurry, else, there will be more monsters lurking around...[/]").Centered());
                        AnsiConsole.Write(new Markup("\n\n\n[red]- Cult Leader.[/]\n").LeftJustified());

                        ExtFunctions.ContinuePrompt();

                        Story_VisitorCentre();

                        break;

                    case "Go back to the Laundry Room.":

                        NewLocation = "Laundry Room";
                        PlayerIsMoving = true;
                        ExtFunctions.CleanConsole();
                        Story_LaundryRoom2();

                        break;
                }
            }

            else
            {
                ExtFunctions.CreateMenuPrompt(new[] { "Follow the sign to the Entrance.", "Go back to the Laundry Room." }, "What do you want to do?", true, false);

                switch (ExtFunctions.ChosenOption)
                {
                    case "Follow the sign to the Entrance.":
                        NewLocation = "Entrance";
                        PlayerIsMoving = true;
                        ExtFunctions.CleanConsole();
                        Story_Entrance1();
                        break;

                    case "Go back to the Laundry Room.":
                        NewLocation = "Laundry Room";
                        PlayerIsMoving = true;
                        ExtFunctions.CleanConsole();
                        Story_LaundryRoom2();
                        break;
                }
            }
        }

        public void Story_Entrance1()
        {
            SoundSystem.CheckBGMPlayer();
            SoundSystem.StoryBGMPlayer(SoundSystem.StoryModeBGMFileNames[3], PlayBGM);

            CurrentLocation = "Entrance";
            ExtFunctions.DisplayCurrentLocation(CurrentLocation);
            ExtFunctions.SaveGame();

            AnsiConsole.MarkupLine("[orange1]\nThe exit is up ahead, but it is blocked off by a shutter; there seem to be some sort of advanced security system in place when the visiting hours are up.\n\nSo far, the security system needs a source of [cyan]power[/].[/]\n");

            EntranceVisited = true;
            EntranceNoPower = true;

            ExtFunctions.ContinuePrompt();

            AnsiConsole.MarkupLine("[orange1]There is nothing else to do, so, you need to head on back...[/]");

            ExtFunctions.ContinuePrompt();

            PlayerIsMoving = true;
            NewLocation = "Laundry Room";
            ExtFunctions.CleanConsole();
            Story_LaundryRoom3();
        }

        public void Story_LaundryRoom3()
        {
            SoundSystem.CheckBGMPlayer();
            SoundSystem.StoryBGMPlayer(SoundSystem.StoryModeBGMFileNames[4], PlayBGM);

            CurrentLocation = "Laundry Room";
            ExtFunctions.DisplayCurrentLocation(CurrentLocation);
            ExtFunctions.SaveGame();

            if (EscapedFromTortureChamber)
            {
                AnsiConsole.MarkupLine("[orange1]\nYou are back in the Laundry Room.[/]");

                ExtFunctions.ContinuePrompt();

                ExtFunctions.CreateMenuPrompt(new[] { "Go to the Visitor Centre.", "Go to the Guard's Post.", "Go to the Corridor." }, "What do you want to do?", true, false);

                switch (ExtFunctions.ChosenOption)
                {
                    case "Go to the Visitor Centre.":
                        NewLocation = "Visitor Centre";
                        PlayerIsMoving = true;
                        ExtFunctions.CleanConsole();
                        Story_VisitorCentre();
                        break;

                    case "Go to the Guard's Post.":
                        NewLocation = "Guard's Post";
                        PlayerIsMoving = true;
                        ExtFunctions.CleanConsole();
                        Story_GuardPost1();
                        break;

                    case "Go to the Corridor.":
                        NewLocation = "Corridor";
                        PlayerIsMoving = true;
                        ExtFunctions.CleanConsole();
                        Story_Corridor1();
                        break;
                }
            }

            if (EscapedFromTortureChamber == false && LaundryRoom3CanteenVisited)
            {
                AnsiConsole.MarkupLine("[orange1]\nYou are back in the Laundry Room. As there is nothing else left to do here, you went onwards to the Corridor, to see what has opened...[/]");

                ExtFunctions.ContinuePrompt();

                NewLocation = "Corridor";
                PlayerIsMoving = true;
                ExtFunctions.CleanConsole();
                Story_Corridor1();
            }

            AnsiConsole.MarkupLine("[orange1]\nAs you were making your way back to the [cyan]Laundry Room[/], you have heard someone faintly talking to a radio within a distance...\n\nYou cannot make out for sure who it is. They are equipped with some form of protective clothing, carrying knives and a metal pipe.[/]\n");

            ExtFunctions.ContinuePrompt();

            ExtFunctions.CreateMenuPrompt(new[] { "Hide in the Visitor Centre.", "Sneak attack the unidentified person." }, "What do you want to do?", true, false);

            switch (ExtFunctions.ChosenOption)
            {
                case "Hide in the Visitor Centre.":
                    CurrentLocation = "Laundry Room";
                    NewLocation = "Visitor Centre";
                    PlayerIsMoving = true;
                    ExtFunctions.CleanConsole();

                    //SoundSystem.BGMPlayer.Stop();
                    SoundSystem.CheckBGMPlayer();

                    // Loop heart beating sound
                    SoundSystem.StoryBGMPlayer(SoundSystem.SFXSoundFileNames[0], PlaySFX);
                    
                    CurrentLocation = "Visitor Centre";
                    CurrentObjective = "Stay hidden!";

                    ExtFunctions.DisplayCurrentLocation(CurrentLocation);

                    AnsiConsole.MarkupLine($"[orange1]\nYou are back in the [cyan]{CurrentLocation}[/], hiding behind some boxes, near to a table.[/]\n");

                    Thread.Sleep(1500);

                    ChanceToSneakGuard = ChanceToSneakGuardRnd.Next(1, 10);

                    // [RANDOMISED CHANCE] Player may succeed in hiding.
                    if (ChanceToSneakGuard >= 5)
                    {
                        AnsiConsole.MarkupLine("[orange1]You have managed to [green]hide[/] from the unidentified person. They have went away. You decided to go back to the [cyan]Laundry Room[/].[/]");

                        SoundSystem.CheckBGMPlayer();
                        SoundSystem.CheckSFXPlayer();
                        SoundSystem.SFXSoundPlayer(SoundSystem.SFXSoundFileNames[6], PlaySFX);

                        LaundryRoomGuardRelocated = true;

                        ExtFunctions.ContinuePrompt();

                        PlayerIsMoving = true;
                        NewLocation = "Laundry Room";

                        ExtFunctions.CleanConsole();
                        laundryRoomCultGuardKilled();
                    }

                    // [RANDOMISED CHANCE] Player drops something causing the Guard to be on an alert and check the source of the noise.
                    else
                    {
                        // [PLAY] Heart beating sound
                        SoundSystem.CheckBGMPlayer();
                        SoundSystem.StoryBGMPlayer(SoundSystem.SFXSoundFileNames[0], PlaySFX);

                        ChanceToSneakAlertedGuard = ChanceToSneakAlertedGuardRND.Next(1, 20);

                        // Play sound of some items dropping...

                        SoundSystem.CheckSFXPlayer();
                        SoundSystem.SFXSoundPlayer(SoundSystem.SFXSoundFileNames[4], PlaySFX);

                        AnsiConsole.MarkupLine("[red]You have dropped some objects on the floor, while you were hiding! The unidentified person is coming to the Visitor Centre to check the noise out![/]\n");

                        LaundryRoomGuardAlerted = true;

                        ExtFunctions.ContinuePrompt();

                        // [CONDITION] If Player gets caught, they must defeat the Guard.
                        if (ChanceToSneakAlertedGuard >= 10)
                        {
                            AnsiConsole.MarkupLine("[red]You have got caught by the unidentified person! As they are armed and provoked to attack, you have no choice to engage in a bloody violence against him.\n[/]");

                            ExtFunctions.ContinuePrompt();

                            BattleSystem.EncounteredEnemy = "Cult Guard";
                            
                            SoundSystem.BGMPlayer.Stop();
                            SoundSystem.BGMPlayer.Dispose();

                            SoundSystem.SFXPlayer.Stop();
                            SoundSystem.SFXPlayer.Dispose();

                            // INITIATES BATTLE
                            BattleSystem.EnemyStatSetup();

                            LaundryRoomGuardKilled = true;

                            laundryRoomCultGuardKilled();
                        }

                        // [CONDITION] If Player succeeds in hiding, they can move to the Laundry Room.
                        else
                        {
                            AnsiConsole.MarkupLine("[orange1]You have managed to [green]hide[/] from the unidentified person. They have went away. You decided to go back to the [cyan]Laundry Room[/].[/]\n");

                            SoundSystem.CheckBGMPlayer();
                            SoundSystem.CheckSFXPlayer();
                            SoundSystem.SFXSoundPlayer(SoundSystem.SFXSoundFileNames[6], PlaySFX);

                            LaundryRoomGuardRelocated = true;

                            ExtFunctions.ContinuePrompt();

                            PlayerIsMoving = true;
                            NewLocation = "Laundry Room";

                            ExtFunctions.CleanConsole();
                            laundryRoomCultGuardKilled();
                        }
                    }

                    break;

                case "Sneak attack the unidentified person.":

                    AnsiConsole.MarkupLine("[orange1]You have decided to sneak attack the unidentified person, and have successfully done so![/]");

                    // INITIATES BATTLE.
                    BattleSystem.PlayerSneakAttack = true;
                    BattleSystem.EncounteredEnemy = "Cult Guard";
                    BattleSystem.EnemyStatSetup();

                    LaundryRoomGuardKilled = true;

                    laundryRoomCultGuardKilled();

                    break;                 
            }

            void laundryRoomCultGuardKilled()
            {
                if (LaundryRoomGuardKilled)
                {
                    SoundSystem.CheckBGMPlayer();
                    SoundSystem.StoryBGMPlayer(SoundSystem.StoryModeBGMFileNames[4], PlayBGM);

                    AnsiConsole.Markup("\n[orange1]After defeating the unidentified person, you examine the body, and find a document.[/]");
                    AnsiConsole.Write(new Markup("\n\n[red][underline]A reminder of our Objective[/]\n\"We must purge the sinful from these unfortunate lost souls, and let them reincarnate into a holy body that is cleansed from evil. Bring the [cyan]three unfortunate souls[/] to the Torture Chamber to prepare them for the ultimate absolution: death in the Execution Chamber.[/]\"\n").Centered());

                    LaundryRoomGuardExamined = true;

                    AnsiConsole.Markup("[orange1]You start to think about who the other two people are, contemplating on whether you should seek out for them and in return they may help you. This also indicates this unidentified person is with some sort of cult...\n\nA new entry has been added to your [cyan]Journal[/].[/]");

                    ExtFunctions.ContinuePrompt();

                    AnsiConsole.MarkupLine("[orange1]The guard's radio is going off...[/]");

                    ExtFunctions.ContinuePrompt();

                    ExtFunctions.CreateMenuPrompt(new[] { "Answer the radio, but try to mimic the person's voice.", "Ignore the radio." }, "What do you want to do?", true, false);

                    switch (ExtFunctions.ChosenOption)
                    {
                        case "Answer the radio, but try to mimic the person's voice.":
                            Random answerRadio = new();
                            int answerRadioInt = answerRadio.Next(1, 10);

                            if (answerRadioInt >= 5)
                            {
                                AnsiConsole.MarkupLine("[orange1]You have successfully mimicked the guard's voice, fooling the other guards of thinking there isn't anything wrong.[/]");
                                ExtFunctions.ContinuePrompt();
                            }

                            else
                            {
                                AnsiConsole.MarkupLine("[red]The other guard suspects something's wrong, as they did not recognise your voice...\nDue to this, they have sent reinforcements![/]");
                                ExtFunctions.ContinuePrompt();
                                LaundryRoomDeadGuardRadioAlerted = true;
                                // Due to time constraints, I will not be implementing the reinforcements.
                            }
                            break;

                        case "Ignore the radio.":

                            AnsiConsole.MarkupLine("You have decided not the answer the person on the radio. However, this may or may not have implications in the future...");

                            Random radioNotAnsweredRnd = new();
                            int radioNotAnswered = radioNotAnsweredRnd.Next(1, 10);

                            if (radioNotAnswered <= 5)
                            {
                                LaundryRoomDeadGuardRadioAlerted = true;                
                                ExtFunctions.CleanConsole();
                            }
                            break;
                    }
                }                               
                // After defeating the guard, you examine the body, and find a document from the guard. Stating that "We must purge the sin from these unfortunate lost souls, and let them reincarnate into a holy body that is cleansed from sin. Bring the three unfortunate souls to the Torture Chamber to prepare them for the ultimate absolution: death in the Execution Chamber."

                // This will add an entry in the player's diary: "If I am one of the three, who is the other two people that is holed up in this prison? Maybe I should go find them? Maybe not, as they may not cooperate with me to escape."

                // Someone will speak from the Radio, calling the dead guard. The player will panic and will not answer the radio.

                // Cult becomes suspicious, they send reinforcement that will be patrolling random rooms.

                if (LaundryRoomGuardKilled || LaundryRoomGuardRelocated)
                {
                    if (CanteenOfficerMet == false)
                    {
                        AnsiConsole.MarkupLine("[orange1]You may have seen the [cyan]Canteen[/] room, but have yet to go there. Would you like to go there now before proceeding further?[/]");

                        ExtFunctions.CreateMenuPrompt(new[] { "Yes.", "No." }, "What do you want to do?", true, false);

                        switch (ExtFunctions.ChosenOption)
                        {
                            case "Yes.":
                                PreviousLocation = "Laundry Room 3";
                                NewLocation = "Canteen";
                                PlayerIsMoving = true;
                                ExtFunctions.CleanConsole();
                                Story_Canteen();
                                break;

                            case "No.":
                                NewLocation = "Corridor 1 - Leading to the Guard Post and Cell Blocks";
                                PlayerIsMoving = true;
                                ExtFunctions.CleanConsole();
                                Story_Corridor1();
                                break;
                        }
                    }

                    else
                    {
                        AnsiConsole.MarkupLine("[orange1]You are back in the Laundry Room, but, as you have entered the Laundry Room, you have heard another [cyan]door unlocking[/] nearby. Seems to be near to the Guard Post, so you will be investigating this.[/]");

                        ExtFunctions.ContinuePrompt();

                        NewLocation = "Corridor 1 - Leading to the Guard Post and Cell Blocks";
                        PlayerIsMoving = true;
                        ExtFunctions.CleanConsole();
                        Story_Corridor1();
                    }
                }
            }
        }

        public void Story_Corridor1()
        {            
            if (ObtainedKeycardLevel3)
            {
                CurrentObjective = "Restore the power.";
                CurrentLocation = "Corridor 1";

                ExtFunctions.DisplayCurrentLocation(CurrentLocation);
                ExtFunctions.SaveGame();

                AnsiConsole.MarkupLine("[orange1]\nIn the Corridor, there is the Guard Post that you have already been inside of. However, you can now get into the [cyan]Weapons Room[/].[/]");

                ExtFunctions.CreateMenuPrompt(new[] { "The Infirmary.", "Laundry Room." , "Cell Block A.", "Cell Block B.", "Weapons Room." }, "Where would you like to go?", true, false);

                switch (ExtFunctions.ChosenOption)
                {
                    case "The Infirmary.":
                        NewLocation = "The Infirmary";
                        PlayerIsMoving = true;
                        ExtFunctions.CleanConsole();
                        Story_Infirmary();
                        break;

                    case "Cell Block A.":
                        NewLocation = "Cell Block A";
                        PlayerIsMoving = true;
                        ExtFunctions.CleanConsole();
                        Story_CellBlockA();
                        break;

                    case "Cell Block B.":
                        NewLocation = "Cell Block B";
                        PlayerIsMoving = true;
                        ExtFunctions.CleanConsole();
                        Story_CellBlockB();
                        break;

                    case "Weapons Room.":
                        NewLocation = "Weapons Room";
                        PlayerIsMoving = true;
                        ExtFunctions.CleanConsole();
                        Story_WeaponsRoom();
                        break;

                    case "Laundry Room.":
                        NewLocation = "Laundry Room";
                        PlayerIsMoving = true;
                        ExtFunctions.CleanConsole();
                        Story_LaundryRoom3();
                        break;
                }
            }

            else
            {
                //SoundSystem.CheckBGMPlayer();
                //SoundSystem.StoryBGMPlayer(SoundSystem.StoryModeBGMFileNames[4], PlayBGM);

                CurrentObjective = "Restore the power.";
                CurrentLocation = "Corridor 1";

                ExtFunctions.DisplayCurrentLocation(CurrentLocation);
                ExtFunctions.SaveGame();

                AnsiConsole.MarkupLine("[orange1]\nIn the Corridor, there is the Guard Post that you have already been inside of. However, there are three new locations that you have discovered. These are:\n- Infirmary.\n- Cell Block A\n- Cell Block B.[/]");

                ExtFunctions.CreateMenuPrompt(new[] { "The Infirmary.", "Cell Block A.", "Cell Block B." }, "Where would you like to go?", true, false);

                switch (ExtFunctions.ChosenOption)
                {
                    case "The Infirmary.":
                        NewLocation = "The Infirmary";
                        PlayerIsMoving = true;
                        ExtFunctions.CleanConsole();
                        Story_Infirmary();
                        break;

                    case "Cell Block A.":
                        NewLocation = "Cell Block A";
                        PlayerIsMoving = true;
                        ExtFunctions.CleanConsole();
                        Story_CellBlockA();
                        break;

                    case "Cell Block B.":
                        NewLocation = "Cell Block B";
                        PlayerIsMoving = true;
                        ExtFunctions.CleanConsole();
                        Story_CellBlockB();
                        break;

                    case "Guard's Post.":
                        NewLocation = "Guard's Post";
                        PlayerIsMoving = true;
                        ExtFunctions.CleanConsole();
                        Story_GuardPost1();
                        break;
                }
            }
        }

        public void Story_Infirmary()
        {
            /*
                -> Requires Level 1 Clearance Keycard.
				-> Contains first aid and bandages.
				-> Contains few recovery items.
				-> Contains food chain puzzle, that the solution to it is in the same room.
             */

            CurrentLocation = "Infirmary";

            ExtFunctions.DisplayCurrentLocation(CurrentLocation);
            ExtFunctions.SaveGame();

            // EntranceNoPower is not required to be checked as the player needs to go and check out the Entrance regardless.
            //CurrentObjective = "Restore the power.\nGet into the Infirmary for First Aid and Bandages.";
            //ExtFunctions.DisplayCurrentLocation(CurrentLocation);

            AnsiConsole.MarkupLine("[orange1]\nYou are now at the [cyan]Infirmary[/], you need to get inside to find some [cyan]bandages and first aid[/]...[/]");

            ExtFunctions.ContinuePrompt();

            if (ObtainedKeycardLevel1)
            {
                SoundSystem.CheckSFXPlayer();
                SoundSystem.SFXSoundPlayer(SoundSystem.SFXSoundFileNames[5], PlaySFX);
                // Play some beep beep sounds of unlocking a door.

                AnsiConsole.MarkupLine(
                    "[orange1]As you have the [cyan]Level 1 Clearance Keycard[/], you have unlocked the door.[/]");

                ExtFunctions.ContinuePrompt();

                AnsiConsole.MarkupLine("[orange1]You are now inside of the [cyan]Infirmary[/]. As you looked around, the room is in a bit of a mess, but it seems like nobody has been inside for a long time.[/]");

                ExtFunctions.ContinuePrompt();

            InfirmaryRoomSelections:

                if (ObtainedBandages == false && ObtainedFirstAid == false)
                {
                    ExtFunctions.CreateMenuPrompt(new[] { "Check for First Aid.", "Check for Bandages.", "Go back to the Corridor." }, "What do you want to do?", true, false);
                  
                    switch (ExtFunctions.ChosenOption)
                    {
                        case "Check for First Aid.":
                            if (ObtainedFirstAid == false && ObtainedBandages == false)
                            {
                                AnsiConsole.MarkupLine("[orange1]While looking around for First Aid, you have came across a cabinet, with see-through doors. There seem to be a unique lock, almost like those suitcase number locks, but this one has pictures of [cyan]animals[/]...[/]");

                                ExtFunctions.ContinuePrompt();

                                ExtFunctions.CreateMenuPrompt(new[] { "Yes.", "No.", "Read the inscription." }, "Would you like to try to open the lock?", true, false);

                                switch (ExtFunctions.ChosenOption)
                                {
                                    case "Yes.":
                                        // "Bear.", "Wolf.", "Snake."

                                        if (ObtainedFirstAid == false)
                                        {

                                            AnsiConsole.MarkupLine($"[orange1]These are the options for you to select...:\n\n- {FoodChainPuzzle1[2]}\n- {FoodChainPuzzle1[0]}\n- {FoodChainPuzzle1[1]}.[/]\n");

                                            ExtFunctions.ContinuePrompt();



                                            ExtFunctions.CreateMenuPrompt(new[] { FoodChainPuzzle1[2], FoodChainPuzzle1[1], FoodChainPuzzle1[0] }, "Choose your first selection.", true, false);

                                            string puzzleInput1 = ExtFunctions.ChosenOption;

                                            ExtFunctions.CreateMenuPrompt(new[] { FoodChainPuzzle1[0], FoodChainPuzzle1[2], FoodChainPuzzle1[1] }, "Choose your second selection.", true, false);
                                            
                                            string puzzleInput2 = ExtFunctions.ChosenOption;

                                            ExtFunctions.CreateMenuPrompt(new[] { FoodChainPuzzle1[0], FoodChainPuzzle1[1], FoodChainPuzzle1[2] }, "Choose your third selection.", true, false);

                                            string puzzleInput3 = ExtFunctions.ChosenOption;
                                            

                                            if (puzzleInput3 == "Bear." && puzzleInput2 == "Wolf." &&
                                                puzzleInput1 == "Snake.")
                                            {
                                                AnsiConsole.MarkupLine("[orange1]You have unlocked the lock, and found a [cyan]First Aid[/] kit inside of the cabinet.[/]");
                                                ObtainedFirstAid = true;
                                                ExtFunctions.ContinuePrompt();
                                                goto InfirmaryRoomSelections;
                                            }

                                            else
                                            {
                                                AnsiConsole.MarkupLine("[orange1]You have failed to unlock the lock, and the lock has reset itself.[/]");
                                                ExtFunctions.ContinuePrompt();
                                                goto InfirmaryRoomSelections;
                                            }
                                        }
                                        break;

                                    case "No.":
                                        goto InfirmaryRoomSelections;

                                    case "Read the inscription.":

                                        AnsiConsole.Write(new Markup("[underline][orange1]The inscription says:\n[/][/]" +
                                                                     "[orange1]The creatures, created by the God, must [cyan]eat[/] each other to evolve and become stronger. The weakest of the three, is [cyan]eaten by the two[/], but the second weakest of the three, [cyan]is eaten by the strongest, but eats the weakest[/]. The strongest of the three, [cyan]eats the weakest and rules over them...[/][/]").Centered());

                                        ExtFunctions.ContinuePrompt();

                                        ExtFunctions.CreateMenuPrompt(new[] { "Yes.", "No." }, "Would you like to try to open the lock?", true, false);

                                        if (ExtFunctions.ChosenOption == "Yes.")
                                        {
                                            goto case "Yes.";
                                        }

                                        if (ExtFunctions.ChosenOption == "No.")
                                        {
                                            goto case "No.";
                                        }
                                        break;
                                }
                            }

                            else
                            {
                                AnsiConsole.MarkupLine("[orange1]You have already gotten the First Aid from here.[/]");
                                ExtFunctions.ContinuePrompt();
                                goto InfirmaryRoomSelections;
                            }

                            break;

                        case "Check for Bandages.":
                            // Adds few bandages to the player's inventory.

                            AnsiConsole.MarkupLine("[orange1]Luckily, as you were intensely looking around, you came across few boxes of supplies. Most of them were empty or rotted away. You end up finding a [cyan]bandage[/] in one of the boxes. After moving that box, you have found two more, so you took them.[/]");

                            Player.Bandages += 2;
                            ObtainedBandages = true;

                            ExtFunctions.ContinuePrompt();

                            goto InfirmaryRoomSelections;

                        case "Go back to the Corridor.":
                            NewLocation = "Corridor 1";
                            PlayerIsMoving = true;
                            ExtFunctions.CleanConsole();
                            Story_Corridor1();
                            break;
                    }
                }

                if (ObtainedFirstAid && ObtainedBandages == false)
                {
                    ExtFunctions.CreateMenuPrompt(new[] { "Check for Bandages.", "Go back to the Corridor." }, "What do you want to do?", true, false);

                    switch (ExtFunctions.ChosenOption)
                    {

                        case "Check for Bandages.":
                            // Adds few bandages to the player's inventory.

                            AnsiConsole.MarkupLine("[orange1]Luckily, as you were intensely looking around, you came across few boxes of supplies. Most of them were empty or rotted away. You end up finding a [cyan]bandage[/] in one of the boxes. After moving that box, you have found few more, so you took them.[/]");

                            Player.Bandages += 2;

                            ExtFunctions.ContinuePrompt();

                            ObtainedBandages = true;
                            goto InfirmaryRoomSelections;

                        case "Go back to the Corridor.":
                            NewLocation = "Corridor 1";
                            PlayerIsMoving = true;
                            ExtFunctions.CleanConsole();
                            Story_Corridor1();
                            break;
                    }
                }

                if (ObtainedFirstAid == false && ObtainedBandages)
                {
                    ExtFunctions.CreateMenuPrompt(new[] { "Check for First Aid.", "Go back to the Corridor." }, "What do you want to do?", true, false);

                    switch (ExtFunctions.ChosenOption)
                    {
                        case "Check for First Aid.":
                                AnsiConsole.MarkupLine("[orange1]\nWhile looking around for First Aid, you have came across a cabinet, with see-through doors. There seem to be a unique lock, almost like those suitcase number locks, but this one has pictures of [cyan]animals[/]...[/]");

                                ExtFunctions.ContinuePrompt();

                                ExtFunctions.CreateMenuPrompt(new[] { "Yes.", "No.", "Read the inscription." }, "Would you like to try to open the lock?", true, false);

                                switch (ExtFunctions.ChosenOption)
                                {
                                    case "Yes.":
                                        // "Bear.", "Wolf.", "Snake."

                                        if (ObtainedFirstAid == false)
                                        {

                                            AnsiConsole.MarkupLine($"[orange1]These are the options for you to select...:\n\n- {FoodChainPuzzle1[2]}\n- {FoodChainPuzzle1[0]}\n- {FoodChainPuzzle1[1]}.[/]\n");

                                            ExtFunctions.ContinuePrompt();

                                            ExtFunctions.CreateMenuPrompt(new[] { FoodChainPuzzle1[2], FoodChainPuzzle1[1], FoodChainPuzzle1[0] }, "Choose your first selection.", true, false);

                                            string puzzleInput1 = ExtFunctions.ChosenOption;

                                            ExtFunctions.CreateMenuPrompt(new[] { FoodChainPuzzle1[0], FoodChainPuzzle1[2], FoodChainPuzzle1[1] }, "Choose your second selection.", true, false);

                                            string puzzleInput2 = ExtFunctions.ChosenOption;

                                            ExtFunctions.CreateMenuPrompt(new[] { FoodChainPuzzle1[0], FoodChainPuzzle1[1], FoodChainPuzzle1[2] }, "Choose your third selection.", true, false);

                                            string puzzleInput3 = ExtFunctions.ChosenOption;

                                            if (puzzleInput3 == "Bear." && puzzleInput2 == "Wolf." &&
                                                puzzleInput1 == "Snake.")
                                            {
                                                AnsiConsole.MarkupLine("[orange1]You have unlocked the lock, and found a [cyan]First Aid[/] kit inside of the cabinet.[/]");
                                                ObtainedFirstAid = true;
                                                ExtFunctions.ContinuePrompt();
                                                goto InfirmaryRoomSelections;
                                            }

                                            else
                                            {
                                                AnsiConsole.MarkupLine("[orange1]You have failed to unlock the lock, and the lock has reset itself.[/]");
                                                ExtFunctions.ContinuePrompt();
                                                goto InfirmaryRoomSelections;
                                            }
                                        }
                                        break;

                                    case "No.":
                                        goto InfirmaryRoomSelections;

                                    case "Read the inscription.":

                                        AnsiConsole.Write(new Markup("[underline][orange1]The inscription says:\n[/][/]" +
                                                                     "[orange1]The creatures, created by the God, must [cyan]eat[/] each other to evolve and become stronger. The weakest of the three, is [cyan]eaten by the two[/], but the second weakest of the three, [cyan]is eaten by the strongest, but eats the weakest[/]. The strongest of the three, [cyan]eats the weakest and rules over them...[/][/]").Centered());

                                        ExtFunctions.ContinuePrompt();

                                        ExtFunctions.CreateMenuPrompt(new[] { "Yes.", "No." }, "Would you like to try to open the lock?", true, false);

                                        if (ExtFunctions.ChosenOption == "Yes.")
                                        {
                                            goto case "Yes.";
                                        }

                                        if (ExtFunctions.ChosenOption == "No.")
                                        {
                                            goto case "No.";
                                        }
                                        break;
                                }
                            break;
                    }
                }

                if (ObtainedFirstAid && ObtainedBandages)
                {
                    AnsiConsole.MarkupLine("[orange1]There is nothing else to do, as you got what you have came for. Due to this, you decide to go back to the Corridor...[/]");
                    ExtFunctions.ContinuePrompt();
                    NewLocation = "Corridor 1";
                    PlayerIsMoving = true;
                    ExtFunctions.CleanConsole();
                    Story_Corridor1();
                }
            }

            else
            {
                SoundSystem.CheckSFXPlayer();
                SoundSystem.SFXSoundPlayer(SoundSystem.SFXSoundFileNames[3], PlaySFX);

                AnsiConsole.MarkupLine("[orange1]You do not carry the required keycard, to unlock this room. The door requires [cyan]Level 1 Clearance Keycard[/] to unlock the door and get inside. Due to this, you are going back to the Corridor...[/]");

                ExtFunctions.ContinuePrompt();

                NewLocation = "Corridor 1";
                PlayerIsMoving = true;
                ExtFunctions.CleanConsole();
                Story_Corridor1();
            }
        }

        public void Story_CellBlockA()
        {
            CurrentLocation = "Cell Block A";
            
            ExtFunctions.DisplayCurrentLocation(CurrentLocation);
            ExtFunctions.SaveGame();

            if (EscapedFromTortureChamber || ObtainedKeycardLevel3)
            {
                AnsiConsole.MarkupLine("[orange1]You are back in the Cell Block A.[/]");

                ExtFunctions.ContinuePrompt();

                ExtFunctions.CreateMenuPrompt(new[] { "Go to the Laundry Room.", "Go to the Entrance.", "Go to the Shower Room.", "Go to Cell Block B.", "Go back to Solitary.", "Go to the Weapons Room (inside Guard's Post)." }, "What do you want to do?", true, false);

                switch (ExtFunctions.ChosenOption)
                {
                    case "Go to the Entrance.":
                        AnsiConsole.MarkupLine("[orange1]The corridor leading to the Entrance, is now blocked, looked like something has happened here, as there is a bunch of debris and blood.[/]");
                        ExtFunctions.ContinuePrompt();
                        ExtFunctions.CleanConsole();
                        Story_CellBlockB();
                        break;

                    case "Go to the Shower Room.":
                        NewLocation = "Shower Room";
                        PlayerIsMoving = true;
                        ExtFunctions.CleanConsole();
                        Story_Showers();
                        break;

                    case "Go to Cell Block B.":
                        NewLocation = "Cell Block B";
                        PlayerIsMoving = true;
                        ExtFunctions.CleanConsole();
                        Story_CellBlockB();
                        break;

                    case "Go to the Solitary.":
                        NewLocation = "Solitary";
                        PlayerIsMoving = true;
                        ExtFunctions.CleanConsole();
                        Story_Solitary2();
                        break;

                    case "Go to the Laundry Room.":
                        NewLocation = "Laundry Room";
                        PlayerIsMoving = true;
                        ExtFunctions.CleanConsole();
                        Story_LaundryRoom3();
                        break;

                    case "Go to the Weapons Room (inside Guard's Post).":
                        NewLocation = "Weapons Room";
                        PlayerIsMoving = true;
                        ExtFunctions.CleanConsole();
                        Story_WeaponsRoom();
                        break;
                }
            }

            //SoundSystem.CheckBGMPlayer();
            //SoundSystem.StoryBGMPlayer(SoundSystem.StoryModeBGMFileNames[4], PlayBGM);

            //CurrentObjective = "Restore the power.";

            AnsiConsole.MarkupLine("[orange1]You are now in Cell Block A, there are many cells in this Cell Block, and all of them seem to be shut.\n\nThere is a room that seem to have a control switch, that opens and closes each cell door.[/]");

            ExtFunctions.ContinuePrompt();

            ExtFunctions.CreateMenuPrompt(new[] { "Yes.", "No." }, "Do you want to go inside?", true, false);

            switch (ExtFunctions.ChosenOption)
            {
                case "Yes.":
                    if (ObtainedKeycardLevel1 == false)
                    {
                        // Play sound of the keycard reader beeping.
                        SoundSystem.CheckSFXPlayer();
                        SoundSystem.SFXSoundPlayer(SoundSystem.SFXSoundFileNames[3], PlaySFX);

                        AnsiConsole.MarkupLine("[orange1]The door is sealed tight. Seems like you need a [cyan]Level 1 Clearance Keycard[/] to open the door. As you do not have the keycard, you have decided to get out from the Cell Block A, to the Corridor.[/]");

                        ExtFunctions.ContinuePrompt();

                        NewLocation = "Corridor 1";
                        PlayerIsMoving = true;
                        ExtFunctions.CleanConsole();
                        Story_Corridor1();
                    }

                    else
                    {
                        // Play sound of the keycard reader beeping.
                        SoundSystem.CheckSFXPlayer();
                        SoundSystem.SFXSoundPlayer(SoundSystem.SFXSoundFileNames[5], PlaySFX);

                        NewLocation = "Cell Block A: Control Room";
                        PlayerIsMoving = true;
                        ExtFunctions.CleanConsole();
                        Story_CellBlockAControlRoom();
                    }

                    break;

                case "No.":

                    if (ObtainedFuel == false && ObtainedBandages == false && ObtainedFirstAid == false)
                    {
                        ExtFunctions.CreateMenuPrompt(new[] { "Guard's Post.", "Shower Room.", "Cell Block B.", "Solitary.", "Infirmary." }, "Where do you want to go?", true, false);

                        switch (ExtFunctions.ChosenOption)
                        {
                            case "Solitary.":
                                NewLocation = "Solitary";
                                PlayerIsMoving = true;
                                ExtFunctions.CleanConsole();
                                Story_Solitary1();

                                break;

                            case "Cell Block B.":
                                NewLocation = "Cell Block B";
                                PlayerIsMoving = true;
                                ExtFunctions.CleanConsole();
                                Story_CellBlockB();

                                break;

                            case "Guard's Post.":
                                NewLocation = "Guard's Post";
                                PlayerIsMoving = true;
                                ExtFunctions.CleanConsole();
                                Story_GuardPost1();

                                break;

                            case "Infirmary.":
                                NewLocation = "Infirmary";
                                PlayerIsMoving = true;
                                ExtFunctions.CleanConsole();
                                Story_Infirmary();

                                break;

                            case "Shower Room.":
                                NewLocation = "Shower Room";
                                PlayerIsMoving = true;
                                ExtFunctions.CleanConsole();
                                Story_Showers();
                                break;
                        }
                    }

                    if (ObtainedFuel && ObtainedBandages == false && ObtainedFirstAid == false)
                    {
                        ExtFunctions.CreateMenuPrompt(new[] { "Solitary.", "Cell Block B.", "Guard's Post.", "Infirmary." }, "Where do you want to go?", true, false);

                        switch (ExtFunctions.ChosenOption)
                        {
                            case "Solitary.":

                                NewLocation = "Solitary";
                                PlayerIsMoving = true;
                                ExtFunctions.CleanConsole();
                                Story_Solitary1();

                                break;

                            case "Cell Block B.":

                                NewLocation = "Cell Block B";
                                PlayerIsMoving = true;
                                ExtFunctions.CleanConsole();
                                Story_CellBlockB();

                                break;

                            case "Guard's Post.":

                                NewLocation = "Guard's Post";
                                PlayerIsMoving = true;
                                ExtFunctions.CleanConsole();
                                Story_GuardPost1();
                                break;

                            case "Infirmary.":
                                NewLocation = "Infirmary";
                                PlayerIsMoving = true;
                                ExtFunctions.CleanConsole();
                                Story_Infirmary();
                                break;
                        }
                    }

                    else if (ObtainedFuel && ObtainedBandages && ObtainedFirstAid)
                    {
                        ExtFunctions.CreateMenuPrompt(new[] { "Solitary.", "Cell Block B.", "Guard's Post.", "Execution Room." }, "Where do you want to go?", true, false);

                       
                        switch (ExtFunctions.ChosenOption)
                        {
                            case "Solitary.":

                                NewLocation = "Solitary";
                                PlayerIsMoving = true;
                                ExtFunctions.CleanConsole();
                                Story_Solitary1();

                                break;

                            case "Cell Block B.":

                                NewLocation = "Cell Block B";
                                PlayerIsMoving = true;
                                ExtFunctions.CleanConsole();
                                Story_CellBlockB();

                                break;

                            case "Guard's Post.":

                                NewLocation = "Guard's Post";
                                PlayerIsMoving = true;
                                ExtFunctions.CleanConsole();
                                Story_GuardPost1();
                                break;

                            case "Execution Room.":
                                NewLocation = "Execution Room";
                                PlayerIsMoving = true;
                                ExtFunctions.CleanConsole();
                                Story_ExecutionRoom();
                                break;
                        }
                    }
                    break;
            }

            void Story_CellBlockAControlRoom()
            {
                //Console.Title = "Location: Cell Block A Control Room";
                CurrentLocation = "Cell Block A - Control Room";

                ExtFunctions.DisplayCurrentLocation(CurrentLocation);
                ExtFunctions.SaveGame();

                AnsiConsole.MarkupLine("[orange1]In the room, you can see a control switch that handles closing and opening of cell doors. Some switches are broken, so you cannot open certain cell doors.[/]");

                // Randomise which room the wire will be.

                GOBACKHEREAFTERBATTLE:

                ExtFunctions.CreateMenuPrompt(new[] { "Cell A-1.", "Cell A-3.", "Cell A-4." }, "Which cell door would you like to open?", true, false);
                
                switch (ExtFunctions.ChosenOption)
                {
                    case "Cell A-1.":
                        NewLocation = "Cell Block A: Cell A-1";
                        PlayerIsMoving = true;
                        ExtFunctions.CleanConsole();

                        BattleSystem.EncounteredEnemy = BattleSystem.EnemyList[2];
                        AnsiConsole.MarkupLine($"[orange1]Upon entering {NewLocation}, [red]you have woken up {BattleSystem.EncounteredEnemy}![/] {BattleSystem.EncounteredEnemy} is now aggravated and ready to chop chop you.[/]");

                        SoundSystem.BGMPlayer.Stop();

                        ExtFunctions.ContinuePrompt();

                        BattleSystem.EnemyStatSetup();

                        goto GOBACKHEREAFTERBATTLE;

                        // Initiate battle

                    case "Cell A-3.":
                        AnsiConsole.MarkupLine("[orange1]You have found a [green]wire[/]! You may need it, so you will take it with you.[/]");

                        ObtainedKeyPadWire1 = true;

                        ExtFunctions.ContinuePrompt();

                        AnsiConsole.MarkupLine("[orange1]There is nothing else to do here...[/]");

                        ExtFunctions.ContinuePrompt();

                        ExtFunctions.CreateMenuPrompt(new[] { "Go to the Solitary Block.", "Go to the Shower Room.", "Go to Cell Block B." }, "What do you want to do?", true, false);

                        switch (ExtFunctions.ChosenOption)
                        {
                            case "Go to the Solitary Block.":
                                NewLocation = "Solitary";
                                PlayerIsMoving = true;
                                ExtFunctions.CleanConsole();
                                Story_Solitary1();
                                break;

                            case "Go to the Shower Room.":
                                NewLocation = "Shower Room";
                                PlayerIsMoving = true;
                                ExtFunctions.CleanConsole();
                                Story_Showers();
                                break;

                            case "Go to Cell Block B.":
                                NewLocation = "Cell Block B";
                                PlayerIsMoving = true;
                                ExtFunctions.CleanConsole();
                                Story_CellBlockB();
                                break;
                        }
                        break;

                    case "Cell A-4":

                        // Has supplies.

                        AnsiConsole.MarkupLine("[orange1]You have found some supplies in this cell! You have found:\n\n- 1x Bandage.\n- 2x Crunchy Chick.\n- 1x Box of Ferrero Rocher.");

                        Player.Bandages += 1;
                        Player.CrunchyChick += 2;
                        Player.BoxofFerreroRocher += 1;

                        ExtFunctions.ContinuePrompt();

                        break;
                }

                if (ObtainedKeyPadWire1)
                {
                    AnsiConsole.MarkupLine("[orange1]There is nothing else to do here...[/]");

                    ExtFunctions.CreateMenuPrompt(new[] { "Go to the Solitary Block.", "Go to the Shower Room.", "Go to Cell Block B." }, "What do you want to do?", true, false);

                    switch (ExtFunctions.ChosenOption)
                    {
                        case "Go to the Solitary Block.":
                            NewLocation = "Solitary";
                            PlayerIsMoving = true;
                            ExtFunctions.CleanConsole();
                            Story_Solitary1();
                            break;

                        case "Go to the Shower Room.":
                            NewLocation = "Shower Room";
                            PlayerIsMoving = true;
                            ExtFunctions.CleanConsole();
                            Story_Showers();
                            break;

                        case "Go to Cell Block B.":
                            NewLocation = "Cell Block B";
                            PlayerIsMoving = true;
                            ExtFunctions.CleanConsole();
                            Story_CellBlockB();
                            break;
                    }
                }
            }
        }

        public void Story_CellBlockB()
        {
            //Console.Title = "Location: Cell Block B";
            CurrentLocation = "Cell Block B";

            ExtFunctions.DisplayCurrentLocation(CurrentLocation);
            ExtFunctions.SaveGame();

            if (EscapedFromTortureChamber)
            {
                AnsiConsole.MarkupLine("[orange1]You are back in the Cell Block B.[/]");

                ExtFunctions.ContinuePrompt();

                ExtFunctions.CreateMenuPrompt(new[] { "Go to the Entrance.", "Go to the Shower Room.", "Go to Cell Block A.", "Go to the Laundry Room.", "Go to the Solitary.", "Go to the Weapons Room (inside Guard's Post)." }, "Where do you want to go?", true, false);

                switch (ExtFunctions.ChosenOption)
                {
                    case "Go to the Entrance.":
                        AnsiConsole.MarkupLine("[orange1]The corridor leading to the Entrance, is now [red]blocked[/], it looked like something has happened here, as there is a bunch of debris and blood.[/]");
                        ExtFunctions.ContinuePrompt();
                        ExtFunctions.CleanConsole();
                        Story_CellBlockB();
                        break;

                    case "Go to the Shower Room.":
                        NewLocation = "Shower Room";
                        PlayerIsMoving = true;
                        ExtFunctions.CleanConsole();
                        Story_Showers();
                        break;

                    case "Go to Cell Block A.":
                        NewLocation = "Cell Block A";
                        PlayerIsMoving = true;
                        ExtFunctions.CleanConsole();
                        Story_CellBlockA();
                        break;

                    case "Go to the Solitary.":
                        NewLocation = "Solitary";
                        PlayerIsMoving = true;
                        ExtFunctions.CleanConsole();
                        Story_Solitary2();
                        break;

                    case "Go to the Laundry Room.":
                        NewLocation = "Laundry Room";
                        PlayerIsMoving = true;
                        ExtFunctions.CleanConsole();
                        Story_LaundryRoom3();
                        break;

                    case "Go to the Weapons Room (inside Guard's Post).":
                        NewLocation = "Weapons Room";
                        PlayerIsMoving = true;
                        ExtFunctions.CleanConsole();
                        Story_WeaponsRoom();
                        break;
                }
            }

            SoundSystem.CheckBGMPlayer();
            SoundSystem.StoryBGMPlayer(SoundSystem.StoryModeBGMFileNames[4], PlayBGM);

            AnsiConsole.MarkupLine("[orange1]You are now in Cell Block B, there are many cells in this Cell Block, and all of them seem to be shut.\n\nThere is a room that seem to have a control switch, that opens and closes each cell door.[/]");

            ExtFunctions.CreateMenuPrompt(new[] { "Yes.", "No." }, "Do you want to go inside the control room?", true, false);

            switch (ExtFunctions.ChosenOption)
            {
                case "Yes.":

                    if (ObtainedKeycardLevel2 == false)
                    {
                        SoundSystem.CheckSFXPlayer();
                        SoundSystem.SFXSoundPlayer(SoundSystem.SFXSoundFileNames[3], PlaySFX);

                        AnsiConsole.MarkupLine("[orange1]The door is sealed tight. Seems like you need a [cyan]Level 2 Clearance Keycard[/] to open the door. As you do not have the keycard, you have decided to get out from the Cell Block B, to the Corridor.[/]");

                        ExtFunctions.ContinuePrompt();

                        NewLocation = "Corridor 1";
                        PlayerIsMoving = true;
                        ExtFunctions.CleanConsole();
                        Story_Corridor1();
                    }

                    else
                    {
                        // Play sound of the keycard reader beeping.
                        SoundSystem.CheckSFXPlayer();
                        SoundSystem.SFXSoundPlayer(SoundSystem.SFXSoundFileNames[5], PlaySFX);

                        NewLocation = "Cell Block B: Control Room";
                        PlayerIsMoving = true;
                        ExtFunctions.CleanConsole();
                        Story_CellBlockBControlRoom();
                    }
                    break;

                case "No.":
                    if (ObtainedFuel == false && ObtainedBandages == false && ObtainedFirstAid == false)
                    {
                        ExtFunctions.CreateMenuPrompt(new[] { "Guard's Post.", "Shower Room.", "Cell Block A.", "Solitary.", "Infirmary." }, "Where do you want to go?", true, false);

                        switch (ExtFunctions.ChosenOption)
                        {
                            case "Solitary.":
                                NewLocation = "Solitary";
                                PlayerIsMoving = true;
                                ExtFunctions.CleanConsole();
                                Story_Solitary1();

                                break;

                            case "Cell Block A.":
                                NewLocation = "Cell Block A.";
                                PlayerIsMoving = true;
                                ExtFunctions.CleanConsole();
                                Story_CellBlockA();

                                break;

                            case "Guard's Post.":
                                NewLocation = "Guard's Post";
                                PlayerIsMoving = true;
                                ExtFunctions.CleanConsole();
                                Story_GuardPost1();

                                break;

                            case "Infirmary.":
                                NewLocation = "Infirmary";
                                PlayerIsMoving = true;
                                ExtFunctions.CleanConsole();
                                Story_Infirmary();

                                break;

                            case "Shower Room.":
                                NewLocation = "Shower Room";
                                PlayerIsMoving = true;
                                ExtFunctions.CleanConsole();
                                Story_Showers();
                                break;
                        }
                    }

                    else if (ObtainedFuel)
                    {
                        ExtFunctions.CreateMenuPrompt(new[] { "Solitary.", "Cell Block A.", "Guard's Post.", "Infirmary." }, "Where do you want to go?", true, false);

                        switch (ExtFunctions.ChosenOption)
                        {
                            case "Solitary.":
                                NewLocation = "Solitary";
                                PlayerIsMoving = true;
                                ExtFunctions.CleanConsole();
                                Story_Solitary1();

                                break;

                            case "Cell Block A.":
                                NewLocation = "Cell Block A";
                                PlayerIsMoving = true;
                                ExtFunctions.CleanConsole();
                                Story_CellBlockA();

                                break;

                            case "Guard's Post.":
                                NewLocation = "Guard's Post";
                                PlayerIsMoving = true;
                                ExtFunctions.CleanConsole();
                                Story_GuardPost1();

                                break;

                            case "Infirmary.":
                                NewLocation = "Infirmary";
                                PlayerIsMoving = true;
                                ExtFunctions.CleanConsole();
                                Story_Infirmary();

                                break;
                        }
                    }

                    else if (ObtainedFuel && ObtainedBandages && ObtainedFirstAid)
                    {
                        ExtFunctions.CreateMenuPrompt(new[] { "Solitary.", "Cell Block A.", "Guard's Post." }, "Where do you want to go?", true, false);

                        switch (ExtFunctions.ChosenOption)
                        {
                            case "Solitary.":
                                NewLocation = "Solitary";
                                PlayerIsMoving = true;
                                ExtFunctions.CleanConsole();
                                Story_Solitary1();

                                break;

                            case "Cell Block A.":
                                NewLocation = "Cell Block A";
                                PlayerIsMoving = true;
                                ExtFunctions.CleanConsole();
                                Story_CellBlockA();

                                break;

                            case "Guard's Post.":
                                NewLocation = "Guard's Post";
                                PlayerIsMoving = true;
                                ExtFunctions.CleanConsole();
                                Story_GuardPost1();
                                break;
                        }
                    }
                    break;
            }

            void Story_CellBlockBControlRoom()
            {
                CurrentLocation = "Cell Block B - Control Room";

                ExtFunctions.DisplayCurrentLocation(CurrentLocation);
                ExtFunctions.SaveGame();

                AnsiConsole.MarkupLine("[orange1]In the room, you can see a control switch that handles closing and opening of cell doors. Some switches are broken, so you cannot open certain cell doors.[/]");

                ExtFunctions.ContinuePrompt();

                GOBACKHEREAFTERBATTLE:

                ExtFunctions.CreateMenuPrompt(new[] { "Cell B-2.", "Cell B-5.", "Cell B-6." }, "Which cell door would you like to open?", true, false);

                switch (ExtFunctions.ChosenOption)
                {
                    case "Cell B-2.":

                        ObtainedKeyPadWire2 = true;

                        AnsiConsole.MarkupLine("[orange1]You have found a [green]wire[/]! You may need it, so you think it with you.[/]");

                        ExtFunctions.ContinuePrompt();

                        AnsiConsole.MarkupLine("[orange1]There is nothing else to do here...[/]");

                        ExtFunctions.CreateMenuPrompt(new[] { "Go to the Solitary Block.", "Go to the Shower Room.", "Go to Cell Block A." }, "What do you want to do?", true, false);

                        if (ObtainedFuel == false)
                        {
                            switch (ExtFunctions.ChosenOption)
                            {
                                case "Go to the Solitary Block.":
                                    NewLocation = "Solitary";
                                    PlayerIsMoving = true;
                                    ExtFunctions.CleanConsole();
                                    Story_Solitary1();
                                    break;

                                case "Go to Cell Block A.":
                                    NewLocation = "Cell Block A";
                                    PlayerIsMoving = true;
                                    ExtFunctions.CleanConsole();
                                    Story_CellBlockA();
                                    break;
                            }
                            break;
                        }

                        else
                        {
                            switch (ExtFunctions.ChosenOption)
                            {
                                case "Go to the Solitary Block.":
                                    NewLocation = "Solitary";
                                    PlayerIsMoving = true;
                                    ExtFunctions.CleanConsole();
                                    Story_Solitary1();
                                    break;

                                case "Go to the Shower Room.":
                                    NewLocation = "Shower Room";
                                    PlayerIsMoving = true;
                                    ExtFunctions.CleanConsole();
                                    Story_Showers();
                                    break;

                                case "Go to Cell Block B.":
                                    NewLocation = "Cell Block A";
                                    PlayerIsMoving = true;
                                    ExtFunctions.CleanConsole();
                                    Story_CellBlockA();
                                    break;
                            }
                            break;
                        }

                    case "Cell B-5.":
                        AnsiConsole.MarkupLine("[orange1]You have found some supplies in this cell! You have found:\n\n- 2x Box of Doughnuts.\n- 1x Bottle of Water Labelled as Holy Water.\n- 3x Packet of Malteasers.[/]");

                        Player.BoxofDoughnuts += 2;
                        Player.BottleOfWaterLabelledHolyWater += 1;
                        Player.ExpiredPacketOfMalteasers += 3;
                        ExtFunctions.ContinuePrompt();

                        goto GOBACKHEREAFTERBATTLE;

                    case "Cell B-6":

                        // Initiate battle.

                        BattleSystem.EncounteredEnemy = BattleSystem.EnemyList[2];
                        AnsiConsole.MarkupLine($"[orange1]Upon entering {NewLocation}, [red]you have woken up {BattleSystem.EncounteredEnemy}![/] {BattleSystem.EncounteredEnemy} is now aggravated and ready to chop chop you.[/]");

                        ExtFunctions.ContinuePrompt();

                        BattleSystem.EnemyStatSetup();

                    goto GOBACKHEREAFTERBATTLE;
                }

                if (ObtainedKeyPadWire2)
                {
                    AnsiConsole.MarkupLine("[orange1]There is nothing else to do here...[/]");

                    ExtFunctions.ContinuePrompt();

                    if (ObtainedFuel)
                    {
                        ExtFunctions.CreateMenuPrompt(new[] { "Go to the Solitary Block.", "Go to Cell Block A." }, "What do you want to do?", true, false);

                        switch (ExtFunctions.ChosenOption)
                        {
                            case "Go to the Solitary Block.":
                                NewLocation = "Solitary";
                                PlayerIsMoving = true;
                                ExtFunctions.CleanConsole();
                                Story_Solitary1();
                                break;

                            case "Go to Cell Block A.":
                                NewLocation = "Cell Block A";
                                PlayerIsMoving = true;
                                ExtFunctions.CleanConsole();
                                Story_CellBlockA();
                                break;
                        }
                    }

                    else
                    {
                        ExtFunctions.CreateMenuPrompt(new[] { "Go to the Solitary Block.", "Go to the Shower Room.", "Go to Cell Block A." }, "What do you want to do?", true, false);

                        switch (ExtFunctions.ChosenOption)
                        {
                            case "Go to the Solitary Block.":
                                NewLocation = "Solitary";
                                PlayerIsMoving = true;
                                ExtFunctions.CleanConsole();
                                Story_Solitary1();
                                break;

                            case "Go to the Shower Room.":
                                NewLocation = "Shower Room";
                                PlayerIsMoving = true;
                                ExtFunctions.CleanConsole();
                                Story_Showers();
                                break;

                            case "Go to Cell Block A.":
                                NewLocation = "Cell Block A";
                                PlayerIsMoving = true;
                                ExtFunctions.CleanConsole();
                                Story_CellBlockA();
                                break;
                        }
                    }
                }
            }
        }

        public void Story_WeaponsRoom()
        {
            if (ObtainedKeycardLevel3)
            {
                CurrentLocation = "Weapons Room";

                ExtFunctions.DisplayCurrentLocation(CurrentLocation);
                ExtFunctions.SaveGame();

                SoundSystem.CheckSFXPlayer();
                SoundSystem.SFXSoundPlayer(SoundSystem.SFXSoundFileNames[5], PlaySFX);

                AnsiConsole.MarkupLine("[orange1]The door leading to the Weapons Room has been unlocked, as you have swiped the Level 3 Clearance Keycard in front of the card reader![/]");

                ExtFunctions.ContinuePrompt();

                AnsiConsole.MarkupLine("[orange1]You are now in the Weapons Room..[/]");

                ExtFunctions.CreateMenuPrompt(new[] { "Look around.", "Go back to the Corridor." }, "What would you like to do?", true, false);

                switch (ExtFunctions.ChosenOption)
                {
                    case "Look around.":
                        AnsiConsole.MarkupLine("[orange1]Looking around, made you draw your attention to a opened cardboard box, labelled 'wires'. Inside of it, you have found another piece of [cyan]wire[/]![/]");

                        ObtainedKeyPadWire3 = true;

                        ExtFunctions.ContinuePrompt();

                        AnsiConsole.MarkupLine("[orange1]There is nothing else to do here, so, you will go back to the Corridor.[/]");

                        ExtFunctions.ContinuePrompt();

                        NewLocation = "Corridor 1";
                        PlayerIsMoving = true;
                        ExtFunctions.CleanConsole();
                        Story_Corridor1();
                        break;
                        /*
                    case "Take the [green]Shotgun[/] and [green]Ammo[/]!":
                        AnsiConsole.MarkupLine("[orange1]You have obtained the [green]Shotgun[/] and [green]10 Shotgun Ammo[/]![/]");

                        Player.GotShotgun = true;
                        Player.ShotgunAmmo += 10;

                        //ObtainedShotgun = true;
                        //ObtainedShotgunAmmo = true;

                        ExtFunctions.ContinuePrompt();

                        if (ObtainedKeyPadWire3)
                        {
                            AnsiConsole.MarkupLine("[orange1]There is nothing else to do here...[/]");

                            ExtFunctions.ContinuePrompt();

                            AnsiConsole.MarkupLine("[orange1]You decide to go back to the Corridor...[/]");

                            ExtFunctions.ContinuePrompt();

                            NewLocation = "Corridor 1";
                            PlayerIsMoving = true;
                            ExtFunctions.CleanConsole();
                            Story_Corridor1();
                        }

                        else
                        {
                            Story_WeaponsRoom();
                        }
                        break;
                        */

                    case "Go back to the Corridor.":
                        AnsiConsole.MarkupLine("[orange1]You decide to go back to the Corridor...[/]");

                        ExtFunctions.ContinuePrompt();

                        NewLocation = "Corridor 1";
                        PlayerIsMoving = true;
                        ExtFunctions.CleanConsole();
                        Story_Corridor1();
                        break;
                }
            }

            else
            {
                AnsiConsole.MarkupLine("[orange1]The door leading to the Weapons Room is locked, and requires a [cyan]Level 3 Clearance Keycard[/] to open the door.[/]");

                ExtFunctions.ContinuePrompt();

                AnsiConsole.MarkupLine("[orange1]You decide to go back to the Corridor...[/]");

                ExtFunctions.ContinuePrompt();

                NewLocation = "Corridor 1";
                PlayerIsMoving = true;
                ExtFunctions.CleanConsole();
                Story_Corridor1();
            }
        }

        public void Story_Showers()
        {
            /*
             	-> Player sees the shower area and took a very small break, trying to gather his thoughts. He heads towards the sink and tried to open the tap to splash some water on his face. Only for blood to pour out from the tap, frightens the player, before the player shuts the tap valve.
				-> Player looks in the mirror and sees someone's torso coming towards the player and moans "Help me...".
				-> Player is petrified, and hides as the player hears someone else is coming, probably a guard. The guard sees the above the torso of a person, and eliminates it.
				-> Player can either investigate what it was, or explore around the room.
				-> Exploring the room causes the player to see a fuel can, that some fuel. Player thinks its useful to keep a hold of it.
				-> Player gets jumpscared by a monster, has a human body, arms and legs, but with a head of an axe's head.
				-> Player engages in a battle with it, due to nearby enemies, the player must decide between using a melee or a gun. Gun will attract other nearby enemies. Player defeats the enemy and decides to investigate the body that the guard eliminated.
				-> Player discovers the body was of an prison officer, who's name is illegible on his ID card. He carries a Level 2 Clearance Keycard.
				-> Player goes back to the Solitary to where the Player's friend is at.
             */

            //Console.Title = "Location: Shower Room";

            SoundSystem.CheckBGMPlayer();
            SoundSystem.StoryBGMPlayer(SoundSystem.StoryModeBGMFileNames[5], PlayBGM);

            CurrentLocation = "Shower Room";

            ExtFunctions.DisplayCurrentLocation(CurrentLocation);
            ExtFunctions.SaveGame();

            if (ShowerRoomGuardKilled == false)
            {
                AnsiConsole.MarkupLine("[orange1]As you made it to the Shower Room, you decide to take a break, trying to gather your thoughts about the situation, and what to make out of some of the things you have seen...\n\nYou go to the nearest sink and opened the tap to splash some water on your face, as a way to tell yourself to get a grip. Instead of the tap releasing water into the sink, the tap was pushing out blood that frightened you and forces you to shut it off.[/]");

                ExtFunctions.ContinuePrompt();

                AnsiConsole.MarkupLine("[orange1]As you look back up and into the stained mirror, you can see an upper body of a human, crawling towards you, moaning: 'Help me'.\n\nThe sight of that, terrifies you, so you decided to run away from it. This also attracted the attention of a guard who was making their way to the Shower Room.\n\nThe Guard sees it and eliminates it.[/]");

                ExtFunctions.CreateMenuPrompt(new[] { "Sneak attack the Guard.", "Wait until the Guard goes away.", "Throw something to distract him away from your current location." }, "What would you like to do?", true, false);

                switch (ExtFunctions.ChosenOption)
                {
                    case "Sneak attack the Guard.":
                        Random showerRoomGuardSneakAttackRnd = new();
                        int showerRoomGuardSneakAttack = showerRoomGuardSneakAttackRnd.Next(1, 10);

                        if (showerRoomGuardSneakAttack <= 5)
                        {
                            Random showerRoomGuardSneakAttackKORnd = new();
                            int showerRoomGuardSneakAttackKO = showerRoomGuardSneakAttackKORnd.Next(1, 10);

                            if (showerRoomGuardSneakAttackKO >= 7)
                            {
                                AnsiConsole.MarkupLine("[orange1]Your sneak attack managed to knock the guard out of his misery.[/]");

                                ExtFunctions.ContinuePrompt();

                                ShowerRoomGuardKilled = true;

                                goto SHOWERROOMGUARDKILLEDLOOPY;
                            }

                            else
                            {
                                AnsiConsole.MarkupLine("[orange1]You have managed to sneak attack the guard, taking a chunk of his health away![/]");

                                ExtFunctions.ContinuePrompt();

                                // Initiate battle.

                                BattleSystem.EncounteredEnemy = BattleSystem.EnemyList[3];

                                BattleSystem.EnemyStatSetup();

                                ShowerRoomGuardKilled = true;

                                goto SHOWERROOMGUARDKILLEDLOOPY;
                            }
                        }

                        else
                        {
                            AnsiConsole.MarkupLine("[orange1]You have failed to sneak attack the guard, and the guard has noticed you![/]");

                            ExtFunctions.ContinuePrompt();

                            // Initiate battle.

                            BattleSystem.EncounteredEnemy = BattleSystem.EnemyList[3];

                            BattleSystem.EnemyStatSetup();

                            ShowerRoomGuardKilled = true;

                            goto SHOWERROOMGUARDKILLEDLOOPY;
                        }

                    case "Wait until the Guard goes away.":
                        SoundSystem.CheckBGMPlayer();
                        SoundSystem.StoryBGMPlayer(SoundSystem.SFXSoundFileNames[0], PlayBGM);

                        AnsiConsole.MarkupLine("[orange1]You have decided to wait in the same spot, hoping for the Guard to go away from your current location.[/]");

                        ExtFunctions.ContinuePrompt();

                        Random showerRoomGuardWaitRnd = new();
                        int showerRoomGuardWait = showerRoomGuardWaitRnd.Next(1, 10);

                        if (showerRoomGuardWait >= 5)
                        {
                            SoundSystem.CheckBGMPlayer();
                            SoundSystem.CheckSFXPlayer();
                            SoundSystem.SFXSoundPlayer(SoundSystem.SFXSoundFileNames[6], PlaySFX);

                            AnsiConsole.MarkupLine("[orange1]The Guard has gone away from your current location![/]");

                            ExtFunctions.ContinuePrompt();
                            ExtFunctions.CleanConsole();

                            ShowerRoomGuardKilled = true;

                            Story_Showers();
                        }

                        else
                        {
                            AnsiConsole.MarkupLine("[orange1]The Guard has not gone away from your current location. So, you have decided to attack the Guard![/]");

                            SoundSystem.BGMPlayer.Stop();

                            ExtFunctions.ContinuePrompt();

                            // Initiates battle.

                            BattleSystem.EncounteredEnemy = BattleSystem.EnemyList[3];

                            BattleSystem.EnemyStatSetup();

                            ShowerRoomGuardKilled = true;

                            goto SHOWERROOMGUARDKILLEDLOOPY;
                        }

                        break;

                    case "Throw something to distract him away from your current location.":
                        Random showerRoomGuardDistractionRnd = new();
                        int showerRoomGuardDistraction = showerRoomGuardDistractionRnd.Next(1, 10);

                        if (showerRoomGuardDistraction <= 5)
                        {
                            AnsiConsole.MarkupLine("[orange1]You have been caught throwing an object in a close proximity to the Guard![/]");
                            SoundSystem.BGMPlayer.Stop();
                            ExtFunctions.ContinuePrompt();
                            BattleSystem.EncounteredEnemy = BattleSystem.EnemyList[3];
                            BattleSystem.EnemyStatSetup();
                            ShowerRoomGuardKilled = true;
                            goto SHOWERROOMGUARDKILLEDLOOPY;
                        }

                        else
                        {
                            AnsiConsole.MarkupLine("[orange1]You have successfully distracted the Guard away from your current location![/]");
                            ExtFunctions.ContinuePrompt();
                            ShowerRoomGuardKilled = true;
                            goto SHOWERROOMGUARDKILLEDLOOPY;
                        }
                }
            }

        SHOWERROOMGUARDKILLEDLOOPY:

            SoundSystem.CheckBGMPlayer();
            SoundSystem.CheckSFXPlayer();

            SoundSystem.StoryBGMPlayer(SoundSystem.StoryModeBGMFileNames[5], PlayBGM);

            while (ShowerRoomGuardKilled)
            {
                //Console.WriteLine("Obtained Fuel: " + ObtainedFuel);
                //Console.WriteLine("Obtained Keycard Level 2: " + ObtainedKeycardLevel2);

                while (ObtainedFuel == false || ObtainedKeycardLevel2 == false)
                {
                    ExtFunctions.CreateMenuPrompt(new[] { "Look around the Shower Room.", "Investigate the body." }, "What would you like to do?", true, false);

                    switch (ExtFunctions.ChosenOption)
                    {
                        case "Look around the Shower Room.":
                            // Discovers an enemy.

                            BattleSystem.EncounteredEnemy = BattleSystem.EnemyList[2];

                            AnsiConsole.MarkupLine($"[orange1]As you were looking around the Shower Room, you were jumpscared by {BattleSystem.EncounteredEnemy}![/]");

                            ExtFunctions.ContinuePrompt();

                            SoundSystem.BGMPlayer.Stop();
                            BattleSystem.EnemyStatSetup();

                            bool showerRoomMonsterKilled = true;

                            while (showerRoomMonsterKilled && ObtainedFuel == false)
                            {
                                AnsiConsole.MarkupLine("[orange1]After the battle, you found a [cyan]fuel can[/] sitting in the corner. You take the fuel just in case you need it.[/]");

                                ObtainedFuel = true;

                                ExtFunctions.ContinuePrompt();
                                goto SHOWERROOMGUARDKILLEDLOOPY;
                            }
                            break;


                        case "Investigate the body.":

                            ShowerRoomBodyExamined = true;

                            AnsiConsole.MarkupLine("[orange1]The thing that was crawling up to you, was an [cyan]officer[/]. He carries an ID badge that is barely legible. Upon looking into his pockets, you have found a [cyan]Level 2 Clearance Keycard[/].[/]");

                            ObtainedKeycardLevel2 = true;

                            ExtFunctions.ContinuePrompt();

                            goto SHOWERROOMGUARDKILLEDLOOPY;
                    }
                }

                while (ObtainedFuel && ObtainedKeycardLevel2 == false)
                {
                    AnsiConsole.MarkupLine("[orange1]The only thing left to do here, is to investigate the body...[/]");

                    ShowerRoomBodyExamined = true;

                    AnsiConsole.MarkupLine("[orange1]The thing that was crawling up to you, was an [cyan]officer[/]. He carries an ID badge that is barely legible. Upon looking into his pockets, you have found a [cyan]Level 2 Clearance Keycard[/].[/]");

                    ObtainedKeycardLevel2 = true;

                    ExtFunctions.ContinuePrompt();

                    goto SHOWERROOMGUARDKILLEDLOOPY;
                }

                while (ObtainedFuel && ObtainedKeycardLevel2 && ExecutionRoomVisited)
                {
                    AnsiConsole.MarkupLine("[orange1]There does not seem to be anything else, worth investigating here. Therefore, you are hurrying back to the Execution Room![/]");

                    ExtFunctions.ContinuePrompt();

                    NewLocation = "Execution Room";
                    PlayerIsMoving = true;
                    ExtFunctions.CleanConsole();
                    Story_ExecutionRoom();

                }

                while (ObtainedFuel && ObtainedKeycardLevel2 && ExecutionRoomFriendVisited == false)
                {
                    ExtFunctions.CreateMenuPrompt(new[] { "Go to the Execution Room.", "Go to the Solitary Block.", "Go to Cell Block A.", "Go to Cell Block B." }, "Where do you want to go?", true, false);

                    switch (ExtFunctions.ChosenOption)
                    {
                        case "Go to the Execution Room.":
                            NewLocation = "Execution Room";
                            PlayerIsMoving = true;
                            ExtFunctions.CleanConsole();
                            Story_ExecutionRoom();
                            break;

                        case "Go to the Solitary Block.":
                            NewLocation = "Solitary";
                            PlayerIsMoving = true;
                            ExtFunctions.CleanConsole();
                            Story_Solitary1();
                            break;

                        case "Go to Cell Block A.":
                            NewLocation = "Cell Block A";
                            PlayerIsMoving = true;
                            ExtFunctions.CleanConsole();
                            Story_CellBlockA();
                            break;

                        case "Go to Cell Block B.":
                            NewLocation = "Cell Block B";
                            PlayerIsMoving = true;
                            ExtFunctions.CleanConsole();
                            Story_CellBlockB();
                            break;
                    }
                }
            }
        }

        public void Story_Solitary1()
        {
            //Console.Title = "Location: Solitary";

            SoundSystem.CheckBGMPlayer();
            SoundSystem.StoryBGMPlayer(SoundSystem.StoryModeBGMFileNames[5], PlayBGM);

            CurrentLocation = "Solitary";

            ExtFunctions.DisplayCurrentLocation(CurrentLocation);
            ExtFunctions.SaveGame();

            while (SolitaryVisited == false || RescuedFriend1 == false)
            {
                AnsiConsole.MarkupLine("[orange1]As you past through the Solitary, you end up finding someone in one of the cells, unconscious...\n\nIt turns out that your [cyan]friend[/] has been locked up in there, however, he is placed on some sort of trap, but you cannot tell for sure.\n\nThe door requires a Level 2 Clearance Keycard to unlock the door.[/]");

                SoundSystem.CheckSFXPlayer();
                SoundSystem.SFXSoundPlayer(SoundSystem.SFXSoundFileNames[3], PlaySFX);

                ExtFunctions.ContinuePrompt();

                if (ObtainedKeycardLevel2)
                {
                    SoundSystem.CheckSFXPlayer();
                    SoundSystem.SFXSoundPlayer(SoundSystem.SFXSoundFileNames[5], PlaySFX);

                    Thread.Sleep(1000);

                    AnsiConsole.MarkupLine("[orange1]You have unlocked the door and went in.\n\nAs you got closer to your friend, the trap activated. The trap mimicked the Holy Cross, which was split into two. There were gears that aims to stretch the person on the cross until they are in agony as they are being stretched with tons of force, before the blades behind them pierces them.\n\nThat is exactly what has happened to your friend, as your friend screamed as loud as he can...[/]");

                    SoundSystem.CheckSFXPlayer();
                    SoundSystem.SFXSoundPlayer(SoundSystem.SFXSoundFileNames[7], PlaySFX);

                    ExtFunctions.ContinuePrompt();

                    SoundSystem.CheckBGMPlayer();
                    SoundSystem.StoryBGMPlayer(SoundSystem.StoryModeBGMFileNames[7], PlayBGM);

                    AnsiConsole.MarkupLine("[orange1]You have investigated the body to see if he has anything on him. When you did, you have found a device your friend has made, and calls the device [cyan]'NoiseMaker'[/], used to make noise and distract anyone within proximity. You do not know how this will help you, but you will take it.[/]");

                    ObtainedNoiseMaker = true;
                    RescuedFriend1 = true;

                    ExtFunctions.ContinuePrompt();

                    AnsiConsole.MarkupLine("[orange1]You made your way out from the Solitary Cell, and in front of you, [red]someone's blood is on the wall[/]. Two arrows, each has a destination name, these are:\n\nRight Arrow: Execution Room.\nLeft then Straight Arrow: Rec Room.[/]");

                    ExtFunctions.ContinuePrompt();

                    ExtFunctions.CreateMenuPrompt(new[] { "Rec Room.", "Execution Room." }, "Where do you want to go?", true, false);

                    switch (ExtFunctions.ChosenOption)
                    {
                        case "Rec Room.":
                            NewLocation = "Corridor 2";
                            PlayerIsMoving = true;
                            ExtFunctions.CleanConsole();
                            Story_Corridor2();
                            break;

                        case "Execution Room.":
                            NewLocation = "Execution Room";
                            PlayerIsMoving = true;
                            ExtFunctions.CleanConsole();
                            Story_ExecutionRoom();
                            break;
                    }
                }

                else
                {
                    SoundSystem.CheckSFXPlayer();
                    SoundSystem.SFXSoundPlayer(SoundSystem.SFXSoundFileNames[3], PlaySFX);

                    AnsiConsole.MarkupLine("[orange1]You cannot get inside, as you will need [cyan]Level 2 Clearance Keycard[/]![/]");

                    SolitaryVisited = true;

                    ExtFunctions.ContinuePrompt();

                    ExtFunctions.CreateMenuPrompt(new[] { "Shower Room.", "Cell Block A.", "Cell Block B." }, "Where do you want to go?", true, false);

                    switch (ExtFunctions.ChosenOption)
                    {
                        case "Shower Room.":
                            NewLocation = "Shower Room";
                            PlayerIsMoving = true;
                            ExtFunctions.CleanConsole();
                            Story_Showers();
                            break;

                        case "Cell Block A.":
                            NewLocation = "Cell Block A";
                            PlayerIsMoving = true;
                            ExtFunctions.CleanConsole();
                            Story_CellBlockA();
                            break;

                        case "Cell Block B.":
                            NewLocation = "Cell Block B";
                            PlayerIsMoving = true;
                            ExtFunctions.CleanConsole();
                            Story_CellBlockB();
                            break;
                    }
                }
            }

            if (SolitaryVisited && RescuedFriend1 == false)
            {
                AnsiConsole.MarkupLine("[orange1]Now that you have got the Level 2 Clearance Keycard, you can open the door![/]");

                ExtFunctions.ContinuePrompt();

                if (ObtainedKeycardLevel2)
                {
                    SoundSystem.CheckSFXPlayer();
                    SoundSystem.SFXSoundPlayer(SoundSystem.SFXSoundFileNames[5], PlaySFX);

                    AnsiConsole.MarkupLine("[orange1]You have unlocked the door and went in.\n\nAs you got closer to your friend, the trap activated. The trap mimicked the Holy Cross, which was split into two. There were gears that aims to stretch the person on the cross until they are in agony as they are being stretched with tons of force, before the blades behind them pierces them.\n\nThat is exactly what has happened to your friend, as your friend screamed as loud as he can...[/]");

                    ExtFunctions.ContinuePrompt();

                    SoundSystem.CheckBGMPlayer();
                    SoundSystem.StoryBGMPlayer(SoundSystem.BattleBGMFileNames[7], PlayBGM);

                    AnsiConsole.MarkupLine("[orange1]You have investigated the body to see if he has anything on him. When you did, you have found a device your friend has made, and calls the device [cyan]'NoiseMaker'[/], used to make noise and distract anyone within proximity. You do not know how this will help you, but you will take it.[/]");

                    ObtainedNoiseMaker = true;
                    RescuedFriend1 = true;

                    ExtFunctions.ContinuePrompt();

                    AnsiConsole.MarkupLine("[orange1]You made your way out from the Solitary Cell, and in front of you, [red]someone's blood is on the wall[/]. Two arrows, each has a destination name, these are:\n\nRight Arrow: Execution Room.\nLeft then Straight Arrow: Rec Room.[/]");

                    ExtFunctions.ContinuePrompt();

                    ExtFunctions.CreateMenuPrompt(new[] { "Rec Room.", "Execution Room." }, "Where do you want to go?", true, false);

                    switch (ExtFunctions.ChosenOption)
                    {
                        case "Rec Room.":
                            NewLocation = "Corridor 2";
                            PlayerIsMoving = true;
                            ExtFunctions.CleanConsole();
                            Story_Corridor2();
                            break;

                        case "Execution Room.":
                            NewLocation = "Execution Room";
                            PlayerIsMoving = true;
                            ExtFunctions.CleanConsole();
                            Story_Corridor1();
                            break;
                    }
                }
            }

            if (RescuedFriend1)
            {
                ExtFunctions.CreateMenuPrompt(new[] { "Go to the Rec Room.", "Go to the Execution Room." }, "Where do you want to go?", true, false);

                switch (ExtFunctions.ChosenOption)
                {
                    case "Go to the Rec Room.":
                        NewLocation = "Corridor 2";
                        PlayerIsMoving = true;
                        ExtFunctions.CleanConsole();
                        Story_Corridor2();
                        break;

                    case "Go to the Execution Room.":
                        NewLocation = "Execution Room";
                        PlayerIsMoving = true;
                        ExtFunctions.CleanConsole();
                        Story_ExecutionRoom();
                        break;
                }
            }
        }

        public void Story_ExecutionRoom()
        {
            CurrentLocation = "Execution Room";

            ExtFunctions.DisplayCurrentLocation(CurrentLocation);
            ExtFunctions.SaveGame();

            if (ExecutionRoomFriendVisited == false)
            {
                AnsiConsole.MarkupLine("[orange1]Another friend in your group, is strapped onto a bloodied chair, with several knives impaled at the friend. Your friend is unconscious, but you have the chance to rescue him.[/]");

                ExtFunctions.ContinuePrompt();

                if (ObtainedKeycardLevel2)
                {
                    //Console.Title = "Location: Execution Room";

                    AnsiConsole.MarkupLine("[orange1]Once inside, you see your friend severely injured, but require immediate first aid, you need to get into the infirmary to get: [cyan]Bandages and First Aid[/].[/]");

                    ExtFunctions.ContinuePrompt();

                    if (ObtainedFirstAid && ObtainedBandages)
                    {
                        AnsiConsole.MarkupLine("[orange1]You have used the [cyan]Bandages and First Aid[/] on your friend. After a few minutes, he regains consciousness. You have told your friend that you will take him to a safer place....[/]");

                        ExtFunctions.ContinuePrompt();

                        AnsiConsole.MarkupLine("[orange1]As you were assisting him to move, you heard someone was talking. Sounds like a group of people...\n\nThey looked like those Guards that were lurking around....[/]");

                        ExtFunctions.ContinuePrompt();

                        AnsiConsole.MarkupLine("[orange1]With nowhere else to go, you both got caught by the Guards. But they seemed like they were higher on the hierarchy. They had more advanced weaponry and protective clothing.\n\nThey slammed a metal pipe across both you and your friend's head, knocking you both out...[/]");

                        ExtFunctions.ContinuePrompt();

                        NewLocation = "Torture Room";
                        PlayerIsMoving = true;
                        ExtFunctions.CleanConsole();
                        Story_TortureRoom();
                    }

                    else
                    {
                        AnsiConsole.MarkupLine("[orange1]Your friend is severely injured from what looks to be a multiple stab wounds with abnormally large knives. There is some Hydrogen Peroxide that is used for wound cleaning, which is what you are going to need.\n\nSome other key medic items you need are:\n\n[cyan]- First Aid.\n- Bandages[/].[/]");

                        ExtFunctions.ContinuePrompt();

                        AnsiConsole.MarkupLine("[orange1]You have decided to go back to the Corridor to see if you can find any of the items that you need, in the Infirmary.[/]");

                        ExtFunctions.ContinuePrompt();

                        NewLocation = "Corridor 1";
                        PlayerIsMoving = true;
                        ExtFunctions.CleanConsole();
                        Story_Corridor1();
                    }

                    if (ObtainedFirstAid && ObtainedBandages == false)
                    {
                        AnsiConsole.MarkupLine("[orange1]Your friend is severely injured from what looks to be a multiple stab wounds with abnormally large knives. There is some Hydrogen Peroxide that is used for wound cleaning, which is what you are going to need.\n\nSome other key medic items you need are:\n\n[cyan]- First Aid [[ALREADY OBTAINED]].\n- Bandages[/].[/]");

                        ExtFunctions.ContinuePrompt();

                        AnsiConsole.MarkupLine("[orange1]You have decided to go back to the Corridor to see if you can find the [cyan]Bandages[/] that you need, in the Infirmary.[/]");

                        ExtFunctions.ContinuePrompt();

                        NewLocation = "Corridor 1";
                        PlayerIsMoving = true;
                        ExtFunctions.CleanConsole();
                        Story_Corridor1();
                    }
                    
                    else if (ObtainedFirstAid == false && ObtainedBandages)
                    {
                        AnsiConsole.MarkupLine("[orange1]Your friend is severely injured from what looks to be a multiple stab wounds with abnormally large knives. There is some Hydrogen Peroxide that is used for wound cleaning, which is what you are going to need.\n\nSome other key medic items you need are:\n\n[cyan]- First Aid.\n- Bandages [[ALREADY OBTAINED]][/].[/]");

                        ExtFunctions.ContinuePrompt();

                        AnsiConsole.MarkupLine("[orange1]You have decided to go back to the Corridor to see if you can find the First Aid that you need, in the Infirmary.[/]");

                        ExtFunctions.ContinuePrompt();

                        NewLocation = "Corridor 1";
                        PlayerIsMoving = true;
                        ExtFunctions.CleanConsole();
                        Story_Corridor1();
                    }
                }

                else
                {
                    SoundSystem.CheckSFXPlayer();
                    SoundSystem.SFXSoundPlayer(SoundSystem.SFXSoundFileNames[3], PlaySFX);

                    AnsiConsole.MarkupLine("[orange1]You cannot get inside with the [cyan]Level 2 Clearance Keycard[/]. You need to go and find it...[/]");

                    ExecutionRoomVisited = true;

                    ExtFunctions.ContinuePrompt();

                    ExtFunctions.CreateMenuPrompt(new[] { "Go to the Shower Room.", "Go to the Rec Room.", "Go to the Corridor leading the to Infirmary." }, "Where do you want to go?", true, false);

                    switch (ExtFunctions.ChosenOption)
                    {
                        case "Go to the Shower Room.":
                            NewLocation = "Shower Room";
                            PlayerIsMoving = true;
                            ExtFunctions.CleanConsole();
                            Story_Showers();
                            break;

                        case "Go to the Rec Room.":
                            NewLocation = "Rec Room";
                            PlayerIsMoving = true;
                            ExtFunctions.CleanConsole();
                            Story_RecRoom();
                            break;

                        case "Go to the Corridor leading the to Infirmary.":
                            NewLocation = "Corridor 1";
                            PlayerIsMoving = true;
                            ExtFunctions.CleanConsole();
                            Story_Corridor1();
                            break;
                    }
                }
            }
        }

        public void Story_TortureRoom()
        {
            SoundSystem.CheckBGMPlayer();
            SoundSystem.StoryBGMPlayer(SoundSystem.StoryModeBGMFileNames[8], PlayBGM);

            CurrentLocation = "The Torture Room";
            CurrentObjective = "???";

            ExtFunctions.DisplayCurrentLocation(CurrentLocation);
            ExtFunctions.SaveGame();

            AnsiConsole.MarkupLine("[orange1]You wake up strapped to an operating bed, while your friend is on the other operating bed. You look around and see giant circular saw, drills, chainsaws, all bloodied. Stains of blood surround the room, and the operating beds.[/]");

            ExtFunctions.ContinuePrompt();

            if (UnidentifiedPeopleIdentified) 
            {
                AnsiConsole.MarkupLine("[orange1]The cult's guards came, with a woman, who looked like the mayor of the town. Back when there was a civilisation.[/]");
                ExtFunctions.ContinuePrompt();
            }

            else
            {
                AnsiConsole.MarkupLine("[orange1]The same unidentified people came, with a woman, who looked like the mayor of the town. Back when there was a civilisation.[/]");
                ExtFunctions.ContinuePrompt();
            }

            AnsiConsole.MarkupLine("[bold][red]> ???:[/][/] (To her Guards) Are those our poor lost souls?\n");

            AnsiConsole.MarkupLine("[bold][red]> ???:[/][/] Very well.\n");

            /*  DETERMINES WHO THE PERSON IS.   // 1 = Mayor, 2 = Headmaster, 3 = Doctor.
            Random TypeofPersonRND = new Random();

            int TypeofPerson = TypeofPersonRND.Next(1, 4);

            switch (TypeofPerson)
            {
                    
            }
            */

            ExtFunctions.ContinuePrompt();

            AnsiConsole.MarkupLine("[orange1]As she stepped towards the light, you, instantly recognised who she was. She was your headmaster; Judy.[/]\n");

            ExtFunctions.ContinuePrompt();

            AnsiConsole.MarkupLine($"[bold][red]> {PlayerNameFinalised}:[/][/] [orange1]Judy! What is going on here?[/]\n");

            ExtFunctions.ContinuePrompt();

            AnsiConsole.MarkupLine($"[bold][red]> Judy:[/][/] {PlayerNameFinalised}, do you know why the world is the way it is? Do you know, what is going on around us?\n");

            ExtFunctions.ContinuePrompt();

            AnsiConsole.MarkupLine($"[bold][red]> {PlayerNameFinalised}:[/][/] No, I don't. What do you mean by that? What is going on here?!\n");

            ExtFunctions.ContinuePrompt();

            AnsiConsole.MarkupLine($"[bold][red]> Judy - The Cult Leader:[/][/] Of course you don't. You see; we bring people here, lost and filthy souls, that have committed a lot of sin. Sin, that we cannot afford to look past. THEY won't understand the consequences of committing sin. Moreover, they don't seek forgiveness or confess unless they have been caught.\n");

            ExtFunctions.ContinuePrompt();

            AnsiConsole.MarkupLine("[bold][red]> Judy - The Cult Leader:[/][/] So, we made a pact. A pact, where we will eliminate, or more like, sacrifice these poor fools to the Gods. As long as we fulfill the pact, we will be able to live our lives as normal. And to do this, we formed an order, called The Ordinary. Designed to let the sin, the unhealthy, the evil do'ers, and the diseased, be eradicated, while the healthy and the clean, lives.\n\nThe unhealthy and diseased have already been punished by the Gods. Why else would they be diseased? They are not allowed to live. They are tainted.");

            ExtFunctions.ContinuePrompt();

            AnsiConsole.MarkupLine($"[bold][red]> {PlayerNameFinalised}:[/][/] What? You're with them?! Anyone with a brain cell, wouldn't believe this crap? What makes you believe by sacrificing the 'poor souls', the 'unhealthy and diseased' people, to the so called 'Gods' would solve this? Are you overdosing yourself on a drug?");

            ExtFunctions.ContinuePrompt();

            SoundSystem.CheckSFXPlayer();
            SoundSystem.SFXSoundPlayer(SoundSystem.SFXSoundFileNames[16], PlaySFX);

            // Player gets punished, loses 25 Max HP.

            Random slapDMGRND = new();

            int slapDMG = slapDMGRND.Next(4, 25);

            AnsiConsole.MarkupLine($"[orange1]Your comment has made Judge Judy [red]irritated[/]. She slaps you and used a knife to embed a moderate cut into your skin. Your Maximum HP has decreased by {slapDMG} HP.[/]");

            Player.PlayerMaxHealth -= slapDMG;

            ExtFunctions.ContinuePrompt();

            AnsiConsole.MarkupLine("[bold][red]> Judy - The Cult Leader:[/][/] Enough of your insolence! There are so many people just like you, riddled with years of cumulative sins. It is the fault of people like YOU, that cause this hell of a curse to be inflicted upon this world.");

            ExtFunctions.ContinuePrompt();

            AnsiConsole.MarkupLine($"[bold][red]> Judy - The Cult Leader:[/][/] Since the bombs fell, everyone became a savage. That uptick of filthy souls was the reason why the Gods punished us, and brought the darkness to this world. Now, it is my responsibility to put an end to this, and restore the order of things, starting with YOU, {PlayerNameFinalised}, and YOUR FRIEND over there. You both, needs to be cleansed.");

            ExtFunctions.ContinuePrompt();

            AnsiConsole.MarkupLine($"[bold][red]> {PlayerNameFinalised}:[/][/] Cleansed? What do you mean by 'Cleaned'? What are you going to do with us?");

            ExtFunctions.ContinuePrompt();

            AnsiConsole.MarkupLine("[bold][red]> Judy - The Cult Leader:[/][/] Watch.");

            ExtFunctions.ContinuePrompt();

            AnsiConsole.MarkupLine($"[bold][red]> {PlayerNameFinalised}:[/][/] Whatever you are going to do, don't do it!");

            ExtFunctions.ContinuePrompt();

            AnsiConsole.MarkupLine("[orange1]Judy orders her guards to flip the operating table, so, you are lying upwards, facing towards you friends. She, then, picks up a butcher's cleaver.[/]");

            ExtFunctions.ContinuePrompt();

            AnsiConsole.MarkupLine("[orange1]Judy makes her way to your friend, while your friend looks frantic and was sweating from the fear he had inside... The Guards put a strap over your friend's mouth, shortly after, Judy cuts your friend open, with the cleaver. Your friend was screaming, as the cut was getting deeper and bigger. You felt helpless, as you have watched...[/]");

            ExtFunctions.ContinuePrompt();

            AnsiConsole.MarkupLine($"[bold][red]> Judy - The Cult Leader:[/][/] This is how we cleanse people like YOU, {PlayerNameFinalised}. Only then, the Gods will be pleased, that there is an order established to help them cleanse the world from sin, and everything can go back to normal.");

            ExtFunctions.ContinuePrompt();

            SoundSystem.CheckSFXPlayer();
            SoundSystem.SFXSoundPlayer(SoundSystem.SFXSoundFileNames[8], PlaySFX);

            AnsiConsole.MarkupLine("[orange1]A guard came running, gaining the attention of Judy, and the other Guards...[/]");

            AnsiConsole.MarkupLine("[bold][red]> Guard:[/][/] We need to get out of here, there are more monsters coming here. We have to take you to safety.");

            ExtFunctions.ContinuePrompt();

            AnsiConsole.MarkupLine($"[bold][red]> Judy - The Cult Leader:[/][/] Very well. We will continue this later. We will be back for you, {PlayerNameFinalised}. Who knows, maybe those demons would finish the job for us.");

            ExtFunctions.ContinuePrompt();

            AnsiConsole.MarkupLine("[orange1]Judy and the Guards left, leaving you and your friend behind, with the operating table flipped over, and your friend, dead... The state that your friend was in made you feel sick as you can see your friend's internal organs protruding.[/]");

            // Player's FEAR meter increases.

            Player.PlayerFear += 25;

            ExtFunctions.ContinuePrompt();

            AnsiConsole.MarkupLine("[orange1]After rattling one of the straps, the strap start to loosen up. Eventually, it fell apart from the table. You needed to get out, and continue your main objective of escaping from the cult and this prison.[/]");

            ExtFunctions.ContinuePrompt();

            ExtFunctions.CreateMenuPrompt(new[] { "Make your way to the Rec Room.", "Go back to the Solitary." }, "Where do you want to go?", true, false);

            switch (ExtFunctions.ChosenOption)
            {
                case "Make your way to the Rec Room.":
                    NewLocation = "Corridor 2";
                    PlayerIsMoving = true;
                    ExtFunctions.CleanConsole();
                    EscapedFromTortureChamber = true;
                    Story_Corridor2();
                    break;

                case "Go back to the Solitary.":
                    NewLocation = "Solitary";
                    PlayerIsMoving = true;
                    ExtFunctions.CleanConsole();
                    EscapedFromTortureChamber = true;
                    Story_Solitary2();
                    break;
            }

            /*
                -> Continuation from the Execution Room. You wake up strapped to an operating bed, while your friend is on the other operating bed. You look around and see giant circular saw, drills, chainsaws, all bloodied. Stains of blood surround the room, and the operating beds.
				-> The same unidentified people come, with a woman, who look like the mayor of the town, back when there was a civilisation.
				-> Introduces herself and her peers as a part of a cult; who are named "The Ordinary" - made and designed to wash away sin by bloodletting and committing genocide to people who are unhealthy, unfit, and have medical issues. The cult believes God has punished these people for their wrongdoing and sins that they have committed, however, they continue to do more evil that they have cursed everyone in the town.
				-> The cult leader elaborates how she firmly believes the player is sinful and deserves to be punished, and that the player was caught by one of the Guards. She also believes the same thing about your friend but in much more of a spiteful manner.
				-> The Guards flip over the operating tables, making the player and the friend face upwards as if they were standing. The cult leader dismissed the guards, and proceeded to grab the drill, turned it on and faced me toward my friend.
				-> Cult leader went to the friend, and proceeded to drill into the friend's eye.
				-> Friend dies, and the player was going to be next, until the Guards came in to alert the cult leader to evacuate as the "monsters" were seen nearby and that they need to go.
				-> She goes, you manage to unstrap yourself. You needed to get out, from now on, the monster spawn frequency increases.
				-> The Rec Room is located upstairs, while the stairs is at the end of the corridor (if the player has yet to go there).
				-> If the player has been to the Rec Room, they would need to find the rest of key items.
				-> Enemies will spawn more.
             */
        }

        public void Story_Corridor2()
        {
            //Console.Title = "Location: Corridor, leading to the Rec Room";
            CurrentLocation = "Corridor, leading to the Rec Room";
            CurrentObjective = "";

            ExtFunctions.DisplayCurrentLocation(CurrentLocation);
            ExtFunctions.SaveGame();

            SoundSystem.CheckBGMPlayer();
            SoundSystem.StoryBGMPlayer(SoundSystem.StoryModeBGMFileNames[9], PlayBGM);

            if (EscapedFromTortureChamber & RecRoomVisited)
            {
                ExtFunctions.EnemySpawnRandomiser(EscapedFromTortureChamber);

                ExtFunctions.CreateMenuPrompt(new[] { "Go to the Rec Room.", "Go to the Solitary Block."}, "Where do you want to go?", true, false);

                switch (ExtFunctions.ChosenOption)
                {
                    case "Go to the Rec Room.":
                        NewLocation = "Rec Room";
                        PlayerIsMoving = true;
                        ExtFunctions.CleanConsole();
                        Story_RecRoom();
                        break;

                    case "Go to the Solitary Block.":
                        NewLocation = "Solitary";
                        PlayerIsMoving = true;
                        ExtFunctions.CleanConsole();
                        Story_Solitary2();
                        break;
                }
            }

            else if (RecRoomVisited == false)
            {
                AnsiConsole.MarkupLine("[orange1]As you were making your way to the Rec Room, you see some guard patrolling inside and outside the Rec Room.[/]");

                ExtFunctions.ContinuePrompt();

                if (ObtainedNoiseMaker)
                {
                    AnsiConsole.MarkupLine("[cyan]You have the Noisemaker that you can use to draw them out![/]");

                    ExtFunctions.ContinuePrompt();

                    ExtFunctions.CreateMenuPrompt(new[] { "Use the Noisemaker.", "Fight the patrolling guards." }, "What would you like to do?", true, false);

                    switch (ExtFunctions.ChosenOption)
                    {
                        case "Use the Noisemaker.":
                            ExtFunctions.CleanConsole();
                            Story_UsedNoiseMaker();
                            break;

                        case "Fight the patrolling guards.":
                            ExtFunctions.CleanConsole();
                            Story_FoughtGuards();
                            break;
                    }
                }
            }            

            void Story_UsedNoiseMaker()
            {
                AnsiConsole.MarkupLine("[orange1]You have used the [cyan]Noisemaker[/], by activating the device before throwing it down the stairs. You take cover behind a group of cardboard boxes, so you can sneak into the Rec Room.[/]");

                ExtFunctions.ContinuePrompt();

                //CurrentLocation = "Corridor, leading to the Rec Room";
                PlayerIsMoving = true;
                NewLocation = "Rec Room";
                ExtFunctions.CleanConsole();
                Story_RecRoom();
            }

            void Story_FoughtGuards()
            {
                AnsiConsole.MarkupLine("[orange1]As you ran towards the guards, you gave them a little scare. They are now aggravated and ready to fight.[/]");

                ExtFunctions.ContinuePrompt();

                BattleSystem.EncounteredEnemy = BattleSystem.EnemyList[3];

                BattleSystem.EnemyStatSetup();

                // INITIATES BATTLE.

                NewLocation = "Rec Room";
                PlayerIsMoving = true;
                ExtFunctions.CleanConsole();
                Story_RecRoom();
            }
        }

        public void Story_RecRoom()
        {
            RecRoomVisited = true;

            /*
                -> Has been turned in a "safe room" for the unidentified group of people who is using the prison as their base. Once discovered, the unidentified group of people will be patrolling the prison, if they detect something is off, they will set up traps or will survillance the area.
				-> Contains the Level 3 Clearence keycard.
				-> Heavily guarded.
				-> Contains weapons; assualt rifle, bulletproof vest.
				    -> Can use the Noisemaker to throw downstairs, drawing the attention from the Guards.
				-> Power can be restored within the Rec Room, by using the fuel found in the showers area.
				-> Contains the map, informing the player the locations of the wires, keycard, and the keycode.
				-> Keycode location will not be revealed if the player has already been inside of the Canteen.             
             */

            //Console.Title = "Location: The Rec Room";

            CurrentLocation = "The Rec Room";
            CurrentObjective = "Look around the room.";

            ExtFunctions.DisplayCurrentLocation(CurrentLocation);
            ExtFunctions.SaveGame();

            AnsiConsole.MarkupLine("[orange1]You are now in the [cyan]Rec Room[/]. This has been turned in a safe room for the Guards, to allow them to patrol the Prison and monitor the situation from here.[/]");

            ExtFunctions.ContinuePrompt();

            if (ObtainedMap == false)
            {
                AnsiConsole.MarkupLine("[orange1]The first thing you see, is a map of the prison, on the table in front of you. It seems to be revealing key items that you need to escape from the prison.[/]");

                ExtFunctions.ContinuePrompt();

                ObtainedMap = true;

                AnsiConsole.MarkupLine("[orange1]You have obtained the [cyan]map[/]! The map details what you need to get out of here. It also suggests that someone else was trying to escape from here...[/]");


                //  IMPLEMENT THE MAP FUNCTIONALITY HERE.   //
                if (ObtainedMap)
                {
                    //      CONDITIONAL STATEMENT SPAM INCOMING     //
                    //      VIEWER DISCRETION IS ADVISED.           //

                    if (ObtainedKeyPadWire1 == false && ObtainedKeyPadWire2 == false && ObtainedKeyPadWire3 == false && ObtainedKeycardLevel2 == false && ObtainedKeycardLevel3 == false && ObtainedFuel == false)
                    {
                        AnsiConsole.MarkupLine("[orange1]The map shows that the following key items and their locations:\n- [cyan]Keycard Level 2[/] (Located in the Solitary Cell).\n- [cyan]Keycard Level 3[/] (Located in the Rec Room).\n- [cyan]KeyPad Wires[/] (Located in the Cell Block A, Cell Block B, and Weapons Room).\n- [cyan]Fuel[/] (Located in the Shower Room).[/]");

                        ExtFunctions.ContinuePrompt();
                    }

                    else if (ObtainedKeyPadWire1 && ObtainedKeyPadWire2 == false && ObtainedKeyPadWire3 == false && ObtainedKeycardLevel2 == false && ObtainedKeycardLevel3 == false && ObtainedFuel == false)
                    {
                        AnsiConsole.MarkupLine("[orange1]The map shows that the following key items and their locations:\n- [cyan]Keycard Level 2[/] (Located in the Solitary Cell).\n- [cyan]Keycard Level 3[/] (Located in the Rec Room).\n- [cyan]KeyPad Wires[/] (Located in the Cell Block B, and Weapons Room).\n- [cyan]Fuel[/] (Located in the Shower Room).[/]");

                        ExtFunctions.ContinuePrompt();
                    }

                    else if (ObtainedKeyPadWire1 == false && ObtainedKeyPadWire2 && ObtainedKeyPadWire3 == false && ObtainedKeycardLevel2 == false && ObtainedKeycardLevel3 == false && ObtainedFuel == false)
                    {
                        AnsiConsole.MarkupLine("[orange1]The map shows that the following key items and their locations:\n- [cyan]Keycard Level 2[/] (Located in the Solitary Cell).\n- [cyan]Keycard Level 3[/] (Located in the Rec Room).\n- [cyan]KeyPad Wires[/] (Located in the Cell Block A, and Weapons Room).\n- [cyan]Fuel[/] (Located in the Shower Room).[/]");

                        ExtFunctions.ContinuePrompt();
                    }

                    else if (ObtainedKeyPadWire1 == false && ObtainedKeyPadWire2 == false && ObtainedKeyPadWire3 && ObtainedKeycardLevel2 == false && ObtainedKeycardLevel3 == false && ObtainedFuel == false)
                    {
                        AnsiConsole.MarkupLine("[orange1]The map shows that the following key items and their locations:\n- [cyan]Keycard Level 2[/] (Located in the Solitary Cell).\n- [cyan]Keycard Level 3[/] (Located in the Rec Room).\n- [cyan]KeyPad Wires[/] (Located in the Cell Block A, and Cell Block B).\n- [cyan]Fuel[/] (Located in the Shower Room).[/]");

                        ExtFunctions.ContinuePrompt();
                    }

                    else if (ObtainedKeyPadWire1 == false && ObtainedKeyPadWire2 == false && ObtainedKeyPadWire3 == false && ObtainedKeycardLevel2 && ObtainedKeycardLevel3 == false && ObtainedFuel == false)
                    {
                        AnsiConsole.MarkupLine("[orange1]The map shows that the following key items and their locations:\n- [cyan]Keycard Level 3[/] (Located in the Rec Room).\n- [cyan]KeyPad Wires[/] (Located in the Cell Block A, Cell Block B, and Weapons Room).\n- [cyan]Fuel[/] (Located in the Shower Room).[/]");

                        ExtFunctions.ContinuePrompt();
                    }

                    else if (ObtainedKeyPadWire1 == false && ObtainedKeyPadWire2 == false && ObtainedKeyPadWire3 == false && ObtainedKeycardLevel2 == false && ObtainedKeycardLevel3 && ObtainedFuel == false)
                    {
                        AnsiConsole.MarkupLine("[orange1]The map shows that the following key items and their locations:\n- [cyan]Keycard Level 2[/] (Located in the Solitary Cell).\n- [cyan]KeyPad Wires[/] (Located in the Cell Block A, Cell Block B, and Weapons Room).\n- [cyan]Fuel[/] (Located in the Shower Room).[/]");

                        ExtFunctions.ContinuePrompt();
                    }

                    else if (ObtainedKeyPadWire1 == false && ObtainedKeyPadWire2 == false && ObtainedKeyPadWire3 == false && ObtainedKeycardLevel2 == false && ObtainedKeycardLevel3 == false && ObtainedFuel)
                    {
                        AnsiConsole.MarkupLine("[orange1]The map shows that the following key items and their locations:\n- [cyan]Keycard Level 2[/] (Located in the Solitary Cell).\n- [cyan]Keycard Level 3[/] (Located in the Rec Room).\n- [cyan]KeyPad Wires[/] (Located in the Cell Block A, Cell Block B, and Weapons Room).[/]");

                        ExtFunctions.ContinuePrompt();
                    }

                    else if (ObtainedKeyPadWire1 && ObtainedKeyPadWire2 && ObtainedKeyPadWire3 == false && ObtainedKeycardLevel2 == false && ObtainedKeycardLevel3 == false && ObtainedFuel == false)
                    {
                        AnsiConsole.MarkupLine("[orange1]The map shows that the following key items and their locations:\n- [cyan]Keycard Level 2[/] (Located in the Solitary Cell).\n- [cyan]Keycard Level 3[/] (Located in the Rec Room).\n- [cyan]KeyPad Wires[/] (Located in the Weapons Room).\n- [cyan]Fuel[/] (Located in the Shower Room).[/]");

                        ExtFunctions.ContinuePrompt();
                    }

                    else if (ObtainedKeyPadWire1 && ObtainedKeyPadWire2 == false && ObtainedKeyPadWire3 && ObtainedKeycardLevel2 == false && ObtainedKeycardLevel3 == false && ObtainedFuel == false)
                    {
                        AnsiConsole.MarkupLine("[orange1]The map shows that the following key items and their locations:\n- [cyan]Keycard Level 2[/] (Located in the Solitary Cell).\n- [cyan]Keycard Level 3[/] (Located in the Rec Room).\n- [cyan]KeyPad Wires[/] (Located in the Cell Block B).\n- [cyan]Fuel[/] (Located in the Shower Room).[/]");

                        ExtFunctions.ContinuePrompt();
                    }

                    else if (ObtainedKeyPadWire1 && ObtainedKeyPadWire2 == false && ObtainedKeyPadWire3 == false && ObtainedKeycardLevel2 && ObtainedKeycardLevel3 == false && ObtainedFuel == false)
                    {
                        AnsiConsole.MarkupLine("[orange1]The map shows that the following key items and their locations:\n- [cyan]Keycard Level 3[/] (Located in the Rec Room).\n- [cyan]KeyPad Wires[/] (Located in the Cell Block B, and Weapons Room).\n- [cyan]Fuel[/] (Located in the Shower Room).[/]");

                        ExtFunctions.ContinuePrompt();
                    }

                    else if (ObtainedKeyPadWire1 && ObtainedKeyPadWire2 == false && ObtainedKeyPadWire3 == false && ObtainedKeycardLevel2 == false && ObtainedKeycardLevel3 && ObtainedFuel == false)
                    {
                        AnsiConsole.MarkupLine("[orange1]The map shows that the following key items and their locations:\n- [cyan]KeyPad Wires[/] (Located in the Cell Block B, and Weapons Room).\n- [cyan]Fuel[/] (Located in the Shower Room).[/]");

                        ExtFunctions.ContinuePrompt();
                    }

                    else if (ObtainedKeyPadWire1 && ObtainedKeyPadWire2 == false && ObtainedKeyPadWire3 == false && ObtainedKeycardLevel2 == false && ObtainedKeycardLevel3 == false && ObtainedFuel)
                    {
                        AnsiConsole.MarkupLine("[orange1]The map shows that the following key items and their locations:\n- [cyan]Keycard Level 2[/] (Located in the Solitary Cell).\n- [cyan]Keycard Level 3[/] (Located in the Rec Room).\n- [cyan]KeyPad Wires[/] (Located in the Weapons Room).[/]");

                        ExtFunctions.ContinuePrompt();
                    }

                    else if (ObtainedKeyPadWire1 && ObtainedKeyPadWire2 && ObtainedKeyPadWire3 && ObtainedKeycardLevel2 == false && ObtainedKeycardLevel3 == false && ObtainedFuel == false)
                    {
                        AnsiConsole.MarkupLine("[orange1]The map shows that the following key items and their locations:\n- [cyan]Keycard Level 2[/] (Located in the Solitary Cell).\n- [cyan]Keycard Level 3[/] (Located in the Rec Room).[/]");

                        ExtFunctions.ContinuePrompt();
                    }

                    else if (ObtainedKeyPadWire1 && ObtainedKeyPadWire2 && ObtainedKeyPadWire3 == false && ObtainedKeycardLevel2 && ObtainedKeycardLevel3 == false && ObtainedFuel == false)
                    {
                        AnsiConsole.MarkupLine("[orange1]The map shows that the following key items and their locations:\n[cyan]Keycard Level 3[/] (Located in the Rec Room).\n- [cyan]KeyPad Wires[/] (Located in the Weapons Room).\n- [cyan]Fuel[/] (Located in the Shower Room).[/]");

                        ExtFunctions.ContinuePrompt();
                    }
                    
                    else if (ObtainedKeyPadWire1 && ObtainedKeyPadWire2 && ObtainedKeyPadWire3 == false && ObtainedKeycardLevel2 == false && ObtainedKeycardLevel3 && ObtainedFuel == false)
                    {
                        AnsiConsole.MarkupLine("[orange1]The map shows that the following key items and their locations:\n[cyan]KeyPad Wires[/] (Located in the Weapons Room).\n- [cyan]Fuel[/] (Located in the Shower Room).[/]");

                        ExtFunctions.ContinuePrompt();
                    }

                    else if (ObtainedKeyPadWire1 && ObtainedKeyPadWire2 && ObtainedKeyPadWire3 == false && ObtainedKeycardLevel2 == false && ObtainedKeycardLevel3 == false && ObtainedFuel)
                    {
                        AnsiConsole.MarkupLine("[orange1]The map shows that the following key items and their locations:\n[cyan]Keycard Level 2[/] (Located in the Solitary Cell).\n- [cyan]Keycard Level 3[/] (Located in the Rec Room).[/]");

                        ExtFunctions.ContinuePrompt();
                    }

                    else if (ObtainedKeyPadWire1 && ObtainedKeyPadWire2 && ObtainedKeyPadWire3 && ObtainedKeycardLevel2 == false && ObtainedKeycardLevel3 == false && ObtainedFuel == false)
                    {
                        AnsiConsole.MarkupLine("[orange1]The map shows that the following key items and their locations:\n[cyan]Keycard Level 2[/] (Located in the Solitary Cell).\n[cyan]Keycard Level 3[/] (Located in the Rec Room).[/]");

                        ExtFunctions.ContinuePrompt();
                    }

                    else if (ObtainedKeyPadWire1 && ObtainedKeyPadWire2 && ObtainedKeyPadWire3 == false && ObtainedKeycardLevel2 && ObtainedKeycardLevel3 == false && ObtainedFuel == false)
                    {
                        AnsiConsole.MarkupLine("[orange1]The map shows that the following key items and their locations:\n[cyan]Keycard Level 3[/] (Located in the Rec Room).\n[cyan]KeyPad Wires[/] (Located in the Weapons Room).\n[cyan]Fuel[/] (Located in the Shower Room).[/]");

                        ExtFunctions.ContinuePrompt();
                    }
                }

                if (ObtainedKeycardLevel3 == false && PowerTurnedOn == false)
                {
                    ExtFunctions.CreateMenuPrompt(new[] { "Look around.", "Investigate the Generator." }, "What would you like to do?", true, false);

                    switch (ExtFunctions.ChosenOption)
                    {
                        case "Look around.":
                            AnsiConsole.MarkupLine("[orange1]While you were looking around, you have seen something was being covered within a slightly-opened cabinet. You have investigated the cabinet and have found a [cyan]Level 3 Clearance Keycard[/]![/]");
                            ObtainedKeycardLevel3 = true;
                            Story_RecRoom();
                            break;

                        case "Investigate the Generator.":
                            AnsiConsole.MarkupLine("[orange1]In the corner of the room; there is a [cyan]generator[/], with a red light that is lit up. Above the light; it says [cyan]'Generator needs fuel, without it, there will be no power.'[/]. This must be the generator, that is powering the prison.[/]");

                            ExtFunctions.ContinuePrompt();

                            if (ObtainedFuel == false)
                            {
                                if (ObtainedMap != false)
                                {
                                    AnsiConsole.MarkupLine("[orange1]You will need to find the Fuel Can, to power up the generator...\n\nAccording to the map that you have collected, this may be found in the Shower Room.[/]");

                                    ExtFunctions.ContinuePrompt();

                                    ExtFunctions.CreateMenuPrompt(new[] { "Yes (Go to the Shower Room).", "No." }, "Do you want to go there now?", true, false);

                                    switch (ExtFunctions.ChosenOption)
                                    {
                                        case "Yes (Go to the Shower Room).":
                                            NewLocation = "Shower Room";
                                            PlayerIsMoving = true;
                                            ExtFunctions.CleanConsole();
                                            Story_Showers();
                                            break;

                                        case "No.":
                                            AnsiConsole.MarkupLine("[orange1]You have decided to stay in the Rec Room.[/]");
                                            ExtFunctions.ContinuePrompt();
                                            Story_RecRoom();
                                            break;
                                    }
                                }
                            }

                            else if (ObtainedFuel)
                            {
                                AnsiConsole.MarkupLine("[orange1]The Fuel Can that you have found in the Shower Room, will be enough to power up the generator. You have poured the fuel and turned the generator on...[/]");

                                ExtFunctions.ContinuePrompt();

                                PowerTurnedOn = true;

                                // Plays sounds of electricity flowing.

                                AnsiConsole.MarkupLine("[orange1]The lights are now on, and the generator is now running. You can now see the Rec Room properly.[/]");

                                if (EntranceVisited)
                                {
                                    AnsiConsole.MarkupLine("[orange1]The [cyan]Keypad[/] should now work. You probably should make your way back to the entrance.[/]");

                                    ExtFunctions.ContinuePrompt();

                                    ExtFunctions.CreateMenuPrompt(new[] { "Start making your way back to the entrance.", "Stay in the Rec Room." }, "Do you want to go there now?", true, false);

                                    switch (ExtFunctions.ChosenOption)
                                    {
                                        case "Start making your way back to the entrance.":
                                            NewLocation = "Corridor 1";
                                            PlayerIsMoving = true;
                                            ExtFunctions.CleanConsole();
                                            Story_Corridor1();
                                            break;

                                        case "Stay in the Rec Room.":
                                            if (PowerTurnedOn && ObtainedKeycardLevel3)
                                            {
                                                AnsiConsole.MarkupLine("[orange1]There is nothing else to do here now, so you will get out from here.[/]");
                                                ExtFunctions.ContinuePrompt();

                                                NewLocation = "Solitary";
                                                PlayerIsMoving = true;
                                                ExtFunctions.CleanConsole();
                                                Story_Solitary2();
                                            }

                                            else if (ObtainedKeycardLevel3 == false)
                                            {
                                                AnsiConsole.MarkupLine("[orange1]As you decided to stay in the room, you had a look around and have found a slight-opened cabinet. As you have opened the cabinet, there seem to be something covered under a thin sheet of paper. Upon inspecting, you have found a [cyan]Level 3 Clearance Keycard[/]![/]");

                                                ObtainedKeycardLevel3 = true;

                                                ExtFunctions.ContinuePrompt();

                                                if (PowerTurnedOn && ObtainedMap && ObtainedKeycardLevel3)
                                                {
                                                    AnsiConsole.MarkupLine("[orange1]There is nothing else to do here now, so you start to make your way back to the Solitary, heading for the Entrance.[/]");

                                                    NewLocation = "Solitary";
                                                    PlayerIsMoving = true;
                                                    ExtFunctions.CleanConsole();
                                                    Story_Solitary2();
                                                }
                                            }
                                            break;
                                    }
                                }
                            }
                            break;
                    }
                }

                if (ObtainedKeycardLevel3 && PowerTurnedOn == false)
                {
                    ExtFunctions.CreateMenuPrompt(new[] {"Investigate the Generator."}, "What would you like to do?", true, false);

                    switch (ExtFunctions.ChosenOption)
                    {
                        case "Investigate the Generator.":
                            AnsiConsole.MarkupLine("[orange1]In the corner of the room; there is a [cyan]generator[/], with a red light that is lit up. Above the light; it says [cyan]'Generator needs fuel, without it, there will be no power.'[/]. This must be the generator, that is powering the prison.[/]");

                            ExtFunctions.ContinuePrompt();

                            if (ObtainedFuel == false)
                            {
                                if (ObtainedMap != false)
                                {
                                    AnsiConsole.MarkupLine("[orange1]You will need to find the Fuel Can, to power up the generator...\n\nAccording to the map that you have collected, this may be found in the Shower Room.[/]");

                                    ExtFunctions.ContinuePrompt();

                                    ExtFunctions.CreateMenuPrompt(new[] { "Yes (Go to the Shower Room).", "No." }, "Do you want to go there now?", true, false);

                                    switch (ExtFunctions.ChosenOption)
                                    {
                                        case "Yes (Go to the Shower Room).":
                                            NewLocation = "Shower Room";
                                            PlayerIsMoving = true;
                                            ExtFunctions.CleanConsole();
                                            Story_Showers();
                                            break;

                                        case "No.":
                                            AnsiConsole.MarkupLine("[orange1]You have decided to stay in the Rec Room.[/]");
                                            Story_RecRoom();
                                            break;
                                    }
                                }
                            }

                            else if (ObtainedFuel)
                            {
                                AnsiConsole.MarkupLine("[orange1]The Fuel Can that you have found in the Shower Room, will be enough to power up the generator. You have poured the fuel and turned the generator on...[/]");

                                ExtFunctions.ContinuePrompt();

                                PowerTurnedOn = true;

                                // Plays sounds of electricity flowing.

                                AnsiConsole.MarkupLine("[orange1]The lights are now on, and the generator is now running. You can now see the Rec Room properly.[/]");

                                if (EntranceVisited)
                                {
                                    AnsiConsole.MarkupLine("[orange1]The [cyan]Keypad[/] should now work. You probably should make your way back to the entrance.[/]");

                                    ExtFunctions.ContinuePrompt();

                                    ExtFunctions.CreateMenuPrompt(new[] { "Start making your way back to the entrance.", "Stay in the Rec Room." }, "Do you want to go there now?", true, false);

                                    switch (ExtFunctions.ChosenOption)
                                    {
                                        case "Start making your way back to the entrance.":
                                            NewLocation = "Corridor 2";
                                            PlayerIsMoving = true;
                                            ExtFunctions.CleanConsole();
                                            Story_Corridor2();
                                            break;

                                        case "Stay in the Rec Room.":

                                            if (PowerTurnedOn && ObtainedKeycardLevel3)
                                            {
                                                AnsiConsole.MarkupLine("[orange1]There is nothing else to do here now, so you will get out from here.[/]");
                                                ExtFunctions.ContinuePrompt();

                                                NewLocation = "Solitary";
                                                PlayerIsMoving = true;
                                                ExtFunctions.CleanConsole();
                                                Story_Solitary2();
                                            }

                                            else if (ObtainedKeycardLevel3 == false)
                                            {
                                                AnsiConsole.MarkupLine("[orange1]As you decided to stay in the room, you had a look around and have found a slight-opened cabinet. As you have opened the cabinet, there seem to be something covered under a thin sheet of paper. Upon inspecting, you have found a [cyan]Level 3 Clearance Keycard[/]![/]");

                                                ObtainedKeycardLevel3 = true;

                                                ExtFunctions.ContinuePrompt();

                                                if (PowerTurnedOn && ObtainedMap && ObtainedKeycardLevel3)
                                                {
                                                    AnsiConsole.MarkupLine("[orange1]There is nothing else to do here now, so you start to make your way back to the Solitary, heading for the Entrance.[/]");

                                                    NewLocation = "Solitary";
                                                    PlayerIsMoving = true;
                                                    ExtFunctions.CleanConsole();
                                                    Story_Solitary2();
                                                }
                                            }
                                            break;
                                    }
                                }
                            }
                            break;
                    }
                }

                if (ObtainedKeycardLevel3 == false && PowerTurnedOn)
                {
                    ExtFunctions.CreateMenuPrompt(new[] { "Look around." }, "What would you like to do?", true, false);

                    switch (ExtFunctions.ChosenOption)
                    {
                        case "Look around.":
                            AnsiConsole.MarkupLine("[orange1]While you were looking around, you have seen something was being covered within a slightly-opened cabinet. You have investigated the cabinet and have found a [cyan]Level 3 Clearance Keycard[/]!");
                            ObtainedKeycardLevel3 = true;
                            Story_RecRoom();
                            break;
                    }
                }
            }

            else if (PowerTurnedOn && ObtainedMap && ObtainedKeycardLevel3)
            {
                AnsiConsole.MarkupLine("[orange1]There is nothing else to do here now, so you start to make your way back to the Solitary, heading for the Entrance.[/]");
                ExtFunctions.ContinuePrompt();
                NewLocation = "Solitary";
                PlayerIsMoving = true;
                ExtFunctions.CleanConsole();
                Story_Solitary2();
            }    
        }

        // From onwards, there will be more enemies spawning...
        public void Story_Solitary2()
        {
            inSolitary2 = true;
            CurrentLocation = "Solitary";

            ExtFunctions.DisplayCurrentLocation(CurrentLocation);
            ExtFunctions.SaveGame();

            while (inSolitary2)
            {
                while (Solitary2GuardKilled == false)
                {
                    AnsiConsole.MarkupLine("[orange1]You are now back in the Solitary, and you see a guard patrolling the area. You heard on the radio, that there is an increase presence of the monsters. Due to this, more guards will be patrolling the prison.[/]");

                    ExtFunctions.ContinuePrompt();

                    ExtFunctions.CreateMenuPrompt(new[] { "Sneak attack the Guard.", "Wait until the Guard goes away.", "Throw something to distract him away from your current location.", "Sneak past the Guard into Cell Block A.", "Sneak past the Guard into Cell Block B." }, "What would you like to do?", true, false);

                    switch (ExtFunctions.ChosenOption)
                    {
                        case "Sneak attack the Guard.":
                            Random enemyKOChance = new();
                            int enemyKOChanceResult = enemyKOChance.Next(1, 100);

                            if (enemyKOChanceResult >= 50 && enemyKOChanceResult <= 75)
                            {
                                AnsiConsole.MarkupLine("[orange1]You have successfully sneaked up to the Guard, and knocked him out. You have taken his weapon.[/]");

                                ExtFunctions.ContinuePrompt();

                                // Obtained his AR. Haha, no.

                                ExtFunctions.CreateMenuPrompt(new[] { "Go to Cell Block A.", "Go to Cell Block B." }, "What would you like to do now?", true, false);
                                
                                switch (ExtFunctions.ChosenOption)
                                {
                                    case "Go to Cell Block A.":
                                        NewLocation = "Cell Block A";
                                        PlayerIsMoving = true;
                                        inSolitary2 = false;
                                        Solitary2GuardKilled = true;
                                        ExtFunctions.CleanConsole();
                                        Story_CellBlockA();
                                        break;

                                    case "Go to Cell Block B.":
                                        NewLocation = "Cell Block B";
                                        PlayerIsMoving = true;
                                        inSolitary2 = false;
                                        Solitary2GuardKilled = true;
                                        ExtFunctions.CleanConsole();
                                        Story_CellBlockB();
                                        break;
                                }
                            }

                            else
                            {
                                AnsiConsole.MarkupLine("[orange1]You have failed to sneak up to the Guard, and he has spotted you. You have no choice but to fight him.[/]");

                                BattleSystem.EncounteredEnemy = BattleSystem.EnemyList[3];

                                BattleSystem.EnemyStatSetup();

                                // INITIATES BATTLE.

                                ExtFunctions.CreateMenuPrompt(new[] { "Go to Cell Block A.", "Go to Cell Block B." }, "What would you like to do now?", true, false);

                                switch (ExtFunctions.ChosenOption)
                                {
                                    case "Go to Cell Block A.":
                                        NewLocation = "Cell Block A";
                                        PlayerIsMoving = true;
                                        inSolitary2 = false;
                                        Solitary2GuardKilled = true;
                                        ExtFunctions.CleanConsole();
                                        Story_CellBlockA();
                                        break;

                                    case "Go to Cell Block B.":
                                        NewLocation = "Cell Block B";
                                        PlayerIsMoving = true;
                                        inSolitary2 = false;
                                        Solitary2GuardKilled = true;
                                        ExtFunctions.CleanConsole();
                                        Story_CellBlockB();
                                        break;
                                }
                            }
                            break;

                        case "Wait until the Guard goes away.":
                            // Chance that the Guard will not go away.
                            Random guardGoAwayChance = new();
                            int guardGoAwayChanceResult = guardGoAwayChance.Next(1, 25);

                            if (guardGoAwayChanceResult <= 10)
                            {
                                AnsiConsole.MarkupLine("[orange1]The Guard went away.[/]");

                                ExtFunctions.ContinuePrompt();

                                ExtFunctions.CreateMenuPrompt(new[] { "Go to Cell Block A.", "Go to Cell Block B." }, "What would you like to do now?", true, false);

                                switch (ExtFunctions.ChosenOption)
                                {
                                    case "Go to Cell Block A.":
                                        NewLocation = "Cell Block A";
                                        PlayerIsMoving = true;
                                        inSolitary2 = false;
                                        Solitary2GuardKilled = true;
                                        ExtFunctions.CleanConsole();
                                        Story_CellBlockA();
                                        break;

                                    case "Go to Cell Block B.":
                                        NewLocation = "Cell Block B";
                                        PlayerIsMoving = true;
                                        inSolitary2 = false;
                                        Solitary2GuardKilled = true;
                                        ExtFunctions.CleanConsole();
                                        Story_CellBlockB();
                                        break;
                                }
                            }

                            else
                            {
                                AnsiConsole.MarkupLine("[orange1]The Guard did not go away and has found you! Forcing you to go against him in a heated battle.[/]");

                                ExtFunctions.ContinuePrompt();

                                BattleSystem.EncounteredEnemy = BattleSystem.EnemyList[3];

                                BattleSystem.EnemyStatSetup();

                                // Initiate battle.

                                ExtFunctions.CreateMenuPrompt(new[] { "Go to Cell Block A.", "Go to Cell Block B." }, "What would you like to do now?", true, false);

                                switch (ExtFunctions.ChosenOption)
                                {
                                    case "Go to Cell Block A.":
                                        NewLocation = "Cell Block A";
                                        PlayerIsMoving = true;
                                        inSolitary2 = false;
                                        Solitary2GuardKilled = true;
                                        ExtFunctions.CleanConsole();
                                        Story_CellBlockA();
                                        break;

                                    case "Go to Cell Block B.":
                                        NewLocation = "Cell Block B";
                                        PlayerIsMoving = true;
                                        inSolitary2 = false;
                                        Solitary2GuardKilled = true;
                                        ExtFunctions.CleanConsole();
                                        Story_CellBlockB();
                                        break;
                                }
                            }
                            break;

                        case "Throw something to distract him away from your current location.":
                            // Chance that the Guard will not go away or find the player.

                            Random guardGoAwayChance2 = new();
                            int guardGoAwayChanceResult2 = guardGoAwayChance2.Next(1, 25);


                            if (guardGoAwayChanceResult2 <= 10)
                            {
                                AnsiConsole.MarkupLine("[orange1]The Guard went away.[/]");

                                ExtFunctions.ContinuePrompt();

                                ExtFunctions.CreateMenuPrompt(new[] { "Go to Cell Block A.", "Go to Cell Block B." }, "What would you like to do now?", true, false);

                                switch (ExtFunctions.ChosenOption)
                                {
                                    case "Go to Cell Block A.":
                                        NewLocation = "Cell Block A";
                                        PlayerIsMoving = true;
                                        inSolitary2 = false;
                                        Solitary2GuardKilled = true;
                                        ExtFunctions.CleanConsole();
                                        Story_CellBlockA();
                                        break;

                                    case "Go to Cell Block B.":
                                        NewLocation = "Cell Block B";
                                        PlayerIsMoving = true;
                                        inSolitary2 = false;
                                        Solitary2GuardKilled = true;
                                        ExtFunctions.CleanConsole();
                                        Story_CellBlockB();
                                        break;
                                }
                            }

                            else
                            {
                                AnsiConsole.MarkupLine("[orange1]The Guard did not go away but the guard found you, as he traced the throw of the object![/]");

                                BattleSystem.EncounteredEnemy = BattleSystem.EnemyList[3];

                                BattleSystem.EnemyStatSetup();

                                ExtFunctions.CreateMenuPrompt(new[] { "Go to Cell Block A.", "Go to Cell Block B." }, "What would you like to do now?", true, false);

                                switch (ExtFunctions.ChosenOption)
                                {
                                    case "Go to Cell Block A.":
                                        NewLocation = "Cell Block A";
                                        PlayerIsMoving = true;
                                        inSolitary2 = false;
                                        Solitary2GuardKilled = true;
                                        ExtFunctions.CleanConsole();
                                        Story_CellBlockA();
                                        break;

                                    case "Go to Cell Block B.":
                                        NewLocation = "Cell Block B";
                                        PlayerIsMoving = true;
                                        inSolitary2 = false;
                                        Solitary2GuardKilled = true;
                                        ExtFunctions.CleanConsole();
                                        Story_CellBlockB();
                                        break;
                                }
                            }
                            break;

                        case "Sneak past the Guard into Cell Block A.":
                            // Chance that the Guard will not go away or find the player.

                            Random guardGoAwayChance3 = new();
                            int guardGoAwayChanceResult3 = guardGoAwayChance3.Next(1, 25);

                            if (guardGoAwayChanceResult3 > 15)
                            {
                                AnsiConsole.MarkupLine("[orange1]You have successfully sneaked your way to the Cell Block A![/]");
                                ExtFunctions.ContinuePrompt();

                                NewLocation = "Cell Block A";
                                PlayerIsMoving = true;
                                inSolitary2 = false;
                                Solitary2GuardKilled = true;
                                ExtFunctions.CleanConsole();
                                Story_CellBlockA();

                            }
                            else
                            {
                                AnsiConsole.MarkupLine("[orange1]As you were sneaking by, you caught the attention of the guard![/]");

                                ExtFunctions.ContinuePrompt();

                                BattleSystem.EncounteredEnemy = BattleSystem.EnemyList[3];

                                BattleSystem.EnemyStatSetup();

                                // Initiate battle.
                                NewLocation = "Cell Block A";
                                PlayerIsMoving = true;
                                inSolitary2 = false;
                                Solitary2GuardKilled = true;
                                ExtFunctions.CleanConsole();
                                Story_CellBlockA();
                            }

                            break;

                        case "Sneak past the Guard into Cell Block B.":
                            // Chance that the Guard will not go away or find the player.

                            Random guardGoAwayChance4 = new();
                            int guardGoAwayChanceResult4 = guardGoAwayChance4.Next(1, 25);

                            if (guardGoAwayChanceResult4 > 15)
                            {
                                AnsiConsole.MarkupLine("[orange1]You have successfully sneaked your way to the Cell Block B![/]");
                                ExtFunctions.ContinuePrompt();

                                NewLocation = "Cell Block B";
                                PlayerIsMoving = true;
                                inSolitary2 = false;
                                Solitary2GuardKilled = true;
                                ExtFunctions.CleanConsole();
                                Story_CellBlockB();

                            }
                            else
                            {
                                AnsiConsole.MarkupLine("[orange1]As you were sneaking by, you caught the attention of the guard![/]");

                                ExtFunctions.ContinuePrompt();

                                BattleSystem.EncounteredEnemy = BattleSystem.EnemyList[3];

                                BattleSystem.EnemyStatSetup();

                                // Initiate battle.
                                NewLocation = "Cell Block B";
                                PlayerIsMoving = true;
                                inSolitary2 = false;
                                Solitary2GuardKilled = true;
                                ExtFunctions.CleanConsole();
                                Story_CellBlockB();
                            }
                            break;
                    }
                }
            }
        }

        public void Story_Entrance2()
        {
            CurrentLocation = "Entrance";

            ExtFunctions.DisplayCurrentLocation(CurrentLocation);
            ExtFunctions.SaveGame();

            if (EntranceVisited && PowerTurnedOn)
            {
                ExtFunctions.EnemySpawnRandomiser(EscapedFromTortureChamber);

                AnsiConsole.MarkupLine("[orange1]You are now back to the Entrance. The advanced security system is now being powered by the generator.[/]");

                ExtFunctions.ContinuePrompt();

                AnsiConsole.MarkupLine("[orange1]It seems like there is more to the security system than you thought. There is a [cyan]keypad[/] here, that is not functioning properly. Seems like the Keypad is not receiving the [cyan]power[/].[/]");

                ExtFunctions.ContinuePrompt();

                AnsiConsole.MarkupLine("[orange1]There is a small power box further to the right, from the way the wires are routed, there is a high chance that the power box is supposed to power the Keypad with electricity. Upon opening the power box, you can see the [cyan]three wires[/]. They have been torn apart by something.[/]");

                ExtFunctions.ContinuePrompt();

                if (ObtainedKeyPadWire1 && ObtainedKeyPadWire2 && ObtainedKeyPadWire3)
                {
                    AnsiConsole.MarkupLine("[green]You have all three wires to put inside the electric box[/],[orange1] that is connected to the Keypad, and have restored power to it.[/]");

                    ExtFunctions.ContinuePrompt();

                    AnsiConsole.MarkupLine("[orange1]You have restored the flow of electricity to the Keypad. It seems to be showing a warning; stating that Level 3 Clearance Keycard is required to access the Keypad.\n\nThis is also suggesting you will also need to figure out the keycode for the keypad.[/]");

                    ExtFunctions.ContinuePrompt();

                    if (ObtainedKeyPadCode)
                    {
                        AnsiConsole.MarkupLine("[orange1]You have just remembered. The person in the Canteen mumbled a code...\n\nThe code is [cyan]791754[/].[/]");

                        ExtFunctions.ContinuePrompt();

                        ExtFunctions.CreateMenuPrompt(new[] { "Enter the code.", "Go back to the Laundry Room." }, "What would you like to do?", true, false);

                        switch (ExtFunctions.ChosenOption)
                        {
                            case "Enter the code.":
                            RETRYKEYPADCODE:

                                EnteringKeyPadCode = true;

                                while (EnteringKeyPadCode)
                                {
                                    int keyPadCode1 = KeyPadCombination1[0];
                                    int keyPadCode2 = KeyPadCombination1[1];
                                    int keyPadCode3 = KeyPadCombination1[2];
                                    int keyPadCode4 = KeyPadCombination1[3];
                                    int keyPadCode5 = KeyPadCombination1[4];
                                    int keyPadCode6 = KeyPadCombination1[5];
                                    int enteredKeyPadCode1;
                                    int enteredKeyPadCode2;
                                    int enteredKeyPadCode3;
                                    int enteredKeyPadCode4;
                                    int enteredKeyPadCode5;
                                    int enteredKeyPadCode6;

                                    AnsiConsole.MarkupLine("[orange1]Enter the first digit of the code.[/]");
                                    enteredKeyPadCode1 = Convert.ToInt16(Console.ReadLine());
                                    AnsiConsole.MarkupLine("[orange1]Enter the second digit of the code.[/]");
                                    enteredKeyPadCode2 = Convert.ToInt16(Console.ReadLine());
                                    AnsiConsole.MarkupLine("[orange1]Enter the third digit of the code.[/]");
                                    enteredKeyPadCode3 = Convert.ToInt16(Console.ReadLine());
                                    AnsiConsole.MarkupLine("[orange1]Enter the fourth digit of the code.[/]");
                                    enteredKeyPadCode4 = Convert.ToInt16(Console.ReadLine());
                                    AnsiConsole.MarkupLine("[orange1]Enter the fifth digit of the code.[/]");
                                    enteredKeyPadCode5 = Convert.ToInt16(Console.ReadLine());
                                    AnsiConsole.MarkupLine("[orange1]Enter the sixth digit of the code.[/]");
                                    enteredKeyPadCode6 = Convert.ToInt16(Console.ReadLine());

                                    if (enteredKeyPadCode1 == keyPadCode1 && enteredKeyPadCode2 == keyPadCode2 && enteredKeyPadCode3 == keyPadCode3 && enteredKeyPadCode4 == keyPadCode4 &&
                                        enteredKeyPadCode5 == keyPadCode5 && enteredKeyPadCode6 == keyPadCode6)
                                    {
                                        AnsiConsole.MarkupLine("[orange1]You have entered the correct code. The door is now unlocking, and you are now able to leave the prison![/]");
                                        ExtFunctions.ContinuePrompt();
                                        AnsiConsole.MarkupLine("[orange1]As you exit from the Prison, you heard someone screaming in the prison, it sounded like Judy. You ignore the noise and left. In front you, you end up realising there is nothing else of the world, as the darkness is everywhere. The area you are in, seems darker, feels like pure evil. Whatever it is; it changed everything. Nothing is the same anymore. Nobody is safe anywhere.[/]");
                                        ExtFunctions.ContinuePrompt();
                                        AnsiConsole.MarkupLine("[orange1]Thank you for playing the game. Press any key to exit the game.[/]");
                                        Console.ReadKey();
                                        EnteringKeyPadCode = false;
                                        Environment.Exit(0);
                                    }

                                    else
                                    {
                                        AnsiConsole.MarkupLine("[orange1]You have entered the wrong code. The door is still locked.[/]");
                                        ExtFunctions.ContinuePrompt();
                                        AnsiConsole.MarkupLine("[orange1]You will need to find or input the correct code.[/]");
                                        ExtFunctions.ContinuePrompt();

                                        ExtFunctions.CreateMenuPrompt(new[] { "Try again.", "Go back to the Laundry Room." }, "What would you like to do?", true, false);

                                        switch (ExtFunctions.ChosenOption)
                                        {
                                            case "Try again.":
                                                goto RETRYKEYPADCODE;

                                            case "Go back to the Laundry Room.":
                                                NewLocation = "Laundry Room";
                                                PlayerIsMoving = true;
                                                ExtFunctions.CleanConsole();
                                                Story_LaundryRoom3();
                                                break;
                                        }
                                    }
                                }
                                break;

                            case "Go back to the Laundry Room.":
                                NewLocation = "Laundry Room";
                                PlayerIsMoving = true;
                                ExtFunctions.CleanConsole();
                                Story_LaundryRoom3();
                                break;
                        }
                    }
                }

                else
                {
                    AnsiConsole.MarkupLine("[orange1]You have not found all three wires to put inside the electric box, that is connected to the Keypad, and have restored power to it. You need to go and find them.[/]");

                    if (ObtainedMap)
                    {
                        AnsiConsole.MarkupLine("[orange1]The map suggests that the wires can be obtained from:\n- Cell Block A.\n- Cell Block B.\n- Weapons Room.[/]");
                        ExtFunctions.ContinuePrompt();
                        
                        if (ObtainedKeyPadWire1 == false && ObtainedKeyPadWire2 == false && ObtainedKeyPadWire3 == false)
                        {
                            ExtFunctions.CreateMenuPrompt(new[] { "Go to Cell Block A.", "Go to Cell Block B.", "Go to the Weapons Room." }, "Where would you like to go first for the wires?", true, false);

                            switch (ExtFunctions.ChosenOption)
                            {
                                case "Go to Cell Block A.":
                                    NewLocation = "Cell Block A";
                                    PlayerIsMoving = true;
                                    ExtFunctions.CleanConsole();
                                    Story_CellBlockA();
                                    break;

                                case "Go to Cell Block B.":
                                    NewLocation = "Cell Block B";
                                    PlayerIsMoving = true;
                                    ExtFunctions.CleanConsole();
                                    Story_CellBlockB();
                                    break;

                                case "Go to the Weapons Room.":
                                    NewLocation = "Weapons Room";
                                    PlayerIsMoving = true;
                                    ExtFunctions.CleanConsole();
                                    Story_WeaponsRoom();
                                    break;
                            }
                        }

                        else if (ObtainedKeyPadWire1 && ObtainedKeyPadWire2 == false && ObtainedKeyPadWire3 == false)
                        {
                            ExtFunctions.CreateMenuPrompt(new[] { "Go to Cell Block B.", "Go to the Weapons Room." }, "Where would you like to go first for the wires?", true, false);

                            switch (ExtFunctions.ChosenOption)
                            {
                                case "Go to Cell Block B.":
                                    NewLocation = "Cell Block B";
                                    PlayerIsMoving = true;
                                    ExtFunctions.CleanConsole();
                                    Story_CellBlockB();
                                    break;

                                case "Go to the Weapons Room.":
                                    NewLocation = "Weapons Room";
                                    PlayerIsMoving = true;
                                    ExtFunctions.CleanConsole();
                                    Story_WeaponsRoom();
                                    break;
                            }
                        }

                        else if (ObtainedKeyPadWire1 && ObtainedKeyPadWire2 && ObtainedKeyPadWire3 == false)
                        {
                            AnsiConsole.MarkupLine("[orange1]You have found two of the three wires. You will need to find the last wire, which is in the Weapons Room. So, you will go there now...[/]");

                            ExtFunctions.ContinuePrompt();

                            NewLocation = "Weapons Room";
                            PlayerIsMoving = true;
                            ExtFunctions.CleanConsole();
                            Story_WeaponsRoom();
                        }
                    }

                    ExtFunctions.ContinuePrompt();
                }
            }
        }
    }
}

/*      TO-DO LIST
   - Add some more checks to ensure that the menu selection reflects the action the player has taken. - Done.
   - Add the randomised events within the game. - Done.
   - Add style to everything in game. - Done.
   - Make the battle system - Done.
   - Add the enemies to the game - Done.
   - Randomised enemies throughout the game - Done.
   - Randomised reward items. - Work in progress.
   - Randomised enemy types depending on the time of day. - Scrapped.
   - Randomised chances of spawning a container that contains rewards. - Scrapped.
   - Fear variable that increases or decreases depending on the action you perform, inflicting debuffs overtime - Done.
   - Player weapons. - Work in progress.
   - Player inventory containing healing items, some buffs the player permanently. - Done.
   - Level up system - Done.
   - Decisions affecting the story - Done.
   - Few puzzles. - Done.
   - BGM and other sounds - Done.
   - Options that allows the player to configure few things of the game. - Done.
   - Game Journal; saves quotes that the player has seen in-game, thoughts, or clues to their journal - Work in progress.
   - Game statistics when player has finished playing.
   - Enemy Bio. - Done.
   - Make different classes that adjusts the battle accordingly (e.g., A class called: The Whiner, will enable the player to cry out loud in battle, out of fear, to make the guard feel a bit guilty, lowering their defence).
   - Add checks to change bgm depending on the enemy's health. - Done

   - Rick Roll the player if the current time the save was made, is not equal to the date and time the save was modified. This is an anti-cheat measure.
 */