using UnityEngine;

public class LockedDoorController : MonoBehaviour, IInteractable
{
    Animator doorAnimator;

    bool isDoorOpen = false; //is the door open or closed
    bool isDoorLocked = false; //is the door locked or unlocked

    BoxCollider triggerCollider;

    private void Awake()
    {
        doorAnimator = GetComponent<Animator>();
        triggerCollider = GetComponent<BoxCollider>();
    }

    //Play the opening/closing animation
    public void Interact()
    {
        if (doorAnimator == null || isDoorLocked) {
            return;
        }

        if (!isDoorOpen) {
            doorAnimator.Play("DoorOpen", 0, 0.0f);
            isDoorOpen = true;
        }
        else
        {
            doorAnimator.Play("DoorClose", 0, 0.0f);
            isDoorOpen = false;
        }
    }

    public void FlipLockedSide()
    {
        if (triggerCollider == null) {
            triggerCollider = GetComponent<BoxCollider>();
        }

        if (triggerCollider != null)
        {
            Vector3 triggerCenter = triggerCollider.center;
            triggerCenter.x = -triggerCenter.x;

            triggerCollider.center = triggerCenter;
        }
    }
}
