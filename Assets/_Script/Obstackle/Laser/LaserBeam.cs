using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{


    [Header("Laser Component")]
    [SerializeField] private int laserIndex;
    [SerializeField] private GameObject laserIndiacter;
    [SerializeField] private GameObject laser;
    [SerializeField] private GameObject postion;
    public Collider ShootingCollider;
    public Collider LaserCollider;
       

    [SerializeField] private Transform start_Postion;
    [SerializeField] private Transform end_Postion;

    [Header("LaserData")]
    [SerializeField] private LayerMask player;
    
    //public void ShowPosition(bool isActive) {
    //    postion.gameObject.SetActive(isActive);
       

    //}
    //public void ShowIndicater(bool isActive) {

    //    laserIndiacter.gameObject.SetActive(isActive);
    //}
    //public void ShowLaser(bool isActive) {
    //    laser.gameObject.SetActive(isActive);
    //}


    public void StartLaserShootingProcess() {

        StartCoroutine(LaserShootingProcess());
    }

    private IEnumerator LaserShootingProcess() {

        laserIndiacter.SetActive(true);
        yield return new WaitForSeconds(5f);

        laserIndiacter.SetActive(false);
        laser.SetActive(true);

        yield return new WaitForSeconds(3f);

        laser.SetActive(false);

        GameManager.Instance.currentLaserHandler.LaserShootingProcessCompleted();
    }


    public void LaserDestroyedByBullet() {

        StopAllCoroutines();
        GameManager.Instance.currentLaserHandler.LaserDestroyedByPlayer(this);

    }

}
