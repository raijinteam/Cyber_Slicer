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
       
      // SpawnCoin(NoOFCoin);
    }

    public void DisableMyObstackle() {
        for (int i = 0; i < all_Obestakle.Length; i++) {
            all_Obestakle[i].gameObject.SetActive(false);
        }
    }
    public void EnableMyObstackle() {
        SpawnObstackle();
    }

    private void SpawnCoin(int noOFCoin) {
        List<Coin> list_CurrentCoin = new List<Coin>();

        for (int i = 0; i < all_Coin.Length; i++) {

            list_CurrentCoin.Add(all_Coin[i]);
        }
        for (int i = 0; i < noOFCoin; i++) {

            int Index = Random.Range(0, list_CurrentCoin.Count);

            list_CurrentCoin[Index].gameObject.SetActive(true);
            list_CurrentCoin.RemoveAt(Index);

        }
    }

    private void SpawnObstackle() {

       

        for (int i = 0; i < all_Obestakle.Length; i++) {

            all_Obestakle[i].SetObstackleData();
        }
        
    }
}
