using UnityEngine;

public class WeaponFaceMouse : MonoBehaviour
{
    [SerializeField] private float rotateSpeed;

    private Vector3 targetToFace => GameManager.Instance.mousePosition;
    private float angle;

    private void Update()
    {
        angle = Mathf.Atan2(targetToFace.y - transform.position.y, targetToFace.x -transform.position.x ) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
    }
}