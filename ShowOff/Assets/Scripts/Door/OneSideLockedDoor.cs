using UnityEngine;

//Door which can be unlocked only from one side
//Once unlocked it can be opened from both sides
public class OneSideLockedDoorController : DoorController
{
    bool isDoorLocked = true; //is the door locked or unlocked

    bool isPlayerOnCorrectSide = false; //is the player on the correct side of the door

    BoxCollider triggerCollider;

    private new void Awake()
    {
        base.Awake();
        triggerCollider = GetComponent<BoxCollider>();
    }

    //Play the opening/closing animation
    public override void Interact()
    {
        if (isPlayerOnCorrectSide) {
            isDoorLocked = false;
        }

        if (isDoorLocked)
        {
            doorAudioController.PlayDoorNotOpeningSound();
            return;
        }

        HandleDoor();
    }

    //Check if player is on the correct side
    void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<PlayerController>(out PlayerController player)){
            isPlayerOnCorrectSide = true;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent<PlayerController>(out PlayerController player)){
            isPlayerOnCorrectSide = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<PlayerController>(out PlayerController player)){
            isPlayerOnCorrectSide = false;
        }
    }


    //Flip which side is locked
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
