using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovements : MonoBehaviour
{
    [SerializeField] float moveSpeed = 8f;
    [SerializeField] float jumpSpeed = 10f;
    [SerializeField] float climbSpeed = 10f;
    [SerializeField] float gravity;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;
    [SerializeField] float waitTime = 2f;
    Vector2 moveInput;
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    BoxCollider2D myBoxCollider;

    GameSession gameSession;

    bool isDead;
    bool alreadyDead;


    void Awake()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBoxCollider = GetComponent<BoxCollider2D>();
        gameSession = FindObjectOfType<GameSession>();
    }
    void Start()
    {
        gravity = myRigidBody.gravityScale;
    }

    void Update()
    {
        if (!isDead)
        {
            Run();
            FlipSprite();
            ClimbLadder();
        }
        else
        {
            if (!alreadyDead) DeadActions();
        }

    }

    void DeadActions()
    {
        alreadyDead = true;
        myAnimator.SetBool("isRunning", false);
        myAnimator.SetBool("isClimbing", false);
        myAnimator.SetBool("isDead", true);
        myRigidBody.velocity = new Vector2(0f, jumpSpeed);
        StartCoroutine(RestartLevel());

    }
    IEnumerator RestartLevel()
    {
        yield return new WaitForSecondsRealtime(waitTime);
        gameSession.ProcessPlayerDeath();
    }



    void OnFire(InputValue value)
    {
        if (!value.isPressed || isDead) return;
        Instantiate(bullet, gun.position, transform.rotation);
    }
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (!myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) || isDead) return;
        if (value.isPressed) myRigidBody.velocity = new Vector2(0f, jumpSpeed);
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * moveSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;

        myAnimator.SetBool("isRunning", false);
        if (moveInput.x != 0) myAnimator.SetBool("isRunning", true);
    }

    void ClimbLadder()
    {

        if (!myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            myAnimator.SetBool("isClimbing", false);
            myRigidBody.gravityScale = gravity;
            return;
        }

        myRigidBody.gravityScale = 0;
        Vector2 climbVelocity = new Vector2(myRigidBody.velocity.x, moveInput.y * climbSpeed);
        myRigidBody.velocity = climbVelocity;

        myAnimator.SetBool("isClimbing", false);
        if (moveInput.y != 0) myAnimator.SetBool("isClimbing", true);
    }


    void FlipSprite()
    {
        if (myRigidBody.velocity.x == 0) return;
        transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), 1f);
    }

    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        if (collisionInfo.gameObject.tag == "Enemies" || collisionInfo.gameObject.tag == "Hazard")
        {
            isDead = true;
        }
    }





}
