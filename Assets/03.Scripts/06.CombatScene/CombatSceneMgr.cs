using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatSceneMgr : MonoBehaviour
{
    public int _maxLevelValue;
    public int _currLevel;
    public float _obstacleBlockPercentage;
    public float _itemBlockPercentage;
    public Image _stageBackGroundImage;
    [Tooltip("�ش� Collection�� ũ��� Max Level Value�� ����� ��")]
    public List<GameObject> _monsterFormationByStage; // �ε������� ���� ���͵� �׷��� �־�����

    private void Start()
    {
        
    }

    IEnumerator DoEnemyAction()
    {
        yield return new WaitUntil(() => GameManager._instance._gameFlowQueue.Peek() == eGameFlow.EnemyTurn);
    }
}
