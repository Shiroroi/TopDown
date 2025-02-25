using UnityEngine;

public class ZigzagMovement : EnemyMovement
{
    public float zigzagSpeed = 3f;  // How fast it zigzags
    public float maxZigzagAmount = 5f;  // Max zigzag width when far away
    public float forwardSpeed = 2f;  // Forward movement speed

    protected override void HandleInput()
    {
        if (target == null) return;

        // Direction toward the player
        Vector3 toPlayer = (target.position - transform.position).normalized;
        float distanceToPlayer = Vector3.Distance(transform.position, target.position);

        // Reduce zigzag amount as enemy gets closer
        float zigzagAmount = Mathf.Clamp(distanceToPlayer / 5, 0.2f, maxZigzagAmount);

        // Get perpendicular direction (for zigzagging)
        Vector3 perpendicular = new Vector3(-toPlayer.y, toPlayer.x, 0);

        // Apply zigzag movement using sine wave
        Vector3 zigzagOffset = perpendicular * Mathf.Sin(Time.time * zigzagSpeed) * zigzagAmount;

        // Ensure movement always has a strong forward force
        _moveDirection = (toPlayer * forwardSpeed + zigzagOffset).normalized;

        // Apply movement direction
        _inputDirection = new Vector2(_moveDirection.x, _moveDirection.y);

        Debug.DrawRay(transform.position, _moveDirection * 2, Color.green); // Debugging
    }
}



