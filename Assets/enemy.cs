using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class enemy : MonoBehaviour
{
    private Rigidbody2D rb;
    public float moveSpeed = 3f;
    public int next_Move;
    public int moveTime;
    private static System.Random rand = new System.Random();

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        next_Move = think();
    }

    void Update()
    {   
        rb.velocity = new Vector2(moveSpeed * next_Move, rb.velocity.y);
    }

    int think()
    {
        moveTime = rand.Next(1, 3);
        next_Move = rand.Next(-1, 2);
        Invoke("think", moveTime);
        return next_Move;
    }   
}