using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JetpackInputReader : MonoBehaviour
{
    [SerializeField] private JetpackController _jetpackController;

    public void Rotation(InputAction.CallbackContext context)
    {
        Debug.Log("rotate");
        var direction = context.ReadValue<float>();
        _jetpackController.SetRotateDirection(direction);
    }

    public void Move(InputAction.CallbackContext context)
    {
        Debug.Log("move");
        if (context.started)
        {
            _jetpackController.MoveIntoSpace();
        }
    }
}
