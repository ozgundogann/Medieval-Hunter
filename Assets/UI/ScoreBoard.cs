using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    [SerializeField] TMP_Text score;
    [SerializeField] int maxRewardPoint = 100;//Max reward is determined by the point that exist in first if statement
                                              //in SetNewReward function in PointHandler script. (Ex. levelMultiplier * 5 which eq 100)
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
        if(scoreCount <= maxRewardPoint)
        {
            if (scoreCount % 20 == 0)
            {
                pointHandler.SetNewReward(scoreCount);
            }
        }
    }
}
