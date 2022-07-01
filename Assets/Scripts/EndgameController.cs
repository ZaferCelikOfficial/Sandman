using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndgameController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    public IEnumerator ScoreIncreaser(float playerScore)
    {
        float myScore = 0;
        
        while (myScore <= playerScore)
        {
            scoreText.text = "% " + myScore.ToString();
            myScore++;
            yield return new WaitForSeconds(0.017f);
        }
    }
}
