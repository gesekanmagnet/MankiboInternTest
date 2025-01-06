using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance { get; private set; }

    [Header("")]
    [SerializeField] private GameObject enemyObject;
    [SerializeField] private int enemyCount;
    [SerializeField] private Transform enemyList;

    [Header("")]
    [SerializeField] private LineRenderer line;
    [SerializeField] private int lineCount;
    [SerializeField] private Transform lineList;

    [Header("")]
    [SerializeField] private Transform particleList;
    [SerializeField] private GameObject explodeParticle, enemyParticle;
    [SerializeField] private int particleCount;


    private Queue<GameObject> enemiesPool = new Queue<GameObject>();
    private Queue<LineRenderer> linePool = new Queue<LineRenderer>();
    private Queue<GameObject> explodePool = new Queue<GameObject>();
    private Queue<GameObject> enemyParticlePool = new Queue<GameObject>();

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void OnEnable()
    {
        GameManager.Instance.OnGameStart += Initialize;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameStart -= Initialize;   
    }

    private void Initialize()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            CreateGameObjectPool(enemyObject, enemyList, enemiesPool);
        }

        for (int i = 0; i < particleCount; i++)
        {
            CreateGameObjectPool(explodeParticle, particleList, explodePool);
            CreateGameObjectPool(enemyParticle, particleList, enemyParticlePool);
        }

        for (int i = 0; i < lineCount; i++)
        {
            CreateLinePool(line, lineList, linePool);
        }
    }

    private void CreateGameObjectPool(GameObject gameObject, Transform parent, Queue<GameObject> queue)
    {
        GameObject target = Instantiate(gameObject, parent);
        target.SetActive(false);
        queue.Enqueue(target);
    }

    private void CreateLinePool(LineRenderer line, Transform parent, Queue<LineRenderer> queue)
    {
        LineRenderer target = Instantiate(line, parent);
        target.gameObject.SetActive(false);
        queue.Enqueue(target);
    }

    private GameObject GetObject(GameObject x, Transform list, Queue<GameObject> queue)
    {
        if(queue.Count == 0) CreateGameObjectPool(x, list, queue);

        GameObject gameObject = queue.Dequeue();
        gameObject.SetActive(true);
        return gameObject;
    }
    
    private void ReturnObject(GameObject x, Queue<GameObject> queue)
    {
        x.SetActive(false);
        queue.Enqueue(x);
    }

    public GameObject GetEnemy()
    {
        return GetObject(enemyObject, enemyList, enemiesPool);
    }

    public GameObject GetExplode()
    {
        return GetObject(explodeParticle, particleList, explodePool);
    }

    public GameObject GetEnemyParticle()
    {
        return GetObject(enemyParticle, particleList, enemyParticlePool);
    }

    public LineRenderer GetLine()
    {
        if (linePool.Count == 0) CreateLinePool(line, lineList, linePool);

        LineRenderer lr = linePool.Dequeue();
        lr.gameObject.SetActive(true);
        return lr;
    }

    public void ReturnEnemy(GameObject enemy)
    {
        ReturnObject(enemy, enemiesPool);
    }

    public void ReturnExplode(GameObject explode)
    {
        ReturnObject(explode, explodePool);
    }

    public void ReturnEnemyParticle(GameObject particle)
    {
        ReturnObject(particle, enemyParticlePool);
    }

    public void ReturnLine(LineRenderer line)
    {
        line.gameObject.SetActive(false);
        linePool.Enqueue(line);
    }
}