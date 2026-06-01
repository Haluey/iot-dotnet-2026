using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

public class MoveCube : MonoBehaviour
{
    //private Vector3 position;
    private float speed = 20f;
    private float rotateSpeed = 100f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // 화면이 시작되고 최초 한번만 실행되는 초기화 메서드
    void Start()
    {
        //position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    // 각 프레임마다 호출. 30fps => 1초동안 30번 호출
    void Update()
    {
        //position = Vector3.zero;    // 현재 위치 초기화

        //if (Input.GetKey(KeyCode.UpArrow))
        //{
        //    position.z = 0.1f;
        //    transform.Translate(position);
        //}

        //if (Input.GetKey(KeyCode.DownArrow))
        //{
        //    position.z = -0.1f;
        //    transform.Translate(position);
        //}

        //if (Input.GetKey(KeyCode.LeftArrow))
        //{
        //    position.y = -0.1f;
        //    transform.Rotate(position);
        //}

        //if (Input.GetKey(KeyCode.RightArrow))
        //{
        //    position.y = 0.1f;
        //    transform.Rotate(position);
        //}

        /// 앞 뒤 이동(컴퓨터 성능과 상관없이 자연스러운 움직임)
        float move = Input.GetAxis("Vertical");
        move = move * speed * Time.deltaTime;

        transform.Translate(Vector3.forward * move);

        // 좌 우 회전
        float rotate = Input.GetAxis("Horizontal");
        rotate = rotate * rotateSpeed * Time.deltaTime;

        transform.Rotate(Vector3.up * rotate);

    }
}
