using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LaserHandler : MonoBehaviour
{

    [Header("laser")]
    [SerializeField] private LaserBeam[] all_LeserBeam;
    [SerializeField] private GameObject[] all_LaserIndiacter;


    [Header("LaserTimedata")]
    [SerializeField] private int LaserCounter;
    [SerializeField] private int MaxCounter;
    [SerializeField] private bool isLaserIndcater;
    [SerializeField] private float flt_maxTimeForLaser;
    [SerializeField] private float flt_maxTimeLaserIndicater;
    [SerializeField] private float flt_CurrentTimeForLaserIndicater;
    [SerializeField] private List<GameObject> list_CurrentTimeLaserIndicater;
    [SerializeField] private List<LaserBeam> list_CurrenTimeLaserBeam;

    private Coroutine cour_ShowLaser;


    
    public void SetLaserLeval() {
        this.gameObject.SetActive(true);
        LaserCounter = 0;
        
        GameManager.Instance.GamePlayingState += MyUpdate;
        SetProbability();
        isLaserIndcater = false;
        flt_CurrentTimeForLaserIndicater = 0;
       
    }

    private void SetProbability() {
        int index = Random.Range(0, 100);

        if (index < 35 ) {
            SetRandomLaser(1);
        }
        else if (index < 70 ) {
            SetRandomLaser(2);
        }else if(index < 80) {
            SetRandomLaser(3);
        }
        else {
            SetRandomLaser(4);
        }
    }

    private void SetRandomLaser(int Index) {
        List<GameObject> list_NotRepeatRefrensh = new List<GameObject>();

        if (Index <3) {
            for (int i = 0; i < list_CurrentTimeLaserIndicater.Count; i++) {
                list_NotRepeatRefrensh.Add(list_CurrentTimeLaserIndicater[i]);
            }
        }
        list_CurrentTimeLaserIndicater.Clear();
        list_CurrenTimeLaserBeam.Clear();
        List<GameObject> Refrence_Indicater = new List<GameObject>();
        List<LaserBeam> Refrence_Beam = new List<LaserBeam>();
        for (int i = 0; i < all_LaserIndiacter.Length; i++) {

            Refrence_Indicater.Add(all_LaserIndiacter[i]);
            Refrence_Beam.Add(all_LeserBeam[i]);
        }
        for (int i = 0; i < Index; i++) {

            int Current;
            if (Index < 3) {
                Current = SetNotRepeatIndex(list_NotRepeatRefrensh, Refrence_Indicater) ;
            }
            else {
                Current = Random.Range(0, Refrence_Indicater.Count);
            }
               
            
           
            list_CurrenTimeLaserBeam.Add(Refrence_Beam[Current]);
            list_CurrentTimeLaserIndicater.Add(Refrence_Indicater[Current]);

        }
        for (int i = 0; i < list_CurrentTimeLaserIndicater.Count; i++) {
            list_CurrentTimeLaserIndicater[i].gameObject.SetActive(true);
        }


    }

    private int SetNotRepeatIndex(List<GameObject> list_NotRepeatRefrensh, List<GameObject> Refrence_Indicater) {
        int index = 0;
        bool isSpawn = false;
        while (!isSpawn) {

            index = Random.Range(0, Refrence_Indicater.Count);
            if (list_NotRepeatRefrensh.Contains(Refrence_Indicater[index])) {
                isSpawn = false;
            }
            else {
                isSpawn = true;
            }
        }
        return index;
    }

    private void MyUpdate() {
        ShowIndicater(); // Indiacter Caluculation
       
    }

    private void StopLaserLevel() {
        for (int i = 0; i < all_LeserBeam.Length; i++) {

            all_LeserBeam[i].gameObject.SetActive(false);
        }
        this.gameObject.SetActive(false);
    }

    private void ShowIndicater() {
        if (isLaserIndcater) {
            return;
        }
        flt_CurrentTimeForLaserIndicater += Time.deltaTime;
        if (flt_CurrentTimeForLaserIndicater > flt_maxTimeLaserIndicater) {
            isLaserIndcater = true;
            if (cour_ShowLaser != null) {
                StopCoroutine(cour_ShowLaser);
            }
           cour_ShowLaser =  StartCoroutine(ShowLaser());
        }
    }

    private IEnumerator ShowLaser() {

        for (int i = 0; i < list_CurrentTimeLaserIndicater.Count; i++) {
            list_CurrentTimeLaserIndicater[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < list_CurrenTimeLaserBeam.Count; i++) {

            list_CurrenTimeLaserBeam[i].gameObject.SetActive(true);
        }

        yield return new WaitForSeconds(flt_maxTimeForLaser);
        for (int i = 0; i < list_CurrenTimeLaserBeam.Count; i++) {

            list_CurrenTimeLaserBeam[i].gameObject.SetActive(false);
        }

        LaserCounter++;
        if (LaserCounter < MaxCounter) {
            isLaserIndcater = false;
            flt_CurrentTimeForLaserIndicater = 0;
            SetProbability();

        }
        else {
            StopLaserLevel();
            LevelManager.instance.CompleteBoss();
        }
    }
}
