using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.EventSystems;

using DG.Tweening;



public class CellController : MonoBehaviour,IPointerClickHandler
{

    //材质们
    public Material neutralMaterial;//中立材质
    public Material playerMaterial;//玩家材质
    public Material enemyMaterial;//敌人材质


    //阵营
    //-1代表中立，0代表玩家，1代表敌人
    private int party=-1;

    //数据部分
    public float geneNum;//基因数量

    //float attack;//攻击力
    //float defence;//防御力

    public float geneGrowTime=0.5f;//增殖一次所需的时间
    public float geneGrowNum=1;//增殖一次的数量
    public float geneSpeed=3.0f;//基因移动速度
    public float maxGeneNum=100;//基因的最大数量




    //游戏中实际上所使用的数据
    private float trueGeneGrowTime;
    private float truegeneGrowNum;
    private float truegeneSpeed;
    private float truemaxGeneNum;


    float growColdTime;//增殖的冷却时间

    public CellType cellType;

    //细胞群

    private CellGroup cellGroup;

    //UI控制
    private TMP_Text numText;
    private Transform myTransform;
    //组件
    private SpriteRenderer spriteRenderer;



    private AudioSource audioSource;

    void Awake()
    {
        //获取组件
        numText = GetComponentInChildren<TMP_Text>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        cellGroup = GetComponent<CellGroup>();

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = Resources.Load<AudioClip>("SE/气泡");
        audioSource.loop = false;
        audioSource.playOnAwake = false;


        //GetHealth(100);
    }

    void Start()
    {

        //cell = new Cell(CellType.均衡细胞);
        //ChangeGeneNum(50);
        myTransform = transform;

        //初始赋值
        RestoreAllData();
    }

    ////////////////////////////////////////////////MEME用的

    /// <summary>
    /// 将所有数值重置
    /// </summary>
    public void RestoreAllData()
    {
     
        trueGeneGrowTime = geneGrowTime;
        truegeneGrowNum = geneGrowNum;
        truegeneSpeed = geneSpeed;
        truemaxGeneNum = maxGeneNum;
    }


    public void StopCellGrow()
    {
        truegeneGrowNum = 0;
    }


    public void Suicide()
    {
        truegeneGrowNum = -0.5f;
    }

    public void Lazy()
    {
        truegeneSpeed = 0.5f;
    }



    void Update()
    {
        //如果是中立的细胞，则不会增加自身的基因，直接退出函数
        if(party == -1)
        {
            return;
        }

        growColdTime += Time.deltaTime;
        if (growColdTime >= trueGeneGrowTime)
        {
            ChangeGeneNum(truegeneGrowNum);
             growColdTime = 0;
        }
    }


    //发射基因
    public void Attack(GameObject enemy)
    {
        //if (party == 0)
        //{
        //    print("玩家");
        //    print(enemy.name);
        //}

        //如果基因数量太少则不能攻击
        if(geneNum<=10)
        {
            return;
        }

        float halfGeneNum = geneNum / 2;

        GameObject tempGene = null;
        ChangeGeneNum(-halfGeneNum);
        switch (cellType)
        {
            case CellType.基因:
                tempGene = Resources.Load<GameObject>("发射基因/发射基因原型");
                break;
            case CellType.均衡细胞:
                tempGene = Resources.Load<GameObject>("发射基因/细胞/小黄");
                break;

            case CellType.大容量细胞:
                tempGene = Resources.Load<GameObject>("发射基因/细胞/小蓝");
                break;
            case CellType.高产率细胞:
                tempGene = Resources.Load<GameObject>("发射基因/细胞/小绿");
                break;
            case CellType.高速度细胞:
                tempGene = Resources.Load<GameObject>("发射基因/细胞/小红");
                break;
            case CellType.高产量细胞:
                tempGene = Resources.Load<GameObject>("发射基因/细胞/小粉");
                break;
            default:
                print("发现没有对应细胞！");
                tempGene = Resources.Load<GameObject>("发射基因/基因原型");
                break;
        }
       StartCoroutine(CreateGenes(tempGene,enemy,(int)halfGeneNum,cellType));
    }


    IEnumerator CreateGenes(GameObject gene, GameObject enemy, int num,CellType cellType)
    {

        
        for (int i = 0; i < num; i++)
        {
            //print(gene);
            //print(gameObject);
            //print(enemy);
            gene.GetComponent<GeneController>().Init(gameObject, enemy, truegeneSpeed, party, cellType);
            Vector3 offPosition = new Vector3(Random.Range(0,0.3f), Random.Range(0,0.3f),0);
            Instantiate(gene, transform.position+offPosition, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(0,0.1f));
        }
    }



    /// <summary>
    /// 更改细胞的阵营
    /// </summary>
    public void changeParty(int newParty)
    {
        audioSource.Play();
        if (newParty == 0)
        {
            spriteRenderer.material = playerMaterial;
           // spriteRenderer.color =  new Color(64/255f,201 / 255f, 255 / 255f);
        }
        else if(newParty == 1)
        {
            spriteRenderer.material = enemyMaterial;
            //spriteRenderer.color = new Color(255 / 255f, 122 / 255f, 122 / 255f);
        }
        party = newParty;
    }


    public int GetParty()
    {
        return party;
    }


    public float GetGeneNum()
    {
        return geneNum;
        
    }

    /// <summary>
    /// 细胞遭受攻击
    /// </summary>
    /// <param name="para">0为基因数值，1为阵营</param>
    public void GetDamage(object[] para)
    {
        //if (party == 0)
        //{
        //    print("受到攻击"+para[1]);
        //}

        float damage =float.Parse(  para[0].ToString());
        int newparty = int.Parse(para[1].ToString());


        ChangeGeneNum(-damage);
        if (geneNum<=0)
        {

            changeParty(newparty);
        }
    }

    public void GetHealth(float health)
    {
        ChangeGeneNum(health);

    }


    public void ChangeGeneNum(float num)
    {
        geneNum = Mathf.Clamp(geneNum + num, 0, truemaxGeneNum);

        //更改细胞群数量
        cellGroup.SetCell((int)geneNum);
        numText.text = geneNum.ToString("f0");//不保留小数
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            //print("左");
            //GameController.Instance.selectedCell = gameObject;

        }

        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            


            List<GameObject> selectCells = GameController.Instance.getSelectCell();

            for (int i = 0; i < selectCells.Count; i++)
            {
                //不能攻击自己
                if (selectCells[i] == gameObject)
                    continue;

                selectCells[i].GetComponent<CellController>().Attack(gameObject);
            }


           // selectCell.Attack(gameObject);
            ////如果对方的细胞不是自己阵营的话，那么直接消耗一般的兵力
            //if (GameController.Instance.selectedCell.GetComponent<CellController>().party != party)
            //{
   

            //}
        }

        else if (eventData.button == PointerEventData.InputButton.Middle)
        { 
        }


    }


    public void beSelected()
    {
        
        Sequence s = DOTween.Sequence();
        s.Append(transform.DOScale(new Vector3(0.5f,0.5f,1f),0.2f));
        s.Append(transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f));
    }


    private void OnMouseEnter()
    {
        //if (GameController.Instance.getSelectCell().Count >= 1 && party != 0)
        //{

        //    transform.DOPunchPosition(new Vector3(0, 0.1f, 0), 1f,3,90,false);
        //}
    }
}
