using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseWalk : Enemy
{
    private Rigidbody2D rb;
    private Collider2D Coll;

    public LayerMask Ground;
    public Transform leftPoint, rightPoint;

    private float leftx, rightx;
    private bool Faceleft = true;

    public float Speed;

    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        Coll = GetComponent<Collider2D>();
        leftx = leftPoint.position.x;
        rightx = rightPoint.position.x;
        Destroy(leftPoint.gameObject);
        Destroy(rightPoint.gameObject);

    }

    private void FixedUpdate()
    {
        
    }

   
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if (Faceleft)//面向左
        {
            if (Coll.IsTouchingLayers(Ground))
            {
                rb.velocity = new Vector2(-Speed, rb.velocity.y);
                
            }

            if (transform.position.x < leftx)//超过左点掉头
            {
                rb.velocity = new Vector2(Speed, rb.velocity.y);
                transform.localScale = new Vector3(-1, 1, 1);
                Faceleft = false;
            }
        }
        else
        {
            if (Coll.IsTouchingLayers(Ground))
            {
                rb.velocity = new Vector2(Speed, rb.velocity.y);
            }
            if (transform.position.x > rightx)
            {
                rb.velocity = new Vector2(-Speed, rb.velocity.y);
                transform.localScale = new Vector3(1, 1, 1);
                Faceleft = true;
            }
        }
    }

}
