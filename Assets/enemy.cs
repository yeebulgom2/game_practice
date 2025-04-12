using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;


public class enemy : MonoBehaviour
{
    private SpriteRenderer enemySpriteRenderer;
    private Rigidbody2D rb;
    public float moveSpeed = 3f;
    public int next_Move;
    public int moveTime;
    private static System.Random rand = new System.Random();
    Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        next_Move = think();
        anim = GetComponent<Animator>();
        enemySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {   
        rb.velocity = new Vector2(moveSpeed * next_Move, rb.velocity.y);
        if (next_Move != 0)
        {
            anim.SetBool("moving", true);
        }
        else
        {
            anim.SetBool("moving", false);
        }
        if (next_Move < 0)
        {
            enemySpriteRenderer.flipX = false;
        }
        if(next_Move > 0)
        {
            enemySpriteRenderer.flipX = true;
        }
    }

    int think()
    {
        moveTime = rand.Next(1, 3);
        next_Move = rand.Next(-1, 2);
        Invoke("think", moveTime);
        return next_Move;
    }   
}