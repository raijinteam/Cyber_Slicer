using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackgroundEffect : MonoBehaviour
{

    public SpriteRenderer background;  // Reference to the background GameObject
    public float parallaxSpeed = 0.5f;  // Speed of the parallax movement
    public float postion = 0;
    private Vector3 prevCameraPosition;
    private bool isMove;


    private void Start() {
        prevCameraPosition = transform.position;
        postion = 0;
        isMove = false;
    }

    public void StartMove() {
        isMove = true;
    }

    private void Update() {
        if (!isMove) {
            return;
        }
        postion = Time.time * parallaxSpeed;
        background.material.mainTextureOffset = new Vector2(0, postion);
    }


}
