using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Frog : Enemy
{
    private Rigidbody2D rb;
    private Collider2D Coll;
    public LayerMask Ground;
    public Transform leftPoint, rightPoint;
    public float Speed,JumpForce;
     
    private float leftx, rightx;
    private bool Faceleft = true;

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

    
    void Update()
    {
        SwitchAnim();
    }

    void Movement()
    {
        if (Faceleft)//面向左
        {
            if (Coll.IsTouchingLayers(Ground))
            {
                rb.velocity = new Vector2(0, JumpForce);
                rb.velocity = new Vector2(-Speed, JumpForce);
                Anim.SetBool("jump", true);
            }
            
            if (transform.position.x < leftx)//超过左点掉头
            {
                rb.velocity = new Vector2(Speed, JumpForce);
                transform.localScale = new Vector3(-1, 1, 1);
                Faceleft = false;
            }
        }
        else
        {
            if (Coll.IsTouchingLayers(Ground))
            {
                rb.velocity = new Vector2(Speed, JumpForce);
                Anim.SetBool("jump", true);
            }
            if (transform.position.x > rightx)
            {
                rb.velocity = new Vector2(-Speed, JumpForce);
                transform.localScale = new Vector3(1, 1, 1);
                Faceleft = true;
            }
        }
    }

    void SwitchAnim()
    {
        if (Anim.GetBool("jump"))
        {
            if (rb.velocity.y < 0.1)
            {
                Anim.SetBool("jump", false);
                Anim.SetBool("fall", true);
            }
        }
        if (Coll.IsTouchingLayers(Ground) && Anim.GetBool("fall"))
        {
            Anim.SetBool("fall", false);
        }
    }

    
}
