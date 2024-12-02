using Agate.MVC.Base;
using ProjectTA.Message;
using ProjectTA.Utility;
using System.Collections;
using System.IO;
using UnityEngine;

namespace ProjectTA.Module.SaveSystem
{
    public class SaveSystemController : DataController<SaveSystemController, SaveSystemModel, ISaveSystemModel>
    {
        public override IEnumerator Initialize()
        {
            if (!PlayerPrefs.HasKey(TagManager.KEY_VERSION) || !PlayerPrefs.GetString(TagManager.KEY_VERSION).Equals(Application.version))
                DeleteSaveFile();

            PlayerPrefs.SetString(TagManager.KEY_VERSION, Application.version);
            _model.SetSaveData(LoadGame());

            yield return base.Initialize();
        }

        public void SaveGame(SaveData data)
        {
            string json = JsonUtility.ToJson(data, true);
            string path = Path.Combine(Application.persistentDataPath, TagManager.DEFAULT_SAVEFILENAME);

            _model.SetSaveData(data);

            File.WriteAllText(path, json);
            Debug.Log("Game saved to " + path);
        }

        public SaveData LoadGame()
        {
            string path = Path.Combine(Application.persistentDataPath, TagManager.DEFAULT_SAVEFILENAME);

            if (!File.Exists(path))
            {
                Debug.LogWarning("Save file not found. Creating a new one.");
                SaveData saveData = new SaveData();
                SaveGame(saveData);
                return saveData;
            }

            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            Debug.Log("Game loaded from " + path);
            return data;
        }

        public bool DeleteSaveFile()
        {
            string path = Path.Combine(Application.persistentDataPath, TagManager.DEFAULT_SAVEFILENAME);

            if (File.Exists(path))
            {
                try
                {
                    File.Delete(path);
                    Debug.Log("Save file deleted successfully: " + path);
                    return true;
                }
                catch (System.Exception e)
                {
                    Debug.LogError("Failed to delete save file: " + e.Message);
                    return false;
                }
            }
            else
            {
                Debug.Log("Save file not found, nothing to delete.");
                return false;
            }
        }

        public void SetCurrentLevelName(string levelName)
        {
            _model.SetCurrentLevelName(levelName);
            SaveGame(_model.SaveData);
        }

        public void SetCurrentCutsceneName(string cutsceneName)
        {
            _model.SetCurrentCutsceneName(cutsceneName);
            SaveGame(_model.SaveData);
        }

        internal void SaveGameResult(UnlockLevelMessage message)
        {
            if (!_model.IsLevelUnlocked(message.LevelData.name) && message.LevelData != null)
            {
                _model.AddUnlockedLevel(message.LevelData.name);
                SaveGame(_model.SaveData);
            }
        }

        internal void DeleteSaveData(DeleteSaveDataMessage message)
        {
            DeleteSaveFile();
            _model.SetSaveData(LoadGame());
        }
    }
}
