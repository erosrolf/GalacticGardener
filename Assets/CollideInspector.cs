using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CollideInspector : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _onPlayerVcam;
    [SerializeField] private GameObject _destroyer;
    [SerializeField] private JetpackController _jetpackController;
    [SerializeField] private Rigidbody _jetpackRigidbody;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            CollisionWithEnemy();
        }
    }

    private void CollisionWithEnemy()
    {
        Debug.Log("Collision detected with enemy");
        _jetpackController.enabled = false;

        _jetpackRigidbody.constraints = RigidbodyConstraints.None;
        _jetpackRigidbody.isKinematic = false;
        // _rigidbody.AddForce(Vector3.back * 5, ForceMode.Impulse);
        _destroyer.SetActive(false);
        _onPlayerVcam.Priority = 0;
    }
}
