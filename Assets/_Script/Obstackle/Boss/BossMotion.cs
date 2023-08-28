using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossMotion : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private Transform spawn_BulletPostion;
    [SerializeField] private BossBulletMotion bossBullet;

    [Header("Boss Data")]
    [SerializeField] private bool isAttacking;
    [SerializeField] private int Count;
    [SerializeField] private bool isBulletSpawn;
    [SerializeField] private float flt_CurrentTime;
    [SerializeField] private float flt_FireRate;
    private float flt_MovementSpeed = 2;
    [SerializeField] private float flt_CounterInterVal;

    [Header("Type 2 Boss")]
    [SerializeField] private bool isType2Boss;
    [SerializeField] private float flt_MinFireRate;
    [SerializeField] private float flt_MaxFireRate;
   

    private void OnEnable() {
        GameManager.Instance.GamePlayingState += MyUpdate;
    }
    private void OnDisable() {

        GameManager.Instance.GamePlayingState -= MyUpdate;
    }

    public void setBulletSpawnActive(bool Value) {
        isBulletSpawn = Value;
    }
    private void MyUpdate() {
        if (!isType2Boss) {
            MovememeGooesToPlayer();
        }
       
        SpawnBulletAsPerFirerate();
    }

    private void MovememeGooesToPlayer() {
       
        Vector3 targetPostion = new Vector3(GameManager.Instance.MyPlayer.transform.position.x, transform.position.y, transform.position.z);

        transform.position = Vector3.MoveTowards(transform.position, targetPostion, flt_MovementSpeed * Time.deltaTime);
    }

    private void SpawnBulletAsPerFirerate() {
        if (!isBulletSpawn) {
            return;
        }
        if (isAttacking) {
            return;
        }

        flt_CurrentTime += Time.deltaTime;
        if (flt_CurrentTime > flt_FireRate) {
            flt_CurrentTime = 0;
            fireBullet();
            if (isType2Boss) {
                SetRandomFireRate();
            }
        }
    }

    private void SetRandomFireRate() {

        float flt_CurrentFirerate = Random.Range(flt_MinFireRate, flt_MaxFireRate);
        flt_FireRate = flt_CurrentFirerate;
    }

    private void fireBullet() {
        isAttacking = true;
        if (isType2Boss) {
            Instantiate(bossBullet, spawn_BulletPostion.position, Quaternion.identity);
            isAttacking = false;
        }
        else {
            StartCoroutine(FireCountingBullet());
        }
      
       

    }

    private IEnumerator FireCountingBullet() {
        for (int i = 0; i < Count; i++) {
            Instantiate(bossBullet, spawn_BulletPostion.position, Quaternion.identity);
            yield return new WaitForSeconds(flt_CounterInterVal);
        }
        isAttacking = false;
    }
}
