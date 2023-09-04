using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUPManager : MonoBehaviour {

    public static PowerUPManager instance;
   


    [Header("PoswerUpdata")]
    [Header("Magenet Data")]
     public float flt_MagnetTime;

    [Header("Shooting Data")]
    public float flt_ShootingMaxTime;
    public float flt_ShootingFirerate;
    


    [Header("Shield data")]
    public float flt_ShieldMaxTime;

    [Header("SpeedBoost Data")]
    public float flt_SpeedBoostMaxTime;
    public float flt_BoostMultiplier;

    [Header("2X Data")]
    public float flt_2XMaxTime;

    [Header("Chronos Data")]
    public float flt_ChronosMaxTime;
    public float flt_ChronosMultiplier;


    

   


    private void Awake() {
        instance = this;
    }


    private void Update() {

        if (Input.GetKeyDown(KeyCode.F1)) {
            GameManager.Instance.MyPlayer.ActiveMagenetPowerUp();
        }
        else if (Input.GetKeyDown(KeyCode.F2)) {
            GameManager.Instance.MyPlayer.ActiveShootingPowerUP();
        }
        else if (Input.GetKeyDown(KeyCode.F3)) {
            GameManager.Instance.MyPlayer.ActiveShieldPowerUp();
        }
        else if (Input.GetKeyDown(KeyCode.F4)) {
            GameManager.Instance.MyPlayer.ActiveSpeedBoostPowerUp();
        }
        else if (Input.GetKeyDown(KeyCode.F5)) {
            GameManager.Instance.MyPlayer.Active2XPowerUp();
        }
        else if (Input.GetKeyDown(KeyCode.F6)) {
            GameManager.Instance.MyPlayer.ActiveChronosPowerUp();
        }
    }

   
}

