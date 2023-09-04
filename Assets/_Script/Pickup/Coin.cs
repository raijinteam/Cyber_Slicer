using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Pickup {

    public int Coinvalue;

    [Header("Magenet Data")]
    [SerializeField] private bool shouldAffectedInMagnet;
    [SerializeField] private float flt_MagenetTimeSpeed;

    private void OnEnable() {
        GameManager.Instance.GamePlayingState += MyUpdate;
    }

    private void OnDisable() {
        GameManager.Instance.GamePlayingState -= MyUpdate;
    }

    

    private void MyUpdate() {
        if (!shouldAffectedInMagnet) {
            return;
        }

        transform.position = Vector3.Lerp(transform.position, GameManager.Instance.MyPlayer.transform.position, 
                            flt_MagenetTimeSpeed * Time.deltaTime);
    }

    public override void CollectedPickUp() {
        base.CollectedPickUp();
        GameManager.Instance.CollectedCoin(Coinvalue);
    }

    public void EffectedInMagenet() {
        shouldAffectedInMagnet = true;
        transform.SetParent(null);
    }
}
