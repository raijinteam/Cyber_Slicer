using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpShield : MonoBehaviour {


    [SerializeField] private bool isShieldActive;
    [SerializeField] private float flt_CurrentTime;
    [SerializeField] private float flt_maxTime;


    public void ActiveShield() {
        this.gameObject.SetActive(true);
      
        GameManager.Instance.IsShieldActive = true;
        flt_maxTime = PowerUPManager.instance.flt_ShieldMaxTime;
        flt_CurrentTime = 0;
        isShieldActive = true;
    }
    private void OnEnable() {
        GameManager.Instance.GamePlayingState += MyUpdate;
    }

    private void MyUpdate() {

        flt_CurrentTime += Time.deltaTime;
        if (flt_CurrentTime > flt_maxTime) {
            DisableShield();
        }
    }

    public void DisableShield() {
        if (!isShieldActive) {
            return;
        }
       
       
      
        StartCoroutine(Delay_Shield());
        
    }

    private IEnumerator Delay_Shield() {
        yield return new WaitForSeconds(1);
        isShieldActive = false;
        GameManager.Instance.IsShieldActive = false;
        this.gameObject.SetActive(false);
    }
}

