namespace ProjectTA.Utility
{
    public static class TagManager
    {
        public const string SCENE_MAINMENU = "MainMenu";
        public const string SCENE_GAMEPLAY = "Gameplay";
        public const string SCENE_SPLASHSCREEN = "SplashScreen";

        public const string TAG_PLAYER = "Player";

        public const string ANIM_IDLE = "Idle";
        public static readonly string[] ANIM_POSE = { "Pose1", "Pose2" };
        public static readonly string[] ANIM_STEP = { "StepLeft", "StepRight" };
        public static readonly string[] ANIM_STOP = { "Stop1", "Stop2" };
        public const string ANIM_FALL = "Fall";
        public const string ANIM_FLY = "Fly";
        public const string ANIM_LOSE = "Lose";
        public const string ANIM_WIN = "Win";

        public const string DEFAULT_CHARACTERNAME = "CharacterData_0";
        public const string DEFAULT_LEVELNAME = "LevelData_0";
        public const string DEFAULT_SAVEFILENAME = "SaveData.json";

        public const string KEY_VERSION = "version";
        public const string KEY_VOLUME = "volume";
        public const string KEY_VIBRATE = "vibrate";

        public const string MIXER_MASTER_VOLUME = "MasterVolume";
    }
}