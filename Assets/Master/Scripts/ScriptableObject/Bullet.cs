using UnityEngine;

public abstract class Bullet : ScriptableObject
{
    public float cooldown;
    public Gradient lineColorGradient;
    public AudioClip clip;

    public abstract void Shoot(IDestroyable destroyable, Vector2 point);
}