using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable] // ����ȭ

public class GameData
{
    // �� é���� ��ݿ���
    public int nowGold =0;
    public int[] nowGems = { 0, 0, 0, 0, 0, 0 };
    public bool[] stageClearCheck = { false, false, false, false, false, false, false, false };
}