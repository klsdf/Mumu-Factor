using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellMove : MonoBehaviour
{
    //0.4 1 -1 3
    public float Range = 50F;

    public float SpeedMax = 50F;
    public float SpeedMin = -50F;

    public float IntervalTime = 3F;
    private Transform mytransform;
    private Rigidbody2D rigidbody;
    // Start is called before the first frame update
    void Awake()
    {
        mytransform = this.gameObject.transform;
        rigidbody = this.gameObject.GetComponent<Rigidbody2D>();
    }
    public void Move()
    {
        StopAllCoroutines();
        StartCoroutine(RandomMove());
    }
    IEnumerator RandomMove()
    {
        rigidbody.velocity = new Vector3(Random.Range(SpeedMin, SpeedMax), Random.Range(SpeedMin, SpeedMax), 0);

        // 等待时间重新随机位置
        yield return new WaitForSeconds(IntervalTime);

        StartCoroutine(RandomMove());
    }
    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(mytransform.localPosition, new Vector3(0, 0, 0)) > Range)
        { rigidbody.velocity = new Vector3(0, 0, 0) - mytransform.localPosition; } //碰撞弹回 改 这里可以乘以一个速度系数
    }
}
