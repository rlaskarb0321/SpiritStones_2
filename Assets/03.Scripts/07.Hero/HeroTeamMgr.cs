using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroTeamMgr : MonoBehaviour, IGameFlow
{
    [Header("=== Hero ===")]
    public GameObject[] _heroPos;
    [SerializeField] public List<HeroBase>[] _heroesTypeCountArr = new List<HeroBase>[4];

    [Header("=== Hp ===")]
    public float _currHp;
    public float _totalHp;
    public Image _hpBarFill;
    public Text _hpTxt;

    [Header("=== Target ===")]
    public GameObject _enemyGroupObj;
    private CombatSceneMgr _enemyGroup;
    private MonsterFormation _monsterForm;

    private void Awake()
    {
        _enemyGroup = _enemyGroupObj.GetComponent<CombatSceneMgr>();
        InitHeroInformation();
        UpdateHp(_currHp);
    }

    public void DoGameFlowAction()
    {
        GameManager._instance._gameFlow = eGameFlow.InProgress;
        _monsterForm = _enemyGroup._monsterFormationByStage[_enemyGroup._currLevel - 1].GetComponent<MonsterFormation>();

        StartCoroutine(Attack());
    }

    // ���� ������ ������Ƽ�� �������� �ֱ����� ���� �Լ�
    public void DecreaseHp(float amount)
    {
        _currHp -= amount;
        if (_currHp <= 0.0f)
        {
            _currHp = 0.0f;
        }
        UpdateHp(_currHp);
    }

    // ���� �������� ������� �� ȸ���ϱ� ���� �Լ�
    public void IncreaseHp(float amount)
    {
        _currHp += amount;
        if (_currHp >= _totalHp)
        {
            _currHp = _totalHp;
        }
        UpdateHp(_currHp);
    }

    public void LooseHeroDmg()
    {
        for (int i = 0; i < _heroPos.Length; i++)
        {
            _heroPos[i].transform.GetChild(0).GetComponent<HeroBase>().LoseLoadedDmg();
        }
    }

    void InitHeroInformation()
    {
        for (int i = 0; i < this.transform.childCount; i++)
            _heroesTypeCountArr[i] = new List<HeroBase>();

        // �Ʊ���Ƽ�� ���������� Count
        foreach (GameObject pos in _heroPos)
        {
            HeroBase heroType = pos.transform.GetChild(0).GetComponent<HeroBase>();
            _totalHp += heroType._hp;
            _currHp = _totalHp;

            for (int i = 0; i < heroType._job.Length; i++)
            {
                _heroesTypeCountArr[(int)heroType._job[i]].Add(heroType);
            }
        }
    }

    IEnumerator Attack()
    {
        int animEndCount = 0;
        int index = 0;
        while (animEndCount < _heroPos.Length)
        {
            HeroBase hero = _heroPos[index].transform.GetChild(0).GetComponent<HeroBase>();
            hero.Attack(_enemyGroup, _enemyGroup._currLevel - 1);

            yield return new WaitUntil(() => 
                hero._heroState == HeroBase.eState.EndAttack || hero._heroState == HeroBase.eState.Idle);

            yield return new WaitForSeconds(0.35f);
            animEndCount++;
            index++;
            hero._heroState = HeroBase.eState.Idle;
        }

        // ���� �ִϸ��̼��� �� ����
        yield return new WaitUntil(() => animEndCount == _heroPos.Length);
        if (GameManager._instance._gameFlow == eGameFlow.StageClear)
            yield break;

        GameManager._instance._gameFlow = eGameFlow.EnemyTurn;
    }

    void UpdateHp(float amount)
    {
        _hpBarFill.fillAmount = amount / _totalHp;
        _hpTxt.text = $"{amount} / {_totalHp}";
    }
}