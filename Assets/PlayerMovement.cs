using UnityEngine;
using System.Collections;

public class PlayerMovement : Movement
{
    public float moveSpeed = 5f;  // Regular movement speed
    public float dashSpeed = 20f;  // Dash speed
    public float dashDuration = 0.2f;  // Duration of the dash in seconds
    public float dashCooldown = 1f;  // Cooldown before the next dash

    private bool isDashing = false;  // Whether the player is currently dashing
    private float dashTime = 0f;  // Timer to track dash duration
    private float dashCooldownTime = 0f;  // Timer to track dash cooldown

    public AudioClip dashSound;  // Dash sound effect
    private AudioSource audioSource;

    protected override void Start()
    {
        base.Start();
        audioSource = GetComponent<AudioSource>();  // Get the AudioSource component on the player
    }
    protected override void HandleInput()
    {
        if (isDashing) return;
        _inputDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashCooldownTime <= 0f)
        {
            Dash();  // Start the dash if the cooldown is finished
        }
    }

    private void Dash()
    {
        isDashing = true;
        dashTime = dashDuration;

        // Apply the dash speed in the direction the player is moving
        Vector2 dashDirection = _rigidBody.linearVelocity.normalized;

        // If no movement input, dash in the direction the player is facing (default to "up" or camera direction)
        if (dashDirection.magnitude == 0)
        {
            dashDirection = transform.up;  // Dash in the direction the player is facing (default to "up")
        }

        // Apply dash speed
        _rigidBody.linearVelocity = dashDirection * dashSpeed;
        // Play the dash sound
        if (dashSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(dashSound);  // Play dash sound
        }

        // Start cooldown timer after dash duration
        StartCoroutine(EndDash());
    }

    // Coroutine to end the dash after the specified duration
    private IEnumerator EndDash()
    {
        yield return new WaitForSeconds(dashTime);

        // After dash, stop the dashing movement
        _rigidBody.linearVelocity = Vector2.zero;  // Stop moving

        // Set isDashing to false and start the cooldown timer
        isDashing = false;

        // Start cooldown time before the next dash
        dashCooldownTime = dashCooldown;

        // Reset cooldown after the dash
        while (dashCooldownTime > 0f)
        {
            dashCooldownTime -= Time.deltaTime;  // Decrease the cooldown
            yield return null;
        }
    }

    protected override void HandleRotation()
    {
        if (_weaponHandler == null || _weaponHandler.CurrentWeapon == null)
        {
            base.HandleRotation();
            return;
        }

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        mousePos = new Vector3(mousePos.x, mousePos.y, transform.position.z);

        Vector2 direction = mousePos - transform.position;

        float angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - 90f;

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}