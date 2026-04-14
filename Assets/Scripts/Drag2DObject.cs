using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class Drag2DObject : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("拖拽设置")]
    [Tooltip("目标判定Tag名称，严格区分大小写")]
    public string targetTag = "Target";
    [Tooltip("拖拽时的层级，避免被其他物体遮挡")]
    public int dragSortOrder = 10;
    [Tooltip("忽略自身碰撞体，防止自检")]
    public bool ignoreSelf = true;

    // 私有变量
    private Vector3 originalPosition;
    private SpriteRenderer spriteRenderer;
    private int originalSortOrder;
    private Collider2D selfCollider;

    public SpriteRenderer spr;

    private void Awake()
    {
        // 获取核心组件
        spriteRenderer = GetComponent<SpriteRenderer>();
        selfCollider = GetComponent<Collider2D>();
        // 记录初始位置，用于回弹
        originalPosition = transform.position;
    }

    // 开始拖拽：提升层级+禁用自身碰撞
    public void OnBeginDrag(PointerEventData eventData)
    {
        // 提升排序层级，防止被遮挡
        if (spriteRenderer != null)
        {
            originalSortOrder = spriteRenderer.sortingOrder;
            spriteRenderer.sortingOrder = dragSortOrder;
        }
        // 禁用自身碰撞，避免检测到自己
        if (ignoreSelf && selfCollider != null)
        {
            selfCollider.enabled = false;
        }
    }

    // 拖拽中：跟随鼠标移动（修复Z轴偏移）
    public void OnDrag(PointerEventData eventData)
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0; // 固定2D平面，消除深度误差
        transform.position = mouseWorldPos;
    }

    // 结束拖拽：恢复状态+多物体Tag判定
    public void OnEndDrag(PointerEventData eventData)
    {
        // 恢复原始层级和碰撞体
        if (spriteRenderer != null)
            spriteRenderer.sortingOrder = originalSortOrder;
        if (ignoreSelf && selfCollider != null)
            selfCollider.enabled = true;

        // 获取鼠标落点所有碰撞体（解决多物体重叠问题）
        Vector2 checkPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D[] hitColliders = Physics2D.OverlapPointAll(checkPoint);

        // 标记是否匹配成功
        bool isMatchSuccess = false;
        List<string> hitTags = new List<string>();

        foreach (var hitCol in hitColliders)
        {
            hitTags.Add(hitCol.tag);
            // 筛选目标Tag
            if (hitCol.CompareTag(targetTag))
            {
                isMatchSuccess = true;
                break;
            }
        }

        // 判定结果处理
        if (isMatchSuccess)
        {
           
            this.gameObject.SetActive(false);
            spr.enabled = true;
            AllController.Instance.Add();
           
        }
        else
        {
            transform.position = originalPosition;
  
        }
    }
}
