using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;

    [Header("UiScreen")]
    public GameObject HomeScreen;
    public GameObject gameOverScreen;
    public GameObject GamePlay;
    public GameObject RewiveScreen;

    private void Awake() {

        instance = this;
    }
}
