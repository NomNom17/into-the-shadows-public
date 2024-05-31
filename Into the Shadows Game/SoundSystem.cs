using NAudio.Wave;

namespace Into_the_Shadows_Game
{

    // This class will handle all of the sound effects and BGM for the game.
    //  Sounds defined within ExtFunctions.cs, needs to be moved here.

    internal class SoundSystem
    {
        // Randomise the BGM for battle.
        public static readonly Random RandomBGM = new();
        public static int RandomBGMInt = RandomBGM.Next(1, 9);

        // Battle BGM file names.
        public readonly static string[] BattleBGMFileNames =
        { 
            // TURNS OUT NAUDIO CAN PLAY MP3 FILES. *INSERT FACEPALM HERE*
            "Battle_Sound_Files\\BattleBGM1_1.mp3",
            "Battle_Sound_Files\\BattleBGM1_2.mp3",
            "Battle_Sound_Files\\BattleBGM1_3.mp3",
            "Battle_Sound_Files\\BattleBGM1_4.mp3",
            "Battle_Sound_Files\\BattleBGM1_5.mp3",
            "Battle_Sound_Files\\BattleBGM1_6.mp3",
            "Battle_Sound_Files\\BattleBGM1_7.mp3",
            "Battle_Sound_Files\\BattleBGM2_1.mp3",
            "Battle_Sound_Files\\BattleBGM2_2.mp3",
            "Battle_Sound_Files\\BattleBGM2_3.mp3",
            "Battle_Sound_Files\\BattleBGM3_1.mp3",
            "Battle_Sound_Files\\BattleBGM3_2.mp3",
            "Battle_Sound_Files\\BattleBGM3_3.mp3",
        };

        // Story mode BGM file names.
        public readonly static string[] StoryModeBGMFileNames = 
        {
            "StoryMode_Sound_Files\\BGM\\disclaimer-bgm.mp3",
            "StoryMode_Sound_Files\\BGM\\basement-bgm.mp3",
            "StoryMode_Sound_Files\\BGM\\laundryroom1-background-ambience.mp3",
            "StoryMode_Sound_Files\\BGM\\beetles-introduction-bgm.mp3",
            "StoryMode_Sound_Files\\BGM\\laundryroom3-scaryspooky-ambience-bgm.mp3",
            "StoryMode_Sound_Files\\BGM\\shower-room-bgm.mp3",
            "StoryMode_Sound_Files\\BGM\\shower-room-guardkilled-bgm.mp3",
            "StoryMode_Sound_Files\\BGM\\friend1-died-bgm.mp3",
            "StoryMode_Sound_Files\\BGM\\torture-room-bgm.mp3",
            "StoryMode_Sound_Files\\BGM\\finale-bgm.mp3",
            "StoryMode_Sound_Files\\BGM\\canteen-bgm.mp3"
        };

        // SFX Sound File Names. I decided to put them all in one array, as it is easier to manage and it'll reduce the chances of making typos.
        public readonly static string[] SFXSoundFileNames =
        {
            // General SFX Sound Files.
            "StoryMode_Sound_Files\\SFX\\heart-beat.mp3",
            "StoryMode_Sound_Files\\SFX\\keyboard-spacebar-hit.mp3",
            "StoryMode_Sound_Files\\SFX\\caught_sting_sfx.mp3",
            "StoryMode_Sound_Files\\SFX\\keycard-reader-beepbeep-sfx.mp3",
            "StoryMode_Sound_Files\\SFX\\laundryroom3-objectclash-sfx.mp3",
            "StoryMode_Sound_Files\\SFX\\keycard-reader-validated-sfx.mp3",
            "StoryMode_Sound_Files\\SFX\\deep-breath-sfx.mp3",
            "StoryMode_Sound_Files\\SFX\\breaking-bones.mp3",
            "StoryMode_Sound_Files\\SFX\\alarm-1.mp3",
            
            // Laundry Room 1 SFX Sound Files.
            "StoryMode_Sound_Files\\SFX\\laundryroom1-ghost-whispering-sfx.mp3",
            "StoryMode_Sound_Files\\SFX\\laundryroom1-screaming-sfx.mp3",
            "StoryMode_Sound_Files\\SFX\\sirens-sfx.mp3",
            "StoryMode_Sound_Files\\SFX\\laundryroom-monster-scratching-sfx.mp3",

            // Guard Post 1 SFX Sound Files.
            "StoryMode_Sound_Files\\SFX\\lever-pull-sfx.mp3",

            // Canteen SFX Sound Files.
            "StoryMode_Sound_Files\\SFX\\canteen-monster-suspense-sting.mp3",
            "StoryMode_Sound_Files\\SFX\\canteen-monster-scream-sfx.mp3",

            // Torture Room SFX Sound Files.
            "StoryMode_Sound_Files\\SFX\\slappy_slap_SFX.mp3"
        };

