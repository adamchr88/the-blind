
using UnityEngine;
using UnityEngine.InputSystem;

public class KeyPickup : MonoBehaviour
{
    public Camera playerCamera;
    public float pickupDistance = 2f;

    public bool hasKey = false;

    void Update()
    {
        if (hasKey || playerCamera == null)
            return;

        if (Keyboard.current != null &&
            Keyboard.current.eKey.wasPressedThisFrame)
        {
            Ray ray = new Ray(
                playerCamera.transform.position,
                playerCamera.transform.forward
            );

            if (Physics.Raycast(ray, out RaycastHit hit, pickupDistance))
            {
                if (hit.collider.CompareTag("BasementKey"))
                {
                    hasKey = true;
                    hit.collider.gameObject.SetActive(false);

                    Debug.Log("Basement key collected!");
                }
            }
        }
    }
}
