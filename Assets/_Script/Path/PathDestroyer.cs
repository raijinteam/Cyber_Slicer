using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PathDestroyer : MonoBehaviour {

    [SerializeField] private BgHandler bg;
    [SerializeField] private PathHandler pathHandler;
    private void OnTriggerEnter(Collider other) {

        if (other.TryGetComponent<PathData>(out PathData currentpath)) {
            pathHandler.pathDestroyed?.Invoke(currentpath);
            LevelManager.instance.SpawnRandomBoss();
        }
        if (other.TryGetComponent<SpriteRenderer>(out SpriteRenderer sprite)) {
            bg.DiableBG?.Invoke(sprite.gameObject);
        }

    }
}
