using UnityEngine;

public class LockedDoorController : DoorController
{
    bool isDoorLocked = true; //is the door locked or unlocked

    //Open / close door
    public override void Interact()
    {
        if (isDoorLocked) {
            return;
        }

        HandleDoor();
    }
}
