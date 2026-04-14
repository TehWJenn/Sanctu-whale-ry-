using UnityEngine;

public class Data : MonoBehaviour
{
    public static Data instance;
    public bool isData1 = false;
    public bool isData2 = false;
    public bool isData3 = false;

    public GameObject prop1;
    public GameObject prop2;
    public GameObject prop3;

    private void Awake()
    {
        instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Add1()
    {
        prop1.gameObject.SetActive(true);
        isData1 = true;
    }

    public void Add2()
    {
        prop2.gameObject.SetActive(true);
        isData2 = true;
    }

    public void Add3()
    {
        prop3.gameObject.SetActive(true);
        isData3 = true;
    }
}
