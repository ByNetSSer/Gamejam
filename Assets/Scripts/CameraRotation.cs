using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraRotation : MonoBehaviour
{
    #region Properties
    private Vector2 movementCamera;
    [SerializeField] private static float velocity = 2f;
    [SerializeField] private float minVerticalAngle = -80f;
    [SerializeField] private float maxVerticalAngle = 80f;

    [SerializeField] private float interactionDistance = 3f;
    [SerializeField] private LayerMask layer;
    private bool takeObject = false;
    #endregion

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        float savedSensitivity = PlayerPrefs.GetFloat("MouseSensitivity", velocity);
        velocity = Mathf.Clamp(savedSensitivity, 0.1f, 5f);
    }

    private void MovementCamera(Vector2 value)
    {
        float finalVelocity = velocity; 

        movementCamera.x += value.x * finalVelocity * Time.deltaTime;
        movementCamera.y += value.y * finalVelocity * Time.deltaTime;

        movementCamera.y = Mathf.Clamp(movementCamera.y, minVerticalAngle, maxVerticalAngle);
        transform.localRotation = Quaternion.Euler(-movementCamera.y, movementCamera.x, 0f);
    }

    public static event Action<Vector2> movementCameraA;
    public void MovementCamera(InputAction.CallbackContext context)
    {
        MovementCamera(context.ReadValue<Vector2>());
    }

    public static void SetSensitivity(float newVelocity)
    {
        velocity = Mathf.Clamp(newVelocity, 0.1f, 5f); 
    }
}
