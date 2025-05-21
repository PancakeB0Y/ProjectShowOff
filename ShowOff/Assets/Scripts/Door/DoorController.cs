using UnityEngine;

public class DoorController : MonoBehaviour, IInteractable
{
    private Animator doorAnimator;

    private bool isDoorOpen = false;

    private void Awake()
    {
        doorAnimator = GetComponent<Animator>();
    }

    //Play the opening/closing animation
    public void Interact()
    {
        if (doorAnimator == null) {
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
}
