using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float dashSpeed = 4f;
    [SerializeField] private float dashCoolDown = .5f;
    [SerializeField] private TrailRenderer trailRenderer;
    [SerializeField] private AudioSource footSound;
    [SerializeField] private AudioSource dashSound;

    private bool facingLeft = false;
    public bool FacingLeft { get { return facingLeft; } set { facingLeft = value; } }
    public static PlayerController Instance;

    private PlayerControls playerControls;
    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private KnockBack knockBack;
    private bool isDashing = false;
    private bool isMoving;


    void Awake()
    {
        Instance = this;
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        knockBack = GetComponent<KnockBack>();
    }

    public void Start()
    {
        playerControls.Combat.Dash.started += _ => Dash();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    void Update()
    {
        PlayerInput();
    }

    private void FixedUpdate()
    {
        AdjustPlayerFacingDirection();
        Move();
    }

    private void Move()
    {
        if (knockBack.gettingKnockBack)
            return;
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
    }

    private void PlayerInput()
    {
        movement = playerControls.Movement.Move.ReadValue<Vector2>();
        animator.SetFloat("moveX", movement.x);
        animator.SetFloat("moveY", movement.y);
        if (isMoving && movement == Vector2.zero)
        {
            footSound.Stop();
        }
        if (!isMoving && movement != Vector2.zero)
        {
            footSound.Play();
        }
        isMoving = movement != Vector2.zero;
    }

    private void AdjustPlayerFacingDirection()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

        if (mousePos.x < playerScreenPoint.x)
        {
            spriteRenderer.flipX = true;
            facingLeft = true;
        }
        else
        {
            spriteRenderer.flipX = false;
            facingLeft = false;
        }
    }

    private void Dash()
    {
        if (!isDashing) StartCoroutine(IEDash());
    }

    private IEnumerator IEDash()
    {
        dashSound.Play();
        isDashing = true;
        moveSpeed *= dashSpeed;
        trailRenderer.emitting = true;
        float dashTime = .2f;
        yield return new WaitForSeconds(dashTime);
        moveSpeed /= dashSpeed;
        trailRenderer.emitting = false;
        yield return new WaitForSeconds(dashCoolDown);
        isDashing = false;
    }
}
