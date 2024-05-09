using System.Collections;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public Rigidbody jetpackRigidbody;
    public float thrust = 1f;
    public float returnSpeed = 1f;

    private float rotationSpeed = 0f;
    private float rotationAcceleration = 0.1f;

    private Vector3 initialPosition;
    private bool returning = false;

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            RotateToRightIntoSpace();

        if (Input.GetKeyDown(KeyCode.D))
            RotateToLeftIntoSpace();

        if (Input.GetKeyDown(KeyCode.Space) && !returning)
        {
            jetpackRigidbody.AddForce(transform.up * thrust, ForceMode.Impulse);
            StartCoroutine(ReturnToInitialPosition());
        }

        if (returning)
        {
            jetpackRigidbody.velocity = Vector3.zero;
            transform.position = Vector3.Lerp(transform.position, initialPosition, Time.deltaTime * returnSpeed);
        }
    }

    public void RotateToLeftIntoSpace()
    {
        Debug.Log("Left");
        rotationSpeed += rotationAcceleration;
        jetpackRigidbody.AddTorque(0, 0, rotationSpeed, ForceMode.Acceleration);
    }

    public void RotateToRightIntoSpace()
    {
        Debug.Log("Right");
        rotationSpeed += rotationAcceleration;
        jetpackRigidbody.AddTorque(0, 0, -rotationSpeed, ForceMode.Acceleration);
    }

    IEnumerator ReturnToInitialPosition()
    {
        yield return new WaitForSeconds(1f);
        returning = true;
    }
}
