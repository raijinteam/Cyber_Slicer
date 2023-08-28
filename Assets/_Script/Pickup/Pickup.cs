using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickup : MonoBehaviour {

    [Header("Vfx")]
    [SerializeField] private ParticleSystem ps_Destroyed;
    [SerializeField] private Collider my_Collider;
    [SerializeField] private GameObject body;



    public virtual void CollectedPickUp() {

        my_Collider.enabled = false;
        body.gameObject.SetActive(false);
        transform.SetParent(null);
        ps_Destroyed.gameObject.SetActive(true);
        ps_Destroyed.Play();
        StartCoroutine(WaitForDestroyed());
    }

    private IEnumerator WaitForDestroyed() {

        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
    
 
