using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager Instance; // Singleton instance

    [Header("GameData")]
    private GameState currentState;
    public int Coin;

    [Header("Player Data")]
    [SerializeField] private GameObject player;
    public GameObject MyPlayer { get { return player; } }
 

    [Header("UiScreen")]
    public GameObject HomeScreen;
    public GameObject gameOverScreen;
    public GameObject victoryScreen;
    public GameObject GamePlay;

   
    public delegate void GameRunning();
    public GameRunning GamePlayingState;



    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }

    private void Start() {
        currentState = new HomeScreenState();
        currentState.EnterState();
    }

    private void Update() {
        currentState.UpdateState();
    }

    public void SetState(GameState newState) {

        currentState.ExitState();
        currentState = newState;
        Debug.Log("SetNew state");
        currentState.EnterState();
    }


    public class HomeScreenState : GameState {

        public override void EnterState() {
            Debug.Log("Enable Home Screen");
            GameManager.Instance.HomeScreen.gameObject.SetActive(true);
        }

        public override void ExitState() {

            Debug.Log("DisableHomescreen");
            GameManager.Instance.HomeScreen.gameObject.SetActive(false);
        }
    }

    public void CollectedCoin(int coinvalue) {
        Coin += coinvalue;
    }


    // Called when the player loses the game
    public void GameOver() {
        SetState(new GameStateGameOver());
    }

    // Called when the player wins the game
    public void Victory() {
        SetState(new GameStateVictory());
    }

    // Called to restart the game
    public void RestartGame() {
        Debug.Log("LoaD sCENE");
        SceneManager.LoadScene(0);
    }
    public void StartPlayGame() {

        Debug.Log("PlayGameStatStart");
        SetState(new GameStatePlaying());
    }
}

// Base state class
public abstract class GameState : MonoBehaviour {
    public virtual void EnterState() { }
    public virtual void UpdateState() { }
    public virtual void ExitState() { }
}

// State when the game is being played

public class GameStatePlaying : GameState {

    public override void EnterState() {
        Time.timeScale = 1f;
        GameManager.Instance.gameOverScreen.SetActive(false);
        GameManager.Instance.victoryScreen.SetActive(false);
    }

    public override void UpdateState() {

        
        GameManager.Instance.GamePlayingState?.Invoke();


    }
    public override void ExitState() {

        Debug.Log("DisableGamePLay");
        GameManager.Instance.GamePlay.gameObject.SetActive(false);
    }
}

// State when the player loses the game
public class GameStateGameOver : GameState {
    public override void EnterState() {
        Time.timeScale = 0f;
        GameManager.Instance.gameOverScreen.SetActive(true);
    }

    public override void UpdateState() {
        // Handle game over UI interactions here
    }
    
}

// State when the player wins the game
public class GameStateVictory : GameState {
    public override void EnterState() {
        Time.timeScale = 0f;
        GameManager.Instance.victoryScreen.SetActive(true);
    }

    public override void UpdateState() {
        // Handle victory UI interactions here
    }
    public override void ExitState() {

        Debug.Log("DisableHomescreen");
        GameManager.Instance.GamePlay.gameObject.SetActive(false);
    }
}





