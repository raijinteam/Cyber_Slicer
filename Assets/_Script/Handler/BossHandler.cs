using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHandler : MonoBehaviour {

    [Header("Boss Component")]
    [SerializeField] private BossMotion obj_Boss;
    [SerializeField] private Transform spawnPostion;
    [SerializeField] private Transform endPostion;
    [SerializeField] private Transform wokingPostion;

    [Header("Boss data")]
    [SerializeField] private float flt_TimeToBossGoesToWorkingPostion;
    [SerializeField] private float flt_MaxTimeToActiveBoss;
    [SerializeField] private float flt_TimeToBossGoesToEndPostion;
   


    public void SetBoss() {

        obj_Boss.gameObject.SetActive(true);
        obj_Boss.transform.position = spawnPostion.position;
        StartCoroutine(BossWorking());

    }

    private IEnumerator BossWorking() {

        float flt_CurrentTime = 0;

        while (flt_CurrentTime < 1) {

            obj_Boss.transform.position = Vector3.Lerp(spawnPostion.position, wokingPostion.position, flt_CurrentTime);
            flt_CurrentTime += Time.deltaTime / flt_TimeToBossGoesToWorkingPostion;
            yield return null;
        }

        obj_Boss.transform.position = wokingPostion.position;
        obj_Boss.setBulletSpawnActive(true);
        yield return new WaitForSeconds(flt_MaxTimeToActiveBoss);
        obj_Boss.setBulletSpawnActive(false);
        float flt_DisableTime = 0;
        while (flt_DisableTime < 1) {

            obj_Boss.transform.position = Vector3.Lerp(wokingPostion.position, endPostion.position, flt_DisableTime);
            flt_DisableTime += Time.deltaTime / flt_TimeToBossGoesToEndPostion;
            yield return null;
        }

        obj_Boss.gameObject.SetActive(false);
        LevelManager.instance.CompleteBoss();
    }
}
