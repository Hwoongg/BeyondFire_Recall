using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class CharacterMover : MonoBehaviour
//{
//    private Transform m_thisTransform;
//    public GameObject m_thisGo;
//    public Rigidbody2D CharacterRigidbody;
//    public Animator CharacterAnimator;

//    public GameObject Icon;
//    private Animator IconAnimator; // 아이콘 표기를 위한 애니메이터

//    public float MoveSpeed;
//    public float JumpHeight;

//    void Start()
//    {
//        IconAnimator = Icon.GetComponent<Animator>();

//        if (ReferenceEquals(m_thisTransform, null))
//        {
//            m_thisTransform = transform;
//        }
//    }

//    void Update()
//    {
//        float x = Input.GetAxis("Horizontal") * Time.deltaTime * MoveSpeed;
//        //float z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

//        if (Input.GetKey(KeyCode.D))
//        {
//            m_thisTransform.Translate(x, 0, 0);
//            m_thisTransform.localScale = new Vector3(-1, 1, 1);
//            Icon.transform.localScale = new Vector3(-1, 1, 1);
//            CharacterAnimator.SetBool("IsWalk", true);
//        }
//        else if (Input.GetKey(KeyCode.A))
//        {
//            m_thisTransform.Translate(x, 0, 0);
//            m_thisTransform.localScale = new Vector3(1, 1, 1);
//            Icon.transform.localScale = new Vector3(1, 1, 1);
//            CharacterAnimator.SetBool("IsWalk", true);
//        }
//        else
//        {
//            CharacterAnimator.SetBool("IsWalk", false);
//        }

//        if (Input.GetKeyDown(KeyCode.Space))
//        {
//            CharacterRigidbody.AddForce(new Vector3(0, JumpHeight, 0));
//        }
//    }

//    private void Flip()
//    {
//        m_thisTransform.localScale = -m_thisTransform.localScale;
//        Icon.transform.localScale = -Icon.transform.localScale;
//    }

//    private void OnTriggerEnter2D(Collider2D collision)
//    {
//        Debug.Log("Enter 이벤트 발생");
//        IconAnimator.SetBool("onDoor", true);
//    }

//    private void OnTriggerExit2D(Collider2D collision)
//    {
//        Debug.Log("Exit 이벤트 발생");
//        IconAnimator.SetBool("onDoor", false);
//    }


//    //////////////////////////////////////////////////////////////////////////
//    //
//    // 버튼 UI 터치시 호출될 함수들...
//}


public class CharacterMover : MonoBehaviour
{
    //
    // 최대한 이동에 관련된 것들만 넣을 수 있도록...
    //
    

    public enum MoveType
    {
        LOCK,
        COMMANDMOVE,
        AUTOMOVE
    }
    [HideInInspector]
    public MoveType moveType;
    // private 전환, getComponent 하는편이 깔끔할 듯
    public Animator myAnimator;

    // 머리 위 아이콘에 대한 변수들... 컴포넌트 분리할지 고려중
    public GameObject Icon;
    [HideInInspector] public Animator IconAnimator; // 아이콘 표기를 위한 애니메이터


    // 이동속도 관련 파라미터
    public float MoveSpeed;
    public float JumpHeight;

    // 이동상태 bool 변수
    bool keyLeftMove;
    bool keyRightMove;
    float InputDelay;
    bool IsDash;

    public enum CharState
    {
        IDLE,
        DASH,
        RESCUE
    }
    [HideInInspector] public CharState charState;
    [HideInInspector] public CharState beforeState;


    void Start()
    {
        IconAnimator = Icon.GetComponent<Animator>();
        myAnimator = GetComponent<Animator>();
        moveType = MoveType.COMMANDMOVE;
        InputDelay = 0;
        IsDash = false;
        charState = CharState.IDLE;
        beforeState = CharState.IDLE;
    }


    void Update()
    {
        
        // 상태에 따른 지속적인 HP 감소를 위해 상태 변수를 계속해서 체크해줄 필요가 있습니다.
        // 이동하고있지 않을 시 이전 상태로 돌립니다.
        if(!keyLeftMove && !keyRightMove)
        {
            charState = beforeState;
        }

        switch (moveType)
        {
            case MoveType.LOCK:
                break;

            case MoveType.COMMANDMOVE: // 플레이어 조작으로 이동
                CommandMove();
                break;

            case MoveType.AUTOMOVE: // 타 스크립트에 의해 자동으로 이동
                AutoMove();
                break;
        }

    }

