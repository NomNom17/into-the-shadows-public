using Spectre.Console;

namespace Into_the_Shadows_Game
{
    internal class BattleSystem
    {
        public static string[] EnemyList = { "Engorged Beetles", "Slitherman", "The Woodchopper", "Cult Guard" };
        public static float EnemyHP = 0f;
        public static float EnemyAttack = 0f;
        public static float EnemyDefense = 0f;
        public static int EnemyCharge = 0;

        public static Random EnemyCriticalChance = new();
        public static int EnemyCriticalChanceInt = 0;

        public static Random PlayerCriticalChance = new();
        public static int PlayerCriticalChanceInt = 0;

        public static Random BleedDMGRND = new();
        public static float BleedDMG = 0f;
        public static Random BleedDMGDurationRND = new();
        public static int BleedDMGDuration = 0;
        public static bool BattleEngaged = false;
        public static bool PlayerIsAlive = true;
        public static bool PlayerSneakAttack = false;
        public static Random PlayerSneakAttackDMGRND = new();
        public static float PlayerSneakAttackDMG = 0f;
        public static bool PlayerIsAttacking = true;
        public static bool EnemyIsAlive = false;
        public static bool EnemyIsAttacking = false;
        public static string EncounteredEnemy = "";

        public static string PlayerActionTaken = "";

        public static bool PlayerIsDazed = false;
        public static bool EnemyIsDazed = false;

        public Random EnemyRandomiser = new();
        public int EnemyRandomiserInt = 0;

        public static Random EnemyAttackTypeRND = new();
        public static int EnemyAttackTypeRNDint = 0;
        public static string[] EnemyAttackType = { };
        public static List<string> EnemyAttackTypeList = new();
        public static string EnemyAttackTypeString = "";

        public static void EnemyStatSetup()
        {
            static void sneakAttack()
            {
                PlayerSneakAttackDMG = PlayerSneakAttackDMGRND.Next(1, 15);

                AnsiConsole.MarkupLine($"You have sneakily dealt {PlayerSneakAttackDMG} damage to {EncounteredEnemy}!");

                EnemyHP -= PlayerSneakAttackDMG;

                PlayerSneakAttack = false;

                ExtFunctions.ContinuePrompt();
            }

            BattleEngaged = true;
            PlayerIsAlive = true;
            EnemyIsAlive = true;

            SoundSystem.CheckBGMPlayer();
            SoundSystem.SetupInitBattleBGM();

            //      FOR DEBUGGING PURPOSES ONLY      //
            //EncounteredEnemy = EnemyList[0];
            //Console.WriteLine(BattleEngaged + "\n" + EncounteredEnemy);
            ///////////////////////////////////////////

            if (BattleEngaged == true)
            {
                switch (EncounteredEnemy)
                {
                    case "Engorged Beetles":
                        EnemyHP = 65f;
                        EnemyAttack = 2.35f;
                        EnemyDefense = 1.05f;
                        //EnemyCharge = 1;

                        EnemyAttackTypeList.Add("Bite");
                        EnemyAttackTypeList.Add("Fly and Hiss");
                        EnemyAttackTypeList.Add("Sprinkle Some Poop");
                        EnemyAttackType = EnemyAttackTypeList.ToArray();

                        //  TEST TO SEE IF STRINGS HAS PROPERLY BEEN ASSIGNED TO ARRAY. //
                        /*
                        Console.WriteLine(EnemyAttackType[0]);
                        Console.WriteLine(EnemyAttackType[1]);
                        Console.WriteLine(EnemyAttackType[2]);
                        Battle();
                        */

                        if (PlayerSneakAttack)
                        {                             
                            sneakAttack();
                        }

                        Battle();

                        break;

                    case "Slitherman":
                        EnemyHP = 125f;
                        EnemyAttack = 4.25f;
                        EnemyDefense = 1.75f;
                        //EnemyCharge = 1;

                        EnemyAttackTypeList.Add("Scratch");
                        EnemyAttackTypeList.Add("Lunge");
                        EnemyAttackTypeList.Add("Wail");
                        EnemyAttackType = EnemyAttackTypeList.ToArray();

                        //  TEST TO SEE IF STRINGS HAS PROPERLY BEEN ASSIGNED TO ARRAY. //
                        /*
                        Console.WriteLine(EnemyAttackType[0]);
                        Console.WriteLine(EnemyAttackType[1]);
                        Console.WriteLine(EnemyAttackType[2]);
                        */

                        if (PlayerSneakAttack)
                        {
                            sneakAttack();
                        }

                        Battle();

                        break;

                    case "The Woodchopper":
                        EnemyHP = 200f;
                        EnemyAttack = 7.75f;
                        EnemyDefense = 2.25f;
                        //EnemyCharge = 1;

                        EnemyAttackTypeList.Add("Single Slice");
                        EnemyAttackTypeList.Add("Double Slice");
                        EnemyAttackTypeList.Add("Grapple");
                        EnemyAttackTypeList.Add("Chop Chop");
                        EnemyAttackType = EnemyAttackTypeList.ToArray();

                        //  TEST TO SEE IF STRINGS HAS PROPERLY BEEN ASSIGNED TO ARRAY. //
                        /*
                        Console.WriteLine(EnemyAttackType[0]);
                        Console.WriteLine(EnemyAttackType[1]);
                        Console.WriteLine(EnemyAttackType[2]);
                        Console.WriteLine(EnemyAttackType[3]);
                        */

                        if (PlayerSneakAttack)
                        {
                            sneakAttack();
                        }

                        Battle();

                        break;

                    case "Cult Guard":
                        EnemyHP = 175f;
                        EnemyAttack = 2.25f;
                        EnemyDefense = 3.75f;
                        //EnemyCharge = 1;

                        EnemyAttackTypeList.Add("Melee with a rusty pipe");
                        EnemyAttackTypeList.Add("Electrocute");
                        EnemyAttackTypeList.Add("Shoot with a 9mm pistol");
                        EnemyAttackTypeList.Add("Eat some junk food");
                        EnemyAttackTypeList.Add("Radio in some motivation");
                        EnemyAttackType = EnemyAttackTypeList.ToArray();

                        //  TEST TO SEE IF STRINGS HAS PROPERLY BEEN ASSIGNED TO ARRAY. //
                        /*
                        Console.WriteLine(EnemyAttackType[0]);
                        Console.WriteLine(EnemyAttackType[1]);
                        Console.WriteLine(EnemyAttackType[2]);
                        Console.WriteLine(EnemyAttackType[3]);
                        */

                        if (PlayerSneakAttack)
                        {
                            sneakAttack();
                        }

                        Battle();

                        break;
                }
            }
        }

