using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MemeController : MonoBehaviour
{

    public MemeType memeType;


    public List<MemeType> memeTypes = new List<MemeType>();


    private bool isMemeState;//是否是meme状态
    //meme所停留的时间
    private float stayTime = 5;//停留10s
    private float stayColdTime;

    private float getMemeColdTime;
    private float getMemeTime = 5;//每5s有机会获得一个meme



    private void Update()
    {
        //每隔5s中，有10%概率获得一个模因

      

        getMemeColdTime += Time.deltaTime;
        if (getMemeColdTime >= getMemeTime)
        {
            if (isMemeState == false) 
            {
                GetMeme();
            }
            
            getMemeColdTime = 0;
        }


        if (isMemeState == true)
        {
            stayColdTime += Time.deltaTime;

            if (stayColdTime >= stayTime)
            {
                LoseMeme();

                stayColdTime = 0;
            }


        }




    }

    private void LoseMeme()
    {
        memeType = MemeType.无;

        TMP_Text tempText = transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>();
        tempText.text = memeType.ToString();
        isMemeState = false;


        GetComponent<CellController>().RestoreAllData();
    }

    private void GetMeme()
    {

        //30%概率可以获得meme
        int p = Random.Range(0, 100);
        if (p > 30)
        {
            return;
        }


        memeType = memeTypes[Random.Range(0,memeTypes.Count)];

        switch (memeType)
        {
            case MemeType.丁克主义:

                GetComponent<CellController>().StopCellGrow();
                break;
            case MemeType.悲观主义:
                GetComponent<CellController>().Suicide();
                break;
            case MemeType.懒惰主义:
                GetComponent<CellController>().Lazy();
                break;


        }


        TMP_Text tempText = transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>();
        tempText.text = memeType.ToString();
        isMemeState = true;


       
    }



}
