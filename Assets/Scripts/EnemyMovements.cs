using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovements : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    Rigidbody2D myRigidBody;
    BoxCollider2D myReversePeriscope;

    void Awake()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myReversePeriscope = GetComponent<BoxCollider2D>();
    }

    void Start()
    {

    }

    void Update()
    {
        myRigidBody.velocity = new Vector2(moveSpeed, 0f);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Ground") EnemyMove();

    }

    void EnemyMove()
    {
        moveSpeed = -moveSpeed;
        FlipEnemy();
    }

    void FlipEnemy()
    {
        transform.localScale = new Vector2(-(transform.localScale.x), 1f);
    }
}
