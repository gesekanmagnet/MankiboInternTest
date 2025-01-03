using System.Collections.Generic;

using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance { get; private set; }

    [SerializeField] private GameObject enemyObject;
    [SerializeField] private int enemyCount;
    [SerializeField] private Transform enemyList;

    private Queue<GameObject> enemiesPool = new Queue<GameObject>();

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
}