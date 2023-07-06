using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileCharacterController : MonoBehaviour
{
    // 스와이프 동작: 사용자가 터치 입력을 사용하여 화면을 빠르게 스와이프하는 동작을 말합니다.
    // 일반적으로 사용자가 손가락을 화면 위에서 아래로, 왼쪽에서 오른쪽으로, 오른쪽에서 왼쪽으로 등으로 빠르게 움직이는 동작을 스와이프 동작이라고 합니다.

    private Vector2 touchStartPosition; // 터치 시작 지점

    private Rigidbody rigidbody; // Rigidbody 컴포넌트를 참조하기 위한 변수

    public float moveSpeed = 5f; // 캐릭터의 이동 속도
    public float jumpForce = 5f; // 점프 힘

    private bool isGrounded = true; // 바닥에 닿아 있는지 여부를 판단하기 위한 변수
    private bool isJumping = false; // 점프 중인지 여부를 판단하기 위한 변수

    public SpawnManager spawnManager;

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
                touchStartPosition = touch.position; // 터치 시작 지점을 저장합니다
            }
            //TouchPhase.Ended: 터치가 종료된 순간
            else if (touch.phase == TouchPhase.Ended)
            {
                Vector2 swipeDirection = touch.position - touchStartPosition;

                if (swipeDirection.magnitude > 50) // 스와이프 거리를 조정하여 원하는 감지 정도를 설정할 수 있습니다.
                {
                    if (Mathf.Abs(swipeDirection.x) > Mathf.Abs(swipeDirection.y))
                    {
                        if (swipeDirection.x < 0) // 왼쪽 슬라이스로 판단되는 경우
                        {
                            MoveCharacterLeft();
                        }
                        else if (swipeDirection.x > 0) // 오른쪽 슬라이스로 판단되는 경우
                        {
                            MoveCharacterRight();
                        }
                    }
                    else
                    {
                        if (swipeDirection.y > 0) // 위쪽 슬라이스로 판단되는 경우
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
        rigidbody.velocity = movement; // 왼쪽으로 이동하는 속도 벡터를 Rigidbody의 속도로 설정합니다
    }

    private void MoveCharacterRight()
    {
        // 오른쪽으로 이동하는 로직
        Vector3 movement = new Vector3(-moveSpeed, 0, 0);
        rigidbody.velocity = movement; // 오른쪽으로 이동하는 속도 벡터를 Rigidbody의 속도로 설정합니다
    }

    private void JumpCharacter()
    {
        // 점프하는 로직
        if (isGrounded && !isJumping)
        {
            rigidbody.velocity = new Vector3(0, jumpForce, 0); // 점프하는 속도 벡터를 Rigidbody의 속도로 설정합니다
            isJumping = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true; // 바닥에 닿았을 때 isGrounded 상태를 true로 업데이트합니다
            isJumping = false; // 점프 중이 아니므로 isJumping 상태를 false로 업데이트합니다
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false; // 바닥에서 벗어났을 때 isGrounded 상태를 false로 업데이트합니다
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        spawnManager.SpawnerTriggerEntered();
    }
}
