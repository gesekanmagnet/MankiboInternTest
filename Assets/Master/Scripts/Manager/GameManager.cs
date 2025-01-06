using System;
using UnityEngine;

public enum GameState
{
    GameStart,
    GameOver
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public Vector3 mousePosition { get; private set; }
    public Transform TowerTransform => towerTransform;
    public float currentHealth { get; private set; }
    public float currentTime { get; private set; }

    public Action OnGameStart { get; set; } = delegate { };
    public Action OnGameWin { get; set; } = delegate { };
    public Action<int> OnGameOver { get; set; } = delegate { };
    public Action OnHeldMouse { get; set; } = delegate { };
    public Action OnGameUpdate { get; set; } = delegate { };

    public Action OnHit { get; set; } = delegate { };
    public Action<int> OnSwitchBullet { get; set; } = delegate { };

    [SerializeField] private Camera mainCamera;
    [SerializeField] private Transform towerTransform;
    [SerializeField] private float maxHealth, maxTimer;

    public GameState currentState { get; private set; } = GameState.GameOver;
    
    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void OnEnable()
    {
        OnGameStart += Initialize;
        OnGameUpdate += UpdateGame;
        OnHit += DecreaseHealth;
    }

    private void OnDisable()
    {
        OnGameStart -= Initialize;
        OnGameUpdate -= UpdateGame;
        OnHit -= DecreaseHealth;
    }

    private void Update()
    {
        OnGameUpdate();
    }

    public void StartGame()
    {
        OnGameStart();
    }

    private void Initialize()
    {
        currentHealth = maxHealth;
        currentTime = maxTimer;
        currentState = GameState.GameStart;
    }

    private void UpdateGame()
    {
        switch (currentState)
        {
            case GameState.GameStart:
                mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                if(currentTime > 0)
                    currentTime -= Time.deltaTime;
                else
                {
                    OnGameOver(1);
                    currentState = GameState.GameOver;
                }
                break;
            case GameState.GameOver:
                currentTime = 0;
                mousePosition = Vector3.zero;
                break;
            default:
                break;
        }
    }

    private void DecreaseHealth()
    {
        currentHealth -= 1;
        if(currentHealth <= 0)
        {
            OnGameOver(0);
            currentState = GameState.GameOver;
        }
    }
}