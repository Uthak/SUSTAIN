using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    //[SerializeField] private Vector3 rotationVector = Vector3.up;
    [SerializeField] float Y = 1f;
    Vector3 rotationVector;
    [SerializeField] float speed = 1f;
    [SerializeField] bool isCow = false;
    float randomFloat;
    private void Start()
    {
        rotationVector = new Vector3(0f, Y, 0f);
    }
    void FixedUpdate()
    {
        if (isCow == false)
        {
            transform.Rotate(rotationVector, speed * Time.deltaTime, Space.World);
        }
        else
        {
            randomFloat = Random.Range(0f, 60f);
            transform.Rotate(rotationVector, randomFloat * Time.deltaTime, Space.Self);

        }
    }
}

