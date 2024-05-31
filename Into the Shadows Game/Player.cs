using Spectre.Console;

namespace Into_the_Shadows_Game
{
    internal class Player
    {
        public static string PlayerName = $"{StoryMode.PlayerNameFinalised}";
        public static float PlayerHealth = 145f;
        public static int PlayerMaxHealth = 145;
        public static float PlayerATKRating = 4.7f;
        public static float PlayerDEFRating = 0.5f;    
        public static float PlayerEXP = 0;
        public static float PlayerEXPRequired = 100;
        public static int PlayerLevel = 0;
        public static int PlayerMaxLevel = 10;
        public static int PlayerFear = 0;
        public static int PlayerMaxFear = 100;
        public static int PlayerStatPoints = 0;
        public static bool PlayerIsBleeding = false;
        public static bool PlayerFeared = false;

        public static int ExpiredPacketOfMalteasers = 1; //
        public static int BottleOfAgedWater = 0;
        public static int BottleOfWaterLabelledHolyWater = 0; // Eradicates all Fear, increases Max HP by 35, increases ATK by 2.5, increases DEF by 2.5.
        public static int BoxofFerreroRocher = 0; // Adds 45 HP, increases Max HP by 10, increases ATK by 1.5, increases DEF by 1.5.
        public static int BoxofDoughnuts = 0; // Adds 75 HP, has chance of causing Lactose Intolerance. Summons ChanceToFart() method.
        public static int CrunchyChick = 0; // Adds 25 HP, increases Max HP by 5, increases ATK by 0.5, increases DEF by 0.5. Increases Fear by 25. Very immorally wrong to eat.
        public static int Bandages = 0; // Adds 25 HP, removes bleeding status.

        public static bool IsPlayerCheckingInventory = false;
        public static bool IsPlayerUsingItem = false;


        // Weapons
        
        // We need to append the weapons to the array using lists, and then we need to check if the weapon has been obtained, which is done by bool checks, and if it is, then we can use it.
        public static string[] ObtainedWeapons = { };
        public static List<string> ObtainedWeaponsList = new();

        public static bool IsPlayerChangingWeapons = false;
        public static string EquippedWeapon = "";
        public static bool GotShotgun = false;
        public static int ShotgunAmmo = 0;
        public static bool GotPistol = false;
        public static int PistolAmmo = 0;
        public static bool GotAR = false;
        public static int ARAmmo = 0;
        public static bool GotMetalPipe = false;

