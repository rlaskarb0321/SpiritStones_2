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
    public bool _isBossStageClear;

    [Tooltip("�Ʒ��� �� Collection�� ũ��� Max Level Value�� ����� ��")]
    public List<GameObject> _monsterFormationByStage; // �ε������� ���� ���͵� �׷��� �־�����
    public List<bool> _isStageClear;

    [Header("=== target ===")]
    public GameObject _heroGroup;

    public void DoGameFlowAction()
    {
        float deltaTime = 115.0f;
        if (GameManager._instance._gameFlow == eGameFlow.StageClear)
        {
            GameManager._instance._gameFlow = eGameFlow.InProgress;
            while (deltaTime >= 0.0f)
            {
                Debug.Log(deltaTime);
                deltaTime -= Time.deltaTime;
            }

            _isStageClear[_currLevel - 1] = true;
            _monsterFormationByStage[_currLevel - 1].SetActive(false);

            // ���⿡ ������ ������ �ؾ� �� ���� �ۼ�
            _currLevel++;
            if (_currLevel <= _monsterFormationByStage.Count)
            {
                _monsterFormationByStage[_currLevel - 1].SetActive(true);
            }
            else
            {
                _isBossStageClear = true;
            }

            GameManager._instance._gameFlow = eGameFlow.Idle;
            deltaTime = 15.0f;
            return;
        }

        Debug.Log("����� ������ �ȵ�");
        DoEnemyAction();
    }

    void DoEnemyAction()
    {
        GameManager._instance._gameFlow = eGameFlow.InProgress;
        GameObject currLevelMonsterFormation = _monsterFormationByStage[_currLevel - 1];
        for (int i = 0; i < currLevelMonsterFormation.transform.childCount; i++)
        {
            Transform monsterPos = currLevelMonsterFormation.transform.GetChild(i); // ���������� ���� �׷�� ������ ��ġ
            EnemyBase monster = monsterPos.transform.GetChild(0).GetComponent<EnemyBase>(); // �� �������ִ� ���͵�

            // �� ���� ���͵� ���� �� ��Ŵ
            if (monster._state != EnemyBase.eState.Die)
                monster.DoMonsterAction(_heroGroup);
        }

        StartCoroutine(CheckAttackEnd(currLevelMonsterFormation));
    }

    IEnumerator CheckAttackEnd(GameObject currMonsterForm)
    {
        MonsterFormation monsterFormation = currMonsterForm.GetComponent<MonsterFormation>();
        yield return new WaitUntil(() => monsterFormation._endTurnMonsterCount == monsterFormation._monsterCount.Count);

        GameManager._instance._gameFlow = eGameFlow.BackToIdle;
        monsterFormation._endTurnMonsterCount = 0;
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
}
