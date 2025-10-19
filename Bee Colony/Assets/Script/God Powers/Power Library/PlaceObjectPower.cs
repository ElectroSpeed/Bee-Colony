using UnityEngine.InputSystem;

public class PlaceObjectPower : PowerBase
{
    public void UsePower(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            ActivatePower();
        }
    }
}