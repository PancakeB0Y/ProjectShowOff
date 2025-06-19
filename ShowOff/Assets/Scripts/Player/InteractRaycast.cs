using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

//Tracks if the player is looking at an interactable object
public class InteractRaycast : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private LayerMask collisionLayers; //all layers the raycast can collide with (incl. interact layer)
    [SerializeField] private LayerMask interactLayer; //interactable object layer
    [SerializeField] private float interactRange = 1.5f;

    int interactLayerIndex; //the layer index of the interact layer

    //Interaction text pop-up
    GameObject textPopup = null; //text to show interact button
    TextMeshProUGUI textPopupMesh = null; //textMeshComponent of the textPopup
    string interactTextTag = "InteractText"; //tag of the textPopup object

    IInteractable interactableObject; //object the player tries to interact with

    bool isTextOn;

    //Raycast timer
    float castRayDelay = 0.1f;
    float timer = 0f;

    private void Awake()
    {
        textPopup = GameObject.FindWithTag(interactTextTag);

        interactLayerIndex = LayerMaskToIndex(interactLayer);
    }

    private void Start()
    {
        if (textPopup != null)
        {
            textPopupMesh = textPopup.GetComponent<TextMeshProUGUI>();
            textPopup.SetActive(false);
        }
    }

    void Update()
    {
        //Cast a ray every "castRayDelay" seconds
        timer += Time.deltaTime;

        if (timer >= castRayDelay) {
            CastRay();

            timer = 0f;
        }
    }

    //Cast a ray to detect an interactable object
    void CastRay()
    {
        RaycastHit hit;
        Vector3 forward = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(transform.position, forward, out hit, interactRange, collisionLayers.value, QueryTriggerInteraction.Ignore))
        {
            //check if the wrong layer was hit
            if (hit.collider.gameObject.layer != interactLayerIndex)
            {
                ResetInteractionState();
                return;
            }

            //Check for interactable in the current object
            if (!hit.collider.gameObject.TryGetComponent<IInteractable>(out interactableObject))
            {
                //Check for interactable in its parent
                interactableObject = hit.collider.gameObject.GetComponentInParent<IInteractable>();
            }

            if (interactableObject != null)
            {
                //Match the text with the object's
                SetPopupText(interactableObject.interactText);

                //Display popup text
                TogglePopupText(true);
            }
        }
        else //when the player is not looking at the object
        {
            ResetInteractionState();
        }
    }

    void ResetInteractionState()
    {
        //hide the text
        TogglePopupText(false);

        //remove the saved object
        interactableObject = null;
    }

    int LayerMaskToIndex(LayerMask layerMask)
    {
        return Mathf.RoundToInt(Mathf.Log(layerMask.value, 2));
    }

    //Interact with current object
    //Called when player presses button
    public void Interact()
    {
        if (interactableObject == null)
        {
            return;
        }

        interactableObject.Interact();
        interactableObject = null;
    }

    // display text to show button input for interactions
    void TogglePopupText(bool displayText)
    {
        if(textPopup == null)
        {
            return;
        }

        // check if the text is already on
        if(displayText)
        {
            textPopup.SetActive(true);
            isTextOn = true;
        }
        else if(!displayText && isTextOn)
        {
            textPopup.SetActive(false);
            isTextOn = false;
        }
    }

    void SetPopupText(string text)
    {
        if (textPopup == null || textPopupMesh == null)
        {
            return;
        }

        textPopupMesh.text = text;
    }
}
