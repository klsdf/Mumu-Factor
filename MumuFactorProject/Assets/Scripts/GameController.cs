using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : Singleton<GameController>
{
    public BasePanel 胜利面板;
    public BasePanel 失败面板;

    private Transform selectionBox;
    private Camera mainCamera;

    public GameObject playerCell;
    public GameObject enemyCell;

    public MemeType[] nowMemeTypes;

    private CellController[] cells;

    /// <summary>
    /// 获得不同阵营的细胞
    /// </summary>
    /// <param name="party">-1代表中立，0代表玩家，1之后代表敌人</param>
    /// <returns>返回该阵营的所有细胞</returns>
    public CellController[] GetCellsByParty(int party)
    {
        List<CellController> tempCells = new List<CellController>();
        foreach (CellController cell in cells)
        {
            if(cell.GetParty() == party)
                tempCells.Add(cell);
        }

        return tempCells.ToArray();
    }


    public CellController GetMaxGeneumCell(int party)
    {
        CellController[] cells =     GetCellsByParty(party);
        if (cells.Length == 0)
        {
            return null;
        }
        CellController maxGeneCell = cells[0];
        foreach (CellController cell in cells)
        {
            if (cell.GetGeneNum() > maxGeneCell.GetGeneNum())
            {
                maxGeneCell = cell;
            }
        }
        return maxGeneCell;
    }


    public CellController GetMinGeneumCell(int party)
    {
        CellController[] cells = GetCellsByParty(party);

        if (cells.Length == 0)
        {
            return null;
        }

        CellController mineGeneCell = cells[0];
        foreach (CellController cell in cells)
        {
            if (cell.GetGeneNum() < mineGeneCell.GetGeneNum())
            {
                mineGeneCell = cell;
            }
        }
        return mineGeneCell;
    }


    

    //当前选中的细胞们
    private List<GameObject> selectedCell;

    public List<GameObject> getSelectCell()
    {
        return selectedCell;
    }

    private void CheckGameOver()
    {

        int geneNum = GameObject.FindGameObjectsWithTag("Gene").Length;
        //没有敌人的话，胜利
        if (GetCellsByParty(1).Length == 0  && geneNum == 0)
        {
            UISystem.Instance.PushPanel(胜利面板);

        }else if(GetCellsByParty(0).Length == 0 && geneNum==0)//玩家没有的话G
        {
            UISystem.Instance.PushPanel(失败面板);
        }
    }


    private  new void Awake()
    {
        mainCamera = Camera.main;
        var temp = Resources.Load<GameObject>("SelectionBox");
        selectionBox = Instantiate(temp).transform;
    }

    void Start()
    {
        playerCell.GetComponent<CellController>().changeParty(0);
        enemyCell.GetComponent<CellController>().changeParty(1);

        if (SaveSystem.Instance.playerType == PlayerType.高初始值)
        {
            playerCell.GetComponent<CellController>().ChangeGeneNum(30);
        }
        else if (SaveSystem.Instance.playerType == PlayerType.高速度)
        {
            playerCell.GetComponent<CellController>().geneSpeed += 1;
        }





        cells = FindObjectsOfType<CellController>();
    }


    /// <summary>
    /// 这是一个自定义类，用来表示一个生效的选定框；创建它时会自动算出包含四角坐标的四项数据
    /// </summary>
    public class Selector
    {
        public float Xmin;
        public float Xmax;
        public float Ymin;
        public float Ymax;

        //构造函数，在创建选定框时自动计算Xmin/Xmax/Ymin/Ymax
        public Selector(Vector3 start, Vector3 end)
        {
            Xmin = Mathf.Min(start.x, end.x);
            Xmax = Mathf.Max(start.x, end.x);
            Ymin = Mathf.Min(start.y, end.y);
            Ymax = Mathf.Max(start.y, end.y);
        }
    }

    private bool onDrawingRect;//是否正在画框(即鼠标左键处于按住的状态)

    private Vector3 startPoint;//框的起始点，即按下鼠标左键时指针的位置
    private Vector3 currentPoint;//在拖移过程中，玩家鼠标指针所在的实时位置
    private Vector3 endPoint;//框的终止点，即放开鼠标左键时指针的位置

    private Selector selector;

    void Update()
    {
        //如果当前的场景名为“开始界面”，那么不进行任何操作
        if (SceneManager.GetActiveScene().name == "开始界面")
        {
            return;
        }
        CheckGameOver();

        //玩家按下鼠标左键，此时进入画框状态，并确定框的起始点
        if (Input.GetMouseButtonDown(0))
        {
            onDrawingRect = true;
            startPoint = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            selectionBox.gameObject.SetActive(true);
        }

        //在鼠标左键未放开时，实时记录鼠标指针的位置
        if (Input.GetMouseButton(0))
        {
            currentPoint = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            UpdateSelectionBox();
        }

        //玩家放开鼠标左键，说明框画完，确定框的终止点，退出画框状态
        if (Input.GetMouseButtonUp(0))
        {
            selectionBox.gameObject.SetActive(false);
            endPoint = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            onDrawingRect = false;

            //当框画完时，创建一个生效的选定框selector
            selector = new Selector(startPoint, endPoint);
            //执行框选事件
            CheckSelection(selector);
            
        }
    }
    void UpdateSelectionBox()
    {
        Vector2 boxStart = startPoint;
        Vector2 boxEnd = currentPoint;

        Vector2 boxCenter = (boxStart + boxEnd) / 2f;
        selectionBox.position = boxCenter;

        Vector2 boxSize = new Vector2(Mathf.Abs(boxStart.x - boxEnd.x), Mathf.Abs(boxStart.y - boxEnd.y));
        selectionBox.localScale = boxSize;
    }

    //框选事件
    //按照选定框的范围，捕获标签为tag的所有物体，并打印这些物体的名字
    void CheckSelection(Selector selector)
    {
        List<GameObject> tempGameObjects = new List<GameObject>();
        GameObject[] Units = GameObject.FindGameObjectsWithTag("Cell");
        foreach (GameObject Unit in Units)
        {
            Vector3 objPosition = Unit.transform.position;
            //Vector3 screenPos = Camera.main.WorldToScreenPoint(Unit.transform.position);
            if (objPosition.x > selector.Xmin && objPosition.x < selector.Xmax && objPosition.y > selector.Ymin && objPosition.y < selector.Ymax)
            {
                //只有玩家才能被选中
                if (Unit.GetComponent<CellController>().GetParty() == 0)
                {
                    //Debug.LogFormat("已选中目标:{0}", Unit.name);
                    tempGameObjects.Add(Unit);
                    Unit.SendMessage("beSelected");
                }
 

            }
        }

        selectedCell = tempGameObjects;
    }

    



    }




