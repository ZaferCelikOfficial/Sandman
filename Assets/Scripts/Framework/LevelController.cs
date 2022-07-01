using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
//using Elephant;

public class LevelController : LocalSingleton<LevelController>
{

    public int maxLevel;
    [SerializeField] TextMeshProUGUI levelText;
    public static bool isLoadedLevel;

    private static int _currLevel;
    public static int currLevel // Gerçek level sayısı, Sdk'larda bunu göndereceğiz
    {
        get
        {
            if(_currLevel == 0)
            {
                _currLevel = PlayerPrefs.GetInt("currLevel",1);
            }
            return _currLevel;
        }
        set
        {
            _currLevel = value;
            PlayerPrefs.SetInt("currLevel",_currLevel);
        }
    }


    private static int _reelLevel;
    public static int reelLevel // Scene'leri yüklediğimiz level
    {
        get
        {
            _reelLevel = PlayerPrefs.GetInt("reelLevel", 1);
            return _reelLevel;
        }
        set
        {
            if(value > Instance.maxLevel)
            {
                _reelLevel = 1;
            }
            else
            { 
                _reelLevel = value;
            }
            PlayerPrefs.SetInt("reelLevel",_reelLevel);
        }
    }

    private void Awake()
    {
        if (!isLoadedLevel)
        {
            isLoadedLevel = true;
            LoadLevel();
        }

        if(levelText != null)
        {
            levelText.text = "Level " + currLevel.ToString();
        }
        //Elephant.LevelStarted(currLevel);
    }


    public void NextLevel()
    {
        currLevel++;
        reelLevel++;
        //Elephant.LevelCompleted(currLevel);
    }

    public void FailLevel()
    {
        //Elephant.LevelFailed(currLevel);
    }

    public void LoadLevel() // NextLevel,Replay,Fail butonlarına tıklayınca bu fonksyonu çağırmalısınız
    {
        SceneManager.LoadScene("Level"+reelLevel);
    }
}
