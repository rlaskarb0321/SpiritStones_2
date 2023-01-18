using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatSceneMgr : MonoBehaviour, IGameFlow
{
    [Header("=== Stage ===")]
    public int _maxLevelValue;
    public int _currLevel;
    public float _obstacleBlockPercentage;
    public float _itemBlockPercentage;
    public Image _stageBackGroundImage;
    [Tooltip("�ش� Collection�� ũ��� Max Level Value�� ����� ��")]
    public List<GameObject> _monsterFormationByStage; // �ε������� ���� ���͵� �׷��� �־�����

    [Header("=== target ===")]
    public GameObject _heroGroup;

    public void DoGameFlowAction()
    {
        // eGameState.EnemyTurn �϶�
        DoEnemyAction();
    }

    void DoEnemyAction()
    {
        GameManager._instance._gameFlow = eGameFlow.InProgress;

        // ���������� ���� �׷�
        GameObject currLevelMonsterFormation = _monsterFormationByStage[_currLevel - 1];
        for (int i = 0; i < currLevelMonsterFormation.transform.childCount; i++)
        {
            Transform monsterPos = currLevelMonsterFormation.transform.GetChild(i); // ���������� ���� �׷�� ������ ��ġ
            EnemyBase monster = monsterPos.transform.GetChild(0).GetComponent<EnemyBase>(); // �� �������ִ� ���͵�

            if (monster._state != EnemyBase.eState.Die)
                monster.DoMonsterAction(_heroGroup); 
        }
        GameManager._instance._gameFlow = eGameFlow.EnemyTurn;
        GameManager._instance._gameFlow++;
    }
}
