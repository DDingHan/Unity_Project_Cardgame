using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable] // 직렬화

public class GameData
{
    // 각 챕터의 잠금여부
    public int nowGold =0;
    public int[] nowGems = { 0, 0, 0, 0, 0, 0 };
    public bool[] stageClearCheck = { false, false, false, false, false, false, false, false };
}