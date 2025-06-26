using System.Collections;
using UnityEngine;

public class DrawerController : MonoBehaviour, IInteractable
{
    [SerializeField] float interactCooldown = 0.3f;
    bool isCooldownOver = true;

    public string interactText { get; } = "Press [E] to interact";

    protected Animator drawerAnimator;

    public bool isDrawerOpen = false; //is the door open or closed

    protected DoorAudioController doorAudioController;


    protected void Awake()
    {
        drawerAnimator = GetComponent<Animator>();

        doorAudioController = GetComponent<DoorAudioController>();
    }

    //Open / close door
    public virtual void Interact()
    {
        HandleDoor();
    }

    public virtual void InteractWithInventory(ItemController inventoryItem) { }

    protected void HandleDoor()
    {
        if (!isCooldownOver)
        {
            return;
        }

        if (!isDrawerOpen)
        {
            OpenDoor();
        }
        else
        {
            CloseDoor();
        }

        StartCoroutine(InteractCooldownCoroutine());
    }

    //Play the opening animation and sound
    public void OpenDoor()
    {

        //Play sound
        doorAudioController.PlayDoorOpenSound();

        if (drawerAnimator == null)
        {
            return;
        }

        //Handle animation
        float startTime = 0.0f; //start time of animation

        //if the closing animation is being player, start opening from the current door position
        if (IsAnimatorPlaying())
        {
            startTime = 1 - drawerAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        }

        drawerAnimator.Play("DrawerOpen", 0, startTime);

        isDrawerOpen = true;
    }

    //Play the closing animation and sound
    public void CloseDoor(float animStartTime = 0f)
    {

        //Play sound
        doorAudioController.PlayDoorCloseSound();

        if (drawerAnimator == null)
        {
            return;
        }

        //Handle animation
        float startTime = animStartTime; //start time of animation

        //if the closing animation is being played, start opening from the current door position
        if (IsAnimatorPlaying())
        {
            startTime = 1 - drawerAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        }

        drawerAnimator.Play("DrawerClose", 0, startTime);

        isDrawerOpen = false;
    }

    //Check if an animation is being played
    bool IsAnimatorPlaying()
    {
        return drawerAnimator.GetCurrentAnimatorStateInfo(0).length >
               drawerAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }

    IEnumerator InteractCooldownCoroutine()
    {
        isCooldownOver = false;

        yield return new WaitForSeconds(interactCooldown);

        isCooldownOver = true;
    }
}
