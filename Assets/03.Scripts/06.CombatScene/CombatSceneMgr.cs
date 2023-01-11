using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatSceneMgr : MonoBehaviour
{
    public int _maxLevelValue;
    private int _currLevel;
    public float _obstacleBlockPercentage;
    public float _itemBlockPercentage;
    public Image _stageBackGroundImage;
    [Tooltip("�ش� Collection�� ũ��� _maxLevelValue�� ����� ��")]
    public List<GameObject> _monsterFormationByStage; // �ε������� ���� ���͵� �׷��� �־�����
}
