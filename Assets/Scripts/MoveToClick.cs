using UnityEngine;

// 挂载到需要移动的角色对象上
public class ClickToMove : MonoBehaviour
{
    [Header("移动设置")]
    [Tooltip("角色移动速度")]
    public float moveSpeed = 5f; // 可在Inspector面板调整

    private Vector2 targetPosition; // 鼠标点击的目标位置
    private bool isMoving = false;  // 是否处于移动状态

    private SpriteRenderer spriteRenderer; // 用于控制角色朝向
    public GameObject talk1;
    public GameObject talk2;
    public GameObject talk3;
    void Start()
    {
        // 初始目标位置设为角色当前位置
        targetPosition = transform.position;

        // 获取SpriteRenderer组件
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // 检测鼠标左键点击
        if (Input.GetMouseButtonDown(0))
        {
            // 将屏幕坐标转换为世界坐标（2D场景）
            Vector2 mouseScreenPos = Input.mousePosition;
            // 注意：2D场景中Z轴设为相机到平面的距离（这里用10，适配默认2D相机）
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(mouseScreenPos.x, mouseScreenPos.y, 10f));

            // 设置移动目标
            targetPosition = mouseWorldPos;
            isMoving = true;
        }

        // 如果需要移动，向目标位置移动
        if (isMoving)
        {
            // 平滑移动：每帧移动一定距离，避免瞬移
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // 检测是否到达目标位置（距离小于0.01时停止）
            if (Vector2.Distance(transform.position, targetPosition) < 0.01f)
            {
                isMoving = false;
                // 强制对齐目标位置（避免微小偏移）
                transform.position = targetPosition;
            }
        }

        // 根据目标位置调整角色的朝向
        if (targetPosition.x > transform.position.x)
        {
            // 目标在右侧，朝向右边
            spriteRenderer.flipX = false;
        }
        else if (targetPosition.x < transform.position.x)
        {
            // 目标在左侧，朝向左边
            spriteRenderer.flipX = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "obj1")
        {
            talk1.gameObject.SetActive(true);
            collision.gameObject.SetActive(false);
        }

        if (collision.gameObject.name == "obj2")
        {
            talk2.gameObject.SetActive(true);
            collision.gameObject.SetActive(false);
        }

        if (collision.gameObject.name == "obj3")
        {
            talk3.gameObject.SetActive(true);
            collision.gameObject.SetActive(false);
        }
    }
}