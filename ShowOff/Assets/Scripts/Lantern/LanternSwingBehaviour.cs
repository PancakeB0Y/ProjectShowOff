using UnityEngine;

public class LanternSwingBehaviour : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform playerRoot; // Assign the Player or PlayerWithLanternSwing

    [Header("Swing Settings")]
    [SerializeField] private float swingStrength = 60f;
    [SerializeField] private float rotationInfluence = 2f;
    [SerializeField] private float damping = 2.5f;
    [SerializeField] private float maxSwingAngle = 30f;


    private Vector3 lastPlayerPosition;
    private float lastPlayerYRotation;

    private Vector3 swingVelocity;
    private Vector3 swingOffset = Vector3.zero;

    void Awake()
    {
    }

    void Start()
    {
        if (playerRoot == null)
        {
            Debug.LogError("LanternSwingBehaviour: Missing player root reference.");
            enabled = false;
            return;
        }

        lastPlayerPosition = playerRoot.position;
        lastPlayerYRotation = playerRoot.eulerAngles.y;
    }

    void Update()
    {
        if(Time.timeScale == 0)
        {
            return;
        }

        float deltaTime = Time.deltaTime;
        if (deltaTime == 0) { //is equal to 0 after setting Time.TimeScale = 0
            deltaTime = Time.unscaledDeltaTime;
        }

        // Movement Influence
        Vector3 playerDelta = (playerRoot.position - lastPlayerPosition) / deltaTime;
        Vector3 localDelta = playerRoot.InverseTransformDirection(playerDelta);
        Vector3 movementSwing = new Vector3(-localDelta.x, 0f, localDelta.z) * swingStrength;

        // Rotation Influence
        float currentYRotation = playerRoot.eulerAngles.y;
        float rotationDelta = Mathf.DeltaAngle(lastPlayerYRotation, currentYRotation) / deltaTime;
        Vector3 rotationSwing = new Vector3(-rotationDelta * rotationInfluence, 0f, 0f);

        // Combine Both
        Vector3 targetOffset = movementSwing + rotationSwing;

        // Spring-like Swing Smoothing
        Vector3 acceleration = (targetOffset - swingOffset) * swingStrength;
        swingVelocity += acceleration * deltaTime;
        swingVelocity *= Mathf.Exp(-damping * deltaTime);
        swingOffset += swingVelocity * deltaTime;

        swingOffset = Vector3.ClampMagnitude(swingOffset, maxSwingAngle);

        // Apply Rotation to Lantern
        transform.localRotation = Quaternion.Euler(swingOffset.z, 0f, swingOffset.x);

        // Store Previous Frame
        lastPlayerPosition = playerRoot.position;
        lastPlayerYRotation = currentYRotation;
    }
}
