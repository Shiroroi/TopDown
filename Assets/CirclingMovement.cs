using UnityEngine;

public class CirclingMovement : EnemyMovement
{
    public float spiralSpeed = 2f;
    public float approachSpeed = 2f;
    public float maxRadius = 3f;
    public float shrinkRate = 0.98f;

    private float angle = 0f;
    private float currentRadius;

    protected override void Start()
    {
        base.Start();
        currentRadius = maxRadius;
    }

    protected override void HandleInput()
    {
        if (target == null) return;

        Vector3 toPlayer = (target.position - transform.position).normalized;

        // Apply time scale to spiral speed for smooth movement when time is slowed
        angle += spiralSpeed * Time.deltaTime * Time.timeScale;

        // Circular motion
        Vector3 circularOffset = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * maxRadius;

        // Move towards the player while spiraling
        Vector3 movementDirection = (toPlayer * approachSpeed * 1.5f) + (circularOffset * 0.4f);

        // Apply movement
        _inputDirection = new Vector2(movementDirection.x, movementDirection.y).normalized;

        Debug.DrawRay(transform.position, _inputDirection * 2, Color.red);
    }
}