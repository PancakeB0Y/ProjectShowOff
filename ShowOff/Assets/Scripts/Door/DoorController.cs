using UnityEngine;

public class DoorController : MonoBehaviour, IInteractable
{
    protected Animator doorAnimator;

    protected bool isDoorOpen = false; //is the door open or closed

    protected void Awake()
    {
        doorAnimator = GetComponent<Animator>();
    }

    //Open / close door
    public virtual void Interact()
    {
        HandleDoor();
    }

    protected void HandleDoor()
    {
        if(IsAnimatorPlaying())
        {
            return;
        }

        if (!isDoorOpen)
        {
            OpenDoor();
            isDoorOpen = true;
        }
        else
        {
            CloseDoor();
            isDoorOpen = false;
        }
    }

    //Play the opening animation and sound
    protected void OpenDoor()
    {
        if (doorAnimator == null)
        {
            return;
        }

        doorAnimator.Play("DoorOpen", 0, 0.0f);

        if (SoundManager.instance != null)
        {
            SoundManager.instance.PlayDoorOpenSound();
        }
    }

    //Play the closing animation and sound
    protected void CloseDoor() {
        if (doorAnimator == null)
        {
            return;
        }

        doorAnimator.Play("DoorClose", 0, 0.0f);

        if (SoundManager.instance != null)
        {
            SoundManager.instance.PlayDoorCloseSound();
        }
    }

    //Check if an animation is being played
    bool IsAnimatorPlaying()
    {
        //ignore function if there is no animator
        if (doorAnimator == null)
        {
            return true;
        }

        //Return true if the animation is 0.1 seconds away from completion
        return doorAnimator.GetCurrentAnimatorStateInfo(0).length >
               doorAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime + 0.1f;
    }
}
