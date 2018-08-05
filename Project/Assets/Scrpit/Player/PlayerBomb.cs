using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBomb : MonoBehaviour {

    public GameObject Bomb;//将要放置的炸弹Prefab

    public float BombCD = 2f; //炸弹放置冷却时间
    public int PlayerID = 1; //玩家ID
    public float CurrentBombArea = 0.7f; //当前炸弹爆炸范围
    public float BuffTime = 5f; //加强时间
    public float BuffValue = 1; //加强数值（此处为爆炸范围）


    private bool BombAvaliable = false; //当前是否允许放置炸弹
    private float SetBombTiming; //放置炸弹计时器

    private bool IsBuffing = false; //当前是否加强
    private float BuffTiming; //加强计时器

	void Start () {
        BombAvaliable = true;
        IsBuffing = false;
        SetBombTiming = 0;
        BuffTiming = 0f;
	}

    // Update is called once per frame
    private void FixedUpdate()
    {
        Timing();//技能计时
        if (Input.GetButton("Fire1") && BombAvaliable) //用户选择放置炸弹
        {
            SetBomb();//放置炸弹
        }
    }

    //如果玩家选择放置炸弹并且技能不在CD
    private void SetBomb()
    {
        //创建炸弹
        GameObject newBomb = Instantiate(Bomb, new Vector3((transform.position.x), -0.15f, (transform.position.z)), gameObject.transform.rotation);
        BombAvaliable = false;
        newBomb.GetComponent<BombManager>().SetBombInfo(PlayerID, CurrentBombArea);//设置炸弹的基本信息
    }

    //计时器 包括炸弹放置和技能计时
    private void Timing()
    {
        if (!BombAvaliable)//技能冷却时计时
        {
            SetBombTiming += Time.deltaTime;
        }
        if(SetBombTiming >= BombCD)//冷却时间到时设置炸弹可用 并重置计时器
        {
            BombAvaliable = true;
            SetBombTiming = 0;
        }

        if (IsBuffing)//如果存在加强炸弹 则进行计时
        {
            BuffTiming += Time.deltaTime;
        }
        if(BuffTiming >= BuffTime) //加强时间到 取消加强
        {
            IsBuffing = false;
            CurrentBombArea -= BuffValue;
            BuffTiming = 0;
        }
    }

    //加强技能 在PlayerScoreManager内调用
    public void IncreaseBombArea()
    {
        IsBuffing = true;
        CurrentBombArea += BuffValue;
    }

}
