using System.Collections;
using UnityEngine;

public class ShootingSystem : MonoBehaviour
{
    [SerializeField] private LayerMask layer;
    [SerializeField] private Bullet[] bullets;

    private Bullet currentBullet;
    private Vector3 direction;
    private RaycastHit2D hit;
    private float fireInterval;

    private void OnEnable()
    {
        GameManager.Instance.OnHeldMouse += Shoot;
        GameManager.Instance.OnSwitchBullet += SwitchBullet;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnHeldMouse -= Shoot;
        GameManager.Instance.OnSwitchBullet += SwitchBullet;
    }

    private void Start()
    {
        currentBullet = bullets[0];
    }

    private void Update()
    {
        if (fireInterval < currentBullet.cooldown)
            fireInterval += Time.deltaTime;
    }

    private void Shoot()
    {
        if(fireInterval >= currentBullet.cooldown)
        {
            fireInterval = 0;
            
            direction = GameManager.Instance.mousePosition - transform.position;
            float distance = Vector2.Distance(GameManager.Instance.mousePosition, transform.position);
            hit = Physics2D.Raycast(transform.position, direction.normalized, distance, layer);

            if (hit)
            {
                Debug.DrawLine(transform.position, hit.point, Color.red);
                
                if(hit.collider.TryGetComponent<IDestroyable>(out var destroyable))
                    currentBullet.Shoot(destroyable, hit.point);

                StartCoroutine(BulletTracer());
                AudioManager.Instance.PlaySFX(currentBullet.clip);
                ObjectPool.Instance.GetEnemyParticle().transform.position = hit.point;

                return;
            }

            // Spawn and throw a bomb. Bomb will explode when reach destination point. Bomb will give area damage to enemies nearby its radius.
            if(currentBullet == bullets[2])
            {
                AudioManager.Instance.PlaySFX(currentBullet.clip);
                currentBullet.Shoot(null, GameManager.Instance.mousePosition);
            }
        }
    }

    private IEnumerator BulletTracer()
    {
        LineRenderer line = ObjectPool.Instance.GetLine();
        line.colorGradient = currentBullet.lineColorGradient;
        line.SetPosition(0, transform.position);
        line.SetPosition(1, hit.point);

        yield return new WaitForSeconds(currentBullet.cooldown / 3);
        
        ObjectPool.Instance.ReturnLine(line);
    }

    private void SwitchBullet(int x)
    {
        currentBullet = bullets[x];
    }
}