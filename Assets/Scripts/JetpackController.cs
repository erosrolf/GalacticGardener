using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class JetpackController : MonoBehaviour
{
    public float rotationSpeed;
    public float timeToPoint;
    public float timeToRest;

    private bool _isMoving = false;
    private float _rotateDirection;
    private float _lastDirection;

    [SerializeField] GameObject _navigator;
    [SerializeField] Image _navigatorImage;

    public void SetRotateDirection(float direction) => _rotateDirection = direction;
    void Update()
    {
        if (_rotateDirection != 0)
        {
            transform.Rotate(new Vector3(0, 0, 1) * _rotateDirection * Time.deltaTime * rotationSpeed);
            _lastDirection = _rotateDirection;
        }
        transform.Rotate(new Vector3(0, 0, 1) * _lastDirection * Time.deltaTime * 50);
        var navigatorPosition = transform.position + findPointToMove();
        if (!IsRedPoint(navigatorPosition))
        {
            _navigatorImage.color = Color.red;
        }
        else
        {
            _navigatorImage.color = Color.white;
        }
        _navigator.transform.position = navigatorPosition;
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

    private bool IsRedPoint(Vector3 targetPoint)
    {
        float distanceToCenter = Vector3.Distance(targetPoint, Vector3.zero);
        return distanceToCenter <= GameSettings.Instance.DistanceToRedPoint;
    }

    public void MoveIntoSpace()
    {
        if (!_isMoving)
        {
            Vector3 targetPoint = transform.position + findPointToMove();
            if (IsRedPoint(targetPoint))
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
        var hexagon = HexagonPoints.CalculatePoints(GameSettings.Instance.DistanceBetweenObjects);
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
