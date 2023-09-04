using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathHandler : GameStatePlaying
{
    [Header("Path Data")]
    
    [SerializeField] private int NoOfCoin;
   
    [SerializeField] private float flt_distanceBeteenTwoPath;
    [SerializeField] private List<PathData> list_CurrentPath;
    public float flt_PathMotionSpeed;

    [SerializeField] private float flt_gameSpeed;



    [Header("Component")]
    [SerializeField] private PathData[] all_Path;
  

    public delegate void PathDestroyed(PathData path);
    public PathDestroyed pathDestroyed;



    




    private void Start() {
        GameManager.Instance.GamePlayingState += MyUpdate;
        SpawnFirstTimePath();
        pathDestroyed += DestroyedPath;
        LevelManager.instance.LevelUpdate += UpdateMyLevel;
        LevelManager.instance.BossActivationStatus += SetBossTimeStatus;
        GameManager.Instance.ChangeGameSpeed += SpeedUpdate;
    }

    private void SpeedUpdate(float GameSpeed) {
        flt_gameSpeed = GameSpeed;
    }

    private void OnDisable() {

        pathDestroyed += DestroyedPath;
        GameManager.Instance.GamePlayingState -= MyUpdate;
        LevelManager.instance.LevelUpdate -= UpdateMyLevel;
    }

    private void SetBossTimeStatus(bool isBossActive) {

        if (isBossActive) {
            for (int i = 0; i < list_CurrentPath.Count; i++) {

                // safe Size For Scrren It cannot Show in Game 
                if (i < 1) {
                    continue;
                }
                else {
                    list_CurrentPath[i].DisableMyObstackle();
                }
            }
        }
        else {
            for (int i = 0; i < list_CurrentPath.Count; i++) {

                // safe Size For Scrren It cannot Show in Game 
                if (i < 2) {
                    continue;
                }
                else {
                    list_CurrentPath[i].EnableMyObstackle();
                }
            }
        }

    }
    private void MyUpdate() {
        PathMotion();
    }
    private void UpdateMyLevel(float ammount) {
        flt_PathMotionSpeed += ammount;
    }

    private void PathMotion() {
        for (int i = 0; i < list_CurrentPath.Count; i++) {

            list_CurrentPath[i].transform.Translate(Vector3.down * flt_PathMotionSpeed *flt_gameSpeed* Time.deltaTime);
           
        }
    }

    private void SpawnFirstTimePath() {
        Vector3 postion = Vector3.zero;
        for (int i = 0; i < 4; i++) {

            int index = Random.Range(0, all_Path.Length);

            PathData currentPath = Instantiate(all_Path[index], postion, transform.rotation, transform);
           
           
            list_CurrentPath.Add(currentPath);
            postion += new Vector3(0, flt_distanceBeteenTwoPath, 0);

            if (i < 3) {
                continue;
            }
            else {
                currentPath.SetPathData(NoOfCoin);
            }

        }
    }

    
    private void DestroyedPath(PathData path) {
        int bgINdex = list_CurrentPath.IndexOf(path);
        list_CurrentPath.Remove(path);
        Destroy(path.gameObject);
        Vector3 postion = GetGretestPostion();
        postion += new Vector3(0, flt_distanceBeteenTwoPath, 0);
       
        
        path.transform.position = postion;
        int Index = Random.Range(0, all_Path.Length);
       PathData CurrentPath = Instantiate(all_Path[Index], postion, transform.rotation, transform);
       
        CurrentPath.SetPathData( NoOfCoin);
       
        list_CurrentPath.Add(CurrentPath);
    }

    private Vector3 GetGretestPostion() {
        float maxPostion = list_CurrentPath[2].transform.position.y;
       
        return new Vector3(0, maxPostion, 0);
    }
   
}
