using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float Speed = 8f;
    [SerializeField] private float Force = 7f;
    [SerializeField] private Rigidbody rb;
    public LayerMask layer;
    [SerializeField] private Camera cameraTransform;
    [SerializeField] private bool OnGround;
    [SerializeField] private float Horizontal;
    [SerializeField] private float Vertical;
    [SerializeField] private float Range;
    private float inputH;
    private float inputV;
    private bool inputJ;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>(); 
    }
    private void Update()
    {
        inputH = Input.GetAxis("Horizontal");
        inputV = Input.GetAxis("Vertical");
        inputJ = Input.GetButtonDown("Jump");
        DetectionGround();
        if (inputJ && OnGround)
        {
            Jump();
        }
        Mover();
    }
    public void DetectionGround()
    {
        RaycastHit hit;
        OnGround = Physics.Raycast(transform.position, Vector3.down, out hit, Range, layer);
    }
    void Jump()
    {
        rb.AddForce(Vector3.up * Force, ForceMode.Impulse);
    }
    void Mover()
    {
        Vector3 cameraForward = cameraTransform.transform.forward;
        Vector3 cameraRight = cameraTransform.transform.right;

        cameraForward.y = 0f;
        cameraRight.y = 0f;
        cameraForward.Normalize();
        cameraRight.Normalize();

        Vector3 movementDirection = cameraRight * inputH + cameraForward * inputV;
        Vector3 targetVelocity = movementDirection * Speed;
        targetVelocity.y = rb.linearVelocity.y;

        rb.linearVelocity = targetVelocity;

  
    }
}
