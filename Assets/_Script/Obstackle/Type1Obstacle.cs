using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Type1Obstacle : Obstackle
{
    [Header("Component")]
    [SerializeField] private BoxCollider2D My_Collider;
    [SerializeField] private Transform SpawnPostion;
    [SerializeField] private LineRenderer line;
    [SerializeField] private SpriteRenderer start_Sprite;
    [SerializeField] private SpriteRenderer end_Sprite;

    [Header("Obstackle Data")]
    [SerializeField] private bool isRotate;
    [SerializeField] private float flt_MinDistance;
    [SerializeField] private float flt_MaxDistance;
    private float flt_RoatationSpeed = 20;
    private float flt_Multipler = 2;
    private float flt_MinRotationSpeed = 75;
    private float flt_MaxRotationSpeed = 100;
    




  
  

    private void Start() {

        flt_RoatationSpeed = Random.Range(flt_MinRotationSpeed, flt_MaxRotationSpeed);
        GameManager.Instance.GamePlayingState += MyUpdate;
    }

    private void OnDisable() {
        GameManager.Instance.GamePlayingState -= MyUpdate;
    }


    public override void SetObstackleData() {

        this.gameObject.SetActive(true);
        float flt_Distance = Random.Range(flt_MinDistance, flt_MaxDistance);

        start_Sprite.transform.position = SpawnPostion.position - SpawnPostion.up * flt_Distance;
        end_Sprite.transform.position = SpawnPostion.position + SpawnPostion.up * flt_Distance;

        line.SetPosition(0, start_Sprite.transform.position);
        line.SetPosition(1, end_Sprite.transform.position);
        My_Collider.size = new Vector3(My_Collider.transform.localScale.x, 
                                        flt_Distance * flt_Multipler, My_Collider.transform.localScale.z);
      
    }

    private void MyUpdate() {
        if (isRotate) {
            RoatateObstackle();
        }
        SetLineRendroPostion();
        
    }

    private void RoatateObstackle() {
        transform.Rotate(Vector3.forward * flt_RoatationSpeed * Time.deltaTime);
    }


    private void SetLineRendroPostion() {
        line.SetPosition(0, start_Sprite.transform.position);
        line.SetPosition(1, end_Sprite.transform.position);
    }

    
}
