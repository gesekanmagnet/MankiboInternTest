using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance { get; private set; }

    [SerializeField] private GameObject enemyObject;
    [SerializeField] private int enemyCount;
    [SerializeField] private Transform enemyList;

    [SerializeField] private LineRenderer line;
    [SerializeField] private int lineCount;
    [SerializeField] private Transform lineList;

    private Queue<GameObject> enemiesPool = new Queue<GameObject>();
    private Queue<LineRenderer> linePool = new Queue<LineRenderer>();

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

    public GameObject GetEnemy()
    {
        if (enemiesPool.Count == 0) CreateGameObjectPool(enemyObject, enemyList, enemiesPool);

        GameObject enemy = enemiesPool.Dequeue();
        enemy.SetActive(true);
        return enemy;
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
        enemy.SetActive(false);
        enemiesPool.Enqueue(enemy);
    }

    public void ReturnLine(LineRenderer line)
    {
        line.gameObject.SetActive(false);
        linePool.Enqueue(line);
    }
}