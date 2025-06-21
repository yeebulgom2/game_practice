/*
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
*/
using System.Collections;
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

    public Transform leftCheck;
    public Transform rightCheck;
    public float checkDistance = 1f;

    private bool canChangeDirection = true;  // 방향 전환 가능 여부

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        next_Move = think();
        anim = GetComponent<Animator>();
        enemySpriteRenderer = GetComponent<SpriteRenderer>();
        leftCheck.localPosition = new Vector2(-0.5f, -0.3f);
        rightCheck.localPosition = new Vector2(0.5f, -0.3f);
    }

    void Update()
    {
        // 이동
        rb.velocity = new Vector2(moveSpeed * next_Move, rb.velocity.y);

        // 애니메이션 설정
        anim.SetBool("moving", next_Move != 0);

        // 방향 전환
        if (next_Move < 0)
            enemySpriteRenderer.flipX = false;
        else if (next_Move > 0)
            enemySpriteRenderer.flipX = true;

        if (canChangeDirection)
        {
            RaycastHit2D hitLeft = Physics2D.Raycast(leftCheck.position, Vector2.down, checkDistance);
            RaycastHit2D hitRight = Physics2D.Raycast(rightCheck.position, Vector2.down, checkDistance);

            Debug.DrawRay(leftCheck.position, Vector2.down * checkDistance, Color.red);

            bool isGroundLeft = hitLeft.collider != null && hitLeft.collider.CompareTag("Ground");
            bool isGroundRight = hitRight.collider != null && hitRight.collider.CompareTag("Ground");

            //Debug.Log("isGroundLeft: " + isGroundLeft + ", isGroundRight: " + isGroundRight);

            if (!isGroundLeft && next_Move < 0)
            {
              //  Debug.Log("왼쪽 벗어남");
                next_Move = 1;
            }
            else if (!isGroundRight && next_Move > 0)
            {
                //Debug.Log("오른쪽 벗어남");
                next_Move = -1;
            }
        }
    }

    IEnumerator DirectionChangeCooldown()
    {
        canChangeDirection = false;
        yield return new WaitForSeconds(0.5f); // 0.5초 동안 방향 전환 불가
        canChangeDirection = true;
    }

    int think()
    {
        moveTime = rand.Next(1, 3);
        next_Move = rand.Next(-1, 2);
        Invoke("think", moveTime);
        return next_Move;
    }
}
