using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgHandler : MonoBehaviour {

    [Header("Bg Component")]
    [SerializeField] private PathHandler path;
    [SerializeField]private List<SetBG> list_BG = new List<SetBG>();
  



    [Header("BG Data")]
    [SerializeField] private bool isActiveStartBG;
    [SerializeField] private float flt_PathMotionSpeed;
    [SerializeField]private float flt_distanceBeteenTwoPath = 26.72f;
   
    public delegate void BGDisable(GameObject bg);
    public BGDisable DiableBG;


    private void Start() {
        isActiveStartBG = true;
        flt_PathMotionSpeed = path.flt_PathMotionSpeed;
        GameManager.Instance.MyPlayer.transform.SetParent(list_BG[0].transform);
        LevelManager.instance.LevelUpdate += IncreaserdMyLevel;
       
       
    }

    private void OnDisable() {

        LevelManager.instance.LevelUpdate -= IncreaserdMyLevel;
    }
    private void LateUpdate() {
        if (!GameManager.Instance.IsplayerLive) {
            return;
        }
        if (isActiveStartBG) {
            if (list_BG[0].transform.position.y < -7) {
                GameManager.Instance.MyPlayer.transform.SetParent(null);
            }
        }
        for (int i = 0; i < list_BG.Count; i++) {

            list_BG[i].transform.Translate(Vector3.down * flt_PathMotionSpeed * Time.deltaTime);
            if (list_BG[i].transform.position.y < -26.72f) {
                int BackIndex = i - 1;
                if (BackIndex < 0) {
                    BackIndex = list_BG.Count - 1;  
                }
                Debug.Log("BackINdex" + BackIndex);
                list_BG[i].transform.position = new Vector3(list_BG[i].transform.position.x,
                                list_BG[BackIndex].transform.position.y + flt_distanceBeteenTwoPath,
                                list_BG[i].transform.position.z);
               
                if (!isActiveStartBG) {
                    list_BG[i].SetBg();
                    StartCoroutine(Delay_SeBG(i, BackIndex));
                }
                else {
                    Destroy(list_BG[i]);
                    list_BG.RemoveAt(i);
                    isActiveStartBG = false;
                   
                }
               
                
            }
        }
    }

    private IEnumerator Delay_SeBG(int i, int backIndex) {
        yield return new WaitForSeconds(0.1f);
        list_BG[i].transform.position = new Vector3(list_BG[i].transform.position.x,
                               list_BG[backIndex].transform.position.y + flt_distanceBeteenTwoPath,
                               list_BG[i].transform.position.z);
    }

   


    #region EventHandler

    private void IncreaserdMyLevel(float ammount) {
        flt_PathMotionSpeed += ammount;
    }
    #endregion


  


   

   

}
