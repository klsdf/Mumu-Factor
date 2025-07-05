using UnityEngine;
using System.IO;
//JSON头文件
using LitJson;



public class SaveSystem : MonoBehaviour
{
    //单例
    static SaveSystem instance;
    public static SaveSystem Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindFirstObjectByType<SaveSystem>();

            }
            return instance;
        }
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        saveFilePath = Application.dataPath + "/Save" + "/save.txt";
    }


    //存储文件的位置
    private string saveFilePath;


    [Header("关卡数目")]
    public int levelNum = 1;


    public PlayerType playerType;




    /// <summary>
    /// 从游戏中获得存储信息，并创建存储对象
    /// </summary>
    /// <returns>存储对象</returns>
    private SaveData GetGameData()
    {
        SaveData saveData = new SaveData();
        saveData.levelNum = levelNum;

        return saveData;
    }


    private void InitData()
    {
        SaveData saveData = new SaveData();
        saveData.levelNum = 10;
 ;

        //利用JsonMapper将save对象转换为Json格式的字符串
        string saveJsonStr = JsonMapper.ToJson(saveData);
        //将这个字符串写入到文件中
        //创建一个StreamWriter，并将字符串写入文件中
        StreamWriter sw = new StreamWriter(saveFilePath);
        sw.Write(saveJsonStr);
        //关闭StreamWriter
        sw.Close();

        print("初始化成功");
    }

    /// <summary>
    /// 将某个存储对象的数据加载到游戏中
    /// </summary>
    private void SetGameData(SaveData saveData)
    {
        levelNum = saveData.levelNum;
        //print("获得数据！" + saveData.levelNum);
    }


    public void SaveByJson()
    {
        SaveData save = GetGameData();

        //利用JsonMapper将save对象转换为Json格式的字符串
        string saveJsonStr = JsonMapper.ToJson(save);
        //将这个字符串写入到文件中
        //创建一个StreamWriter，并将字符串写入文件中
        StreamWriter sw = new StreamWriter(saveFilePath);
        sw.Write(saveJsonStr);
        //关闭StreamWriter
        sw.Close();

        print("保存成功");
    }

    public void LoadByJson()
    {

        if (File.Exists(saveFilePath))
        {
            //创建一个StreamReader，用来读取流
            StreamReader sr = new StreamReader(saveFilePath);
            //将读取到的流赋值给jsonStr
            string jsonStr = sr.ReadToEnd();
            //关闭
            sr.Close();

            //将字符串jsonStr转换为Save对象
            SaveData save = JsonMapper.ToObject<SaveData>(jsonStr);
            SetGameData(save);
            //print("保存成功");
        }
        else
        {
            print("存档文件不存在");
            //创建存档文件夹save
            Directory.CreateDirectory(Application.dataPath + "/Save");
            InitData();
            LoadByJson();
        }
    }

}

