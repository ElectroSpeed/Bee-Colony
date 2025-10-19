using UnityEngine;
using UnityEngine.InputSystem;

public class DestroyObjectPower : PowerBase
{
    public void UsePower(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            ActivatePower();
        }
    }
}