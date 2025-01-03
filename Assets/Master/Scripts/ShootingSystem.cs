using UnityEngine;

public class ShootingSystem : MonoBehaviour
{
    [SerializeField] private LayerMask layer;

    private Vector3 direction;

    private void Update()
    {
        direction = GameManager.Instance.mousePosition - transform.position;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction.normalized, Mathf.Infinity, layer);
        if(hit)
        {
            Debug.DrawRay(transform.position, hit.point, Color.red);
        }
    }
}