        public static void PlayerStatsDisplay()
        {
            Grid playerStatsGrid = new(); //.Alignment(Justify.Center) - Alignment doesn't seem to work.
            Rule underline = new Rule($"{StoryMode.PlayerNameFinalised}'s Stats").Centered().DoubleBorder();
            Rule underlineEnd = new Rule().Centered().AsciiBorder();

            string playerHPString = PlayerHealth.ToString() + " HP";
            string playerDefString = PlayerDEFRating.ToString();
            string playerAtkString = PlayerATKRating.ToString();
            string playerLevelString = PlayerLevel.ToString();
            string playerExpString = PlayerEXP.ToString() + " XP";

            /*------------------------------------------------Text------------------------------------------------*/

            // Text allows us to apply style where it cannot be possible in any other way in a specific use.
            Text playerHPHeading = new Text("HP [Health Points]", new Style(Color.Green)).Centered();
            Text playerDefHeading = new Text("DEF Rating [Defence]", new Style(Color.Blue)).Centered();
            Text playerATKHeading = new Text("ATK Rating [Attack]", new Style(Color.Red)).Centered();
            Text playerLevelHeading = new Text("Player Level", new Style(Color.SteelBlue1_1)).Centered();
            Text playerExpHeading = new Text("Accumulated EXP", new Style(Color.Orange1)).Centered();

            Text playerHPDisplay = new Text(playerHPString, new Style()).Centered();
            Text playerDefDisplay = new Text(playerDefString, new Style()).Centered();
            Text playerATKDisplay = new Text(playerAtkString, new Style()).Centered();
            Text playerLevelDisplay = new Text(playerLevelString, new Style()).Centered();
            Text playerExpDisplay = new Text(playerExpString, new Style()).Centered();

            /*-----------------------------------------------Padding----------------------------------------------*/

            Padder playerHPHeadingPadder = new Padder(playerHPHeading).PadLeft(8).PadRight(1).PadBottom(0).PadTop(0);
            Padder playerDefHeadingPadder = new Padder(playerDefHeading).PadLeft(0).PadRight(1).PadBottom(0).PadTop(0);
            Padder playerATKHeadingPadder = new Padder(playerATKHeading).PadLeft(0).PadRight(1).PadBottom(0).PadTop(0);
            Padder playerLevelHeadingPadder = new Padder(playerLevelHeading).PadLeft(0).PadRight(1).PadBottom(0).PadTop(0);
            Padder playerExpHeadingPadder = new Padder(playerExpHeading).PadLeft(0).PadRight(1).PadBottom(0).PadTop(0);

            Padder playerHPDisplayPadder = new Padder(playerHPDisplay).PadLeft(8).PadRight(1).PadBottom(0).PadTop(0);
            Padder playerDefDisplayPadder = new Padder(playerDefDisplay).PadLeft(0).PadRight(1).PadBottom(0).PadTop(0);
            Padder playerATKDisplayPadder = new Padder(playerATKDisplay).PadLeft(0).PadRight(1).PadBottom(0).PadTop(0);
            Padder playerLevelDisplayPadder = new Padder(playerLevelDisplay).PadLeft(0).PadRight(1).PadBottom(0).PadTop(0);
            Padder playerExpDisplayPadder = new Padder(playerExpDisplay).PadLeft(0).PadRight(1).PadBottom(0).PadTop(0);

            //string[] PlayerStatsArray = {PlayerHP.ToString(), PlayerDef.ToString(), PlayerAtk.ToString(),PlayerLevel.ToString()};

            //Console.WriteLine(PlayerStatsArray);


            underline.Style = Style.Parse("orange1");
            underlineEnd.Style = Style.Parse("orange1");
            AnsiConsole.Write(underline);

            AnsiConsole.WriteLine();

            playerStatsGrid.AddColumn();
            playerStatsGrid.AddColumn();
            playerStatsGrid.AddColumn();
            playerStatsGrid.AddColumn();
            playerStatsGrid.AddColumn();
            playerStatsGrid.AddRow(playerHPHeadingPadder, playerDefHeadingPadder, playerATKHeadingPadder, playerLevelHeadingPadder, playerExpHeadingPadder);

            playerStatsGrid.AddRow(playerHPDisplayPadder, playerDefDisplayPadder, playerATKDisplayPadder, playerLevelDisplayPadder, playerExpDisplayPadder);

            AnsiConsole.Write(playerStatsGrid);
            AnsiConsole.WriteLine();

            int initialPlayerHP = (int)PlayerHealth;

            AnsiConsole.Write(new BarChart()
            .Width(initialPlayerHP)
            .Label("[green bold underline]Your health[/]")
            .CenterLabel()
            .AddItem($"[green]{StoryMode.PlayerNameFinalised}'s HP: [/]", (int)PlayerHealth, Color.Green));
            AnsiConsole.Write(underlineEnd);
            AnsiConsole.WriteLine();

            // Old Code
            // Stops me from having to continuously having to write the same line over again for every time the player moves location.
            //Console.WriteLine("\nCurrent Stats:\n{0} HP - Health Points\n{1} DEF Rating - Defence Rating\n{2} ATK Rating - Attack Rating\n\nCurrent Level: {3}\nCurrent EXP: {4}\n", PlayerHP, PlayerDef, PlayerAtk, PlayerLevel, PlayerExp);
        }

        public static void PlayerFearCheck()
        {
            if (PlayerFear >= PlayerMaxFear)
            {
                AnsiConsole.MarkupLine("[red]Your fear has reached 100, lowering your ATK and DEF by 1.25, and your Max HP by 10![/]");

                ExtFunctions.ContinuePrompt();
                PlayerFeared = true;

                PlayerATKRating -= 1.25f;
                PlayerDEFRating -= 1.25f;
                PlayerMaxHealth -= 10;

                PlayerFear = 0;

                if (PlayerHealth > PlayerMaxHealth)
                {
                    PlayerHealth = PlayerMaxHealth;
                }
            }
        }

