using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Explode", menuName = "Bullet/Explode", order = 0)]
public class Explode : Bullet
{
    [SerializeField] private ParticleSystem particle;
    [SerializeField] private float radius;

    public override void Shoot(IDestroyable destroyable, Vector2 point)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(point, radius);
        foreach (var item in colliders)
        {
            if (item.TryGetComponent<IDestroyable>(out var component))
                component.Hit();
        }
    }
}