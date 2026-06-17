using UnityEngine;

public class Collectable : MonoBehaviour
{
    [Header("회전 설정")]
    [Tooltip("프레임당 회전 속도")]
    [Range(0, 10)]
    public float rotationSpeed = 0.5f;

    [Tooltip("아이템 획득시 이펙트 지정")]
    public GameObject collectEffect;

    [Header("이펙트 사운드")]
    public AudioClip pickupSound;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, rotationSpeed, 0);  // 매 프레임마다 y축을 0.5f씩 회전
    }

    // 물체끼리 충돌이 발생했을 때 이벤트 처리
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(pickupSound, transform.position);    // 뾰롱소리

            Destroy(gameObject);    // 코인 삭제

            Instantiate(collectEffect, transform.position, transform.rotation); // 파티클 이펙트 실행
        }
    }
}