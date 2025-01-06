using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDestroyable
{
    private void OnEnable()
    {
        GameManager.Instance.OnGameOver += ReturnEnemy;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameOver -= ReturnEnemy;
    }
    
    public void Hit()
    {
        ObjectPool.Instance.ReturnEnemy(gameObject);
    }

    private void ReturnEnemy(int x)
    {
        Hit();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.Instance.OnHit();
        Hit();
    }
}