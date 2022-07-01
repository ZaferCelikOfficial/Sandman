using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    ListParametres saveParametres;
    private void Awake()
    {
        Load();   
    }
    public void Save()
    {
        var values = JsonUtility.ToJson(saveParametres);
        PlayerPrefs.SetString("SaveGame", values);
    }
    public void Load()
    {
        saveParametres = JsonUtility.FromJson<ListParametres>(PlayerPrefs.GetString("SaveGame"));
    }
}
[System.Serializable]
public class ListParametres
{
    public List<SaveLoadParametres> saveLoad = new List<SaveLoadParametres>();
}
[System.Serializable]
public class SaveLoadParametres
{//Kaydedilecek de?i?kenleri buraya girin
    
}

