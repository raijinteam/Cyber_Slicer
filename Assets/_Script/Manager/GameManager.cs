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
    [SerializeField] private PlayerCantroller player;
    public PlayerCantroller MyPlayer { get { return player; } }
    private bool isRewive;


    [Header("Powerup data")]
    [SerializeField] private bool is2XPowerUpActive;

    public bool isRewiveScreenShown;
    [SerializeField] private bool isShieldActivate;
    [SerializeField] private bool isSpeedBoostActive;
    public bool IsShieldActive { get { return isShieldActivate; } set { isShieldActivate = value; } }
    public bool IsSpeedBoostActive { get { return isSpeedBoostActive; } set { isSpeedBoostActive = value; } }
    public bool Is2XActive { get { return is2XPowerUpActive; } set { is2XPowerUpActive = value; } }

    public bool IsplayerLive { get; internal set; }

    [Header("Laser Boss")]
    [SerializeField] private LaserHandler laserHandler;
    public LaserHandler currentLaserHandler;

    [Header("Big Boss")]
    [SerializeField] private BossHandler bossHandler;
    public BossHandler CurrentBossHandler;

    [Header("Small Multiple Boss")]
    [SerializeField] private BossType2Handler bossType2Handler;
    public BossType2Handler CurrentSmallBossHandler;


    [Header("Rocket")]
    [SerializeField] private RocketHandler rocketHandler;
    public RocketHandler rocketEnemyHandler;

    [Header("Rocket Handler")]
    [SerializeField] private AstroidHandler astroidHandler;
    public AstroidHandler currentAstroidHandler;

    [Header("EnemyShip")]
    [SerializeField] private EnemyShipHandler enemyshipHandler;
    public EnemyShipHandler currentEnemyShipHandler;


    public delegate void GameRunning();
    public GameRunning GamePlayingState;
    public delegate void RewiveTime();
    public RewiveTime RewiveCalculation;
    public delegate void GamespeedChange(float GameSpeed);
    public GamespeedChange ChangeGameSpeed;



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




    public void CollectedCoin(int coinvalue) {
        if (is2XPowerUpActive) {
            coinvalue = coinvalue * 2;
            
        }
        Debug.Log(coinvalue + "CollectedCoin");
        Coin += coinvalue;
    }

    public void InstantiateLaserBoss() {

        currentLaserHandler = Instantiate(laserHandler, transform.position, transform.rotation);
        currentLaserHandler.SetLaserLeval();
    }

    public void InstatiateBigBoss() {
        CurrentBossHandler = Instantiate(bossHandler, transform.position, transform.rotation);
        CurrentBossHandler.SetBigBossLevel();
    }

    public void InstatiateSmallBoss() {
        CurrentSmallBossHandler = Instantiate(bossType2Handler, transform.position, transform.rotation);
        CurrentSmallBossHandler.SetType2Boss();
    }

    public void InstatiateRocketEnemy() {
        rocketEnemyHandler = Instantiate(rocketHandler, transform.position, transform.rotation);
        rocketEnemyHandler.setRocket();
    }
    public void InstatiateAstroidEnemy() {
        currentAstroidHandler = Instantiate(astroidHandler, transform.position, transform.rotation);
        currentAstroidHandler.SetAstroidData();
    }
    
    public void InstatiateEnemyShip() {
        currentEnemyShipHandler = Instantiate(enemyshipHandler, transform.position, transform.rotation);
        currentEnemyShipHandler.SetEnemy();
    }


    // Called when the player loses the game
    public void GameOver() {
        if (isShieldActivate) {
            return;
        }
        else if (isSpeedBoostActive) {
            return;
        }
        else if (isRewiveScreenShown) {
            return;
        }
        else {
            if (!isRewiveScreenShown) {
                isRewiveScreenShown = true;
                IsplayerLive = false;
                SetState(new GameStateRewive());
            }
            else {
                IsplayerLive = false;
                SetState(new GameStateGameOver());
            }
        }

        

    }

  
   
    public void RestartGame() {
        Debug.Log("Load  Scene");
        SceneManager.LoadScene(0);
    }


   
    public void StartGame() {

        IsplayerLive = true;
        Debug.Log("PlayGameStatStart");
        UiManager.instance.GamePlay.gameObject.SetActive(true);
        SetState(new GameStatePlaying());
    }

    public void PlayerGetRewive() {

       UiManager.instance.RewiveScreen.gameObject.SetActive(false);
        SetState(new GameStatePlaying());
        StartCoroutine(DeisablePlayer());
    }

    private IEnumerator DeisablePlayer() {
        isRewive = true;
        yield return new WaitForSeconds(1);
        isRewive = false;
    }

   
}







// Base state class
public abstract class GameState : MonoBehaviour {
    public virtual void EnterState() { }
    public virtual void UpdateState() { }
    public virtual void ExitState() { }
}


public class HomeScreenState : GameState {

    public override void EnterState() {
        Debug.Log("Enable Home Screen");
        UiManager.instance.HomeScreen.gameObject.SetActive(true);
    }

    public override void ExitState() {

        Debug.Log("DisableHomescreen");
        UiManager.instance.HomeScreen.gameObject.SetActive(false);
    }
}

// State when the game is being played

public class GameStatePlaying : GameState {

    public override void EnterState() {
        Time.timeScale = 1f;
       
    }

    public override void UpdateState() {

        GameManager.Instance.GamePlayingState?.Invoke();


    }
    public override void ExitState() {

        Debug.Log("DisableGamePLay");
        UiManager.instance.GamePlay.gameObject.SetActive(false);
    }
}

// State when the player loses the game
public class GameStateGameOver : GameState {
    public override void EnterState() {
        Time.timeScale = 0f;
        UiManager.instance.gameOverScreen.SetActive(true);
    }

    public override void UpdateState() {
        // Handle game over UI interactions here
    }
    
}



public class GameStateRewive : GameState {

    public override void EnterState() {
        
        UiManager.instance.RewiveScreen.SetActive(true);
    }

    public override void UpdateState() {

        GameManager.Instance.RewiveCalculation?.Invoke();
    }
    public override void ExitState() {

        UiManager.instance.GamePlay.gameObject.SetActive(false);
    }
}





