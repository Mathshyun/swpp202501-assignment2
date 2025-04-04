using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float accelerationRate;
    public float maxSpeed;
    public float rotationRate;

    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Vector3 cameraPositionOffset;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    
    private void Update()
    {
        var verticalInput = Input.GetAxis("Vertical");
        var horizontalInput = Input.GetAxis("Horizontal");
        var forwardVelocity = Vector3.Dot(_rigidbody.velocity, transform.forward);
        
        // Accelerate
        _rigidbody.AddForce(verticalInput * accelerationRate * Time.deltaTime * transform.forward);
        
        // Rotate
        _rigidbody.angularVelocity = horizontalInput * rotationRate * forwardVelocity * transform.up;
        _rigidbody.AddForce(horizontalInput * rotationRate * forwardVelocity * Time.deltaTime * transform.right);
        
        Debug.Log($"forwardVelocity: {forwardVelocity}, " +
                  $"angularVelocity: {_rigidbody.angularVelocity}");
    }

    private void LateUpdate()
    {
        cameraTransform.position = transform.TransformPoint(cameraPositionOffset);
        cameraTransform.LookAt(transform);
    }
}
