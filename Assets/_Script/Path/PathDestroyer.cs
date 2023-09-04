using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PathDestroyer : MonoBehaviour {

    [SerializeField] private BgHandler bg;
    [SerializeField] private PathHandler pathHandler;

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.TryGetComponent<PathData>(out PathData currentpath)) {
            pathHandler.pathDestroyed?.Invoke(currentpath);
            LevelManager.instance.SpawnRandomBoss();
        }
       
    }
}
