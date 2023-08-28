using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Type3Obstacle : Obstackle {


    [Header("Component")]
    [SerializeField] private Transform SpawnPostion;
    [SerializeField] private LineRenderer line;
    [SerializeField] private SpriteRenderer start_Sprite;
    [SerializeField] private SpriteRenderer end_Sprite;


    [Header("Obstackle Data")]
    [SerializeField] private bool isLeftPostion;
    [SerializeField] private float flt_CurrentTimeForChagePostion;
    [SerializeField] private float flt_MaxTimeForChangeCalculation;
    [SerializeField] private float leftPostion;
    [SerializeField] private float rigthPostion;
    [SerializeField] private float flt_offsetFromEndPostion;
    private float screenWidth;
    [SerializeField] private bool isWatingNewPostion;




    [SerializeField] private LayerMask MyLayer;
    private Vector3 raycastDirection;
    private float raycastDistnce;


    private void Start() {

       
        GameManager.Instance.GamePlayingState += MyUpdate;
    }

   

    private void OnDisable() {
        GameManager.Instance.GamePlayingState -= MyUpdate;
    }


    public override void SetObstackleData() {

        this.gameObject.SetActive(true);
        SetPostionAsPerScreen();

        start_Sprite.transform.position = SpawnPostion.position;
        int index = Random.Range(0, 100);
        if (index < 50) {
            end_Sprite.transform.position = new Vector3(leftPostion, end_Sprite.transform.position.y, end_Sprite.transform.position.z);
            isLeftPostion = true;
        }
        else {
            end_Sprite.transform.position = new Vector3(rigthPostion, end_Sprite.transform.position.y, end_Sprite.transform.position.z);
            isLeftPostion = false;
        }

        
        line.SetPosition(0, start_Sprite.transform.position);
        line.SetPosition(1, end_Sprite.transform.position);

       
    }

    private void MyUpdate() {

        ObstackleEndMovementSetUp();
        SetLineRendroPostion();
        ActiveRayCastDetactPlayer();
    }

    private void ObstackleEndMovementSetUp() {
        if (isWatingNewPostion) {
            return;
        }
        flt_CurrentTimeForChagePostion += Time.deltaTime;
        if (flt_CurrentTimeForChagePostion > flt_MaxTimeForChangeCalculation) {
            flt_CurrentTimeForChagePostion = 0;
            ChangeEndPostion();
        }
    }

    private void ChangeEndPostion() {

        isWatingNewPostion = true;
        StartCoroutine(Delay_CahngePostion());
    }

    private IEnumerator Delay_CahngePostion() {

        //line.gameObject.SetActive(false);
        end_Sprite.gameObject.SetActive(false);
       
        Vector3 targetPostion = Vector3.zero;

        if (isLeftPostion) {

            targetPostion = new Vector3(rigthPostion, end_Sprite.transform.position.y, end_Sprite.transform.position.z);
            isLeftPostion = false;
        }
        else {
            targetPostion = new Vector3(leftPostion, end_Sprite.transform.position.y, end_Sprite.transform.position.z);
            isLeftPostion = true;
        }
        Vector3 StartPostion = new Vector3(start_Sprite.transform.position.x, end_Sprite.transform.position.y, end_Sprite.transform.position.z);
        end_Sprite.transform.position = StartPostion;
        float flt_CurrentTime = 0;
        while (flt_CurrentTime <1) {
            Debug.Log(targetPostion + "TargetPostion");
             StartPostion = new Vector3(start_Sprite.transform.position.x, end_Sprite.transform.position.y, end_Sprite.transform.position.z);
            targetPostion = new Vector3(targetPostion.x, start_Sprite.transform.position.y, targetPostion.z);
            end_Sprite.transform.position = Vector3.Lerp(StartPostion, targetPostion, flt_CurrentTime);
            line.SetPosition(0, start_Sprite.transform.position);
            line.SetPosition(1, end_Sprite.transform.position);
            flt_CurrentTime += Time.deltaTime/2;
            yield return null;
        }
      
        isWatingNewPostion = false;
        end_Sprite.gameObject.SetActive(true);
        line.gameObject.SetActive(true);
    }

    private void SetPostionAsPerScreen() {
        float cameraHeight = Camera.main.orthographicSize * 2;
        screenWidth = cameraHeight * Camera.main.aspect;
        leftPostion = -screenWidth / 2 + flt_offsetFromEndPostion;
        rigthPostion = screenWidth / 2 - flt_offsetFromEndPostion;
    }

    private void ActiveRayCastDetactPlayer() {

        RaycastHit hit;

        raycastDirection = (end_Sprite.transform.position - start_Sprite.transform.position).normalized;
        raycastDistnce = Mathf.Abs(Vector3.Distance(end_Sprite.transform.position, start_Sprite.transform.position));

        if (Physics.Raycast(start_Sprite.transform.position, raycastDirection, out hit, raycastDistnce, MyLayer)) {

            Debug.Log("hit" + hit.transform.name);
            GameManager.Instance.GameOver();
        }
    }

    private void SetLineRendroPostion() {
       
        line.SetPosition(0, start_Sprite.transform.position);
        line.SetPosition(1, end_Sprite.transform.position);
    }

    private void OnDrawGizmos() {
       
        Debug.DrawRay(start_Sprite.transform.position, raycastDirection * raycastDistnce, Color.red);
    }
}
