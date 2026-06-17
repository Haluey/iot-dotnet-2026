using UnityEngine;


public class DoorOpener : MonoBehaviour
{
    private Animator doorAnimator;

    void Start()
    {
        // 현재 오브젝트에 할당된 애니메이터를 가져올 것
        doorAnimator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is the player (or another specified object)
        if (other.CompareTag("Player")) // 게임 오브젝트 중 Player라는 태그를 가지고 있는 객체에
        {
            if (doorAnimator != null)
            {
                // Door_Open 애니메이션을 실행하라
                doorAnimator.SetTrigger("Door_Open");
            }
        }
    }
}
