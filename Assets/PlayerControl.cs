using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [Header("移动设置")]
    public float normalSpeed = 5f;      // 普通移动速度
    public float boostSpeed = 12f;      // 被撞后的加速速度
    public float boostDuration = 1.5f;  // 加速持续时间
    public float boostDistance = 10f;   // 与追赶者拉开至少10单位后停止加速

    private Rigidbody2D rb;
    private float currentSpeed;
    private bool isBoosting = false;
    private Chaser chaser;
    private PlayerFlash flash;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Player 缺少 Rigidbody2D 组件！");
            return;
        }

        flash = GetComponent<PlayerFlash>();
        if (flash == null)
            flash = gameObject.AddComponent<PlayerFlash>();

        currentSpeed = normalSpeed;
        chaser = FindObjectOfType<Chaser>();
        if (chaser == null)
            Debug.LogWarning("场景中没有 Chaser 对象，加速距离检查将失效");
    }

    void FixedUpdate()
    {
        // 水平输入（改为您的按键：A/D 或 左/右箭头）
        float moveInput = Input.GetAxisRaw("Horizontal");
        // 垂直输入（如果需要上下移动，取消注释）
        // float moveVertical = Input.GetAxisRaw("Vertical");
        
        Vector2 velocity = new Vector2(moveInput * currentSpeed, rb.linearVelocity.y);
        // 如果启用垂直移动：velocity = new Vector2(moveInput * currentSpeed, moveVertical * currentSpeed);
        
        rb.linearVelocity = velocity;
    }

    // 被追赶者撞到时调用
    public void TakeHitAndBoost()
    {
        if (isBoosting) return; // 已经在加速中，不再重复触发

        // 闪烁红色
        if (flash != null) flash.Flash(0.5f);

        // 启动加速协程
        StartCoroutine(BoostCoroutine());
    }

    private IEnumerator BoostCoroutine()
    {
        isBoosting = true;
        currentSpeed = boostSpeed;
        
        float boostEndTime = Time.time + boostDuration;

        // 等待直到距离足够或时间结束
        while (Time.time < boostEndTime)
        {
            if (chaser != null)
            {
                float distance = Vector2.Distance(transform.position, chaser.transform.position);
                if (distance >= boostDistance)
                    break; // 已经拉开足够距离，提前结束加速
            }
            yield return null; // 等待下一帧
        }

        currentSpeed = normalSpeed;
        isBoosting = false;
        Debug.Log("加速结束，恢复正常速度"); // 可在控制台验证
    }
}