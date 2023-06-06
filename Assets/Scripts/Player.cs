using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class MobileCharacterController : MonoBehaviour
{
    private Vector2 touchStartPosition; // 터치 시작 지점

    private Rigidbody rigidbody; // Rigidbody 컴포넌트를 참조하기 위한 변수

    public float moveSpeed = 5f; // 캐릭터의 이동 속도
    public float jumpForce = 5f; // 점프 힘

    private bool isGrounded = true; // 바닥에 닿아 있는지 여부를 판단하기 위한 변수
    private bool isJumping = false; // 점프 중인지 여부를 판단하기 위한 변수

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>(); // Rigidbody 컴포넌트를 가져옵니다
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                touchStartPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                Vector2 swipeDirection = touch.position - touchStartPosition;

                if (swipeDirection.magnitude > 50) // 스와이프 거리를 조정하여 원하는 감지 정도를 설정할 수 있습니다.
                {
                    // 스와이프 방향을 판단하여 해당 동작 수행
                    if (Mathf.Abs(swipeDirection.x) > Mathf.Abs(swipeDirection.y))
                    {
                        // 왼쪽 슬라이스로 판단되는 경우
                        if (swipeDirection.x < 0)
                        {
                            MoveCharacterLeft();
                        }
                        // 오른쪽 슬라이스로 판단되는 경우
                        else if (swipeDirection.x > 0)
                        {
                            MoveCharacterRight();
                        }
                    }
                    else
                    {
                        // 위쪽 슬라이스로 판단되는 경우
                        if (swipeDirection.y > 0)
                        {
                            JumpCharacter();
                        }
                    }
                }
            }
        }
    }

    private void MoveCharacterLeft()
    {
        // 왼쪽으로 이동하는 로직
        Vector3 movement = new Vector3(moveSpeed, 0, 0);
        transform.Translate(movement * Time.deltaTime);
    }

    private void MoveCharacterRight()
    {
        // 오른쪽으로 이동하는 로직
        Vector3 movement = new Vector3(-moveSpeed, 0, 0);
        transform.Translate(movement * Time.deltaTime);
    }

    private void JumpCharacter()
    {
        // 점프하는 로직
        if (isGrounded && !isJumping)
        {
            rigidbody.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            isJumping = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 바닥에 닿았을 때 isGrounded와 isJumping 상태를 업데이트합니다
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            isJumping = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // 바닥에서 벗어났을 때 isGrounded 상태를 업데이트합니다
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}


