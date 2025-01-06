using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Basic", menuName = "Bullet/Basic", order = 0)]
public class Basic : Bullet
{
    public override void Shoot(IDestroyable destroyable, Vector2 point)
    {
        destroyable.Hit();
        CameraShake.Instance.enabled = true;
    }
}