using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public int currentScore = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       GetComponent<TMPro.TextMeshProUGUI>().text = currentScore.ToString();
    }
    public void increaseScore(int scoreToIncreaseBy) {
        currentScore += scoreToIncreaseBy;
        print(currentScore);
    }
}