        public static void PlayerLevelEXPCheck()
        {
            if (PlayerEXP >= PlayerEXPRequired)
            {
                PlayerEXPRequired *= 1.35f;
                PlayerEXPRequired = MathF.Round(PlayerEXPRequired, 2);
                PlayerLevelSystem();
            }
        }

        public static void PlayerLevelSystem()
        {
            AnsiConsole.Clear();
            Console.Clear();

            PlayerLevel++; // Increase the player's level by 1
            PlayerStatPoints++; // Increase the player's stat points by 1

            AnsiConsole.MarkupLine($"[bold green]You have leveled up!\nPlease decide which stat you want to boost!\n\n{PlayerStatPoints} points remaining.[/]\n\n");

            ExtFunctions.ContinuePrompt();

            while (PlayerStatPoints > 0)
            {
                // If the player's level is a multiple of 5, increase their maximum health by 15
                if (PlayerLevel % 5 == 0)
                {
                    ExtFunctions.CreateMenuPrompt(new[] { "Health [[by 15]].", "Attack Rating [[by 0.75]].", "Defense Rating [[by 0.75]]." }, $"Select a stat to boost! {PlayerStatPoints} points remaining.", true, false);

                    switch (ExtFunctions.ChosenOption)
                    {
                        case "Health [[by 15]].":
                            PlayerMaxHealth += 15;
                            PlayerHealth = PlayerMaxHealth;
                            PlayerStatPoints--;
                            PlayerStatsDisplay();
                            AnsiConsole.MarkupLine("[green]You have increased your HP by 15![/]");
                            ExtFunctions.ContinuePrompt();
                            break;

                        case "Attack Rating [[by 0.75]].":
                            PlayerATKRating += 0.75f;
                            PlayerStatPoints--;
                            PlayerStatsDisplay();
                            AnsiConsole.MarkupLine("[green]You have increased your ATK Rating by 0.75![/]");
                            ExtFunctions.ContinuePrompt();
                            break;

                        case "Defense Rating [[by 0.75]].":
                            PlayerDEFRating += 0.75f;
                            PlayerStatPoints--;
                            PlayerStatsDisplay();
                            AnsiConsole.MarkupLine("[green]You have increased your DEF Rating by 0.75![/]");
                            ExtFunctions.ContinuePrompt();
                            break;
                    }
                }

                else
                {
                    ExtFunctions.CreateMenuPrompt(new[] { "Health [[by 10]].", "Attack Rating [[by 0.5]].", "Defense Rating [[by 0.5]]." }, $"Select a stat to boost! {PlayerStatPoints} points remaining.", true, false);

                    switch (ExtFunctions.ChosenOption)
                    {
                        case "Health [[by 10]].":
                            PlayerMaxHealth += 10;
                            PlayerHealth = PlayerMaxHealth;
                            PlayerStatPoints--;
                            PlayerStatsDisplay();
                            AnsiConsole.MarkupLine("[green]You have increased your HP by 10![/]");
                            ExtFunctions.ContinuePrompt();
                            break;

                        case "Attack Rating [[by 0.5]].":
                            PlayerATKRating += 0.5f;
                            PlayerStatPoints--;
                            PlayerStatsDisplay();
                            AnsiConsole.MarkupLine("[green]You have increased your ATK Rating by 0.5![/]");
                            ExtFunctions.ContinuePrompt();
                            break;

                        case "Defense Rating [[by 0.5]].":
                            PlayerDEFRating += 0.5f;
                            PlayerStatPoints--;
                            PlayerStatsDisplay();
                            AnsiConsole.MarkupLine("[green]You have increased your DEF Rating by 0.5![/]");
                            ExtFunctions.ContinuePrompt();
                            break;
                    }
                }
            }
        }

