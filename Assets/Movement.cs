using UnityEngine;

public class Movement : MonoBehaviour
{
    public float Acceleration = 5f;
    protected float m_MovementSmoothing = 0.05f;


    protected Collider2D _collider;
    protected Rigidbody2D _rigidBody;
    protected WeaponHandler _weaponHandler;

    protected bool _isMoving = false;


    public Vector2 _inputDirection;
    protected Vector2 m_velocity;
    protected Vector2 _targetVelocity;


        




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        _collider = GetComponent<Collider2D>();
        _rigidBody = GetComponent<Rigidbody2D>();
        _weaponHandler = GetComponent<WeaponHandler>();

    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
        HandleMovement();
        HandleRotation();

    }

    protected virtual void HandleInput()
    {

    }
        
    protected virtual void HandleMovement()
    {
        if(_rigidBody == null || _collider == null)
            return;
        

        Vector2 targetVelocity = Vector2.zero;
        targetVelocity = new Vector2(_inputDirection.x * Acceleration, _inputDirection.y * Acceleration);

        _rigidBody.linearVelocity = Vector2.SmoothDamp(_rigidBody.linearVelocity, targetVelocity, ref m_velocity, m_MovementSmoothing);

        _isMoving = targetVelocity.x != 0 || targetVelocity.y != 0;

        _targetVelocity = targetVelocity;
    }

    protected virtual void HandleRotation()
    {
        if (_inputDirection == Vector2.zero)
            return;

        transform.rotation = Quaternion.LookRotation(Vector3.forward, upwards: (_targetVelocity));
    }

    
}
