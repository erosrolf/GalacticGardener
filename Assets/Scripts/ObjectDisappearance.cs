using System.Collections;
using Architecture;
using UnityEngine;

public class ObjectDisappearance : MonoBehaviour
{
    public float shrinkSpeed = 0.01f; // Скорость уменьшения объекта
    public float moveSpeed = 0.01f; // Скорость перемещения объекта

    public void StartDisappearance()
    {
        StartCoroutine(Disappearance());
    }

    IEnumerator Disappearance()
    {
        Vector3 playerPosition = GameManager.Instance.GetPlayerPosition();
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        while (transform.localScale.magnitude > 0.1f) // Замените 0.1 на любое значение, которое вам подходит
        {
            transform.position = Vector3.MoveTowards(transform.position, playerPosition, moveSpeed);
            transform.localScale -= new Vector3(shrinkSpeed, shrinkSpeed, shrinkSpeed);
            yield return null;
        }
        Destroy(gameObject);
    }
}
