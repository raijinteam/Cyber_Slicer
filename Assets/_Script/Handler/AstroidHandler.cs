using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AstroidHandler : MonoBehaviour {

    [Header("Component")]
    [SerializeField] private Transform left_Indicater;
    [SerializeField] private Transform rigth_Indicater;
    [SerializeField] private Transform left_Postion;
    [SerializeField] private Transform rigth_Postion;
    [SerializeField] private AstroidMotion astroid;

    [Header("Astroid Data")]
    [SerializeField] private float flt_AstroidSpeed;
    [SerializeField] private float flt_IndicaterTime;


    private void Start() {
        LevelManager.instance.LevelUpdate += ChangeSpeed;
    }
    private void OnDisable() {
        LevelManager.instance.LevelUpdate -= ChangeSpeed;
    }

    private void ChangeSpeed(float ammount) {
        flt_AstroidSpeed += ammount;
    }




    public void SetAstroidData() {

        int Index = Random.Range(0, 100);
        if (Index < 50) {
            SpawnAstroid(left_Postion , true);
        }
        else {
            SpawnAstroid(rigth_Postion , false);
        }
    }

    private void SpawnAstroid(Transform postion , bool isleft) {

        StartCoroutine(Delay_OfSpawn(postion,  isleft));
            
        

    }

    private IEnumerator Delay_OfSpawn(Transform postion, bool isleft) {

        if (isleft) {
            left_Indicater.gameObject.SetActive(true);
        }
        else {
            rigth_Indicater.gameObject.SetActive(true);
        }

        yield return new WaitForSeconds(flt_IndicaterTime);
        AstroidMotion current_Astroid = Instantiate(astroid, postion.position, Quaternion.identity);
        current_Astroid.SetDiretionOfChild(isleft, flt_AstroidSpeed);

        if (isleft) {
            left_Indicater.gameObject.SetActive(false);
        }
        else {
            rigth_Indicater.gameObject.SetActive(false);
        }
    }
}

