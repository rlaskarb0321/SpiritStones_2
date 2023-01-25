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

    private void Awake()
    {
        _enemyGroup = _enemyGroupObj.GetComponent<CombatSceneMgr>();
        InitHeroInformation();
        UpdateHp(_currHp);
    }

    public void DoGameFlowAction()
    {
        GameManager._instance._gameFlow = eGameFlow.InProgress;
        Attack();

        if (_enemyGroup.IsStageClear())
        {
            GameManager._instance._gameFlow = eGameFlow.Idle;
        }
        else
        {
            GameManager._instance._gameFlow = eGameFlow.EnemyTurn; 
        }
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

    void Attack()
    {
        foreach (GameObject pos in _heroPos)
        {
            HeroBase hero = pos.transform.GetChild(0).GetComponent<HeroBase>();
            hero.Attack(_enemyGroup, _enemyGroup._currLevel - 1);

            hero._loadedDamage = 0.0f;
            hero._txt.UpdateText(hero._loadedDamage);
        }
    }

    void UpdateHp(float amount)
    {
        _hpBarFill.fillAmount = amount / _totalHp;
        _hpTxt.text = $"{amount} / {_totalHp}";
    }
}
