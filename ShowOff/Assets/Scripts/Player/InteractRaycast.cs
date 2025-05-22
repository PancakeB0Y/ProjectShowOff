using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InteractRaycast : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private LayerMask interactLayer; //interactable object layer
    [SerializeField] private float interactRange = 1.5f;

    [Header("UI")]
    private GameObject textPopup = null; //text to show interact button
    private string interactTextTag = "InteractText";

    private IInteractable interactableObject; //object the player tries to interact with

    private bool isTextOn;

    private void Awake()
    {
        textPopup = GameObject.FindWithTag(interactTextTag);

        if (textPopup != null) {
            textPopup.SetActive(false);
        }
    }

    void Update()
    {
        RaycastHit hit;
        Vector3 forward = transform.TransformDirection(Vector3.forward);

        if(Physics.Raycast(transform.position, forward, out hit, interactRange, interactLayer.value, QueryTriggerInteraction.Ignore))
        {
            //Check for interactable in the current object
            if (!hit.collider.gameObject.TryGetComponent<IInteractable>(out interactableObject))
            {
                //Check for interactable in its parent
                interactableObject = hit.collider.gameObject.GetComponentInParent<IInteractable>();
            }

            if (interactableObject == null)
            {
                return;
            }

            //Display popup text
            DisplayText(true);
        }
        else //when the player is not looking at the object
        {
            //hide the text
            DisplayText(false);
        }

    }

    //Interact with current object
    //Called when player presses button
    public void OnInteract()
    {
        if (interactableObject == null)
        {
            return;
        }

        interactableObject.Interact();
    }

    //Display text to show button input for interactions
    void DisplayText(bool display)
    {
        if(textPopup == null)
        {
            return;
        }

        //Check if the text is already on
        if(display && !isTextOn)
        {
            textPopup.SetActive(true);
            isTextOn = true;
        }
        else if(!display && isTextOn)
        {
            textPopup.SetActive(false);
            isTextOn = false;
        }
    }
}
