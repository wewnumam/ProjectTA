using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace ProjectTA.Utility
{
    public class SaveSystem<T> where T : class, new()
    {
        private const string JsonExtension = ".json";
        private const string BinExtension = ".bin";

        private readonly string _dataPath;

        public SaveSystem(string fileName, bool isDevelopment = true)
        {
            if (!PlayerPrefs.HasKey(TagManager.KEY_VERSION) || !PlayerPrefs.GetString(TagManager.KEY_VERSION).Equals(Application.version))
                Delete();

            string extension = isDevelopment ? JsonExtension : BinExtension;
            _dataPath = Path.Combine(Application.persistentDataPath, fileName + extension);
        }

        public void Save(T data, bool isDevelopment = true)
        {
            if (isDevelopment)
            {
                SaveAsJson(data);
            }
            else
            {
                SaveAsBinary(data);
            }
        }

        public T Load(bool isDevelopment = true)
        {
            if (File.Exists(_dataPath))
            {
                return isDevelopment ? LoadFromJson() : LoadFromBinary();
            }
            return new T();
        }

        public void Delete()
        {
            try
            {
                if (File.Exists(_dataPath))
                {
                    File.Delete(_dataPath);
                    Debug.Log($"Save file deleted at: {_dataPath}");
                }
                else
                {
                    Debug.LogWarning($"No save file found to delete at: {_dataPath}");
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to delete save file: {ex.Message}");
            }
        }

        private void SaveAsJson(T data)
        {
            try
            {
                string json = JsonUtility.ToJson(data, true);
                File.WriteAllText(_dataPath, json);
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to save JSON file: {ex.Message}");
            }
        }

        private T LoadFromJson()
        {
            try
            {
                string json = File.ReadAllText(_dataPath);
                return JsonUtility.FromJson<T>(json);
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to load JSON file: {ex.Message}");
                return new T();
            }
        }

        private void SaveAsBinary(T data)
        {
            try
            {
                using (FileStream stream = File.Open(_dataPath, FileMode.Create))
                {
                    var formatter = new BinaryFormatter();
                    formatter.Serialize(stream, data);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to save binary file: {ex.Message}");
            }
        }

        private T LoadFromBinary()
        {
            try
            {
                using (FileStream stream = File.Open(_dataPath, FileMode.Open))
                {
                    var formatter = new BinaryFormatter();
                    return formatter.Deserialize(stream) as T;
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to load binary file: {ex.Message}");
                return new T();
            }
        }
    }
}
