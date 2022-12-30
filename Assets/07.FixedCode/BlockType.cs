using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eBlockType
{
    Normal,
    Item,
    Obstacle,
    Count,
}

public enum eNormalBlockType
{
    Warrior,
    Archer,
    Thief,
    Magician,
    None,
}

public enum eSpecialBlockType
{
    // �Ϲ� ������
    Arrow_Archer,
    Bomb_Thief,
    Potion_Magician,
    Sword_Warrior,
    // ���� ������
    DoubleArrow_Archer,
    Dynamite_Thief,
    Elixir_Magician,
    DualSword_Warrior,
    None,
}