        public static void DisplayEnemyStats()
        {
            Grid enemyStatsGrid = new();
            Rule underline = new Rule($"{EncounteredEnemy}'s Stats").Centered().DoubleBorder();
            Rule underlineEnd = new Rule().Centered().AsciiBorder();

            /*------------------------------------------------Text------------------------------------------------*/

            // Text allows us to apply style where it cannot be possible in any other way in a specific use.
            Text enemyHPHeading = new Text("HP [Health Points]", new Style(Color.Red)).Centered();
            Text enemyDefHeading = new Text("DEF Rating [Defence]", new Style(Color.Red)).Centered();
            Text enemyATKHeading = new Text("ATK Rating [Attack]", new Style(Color.Red)).Centered();


            Text enemyHPDisplay = new Text(EnemyHP.ToString() + " HP", new Style()).Centered();
            Text enemyDefDisplay = new Text(EnemyDefense.ToString(), new Style()).Centered();
            Text enemyATKDisplay = new Text(EnemyAttack.ToString(), new Style()).Centered();

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

            int initialEnemyHP = (int)EnemyHP;

            // Stops the bar from displaying when the health value is too low. This is because the text on the bar, will scale along with the bar itself, making it unreadable.
            if (EnemyHP > 15)
            {
                AnsiConsole.Write(new BarChart()
                .Width(initialEnemyHP) // This may contribute to the bug reported above. But, I do not know, how to adjust the width of the bar, without manipulating the width of the label.
                .Label($"[red bold underline]{EncounteredEnemy}'s health[/]")
                .CenterLabel()
                .ShowValues()
                .AddItem($"[red]{EncounteredEnemy}'s HP: [/]", (int)EnemyHP, Color.Red));
            }

            AnsiConsole.Write(underlineEnd);
            AnsiConsole.WriteLine("\n\n");
        }

