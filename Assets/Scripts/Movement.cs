using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float Speed = 8f;
    [SerializeField] private float Force = 7f;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private LayerMask layer;
    [SerializeField] private Camera cameraTransform;
    [SerializeField] private bool OnGround;
    [SerializeField] private float Range;
    private float inputH;
    private float inputV;
    private bool inputJ;
    private AudioSource moveAudioSource;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        moveAudioSource = gameObject.AddComponent<AudioSource>();
        moveAudioSource.clip = AudioManager.Instance.GetSFXClip(3);
        moveAudioSource.loop = false;
        moveAudioSource.playOnAwake = false;
    }
    private void Update()
    {
        inputH = Input.GetAxis("Horizontal");
        inputV = Input.GetAxis("Vertical");
        inputJ = Input.GetButtonDown("Jump");

        if (inputH != 0 || inputV != 0)
        {
            Mover();
            ControlarAudioMovimiento(true);
        }
        else
        {
            ControlarAudioMovimiento(false);
        }

        DetectionGround();

        if (inputJ && OnGround)
        {
            Jump();
        }
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

    void ControlarAudioMovimiento(bool estaMoviendose)
    {
        if (estaMoviendose)
        {
            if (!moveAudioSource.isPlaying)
            {
                moveAudioSource.pitch = Random.Range(0.95f, 1.05f);
                moveAudioSource.Play();
            }
        }
        else
        {
            if (moveAudioSource.isPlaying)
            {
                moveAudioSource.Stop();
            }
        }
    }
}
