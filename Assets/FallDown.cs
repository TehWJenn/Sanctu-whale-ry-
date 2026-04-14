using UnityEngine;

public class FallDown : MonoBehaviour
{
    public float fallSpeed = 2f;       // How fast it falls
    public float startYOffset = 2f;    // How far above the camera it starts
    public float destroyYOffset = 2f;  // How far below camera before it's destroyed

    private Camera mainCam;
    private float topEdge;
    private float bottomEdge;

    void Start()
    {
        mainCam = Camera.main;

        // Calculate the top and bottom edges of the camera view in world space
        topEdge = mainCam.transform.position.y + mainCam.orthographicSize + startYOffset;
        bottomEdge = mainCam.transform.position.y - mainCam.orthographicSize - destroyYOffset;

        // Spawn the sprite above the camera
        Vector3 startPos = transform.position;
        startPos.y = topEdge;
        transform.position = startPos;
    }

    void Update()
    {
        // Move downward every frame
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);

        // Destroy the object once it goes below the camera view
        if (transform.position.y < bottomEdge)
        {
            Destroy(gameObject);
        }
    }
}
