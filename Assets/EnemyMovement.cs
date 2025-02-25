using UnityEngine;
using UnityEngine.UIElements;

public class EnemyMovement : Movement
{
    public float _movementSpeed = 2f;
    public float RotationDegree = 22.5f;
    protected Transform target;
    protected Vector2 _moveDirection;
   protected override void Start()
    {
        base.Start();
        target = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    protected override void HandleInput()
    {

        
        if (target == null)
            return;
        
        Vector3 direction = (target.position - transform.position).normalized;
        _moveDirection = direction;
        
        Debug.DrawRay(transform.position, _moveDirection, Color.yellow);

        _inputDirection = new Vector2(_moveDirection.x, _moveDirection.y);


        
    }

    




}
