using UnityEngine;

public class AllController : MonoBehaviour
{
    public static AllController Instance;

    public int count = 0;
    public GameObject win;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Add()
    {
        count += 1;
        if (count >= 3)
        {
            win.gameObject.SetActive(true);
        }
    }
}
