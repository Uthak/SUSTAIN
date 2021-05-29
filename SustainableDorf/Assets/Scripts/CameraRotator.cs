using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    [SerializeField] private Vector3 rotationVector = Vector3.up;
    [SerializeField] private float speed = 1f;

    void FixedUpdate()
    {
        transform.Rotate(rotationVector, speed * Time.deltaTime, Space.World);
    }
}

