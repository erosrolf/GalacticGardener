using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CollideInspector : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _onPlayerVcam;
    [SerializeField] private GameObject _destroyer;
    private JetpackController _controller;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _controller = GetComponent<JetpackController>();
        _rigidbody = GetComponent<Rigidbody>();
    }

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
        _controller.enabled = false;

        _rigidbody.constraints = RigidbodyConstraints.None;
        _rigidbody.isKinematic = false;
        // _rigidbody.AddForce(Vector3.back * 5, ForceMode.Impulse);
        _destroyer.SetActive(false);
        _onPlayerVcam.Priority = 0;
    }
}
