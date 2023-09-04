using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCantroller : GameStatePlaying
{
    [Header(" Powerup - Components")]
   
    [SerializeField] private PowerUpMagenet powerUpMagenet;
    [SerializeField] private PowerUpShield PowerUpShield;
    [SerializeField] private PowerUpShooting powerupShooting;
    [SerializeField] private PowerUpSpeedBoost PowerupSpeedBoost;
    [SerializeField] private PowerUp2X powerUp2X;
    [SerializeField] private PowerUpChronos powerupChronos;



    
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

    public void ActiveMagenetPowerUp() {
        powerUpMagenet.ActivateMagnet();
    }

    public void ActiveShootingPowerUP() {
        powerupShooting.ActivateShooting();
    }

    public void ActiveShieldPowerUp() {
        PowerUpShield.ActiveShield();
    }

    public void ActiveSpeedBoostPowerUp() {
        PowerupSpeedBoost.ActiveSpeedBoost();
    }

    public void Active2XPowerUp() {
        powerUp2X.Active2X();
    }

    public void ActiveChronosPowerUp() {
        powerupChronos.ActiveChronos();
    }



    private void OnTriggerEnter2D(Collider2D collision) {

       
        if (collision.gameObject.CompareTag(TagName.Astroid)) {
            GameManager.Instance.GameOver();
        }
        else if (collision.gameObject.CompareTag(TagName.obstackle)) {
            GameManager.Instance.GameOver();
        }
        else if (collision.gameObject.CompareTag(TagName.Astroid)) {
            GameManager.Instance.GameOver();
        }
        else if (collision.gameObject.CompareTag(TagName.BossBullet)) {
            collision.GetComponent<BossBulletMotion>().DestroyBullet();
            StartCoroutine(Delay_GameOver());
        }
        else if (collision.TryGetComponent<Pickup>(out Pickup pickup)) {
            pickup.CollectedPickUp();
        }
    }

    private IEnumerator Delay_GameOver() {

        yield return new WaitForSeconds(1);
        GameManager.Instance.GameOver();
    }
}
