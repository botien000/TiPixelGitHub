using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    public RuntimeAnimatorController controller;
    enum State
    {
        Idle, Run, Jump, WallJump, Fall, DoubleJump, Attack
    }
    [SerializeField] private float jumpForce;
    [SerializeField] private float speedRun;
    [SerializeField] private GameObject fire;
    [SerializeField] private float timeNoHit;
    [SerializeField] private Transform foot;

    private int heart;
    private SpriteRenderer spRdr;
    private float curTimeNoHit;
    private bool isHitted;
    private Vector2 prePos;
    private int jumpAgain;
    private bool isOnGround;
    private Rigidbody2D rgbody;
    private Animator animator;
    private State curState;
    private InputCtrler playerInput;
    private Vector2 movePlayer;

    private void OnEnable()
    {

        if (playerInput == null)
        {
            playerInput = new InputCtrler();
            playerInput.Player.Movement.started += OnMovement;
            playerInput.Player.Movement.performed += OnMovement;
            playerInput.Player.Movement.canceled += OnMovement;
            playerInput.Player.Attack.started += OnAttack;
            playerInput.Player.Attack.performed += OnAttack;
            playerInput.Player.Attack.canceled += OnAttack;
            playerInput.Player.Jump.started += OnJump;
            playerInput.Player.Jump.performed += OnJump;
            playerInput.Player.Jump.canceled += OnJump;
        }
        playerInput.Enable();
    }
    private void OnDisable()
    {
        if (playerInput != null)
        {
            playerInput.Disable();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        rgbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spRdr = GetComponent<SpriteRenderer>();
        prePos = transform.position;
        heart = 5;
    }
    void FixedUpdate()
    {
        if (isHitted)
        {
            curTimeNoHit += Time.fixedDeltaTime;
            if (curTimeNoHit >= timeNoHit)
            {
                curTimeNoHit = 0;
                foot.gameObject.SetActive(true);
                spRdr.color = Color.white;
                isHitted = false;
                StopAllCoroutines();
            }
        }
        if (movePlayer == Vector2.zero && isOnGround)
        {
            PlayAni(State.Idle);
            rgbody.velocity = new Vector2(movePlayer.x * speedRun, rgbody.velocity.y);
            return;
        }
        Move();
    }
    private void SetDirection()
    {

        if (movePlayer.x < 0)
        {
            transform.rotation = Quaternion.AngleAxis(180, Vector3.up);
        }
        else if (movePlayer.x > 0)
        {
            transform.rotation = Quaternion.AngleAxis(0, Vector3.zero);
        }
    }
    private void Move()
    {
        SetDirection();
        if (!isOnGround && jumpAgain == 2)
        {
            PlayAni(State.DoubleJump);
        }
        else if (!isOnGround && movePlayer.x != 0)
        {
            PlayAni(State.WallJump);
        }
        else if (!isOnGround && transform.position.y >= prePos.y)
        {
            PlayAni(State.Jump);
        }
        else if (isOnGround && movePlayer.x != 0)
        {
            PlayAni(State.Run);
        }
        else
        {
            PlayAni(State.Fall);
        }
        prePos = transform.position;
        rgbody.velocity = new Vector2(movePlayer.x * speedRun, rgbody.velocity.y);
    }
    private void OnAttack(InputAction.CallbackContext obj)
    {
        if (obj.started)
        {
            Fire firee = Instantiate(fire, transform.position, Quaternion.identity).GetComponent<Fire>();
            if (transform.rotation.y == 1 || transform.rotation.y == -1)
            {
                firee.SetDir(-1);
            }
            else
            {
                firee.SetDir(1);
            }
        }

    }

    private void OnMovement(InputAction.CallbackContext obj)
    {
        if (obj.started || obj.performed)
        {
            movePlayer = obj.ReadValue<Vector2>();
        }
        else
        {
            movePlayer = Vector2.zero;
        }
    }
    private void OnJump(InputAction.CallbackContext obj)
    {
        if (obj.started)
        {
            if (isOnGround || jumpAgain < 2)
            {
                jumpAgain++;
                isOnGround = false;
                rgbody.velocity = new Vector2(rgbody.velocity.x, jumpForce);
                PlayAni(State.Jump);
                AudioManager.instance.PlayAudio("jump");
            }
        }

    }
    private void PlayAni(State state)
    {

        if (curState == state)
            return;

        animator.SetInteger("State", (int)state);
        animator.SetTrigger("Change");
        curState = state;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground") || collision.collider.CompareTag("Wall"))
        {
            jumpAgain = 0;
            isOnGround = true;
        }
        else if (collision.collider.CompareTag("Trap"))
        {
            if (isHitted)
                return;
            UIManager.instance.ToGameOver();
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Fruit"))
        {
            if (collision.TryGetComponent(out Fruit fruit))
            {
                fruit.Hit();
                AudioManager.instance.PlayAudio("earn");
            }
        }
        else if (collision.CompareTag("Conduit"))
        {
            SceneManager.LoadScene("Scene" + 2, LoadSceneMode.Additive);
            gameObject.SetActive(false);
        }
        else if (collision.CompareTag("Enemy"))
        {
            if(collision.TryGetComponent(out Enemy enemy))
            {
               
                if (enemy.IsHitted)
                    return;
                if (!isHitted) 
                    StartCoroutine(Hit());
            }
        }else if (collision.CompareTag("End"))
        {
            UIManager.instance.ToGameOver();
        }
    }
    IEnumerator Hit()
    {
        UIManager.instance.HitPlayer();
        heart--;
        if(heart == 0)
        {
            UIManager.instance.ToGameOver();
            Destroy(gameObject);
        }
        foot.gameObject.SetActive(false);
        Color transpent = Color.white;
        transpent.a = 0.25f;
        isHitted = true;
        while (isHitted)
        {
            spRdr.color = transpent;
            yield return new WaitForSeconds(0.1f);
            spRdr.color = Color.white;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
