using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Pickup {

    public int Coinvalue;

    public override void CollectedPickUp() {
        base.CollectedPickUp();
        GameManager.Instance.CollectedCoin(Coinvalue);
    }
}
