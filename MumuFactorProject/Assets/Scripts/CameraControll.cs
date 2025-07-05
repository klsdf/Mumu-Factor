using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControll : MonoBehaviour
{
    private float moveSpeed = 5;
    //public Vector3 startV;
    public float maxX;
    public float maxY;
    public float minX;
    public float minY;


    public Vector3 MaxV = new Vector3(10,5,0);
    public Vector3 MinV = new Vector3(-25,5,0);

    [Header("摄像机视野")]
    public float maxSize=5; //5
    public float minSize=1; //1
    //public float cSize;

    private Vector3 position;



    Vector3 lastMousePosition;
    Vector3 nowMousePosition;
    bool isMouseDown;

    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float x =  Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");


        //键盘控制镜头移动
        if ( y>0  && position.y < maxY)
        {
            position.y += moveSpeed * Camera.main.orthographicSize * Time.deltaTime;

        }
        else if (y<0 && position.y > minY)

        {
            position.y -= moveSpeed * Camera.main.orthographicSize * Time.deltaTime;

        }

        if (x<0 && position.x > minX)
        {
            position.x -= moveSpeed * Camera.main.orthographicSize * Time.deltaTime;

        }

        else if (x>0 && position.x < maxX)
        {
            position.x += moveSpeed * Camera.main.orthographicSize * Time.deltaTime;
   
        }
        transform.position = position;


        ////鼠标中键拖动画面
        //if (Input.GetMouseButtonDown(2))
        //{
        //    isMouseDown = true;
        //}
        //if (Input.GetMouseButtonUp(2))
        //{
        //    isMouseDown = false;
        //    lastMousePosition = Vector3.zero;
        //}
        //if (isMouseDown)
        //{
        //    nowMousePosition = Input.mousePosition;

        //    if (lastMousePosition != Vector3.zero)
        //    {
        //        Vector3 offset = nowMousePosition - lastMousePosition;
        //        transform.position = transform.position - offset * 0.05f;


        //    }
        //    lastMousePosition = nowMousePosition;

        //}



        //鼠标控制缩放
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (Camera.main.orthographicSize <= maxSize)
                Camera.main.orthographicSize += 0.5f;
        }
        //Zoom in
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (Camera.main.orthographicSize >= minSize)
                Camera.main.orthographicSize -= 0.5F;
        }
        if (Input.GetKey(KeyCode.Space))
        {


        }
    }
}
