using UnityEngine;
using UnityEngine.UIElements;

public class EnemyMovement : Movement
{
    public float RotationDegree;
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
        if (target == null) return;

        Vector3 direction = (target.position - transform.position).normalized;

        // Ensure enemy movement works with Time.timeScale changes
        float speedAdjustment = Time.timeScale == 1f ? 1f : Time.timeScale;

        _moveDirection = direction * speedAdjustment;
        _inputDirection = new Vector2(_moveDirection.x, _moveDirection.y);
    }







}
