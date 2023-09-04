using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable_Magnet : Pickup {

    public override void CollectedPickUp() {
        base.CollectedPickUp();
        //GameManager.Instance.CollectedCoin(Coinvalue);
       
    }
}
