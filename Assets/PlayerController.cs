using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody Rigidbody;
    public float ImpulseForce;

    public void Jump() => SetImpulse(Vector3.up);
    public void ImpulseToForward() => SetImpulse(Vector3.forward);
    public void ImpulseToBackward() => SetImpulse(Vector3.back);
    public void ImpulseToLeft() => SetImpulse(Vector3.left);
    public void ImpulseToRight() => SetImpulse(Vector3.right);

    private void SetImpulse(Vector3 vector)
    {
        Rigidbody.AddForce(vector * ImpulseForce, ForceMode.Impulse);
    }
}
