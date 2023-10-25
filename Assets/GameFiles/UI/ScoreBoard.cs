using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    [SerializeField] TMP_Text score;

    private int scoreCount = 0;         
    public int ScoreCount { get => scoreCount; }

    PointHandler pointHandler;

    void Start()
    {
        score.text = scoreCount.ToString();
        pointHandler = GameObject.Find("PointHandler").GetComponent<PointHandler>();
    }

    public void AddPoints(int point)
    {
        scoreCount += point; 
        score.text = scoreCount.ToString();
        if (scoreCount % 20 == 0)
        {
            pointHandler.SetNewReward(scoreCount);
        }
    }
}
