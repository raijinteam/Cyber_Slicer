using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletMotion : MonoBehaviour
{
    [Header("Bullet Component")]
    [SerializeField] private GameObject body;
    [SerializeField] private ParticleSystem ps_Explotion;
    [SerializeField] private Collider2D myCollider;

    [Header("BulletData")]
    [SerializeField] private bool isMove;
    [SerializeField]private float flt_BulletSpeed = 10;

    private void Start() {
        GameManager.Instance.GamePlayingState += MyUpdate;
        isMove = true;
        StartCoroutine(DelayOfSetroy(5));
    }

   

    private void OnDisable() {
        GameManager.Instance.GamePlayingState += MyUpdate;
    }


    private void MyUpdate() {
        if (!isMove) {
            return;
        }
        transform.Translate(Vector3.down * flt_BulletSpeed * Time.deltaTime);
    }

    public void DestroyBullet() {

        isMove = false;
        body.gameObject.SetActive(false);
        myCollider.enabled = false;
        ps_Explotion.gameObject.SetActive(true);
        ps_Explotion.Play();
        Destroy(this.gameObject, 1);
    }

    private IEnumerator DelayOfSetroy(float time) {
        yield return new WaitForSeconds(1);
        isMove = false;
        Destroy(gameObject);
    }


}