        public static void CheckPlayerInventory()
        {
            void SetupInventoryGrid()
            {
                Grid consumableItemsInventory = new();

                string expiredPacketOfMalteasersQuantityString = ExpiredPacketOfMalteasers.ToString();
                string bottleOfAgedWaterQuantityString = BottleOfAgedWater.ToString();
                string bottleOfWaterLabelledHolyWaterQuantityString = BottleOfWaterLabelledHolyWater.ToString();
                string boxOfFerreroRocherQuantityString = BoxofFerreroRocher.ToString();
                string boxOfDoughnutsQuantityString = BoxofDoughnuts.ToString();
                string crunchyChickQuantityString = CrunchyChick.ToString();
                string bandagesQuantityString = Bandages.ToString();

                /*-----------------------------------------------Padding----------------------------------------------*/

                Padder consumableItemNameHeadingPadder = new Padder(new Text("Name", new Style(Color.Orange1)).Centered()).PadLeft(15).PadRight(1).PadBottom(0).PadTop(0);
                Padder consumableItemQuantityHeadingPadder = new Padder(new Text("Quantity", new Style(Color.Orange1)).Centered()).PadLeft(0).PadRight(1).PadBottom(0).PadTop(0);
                Padder consumableItemDescriptionHeadingPadder = new Padder(new Text("Description", new Style(Color.Orange1)).Centered()).PadLeft(0).PadRight(1).PadBottom(0).PadTop(0);

                Padder consumableItemExpiredPacketOfMalteasersName = new Padder(new Text("Expired Pack of Malteasers", new Style(Color.Orange1)).Centered()).PadLeft(15).PadRight(1).PadBottom(0).PadTop(1);
                Padder consumableItemExpiredPacketOfMalteasersQuantity = new Padder(new Text(expiredPacketOfMalteasersQuantityString, new Style(Color.Orange1)).Centered()).PadLeft(0).PadRight(1).PadBottom(0).PadTop(1);
                Padder consumableItemExpiredPacketOfMalteasersDescription = new Padder(new Text("A delicious treat, that is past its due date. Restores 25 HP.", new Style(Color.Orange1)).Centered()).PadLeft(0).PadRight(1).PadBottom(0).PadTop(0);

                Padder consumableItemBottleOfAgedWaterName = new Padder(new Text("Bo'le of Aged Wa'er", new Style(Color.Orange1)).Centered()).PadLeft(15).PadRight(2).PadBottom(0).PadTop(1);
                Padder consumableItemBottleOfAgedWaterQuantity = new Padder(new Text(bottleOfAgedWaterQuantityString, new Style(Color.Orange1)).Centered()).PadLeft(0).PadRight(1).PadBottom(0).PadTop(1);
                Padder consumableItemBottleOfAgedWaterDescription = new Padder(new Text("A Bottle of Water, that looks to be aged, judging from the bubbles and other chunky bits inside.\nRestores 15 HP, and removes 5 FEAR.", new Style(Color.Orange1)).Centered()).PadLeft(0).PadRight(1).PadBottom(0).PadTop(1);

                Padder consumableItemBottleOfWaterLabelledHolyWaterName = new Padder(new Text("Bo'le of Wa'er, labelled as Holy Water", new Style(Color.Orange1)).Centered()).PadLeft(15).PadRight(1).PadBottom(0).PadTop(2);
                Padder consumableItemBottleOfWaterLabelledHolyWaterQuantity = new Padder(new Text(bottleOfWaterLabelledHolyWaterQuantityString, new Style(Color.Orange1)).Centered()).PadLeft(0).PadRight(1).PadBottom(0).PadTop(2);
                Padder consumableItemBottleOfWaterLabelledHolyWaterDescription = new Padder(new Text("A Bottle of Water, that coincidentally labelled as 'Holy Water'. Water contained in the bottle looks to be pure.\nEradicates all Fear, increases Max HP by 35, increases ATK by 2.5, increases DEF by 2.5.", new Style(Color.Orange1)).Centered()).PadLeft(0).PadRight(1).PadBottom(0).PadTop(1);

                Padder consumableItemBoxOfFerreroRocherName = new Padder(new Text("Box of Ferrero Rocher", new Style(Color.Orange1)).Centered()).PadLeft(15).PadRight(1).PadBottom(0).PadTop(2);
                Padder consumableItemBoxOfFerreroRocherQuantity = new Padder(new Text(boxOfFerreroRocherQuantityString, new Style(Color.Orange1)).Centered()).PadLeft(0).PadRight(1).PadBottom(0).PadTop(2);
                Padder consumableItemBoxOfFerreroRocherDescription = new Padder(new Text("An exquisite, prestigious and expensive box of chocolates, called Ferrero Rocher. Contains Deez Nuts, that may trigger your Deez Nuts allergy.\nRestores 45 HP, increases Max HP by 10, increases ATK by 1.5, increases DEF by 1.5.", new Style(Color.Orange1)).Centered()).PadLeft(0).PadRight(1).PadBottom(0).PadTop(1);

                Padder consumableItemBoxOfDoughnutsName = new Padder(new Text("Box of Doughnuts", new Style(Color.Orange1)).Centered()).PadLeft(15).PadRight(1).PadBottom(0).PadTop(2);
                Padder consumableItemBoxOfDoughnutsQuantity = new Padder(new Text(boxOfDoughnutsQuantityString, new Style(Color.Orange1)).Centered()).PadLeft(0).PadRight(1).PadBottom(0).PadTop(2);
                Padder consumableItemBoxOfDoughnutsDescription = new Padder(new Text("A box of doughnuts, that looks to be from a local bakery. The doughnuts looks oddly fresh.\nRestores 75 HP, increases Max HP by 15.", new Style(Color.Orange1)).Centered()).PadLeft(0).PadRight(1).PadBottom(0).PadTop(1);

                Padder consumableItemCrunchyChickName = new Padder(new Text("Crunchy Chick", new Style(Color.Orange1)).Centered()).PadLeft(15).PadRight(1).PadBottom(0).PadTop(3);
                Padder consumableItemCrunchyChickQuantity = new Padder(new Text(crunchyChickQuantityString, new Style(Color.Orange1)).Centered()).PadLeft(0).PadRight(1).PadBottom(0).PadTop(3);
                Padder consumableItemCrunchyChickDescription = new Padder(new Text("\"A Crunchy Chick, that has been executed, but has not been cleaned nor made to be edible.\nRestores 25 HP, increases Max HP by 5, increases ATK by 0.5, increases DEF by 0.5. Increases Fear by 25.\n\nIt is a very immoral thing to do...", new Style(Color.Orange1)).Centered()).PadLeft(0).PadRight(1).PadBottom(0).PadTop(1);

                Padder consumableItemBandagesName = new Padder(new Text("Bandages", new Style(Color.Orange1)).Centered()).PadLeft(15).PadRight(1).PadBottom(0).PadTop(2);
                Padder consumableItemBandagesQuantity = new Padder(new Text(bandagesQuantityString, new Style(Color.Orange1)).Centered()).PadLeft(0).PadRight(1).PadBottom(0).PadTop(2);
                Padder consumableItemBandagesDescription = new Padder(new Text("A roll of bandages, that can be used to stop bleeding.\nRestores 25 HP, and removes bleeding status.", new Style(Color.Orange1)).Centered()).PadLeft(0).PadRight(1).PadBottom(0).PadTop(1);


                //Console.WriteLine("\nHere are all of the consumable items that are at your disposal.\n\n- Chocolates: {0}       - Bo'le of Wa'er: {1}\n- Can of Coke: {2}       - Holy Tap Water: {3}", Chocolates, BottleOfWater, Coke, HolyTapWater);
                consumableItemsInventory.AddColumn();
                consumableItemsInventory.AddColumn();
                consumableItemsInventory.AddColumn();
                //ConsumableItemsInventory.AddColumn();

                consumableItemsInventory.AddRow(consumableItemNameHeadingPadder, consumableItemQuantityHeadingPadder, consumableItemDescriptionHeadingPadder);
                consumableItemsInventory.AddRow(consumableItemExpiredPacketOfMalteasersName, consumableItemExpiredPacketOfMalteasersQuantity, consumableItemExpiredPacketOfMalteasersDescription);
                consumableItemsInventory.AddRow(consumableItemBottleOfAgedWaterName, consumableItemBottleOfAgedWaterQuantity, consumableItemBottleOfAgedWaterDescription);
                consumableItemsInventory.AddRow(consumableItemBottleOfWaterLabelledHolyWaterName, consumableItemBottleOfWaterLabelledHolyWaterQuantity, consumableItemBottleOfWaterLabelledHolyWaterDescription);
                consumableItemsInventory.AddRow(consumableItemBoxOfFerreroRocherName, consumableItemBoxOfFerreroRocherQuantity, consumableItemBoxOfFerreroRocherDescription);
                consumableItemsInventory.AddRow(consumableItemBoxOfDoughnutsName, consumableItemBoxOfDoughnutsQuantity, consumableItemBoxOfDoughnutsDescription);
                consumableItemsInventory.AddRow(consumableItemCrunchyChickName, consumableItemCrunchyChickQuantity, consumableItemCrunchyChickDescription);
                consumableItemsInventory.AddRow(consumableItemBandagesName, consumableItemBandagesQuantity, consumableItemBandagesDescription);

                Rule Underline = new Rule("Your Inventory").Centered().DoubleBorder();
                Underline.Style = Style.Parse("orange1");
                AnsiConsole.WriteLine();
                AnsiConsole.Write(Underline);

                AnsiConsole.WriteLine();

                AnsiConsole.Write(consumableItemsInventory);
            }

            void UseItem()
            {
                while (IsPlayerUsingItem)
                {
                    string[] consumableItems = { "Expired Pack of Malteasers", "Bo'le of Aged Wa'er", "Bo'le of Wa'er, labelled as Holy Water", "Ferrero Rocher", "Box of Doughnuts", "Crunchy Chick", "Bandages" ,"Go back" };

                    ExtFunctions.CreateMenuPrompt(consumableItems, "Which item would you like to use?", true, false);

                    switch (ExtFunctions.ChosenOption)
                    {
                        case "Expired Pack of Malteasers":
                            if (ExpiredPacketOfMalteasers > 0)
                            {
                                PlayerHealth += 25;
                                PlayerHealth = MathF.Round(PlayerHealth, 2);
                                ExpiredPacketOfMalteasers--;
                                AnsiConsole.MarkupLine("[green]You have eaten an Expired Pack of Malteasers, and have restored 25 HP![/]");
                                ExtFunctions.ContinuePrompt();
                                IsPlayerUsingItem = false;
                            }
                            else
                            {
                                AnsiConsole.MarkupLine("[red]You do not have any Expired Pack of Malteasers to eat![/]");
                                ExtFunctions.ContinuePrompt();
                                IsPlayerUsingItem = false;
                            }
                            break;

                        case "Bo'le of Aged Wa'er":
                            if (BottleOfAgedWater > 0)
                            {
                                PlayerHealth += 15;
                                PlayerHealth = MathF.Round(PlayerHealth, 2);
                                PlayerFear -= 5;
                                PlayerFear = Math.Clamp(PlayerFear, 0, PlayerMaxFear);
                                BottleOfAgedWater--;
                                AnsiConsole.MarkupLine("[green]You have drank a Bo'le of Aged Wa'er, and have restored 15 HP, and have removed 5 FEAR![/]");
                                ExtFunctions.ContinuePrompt();
                                IsPlayerUsingItem = false;
                            }
                            else
                            {
                                AnsiConsole.MarkupLine("[red]You do not have any Bo'le of Aged Wa'er to drink![/]");
                                ExtFunctions.ContinuePrompt();
                                IsPlayerUsingItem = false;
                            }
                            break;

                        case "Bo'le of Wa'er, labelled as Holy Water":
                            if (BottleOfWaterLabelledHolyWater > 0)
                            {
                                PlayerHealth += 35;
                                PlayerHealth = MathF.Round(PlayerHealth, 2);
                                PlayerFear = 0;
                                PlayerMaxHealth += 35;
                                PlayerATKRating += 2.5f;
                                PlayerDEFRating += 2.5f;
                                BottleOfWaterLabelledHolyWater--;
                                AnsiConsole.MarkupLine("[green]You have drank a Bo'le of Wa'er, labelled as Holy Water, and have restored 35 HP, and have removed all FEAR!\nYour Max HP has increased by 35, your ATK Rating has increased by 2.5, and your DEF Rating has increased by 2.5![/]");
                                ExtFunctions.ContinuePrompt();
                            }
                            else
                            {
                                AnsiConsole.MarkupLine("[red]You do not have any Bo'le of Wa'er, labelled as Holy Water to drink![/]");
                                ExtFunctions.ContinuePrompt();
                            }
                            break;

                        case "Ferrero Rocher":
                            if (BoxofFerreroRocher > 0)
                            {
                                PlayerHealth += 45;
                                PlayerHealth = MathF.Round(PlayerHealth, 2);
                                PlayerMaxHealth += 10;
                                PlayerATKRating += 1.5f;
                                PlayerDEFRating += 1.5f;
                                BoxofFerreroRocher--;
                                AnsiConsole.MarkupLine("[green]You have eaten a Ferrero Rocher, and have restored 45 HP!\nYour Max HP has increased by 10, your ATK Rating has increased by 1.5, and your DEF Rating has increased by 1.5![/]");
                                ExtFunctions.ContinuePrompt();
                            }
                            else
                            {
                                AnsiConsole.MarkupLine("[red]You do not have any Ferrero Rocher to eat![/]");
                                ExtFunctions.ContinuePrompt();
                            }
                            break;

                        case "Box of Doughnuts":
                            if (BoxofDoughnuts > 0)
                            {
                                PlayerHealth += 75;
                                PlayerHealth = MathF.Round(PlayerHealth, 2);
                                PlayerMaxHealth += 15;
                                PlayerATKRating += 2.5f;
                                PlayerDEFRating += 2.5f;
                                BoxofDoughnuts--;
                                AnsiConsole.MarkupLine("[green]You have eaten a Box of Doughnuts, and have restored 75 HP!\nYour Max HP has increased by 15, your ATK Rating has increased by 2.5, and your DEF Rating has increased by 2.5![/]");
                                ExtFunctions.ContinuePrompt();
                            }
                            else
                            {
                                AnsiConsole.MarkupLine("[red]You do not have any Box of Doughnuts to eat![/]");
                                ExtFunctions.ContinuePrompt();
                            }
                            break;

                        case "Crunchy Chick":
                            if (CrunchyChick > 0)
                            {
                                PlayerHealth += 25;
                                PlayerHealth = MathF.Round(PlayerHealth, 2);
                                PlayerMaxHealth += 5;
                                PlayerATKRating += 0.5f;
                                PlayerDEFRating += 0.5f;
                                PlayerFear += 25;
                                CrunchyChick--;
                                AnsiConsole.MarkupLine("[green]You have eaten a Crunchy Chick, and have restored 25 HP!\nYour Max HP has increased by 5, your ATK Rating has increased by 0.5, and your DEF Rating has increased by 0.5!\nYou have also increased your FEAR by 25![/]");
                                ExtFunctions.ContinuePrompt();
                            }
                            else
                            {
                                AnsiConsole.MarkupLine("[red]You do not have any Crunchy Chick to eat![/]");
                                ExtFunctions.ContinuePrompt();
                            }
                            break;

                        case "Bandages":
                            if (Bandages > 0)
                            {
                                PlayerHealth += 25;
                                PlayerHealth = MathF.Round(PlayerHealth, 2);
                                PlayerIsBleeding = false;
                                Bandages--;
                                AnsiConsole.MarkupLine("[green]You have used a Bandage, and have restored 25 HP, and have removed the bleeding status![/]");
                                ExtFunctions.ContinuePrompt();
                            }
                            else
                            {
                                AnsiConsole.MarkupLine("[red]You do not have any Bandages to use![/]");
                                ExtFunctions.ContinuePrompt();
                            }
                            break;

                        case "Go back":
                            IsPlayerUsingItem = false;
                            break;
                    }
                }
            }

            void ChangeWeapons()
            {
                while (IsPlayerChangingWeapons)
                {
                    string[] availableWeaponsToEquip = { };
                    List<string> availableWeaponsToEquipList = new();

                    // Add all of the obtained weapons to the list.
                    for (int i = 0; i < ObtainedWeapons.Length; i++)
                    {
                        availableWeaponsToEquipList.Add(ObtainedWeapons[i]);
                    }

                    availableWeaponsToEquipList.Add("Go back");

                    // Add the obtained weapons to the array. This will then be used to create a menu prompt.
                    availableWeaponsToEquip = availableWeaponsToEquipList.ToArray();

                    ExtFunctions.CreateMenuPrompt(availableWeaponsToEquip, "Which weapon would you like to equip?", true, false);

                    switch (ExtFunctions.ChosenOption)
                    {
                        case "Metal Pipe":
                            EquippedWeapon = "Metal Pipe";
                            AnsiConsole.MarkupLine("[green]You have equipped the Metal Pipe![/]");
                            PlayerATKRating += 1.5f;
                            ExtFunctions.ContinuePrompt();
                        break;

                        case "Shotgun":
                            if (ShotgunAmmo > 0)
                            {
                                EquippedWeapon = "Shotgun";
                                AnsiConsole.MarkupLine("[green]You have equipped the Shotgun![/]");
                                PlayerATKRating += 2.5f;
                                ExtFunctions.ContinuePrompt();
                            }
                            else
                            {
                                AnsiConsole.MarkupLine("[red]You do not have any Shotgun Ammo to equip the Shotgun![/]");
                                ExtFunctions.ContinuePrompt();
                            }
                            break;

                        case "Pistol":
                            if (PistolAmmo > 0)
                            {
                                EquippedWeapon = "Pistol";
                                AnsiConsole.MarkupLine("[green]You have equipped the Pistol![/]");
                                PlayerATKRating += 1.5f;
                                ExtFunctions.ContinuePrompt();
                            }

                            else
                            {
                                AnsiConsole.MarkupLine("[red]You do not have any Pistol Ammo to equip the Pistol![/]");
                                ExtFunctions.ContinuePrompt();
                            }
                            break;

                        case "Assault Rifle":
                            if (ARAmmo > 0) 
                            { 
                                EquippedWeapon = "Assault Rifle";
                                AnsiConsole.MarkupLine("[green]You have equipped the Assault Rifle![/]");
                                PlayerATKRating += 3.5f;
                                ExtFunctions.ContinuePrompt();
                            }

                            else
                            {
                                AnsiConsole.MarkupLine("[red]You do not have any Assault Rifle Ammo to equip the Assault Rifle![/]");
                                ExtFunctions.ContinuePrompt();
                            }
                            break;

                        case "Go back":
                            IsPlayerChangingWeapons = false;
                            break;
                    }
                }
            }

            while (IsPlayerCheckingInventory)
            {
                string[] optionsToChoose = {"Use an item.", "Equip a different weapon." ,"Exit." };

                ExtFunctions.CreateMenuPrompt(optionsToChoose, "What would you like to do?", true, false);

                switch (ExtFunctions.ChosenOption)
                {
                    case "Use an item.":
                        SetupInventoryGrid();
                        IsPlayerUsingItem = true;
                        UseItem();
                        break;

                    case "Equip a different weapon.":
                        IsPlayerChangingWeapons = true;
                        ChangeWeapons();
                        break;

                    case "Exit.":
                        IsPlayerCheckingInventory = false;
                        break;
                }
            }
        }

