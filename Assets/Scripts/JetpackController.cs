using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class JetpackController : MonoBehaviour
{
    public float rotationSpeed;
    public float timeToPoint;
    public float timeToRest;

    private float _distanceToPoint = 3.48f;
    private bool _isMoving = false;
    private Rigidbody _jetpack;
    private float _rotateDirection;

    public void SetRotateDirection(float direction) => _rotateDirection = direction;

    void Awake()
    {
        _jetpack = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (_rotateDirection != 0)
        {
            transform.Rotate(new Vector3(0, 0, 1) * _rotateDirection * Time.deltaTime * rotationSpeed);
        }
    }

    public void RotateLeft(InputAction.CallbackContext context)
    {
        if (context.started || context.performed)
        {
            Debug.Log("rotateLeft");
            transform.Rotate(Vector3.up * -rotationSpeed * Time.deltaTime);
        }
    }

    public void RotateRight(InputAction.CallbackContext context)
    {
        if (context.started || context.performed)
        {
            Debug.Log("rotateRight");
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        }
    }

    public void MoveIntoSpace(InputAction.CallbackContext context)
    {
        if (!_isMoving && context.started)
        {
            Vector3 targetPoint = transform.position + findPointToMove();
            float distanceToCenter = Vector3.Distance(targetPoint, Vector3.zero);

            if (distanceToCenter <= 4f)
            {
                StartCoroutine(MoveToPoint(targetPoint));
            }
            else
            {
                StartCoroutine(MoveToPointAndBack(targetPoint));
            }
        }
    }

    public Vector3 findPointToMove()
    {
        var hexagon = HexagonPoints.CalculatePoints(_distanceToPoint);
        float angle = transform.eulerAngles.z;
        var index = Mathf.RoundToInt(angle / 360f * hexagon.Length) % hexagon.Length;
        return hexagon[index];
    }

    public IEnumerator MoveToPointAndBack(Vector3 target)
    {
        Vector3 startPosition = transform.position;
        _isMoving = true;
        Debug.Log($"tuda {startPosition + findPointToMove()}");
        yield return StartCoroutine(MoveToPoint(target));
        Debug.Log($"rest = {timeToRest}");
        yield return new WaitForSeconds(timeToRest);
        Debug.Log($"obratno = {startPosition}");
        yield return StartCoroutine(MoveToPoint(startPosition));
        _isMoving = false;
    }


    public IEnumerator MoveToPoint(Vector3 target)
    {
        _isMoving = true;
        Vector3 startPosition = transform.position;
        float timeElapsed = 0;

        while (timeElapsed < timeToPoint)
        {
            float t = timeElapsed / timeToPoint;
            t = t * t * (3f - 2f * t);
            transform.position = Vector3.Lerp(startPosition, target, t);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = target;
        _isMoving = false;
    }
}
