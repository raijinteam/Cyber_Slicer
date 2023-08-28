using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgHandler : MonoBehaviour {

    [Header("Bg Component")]
    [SerializeField] private GameObject[] all_BG1;
    [SerializeField] private GameObject[] all_Bg2;
    [SerializeField] private GameObject[] all_Bg3;
    [SerializeField] private GameObject[] all_Bg4;

   

    [Header("BG Data")]
    [SerializeField] private int Bg1Iddex;
    [SerializeField] private int Bg2Index;
    [SerializeField] private int BG3Index;
    [SerializeField] private int Bg4Index;
    [SerializeField]private float flt_PathMotionSpeed;
    private float flt_distanceBeteenTwoPath = 23.44f;
    private List<GameObject> list_BG = new List<GameObject>();

    public delegate void BGDisable(GameObject bg);
    public BGDisable DiableBG;


    private void Start() {

        LevelManager.instance.LevelUpdate += IncreaserdMyLevel;
        DiableBG += DestroyedBG;
        SpawnStartBg();
    }
    private void LateUpdate() {
        for (int i = 0; i < list_BG.Count; i++) {

            list_BG[i].transform.Translate(Vector3.left * flt_PathMotionSpeed * Time.deltaTime);
        }
    }


    private void OnDisable() {

        DiableBG -= DestroyedBG;
        LevelManager.instance.LevelUpdate -= IncreaserdMyLevel;
    }


    #region EventHandler

    private void IncreaserdMyLevel(float ammount) {
        flt_PathMotionSpeed += ammount;
    }
    #endregion


    #region Destroy Handler
    private void DestroyedBG(GameObject bg) {

        list_BG.Remove(bg);
        bg.gameObject.SetActive(false);
        Vector3 postion = GetGretestPostion();
        postion += new Vector3(0, flt_distanceBeteenTwoPath, 0);
        SpawnRandomBG(postion);

    }

    private Vector3 GetGretestPostion() {
        float maxPostion = list_BG[2].transform.position.y;

        return new Vector3(0, maxPostion, 0);
    }

    #endregion


    #region Bg_Spawn Handler

    private void SpawnStartBg() {

        Vector3 postion = Vector3.zero;
        for (int i = 0; i < 4; i++) {

            SpawnRandomBG(postion);
            postion += new Vector3(0, flt_distanceBeteenTwoPath, 0);

        }
    }
    private void SpawnRandomBG(Vector3 spawnPostion) {
        int index = Random.Range(0, 4);
        switch (index) {

            case 0:
                EnableBG1(spawnPostion);
                break;
            case 1:
                EnableBG2(spawnPostion);
                break;
            case 2:
                EnableBG3(spawnPostion);
                break;
            case 3:
                EnableBG4(spawnPostion);
                break;

        }
    }

    private void EnableBG4(Vector3 spawnPostion) {
        all_Bg4[Bg4Index].gameObject.SetActive(true);
        all_Bg4[Bg4Index].transform.position = spawnPostion;
        list_BG.Add(all_Bg4[Bg4Index]);
        Bg4Index++;
        if (Bg4Index > all_Bg4.Length - 1) {
            Bg4Index = 0;
        }

    }

    private void EnableBG3(Vector3 spawnPostion) {

        all_Bg3[BG3Index].gameObject.SetActive(true);
        all_Bg3[BG3Index].transform.position = spawnPostion;
        list_BG.Add(all_Bg3[BG3Index]);
        BG3Index++;
        if (BG3Index > all_Bg3.Length - 1) {
            BG3Index = 0;
        }


    }

    private void EnableBG2(Vector3 spawnPostion) {

        all_Bg2[Bg2Index].gameObject.SetActive(true);
        all_Bg2[Bg2Index].transform.position = spawnPostion;
        list_BG.Add(all_Bg2[Bg2Index]);
        Bg2Index++;
        if (Bg2Index > all_Bg2.Length - 1) {
            Bg2Index = 0;
        }
    }

    private void EnableBG1(Vector3 spawnPostion) {

        all_BG1[Bg1Iddex].gameObject.SetActive(true);
        all_BG1[Bg1Iddex].transform.position = spawnPostion;
        list_BG.Add(all_BG1[Bg1Iddex]);
        Bg1Iddex++;
        if (Bg1Iddex > all_BG1.Length - 1) {
            Bg1Iddex = 0;
        }
    }
    #endregion
}
