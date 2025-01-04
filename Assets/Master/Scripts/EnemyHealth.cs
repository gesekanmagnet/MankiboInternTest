using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDestroyable
{
    public void Hit()
    {
        ObjectPool.Instance.ReturnEnemy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.Instance.DecreaseHealth(1);
        Hit();
    }
}