using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float accelerationFactor;
    public float rotationFactor;
    public float horizontalForceFactor;
    public float maxSpeedFactor;
    
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Vector3[] cameraOffsets;
    [SerializeField] private Vector3 cameraOffset;
    
    private Rigidbody _rigidbody;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.centerOfMass = new Vector3(0f, -0.5f, -0.2f);
        cameraOffset = cameraOffsets[0];
    }
    
    private void FixedUpdate()
    {
        var verticalInput = Input.GetAxis("Vertical");
        var horizontalInput = Input.GetAxis("Horizontal");
        var forwardVelocity = Vector3.Dot(_rigidbody.velocity, transform.forward);
        
        // Accelerate
        var accelerationRate =
            Mathf.Sqrt(Mathf.Clamp01((maxSpeedFactor - (verticalInput >= 0 ? forwardVelocity : -forwardVelocity)) /
                                     maxSpeedFactor));
        _rigidbody.AddForce(verticalInput * accelerationRate * accelerationFactor * Time.fixedDeltaTime *
                            transform.forward);
        
        // Rotate
        _rigidbody.angularVelocity = horizontalInput * forwardVelocity * rotationFactor * transform.up;
        _rigidbody.AddForce(horizontalInput * forwardVelocity * horizontalForceFactor * Time.fixedDeltaTime *
                            transform.right);
        
        // Debug.Log($"forwardVelocity: {forwardVelocity}, " +
        //           $"angularVelocity: {_rigidbody.angularVelocity}");
    }
    
    private void LateUpdate()
    {
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
