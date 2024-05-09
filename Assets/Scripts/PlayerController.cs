using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform centerPoint;
    public Transform[] points;

    public Rigidbody jetpackRigidbody;
    public float moveSpeed;
    public float damping;

    private float rotationSpeed = 0f;
    private float rotationAcceleration = 0.5f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            RotateToRightIntoSpace();

        if (Input.GetKeyDown(KeyCode.D))
            RotateToLeftIntoSpace();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("space");
            StartCoroutine(MoveToPoint(points[findPointToMove()]));
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

    public int findPointToMove()
    {
        float angle = transform.eulerAngles.z;

        var res = Mathf.RoundToInt(angle / 360f * points.Length) % points.Length;
        Debug.Log(angle);
        Debug.Log(res);
        return res;
    }

    private IEnumerator MoveToPoint(Transform point)
    {
        Vector3 direction = (point.position - transform.position).normalized;
        jetpackRigidbody.AddForce(direction * moveSpeed, ForceMode.Impulse);

        while (Vector3.Distance(transform.position, point.position) > 0.1f)
        {
            jetpackRigidbody.velocity *= damping;
            yield return null;
        }
        yield return MoveToPoint(centerPoint);

        jetpackRigidbody.velocity = Vector3.zero;
    }
}
