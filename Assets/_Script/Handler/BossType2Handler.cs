using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossType2Handler : MonoBehaviour {


    [Header("Component")]
    [SerializeField] private BossMotion[] all_Enemy;
    [Header("Data")]
    [SerializeField] private Vector3[] all_SpawnPostion;
    [SerializeField] private Vector3[] all_WorkingPosition;
    [SerializeField] private Vector3[] all_EndPostion;
    [SerializeField] private float flt_GoestoWorkingPostionTime;
    [SerializeField] private float flt_ActiveEnemeyTime;
    [SerializeField] private float flt_GoesToEndPostion;



   




    public void SetEnemy() {
       
        for (int i = 0; i < all_Enemy.Length; i++) {

            all_Enemy[i].gameObject.SetActive(true);
            all_Enemy[i].transform.position = all_SpawnPostion[i];
        }
        StartCoroutine(BossWorking());

    }

    private IEnumerator BossWorking() {

        float flt_CurrentTime = 0;

        while (flt_CurrentTime < 1) {

            for (int i = 0; i < all_Enemy.Length; i++) {
                all_Enemy[i].transform.position = Vector3.Lerp(all_SpawnPostion[i], all_WorkingPosition[i], flt_CurrentTime);
               
            }
            flt_CurrentTime += Time.deltaTime / flt_GoestoWorkingPostionTime;
            yield return null;
        }

        for (int i = 0; i < all_Enemy.Length; i++) {
            all_Enemy[i].transform.position = all_WorkingPosition[i];
            all_Enemy[i].setBulletSpawnActive(true);
        }

       
        yield return new WaitForSeconds(flt_ActiveEnemeyTime);


        for (int i = 0; i < all_Enemy.Length; i++) {

            all_Enemy[i].setBulletSpawnActive(false);

        }
        
        float flt_DisableTime = 0;
        while (flt_DisableTime < 1) {

            for (int i = 0; i < all_Enemy.Length; i++) {

                all_Enemy[i].transform.position = Vector3.Lerp(all_WorkingPosition[i], all_EndPostion[i], flt_DisableTime);
            }
            flt_DisableTime += Time.deltaTime / flt_GoesToEndPostion;
            yield return null;
        }

        for (int i = 0; i < all_Enemy.Length; i++) {

            all_Enemy[i].gameObject.SetActive(false);
        }
        
       LevelManager.instance.CompleteBoss();
    }
}
