using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;  // 目标物体
    public float smoothSpeed = 0.125f;  // 平滑跟随的速度
    public Vector3 offset;  // 摄像机与目标之间的偏移量

    public float minX = -10f;  // X 轴最小值
    public float maxX = 10f;   // X 轴最大值

    void Start()
    {
        // 初始时，设定偏移量（可以在编辑器中设置）
        offset = transform.position - target.position;
    }

    void FixedUpdate()
    {
        // 计算目标位置
        Vector3 desiredPosition = target.position + offset;

        // 限制 X 轴范围
        desiredPosition.x = Mathf.Clamp(desiredPosition.x, minX, maxX);

        // 保持 Y 坐标不变
        desiredPosition.y = transform.position.y;

        // 平滑跟随
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // 更新摄像机位置
        transform.position = smoothedPosition;
    }
}