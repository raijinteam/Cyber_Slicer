using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpChronos : MonoBehaviour {

    [SerializeField] private float flt_maxTime;
    [SerializeField] private float flt_CurrentTime;
    [SerializeField] private float flt_ChronosMultiplier;


    public void ActiveChronos() {
        flt_CurrentTime = 0;
      
        flt_maxTime = PowerUPManager.instance.flt_ChronosMaxTime;
        flt_ChronosMultiplier = PowerUPManager.instance.flt_ChronosMultiplier;
        this.gameObject.SetActive(true);
        GameManager.Instance.ChangeGameSpeed?.Invoke(flt_ChronosMultiplier);
        
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
      
        GameManager.Instance.ChangeGameSpeed?.Invoke(1);
        this.gameObject.SetActive(false);
    }
}
