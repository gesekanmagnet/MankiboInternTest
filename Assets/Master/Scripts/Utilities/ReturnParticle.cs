using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ParticleType
{
    Explode,
    Enemy
}

public class ReturnParticle : MonoBehaviour
{
    [SerializeField] private ParticleType particleType;

    public void OnParticleSystemStopped()
    {
        switch (particleType)
        {
            case ParticleType.Explode:
                ObjectPool.Instance.ReturnExplode(gameObject);
                break;
            case ParticleType.Enemy:
                ObjectPool.Instance.ReturnEnemyParticle(gameObject);
                break;
            default:
                break;
        }
    }
}