        public static WaveOutEvent SFXPlayer = new();

        public static LoopingWaveStream? LoopBGMAudioFile = null;
        public static WaveOutEvent BGMPlayer = new();
        
        // Enemy HP percentages.
        public static double InitialEnemyHP;
        public static double EnemyHP20Percent;
        public static readonly Random EnemyHP20PercentBGMRND = new();
        public static int EnemyHP20PercentBGMInt = EnemyHP20PercentBGMRND.Next(1, 8);
        public static double EnemyHP60Percent;

        public static bool EnemyHP20PercentBGMPlayed = false;
        public static bool EnemyHP60PercentBGMPlayed = false;

        ////////////////////////////////////////////////////
        ///              SOUND FUNCTIONS                 ///
        ////////////////////////////////////////////////////

        public static void SetupInitBattleBGM()
        {
            BGMPlayer.DeviceNumber = -1;

            switch (RandomBGMInt)
            {
                case 1:
                    LoopBGMAudioFile = new LoopingWaveStream(new Mp3FileReader(BattleBGMFileNames[0]));
                    BGMPlayer.Init(LoopBGMAudioFile);
                    BGMPlayer.Play();
                    break;

                case 2:
                    LoopBGMAudioFile = new LoopingWaveStream(new Mp3FileReader(BattleBGMFileNames[1]));
                    BGMPlayer.Init(LoopBGMAudioFile);
                    BGMPlayer.Play();
                    break;

                case 3:
                    LoopBGMAudioFile = new LoopingWaveStream(new Mp3FileReader(BattleBGMFileNames[2]));
                    BGMPlayer.Init(LoopBGMAudioFile);
                    BGMPlayer.Play();
                    break;

                case 4:
                    LoopBGMAudioFile = new LoopingWaveStream(new Mp3FileReader(BattleBGMFileNames[3]));
                    BGMPlayer.Init(LoopBGMAudioFile);
                    BGMPlayer.Play();
                    break;

                case 5:
                    LoopBGMAudioFile = new LoopingWaveStream(new Mp3FileReader(BattleBGMFileNames[4]));
                    BGMPlayer.Init(LoopBGMAudioFile);
                    BGMPlayer.Play();
                    break;

                case 6:
                    LoopBGMAudioFile = new LoopingWaveStream(new Mp3FileReader(BattleBGMFileNames[5]));
                    BGMPlayer.Init(LoopBGMAudioFile);
                    BGMPlayer.Play();
                    break;

                case 7:
                    LoopBGMAudioFile = new LoopingWaveStream(new Mp3FileReader(BattleBGMFileNames[6]));
                    BGMPlayer.Init(LoopBGMAudioFile);
                    BGMPlayer.Play();
                    break;
            }
        }

        public static void CheckEnemyHP()
        {
            // Tell NAudio to use the default audio device.
            BGMPlayer.DeviceNumber = -1;

            static void CalculateEnemyHPPercentage()
            {
                // Here we will calculate the percentage of the enemy's HP.
                // This will be used to determine what BGM to play.

                switch (BattleSystem.EncounteredEnemy)
                {
                    case "Engorged Beetles":
                        InitialEnemyHP = 65f;
                        break;

                    case "Slitherman":
                        InitialEnemyHP = 125f;
                        break;

                    case "The Woodchopper":
                        InitialEnemyHP = 200f;
                        break;

                    case "Cult Guard":
                        InitialEnemyHP = 175f;
                        break;
                }

                // 20 % of the enemy's HP.
                EnemyHP20Percent = InitialEnemyHP * 0.2;

                // 60 % of the enemy's HP.
                EnemyHP60Percent = InitialEnemyHP * 0.6;
            }

            static void AdjustBGM()
            {
                if (BattleSystem.EnemyHP < EnemyHP60Percent)
                {
                    if (EnemyHP60PercentBGMPlayed == false)
                    {
                        BGMPlayer.Stop();
                        BGMPlayer.Dispose();

                        Random enemyHP60PercentBGMRND = new();
                        int enemyHP60PercentBGMInt = enemyHP60PercentBGMRND.Next(1, 4);

                        switch (enemyHP60PercentBGMInt)
                        {
                            case 1:

                                LoopBGMAudioFile = new LoopingWaveStream(new Mp3FileReader(BattleBGMFileNames[7]));
                                BGMPlayer.Init(LoopBGMAudioFile);
                                BGMPlayer.Play();
                                break;

                            case 2:
                                LoopBGMAudioFile = new LoopingWaveStream(new Mp3FileReader(BattleBGMFileNames[8]));
                                BGMPlayer.Init(LoopBGMAudioFile);
                                BGMPlayer.Play();
                                break;

                            case 3:
                                LoopBGMAudioFile = new LoopingWaveStream(new Mp3FileReader(BattleBGMFileNames[9]));
                                BGMPlayer.Init(LoopBGMAudioFile);
                                BGMPlayer.Play();
                                break;
                        }
                        EnemyHP60PercentBGMPlayed = true;
                    }
                }

                if (BattleSystem.EnemyHP < EnemyHP20Percent)
                {
                    if (EnemyHP20PercentBGMPlayed == false)
                    {
                        BGMPlayer.Stop();
                        BGMPlayer.Dispose();

                        Random enemyHP20PercentBGMRND = new();
                        int enemyHP20PercentBGMInt = enemyHP20PercentBGMRND.Next(1, 8);

                        switch (enemyHP20PercentBGMInt)
                        {
                            case 1:
                                LoopBGMAudioFile = new LoopingWaveStream(new Mp3FileReader(BattleBGMFileNames[10]));
                                BGMPlayer.Init(LoopBGMAudioFile);
                                BGMPlayer.Play();
                                break;

                            case 2:
                                LoopBGMAudioFile = new LoopingWaveStream(new Mp3FileReader(BattleBGMFileNames[11]));
                                BGMPlayer.Init(LoopBGMAudioFile);
                                BGMPlayer.Play();
                                break;

                            case 3:
                                LoopBGMAudioFile = new LoopingWaveStream(new Mp3FileReader(BattleBGMFileNames[12]));
                                BGMPlayer.Init(LoopBGMAudioFile);
                                BGMPlayer.Play();
                                break;
                        }
                    }

                    EnemyHP20PercentBGMPlayed = true;
                }
            }

            // Calculate the enemy's HP in percentage, before adjusting the BGM of the battle, which hopefully should dynamically adjusts itself.
            CalculateEnemyHPPercentage();
            AdjustBGM();
        }

