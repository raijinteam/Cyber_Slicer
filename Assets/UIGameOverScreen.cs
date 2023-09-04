using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGameOverScreen : MonoBehaviour {

    public void OnClick_OnRestartBtnClick() {
        GameManager.Instance.RestartGame();
    }
}
