using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PathData : MonoBehaviour
{
    [SerializeField] private Obstackle[] all_Obestakle;
    [SerializeField] private Coin[] all_Coin;


    public void SetPathData( int NoOFCoin) {

        if (LevelManager.instance.IsSpawnObstackle) {
            SpawnObstackle();
        }
       
        SpawnCoin();
    }

    public void DisableMyObstackle() {
        for (int i = 0; i < all_Obestakle.Length; i++) {
            all_Obestakle[i].gameObject.SetActive(false);
        }
    }
    public void EnableMyObstackle() {
        SpawnObstackle();
    }

    private void SpawnCoin() {

        for (int i = 0; i < all_Coin.Length; i++) {

            all_Coin[i].gameObject.SetActive(true);
        }
    }

    private void SpawnObstackle() {

       

        for (int i = 0; i < all_Obestakle.Length; i++) {

            all_Obestakle[i].SetObstackleData();
        }
        
    }
}
