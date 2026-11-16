
using UnityEngine;
using UnityEngine.InputSystem;

public class BasementDoor : MonoBehaviour
{
    public KeyPickup keyPickup;
    public Camera playerCamera;
    public float interactionDistance = 4f;
    public AudioSource doorSound;

    private Animator doorAnimator;
    private bool doorOpened = false;

    void Start()
    {
        doorAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        // Stop checking once the door is open
        if (doorOpened)
            return;

        // Check if E is pressed
        if (Keyboard.current == null ||
            !Keyboard.current.eKey.wasPressedThisFrame)
            return;

        if (playerCamera == null || keyPickup == null)
            return;

        // Raycast from the player's camera
        Ray ray = new Ray(
            playerCamera.transform.position,
            playerCamera.transform.forward
        );

        if (Physics.Raycast(ray, out RaycastHit hit, interactionDistance))
        {
            // Check if looking at the door or its children
            if (hit.collider.transform.IsChildOf(transform))
            {
                if (keyPickup.hasKey)
                {
                    doorOpened = true;

                    // Play opening animation
                    doorAnimator.SetTrigger("Open");

                    // Play opening sound
                    if (doorSound != null)
                    {
                        doorSound.Play();
                        Debug.Log("Door sound triggered!");
                    }
                    else
                    {
                        Debug.LogWarning("Door AudioSource is missing");
                    }

                    Debug.Log("Basement door unlocked!");
                }
                else
                {
                    Debug.Log("Door locked! Find the basement key.");
                }
            }
        }
    }
}
