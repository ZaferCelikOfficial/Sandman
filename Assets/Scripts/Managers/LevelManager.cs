using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
//using ElephantSDK;

public class LevelManager : LocalSingleton<LevelManager>
{
    #region Variables
    public static bool isGameStarted;
    public static bool isGameEnded;

    public static int LevelNumber = 0;
    
    public TextMeshProUGUI LevelText;

    [Header("Panels")]

    public GameObject LevelCompletedPanel;
    public GameObject LevelFailedPanel, StartGamePanel, LevelPanel,IntroductionPanel;
    #endregion

    #region Unity

    void Awake()
    {                
        CreateLevel();
        PlayerPrefs.GetInt("LevelNo", 0);
    }
    #endregion

    #region LevelCreator
    public void CreateLevel()
    {
        if ("Level" + PlayerPrefs.GetInt("currLevel", 1) != SceneManager.GetActiveScene().name)
        {
            SceneManager.LoadScene("Level" + PlayerPrefs.GetInt("currLevel", 1));
        }
        PlayerPrefs.GetInt("LevelNo", 0);
        LevelNumber = PlayerPrefs.GetInt("levelNumberLooped", 1);
        //Elephant.LevelStarted(LevelNumber);
    }
    void UpdateLevelNumber()
    {
        //LevelPanel.SetActive(true);
        //LevelText.text = "LEVEL " + LevelNumber.ToString();
    }
    #endregion

    #region Buttons
    public void StartGame()
    {
        StartGamePanel.SetActive(false);

        BodyCountController.Instance.CheckBodyParameters();

        isGameStarted = true;
        isGameEnded = false;
    }
    public void ReturntoMainMenu()
    {
        StartGamePanel.SetActive(true);
        IntroductionPanel.SetActive(false);
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        CreateLevel();
        isGameStarted = false;
        isGameEnded = false;
    }
    int maxLevel = 1;
    int currLevel;
    public void NextLevel()
    {
        int currLevel = PlayerPrefs.GetInt("currLevel", 1);
        int increasedLevel = PlayerPrefs.GetInt("levelNumberLooped", 1) + 1;
        PlayerPrefs.SetInt("levelNumberLooped", increasedLevel);
        isGameStarted = false;
        currLevel++;
        if (currLevel > maxLevel)
        {
            currLevel = 1;
        }

        PlayerPrefs.SetInt("currLevel", currLevel);
        SceneManager.LoadScene("Level" + currLevel);
    }
    #endregion

    #region LevelFinishStates
    public void OnLevelCompleted()
    {
        //Elephant.LevelCompleted(LevelNumber);
        isGameStarted = false;
        isGameEnded = true;

        GameManager.Instance.player.GetComponent<MovementController>().StopPlayerWon();

        WAController.WaFunction(() =>
        {
            LevelCompletedPanel.SetActive(true);
            GameManager.Instance.endgameController.StartCoroutine(GameManager.Instance.endgameController.ScoreIncreaser(BodyCountController.Instance.GetFinishPercentage()));
            GameManager.Instance.confetti.Play();
        }, 1f);
    }
    public void OnLevelFailed()
    {
        //Elephant.LevelFailed(LevelNumber);
        isGameStarted = false;
        isGameEnded = true;

        GameManager.Instance.player.GetComponent<MovementController>().StopPlayerFailed();
        LevelFailedPanel.SetActive(true);
    }
    #endregion
}
