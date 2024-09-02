using ProjectTA.Module.LevelData;
using UnityEngine;

namespace ProjectTA.Message
{
    public struct LoadLevelCompleteMessage
    {
        public string LevelName { get; }
        public Sprite Artwork { get; }
        public AudioClip MusicClip { get; }
        public Material Skybox { get; }
        
        public LoadLevelCompleteMessage(string levelName, Sprite artwork, AudioClip musicClip, Material skybox)
        {
            LevelName = levelName;
            Artwork = artwork;
            MusicClip = musicClip;
            Skybox = skybox;
        }
    }
}