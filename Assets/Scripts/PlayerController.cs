using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Acceleration")]
    public float accelerationFactor;
    public float maxSpeed;

    [Header("Rotation")]
    public float originalRotationAngle;
    public float rotationAngle;
    public float maxRotationAngle;
    public float rotationSpeed;
    public float rotationRecoveryFactor;
    public float rotationFactor;
    public float horizontalForceFactor;
    public float wheelRotationSpeed;

    [Header("Projectile")]
    public float projectileCooldown;
    
    [Header("Camera")]
    public Vector3[] cameraOffsets;
    public Vector3 cameraOffset;
    
    [Header("References")]
    [SerializeField] private Transform[] frontWheels;
    [SerializeField] private Transform[] backWheels;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private ProjectileSpawner projectileSpawner;
    [SerializeField] private UIManager uiManager;
    
    private Rigidbody _rigidbody;
    private float _projectileCooldownTimer;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.centerOfMass = new Vector3(0f, -0.5f, -0.2f);
        _projectileCooldownTimer = 0f;
        
        cameraOffset = cameraOffsets[0];
    }
    
    private void FixedUpdate()
    {
        if (!PlayManager.Instance.isGameActive) return;
        
        var verticalInput = Input.GetAxis("Vertical");
        var horizontalInput = Input.GetAxis("Horizontal");
        var forwardVelocity = Vector3.Dot(_rigidbody.velocity, transform.forward);
        
        // Accelerate
        var accelerationRate =
            Mathf.Sqrt(Mathf.Clamp01((maxSpeed - (verticalInput >= 0 ? forwardVelocity : -forwardVelocity)) /
                                     maxSpeed));
        _rigidbody.AddForce(verticalInput * accelerationRate * accelerationFactor * Time.fixedDeltaTime *
                            transform.forward);
        
        // Rotate
        if (horizontalInput != 0)
        {
            rotationAngle = Mathf.MoveTowards(rotationAngle, horizontalInput * maxRotationAngle,
                rotationSpeed * Time.fixedDeltaTime);
        }
        else
        {
            rotationAngle = Mathf.MoveTowards(rotationAngle, 0f,
                rotationSpeed * rotationRecoveryFactor * Time.fixedDeltaTime);
        }

        _rigidbody.angularVelocity = forwardVelocity * rotationAngle * rotationFactor * transform.up;
        _rigidbody.AddForce(rotationAngle * horizontalForceFactor * Time.fixedDeltaTime *
                            transform.right);
        
        // Front wheel rotation
        foreach (var wheel in frontWheels)
        {
            var wheelRotation = wheel.localEulerAngles;

            wheelRotation.z += verticalInput * wheelRotationSpeed * Time.fixedDeltaTime;
            wheelRotation.y = originalRotationAngle + rotationAngle;
            
            wheel.localEulerAngles = wheelRotation;
        }

        // Back wheel rotation
        foreach (var wheel in backWheels)
        {
            var wheelRotation = wheel.localEulerAngles;
            wheelRotation.z += verticalInput * wheelRotationSpeed * Time.fixedDeltaTime;
            wheel.localEulerAngles = wheelRotation;
        }
    }

    private void Update()
    {
        // Update speed text
        var forwardSpeed = Mathf.Abs(Vector3.Dot(_rigidbody.velocity, transform.forward));
        uiManager.SetSpeedText(forwardSpeed);
        
        if (!PlayManager.Instance.isGameActive) return;
        
        // Projectile
        if (Input.GetKeyDown(KeyCode.Space) && _projectileCooldownTimer <= 0f)
        {
            projectileSpawner.SpawnProjectile();
            _projectileCooldownTimer = projectileCooldown;
        }
        
        if (_projectileCooldownTimer > 0f) _projectileCooldownTimer -= Time.deltaTime;
    }
    
    private void LateUpdate()
    {
        // Camera control
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            cameraOffset = cameraOffsets[0];
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            cameraOffset = cameraOffsets[1];
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            cameraOffset = cameraOffsets[2];
        }
        
        cameraTransform.position = transform.TransformPoint(cameraOffset);
        cameraTransform.LookAt(transform);
    }
}
