using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public Vector3 mousePosition { get; private set; }
    public Transform TowerTransform => towerTransform;
    public float currentHealth { get; private set; }
    public float currentTime { get; private set; }

    public Action OnGameStart { get; set; } = delegate { };
    public Action OnGameWin { get; set; } = delegate { };
    public Action OnGameOver { get; set; } = delegate { };
    public Action OnFire { get; set; } = delegate { };

    [SerializeField] private Camera mainCamera;
    [SerializeField] private Transform towerTransform;
    [SerializeField] private float maxHealth, maxTimer;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        currentHealth = maxHealth;
        currentTime = maxTimer;
        OnGameStart();
    }

    private void Update()
    {
        currentTime -= Time.deltaTime;
        mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        if(Input.GetButton("Fire1"))
        {
            OnFire();
        }
    }

    public void DecreaseHealth(float health)
    {
        currentHealth -= health;
        if(currentHealth <= 0)
            OnGameOver();
    }
}