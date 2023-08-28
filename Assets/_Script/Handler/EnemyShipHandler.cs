using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipHandler : MonoBehaviour {

    [Header("Enemy Component")]
    [SerializeField] private EnemyShipMotion obj_Enemy;
    [SerializeField] private Transform spawnPostion;
    [SerializeField] private Transform endPostion;
    [SerializeField] private Transform wokingPostion;

    [Header("Enemy data")]
    [SerializeField] private float flt_TimeToBossGoesToWorkingPostion;
    [SerializeField] private float flt_MaxTimeToActiveBoss;
    [SerializeField] private float flt_TimeToBossGoesToEndPostion;

    private void Start() {
        LevelManager.instance.BossActivationStatus += BossActivationStatus;
    }

    private void BossActivationStatus(bool isBossActive) {
        if (isBossActive) {
            StopAllCoroutines();
            StartCoroutine(EnemyGoesToEndpostion());
        }
      
        
    }

    public void SetEnemy() {

        obj_Enemy.gameObject.SetActive(true);
        wokingPostion.position = new Vector3(spawnPostion.position.x, wokingPostion.position.y, wokingPostion.position.z);
        endPostion.position = new Vector3(spawnPostion.position.x, endPostion.position.y, endPostion.position.z);
        obj_Enemy.transform.position = spawnPostion.position;
        StartCoroutine(BossWorking());

    }

    private IEnumerator BossWorking() {

        float flt_CurrentTime = 0;

        while (flt_CurrentTime < 1) {

            obj_Enemy.transform.position = Vector3.Lerp(spawnPostion.position, wokingPostion.position, flt_CurrentTime);
            flt_CurrentTime += Time.deltaTime / flt_TimeToBossGoesToWorkingPostion;
            yield return null;
        }

        obj_Enemy.transform.position = wokingPostion.position;
        obj_Enemy.setBulletSpawnActive(true);

        yield return new WaitForSeconds(flt_MaxTimeToActiveBoss);
         wokingPostion.position = obj_Enemy.transform.position;
        endPostion.transform.position = new Vector3(wokingPostion.position.x, endPostion.transform.position.y, endPostion.transform.position.z);
        obj_Enemy.setBulletSpawnActive(false);

        StartCoroutine(EnemyGoesToEndpostion());
    }

    private IEnumerator EnemyGoesToEndpostion() {
        float flt_DisableTime = 0;
        while (flt_DisableTime < 1) {

            Debug.Log("CoyuritineStrart");
            obj_Enemy.transform.position = Vector3.Lerp(wokingPostion.position, endPostion.position, flt_DisableTime);
            flt_DisableTime += Time.deltaTime / flt_TimeToBossGoesToEndPostion;
            yield return null;
        }
        obj_Enemy.gameObject.SetActive(false);
        LevelManager.instance.StopType1Level = false;
    }
}
