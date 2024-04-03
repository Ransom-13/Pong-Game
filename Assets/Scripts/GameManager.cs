using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public Text leftScoreText;
    public Text rightScoreText;

    public int leftScore;
    public int rightScore;

    public void LeftScores()
    {
        leftScore++;
        this.leftScoreText.text = leftScore.ToString();
        if (leftScore == 5)
        {
            Invoke(nameof(Reset), 1);
        }

    }
    public void RightScores()
    {
        rightScore++;
        this.rightScoreText.text = rightScore.ToString();
        if (rightScore == 5)
        {
            Invoke(nameof(Reset), 1);
        }
    }
    public void Reset()
    {
        leftScore = 0;
        rightScore = 0;
        this.leftScoreText.text = leftScore.ToString();
        this.rightScoreText.text = rightScore.ToString();
    }
}
