using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatSceneMgr : MonoBehaviour, IGameFlow
{
    [Header("=== Stage ===")]
    public int _maxLevelValue;
    public int _currLevel;
    public float _itemBlockPercentage;
    public Image _stageBackGroundImage;

    [Tooltip("�Ʒ��� �� Collection�� ũ��� Max Level Value�� ����� ��")]
    public List<GameObject> _monsterFormationByStage; // �ε������� ���� ���͵� �׷��� �־�����
    public List<bool> _isStageClear;

    [Header("=== target ===")]
    public GameObject _heroGroup;

    public void DoGameFlowAction()
    {
        DoEnemyAction();
    }

    void DoEnemyAction()
    {
        if (IsStageClear())
            return;

        GameObject currLevelMonsterFormation = _monsterFormationByStage[_currLevel - 1];
        for (int i = 0; i < currLevelMonsterFormation.transform.childCount; i++)
        {
            Transform monsterPos = currLevelMonsterFormation.transform.GetChild(i); // ���������� ���� �׷�� ������ ��ġ
            EnemyBase monster = monsterPos.transform.GetChild(0).GetComponent<EnemyBase>(); // �� �������ִ� ���͵�

            // �� ���� ���͵� ���� �� ��Ŵ
            if (monster._state != EnemyBase.eState.Die)
                monster.DoMonsterAction(_heroGroup);
        }

        GameManager._instance._gameFlow = eGameFlow.BackToIdle;
    }

    public void GoToNextStage()
    {
        GameManager._instance._gameFlow = eGameFlow.StageClear;

        for (int i = 0; i < _heroGroup.transform.childCount; i++)
        {
            HeroBase hero = _heroGroup.transform.GetChild(i).GetChild(0).GetComponent<HeroBase>();
            hero.LoseLoadedDmg();
        }
    }

    public bool IsStageClear()
    {
        if (GameManager._instance._gameFlow != eGameFlow.StageClear)
        {
            GameManager._instance._gameFlow = eGameFlow.EnemyTurn;
            return false;
        }
        else
        {
            _isStageClear[_currLevel - 1] = true;

            _monsterFormationByStage[_currLevel - 1].SetActive(false);

            // ���⿡ ������ ������ �ؾ� �� ���� �ۼ�
            _currLevel++;
            _monsterFormationByStage[_currLevel - 1].SetActive(true); 

            return true;
        }
    }
}
