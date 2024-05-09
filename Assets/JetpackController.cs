using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class JetpackController : MonoBehaviour
{

    public Transform startPosition;
    public Transform[] points;
    public float timeToPoint;
    public float timeToRest;
    private bool isMoving = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isMoving)
        {
            Debug.Log("click");
            StartCoroutine(MoveToPointAndBack());
        }
    }

    public IEnumerator MoveToPointAndBack()
    {
        isMoving = true;

        Debug.Log("tuda");
        Transform targetPoint = points[findPointToMove()];
        yield return StartCoroutine(MoveToPointSmooth(targetPoint, timeToPoint));

        Debug.Log("stop");
        yield return new WaitForSeconds(timeToRest);

        Debug.Log("obratno");
        yield return StartCoroutine(MoveToPointSmooth(startPosition, timeToPoint));

        isMoving = false;
    }

    public int findPointToMove()
    {
        float angle = transform.eulerAngles.z;

        var res = Mathf.RoundToInt(angle / 360f * points.Length) % points.Length;
        Debug.Log(angle);
        Debug.Log(res);
        return res;
    }

    public IEnumerator MoveToPointSmooth(Transform target, float duration)
    {
        Vector3 startingPosition = transform.position;
        float timeElapsed = 0;

        while (timeElapsed < duration)
        {
            float t = timeElapsed / duration;
            t = t * t * (3f - 2f * t);
            transform.position = Vector3.Lerp(startingPosition, target.position, t);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = target.position;
    }
}
