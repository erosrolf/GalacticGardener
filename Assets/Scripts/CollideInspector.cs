using Cinemachine;
using UnityEngine;

public class CollideInspector : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _onPlayerVcam;
    [SerializeField] private GameObject _destroyer;
    [SerializeField] private JetpackController _jetpackController;
    [SerializeField] private Rigidbody _jetpackRigidbody;

    public delegate void CollisionEventDelegate();
    public static event CollisionEventDelegate CollideWithEnemy;
    public static event CollisionEventDelegate CollideWithTree;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Enemy collision");
            CollideWithEnemy?.Invoke();
            CollisionWithEnemy();
        }
        else if (other.gameObject.CompareTag("Tree"))
        {
            Debug.Log("Tree collision");
            other.gameObject.GetComponent<ObjectDisappearance>().StartDisappearance();
            CollideWithTree?.Invoke();
        }
    }

    private void CollisionWithEnemy()
    {
        _jetpackController.enabled = false;

        _jetpackRigidbody.constraints = RigidbodyConstraints.None;
        _jetpackRigidbody.isKinematic = false;
        // _rigidbody.AddForce(Vector3.back * 5, ForceMode.Impulse);
        _destroyer.SetActive(false);
        _onPlayerVcam.Priority = 0;
    }
}
