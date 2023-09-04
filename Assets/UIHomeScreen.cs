using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHomeScreen : MonoBehaviour {

   public void OnClick_OnPlayBtnClick() {

        GameManager.Instance.StartGame();
    }

}
