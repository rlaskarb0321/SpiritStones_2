using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatSceneMgr : MonoBehaviour, IGameFlow
{
    public int _maxLevelValue;
    public int _currLevel;
    public float _obstacleBlockPercentage;
    public float _itemBlockPercentage;
    public Image _stageBackGroundImage;
    [Tooltip("�ش� Collection�� ũ��� Max Level Value�� ����� ��")]
    public List<GameObject> _monsterFormationByStage; // �ε������� ���� ���͵� �׷��� �־�����

    public void DoGameFlowAction()
    {
        // eGameState.EnemyTurn �϶�
        DoEnemyAction();
    }

    void DoEnemyAction()
    {
        Debug.Log("EnemyTurn");
    }
}
