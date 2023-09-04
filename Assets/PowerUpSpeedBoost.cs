using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpeedBoost : MonoBehaviour {

    [SerializeField] private float flt_CurrentTime;
    [SerializeField] private float flt_MaxTime;
    [SerializeField] private float flt_SpeedBoostMultiplier;

    public void ActiveSpeedBoost() {

        flt_CurrentTime = 0;
        flt_SpeedBoostMultiplier = PowerUPManager.instance.flt_SpeedBoostMaxTime;
        flt_MaxTime = PowerUPManager.instance.flt_BoostMultiplier;
        GameManager.Instance.IsSpeedBoostActive = true;
       
        GameManager.Instance.ChangeGameSpeed?.Invoke(flt_SpeedBoostMultiplier);
        
        this.gameObject.SetActive(true);
    }

    private void OnEnable() {

        GameManager.Instance.GamePlayingState += MyUpdate;
    }
    private void OnDisable() {

        GameManager.Instance.GamePlayingState -= MyUpdate;
    }

    private void MyUpdate() {
        flt_CurrentTime += Time.deltaTime;
        if (flt_CurrentTime > flt_MaxTime) {

            DisablePowerUp();

        }
    }

    private void DisablePowerUp() {

        GameManager.Instance.ChangeGameSpeed?.Invoke(1);
       
        StartCoroutine(Delay_StopPowerUp());
    }

    private IEnumerator Delay_StopPowerUp() {
        yield return new WaitForSeconds(0.5f);
        GameManager.Instance.IsSpeedBoostActive = false;
        this.gameObject.SetActive(false);


    }
}
        
        
    

