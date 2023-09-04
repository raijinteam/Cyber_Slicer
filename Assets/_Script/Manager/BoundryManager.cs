using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundryManager : MonoBehaviour {

    public BoxCollider2D topCollider;
    public BoxCollider2D bottomCollider;
    public BoxCollider2D leftCollider;
    public BoxCollider2D rightCollider;

    void Start() {
        SetBoundaryColliders();
    }

    void SetBoundaryColliders() {
        float screenAspect = (float)Screen.width / Screen.height;
        float cameraOrthoSize = Camera.main.orthographicSize;
        float cameraWidth = cameraOrthoSize * 2 * screenAspect;
        float cameraHeight = cameraOrthoSize * 2;

        Vector2 cameraPosition = Camera.main.transform.position;

        // Position and size the colliders based on screen and camera parameters
        topCollider.transform.position = cameraPosition + Vector2.up * (cameraHeight / 2 + 0.5f) ;
        topCollider.size = new Vector2(cameraWidth, 1);

        bottomCollider.transform.position = cameraPosition + Vector2.down * (cameraHeight / 2 + 0.5f);
        bottomCollider.size = new Vector2(cameraWidth, 1);

        leftCollider.transform.position = cameraPosition + Vector2.left * (cameraWidth / 2 + 0.5f);
        leftCollider.size = new Vector2(1, cameraHeight);

        rightCollider.transform.position = cameraPosition + Vector2.right * (cameraWidth / 2 + 0.5f);
        rightCollider.size = new Vector2(1, cameraHeight);
    }




   
    //[SerializeField] private BoxCollider topCollider;
    //[SerializeField] private BoxCollider bottomCollider;
    //[SerializeField] private BoxCollider leftCollider;
    //[SerializeField] private BoxCollider rightCollider;

    //private void Start() {
    //    SetBoundaryColliders();
    //}

    //private void SetBoundaryColliders() {

    //    float screenAspect = (float)Screen.width / Screen.height;
    //    float cameraOrthoSize = Camera.main.orthographicSize;
    //    float cameraWidth = cameraOrthoSize * 2 * screenAspect;
    //    float cameraHeight = cameraOrthoSize * 2;

    //    Vector3 cameraPosition = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y,0);

    //    // Position and size the colliders based on screen and camera parameters
    //    topCollider.transform.position = cameraPosition + Vector3.up * (cameraHeight / 2 + 0.5f);
    //    topCollider.size = new Vector3(cameraWidth, 1, cameraWidth);

    //    bottomCollider.transform.position = cameraPosition + Vector3.down * (cameraHeight / 2 + 0.5f);
    //    bottomCollider.size = new Vector3(cameraWidth, 1, cameraWidth);

    //    leftCollider.transform.position = cameraPosition + Vector3.left * (cameraWidth / 2 +0.5f);
    //    leftCollider.size = new Vector3(1, cameraHeight, cameraWidth);

    //    rightCollider.transform.position = cameraPosition + Vector3.right * (cameraWidth / 2 + 0.5f);
    //    rightCollider.size = new Vector3(1, cameraHeight, cameraWidth);
    //}
}
