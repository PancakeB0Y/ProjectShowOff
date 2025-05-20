using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float moveSpeed;

    Rigidbody rb;

    Vector3 moveInput;

    LanternController lantern;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lantern = transform.parent.GetComponentInChildren<LanternController>();
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;
    }

    void FixedUpdate()
    {
        Vector3 moveDirection = moveInput.z * transform.forward + moveInput.x * transform.right;
        moveDirection = moveDirection * moveSpeed;
        rb.linearVelocity = new Vector3(moveDirection.x, rb.linearVelocity.y, moveDirection.z);
    }

    //Detect player collision with wind and inform lantern
    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<WindSphere>(out WindSphere wind))
        {
            return;
        }

        if (lantern == null)
        {
            lantern = transform.parent.GetComponentInChildren<LanternController>();
        }

        if (lantern != null)
        {
            lantern.HandleWindCollision();
        }
    }
}
