using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBG : MonoBehaviour {

    [Header("Component")]
    [SerializeField]private SpriteRenderer sr;

    [Header("Data")]
    [SerializeField] private GameObject left_Side;
    [SerializeField] private GameObject right_Side;
    [SerializeField] private GameObject rigth_Clamp;
    [SerializeField] private GameObject left_Clamp;
    [SerializeField] private float flt_MinLeftDistance;
    [SerializeField] private float flt_MaxLeftDistance;
    [SerializeField] private float flt_MinRightDistance;
    [SerializeField] private float flt_MaxRightDistance;


    

    private void Start() {


        //// world height is always camera's orthographicSize * 2
        //float worldScreenHeight = Camera.main.orthographicSize * 2;

        //// world width is calculated by diving world height with screen heigh
        //// then multiplying it with screen width
        //float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        //// to scale the game object we divide the world screen width with the
        //// size x of the sprite, and we divide the world screen height with the
        //// size y of the sprite
        //transform.localScale = new Vector3(
        //    worldScreenWidth / sr.sprite.bounds.size.x,
        //   worldScreenWidth / sr.sprite.bounds.size.x, worldScreenWidth / sr.sprite.bounds.size.x);
    }

    public void SetBg() {

        int index = Random.Range(0, 100);
        if (index < 50) {
            left_Side.gameObject.SetActive(false);
            right_Side.gameObject.SetActive(false);
            left_Clamp.gameObject.SetActive(false);
            rigth_Clamp.gameObject.SetActive(false);
        }
        else {
            left_Side.gameObject.SetActive(true);
            right_Side.gameObject.SetActive(true);

            int Index = Random.Range(0, 100);
            if (index < 50) {
                left_Clamp.gameObject.SetActive(true);
                float leftDistance = Random.Range(flt_MinLeftDistance, flt_MaxLeftDistance);
                left_Clamp.transform.localPosition = new Vector3(left_Clamp.transform.localPosition.x, leftDistance, left_Clamp.transform.localPosition.z);
            }

            index = Random.Range(0, 100);
            if (index < 50) {

                rigth_Clamp.gameObject.SetActive(true);
                float rigthDistance = Random.Range(flt_MaxRightDistance, flt_MinRightDistance);
                rigth_Clamp.transform.localPosition = new Vector3(rigth_Clamp.transform.localPosition.x, rigthDistance, rigth_Clamp.transform.localPosition.z);
            }
          
           

        }

    }
}
