using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score_Tracking : MonoBehaviour
{
    public static Score_Tracking instance;
    public Text goldText;
    public Text scoreText;
    int coinsCollected;
    private float score = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Update()
    {
        

        score += Time.deltaTime;
        scoreText.text = ((int)score).ToString();
    }

    public void ChangeCoinCount(int coinValue)
    {
        coinsCollected += coinValue;
        goldText.text = coinsCollected.ToString();
    }



}
