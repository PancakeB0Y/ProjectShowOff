using UnityEditor.UIElements;
using UnityEngine;

public class StatueController : MonoBehaviour
{
    GameObject player;
    Collider statueCollider;

    float speed = 2.0f;

    bool canMove = true;

    string lightTag = "Light";

    void Start()
    {
        player = FindFirstObjectByType<PlayerController>().gameObject;
        statueCollider = GetComponent<Collider>();
    }

    void Update()
    {
        if (!canMove)
        {
            return;
        }

        transform.LookAt(player.transform.position);

        Vector3 moveDir = (player.transform.position - transform.position).normalized;

        transform.position += moveDir * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(lightTag))
        {
            canMove = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag(lightTag))
        {
            canMove = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(lightTag))
        {
            canMove = true;
        }
    }
}
