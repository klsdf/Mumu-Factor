using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CellGroup : MonoBehaviour
{
    public int cellNum;
    private int cellMax = 30;//所显示的最大数量

    [Header("里面填充的细胞")]
    public GameObject oneCell;//小细胞  
    //public GameObject bigCell;//大细胞 代表10个
    //private List<GameObject> tencell = new List<GameObject>();//采用对象池模式设计

    
    private List<GameObject> onecell = new List<GameObject>();
    private List<SpriteRenderer> onecellSpriteRenderers = new List<SpriteRenderer>();

    private CellController cellController;

    // 挂载目标（圆心）
    //private Transform target;
    // 移动范围
    public float Range = 50F;//暂时不用
    // Start is called before the first frame update
  


    void Start()
    {
        cellController = GetComponent<CellController>();
        //for (int i = 0; i < cellMax / 10; i++)
        //{
        //    Debug.Log("生成大细胞");
        //    GameObject newbigCell = GameObject.Instantiate(bigCell) as GameObject;
        //    tencell.Add(newbigCell);
        //    newbigCell.transform.SetParent(target);
        //    newbigCell.transform.localPosition = new Vector3(0, 0, 0);
        //}

        //对象池先缓存
        for (int i = 0; i < cellMax; i++)
        {
            //Debug.Log("生成小细胞");
            GameObject newoneCell = GameObject.Instantiate(oneCell) as GameObject;
            onecell.Add(newoneCell);
            //默认全部挂载到自己下面
            newoneCell.transform.SetParent(gameObject.transform);
            newoneCell.transform.localPosition = new Vector3(0, 0, 0);


            onecellSpriteRenderers.Add(newoneCell.GetComponent<SpriteRenderer>());
        }
        SetCell(0);
    }

    //为了视觉统一，全部用小细胞了
    public void SetCell(int Num)
    { //调用共有函数  设置种群数目
        cellNum = Num;
        for (int i = 0; i < cellMax; i++)
        {
            if (i < Num)
            {
                onecell[i].SetActive(true);
                onecell[i].GetComponent<CellMove>().Move();
            }
            else
            {
                onecell[i].SetActive(false);
            }


            //只有基因才会进行填色
            if (cellController.cellType == CellType.基因)
            {
                //更改颜色
                if (cellController.GetParty() == -1)
                {
                    onecellSpriteRenderers[i].color = Color.white;
                }
                else if (cellController.GetParty() == 0)
                {
                    onecellSpriteRenderers[i].color = Color.blue;
                }
                else
                {
                    onecellSpriteRenderers[i].color = Color.red;
                }
            }


 
            
        }

        //int p = cellNum % 10;
        //for (int i = 0; i < 10; i++)
        //{
        //    if (i < p)
        //    {
        //        onecell[i].SetActive(true);
        //        onecell[i].GetComponent<CellMove>().Move();
        //    }
        //    else { onecell[i].SetActive(false); }
        //}
        //for (int j = 0; j < cellMax / 10; j++)
        //{
        //    if (j < cellNum / 10)
        //    {
        //        tencell[j].SetActive(true);
        //        tencell[j].GetComponent<CellMove>().Move();
        //    }
        //    else { tencell[j].SetActive(false); }
        //}
    }
    //public void SetCell(int Num)
    //{ //调用共有函数  设置种群数目
    //    cellNum = Num;
    //    int p = cellNum % 10;
    //    for (int i = 0; i < 10; i++)
    //    {
    //        if (i < p)
    //        {
    //            onecell[i].SetActive(true);
    //            onecell[i].GetComponent<CellMove>().Move();
    //        }
    //        else { onecell[i].SetActive(false); }
    //    }
    //    for (int j = 0; j < cellMax / 10; j++)
    //    {
    //        if (j < cellNum / 10)
    //        {
    //            tencell[j].SetActive(true);
    //            tencell[j].GetComponent<CellMove>().Move();
    //        }
    //        else { tencell[j].SetActive(false); }
    //    }
    //}
    // Update is called once per frame
    void Update()
    {

    }
}
