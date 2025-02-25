using UnityEngine;

public class CirclingMovement : EnemyMovement
{
    public float spiralSpeed = 2f;  // How fast it rotates around the movement path
    public float approachSpeed = 2f;  // Speed toward the player
    public float maxRadius = 3f;  // Maximum circling radius
    public float shrinkRate = 0.98f;  // How quickly the spiral tightens

    private float angle = 0f;  // Angle for circular motion
    private float currentRadius;  // Dynamic radius that shrinks over time

    protected override void Start()
    {
        base.Start();
        currentRadius = maxRadius;  // Start with the max radius
    }

    protected override void HandleInput()
    {
        if (target == null) return;

        // Direction toward the player
        Vector3 toPlayer = (target.position - transform.position).normalized;

        // Circular motion
        angle += spiralSpeed * Time.deltaTime;
        Vector3 circularOffset = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * maxRadius;

        // Move towards the player while spiraling
        Vector3 movementDirection = (toPlayer * approachSpeed * 1.5f) + (circularOffset * 0.4f);
        
        // Apply movement
        _inputDirection = new Vector2(movementDirection.x, movementDirection.y).normalized;

        Debug.DrawRay(transform.position, _inputDirection * 2, Color.red); // Debugging
    }
}







