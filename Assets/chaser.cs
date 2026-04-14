using UnityEngine;

public class Chaser : MonoBehaviour
{
    [Header("追逐设置")]
    public Transform player;
    public float baseSpeed = 5f;
    public float swayStrength = 1.0f;      // 摆动强度（Y轴随机偏移大小）
    public float swayFrequency = 0.5f;     // 摆动频率（秒/次）

    private Rigidbody2D rb;
    private float timer;
    private float currentSway;              // 当前的Y轴偏移量

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) player = p.transform;
            else Debug.LogError("未找到 Tag 为 Player 的对象");
        }
        // 随机初始化偏移方向
        currentSway = Random.Range(-swayStrength, swayStrength);
    }

    void FixedUpdate()
    {
        if (player == null) return;

        // 定时随机改变Y轴偏移量
        timer += Time.fixedDeltaTime;
        if (timer >= swayFrequency)
        {
            timer = 0f;
            currentSway = Random.Range(-swayStrength, swayStrength);
        }

        // 计算指向玩家的方向（单位向量）
        Vector2 directionToPlayer = ((Vector2)player.position - rb.position).normalized;

        // 在垂直于运动方向上加一个Y轴偏移（实际是构造一个随机偏移向量，主要影响Y轴）
        // 方法：保持方向指向玩家，但额外加上一个随机的垂直偏移
        Vector2 perpendicular = new Vector2(-directionToPlayer.y, directionToPlayer.x); // 垂直方向
        Vector2 randomOffset = perpendicular * currentSway;
        
        // 最终移动方向 = 指向玩家 + 垂直偏移，然后归一化保持速度稳定
        Vector2 moveDir = (directionToPlayer + randomOffset).normalized;
        
        // 移动
        Vector2 newPos = rb.position + moveDir * baseSpeed * Time.fixedDeltaTime;
        rb.MovePosition(newPos);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController pc = other.GetComponent<PlayerController>();
            if (pc != null)
                pc.TakeHitAndBoost();
            else
            {
                PlayerFlash flash = other.GetComponent<PlayerFlash>();
                if (flash != null) flash.Flash(0.5f);
            }
        }
    }
}