        public static void Battle()
        {
            static float calculatedPlayerAttackDMG(float playerATK)
            {
                float playerAttackDMG = 0;
                PlayerIsAttacking = true;
                Random playerAttackDMGMultRND = new();
                float playerAttackDMGMultRNDfloat = (float)playerAttackDMGMultRND.NextDouble();
                Random playerIntimiatedEnemyRND = new();

                if (PlayerIsDazed != true)
                {
                    switch (PlayerActionTaken)
                    {
                        case "Slam the metal pipe.":
                            playerAttackDMG = playerATK * playerAttackDMGMultRNDfloat / 0.12f;

                            //criticalDamage();

                            EnemyHP -= playerAttackDMG;

                            RoundToDecimalPlaces();

                            AnsiConsole.MarkupLine($"\n[orange1]You have dealt [green]{playerAttackDMG}[/] damage to the {EncounteredEnemy}!\n\n{EncounteredEnemy} now has {EnemyHP} HP left.[/]\n");

                            ExtFunctions.ContinuePrompt();
                            break;

                        case "Give the death stares.":
                            int PlayerIntimiatedEnemyRNDint = playerIntimiatedEnemyRND.Next(1, 20);

                            if (PlayerIntimiatedEnemyRNDint > 17 || PlayerIntimiatedEnemyRNDint < 5)
                            {
                                EnemyIsDazed = true;

                                playerAttackDMG = playerATK * playerAttackDMGMultRNDfloat / 0.28f;

                                //criticalDamage();

                                EnemyHP -= playerAttackDMG;

                                RoundToDecimalPlaces();

                                AnsiConsole.MarkupLine($"\n[orange1]You have successfully [green]intimidated[/] {EncounteredEnemy}! Enemy loses a turn.\n\nYou have dealt {playerAttackDMG} damage to the enemy!\n\n{EncounteredEnemy} now has {EnemyHP} HP left.[/]\n");

                                ExtFunctions.ContinuePrompt();
                            }

                            else
                            {
                                AnsiConsole.MarkupLine($"\n[orange1]You have [red]failed[/] to intimidate {EncounteredEnemy}! You have not dealt damage to {EncounteredEnemy}...[/]\n");

                                ExtFunctions.ContinuePrompt();
                            }

                            break;

                        case "Punch.":
                            playerAttackDMG = playerATK * playerAttackDMGMultRNDfloat / 0.25f;

                            //criticalDamage();

                            EnemyHP -= playerAttackDMG;

                            RoundToDecimalPlaces();

                            AnsiConsole.MarkupLine($"\n[orange1]You have dealt [green]{playerAttackDMG}[/] damage to the {EncounteredEnemy}!\n\n{EncounteredEnemy} now has {EnemyHP} HP left.[/]\n");

                            ExtFunctions.ContinuePrompt();
                            break;
                    }

                    SoundSystem.CheckEnemyHP();

                    PlayerIsAttacking = false;
                    return playerAttackDMG;
                }

                else
                {
                    AnsiConsole.MarkupLine("[orange1]You are [red]dazed[/]! You cannot attack this turn only![/]");
                    PlayerIsDazed = false;
                    return playerAttackDMG;
                }

                void RoundToDecimalPlaces()
                {
                    playerAttackDMG = (float)Math.Round(playerAttackDMG, 2);

                    EnemyHP = (float)Math.Round(EnemyHP, 2);
                }
            }
            static float calculatedEnemyAttackDMG()
            {
                float enemyAttackDMG = 0;
                Random enemyAttackDMGMultRND = new();
                float enemyAttackDMGMultRNDfloat = (float)enemyAttackDMGMultRND.NextDouble();
                Random enemyIntimiatedEnemyRND = new();

                if (EnemyIsDazed != true)
                {
                    switch (EncounteredEnemy)
                    {
                        case "Engorged Beetles":
                            switch (EnemyAttackTypeString)
                            {
                                case "Bite":
                                    enemyAttackDMG = EnemyAttack * enemyAttackDMGMultRNDfloat;

                                    //criticalDamage();

                                    Player.PlayerHealth -= enemyAttackDMG;

                                    RoundToDecimalPlaces();

                                    AnsiConsole.MarkupLine($"\n[orange1]{EncounteredEnemy} has [red]bitten[/] you, dealing [red]{enemyAttackDMG}[/] damage!\n\n{StoryMode.PlayerNameFinalised} now has {Player.PlayerHealth} HP.[/]\n");

                                    ExtFunctions.ContinuePrompt();
                                    break;

                                case "Fly and Hiss":
                                    Random beetlePlayerFearIncreaseRND = new();

                                    int beetlePlayerFearIncreaseRNDint = beetlePlayerFearIncreaseRND.Next(1, 15);
                                    int ememyIntimiatedPlayerRNDint = enemyIntimiatedEnemyRND.Next(1, 20);

                                    if (ememyIntimiatedPlayerRNDint > 17 || ememyIntimiatedPlayerRNDint < 5)
                                    {
                                        PlayerIsDazed = true;

                                        Player.PlayerFear += beetlePlayerFearIncreaseRNDint;

                                        RoundToDecimalPlaces();

                                        AnsiConsole.MarkupLine($"\n[orange1]{EncounteredEnemy} has [red]scared[/] you, by performing the '{EnemyAttackTypeString}' attack! Your FEAR meter has increased by [red]{beetlePlayerFearIncreaseRNDint}[/].[/]\n");

                                        ExtFunctions.ContinuePrompt();

                                        Player.PlayerFearCheck();
                                    }

                                    else
                                    {
                                        AnsiConsole.MarkupLine($"\n[orange1]{EncounteredEnemy} has [green]failed[/] to intimidate you, therefore, it's attack had no effect on you.[/]\n");

                                        ExtFunctions.ContinuePrompt();
                                    }

                                    break;

                                case "Sprinkle Some Poop":
                                    enemyAttackDMG = EnemyAttack * enemyAttackDMGMultRNDfloat / 0.25f;

                                    //criticalDamage();

                                    RoundToDecimalPlaces();

                                    Player.PlayerHealth -= enemyAttackDMG;

                                    AnsiConsole.MarkupLine($"\n[orange1]{EncounteredEnemy} has [red]Sprinkled Some Poop[/] on you! Somehow, you thought the droppings were chocolate, so your nasty mind decided to gobble some.\n\nThe attack has dealt you [red]{enemyAttackDMG}[/] damage. You now have {Player.PlayerHealth} HP.[/]\n");

                                    ExtFunctions.ContinuePrompt();
                                    break;
                            }
                            break;

                        case "Slitherman":

                            switch (EnemyAttackTypeString)
                            {
                                case "Scratch":
                                    enemyAttackDMG = EnemyAttack * enemyAttackDMGMultRNDfloat;

                                    //criticalDamage();

                                    RoundToDecimalPlaces();

                                    Player.PlayerHealth -= enemyAttackDMG;

                                    AnsiConsole.MarkupLine($"\n[red]{EncounteredEnemy} has [red]scratched[/] you, dealing [red]{enemyAttackDMG}[/] damage to you!\n\n{StoryMode.PlayerNameFinalised} now has {Player.PlayerHealth} HP.[/]\n");

                                    playerBleed();

                                    ExtFunctions.ContinuePrompt();
                                    break;

                                case "Lunge":
                                    enemyAttackDMG = EnemyAttack * enemyAttackDMGMultRNDfloat * 0.45f;

                                    //criticalDamage();

                                    Player.PlayerHealth -= enemyAttackDMG;
                                    RoundToDecimalPlaces();

                                    AnsiConsole.MarkupLine($"\n[orange1]{EncounteredEnemy} has lunged at you, dealing [red]{enemyAttackDMG}[/] damage!\n\n{StoryMode.PlayerNameFinalised} now has {Player.PlayerHealth} HP.[/]\n");

                                    ExtFunctions.ContinuePrompt();
                                    break;

                                case "Wail":
                                    Random slithermanRestoreHPChanceRND = new();
                                    int slithermanRestoreHPChanceRNDint = slithermanRestoreHPChanceRND.Next(1, 20);

                                    if (slithermanRestoreHPChanceRNDint <= 11)
                                    {
                                        Random slithermanRestoreHPRND = new();
                                        int SlithermanRestoreHPRNDint = slithermanRestoreHPRND.Next(5, 12);

                                        EnemyHP += SlithermanRestoreHPRNDint;

                                        RoundToDecimalPlaces();

                                        AnsiConsole.MarkupLine($"\n[orange1]{EncounteredEnemy} has [red]restored {SlithermanRestoreHPRNDint} HP[/]!\n\n{EncounteredEnemy} now has {EnemyHP} HP left.[/]\n");
                                    }

                                    else
                                    {
                                        AnsiConsole.MarkupLine($"\n[orange1]{EncounteredEnemy} has [green]failed[/] to restore it's HP.[/]\n");
                                    }

                                    Random slithermanScarePlayerRND = new();
                                    int slithermanScarePlayerRNDint = slithermanScarePlayerRND.Next(1, 20);

                                    if (slithermanScarePlayerRNDint > 17 || slithermanScarePlayerRNDint < 5)
                                    {
                                        Player.PlayerFear += 5;

                                        AnsiConsole.MarkupLine($"\n[orange1]{EncounteredEnemy} has [red]scared[/] you, by performing the '{EnemyAttackTypeString}' attack! Your FEAR meter has increased by [red]5[/].[/]\n");
                                    }

                                    else
                                    {
                                        AnsiConsole.MarkupLine($"\n[orange1]{EncounteredEnemy} has [green]failed[/] to intimidate you, therefore, it's attack had no effect on you.[/]\n");
                                    }
                                    break;
                            }

                            break;

                        case "The Woodchopper":
                            switch (EnemyAttackTypeString)
                            {
                                case "Single Slice":
                                    enemyAttackDMG = EnemyAttack * enemyAttackDMGMultRNDfloat * 0.24f;

                                    //criticalDamage();

                                    playerBleed();

                                    RoundToDecimalPlaces();

                                    Player.PlayerHealth -= enemyAttackDMG;

                                    AnsiConsole.MarkupLine($"\n[orange1]{EncounteredEnemy} has [red]{EnemyAttackTypeString}d[/] you, dealing a devastating damage of [red]{enemyAttackDMG}[/]!\n\n{StoryMode.PlayerNameFinalised} now has {Player.PlayerHealth} HP left.[/]\n");

                                    ExtFunctions.ContinuePrompt();
                                    break;

                                case "Double Slice":
                                    Random doubleSliceDMG = new();
                                    int doubleSliceDMGint = doubleSliceDMG.Next(1, 3);

                                    switch (doubleSliceDMGint)
                                    {
                                        case 1:
                                            enemyAttackDMG = EnemyAttack * enemyAttackDMGMultRNDfloat * 0.31f;
                                            break;

                                        case 2:
                                            enemyAttackDMG = EnemyAttack * enemyAttackDMGMultRNDfloat * 0.29f;
                                            break;

                                        case 3:
                                            enemyAttackDMG = EnemyAttack * enemyAttackDMGMultRNDfloat * 0.19f;
                                            break;
                                    }

                                    //criticalDamage();

                                    playerBleed();

                                    RoundToDecimalPlaces();

                                    Player.PlayerHealth -= enemyAttackDMG;

                                    AnsiConsole.MarkupLine($"\n[orange1]{EncounteredEnemy} has [red]{EnemyAttackTypeString}d[/] you, dealing a devastating damage of [red]{enemyAttackDMG}[/]!\n\n{StoryMode.PlayerNameFinalised} now has {Player.PlayerHealth} HP left.[/]\n");

                                    ExtFunctions.ContinuePrompt();

                                    break;

                                case "Grapple":
                                    Random grappleChanceRND = new();
                                    int grappleChanceRNDint = grappleChanceRND.Next(1, 20);

                                    if (grappleChanceRNDint >= 18 || grappleChanceRNDint <= 2)
                                    {
                                        Random grappleDMGRND = new();
                                        int grappleDMGRNDint = grappleDMGRND.Next(1, 5);

                                        switch (grappleDMGRNDint)
                                        {
                                            case 1:
                                                enemyAttackDMG = EnemyAttack * enemyAttackDMGMultRNDfloat;
                                                //criticalDamage();
                                                RoundToDecimalPlaces();
                                                Player.PlayerHealth -= enemyAttackDMG;
                                                AnsiConsole.MarkupLine($"\n[orange1]{EncounteredEnemy} has damaged you, taking off [red]{enemyAttackDMG}[/] HP from you.\n{EncounteredEnemy} did not manage to instantly kill you!\n\nYou now have {Player.PlayerHealth} HP![/]\n");
                                                break;

                                            case 2:
                                                enemyAttackDMG = EnemyAttack * enemyAttackDMGMultRNDfloat;
                                                //criticalDamage();
                                                RoundToDecimalPlaces();
                                                Player.PlayerHealth -= enemyAttackDMG;
                                                AnsiConsole.MarkupLine($"\n[orange1]{EncounteredEnemy} has damaged you, taking off [red]{enemyAttackDMG}[/] HP.\n{EncounteredEnemy} did not manage to instantly kill you!\n\nYou now have {Player.PlayerHealth} HP![/]\n");
                                                break;

                                            case 3:
                                                enemyAttackDMG = Player.PlayerHealth;
                                                //criticalDamage();
                                                RoundToDecimalPlaces();
                                                Player.PlayerHealth -= enemyAttackDMG;
                                                AnsiConsole.MarkupLine($"\n[orange1]{EncounteredEnemy} has damaged you, taking off [red]{enemyAttackDMG}[/] HP from you.\n{EncounteredEnemy} instantly [red]tore you apart, killing you[/].[/]\n");
                                                checkPlayerIsAlive();
                                                break;

                                            case 4:
                                                enemyAttackDMG = EnemyAttack * enemyAttackDMGMultRNDfloat;
                                                //criticalDamage();
                                                RoundToDecimalPlaces();
                                                Player.PlayerHealth -= enemyAttackDMG;
                                                AnsiConsole.MarkupLine($"\n[orange1]{EncounteredEnemy} has damaged you, taking off [red]{enemyAttackDMG}[/] HP from you.\n{EncounteredEnemy} did not manage to instantly kill you!\n\nYou now have {Player.PlayerHealth} HP ![/]\n");
                                                break;

                                            case 5:
                                                enemyAttackDMG = EnemyAttack * enemyAttackDMGMultRNDfloat;
                                                //criticalDamage();
                                                RoundToDecimalPlaces();
                                                Player.PlayerHealth -= enemyAttackDMG;
                                                AnsiConsole.MarkupLine($"\n[orange1]{EncounteredEnemy} has damaged you, taking off [red]{enemyAttackDMG}[/] HP from you.\n{EncounteredEnemy} did not manage to instantly kill you!\n\nYou now have {Player.PlayerHealth} HP![/]\n");
                                                break;
                                        }

                                        ExtFunctions.ContinuePrompt();
                                    }

                                    else
                                    {
                                        AnsiConsole.MarkupLine($"[orange1]{EncounteredEnemy} has [green]failed[/] to grab you, thus, taking no HP off from you.[/]\n");
                                    }
                                    break;
                            }

                            break;

                        case "Cult Guard":
                            switch (EnemyAttackTypeString)
                            {
                                case "Melee with a rusty pipe":
                                    enemyAttackDMG = EnemyAttack * enemyAttackDMGMultRNDfloat;

                                    //criticalDamage();

                                    RoundToDecimalPlaces();

                                    Player.PlayerHealth -= enemyAttackDMG;

                                    AnsiConsole.MarkupLine($"\n[orange1]{EncounteredEnemy} has [red]smacked you with a rusty pipe[/], dealing a devastating damage of [red]{enemyAttackDMG}[/]!\n\n{StoryMode.PlayerNameFinalised} now has {Player.PlayerHealth} HP.[/]\n");

                                    ExtFunctions.ContinuePrompt();
                                    break;

                                case "Electrocute":
                                    Random electrocuteDMGRND = new();
                                    int electrocuteDMGRNDint = electrocuteDMGRND.Next(1, 11);

                                    enemyAttackDMG = electrocuteDMGRNDint + EnemyAttack * enemyAttackDMGMultRNDfloat;

                                    //criticalDamage();

                                    Player.PlayerHealth -= enemyAttackDMG;

                                    RoundToDecimalPlaces();

                                    AnsiConsole.MarkupLine($"\n[orange1]{EncounteredEnemy} has [red]electrocuted[/] you, dealing a sizzling damage of [red]{enemyAttackDMG}[/]!\n\n{StoryMode.PlayerNameFinalised} now has {Player.PlayerHealth} HP.[/]\n");

                                    ExtFunctions.ContinuePrompt();
                                    break;

                                case "Shoot with a 9mm pistol":
                                    Random shootWith9mmPistolDMGRND = new();
                                    int shootWith9mmPistolDMGRNDint = shootWith9mmPistolDMGRND.Next(1, 13);

                                    enemyAttackDMG = shootWith9mmPistolDMGRNDint + EnemyAttack * enemyAttackDMGMultRNDfloat * 0.24f;

                                    //criticalDamage();

                                    Player.PlayerHealth -= enemyAttackDMG;

                                    RoundToDecimalPlaces();

                                    AnsiConsole.MarkupLine($"\n[orange1]{EncounteredEnemy} has [red]shot you with a 9mm pistol[/], dealing a devastating damage of [red]{enemyAttackDMG}[/]!\n\n{StoryMode.PlayerNameFinalised} now has {Player.PlayerHealth} HP.[/]\n");

                                    ExtFunctions.ContinuePrompt();
                                    break;

                                case "Eat some junk food":
                                    Random eatSomeJunkFoodHPGainRND = new();
                                    int eatSomeJunkFoodHPGainRNDint = eatSomeJunkFoodHPGainRND.Next(1, 31);

                                    Random eatSomeJunkFoodATKGainRND = new();
                                    int eatSomeJunkFoodATKGainRNDint = eatSomeJunkFoodATKGainRND.Next(1, 5);

                                    EnemyHP += eatSomeJunkFoodHPGainRNDint;
                                    EnemyAttack += eatSomeJunkFoodATKGainRNDint;

                                    RoundToDecimalPlaces();

                                    AnsiConsole.MarkupLine($"\n[orange1]{EncounteredEnemy} has ate some [red]tasty junk food[/], restoring [red]{eatSomeJunkFoodHPGainRNDint}[/] HP!\nThis also increased {EncounteredEnemy}'s Attack Rating by [red]{eatSomeJunkFoodATKGainRNDint}[/]!\n{EncounteredEnemy} now has {EnemyHP} HP.[/]\n");

                                    ExtFunctions.ContinuePrompt();
                                    break;

                                case "Radio in some motivation":
                                    Random radioInSomeMotivationHPGainRND = new();
                                    Random radioInSomeMotivationDEFGainRND = new();

                                    int radioInSomeMotivationHPGainRNDint = radioInSomeMotivationHPGainRND.Next(1, 31);
                                    int radioInSomeMotivationDEFGainRNDint = radioInSomeMotivationDEFGainRND.Next(1, 5);

                                    EnemyHP += radioInSomeMotivationHPGainRNDint;

                                    EnemyDefense += radioInSomeMotivationDEFGainRNDint;

                                    RoundToDecimalPlaces();

                                    AnsiConsole.MarkupLine($"\n[orange1]{EncounteredEnemy} has [red]called in some motivation[/], through his radio, restoring [red]{radioInSomeMotivationHPGainRNDint}[/] HP!\n{EncounteredEnemy}'s Defense has increased to [red]{EnemyDefense}[/]!\n\n{EncounteredEnemy} now has {EnemyHP} HP.[/]\n");

                                    ExtFunctions.ContinuePrompt();
                                    break;
                            }
                            break;
                    }
                }

                else
                {
                    AnsiConsole.MarkupLine($"\n[orange1]{EncounteredEnemy} is [green]dazed[/]! It cannot attack this turn only![/]");
                    EnemyIsDazed = false;
                    return enemyAttackDMG;
                }

                void RoundToDecimalPlaces()
                {
                    enemyAttackDMG = (float)Math.Round(enemyAttackDMG, 2);
                    Player.PlayerHealth = (float)Math.Round(Player.PlayerHealth, 2);
                }

                //EnemyIsAttacking = false;

                return enemyAttackDMG;
            }
            static void criticalDamage()
            {
                if (PlayerIsAttacking == true)
                {
                    PlayerCriticalChanceInt = PlayerCriticalChance.Next(1, 10);

                    if (PlayerCriticalChanceInt >= 7)
                    {
                        AnsiConsole.MarkupLine($"[red]You have dealt a critical damage to {EncounteredEnemy}![/]");

                        EnemyHP -= calculatedPlayerAttackDMG(Player.PlayerATKRating) * 2;

                        ExtFunctions.ContinuePrompt();
                    }
                }

                if (EnemyIsAttacking == true)
                {
                    EnemyCriticalChanceInt = EnemyCriticalChance.Next(1, 10);

                    if (EnemyCriticalChanceInt >= 7)
                    {
                        AnsiConsole.MarkupLine($"\n[red]{EncounteredEnemy} has dealt a critical damage to you![/]");

                        Player.PlayerHealth -= calculatedEnemyAttackDMG() * 2;

                        ExtFunctions.ContinuePrompt();
                    }
                }
            }
            static void playerBleed()
            {
                if (Player.PlayerIsBleeding == false)
                {
                    Random chanceToBleedRND = new();
                    int chanceToBleedRNDint = chanceToBleedRND.Next(1, 10);

                    if (chanceToBleedRNDint >= 5)
                    {
                        if (EncounteredEnemy == "Slitherman")
                        {
                            Player.PlayerIsBleeding = true;

                            BleedDMGDuration = BleedDMGDurationRND.Next(1, 3);

                            BleedDMG = chanceToBleedRND.Next(1, 4);
                            AnsiConsole.MarkupLine($"{EncounteredEnemy} made you bleed! This will cause you to lose {BleedDMG} HP for the next {BleedDMGDuration} turns!");

                            Player.PlayerHealth -= BleedDMG;

                            ExtFunctions.ContinuePrompt();
                        }

                        if (EncounteredEnemy == "The Woodchopper")
                        {
                            Player.PlayerIsBleeding = true;

                            BleedDMGDuration = BleedDMGDurationRND.Next(1, 4);

                            BleedDMG = chanceToBleedRND.Next(2, 6);
                            AnsiConsole.MarkupLine($"{EncounteredEnemy} made you bleed! This will cause you to lose {BleedDMG} HP for the next {BleedDMGDuration} turns!");

                            Player.PlayerHealth -= BleedDMG;

                            ExtFunctions.ContinuePrompt();
                        }
                    }
                }

                if (Player.PlayerIsBleeding == true)
                {
                    BleedDMGDuration -= 1;

                    if (BleedDMGDuration <= 0) 
                    {
                        Player.PlayerIsBleeding = false;
                        AnsiConsole.MarkupLine("[green]You are no longer bleeding![/]");
                    }

                    else
                    {
                        Player.PlayerHealth -= BleedDMG;
                        AnsiConsole.MarkupLine($"[red]You are bleeding! {BleedDMG} HP has been taken away, as a result of you bleeding.\n\nYou have {Player.PlayerHealth} HP left![/]");
                    }
                }
            }

            static void checkPlayerIsAlive()
            {
                if (Player.PlayerHealth <= 0)
                {
                    DateTime playerCurrentDate = DateTime.Now.Date;

                    PlayerIsAlive = false;
                    BattleEngaged = false;
                    SoundSystem.EnemyHP20PercentBGMPlayed = false;
                    SoundSystem.EnemyHP60PercentBGMPlayed = false;
                    AnsiConsole.MarkupLine($"[red]GAME OVER! You have died!\nRest in Piece - {StoryMode.PlayerNameFinalised}.\nDate of Death: {playerCurrentDate}.\nCause of death: Got killed by {EncounteredEnemy}.[/]");
                    ExtFunctions.ContinuePrompt();

                    AnsiConsole.MarkupLine("[sandybrown]Would you like to go back to the main menu?[/]");

                    ExtFunctions.CreateMenuPrompt(new[] { "Yes", "No" }, "Would you like to go back to the main menu?", true, true);

                    switch (ExtFunctions.ChosenOption)
                    {
                        case "Yes":
                            ExtFunctions.CleanConsole();
                            MainGame.Main();
                            break;

                        case "No":
                            StoryMode.PlayerIsQuitting = true;
                            ExtFunctions.CleanConsole();
                            Environment.Exit(0);
                            break;
                    }
                }
            }
            static void checkEnemyIsAlive()
            {
                if (EnemyHP <= 0)
                {
                    EnemyIsAlive = false;
                    BattleEngaged = false;
                    SoundSystem.EnemyHP20PercentBGMPlayed = false;
                    SoundSystem.EnemyHP60PercentBGMPlayed = false;
                    AnsiConsole.MarkupLine($"[red]You have defeated {EncounteredEnemy}![/]");
                    ExtFunctions.ContinuePrompt();
                    BattleRewards();
                }
            }

            if (BattleEngaged == true)
            {
                AnsiConsole.Markup($"[red]You have entered in a battle with {EncounteredEnemy}![/]\n\n");

                ExtFunctions.ContinuePrompt();

                // Need to use while loop to execute battle if the player and enemy is alive. //

                while (PlayerIsAlive == true && EnemyIsAlive == true)
                {
                    // Need to use if statements to check if the player or enemy is dead. //
                    playerAttack();
                    checkEnemyIsAlive();
                    enemyAttack();
                    checkPlayerIsAlive();
                    // Need to create methods for player and enemy attacks. //
                }

                static void playerAttack()
                {
                    Player.PlayerStatsDisplay();

                    AnsiConsole.MarkupLine($"[red]It's now your turn to attack![/]\n");

                    AnsiConsole.WriteLine();

                    ExtFunctions.CreateMenuPrompt(new[] { "Slam the metal pipe.", "Give the death stares.", "Punch.", "Check your inventory." }, "What do you want to do?", true, true);

                    switch (ExtFunctions.ChosenOption)
                    {
                        case "Slam the metal pipe.":
                            PlayerActionTaken = "Slam the metal pipe.";
                            calculatedPlayerAttackDMG(Player.PlayerATKRating);
                            break;

                        case "Give the death stares.":
                            PlayerActionTaken = "Give the death stares.";
                            calculatedPlayerAttackDMG(Player.PlayerATKRating);
                            break;

                        case "Punch.":
                            PlayerActionTaken = "Punch.";
                            calculatedPlayerAttackDMG(Player.PlayerATKRating);
                            checkEnemyIsAlive();
                            break;

                        case "Check your inventory.":
                            PlayerActionTaken = "Check your inventory.";
                            Player.IsPlayerCheckingInventory = true;
                            Player.CheckPlayerInventory();
                            playerAttack();
                            break;
                    }
                }

                static void enemyAttack()
                {
                    DisplayEnemyStats();

                    AnsiConsole.MarkupLine($"[red]It's {EncounteredEnemy}'s turn to attack![/]\n");

                    AnsiConsole.WriteLine();

                    if (EncounteredEnemy == "Engorged Beetles")
                    {
                        EnemyAttackTypeRNDint = EnemyAttackTypeRND.Next(1, 4);

                        switch (EnemyAttackTypeRNDint)
                        {
                            case 1:
                                EnemyAttackTypeString = "Bite";
                                calculatedEnemyAttackDMG();
                                break;

                            case 2:
                                EnemyAttackTypeString = "Fly and Hiss";
                                calculatedEnemyAttackDMG();
                                break;

                            case 3:
                                EnemyAttackTypeString = "Sprinkle Some Poop";
                                calculatedEnemyAttackDMG();
                                break;
                        }
                    }

                    else if (EncounteredEnemy == "Slitherman")
                    {
                        EnemyAttackTypeRNDint = EnemyAttackTypeRND.Next(1, 3);

                        switch (EnemyAttackTypeRNDint)
                        {
                            case 1:
                                EnemyAttackTypeString = "Scratch";
                                calculatedEnemyAttackDMG();
                                break;

                            case 2:
                                EnemyAttackTypeString = "Lunge";
                                calculatedEnemyAttackDMG();
                                break;

                            case 3:
                                EnemyAttackTypeString = "Wail";
                                calculatedEnemyAttackDMG();
                                break;
                        }
                    }

                    else if (EncounteredEnemy == "The Woodchopper")
                    {
                        EnemyAttackTypeRNDint = EnemyAttackTypeRND.Next(1, 4);

                        switch (EnemyAttackTypeRNDint)
                        {
                            case 1:
                                EnemyAttackTypeString = "Single Slice";
                                calculatedEnemyAttackDMG();
                                break;

                            case 2:
                                EnemyAttackTypeString = "Double Slice";
                                calculatedEnemyAttackDMG();
                                break;

                            case 3:
                                EnemyAttackTypeString = "Grapple";
                                calculatedEnemyAttackDMG();
                                break;

                            case 4:
                                EnemyAttackTypeString = "Chop Chop";
                                calculatedEnemyAttackDMG();
                                break;
                        }
                    }

                    else if (EncounteredEnemy == EnemyList[3])
                    {
                        EnemyAttackTypeRNDint = EnemyAttackTypeRND.Next(1, 5);

                        switch (EnemyAttackTypeRNDint)
                        {
                            case 1:
                                EnemyAttackTypeString = "Melee with a rusty pipe";
                                calculatedEnemyAttackDMG();
                                break;

                            case 2:
                                EnemyAttackTypeString = "Electrocute";
                                calculatedEnemyAttackDMG();
                                break;

                            case 3:
                                EnemyAttackTypeString = "Shoot with a 9mm pistol";
                                calculatedEnemyAttackDMG();
                                break;

                            case 4:
                                EnemyAttackTypeString = "Eat some junk food";
                                calculatedEnemyAttackDMG();
                                break;

                            case 5:
                                EnemyAttackTypeString = "Radio in some motivation";
                                calculatedEnemyAttackDMG();
                                break;
                        }
                    }
                }
            }
        }
        public static void BattleRewards()
        {
            Random expRewardRND = new();

            if (EncounteredEnemy == "Engorged Beetles" && EnemyIsAlive != true)
            {
                BattleEngaged = false;
                int EXPRewardRNDint = expRewardRND.Next(10, 35);
                Player.PlayerEXP += EXPRewardRNDint;
                AnsiConsole.MarkupLine($"[green]Defeating {EncounteredEnemy} has granted you {EXPRewardRNDint} EXP![/]\n");
                ExtFunctions.RandomRewardFound();
                Player.PlayerLevelEXPCheck();
                SoundSystem.CheckBGMPlayer();
                ExtFunctions.CleanConsole();
            }

            else if (EncounteredEnemy == EnemyList[1] && EnemyIsAlive != true)
            {
                BattleEngaged = false;
                int EXPRewardRNDint = expRewardRND.Next(26, 45);
                Player.PlayerEXP += EXPRewardRNDint;
                AnsiConsole.MarkupLine($"[green]Defeating {EncounteredEnemy} has granted you {EXPRewardRNDint} EXP![/]\n");
                ExtFunctions.RandomRewardFound();
                Player.PlayerLevelEXPCheck();
                SoundSystem.CheckBGMPlayer();
                ExtFunctions.CleanConsole();
            }

            else if (EncounteredEnemy == EnemyList[2] && EnemyIsAlive != true)
            {
                BattleEngaged = false;
                int EXPRewardRNDint = expRewardRND.Next(37, 80);
                Player.PlayerEXP += EXPRewardRNDint;
                AnsiConsole.MarkupLine($"[green]Defeating {EncounteredEnemy} has granted you {EXPRewardRNDint} EXP![/]\n");
                ExtFunctions.RandomRewardFound();
                Player.PlayerLevelEXPCheck();
                SoundSystem.CheckBGMPlayer();
                ExtFunctions.CleanConsole();
            }

            else if (EncounteredEnemy == EnemyList[3] && EnemyIsAlive != true)
            {
                BattleEngaged = false;
                int EXPRewardRNDint = expRewardRND.Next(50, 125);
                Player.PlayerEXP += EXPRewardRNDint;
                AnsiConsole.MarkupLine($"[green]Defeating {EncounteredEnemy} has granted you {EXPRewardRNDint} EXP![/]\n");
                ExtFunctions.RandomRewardFound();
                Player.PlayerLevelEXPCheck();
                SoundSystem.CheckBGMPlayer();
                ExtFunctions.CleanConsole();
            }
        }
    }
}
