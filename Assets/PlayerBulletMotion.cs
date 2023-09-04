using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletMotion : MonoBehaviour {

    [Header("Bullet Compononet")]
    [SerializeField] private Transform body;
    [SerializeField] private ParticleSystem ps_Expoltion;
    [SerializeField] private Collider2D myCollider;

    [Header("Bullet data")]
    [SerializeField] private float flt_BulletSpeed;
    [SerializeField] private bool isMove;


    // Tags
    private string tag_Laser = "Laser";

    private void OnEnable() {
        GameManager.Instance.GamePlayingState += MyUpdate;
    }
    private void Start() {
        Destroy(this.gameObject, 5);
        isMove = true;
    }

    private void OnDisable() {
        GameManager.Instance.GamePlayingState -= MyUpdate;
    }
    private void MyUpdate() {
        if (!isMove ) {
            return;
        }
        transform.Translate(Vector3.up * flt_BulletSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag.Equals(tag_Laser)) {

            Debug.Log("Laser Trigger");
            collision.GetComponent<LaserBeam>().LaserDestroyedByBullet();
            StartCoroutine(Dealy_Destroyed());
        }
        else if (collision.gameObject.CompareTag(TagName.obstackle)) {
            collision.GetComponent<EnemyDestroyHandler>().EnemyDestoyed();
            StartCoroutine(Dealy_Destroyed());
        }
        else if (collision.gameObject.CompareTag(TagName.MultyBoss)) {
            collision.GetComponent<BossMotion>().DetsroyBoss2();
            StartCoroutine(Dealy_Destroyed());
        }
        else if (collision.gameObject.CompareTag(TagName.Astroid)) {

           collision.GetComponent<AstroidMotion>().DestroyedAstroid();
            StartCoroutine(Dealy_Destroyed());
        }
       
    }
    private IEnumerator Dealy_Destroyed() {
        isMove = false;
        body.gameObject.SetActive(false);
        myCollider.enabled = false;
        ps_Expoltion.gameObject.SetActive(true);
        ps_Expoltion.Play();
       
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
