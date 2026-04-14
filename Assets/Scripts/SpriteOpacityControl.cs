using UnityEngine;
using UnityEngine.EventSystems;

// 支持鼠标点击+悬停事件接口
public class SpriteOpacityControl : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private SpriteRenderer spriteRenderer;
    private Color originalColor;


    [Header("透明度设置")]
    [Tooltip("鼠标进入时的透明度值 (0~1)")]
    public float hoverOpacity = 0.5f;

    void Start()
    {
        // 获取Sprite渲染组件
        spriteRenderer = GetComponent<SpriteRenderer>();
        // 备份初始颜色（含透明度）
        if (spriteRenderer != null)
            originalColor = spriteRenderer.color;
    }

    // 鼠标移入
    public void OnPointerEnter(PointerEventData eventData)
    {
        SetOpacity(hoverOpacity);
    }

    // 鼠标移出
    public void OnPointerExit(PointerEventData eventData)
    {
        SetOpacity(originalColor.a);
    }

    // 点击事件（预留，可扩展）
    public void OnPointerClick(PointerEventData eventData)
    {
    }

    // 封装透明度设置（防空引用报错）
    private void SetOpacity(float alpha)
    {
        if (spriteRenderer == null) return;
        Color tempColor = spriteRenderer.color;
        tempColor.a = alpha;
        spriteRenderer.color = tempColor;
    }

 
}