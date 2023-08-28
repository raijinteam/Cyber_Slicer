using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using Random = UnityEngine.Random;

public class Type2Obstacle : Obstackle {

    [SerializeField] private Type2ObstackleState myState;
    [SerializeField] private float leftPostion;
    [SerializeField] private float rigthPostion;
    public float moveDuration = 2f;
    private float flt_MinDuration = 1;
    private float flt_MaxDuration = 2;

    private float screenWidth;


    public override void SetObstackleData() {

        moveDuration = Random.Range(flt_MinDuration, flt_MaxDuration);
        SetLeftAndRigthPostionAsPerScreen();
        SetRandomAnimation();
       

    }

    private void SetLeftAndRigthPostionAsPerScreen() {
        this.gameObject.SetActive(true);
        // Calculate the screen width based on the camera's orthographic size and aspect ratio
        float cameraHeight = Camera.main.orthographicSize * 2;
        screenWidth = cameraHeight * Camera.main.aspect;
        float halfWidth = GetComponent<SpriteRenderer>().bounds.extents.x;

        leftPostion = -screenWidth / 2 + halfWidth;
        rigthPostion = screenWidth / 2 - halfWidth;

    }

    private void SetRandomAnimation() {

        switch (myState) {

            case Type2ObstackleState.notMove:
                break;
            case Type2ObstackleState.centertoLeft:
                SetLeftToCenterMoveObstackle();
                break;
            case Type2ObstackleState.centerToRight:
                SetCenterToRightMoveObstackle();
                break;
            case Type2ObstackleState.leftToRigth:
                SetLeftToRightMoveObsatckle();
                break;
            default:
                break;
        }

    }

    private void SetLeftToRightMoveObsatckle() {
        Sequence seq = DOTween.Sequence();

        seq.Append(transform.DOLocalMoveX(leftPostion, moveDuration)).
            Append(transform.DOLocalMoveX(rigthPostion, moveDuration).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear));
    }

    private void SetCenterToRightMoveObstackle() {

        transform.DOLocalMoveX(rigthPostion, moveDuration).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
    }

    private void SetLeftToCenterMoveObstackle() {

        transform.DOLocalMoveX(leftPostion, moveDuration).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
    }

 
}

public enum Type2ObstackleState {

    notMove,centertoLeft,centerToRight,leftToRigth
}


