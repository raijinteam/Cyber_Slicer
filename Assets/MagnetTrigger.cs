using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.CompareTag(TagName.Coin)) {

            collision.GetComponent<Coin>().EffectedInMagenet();
        }
    }
}
