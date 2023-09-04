using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipMotion : MonoBehaviour {

    [Header("Component")]
    [SerializeField] private Transform spwan_Postion;
    [SerializeField] private BossBulletMotion bullet;

    [Header("Enemy Data")]
    [SerializeField] private bool isBulletSpawning;
    [SerializeField] private float flt_MovementSpeed;
    [SerializeField] private bool isBulletSpawn;
    [SerializeField] private float flt_Firerate;
    [SerializeField] private float flt_CurrentTime;
    private Vector3 direction = Vector3.right;

    private void OnEnable() {
        GameManager.Instance.GamePlayingState += MyUpdate;
    }
    private void OnDisable() {

        GameManager.Instance.GamePlayingState -= MyUpdate;
    }

   

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.CompareTag(TagName.left_Boundry)) {
            direction = -direction;
        }
        else if (collision.CompareTag(TagName.rigth_Boundry)) {
            direction = -direction;
        }
    }

    private void MyUpdate() {

        MovememeGooesToPlayer();
        HandlingFireRate();
    }

    private void MovememeGooesToPlayer() {
        if (!isBulletSpawn) {
            return;
        }
        transform.Translate(direction * flt_MovementSpeed * Time.deltaTime);
    }

    private void HandlingFireRate() {
        if (!isBulletSpawn) {
            return;
        }
        else if (isBulletSpawning) {
            return;
        }
        flt_CurrentTime += Time.deltaTime;
        if (flt_CurrentTime > flt_Firerate) {
            flt_CurrentTime = 0;
            StartCoroutine(Delayof_Spawninng());
        }
    }

    public void setBulletSpawnActive(bool v) {
        isBulletSpawn = v;
        Debug.Log("V");
        isBulletSpawning = false;
    }

   

    private IEnumerator Delayof_Spawninng() {
        isBulletSpawning = true;
        yield return new WaitForSeconds(0.5f);
        Instantiate(bullet, spwan_Postion.position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        isBulletSpawning = false;
    }
}
