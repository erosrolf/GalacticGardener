using System.Collections;
using System.Collections.Generic;
using Architecture;
using UnityEngine;
using UnityEngine.InputSystem;

public class JetpackInputReader : MonoBehaviour
{
    [SerializeField] private JetpackController _jetpackController;

    public void Rotation(InputAction.CallbackContext context)
    {
        var direction = context.ReadValue<float>();
        _jetpackController.SetRotateDirection(direction);
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _jetpackController.MoveIntoSpace();
        }
    }
}