    // 사용자 입력에 따른 무브먼트
    private void CommandMove()
    {
        float moveSpeed = 0;

        switch(charState)
        {
            case CharState.IDLE:
                moveSpeed = MoveSpeed;
                break;
            case CharState.DASH:
                moveSpeed = MoveSpeed * 2;
                break;
            case CharState.RESCUE:
                moveSpeed = MoveSpeed * 0.75f;
                break;
        }

        if (keyRightMove)
        {
            float x = 1 * Time.deltaTime * moveSpeed;
            transform.Translate(x, 0, 0);


            transform.localScale = new Vector3(-1, 1, 1);
            Icon.transform.localScale = new Vector3(-1, 1, 1);
            myAnimator.SetBool("IsWalk", true);
        }
        else if (keyLeftMove)
        {

            float x = -1 * Time.deltaTime * moveSpeed;
            transform.Translate(x, 0, 0);


            transform.localScale = new Vector3(1, 1, 1);
            Icon.transform.localScale = new Vector3(1, 1, 1);
            myAnimator.SetBool("IsWalk", true);
        }
        else
        {
            myAnimator.SetBool("IsWalk", false);
        }
        
    }

    // 사용자 입력을 받지 않는 자동 이동
    private void AutoMove()
    {

    }


    // 캐릭터의 상태 아이콘
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("CharacterMover의 TriggerEnter 이벤트 발생");
        if (collision.CompareTag("Door"))
        {
            IconAnimator.SetBool("onDoor", true);
            IconAnimator.SetBool("onDownStair", false);
            IconAnimator.SetBool("onUpStair", false);
            IconAnimator.SetBool("onDuplex", false);
            IconAnimator.SetBool("onItem", false);
            IconAnimator.SetBool("onTalk", false);
            IconAnimator.SetBool("onHelp", false);
            IconAnimator.SetBool("onExit", false);
        }
        if (collision.CompareTag("DownStair"))
        {
            IconAnimator.SetBool("onDoor", false);
            IconAnimator.SetBool("onDownStair", true);
            IconAnimator.SetBool("onUpStair", false);
            IconAnimator.SetBool("onDuplex", false);
            IconAnimator.SetBool("onItem", false);
            IconAnimator.SetBool("onTalk", false);
            IconAnimator.SetBool("onHelp", false);
            IconAnimator.SetBool("onExit", false);
        }
        if (collision.CompareTag("UpStair"))
        {
            IconAnimator.SetBool("onDoor", false);
            IconAnimator.SetBool("onDownStair", false);
            IconAnimator.SetBool("onUpStair", true);
            IconAnimator.SetBool("onDuplex", false);
            IconAnimator.SetBool("onItem", false);
            IconAnimator.SetBool("onTalk", false);
            IconAnimator.SetBool("onHelp", false);
            IconAnimator.SetBool("onExit", false);
        }
        if (collision.CompareTag("DuplexStair"))
        {
            IconAnimator.SetBool("onDoor", false);
            IconAnimator.SetBool("onDownStair", false);
            IconAnimator.SetBool("onUpStair", false);
            IconAnimator.SetBool("onDuplex", true);
            IconAnimator.SetBool("onItem", false);
            IconAnimator.SetBool("onTalk", false);
            IconAnimator.SetBool("onHelp", false);
            IconAnimator.SetBool("onExit", false);
        }
        if (collision.CompareTag("Item"))
        {
            IconAnimator.SetBool("onDoor", false);
            IconAnimator.SetBool("onDownStair", false);
            IconAnimator.SetBool("onUpStair", false);
            IconAnimator.SetBool("onDuplex", false);
            IconAnimator.SetBool("onItem", true);
            IconAnimator.SetBool("onTalk", false);
            IconAnimator.SetBool("onHelp", false);
            IconAnimator.SetBool("onExit", false);
        }
        if (collision.CompareTag("NPC"))
        {
            IconAnimator.SetBool("onDoor", false);
            IconAnimator.SetBool("onDownStair", false);
            IconAnimator.SetBool("onUpStair", false);
            IconAnimator.SetBool("onDuplex", false);
            IconAnimator.SetBool("onItem", false);
            IconAnimator.SetBool("onTalk", true);
            IconAnimator.SetBool("onHelp", false);
            IconAnimator.SetBool("onExit", false);
        }
        if(collision.CompareTag("Help"))
        {
            IconAnimator.SetBool("onDoor", false);
            IconAnimator.SetBool("onDownStair", false);
            IconAnimator.SetBool("onUpStair", false);
            IconAnimator.SetBool("onDuplex", false);
            IconAnimator.SetBool("onItem", false);
            IconAnimator.SetBool("onTalk", false);
            IconAnimator.SetBool("onHelp", true);
            IconAnimator.SetBool("onExit", false);
        }
        if(collision.CompareTag("Exit"))
        {
            IconAnimator.SetBool("onDoor", false);
            IconAnimator.SetBool("onDownStair", false);
            IconAnimator.SetBool("onUpStair", false);
            IconAnimator.SetBool("onDuplex", false);
            IconAnimator.SetBool("onItem", false);
            IconAnimator.SetBool("onTalk", false);
            IconAnimator.SetBool("onHelp", false);
            IconAnimator.SetBool("onExit", true);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log("CharacterMover의 TriggerExit 이벤트 발생");
        //if (collision.CompareTag("Door"))
        //{
        //    IconAnimator.SetBool("onDoor", false);
        //}
        //if (collision.CompareTag("DownStair"))
        //{
        //    IconAnimator.SetBool("onDownStair", false);
        //}
        //if (collision.CompareTag("UpStair"))
        //{
        //    IconAnimator.SetBool("onUpStair", false);
        //}
        //if (collision.CompareTag("DuplexStair"))
        //{
        //    IconAnimator.SetBool("onDuplex", false);
        //}
        //if (collision.CompareTag("Item"))
        //{
        //    IconAnimator.SetBool("onItem", false);
        //}
        //if (collision.CompareTag("NPC"))
        //{
        //    IconAnimator.SetBool("onTalk", false);
        //}
        IconAnimator.SetBool("onDoor", false);
        IconAnimator.SetBool("onDownStair", false);
        IconAnimator.SetBool("onUpStair", false);
        IconAnimator.SetBool("onDuplex", false);
        IconAnimator.SetBool("onItem", false);
        IconAnimator.SetBool("onTalk", false);
        IconAnimator.SetBool("onHelp", false);
        IconAnimator.SetBool("onExit", false);
    }

