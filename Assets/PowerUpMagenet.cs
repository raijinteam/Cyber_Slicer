using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpMagenet : MonoBehaviour {

    [SerializeField] private float flt_MaxTime;
    [SerializeField] private float flt_CurrentTime;
    [SerializeField] private MagnetTrigger magnetTrigger;
    [SerializeField] private MagnetTrigger currentmagnetTrigger;
    


   

    public void ActivateMagnet() {
        flt_CurrentTime = 0;
        this.gameObject.SetActive(true);
        flt_MaxTime = PowerUPManager.instance.flt_MagnetTime;
        currentmagnetTrigger = Instantiate(magnetTrigger, transform.position, transform.rotation);
      
       
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

            Destroy(currentmagnetTrigger.gameObject);
            this.gameObject.SetActive(false);
           
        }
    }

    


}
