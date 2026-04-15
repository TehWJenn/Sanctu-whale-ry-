using UnityEngine;

public class ClickToMove : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f; 

    private Vector2 targetPosition; 
    private bool isMoving = false;  

    private SpriteRenderer spriteRenderer; 
    private Animator anim; // Added for animations

    [Header("Dialogue Objects")]
    public GameObject talk1;
    public GameObject talk2;
    public GameObject talk3;

    void Start()
    {
        targetPosition = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        // Link the animator component
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // 1. Check for Click
       if (Input.GetMouseButtonDown(0))
    {
        // Convert mouse click to world space
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        // CRITICAL: Force the Z to 0 so the whale stays on the 2D plane
        targetPosition = new Vector2(mousePos.x, mousePos.y); 
        isMoving = true;
    }

        // 2. Handle Movement
        if (isMoving)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, targetPosition) < 0.05f) // Increased threshold slightly
            {
                isMoving = false;
                transform.position = targetPosition;

                // Stop the animation!
                if(anim != null) anim.SetBool("isWalking", false);
            }
        }

        // 3. Handle Flipping (Whale Direction)
        if (targetPosition.x > transform.position.x + 0.01f)
        {
            spriteRenderer.flipX = false;
        }
        else if (targetPosition.x < transform.position.x - 0.01f)
        {
            spriteRenderer.flipX = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Using .CompareTag is more efficient than .name
        if (collision.gameObject.name == "obj1")
        {
            talk1.SetActive(true);
            collision.gameObject.SetActive(false);
        }
        // ... same for obj2 and obj3
    }
}


// using UnityEngine;

// // ���ص���Ҫ�ƶ��Ľ�ɫ������
// public class ClickToMove : MonoBehaviour
// {
//     [Header("�ƶ�����")]
//     [Tooltip("��ɫ�ƶ��ٶ�")]
//     public float moveSpeed = 5f; // ����Inspector������

//     private Vector2 targetPosition; // �������Ŀ��λ��
//     private bool isMoving = false;  // �Ƿ����ƶ�״̬

//     private SpriteRenderer spriteRenderer; // ���ڿ��ƽ�ɫ����
//     public GameObject talk1;
//     public GameObject talk2;
//     public GameObject talk3;
//     void Start()
//     {
//         // ��ʼĿ��λ����Ϊ��ɫ��ǰλ��
//         targetPosition = transform.position;

//         // ��ȡSpriteRenderer���
//         spriteRenderer = GetComponent<SpriteRenderer>();
//     }

//     void Update()
//     {
//         // ������������
//         if (Input.GetMouseButtonDown(0))
//         {
//             // ����Ļ����ת��Ϊ�������꣨2D������
//             Vector2 mouseScreenPos = Input.mousePosition;
//             // ע�⣺2D������Z����Ϊ�����ƽ��ľ��루������10������Ĭ��2D�����
//             Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(mouseScreenPos.x, mouseScreenPos.y, 10f));

//             // �����ƶ�Ŀ��
//             targetPosition = mouseWorldPos;
//             isMoving = true;
//         }

//         // �����Ҫ�ƶ�����Ŀ��λ���ƶ�
//         if (isMoving)
//         {
//             // ƽ���ƶ���ÿ֡�ƶ�һ�����룬����˲��
//             transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

//             // ����Ƿ񵽴�Ŀ��λ�ã�����С��0.01ʱֹͣ��
//             if (Vector2.Distance(transform.position, targetPosition) < 0.01f)
//             {
//                 isMoving = false;
//                 // ǿ�ƶ���Ŀ��λ�ã�����΢Сƫ�ƣ�
//                 transform.position = targetPosition;
//             }
//         }

//         // ����Ŀ��λ�õ�����ɫ�ĳ���
//         if (targetPosition.x > transform.position.x)
//         {
//             // Ŀ�����Ҳ࣬�����ұ�
//             spriteRenderer.flipX = false;
//         }
//         else if (targetPosition.x < transform.position.x)
//         {
//             // Ŀ������࣬�������
//             spriteRenderer.flipX = true;
//         }
//     }

//     private void OnTriggerEnter2D(Collider2D collision)
//     {
//         if (collision.gameObject.name == "obj1")
//         {
//             talk1.gameObject.SetActive(true);
//             collision.gameObject.SetActive(false);
//         }

//         if (collision.gameObject.name == "obj2")
//         {
//             talk2.gameObject.SetActive(true);
//             collision.gameObject.SetActive(false);
//         }

//         if (collision.gameObject.name == "obj3")
//         {
//             talk3.gameObject.SetActive(true);
//             collision.gameObject.SetActive(false);
//         }
//     }
// }