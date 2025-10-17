using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class CameraRotation : MonoBehaviour
{
    #region Properties
    private Vector2 movementCamera;
    [SerializeField] private float velocity;

    [SerializeField] private float minVerticalAngle = -80f;
    [SerializeField] private float maxVerticalAngle = 80f;

    [SerializeField] private float interactionDistance = 3f;
    [SerializeField] private LayerMask layer;
    private bool takeObject = false;
    #endregion
    #region Unity Mehods
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void Update()
    {
       

    }

    #endregion
    private void MovementCamera(Vector2 value)
    {
        movementCamera.x += value.x * velocity * Time.deltaTime;
        movementCamera.y += value.y * velocity * Time.deltaTime;

        movementCamera.y = Mathf.Clamp(movementCamera.y, minVerticalAngle, maxVerticalAngle);

        transform.localRotation = Quaternion.Euler(-movementCamera.y, movementCamera.x, 0f);
    }
    public static event Action<Vector2> movementCameraA;
    public void MovementCamera(InputAction.CallbackContext context)
    {
       // movementCameraA?.Invoke(context.ReadValue<Vector2>());
        MovementCamera(context.ReadValue<Vector2>());
    }
}
