using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroTeamMgr : MonoBehaviour, IGameFlow
{
    [Header("=== Hero ===")]
    public GameObject[] _heroPos;
    [SerializeField] public List<HeroBase>[] _heroesTypeCountArr = new List<HeroBase>[4];
    [HideInInspector] public float _totalHp;

    [Header("=== Target ===")]
    public GameObject _enemyGroup;

    private void Awake()
    {
        InitHeroInformation();
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
            for (int i = 0; i < heroType._job.Length; i++)
            {
                _heroesTypeCountArr[(int)heroType._job[i]].Add(heroType);
            }
        }
    }

    public void DoGameFlowAction()
    {
        // eGameState.HeroAttack �϶�
        Attack();
        Debug.Log("������ �������ϴ�");

        GameManager._instance._gameFlow++;
    }

    void Attack()
    {
        foreach (GameObject pos in _heroPos)
        {
            HeroBase hero = pos.transform.GetChild(0).GetComponent<HeroBase>();
            hero.Attack(_enemyGroup);
        }
    }
}
