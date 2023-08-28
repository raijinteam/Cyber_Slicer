using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroidMotion : MonoBehaviour {


   

    [Header("Astroid  Data")]
    [SerializeField] private float angleMinRange;
    [SerializeField] private float angleMaxRange;
    [SerializeField] private float flt_AstroidSpeed;
    [SerializeField] private bool isAstroidInScreen;
    [SerializeField] private Transform child_Dircetion;


    private void Start() {

        Destroy(gameObject, 10);
        GameManager.Instance.GamePlayingState += MyUpdate;
    }
    private void OnDisable() {
        GameManager.Instance.GamePlayingState -= MyUpdate;
    }

    public void SetDiretionOfChild(bool isleftSize,float flt_AstroidSpeed) {

        if (isleftSize) {
            child_Dircetion.localEulerAngles = new Vector3(0, 0, -(UnityEngine.Random.Range(angleMinRange, angleMaxRange)));
        }
        else {
            child_Dircetion.localEulerAngles = new Vector3(0, 0, UnityEngine.Random.Range(180 + angleMinRange, 180 +angleMaxRange));
        }
        this.flt_AstroidSpeed = flt_AstroidSpeed;
        isAstroidInScreen = false;
    }

   

    private void MyUpdate() {

        AstroidMovent();
    }

    private void AstroidMovent() {

        transform.Translate(child_Dircetion.right * flt_AstroidSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other) {

        Debug.Log("EnterCollision");
        if (!isAstroidInScreen) {
            return;
        }

        ChangeDircetion(other);



    }

    private void ChangeDircetion(Collider other) {

      

        if (other.TryGetComponent<LeftBoundry>(out LeftBoundry left)) {

            child_Dircetion.right = Vector3.Reflect(child_Dircetion.right, Vector3.right);
        }
        else if (other.TryGetComponent<RightBoundry>(out RightBoundry rigth)) {

            child_Dircetion.right = Vector3.Reflect(child_Dircetion.right, Vector3.left);
        }
       
    }

    private void OnTriggerExit(Collider other) {

        Debug.Log("ExitCollision");
        if (!isAstroidInScreen) {

            isAstroidInScreen = true;
        }
    }

   


}