        public static void CheckBGMPlayer()
        {
            BGMPlayer.DeviceNumber = -1;

            // This will be used to check if the BGM player is currently playing a BGM file, to prevent it from throwing an exception.

            if (BGMPlayer.PlaybackState == PlaybackState.Playing)
            {
                BGMPlayer.Stop();
                BGMPlayer.Dispose();
            }
        }

        public static void CheckSFXPlayer()
        {
            BGMPlayer.DeviceNumber = -1;

            // This will be used to check if the SFX player is currently playing a SFX file, to prevent it from throwing an exception.

            if (SFXPlayer.PlaybackState == PlaybackState.Playing)
            {
                SFXPlayer.Stop();
                SFXPlayer.Dispose();
            }
        }

        //      PLAYS THE BACKGROUND MUSIC      //
        public static void StoryBGMPlayer(string fileName, bool bgmEnabled)
        {
            if (bgmEnabled == true)
            {
                BGMPlayer.DeviceNumber = -1;
                LoopBGMAudioFile = new LoopingWaveStream(new Mp3FileReader(fileName));
                BGMPlayer.Init(LoopBGMAudioFile);
                BGMPlayer.Play();
            }
        }

        //      PLAYS THE SOUND EFFECTS      //
        public static void SFXSoundPlayer(string fileName, bool sfxEnabled)
        {
            Mp3FileReader? mp3SFXAudioReader = new Mp3FileReader(fileName);

            if (sfxEnabled == true)
            {
                BGMPlayer.DeviceNumber = -1;
                SFXPlayer.Init(mp3SFXAudioReader);

                SFXPlayer.Play();

                if (SFXPlayer.PlaybackState == PlaybackState.Stopped || SFXPlayer.PlaybackState == PlaybackState.Paused)
                {
                    SFXPlayer.Dispose();
                }
            }
        }
    }

    // Expands the WaveStream class, to include a custom class that is designed to loop the BGM.
    // This was taken from online, as I did not know how to make a loop function for NAudio. I do understand some of the code, but not all.
    class LoopingWaveStream : WaveStream
    {
        private readonly WaveStream sourceStream;

        public LoopingWaveStream(WaveStream sourceStream)
        {
            this.sourceStream = sourceStream;
        }

        public override WaveFormat WaveFormat => sourceStream.WaveFormat;

        public override long Length => long.MaxValue;

        public override long Position
        {
            // Grabs the current position of the audio file.
            get => sourceStream.Position;
            set => sourceStream.Position = value;
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            int totalBytesRead = 0;
            while (totalBytesRead < count)
            {
                int bytesRead = sourceStream.Read(buffer, offset + totalBytesRead, count - totalBytesRead);
                if (bytesRead == 0)
                {
                    sourceStream.Position = 0; // Reset to the beginning for looping
                }
                totalBytesRead += bytesRead;
            }
            return totalBytesRead;
        }
    }
}
