using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    [Header("Laser Component")]
    [SerializeField] private LineRenderer line;
    [SerializeField] private Transform start_Postion;
    [SerializeField] private Transform end_Postion;

    [Header("LaserData")]
    [SerializeField] private LayerMask player;
    private float flt_raycastDistance;
    private Vector3 raycast_Direction;


    private void Start() {

        SetLaserRayCastData();
        GameManager.Instance.GamePlayingState += LaserUpdate;
    }

   

    private void OnDisable() {
        GameManager.Instance.GamePlayingState -= LaserUpdate;
    }

    private void LaserUpdate() {

        SetLaserLineRenderorPostion();
        ActiveRaycast();
    }

    private void ActiveRaycast() {


        if (Physics.Raycast(start_Postion.position,raycast_Direction,out RaycastHit hit,flt_raycastDistance,player)) {

            GameManager.Instance.GameOver();
        }
    }

    private void SetLaserLineRenderorPostion() {
        line.SetPosition(0, start_Postion.position);
        line.SetPosition(1, end_Postion.position);
    }

    private void SetLaserRayCastData() {
        raycast_Direction = (end_Postion.position - start_Postion.position).normalized;
        flt_raycastDistance = MathF.Abs(Vector3.Distance(end_Postion.position, start_Postion.position));
        
    }
}
