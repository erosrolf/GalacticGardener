using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class JetpackController : MonoBehaviour
{
    public float rotationSpeed;
    public float timeToPoint;
    public float timeToRest;

    private bool _isMoving = false;
    private float _rotateDirection;
    private float _lastDirection;

    [SerializeField] GameObject _navigator;

    public delegate void newPositionDelegate(Vector3 target);
    public static event newPositionDelegate NewPositionEvent;
    public delegate void newZonePositionDelegate(Vector3 target);
    public static event newPositionDelegate NewZonePositionEvent;

    public void SetRotateDirection(float direction) => _rotateDirection = direction;
    void Update()
    {
        if (_rotateDirection != 0)
        {
            transform.Rotate(new Vector3(0, 0, 1) * _rotateDirection * Time.deltaTime * rotationSpeed);
            _lastDirection = _rotateDirection;
        }
        transform.Rotate(new Vector3(0, 0, 1) * _lastDirection * Time.deltaTime * 50);
        if (!_isMoving)
        {
            _navigator.transform.position = findPointToMove();
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

    public void MoveIntoSpace()
    {
        if (!_isMoving)
        {
            StartCoroutine(MoveToPoint(findPointToMove()));
        }
    }

    public Vector3 findPointToMove()
    {
        var hexagon = HexagonPoints.CalculatePoints(GameSettings.Instance.DistanceBetweenObjects);
        float angle = transform.eulerAngles.z;
        var index = Mathf.RoundToInt(angle / 360f * hexagon.Length) % hexagon.Length;
        return hexagon[index] + transform.position;
    }

    public IEnumerator MoveToPoint(Vector3 target)
    {
        _isMoving = true;
        Vector3 startPosition = transform.position;
        NewZonePositionEvent?.Invoke(target - startPosition);
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
        NewPositionEvent?.Invoke(target);
    }
}
