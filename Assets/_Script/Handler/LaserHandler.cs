using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LaserHandler : MonoBehaviour {
    [Header("Laser")]
    [SerializeField] private List<LaserBeam> list_All_Laser;
    [SerializeField] private List<LaserBeam> list_CurrentTimeLaser;

    [Header("Laserdata")]
    [SerializeField] private int CurrentCount;
    [SerializeField] private int MaxCount;
    [SerializeField] private float flt_ShownAllLaserTime;
    [SerializeField] private float flt_ShownCurrentTimeLaserIndicater;
    [SerializeField] private float flt_LaserShownTime;
    [SerializeField] private float flt_DelayOfIndicater;


    private bool hasLaserAlreadyEnded = false;
    private int counterForCheckingDestroyedLaserOfCurrentRound = 0;

    public void SetLaserLeval() {

        ChooseRandomLaser();
    }

    private void ChooseRandomLaser() {
        int laserActivationCount = CheckProbabiltyAndGetCount();

        List<LaserBeam> SwapingList = new List<LaserBeam>();
        for (int i = 0; i < list_All_Laser.Count; i++) {
            SwapingList.Add(list_All_Laser[i]);

        }
        list_CurrentTimeLaser.Clear();
        for (int i = 0; i < laserActivationCount; i++) {

            int index = Random.Range(0, SwapingList.Count);

            list_CurrentTimeLaser.Add(SwapingList[index]);
            SwapingList.RemoveAt(index);
        }

        hasLaserAlreadyEnded = false;
        counterForCheckingDestroyedLaserOfCurrentRound = 0;
        for (int i = 0; i < list_CurrentTimeLaser.Count; i++) {
            // list_CurrentTimeLaser[i].ShowIndicater(true);
            list_CurrentTimeLaser[i].StartLaserShootingProcess();
        }
    }

    public void LaserShootingProcessCompleted() {

        if (hasLaserAlreadyEnded) {
            return;
        }

        hasLaserAlreadyEnded = true;

        CurrentCount += 1;

        if(CurrentCount >= MaxCount) {

            LevelManager.instance.CompleteBoss();
            Destroy(gameObject);
            return;
        }
        StartCoroutine(WaitAndChooseLaserAgain());       
    }

    private IEnumerator WaitAndChooseLaserAgain() {

        yield return new WaitForSeconds(0.2f);
        ChooseRandomLaser();
    }


    public void LaserDestroyedByPlayer(LaserBeam _laserBeam) {

        list_All_Laser.Remove(_laserBeam);

        if (list_CurrentTimeLaser.Contains(_laserBeam)){
            counterForCheckingDestroyedLaserOfCurrentRound += 1;
        }
       
        Destroy(_laserBeam.gameObject);
        if (list_All_Laser.Count == 0) {
            LevelManager.instance.CompleteBoss();
            Destroy(gameObject);
        }
        else if(counterForCheckingDestroyedLaserOfCurrentRound >= list_CurrentTimeLaser.Count) {

            LaserShootingProcessCompleted();
        }
    }

    private int CheckProbabiltyAndGetCount() {

        int index = Random.Range(0, 100);

        //if (list_All_Laser.Count == 0) {
        //    return 0;
        //}

        int MyINdex = 1;

        if (list_All_Laser.Count >= 4) {
            if (index < 35) {
                MyINdex = 1;
            }
            else if (index < 70) {
                MyINdex = 2;
            }
            else if (index < 80) {
                MyINdex = 3;
            }
            else {
                MyINdex = 4;
            }
        }
        else if (list_All_Laser.Count >= 3) {

            if (index < 40) {
                MyINdex = 1;
            }
            else if (index < 75) {
                MyINdex = 2;
            }
            else {
                MyINdex = 3;
            }
        }
        else if (list_All_Laser.Count >= 2) {

            if (index < 0) {
                MyINdex = 1;
            }
            else {
                MyINdex = 2;
            }
        }

        return MyINdex;
    }

    //private IEnumerator ShowAllLaserPostion() {
    //    //for (int i = 0; i < list_All_Laser.Count; i++) {
    //    //    list_All_Laser[i].gameObject.SetActive(true); 
    //    //    list_All_Laser[i].ShowIndicater(false);
    //    //    list_All_Laser[i].ShowLaser(false);
    //    //    list_All_Laser[i].ShootingCollider.enabled = true;
    //    //    list_All_Laser[i].LaserCollider.enabled = false;
    //    //    list_All_Laser[i].ShowPosition(true);
    //    //}

    //    yield return new WaitForSeconds(flt_ShownAllLaserTime);



    //    int laserActivationCount = CheckProbabiltyAndGetCount();
    //    SetRandomLaserShowIndicaater(laserActivationCount);
    //    yield return new WaitForSeconds(flt_ShownCurrentTimeLaserIndicater);


    //    //if (MyINdex >= list_All_Laser.Count - 1) {
    //    //    LevelManager.instance.CompleteBoss();
    //    //    Attack = null;
    //    //}
    //    //else {
    //    //    SetRandomLaserShowIndicaater(MyINdex);

    //    //    Debug.Log("Activate Indiacter");
    //    //    yield return new WaitForSeconds(flt_ShownCurrentTimeLaserIndicater);
    //    //    Debug.Log("Activate Laser");
    //    //    ShootCurrentLaser();

    //    //    yield return new WaitForSeconds(flt_LaserShownTime);
    //    //    Debug.Log("Disable Laser");
    //    //    DisableLaser();
    //    //    Debug.Log("Stop Laser");
    //    //}



    //}



    //private void SetRandomLaserShowIndicaater(int INdex) {

    //    List<LaserBeam> SwapingList = new List<LaserBeam>();
    //    for (int i = 0; i < list_All_Laser.Count; i++) {
    //        SwapingList.Add(list_All_Laser[i]);

    //    }
    //    list_CurrentTimeLaser.Clear();
    //    for (int i = 0; i < INdex; i++) {

    //        int index = Random.Range(0, SwapingList.Count);

    //        list_CurrentTimeLaser.Add(SwapingList[index]);
    //        SwapingList.RemoveAt(index);
    //    }

    //    for (int i = 0; i < list_CurrentTimeLaser.Count; i++) {
    //        list_CurrentTimeLaser[i].ShowIndicater(true);
    //    }

    //    //for (int i = 0; i < list_All_Laser.Count; i++) {


    //        //   // list_All_Laser[i].ShootingCollider.enabled = false;
    //        //    if (list_CurrentTimeLaser.Contains(list_All_Laser[i])) {
    //        //        list_All_Laser[i].ShowIndicater(true);
    //        //    }
    //        //    //else {
    //        //    //    list_All_Laser[i].ShowPosition(false);
    //        //    //    list_All_Laser[i].gameObject.SetActive(false);
    //        //    //}


    //        //}

    //}



    //private void ShootCurrentLaser() {
    //    for (int i = 0; i < list_CurrentTimeLaser.Count; i++) {
    //        list_CurrentTimeLaser[i].ShowIndicater(false);
    //        list_CurrentTimeLaser[i].ShowLaser(true);
    //        list_CurrentTimeLaser[i].LaserCollider.enabled = true;
    //    }
    //}

    //private void DisableLaser() {
    //    for (int i = 0; i < list_All_Laser.Count; i++) {
    //        list_All_Laser[i].ShowPosition(false);
    //        list_All_Laser[i].ShowIndicater(false);
    //        list_All_Laser[i].ShowLaser(false);
    //        list_All_Laser[i].ShootingCollider.enabled = true;
    //        list_All_Laser[i].gameObject.SetActive(false);
    //    }
    //    CurrentCount++;
    //    if (CurrentCount >= MaxCount) {
    //        LevelManager.instance.CompleteBoss();
    //        Attack = null;
    //    }
    //    else {
    //        SetLaserforNewWave();
    //    }
    //}

    //private void SetLaserforNewWave() {
    //    StartCoroutine(ShowAllLaserPostion());
    //}

    //public void RemoveThisLaser(LaserBeam Laser) {

    //    list_All_Laser.Remove(Laser);
    //    list_CurrentTimeLaser.Remove(Laser);
    //    Laser.gameObject.SetActive(false);
    //}
}


    //[Header("laser")]
    //[SerializeField] private List<LaserBeam> all_LeserBeam;
  


    //[Header("LaserTimedata")]
    //[SerializeField] private int LaserCounter;
    //[SerializeField] private int MaxCounter;
    //[SerializeField] private bool isLaserIndcater;
    //[SerializeField] private float flt_maxTimeForLaser;
    //[SerializeField] private float flt_maxTimeLaserIndicater;
    //[SerializeField] private float flt_CurrentTimeForLaserIndicater;
    //[SerializeField] private List<LaserBeam> list_CurrenTimeLaserBeam;

    //private Coroutine cour_ShowLaser;


    
    //public void SetLaserLeval() {
    //    this.gameObject.SetActive(true);

    //    StartCoroutine(DelayOfLaserActvation());
       
    //}


    //private IEnumerator DelayOfLaserActvation() {

    //    for (int i = 0; i < all_LeserBeam.Count; i++) {

    //        all_LeserBeam[i].gameObject.SetActive(true);
    //        all_LeserBeam[i].SetIndiacter(true);
    //        all_LeserBeam[i].SetLaserActivetion(false);
    //    }
    //    yield return new WaitForSeconds(2);
    //    for (int i = 0; i < all_LeserBeam.Count; i++) {

    //        all_LeserBeam[i].SetIndiacter(false);
           
    //    }
    //    LaserCounter = 0;
    //    GameManager.Instance.GamePlayingState += MyUpdate;
    //    SetProbability();
    //    isLaserIndcater = false;
    //    flt_CurrentTimeForLaserIndicater = 0;
    //}

    //private void SetProbability() {


    //    int index = Random.Range(0, 100);

    //    if (index < 35) {
    //        SetRandomLaser(1);
    //    }
    //    else if (index < 70) {
    //        SetRandomLaser(2);
    //    }
    //    else if (index < 80) {
    //        SetRandomLaser(3);
    //    }
    //    else {
    //        SetRandomLaser(4);
    //    }
    //}

    //private void SetRandomLaser(int Index) {
    //    List<LaserBeam> list_NotRepeatRefrensh = new List<LaserBeam>();

    //    if (Index < 3) {
    //        for (int i = 0; i < all_LeserBeam.Count; i++) {
    //            list_NotRepeatRefrensh.Add(all_LeserBeam[i]);
    //        }
    //    }

    //    list_CurrenTimeLaserBeam.Clear();
    //    List<LaserBeam> Refrence_Beam = new List<LaserBeam>();
       
    //    for (int i = 0; i < Index; i++) {

    //        int Current;
    //        if (Index < 3) {
    //            Current = SetNotRepeatIndex(list_NotRepeatRefrensh, Refrence_Beam) ;
    //        }
    //        else {
    //            Current = Random.Range(0, all_LeserBeam.Count);
    //        }
               
            
           
    //        list_CurrenTimeLaserBeam.Add(all_LeserBeam[Current]);
            

    //    }
    //    for (int i = 0; i < list_CurrenTimeLaserBeam.Count; i++) {
    //        list_CurrenTimeLaserBeam[i].SetIndiacter(true);
    //    }


    //}

    //private int SetNotRepeatIndex(List<LaserBeam> list_NotRepeatRefrensh, List<LaserBeam> Refrence_Indicater) {
    //    int index = 0;
    //    bool isSpawn = false;
    //    while (!isSpawn) {

    //        index = Random.Range(0, Refrence_Indicater.Count);
    //        if (list_NotRepeatRefrensh.Contains(Refrence_Indicater[index])) {
    //            isSpawn = false;
    //        }
    //        else {
    //            isSpawn = true;
    //        }
    //    }
    //    return index;
    //}

    //private void MyUpdate() {
    //    ShowIndicater(); // Indiacter Caluculation
       
    //}

    //private void StopLaserLevel() {
    //    for (int i = 0; i < all_LeserBeam.Count; i++) {

    //        all_LeserBeam[i].gameObject.SetActive(false);
    //    }
    //    this.gameObject.SetActive(false);
    //}

    //private void ShowIndicater() {
    //    if (isLaserIndcater) {
    //        return;
    //    }
    //    flt_CurrentTimeForLaserIndicater += Time.deltaTime;
    //    if (flt_CurrentTimeForLaserIndicater > flt_maxTimeLaserIndicater) {
    //        isLaserIndcater = true;
    //        if (cour_ShowLaser != null) {
    //            StopCoroutine(cour_ShowLaser);
    //        }
    //       cour_ShowLaser =  StartCoroutine(ShowLaser());
    //    }
    //}

    //private IEnumerator ShowLaser() {

       
    //    for (int i = 0; i < list_CurrenTimeLaserBeam.Count; i++) {

    //        list_CurrenTimeLaserBeam[i].SetIndiacter(false);
    //        list_CurrenTimeLaserBeam[i].SetLaserActivetion(true);
    //    }

    //    yield return new WaitForSeconds(flt_maxTimeForLaser);
    //    for (int i = 0; i < list_CurrenTimeLaserBeam.Count; i++) {

    //        list_CurrenTimeLaserBeam[i].SetLaserActivetion(false);
    //    }

    //    LaserCounter++;
    //    if (LaserCounter < MaxCounter) {
    //        isLaserIndcater = false;
    //        flt_CurrentTimeForLaserIndicater = 0;
    //        SetProbability();

    //    }
    //    else {
    //        StopLaserLevel();
    //        LevelManager.instance.CompleteBoss();
    //    }
    //}

    //public void RemoveThisLaser(GameObject gameObject) {

    //    Debug.Log(gameObject.name);
    //    LaserBeam Current = gameObject.GetComponent<LaserBeam>();
    //    gameObject.gameObject.SetActive(false);
    //    int Index = all_LeserBeam.IndexOf(Current);
    //    
       
       
    //}

