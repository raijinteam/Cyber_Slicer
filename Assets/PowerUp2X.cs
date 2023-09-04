using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp2X : MonoBehaviour {

    [SerializeField] private float flt_maxTime;
    [SerializeField] private float flt_CurrentTime;


    public void Active2X() {

        this.gameObject.SetActive(true);
        
        GameManager.Instance.Is2XActive = true;
        flt_maxTime = PowerUPManager.instance.flt_2XMaxTime;
        flt_CurrentTime = 0;
    }

    private void OnEnable() {
        GameManager.Instance.GamePlayingState += MyUpdate;
    }

   
    private void OnDisable() {
        GameManager.Instance.GamePlayingState -= MyUpdate;
    }
    private void MyUpdate() {
        flt_CurrentTime += Time.deltaTime;
        if (flt_CurrentTime > flt_maxTime) {
            DisablePowerUp();
        }
    }

    private void DisablePowerUp() {

        GameManager.Instance.Is2XActive = false;
     
        this.gameObject.SetActive(false);
    }
}
