using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpShooting : MonoBehaviour {

    [Header("Shhoting Component")]
    [SerializeField] private Transform spawnPostion;
    [SerializeField] private PlayerBulletMotion bullet;


    [Header("Shooting data")]
    private float flt_Firerate;
    private float flt_CurrentTimeForFireRate = 0;
    private float flt_MaxActiveTime;
    private float flt_CurrentTimePassedForActive = 0;


    private void Start() {

        flt_Firerate = PowerUPManager.instance.flt_ShootingFirerate;
        flt_MaxActiveTime = PowerUPManager.instance.flt_ShootingMaxTime;


    }

  

    public void ActivateShooting() {

        gameObject.SetActive(true);
        flt_CurrentTimeForFireRate = 0f;
        flt_CurrentTimePassedForActive = 0f;
    }

    private void OnEnable() {
        GameManager.Instance.GamePlayingState += MyUpdate;
    }

    private void OnDisable() {
        GameManager.Instance.GamePlayingState -= MyUpdate;
    }


    private void MyUpdate() {

        CalculateActiveTime();
        HandleShooting();

        //if (Input.GetMouseButtonDown(0)) {

        //    FireBullet();
        //}
    }

    private void CalculateActiveTime() {
        flt_CurrentTimePassedForActive += Time.deltaTime;

        if (flt_CurrentTimePassedForActive >= flt_MaxActiveTime) {
            gameObject.SetActive(false);
        }
    }
    private void HandleShooting() {
        flt_CurrentTimeForFireRate += Time.deltaTime;
        if (flt_CurrentTimeForFireRate >= flt_Firerate) {
            flt_CurrentTimeForFireRate = 0f;
            FireBullet();
        }
    }

    private void FireBullet() {

        Instantiate(bullet, spawnPostion.position, Quaternion.identity);

    }
}
