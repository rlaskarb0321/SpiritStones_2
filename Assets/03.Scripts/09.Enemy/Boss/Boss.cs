using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Boss : EnemyBase, IBossType
{
    public enum eBossType { Aggressive, Defensive, }
    public eBossType _eBossType;
    public IBossType _bossType;

    [Header("=== Weight Random Boss AttackPattern ===")]
    public int _normalPattern;
    public int _patternByBossType;
    public int _interruptPattern;

    private void Start()
    {
        switch (_eBossType)
        {
            case eBossType.Aggressive:
                _bossType = this.GetComponent<AggressiveBoss>();
                break;
            case eBossType.Defensive:
                _bossType = this.GetComponent<DefensiveBoss>();
                break;
        }

        if (_bossType == null)
        {
            Debug.Log("null");
        }
    }

    public override void DoMonsterAction(GameObject heroGroup)
    {
        // ����ġ ������ Ȱ���ؼ� ������ ���������� �پ��ϰ� �غ���
        // ����Ÿ���� ���������� ����������� ���� �ٸ�����
    }

    public override void DecreaseMonsterHP(float amount, HeroBase hero)
    {

    }

    public override void DieMonster()
    {

    }

    public void NormalBossAttack()
    {

    }
}
