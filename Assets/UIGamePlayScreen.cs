using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIGamePlayScreen : MonoBehaviour {

    [Header("Component")]
    [SerializeField] private TextMeshProUGUI txt_Score;
    [Header("GamePlayScreen Data")]
    [SerializeField] int increasedScore;
    [SerializeField] int CurrentScore;
    [SerializeField] private float flt_ScoreIncreasedTime;
    [SerializeField] private float flt_ScoreIncreasedMultypler;
    [SerializeField] private float flt_CurrentTime;
    [SerializeField] private float flt_ThisWaveScoreINcresedTime;

    private void OnEnable() {

        GameManager.Instance.ChangeGameSpeed += updateSpeed;
        GameManager.Instance.GamePlayingState += MyUpdate;
        flt_ThisWaveScoreINcresedTime = flt_ScoreIncreasedTime * flt_ScoreIncreasedMultypler;
    }

    private void updateSpeed(float GameSpeed) {
        if (GameSpeed >= 1) {
            flt_ScoreIncreasedMultypler = 1/GameSpeed;
        }
    }

    private void OnDisable() {
        GameManager.Instance.GamePlayingState -= MyUpdate;
    }

    private void MyUpdate() {
        flt_CurrentTime += Time.deltaTime;
        if (flt_CurrentTime > flt_ThisWaveScoreINcresedTime) {
            IncresedScore();
        }

    }

    private void IncresedScore() {
        CurrentScore += increasedScore;
        txt_Score.text = CurrentScore + "m";
        flt_ThisWaveScoreINcresedTime += flt_ScoreIncreasedMultypler * flt_ScoreIncreasedTime;
    }
}
