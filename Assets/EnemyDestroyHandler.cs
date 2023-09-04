using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDestroyHandler : MonoBehaviour {

    [SerializeField] private Transform parent;
   
    [SerializeField] private int CurrentCount;
    [SerializeField] private int MaxCount;
    
    public void EnemyDestoyed() {
        CurrentCount++;
       
        if (CurrentCount >= MaxCount) {

            Destroy(parent.gameObject);

        }
    }
}
