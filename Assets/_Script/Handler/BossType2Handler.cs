using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossType2Handler : MonoBehaviour {


    [Header("Component")]
    [SerializeField] private List<BossMotion> all_Enemy;
    [Header("Data")]
    [SerializeField] private List<Vector3> all_SpawnPostion;
    [SerializeField] private List<Vector3> all_WorkingPosition;
    [SerializeField] private List<Vector3> all_EndPostion;
    [SerializeField] private float flt_GoestoWorkingPostionTime;
    [SerializeField] private float flt_ActiveEnemeyTime;
    [SerializeField] private float flt_GoesToEndPostion;



   




    public void SetType2Boss() {
       
       
        StartCoroutine(BossWorking());

    }

    private IEnumerator BossWorking() {

        float flt_CurrentTime = 0;

        while (flt_CurrentTime < 1) {

            for (int i = 0; i < all_Enemy.Count; i++) {
                all_Enemy[i].transform.position = Vector3.Lerp(all_SpawnPostion[i], all_WorkingPosition[i], flt_CurrentTime);
               
            }
            flt_CurrentTime += Time.deltaTime / flt_GoestoWorkingPostionTime;
            yield return null;
        }

        for (int i = 0; i < all_Enemy.Count; i++) {
            all_Enemy[i].transform.position = all_WorkingPosition[i];
            all_Enemy[i].setBulletSpawnActive(true);
        }

       
        yield return new WaitForSeconds(flt_ActiveEnemeyTime);


        for (int i = 0; i < all_Enemy.Count; i++) {

            all_Enemy[i].setBulletSpawnActive(false);

        }
        yield return new WaitForSeconds(0.5f);
        float flt_DisableTime = 0;
        while (flt_DisableTime < 1) {

            for (int i = 0; i < all_Enemy.Count; i++) {

                all_Enemy[i].transform.position = Vector3.Lerp(all_WorkingPosition[i], all_EndPostion[i], flt_DisableTime);
            }
            flt_DisableTime += Time.deltaTime / flt_GoesToEndPostion;
            yield return null;
        }

        for (int i = 0; i < all_Enemy.Count; i++) {

            all_Enemy[i].gameObject.SetActive(false);
        }

        Destroy(gameObject);
       LevelManager.instance.CompleteBoss();
    }

    public void DestroyType2Boss(BossMotion boss) {

        if (all_Enemy.Contains(boss)) {

            int index = all_Enemy.IndexOf(boss);
            all_Enemy.RemoveAt(index);
            all_EndPostion.RemoveAt(index);
            all_WorkingPosition.RemoveAt(index);
            all_SpawnPostion.RemoveAt(index);
            Destroy(boss.gameObject);
        }
        if (all_Enemy.Count == 0) {
            Destroy(this.gameObject);
            LevelManager.instance.CompleteBoss();
        }
    }
}
