using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public TMP_Text scoreText;

    private void Update()
    {
        scoreText.text = "Score: " + GameManager.Instance.fullSaveData.scoreAmount.ToString();
    }
}
