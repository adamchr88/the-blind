
using UnityEngine;

public class FootstepSounds : MonoBehaviour
{
    public AudioClip[] footsteps;
    public float stepInterval = 0.5f;

    private AudioSource audioSource;
    private CharacterController controller;
    private float stepTimer = 0f;
    private int currentStep = 0;
    private bool wasMoving = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        bool isMoving = controller.isGrounded &&
                        controller.velocity.magnitude > 0.1f;

        if (isMoving)
        {
            stepTimer += Time.deltaTime;

            if (stepTimer >= stepInterval && footsteps.Length > 0)
            {
                audioSource.PlayOneShot(footsteps[currentStep]);

                currentStep++;

                if (currentStep >= footsteps.Length)
                {
                    currentStep = 0;
                }

                stepTimer = 0f;
            }
        }
        else
        {
            stepTimer = 0f;

            if (wasMoving)
            {
                audioSource.Stop();
            }
        }

        wasMoving = isMoving;
    }
}
