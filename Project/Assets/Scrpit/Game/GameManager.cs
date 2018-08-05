using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static float RemainingTime = 0;

	void Start () {
        RemainingTime = 120f;
	}

    private void FixedUpdate()
    {
        //倒计时大于0时 倒计时
        if (RemainingTime > 0)
        {
            Timing();
        }
        //时间到时结束游戏
        else
        {
            RemainingTime = 0;
            GameOver();
        }
    }

    private void Timing()
    {
        RemainingTime -= Time.deltaTime;
    }

    private void GameOver()
    {
        //TODO 显示比分UI 禁用玩家功能 停止游戏
    }


}
