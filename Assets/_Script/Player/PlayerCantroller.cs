using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCantroller : GameStatePlaying
{
    
    [Header("Player Data")]
    [SerializeField] private float flt_MevementSpeed;
    [SerializeField] private float flt_Boundry;
    private float flt_HorizontalInput;



    private void Start() {
        GameManager.Instance.GamePlayingState += CantrollerUpdate;

    }

    private void OnDisable() {
       
       GameManager.Instance.GamePlayingState -= CantrollerUpdate;
    }

    private void CantrollerUpdate() {
        
        PlayerMotion();
        PlayerInput();
    }

  

    private void PlayerMotion() {

        transform.position += Vector3.right * flt_HorizontalInput * flt_MevementSpeed * Time.deltaTime;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -flt_Boundry, flt_Boundry), transform.position.y, transform.position.z);
    }

    private void PlayerInput() {

       flt_HorizontalInput = Input.GetAxis("Horizontal");
    }
}
