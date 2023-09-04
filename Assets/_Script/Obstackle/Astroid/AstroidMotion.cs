using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroidMotion : MonoBehaviour {

    [Header("Component")]
    [SerializeField] private Transform child_Dircetion;
   


    [Header("Astroid  Data")]
    [SerializeField] private float angleMinRange;
    [SerializeField] private float angleMaxRange;
    [SerializeField] private float flt_AstroidSpeed;
    [SerializeField] private bool isAstroidInScreen;
   


    private void OnEnable() {

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

    private void OnTriggerEnter2D(Collider2D collision) {

        Debug.Log("EnterCollision");
        if (!isAstroidInScreen) {
          
            return;
        }

        ChangeDircetion(collision);

    }
    private void OnTriggerExit2D(Collider2D collision) {

        Debug.Log("ExitCollision");
        if (!isAstroidInScreen) {

            isAstroidInScreen = true;
        }
    }

    public void DestroyedAstroid() {
        Destroy(gameObject);
       Destroy(GameManager.Instance.currentAstroidHandler.gameObject);
    }

    private void ChangeDircetion(Collider2D other) {

        if (other.CompareTag(TagName.left_Boundry)) {
            child_Dircetion.right = Vector3.Reflect(child_Dircetion.right, Vector3.right);
        }
        else if (other.CompareTag(TagName.rigth_Boundry)) {
            child_Dircetion.right = Vector3.Reflect(child_Dircetion.right, Vector3.left);
        }

    }

   

   

   


}

