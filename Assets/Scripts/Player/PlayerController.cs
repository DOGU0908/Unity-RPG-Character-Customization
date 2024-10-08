using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    // movement
    [SerializeField] private float walkSpeed = 2.0f;
    [SerializeField] private float runSpeed = 5.0f;
    private PlayerControls _playerControls;
    private Rigidbody _playerRigidbody;
    private Vector3 _movement;
    private bool _isRunning;
    
    // animation
    [SerializeField] private Animator animator;
    private static readonly int IsMoveHash = Animator.StringToHash("IsMove");
    private static readonly int IsRunHash = Animator.StringToHash("IsRun");
    
    // sprite control
    [SerializeField] private SpriteManager spriteManager;
    
    // enemy encounter
    private bool _isCheckBattleEncounter;
    private Vector3 _lastPosition;
    private float _distanceTraveled = 0.0f;
    private const float EncounterCheckDistance = 5.0f;
    private const float EncounterProbability = 0.2f;
    
    // interact
    private const float InteractRange = 2.0f;
    private const int NPCLayer = 1 << 6;
    private readonly Collider[] _interactColliders = new Collider[1];
    
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
        _lastPosition = transform.position;

        _isCheckBattleEncounter = FieldManager.Instance.IsBattleEncounterEnabled;
    }
    
    private void Update()
    {
        // movement
        Vector2 input = _playerControls.Player.PlayerMovement.ReadValue<Vector2>();
        _isRunning = _playerControls.Player.Run.IsPressed();

        _movement = new Vector3(input.x, 0.0f, input.y).normalized;
        
        animator.SetBool(IsMoveHash, _movement != Vector3.zero);
        animator.SetBool(IsRunHash, _isRunning);

        switch (input.x)
        {
            case > 0:
                spriteManager.Flip(false);
                break;
            case < 0:
                spriteManager.Flip(true);
                break;
        }

        if (_isCheckBattleEncounter)
        {
            CheckBattleEncounter();
        }

        if (_playerControls.Player.Interact.WasPressedThisFrame() && !DialogManager.Instance.IsOpened)
        {
            if (Physics.OverlapSphereNonAlloc(transform.position, InteractRange, _interactColliders, NPCLayer) > 0)
            {
                foreach (Collider interactCollider in _interactColliders)
                {
                    if (interactCollider.TryGetComponent(out NPC npc))
                    {
                        npc.Interact();
                    }
                }

                for (int i = 0; i < _interactColliders.Length; ++i)
                {
                    _interactColliders[i] = null;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        float speed = _isRunning ? runSpeed : walkSpeed;
        _playerRigidbody.MovePosition(transform.position + _movement * (speed * Time.fixedDeltaTime));
    }

    private void CheckBattleEncounter()
    {
        _distanceTraveled += Vector3.Distance(transform.position, _lastPosition);
        _lastPosition = transform.position;

        if (_distanceTraveled >= EncounterCheckDistance)
        {
            _distanceTraveled = 0.0f;

            if (Random.Range(0.0f, 1.0f) < EncounterProbability)
            {
                Debug.Log("Battle Encounter");
            }
        }
    }
}
