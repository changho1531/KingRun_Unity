using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MobileCharacterController : MonoBehaviour
{
    //스와이프 동작 :  사용자가 터치 입력을 사용하여 화면을 빠르게 스와이프하는 동작을 말합니다
    //일반적으로 사용자가 손가락을 화면 위에서 아래로, 왼쪽에서 오른쪽으로, 오른쪽에서 왼쪽으로 등으로 빠르게 움직이는 동작을 스와이프 동작이라고 한단.

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
            //Input.GetTouch(0) : 첫 번째 터치에 대한 정보를 가져오는 메서드
            Touch touch = Input.GetTouch(0);

            //touch.phase : 터치의 상태를 나타낸다, 이 속성을 통해 현재 상태를 확인
            //TouchPhase.Began: 터치가 시작된 순간
            if (touch.phase == TouchPhase.Began)
            {
                // 터치 시작 지점을 저장합니다
                touchStartPosition = touch.position;
            }
            //TouchPhase.Ended: 터치가 종료된 순간
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