        public static void PlayerWeaponsObtained()
        {
            // We'll use this to add the obtained weapons to the array, from a list. This is so that the player can see obtained weapons in the inventory.
            static void AddWeaponsToListToPopulatedArray(string weaponName)
            {
                ObtainedWeaponsList = ObtainedWeapons.ToList();

                // We want to clear the array, so the same copied values won't be added to the array again.
                for (int i = 0; i < ObtainedWeapons.Length; i++)
                {
                    ObtainedWeapons[i] = ""; // You can set the default value according to the element's data type
                }

                ObtainedWeaponsList.Add(weaponName);
                ObtainedWeapons = ObtainedWeaponsList.ToArray();
            }

            // Need to check if the array isn't populated with values.
            if (ObtainedWeapons.Length == 0)
            {
                if (GotMetalPipe)
                {
                    ObtainedWeaponsList.Add("Metal Pipe");
                    ObtainedWeapons = ObtainedWeaponsList.ToArray();
                }

                if (GotShotgun)
                {
                    ObtainedWeaponsList.Add("Shotgun");
                    ObtainedWeapons = ObtainedWeaponsList.ToArray();
                }

                if (GotPistol)
                {
                    ObtainedWeaponsList.Add("Pistol");
                    ObtainedWeapons = ObtainedWeaponsList.ToArray();
                }

                if (GotAR)
                {
                    ObtainedWeaponsList.Add("Assault Rifle");
                    ObtainedWeapons = ObtainedWeaponsList.ToArray();
                }
            }

            // Need to check if the array is populated with values.
            if (ObtainedWeapons.Length > 0)
            {
                if (GotMetalPipe)
                {
                    AddWeaponsToListToPopulatedArray("Metal Pipe");
                }

                if (GotShotgun)
                {
                    AddWeaponsToListToPopulatedArray("Shotgun");
                }

                if (GotPistol)
                {
                    AddWeaponsToListToPopulatedArray("Pistol");
                }

                if (GotAR)
                {
                    AddWeaponsToListToPopulatedArray("Assault Rifle");
                }
            }
        }
    }
}