    public void IconStateClear()
    {
        IconAnimator.SetBool("onDoor", false);
        IconAnimator.SetBool("onDownStair", false);
        IconAnimator.SetBool("onUpStair", false);
        IconAnimator.SetBool("onDuplex", false);
        IconAnimator.SetBool("onItem", false);
        IconAnimator.SetBool("onTalk", false);
        IconAnimator.SetBool("onHelp", false);
        IconAnimator.SetBool("onExit", false);
    }


    
    //////////////////////////////////////////////////////////////////////////
    //
    // 버튼 OnClick에서 호출될 함수들...
    // 버튼의 OnClick은 눌렀다 뗄때 한번 호출된다.
    // 현재 PointerDown, PointerUp을 통해 실행되는중...
    //
    // Move 함수 내에서 좌표이동을 실시하는 경우 이벤트 발생한 한 프레임동안만 이동하게 된다
    // 이동방향을 bool변수로 지정, 조작하여 Update쪽에서 프레임마다 이동이 이루어지도록 구현
    public void MoveLeft()
    {
        if (keyLeftMove)
        {
            keyLeftMove = false;
            IsDash = false;

            // 캐릭터 상태변경이 필요한지 판단 필요
            // ...
            return;
        }
        keyLeftMove = true;
        if (Time.time < InputDelay + 0.3f)
        {
            if (charState == CharState.RESCUE)
                return;
            Debug.Log("대시 활성화");
            IsDash = true;
            beforeState = charState;
            charState = CharState.DASH;
            
        }
        InputDelay = Time.time;

    }
    public void MoveRight()
    {
        if (keyRightMove)
        {
            keyRightMove = false;
            IsDash = false;
            return;
        }

        keyRightMove = true;
        if (Time.time < InputDelay + 0.3f)
        {
            if (charState == CharState.RESCUE)
                return;
            Debug.Log("대시 활성화");
            IsDash = true;

            beforeState = charState;
            charState = CharState.DASH;
        }
        InputDelay = Time.time;
    }

}
