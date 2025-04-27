using UnityEngine;

public class ZigzagMovement : EnemyMovement
{
    public float zigzagSpeed = 3f;
    public float maxZigzagAmount = 5f;
    public float forwardSpeed = 2f;

    protected override void HandleInput()
    {
        if (target == null) return;

        Vector3 toPlayer = (target.position - transform.position).normalized;
        float distanceToPlayer = Vector3.Distance(transform.position, target.position);

        // Adjust the zigzag movement to be independent of Time.timeScale
        float zigzagAmount = Mathf.Clamp(distanceToPlayer / 5, 0.2f, maxZigzagAmount);

        // Get perpendicular direction (for zigzagging)
        Vector3 perpendicular = new Vector3(-toPlayer.y, toPlayer.x, 0);

        // Apply zigzag movement using sine wave and take time scale into account
        Vector3 zigzagOffset = perpendicular * Mathf.Sin(Time.time * zigzagSpeed) * zigzagAmount;

        // Ensure movement always has a strong forward force
        _moveDirection = (toPlayer * forwardSpeed + zigzagOffset).normalized;

        // Apply movement direction
        _inputDirection = new Vector2(_moveDirection.x, _moveDirection.y);

        Debug.DrawRay(transform.position, _moveDirection * 2, Color.green);
    }
}




