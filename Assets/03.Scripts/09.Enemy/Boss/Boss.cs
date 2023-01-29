using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Boss : EnemyBase
{
    [HideInInspector] public AggressiveBossPattern _bossPattern;
    public BossWeightedRandomPattern _weightRandomPattern;

    private void Start()
    {
        _bossPattern = GetComponent<AggressiveBossPattern>();
        _weightRandomPattern = GetComponent<BossWeightedRandomPattern>();
        _weightRandomPattern.SetWeightData();
    }

    public override void DoMonsterAction(GameObject heroGroup)
    {
        // ����ġ������������ ������ ���ݹ���� ����
        string attackType = _weightRandomPattern.ReturnRandomPattern();
        switch (attackType)
        {
            case "Normal Pattern":
                Debug.Log("Normal");
                break;
            case "ObstacleBlock Pattern":
                Debug.Log("Obstacle");
                break;
            case "Type By Pattern":
                Debug.Log("Type");
                break;
        }
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
