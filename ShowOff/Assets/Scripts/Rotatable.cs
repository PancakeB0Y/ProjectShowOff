using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Rotatable : MonoBehaviour
{
    [SerializeField] private InputAction pressed, axis;

    [SerializeField] private float rotationSpeed = 1;

    public float distanceFromCamera = 1.5f;
    private Vector2 rotation;

    private bool rotateAllowed = false;


    private void OnEnable()
    {
        pressed.Enable();
        axis.Enable();

        pressed.performed += _ => { StartCoroutine(Rotate()); };
        pressed.canceled += _ => { rotateAllowed = false; };

        axis.performed += context => { rotation = context.ReadValue<Vector2>(); };
    }

    private void OnDisable()
    {
        pressed.Disable();
        axis.Disable();

        pressed.performed -= _ => { StartCoroutine(Rotate()); };
        pressed.performed -= _ => { rotateAllowed = false; };

        axis.performed -= context => { rotation = context.ReadValue<Vector2>(); };

        StopAllCoroutines();
        rotateAllowed = false;
    }

    private IEnumerator Rotate()
    {
        rotateAllowed = true;

        while (rotateAllowed) {
            rotation *= rotationSpeed;
            transform.Rotate(Vector3.up, rotation.x, Space.World);
            transform.Rotate(-Camera.main.transform.right, rotation.y, Space.World);

            yield return null;
        }
    }
}
