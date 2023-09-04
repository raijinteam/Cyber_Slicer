using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class uiRewiveScreen : MonoBehaviour {

    [Header("Component")]
    [SerializeField] private TextMeshProUGUI txt_Timer;

    [Header("Data")]
    [SerializeField] private float flt_CurrentTime;
    [SerializeField] private float flt_Maxtime;


    private void OnEnable() {

        GameManager.Instance.RewiveCalculation += MyUpdate;
        flt_CurrentTime = flt_Maxtime;
    }
    private void OnDisable() {
        GameManager.Instance.RewiveCalculation -= MyUpdate;
    }

    private void MyUpdate() {

        flt_CurrentTime -= Time.deltaTime/flt_Maxtime ;
        txt_Timer.text = flt_CurrentTime.ToString("f0");
        if (flt_CurrentTime < 0) {
            GameManager.Instance.GameOver();
        }
    }
}