using Unity.VisualScripting;
using UnityEngine;

public class FolowThePlayer : MonoBehaviour
{
    private void OnEnable()
    {
        JetpackController.NewPositionEvent += TranslateOnPlayerPotition;
    }

    private void OnDisable()
    {
        JetpackController.NewPositionEvent -= TranslateOnPlayerPotition;
    }

    private void TranslateOnPlayerPotition(Vector3 target)
    {
        var newPosition = new Vector3(target.x, target.y, transform.position.z);
        transform.position = newPosition;
    }
}
