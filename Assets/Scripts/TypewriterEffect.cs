using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TypewriterEffect : MonoBehaviour
{
    public Text displayText;       // 需要显示的文本组件
    public string fullText = "";   // 完整的文本
    public float typingSpeed = 0.1f; // 每个字符打字的间隔时间

    private bool isTyping = false;  // 判断是否正在打字
    private bool isComplete = false; // 判断是否已打完文本
    private Coroutine typingCoroutine;  // 用于存储协程，以便可以在需要时停止

    public bool isNeedNext = false;
    public GameObject next;

    public int index = 1;

    void Start()
    {
        // 初始化文本为空
        displayText.text = "";
        typingCoroutine = StartCoroutine(TypeText());  // 启动协程
    }

    // 打字机效果协程
    private IEnumerator TypeText()
    {
        isTyping = true;
        foreach (char letter in fullText)
        {
            displayText.text += letter;  // 逐个字母显示
            yield return new WaitForSeconds(typingSpeed); // 等待指定的时间
        }
        isTyping = false;
        isComplete = true; // 打字完成
    }

    // 显示完整文本
    public void ShowFullText()
    {
        // 如果文本已经完成打字，则输出debug xxx
        if (isComplete)
        {
            if (isNeedNext)
            {
                next.SetActive(true);
            }
            else
            {
                Show();
            }
        
            this.gameObject.SetActive(false);
        }
        else
        {
            // 如果文本没有完全显示，直接设置为完整文本
            displayText.text = fullText;
            // 取消打字效果协程
            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine);  // 停止协程，避免继续打字
            }
            isComplete = true;
        }
    }

    public void Show()
    {
        if (index == 1)
        {
            Data.instance.Add1();
        }
        else if (index == 2)
        {
            Data.instance.Add2();
        }
        else
        {
            Data.instance.Add3();

        }
    }
}