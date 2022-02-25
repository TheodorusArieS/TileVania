using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsBehavior : MonoBehaviour
{
    Rigidbody2D myRigidBody;
    [SerializeField] float bulletSpeed = 20f;

    PlayerMovements player;
    float xSpeed;

    void Awake()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovements>();
    }
    void Start()
    {
        xSpeed = player.transform.localScale.x * bulletSpeed;
    }


    void Update()
    {
        myRigidBody.velocity = new Vector2( xSpeed, 0f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemies")
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        if(other.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }
}
