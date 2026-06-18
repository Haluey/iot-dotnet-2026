using UnityEngine;  // 유니티 엔진 클래스

// MonoBehaviour C# 스크립트가 기본적으로 상속받는 핵심 클래스
// 개발자 코드가 유니티 엔진과 인터랙티브하게 소통할 수 있도록
// 오브젝트에 컴포넌트로 연결, 동작을 제어
public class ConveyorBelt : MonoBehaviour
{
    [Header("물체이동 방향")]
    public Vector3 moveDirection = Vector3.right;

    [Header("물체이동 속도")]
    public float speed = 2.0f;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    // 매 프레임마다 두 충돌 영역이 접촉하고 있는 동안 발생하는 이벤트 핸들러
    private void OnCollisionStay(Collision collision)
    {
        Rigidbody rb = collision.rigidbody; // 충돌 감지된 오브젝트에 리지드바디 가져오기

        if (rb != null)
        {
            rb.linearVelocity = moveDirection.normalized * speed;   // 이동방향으로 속도만큼 이동
        }
    }
}