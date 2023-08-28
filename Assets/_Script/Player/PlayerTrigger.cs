using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {

       
        if (other.TryGetComponent<Obstackle>(out Obstackle obstackle)) {

          
            GameManager.Instance.GameOver();
        }
        else if (other.TryGetComponent<Coin>(out Coin coin)) {

            coin.CollectedPickUp();
        }
        else if (other.TryGetComponent<RocketMotion>(out RocketMotion rocketMotion)) {

            GameManager.Instance.GameOver();
        }
        else if (other.TryGetComponent<BossBulletMotion>(out BossBulletMotion bulletMotion)) {
            bulletMotion.DestroyBullet();
            StartCoroutine(Delay_GameOver());
        }
        else if (other.TryGetComponent<AstroidMotion>(out AstroidMotion astroid)) {
            GameManager.Instance.GameOver();
        }





    }

    private IEnumerator Delay_GameOver() {

        yield return new WaitForSeconds(1);
        GameManager.Instance.GameOver();
    }
}
