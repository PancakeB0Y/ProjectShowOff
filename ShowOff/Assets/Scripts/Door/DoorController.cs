using UnityEngine;

public class DoorController : MonoBehaviour, IInteractable
{
    public string interactText { get; } = "Press [E] to interact";

    protected Animator doorAnimator;

    protected bool isDoorOpen = false; //is the door open or closed

    private DoorAudioController doorAudioController;

    protected void Awake()
    {
        doorAnimator = GetComponent<Animator>();

        doorAudioController = GetComponent<DoorAudioController>();
    }

    //Open / close door
    public virtual void Interact()
    {
        HandleDoor();
    }

    public virtual void InteractWithInventory(ItemController inventoryItem){}

    protected void HandleDoor()
    {
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
        //Play sound
        doorAudioController.PlayDoorOpenSound();

        if (doorAnimator == null)
        {
            return;
        }

        //Handle animation
        float startTime = 0.0f; //start time of animation

        //if the closing animation is being player, start opening from the current door position
        if (IsAnimatorPlaying())
        {
            startTime = 1 - doorAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        }

        doorAnimator.Play("DoorOpen", 0, startTime);
    }

    //Play the closing animation and sound
    protected void CloseDoor() {
        //Play sound
        doorAudioController.PlayDoorCloseSound();

        if (doorAnimator == null)
        {
            return;
        }

        //Handle animation
        float startTime = 0.0f; //start time of animation

        //if the closing animation is being player, start opening from the current door position
        if (IsAnimatorPlaying())
        {
            startTime = 1 - doorAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        }

        doorAnimator.Play("DoorClose", 0, startTime);  
    }

    //Check if an animation is being played
    bool IsAnimatorPlaying()
    {
        return doorAnimator.GetCurrentAnimatorStateInfo(0).length >
               doorAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }
}
