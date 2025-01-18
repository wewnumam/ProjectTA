namespace ProjectTA.Utility
{
    public static class TagManager
    {
        public const string SCENE_MAINMENU = "MainMenu";
        public const string SCENE_GAMEPLAY = "Gameplay";
        public const string SCENE_SPLASHSCREEN = "SplashScreen";
        public const string SCENE_LEVELSELECTION = "LevelSelection";
        public const string SCENE_CUTSCENE = "Cutscene";
        public const string SCENE_QUIZ = "Quiz";

        public const string TAG_PLAYER = "Player";
        public const string TAG_ENEMY = "Enemy";
        public const string TAG_BULLET = "Bullet";
        public const string TAG_COLLECTIBLE = "Collectible";
        public const string TAG_PADLOCK = "Padlock";

        public const string ANIM_IDLE = "Idle";
        public const string ANIM_WALK = "Walk";
        public const string ANIM_DEAD = "Dead";

        public const string DEFAULT_CUTSCENENAME = "CutsceneData_Intro";
        public const string DEFAULT_LEVELNAME = "LevelData_0";
        public const string DEFAULT_SAVEFILENAME = "SaveData.json";

        public const string KEY_VERSION = "version";

        public const string MIXER_MASTER_VOLUME = "MasterVolume";
        public const string MIXER_SFX_VOLUME = "SfxVolume";
        public const string MIXER_BGM_VOLUME = "BgmVolume";

        public const string FILENAME_QUESTDATA = "SavedQuestData";
        public const string FILENAME_SAVEDUNLOCKEDCOLLECTIBLES = "SavedUnlockedCollectibles";
        public const string FILENAME_SAVEDLEVELDATA = "SavedLevelData";

        public const string SO_QUESTCOLLECTION = "QuestCollection";
        public const string SO_COLLECTIBLECOLLECTION = "CollectibleCollection";
        public const string SO_LEVELCOLLECTION = "LevelCollection";
        public const string SO_GAMECONSTANTS = "GameConstants";
    }
}