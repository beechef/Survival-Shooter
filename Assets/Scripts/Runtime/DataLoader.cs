using System;
using Newtonsoft.Json;
using UnityEngine;

namespace Runtime
{
    public class DataLoader : MonoBehaviour
    {
        private static readonly string SaveLocation = "PlayerData";
        public static SaveData saveData;

        private void Awake()
        {
            LoadData();
        }

        private static void LoadData()
        {
            saveData = JsonConvert.DeserializeObject<SaveData>(PlayerPrefs.GetString(SaveLocation));
        }

        public static void SaveData()
        {
            PlayerPrefs.SetString(SaveLocation, JsonConvert.SerializeObject(saveData));
        }
        
    }

    public class SaveData
    {
        public float HighestScore;
        public float MusicValue;
        public float EffectValue;
    }
}