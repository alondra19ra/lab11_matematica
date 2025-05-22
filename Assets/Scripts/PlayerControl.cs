using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float jumpForce;
    [SerializeField] private float speed;
    [SerializeField] private float descendForce;
    [SerializeField] private float horizontalPushForce;
    [SerializeField] private float verticalPushForce;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private GameObject gameOverPanel;
    private Rigidbody _rigidbody;
    private bool crashed = false;
    private float stunTime = 0;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
    }

    void Update()
    {
        CheckOutOfBounds();
    }

    void FixedUpdate()
    {
        HandleStun();
        PlayerRotation();
    }

    private void CheckOutOfBounds()
    {
        if (transform.position.y < -15 || transform.position.y > 16 || transform.position.x <= -27)
        {
            EndGame();
        }
    }

    private void HandleStun()
    {
        if (crashed)
        {
            if (stunTime < 3)
            {
                stunTime += Time.fixedDeltaTime;
                _rigidbody.linearVelocity = new Vector3(0, _rigidbody.linearVelocity.y, _rigidbody.linearVelocity.z);
            }
            else
            {
                stunTime = 0;
                crashed = false;
            }
        }
    }

    public void ReadJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    public void ReadDescend(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _rigidbody.AddForce(Vector3.down * descendForce, ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("LowObstacle"))
        {
            ApplyCrashForce(Vector3.up);
        }
        else if (collision.CompareTag("HighObstacle"))
        {
            ApplyCrashForce(Vector3.down);
        }
    }

    private void ApplyCrashForce(Vector3 verticalDirection)
    {
        _rigidbody.AddForce(Vector3.left * horizontalPushForce, ForceMode.Impulse);
        _rigidbody.AddForce(verticalDirection * verticalPushForce, ForceMode.Impulse);
        crashed = true;
    }

    private void PlayerRotation()
    {
        float rotationAngle = Mathf.Clamp(-_rigidbody.linearVelocity.y * rotationSpeed, -65f, 65f);
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, rotationAngle);
        _rigidbody.MoveRotation(Quaternion.Slerp(_rigidbody.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime));
    }

    private void EndGame()
    {
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);
    }
}



