using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private SpriteRenderer playerSpriteRenderer;  // 플레이어 이미지 스프라이트
    public float moveSpeed = 5f;     // 캐릭터 이동 속도
    public float jumpForce = 10f;    // 점프 힘
    public int maxJumpCount = 2;     // 최대 점프 횟수
    public int MaxHp = 6;            // 최대 히트포인트
    public int CurrentHp;            // 현재 히트포인트
    public Transform player;         // player객체 위치
    public Vector2 resetPosition;    // 위치 초기값

    public Slider HpBarSlider; // HP바 컴포넌트

    private Rigidbody2D rb;          // Rigidbody2D 컴포넌트
    private int jumpCount = 0;       // 현재 점프 횟수
    private bool isGrounded = false; // 바닥에 닿아있는지 확인
    private bool isDead = false;     // HP가 0이거나 y좌표가 -10보다 낮은지 확인
    Animator anim;
    
    void Start()
    {
        // Rigidbody2D 가져오기
        rb = GetComponent<Rigidbody2D>();
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
        CurrentHp = MaxHp;
        anim = GetComponent<Animator>();
    }

    void Update()
    {

        // 좌우 이동
        float moveX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);
        if(moveX < 0)
        {
            playerSpriteRenderer.flipX = true;
        }
        if(moveX > 0)
        {
            playerSpriteRenderer.flipX = false;
        }
        

        // 점프 입력 처리
        if (Input.GetKeyDown(KeyCode.UpArrow) && jumpCount < maxJumpCount)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount++;

            updateIsGrounded(true);
        }
        else if(jumpCount == maxJumpCount)
        {
            updateIsGrounded(false);
        }
        if (rb.velocity.x == 0)
            anim.SetBool("iswalking", false);
        else
            anim.SetBool("iswalking", true);
        CheckBoundary();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 바닥과 충돌 시 점프 횟수 초기화(수정 필요)
        if (collision.gameObject.CompareTag("Ground")|| collision.gameObject.CompareTag("enemy"))
        {
            updateIsGrounded(true);
            jumpCount = 0;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("DeadZone"))
        {
            CurrentHp -= 1;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce * 1.5f);

            if (CurrentHp <= 0)
            {
                isDead = true;
                
            }

            HpBarSlider.value = (float)CurrentHp / MaxHp;

        }

        // 체크포인트 접촉시 현 플레이어 위치를 resetPosition에 저장
        if(collision.gameObject.CompareTag("CheckPoint"))
        {
            Vector2 currentPosition = transform.position;
            resetPosition = currentPosition;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // 바닥에서 떨어질 때 상태 변경(수정 필요)
        if (collision.gameObject.CompareTag("Ground")|| collision.gameObject.CompareTag("enemy"))
        {
            updateIsGrounded(false);
        }

    }

   

    private void CheckBoundary()
    {
        // player객체의 위치, HP확인후 리스폰
        Vector2 currentPosition = transform.position;


        if (isDead)
        {
            transform.position = new Vector2(resetPosition.x, resetPosition.y);
            CurrentHp = MaxHp;
            HpBarSlider.value = 1.0F;
            isDead = false;
        }
    }

    private void updateIsGrounded(bool flag)
    {
        isGrounded = flag;
        anim.SetBool("isGrounded", flag);
    }
}
