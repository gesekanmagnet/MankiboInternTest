using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float offset;

    private Transform tower => GameManager.Instance.TowerTransform;
    private Vector3 mousePosition => GameManager.Instance.mousePosition;
    private Vector3 direction;

    private void Update()
    {
        direction = mousePosition - tower.position;

        if(direction.magnitude > offset)
        {
            direction = direction.normalized * offset;
        }

        transform.position = direction + tower.position;
    }
}