using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // movement
    [SerializeField] private int speed;
    private PlayerControls _playerControls;
    private Rigidbody _playerRigidbody;
    private Vector3 _movement;
    
    // animation
    [SerializeField] private Animator animator;
    private readonly int _isMoveHash = Animator.StringToHash("IsMove");
    
    // sprite control
    [SerializeField] private SpriteManager spriteManager;
    
    private void Awake()
    {
        _playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        _playerControls.Enable();
    }

    private void OnDisable()
    {
        _playerControls.Disable();
    }

    private void Start()
    {
        _playerRigidbody = gameObject.GetComponent<Rigidbody>();
    }
    
    private void Update()
    {
        Vector2 input = _playerControls.Player.PlayerMovement.ReadValue<Vector2>();

        _movement = new Vector3(input.x, 0.0f, input.y).normalized;
        
        animator.SetBool(_isMoveHash, _movement != Vector3.zero);

        if (input.x < 0)
        {
            spriteManager.Flip(true);
        }
        else if (input.x > 0)
        {
            spriteManager.Flip(false);
        }
    }

    private void FixedUpdate()
    {
        _playerRigidbody.MovePosition(transform.position + _movement * (speed * Time.fixedDeltaTime));
    }
}