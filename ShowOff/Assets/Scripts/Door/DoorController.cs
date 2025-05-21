using UnityEngine;

public class DoorController : MonoBehaviour, IInteractable
{
    Animator doorAnimator;

    bool isDoorOpen = false; //is the door open or closed

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

        if (!isDoorOpen)
        {
            doorAnimator.Play("DoorOpen", 0, 0.0f);
            isDoorOpen = true;

            if (SoundManager.instance != null)
            {
                SoundManager.instance.PlayDoorOpenSound();
            }
        }
        else
        {
            doorAnimator.Play("DoorClose", 0, 0.0f);
            isDoorOpen = false;

            if (SoundManager.instance != null)
            {
                SoundManager.instance.PlayDoorCloseSound();
            }
        }
    }
}
