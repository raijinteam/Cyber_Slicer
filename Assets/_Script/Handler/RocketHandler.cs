using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketHandler : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private Collider2D rocket;
    [SerializeField] private GameObject Spawner_Rocket;


    [Header("Rocket data")]
    [SerializeField] private bool isRocketIndicating;
    [SerializeField] private float flt_MaxTimeToIndicaterRocket;
    [SerializeField] private float flt_CurrentTimeForIndiacter;
    [SerializeField] private float flt_TimeToRocketGoesToEndPostion;
    [SerializeField] private float flt_LerpSpeed;

    private float flt_RocketYPostion = 20f;

    private void OnEnable() {
        GameManager.Instance.GamePlayingState += MyUpdate;
        LevelManager.instance.BossActivationStatus += SetBossActivation;
    }


    private void OnDisable() {
        GameManager.Instance.GamePlayingState -= MyUpdate;
        LevelManager.instance.BossActivationStatus -= SetBossActivation;
    }

    public void setRocket() {

        Spawner_Rocket.gameObject.SetActive(true);
        flt_CurrentTimeForIndiacter = 0;
        isRocketIndicating = true;


    }

    private void SetBossActivation(bool isBossActive) {
        if (isBossActive) {
            if (isRocketIndicating) {
                isRocketIndicating = false;
                StartCoroutine(SpwnRocket());
            }
           
        }
    }

    private void MyUpdate() {

        TimeCalculationForRocketIndicater();
       
    }

    private void TimeCalculationForRocketIndicater() {
        if (!isRocketIndicating) {
            return;
        }
        ChangingIndicaterAsPerPlayer();
        flt_CurrentTimeForIndiacter += Time.deltaTime;
        if (flt_CurrentTimeForIndiacter > flt_MaxTimeToIndicaterRocket) {
            isRocketIndicating = false;
          
            StartCoroutine(SpwnRocket());
        }
    }

    private void ChangingIndicaterAsPerPlayer() {

       Vector3 targetPostion = new Vector3(GameManager.Instance.MyPlayer.transform.position.x, Spawner_Rocket.transform.position.y, 0);

        Spawner_Rocket.transform.position = Vector3.MoveTowards(Spawner_Rocket.transform.position, targetPostion, flt_LerpSpeed * Time.deltaTime);
    }

    private IEnumerator SpwnRocket() {

        yield return new WaitForSeconds(1);
        Spawner_Rocket.gameObject.SetActive(false);
        rocket.gameObject.SetActive(true);
        rocket.enabled = false;
        float flt_CurerentTime = 0;
        
        rocket.transform.position = new Vector3(Spawner_Rocket.transform.position.x, 20, 0);
        
        Vector3 rocketStartPosition = rocket.transform.position;
        Vector3 rocketTargetEndPostion = new Vector3(Spawner_Rocket.transform.position.x, -flt_RocketYPostion, 0);

        bool isCollider = false;
        while (flt_CurerentTime < 1) {

            flt_CurerentTime += Time.deltaTime / flt_TimeToRocketGoesToEndPostion;

            rocket.transform.position = Vector3.Lerp(rocketStartPosition, rocketTargetEndPostion, flt_CurerentTime);
            if (rocket.transform.position.y < 6 && !isCollider) {
                rocket.enabled = true;
                isCollider = true;
            }
            yield return null;
        }

        LevelManager.instance.StopType1Level = false;
        rocket.gameObject.SetActive(false);
    
    }
}
