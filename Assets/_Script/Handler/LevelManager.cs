using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    [Header("TestMode")]
    [SerializeField] private bool isTest;

    [Header("LevelData")]
    [SerializeField] private bool isLevelUpdate;
    [SerializeField] private float currentAmmount;
    [SerializeField] private float incresedAmmount;
    [SerializeField] private float maxAmmount;
    [SerializeField] private float incresedLevelTime;

    

    [SerializeField] private float flt_CurrentTime;

    [Header("ExtraLevelAddIn Game")]
    [SerializeField] private float flt_CurretTimeForLevel;
    [SerializeField] private float flt_MaxTimeAddLevel;


    [Header("Boss Data")]
    [SerializeField] private bool isWaitingToEnbleObstackle;
    [SerializeField] private bool isWaitingToSpawn;
    [SerializeField] private int SpawnBossPath;
    [SerializeField] private int CurrentPath;



    [Header("Level Staus")]
    [SerializeField] private bool isBossActive;
    [SerializeField]private bool isSpawnObaskle;
    [SerializeField] private bool isType1LevelSpawn;
    public bool StopType1Level {
        set { isType1LevelSpawn = value; }
    }
  
    

    [Header("----Lavehandler Data ----")]
    [SerializeField] private PathHandler pathHandler;
   

  

    public delegate void LevelStatus(float ammount);
    public LevelStatus LevelUpdate;

    public delegate void BossStatus(bool isBossActive);
    public BossStatus BossActivationStatus;


    private void Awake() {
        instance = this;
    }


    private void Start() {

        isLevelUpdate = true;
        isSpawnObaskle = true;
        GameManager.Instance.GamePlayingState += myUpdate;
        
    }

    private void OnDisable() {
        GameManager.Instance.GamePlayingState -= myUpdate;
    }

    private void myUpdate() {
        if (isTest) {
            return;
        }
        LevelSpeedCaluclation();
        ExtraLevelAdd();
    }



    #region Type1 Enemy Setup
    private void ExtraLevelAdd() {
        if (isWaitingToSpawn) {
            return;
        }
        else if (isBossActive) {
            return;
        }
        else if (!isSpawnObaskle) {
            return;
        }
        else if (isType1LevelSpawn) {
            return;
        }



        flt_CurretTimeForLevel += Time.deltaTime;
        if (flt_CurretTimeForLevel > flt_MaxTimeAddLevel) {
            flt_CurretTimeForLevel = 0;
            isType1LevelSpawn = true;
            GetType1RandomLevel();
        }
    }


    private void Update() {
        if (Input.GetKeyDown(KeyCode.Q)) {
            GameManager.Instance.InstatiateRocketEnemy();
        }
        else if (Input.GetKeyDown(KeyCode.W)) {
            GameManager.Instance.InstatiateAstroidEnemy();
        }
        else if (Input.GetKeyDown(KeyCode.E)) {
            GameManager.Instance.InstatiateEnemyShip();
        }
        else if (Input.GetKeyDown(KeyCode.R)) {
            SetKeyBoss(0); //Laser
        }
        else if (Input.GetKeyDown(KeyCode.T)) {
            SetKeyBoss(1); //Boss 1
        }
        else if (Input.GetKeyDown(KeyCode.Y)) {
            SetKeyBoss(2); //Boss 1
        }
    }

   

    private void GetType1RandomLevel() {
        int Index = Random.Range(0, 3);
        
        switch (Index) {
            case 0:
                GameManager.Instance.InstatiateRocketEnemy();
                break;
            case 1:
                GameManager.Instance.InstatiateAstroidEnemy();
                break;
            case 2:
                GameManager.Instance.InstatiateEnemyShip();
                break;

        }

    }
    private IEnumerator LevelCompletedTime() {
        yield return new WaitForSeconds(5);
        isType1LevelSpawn = false;
    }

    #endregion


    #region Game Speed calculationAnd UpdatingAll
    private void LevelSpeedCaluclation() {
        if (!isBossActive) {
            return;
        }
        if (!isLevelUpdate) {
            return;
        }
        flt_CurrentTime += Time.deltaTime;
        if (flt_CurrentTime > incresedLevelTime) {
            flt_CurrentTime = 0;
            currentAmmount += incresedAmmount;
            if (currentAmmount < maxAmmount) {
                LevelUpdate?.Invoke(incresedAmmount);
            }
            else {
                isLevelUpdate = false;
            }

        }
    }

    #endregion


    #region Boss Handling
    public void SpawnRandomBoss() {
        if (isTest) {
            return;
        }
        if (isBossActive) {
            BossDisableHandler();
            return;
        }

        CurrentPath++;
        if (CurrentPath > SpawnBossPath) {

            BossSpawningHandler();


        }


    }
  
    private void BossDisableHandler() {
        if (!isWaitingToEnbleObstackle) {
            return;
        }
        BossActivationStatus?.Invoke(false);
        isBossActive = false;
        flt_CurretTimeForLevel = 0;
        flt_CurrentTime = 0;
        isWaitingToEnbleObstackle = false;

    }

    private void BossSpawningHandler() {

        if (!isWaitingToSpawn) {
            isWaitingToSpawn = true;
            isType1LevelSpawn = false;
            isSpawnObaskle = false;
            BossActivationStatus?.Invoke(true);
        }
        else {
            isWaitingToSpawn = false;
            isBossActive = true;
            SetRandomBoss();
            CurrentPath = 0;
        }
    }

    private void SetRandomBoss() {
      
        int Index = Random.Range(0, 100);

        if (Index < 35) {
            GameManager.Instance.InstantiateLaserBoss();
        }
        else if (Index < 70) {
            GameManager.Instance.InstatiateBigBoss();
        }
        else {

            GameManager.Instance.InstatiateSmallBoss();
        }


    }
    public void CompleteBoss() {
        isSpawnObaskle = true;
        isWaitingToEnbleObstackle = true;

    }

    #endregion;

   
    
    private void SetKeyBoss(int V) {
        if (V == 0) {

            GameManager.Instance.InstantiateLaserBoss();
        }
        else if (V == 1) {
            GameManager.Instance.InstatiateBigBoss();
        }
        else {

            GameManager.Instance.InstatiateSmallBoss();
        }
    }

    public bool IsSpawnObstackle {
        get { return isSpawnObaskle; }
    }

    public void RemoveLaser(LaserBeam Laser) {
       // laserhandler.RemoveThisLaser(Laser);
    }

}
