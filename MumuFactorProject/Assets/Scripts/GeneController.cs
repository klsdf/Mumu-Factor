using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GeneController : MonoBehaviour
{



    //发射源
    public GameObject source;
    public GameObject target;

    //[Header("玩家材质")]
    //public Material playerMaterial;
    //[Header("敌人材质")]
    //public Material enemyMaterial;

    public float speed;
    public float geneNum;
    public int party;



    /// <summary>
    /// 基因的初始化函数
    /// </summary>
    /// <param name="target">基因攻击的目标</param>
    /// <param name="speed">基因移动速度</param>
    /// <param name="cellNum">所携带的基因数量</param>
    //public void Init(GameObject source,GameObject target,float speed,float geneNum,int party)
    //{
    //    this.source = source;
    //    this.target = target;
    //    this.speed = speed;

    //    this.geneNum = geneNum;
    //    this.party = party;

    //}

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Init(GameObject source, GameObject target, float speed, int party ,CellType cellType)
    {
        this.source = source;
        this.target = target;
        this.speed = speed;

        this.geneNum = 1;
        this.party = party;


        if (cellType == CellType.基因)
        {
            if (party == 1)
            {
                GetComponent<SpriteRenderer>().color = Color.red;
                //GetComponent<SpriteRenderer>().material = enemyMaterial;

            }
            if (party == 0)
            {
                GetComponent<SpriteRenderer>().color = Color.blue;
                //GetComponent<SpriteRenderer>().material = playerMaterial;
            }
        }
        else {

            if (party == 1)
            {
                GetComponent<SpriteRenderer>().color = new Color(200/255f,200/255f,255/255f);
                //GetComponent<SpriteRenderer>().material = enemyMaterial;

            }
            if (party == 0)
            {
                GetComponent<SpriteRenderer>().color = new Color(255 / 255f, 200 / 255f, 200 / 255f);
                //GetComponent<SpriteRenderer>().material = playerMaterial;
            }
        }


    }



    // Update is called once per frame
    void Update()
    {
        //print(target.name+"，"+transform.name);
        Vector3 direction = (target.transform.position - transform.position);
        if (direction.magnitude <= 0.05f)
        {
            print("太近了！");
            Destroy(gameObject);
        }

        transform.Translate(direction.normalized* speed * Time.deltaTime);


       
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
      

        CellController cell =   collision.GetComponent<CellController>();
        //如果基因打到的不是细胞，直接跳过
        if (cell == null)
        {
            return;
        }

        //如果碰到的细胞就是自己，跳过
        if (collision.gameObject == source)
        {
            return;
        }

        //如果是相同阵营的细胞，则给自己人加资源
        if (cell.GetParty() == party)
        {
            collision.SendMessage("GetHealth", geneNum);
            Destroy(gameObject);
            return;
        }
        //否则就减少资源
        else
        {
            object[] para = new object[2];
            para[0] = geneNum;
            para[1] = party;


            collision.SendMessage("GetDamage", para);

            //print(collision.name);
            Destroy(gameObject);
        }




    }
}
