using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggressiveBossPattern : MonoBehaviour
{
    // ������� ����ġ ������ �����Ͽ� ���� ����ġ������ �پ��� ������ �ϳ��� �����ϰ� �� ����
    public void ChooseAggressiveAttack(HeroTeamMgr heroTeam, EnemyBase enemy)
    {
        AttackTwice(enemy, heroTeam);
    }


    void AttackTwice(EnemyBase enemy, HeroTeamMgr heroTeam)
    {
        
        float newDmg = enemy._atkPower * 0.75f;
        for (int i = 0; i < 2; i++)
        {
            heroTeam.GetComponent<HeroTeamUI>().DecreaseHp(newDmg);
        }
    }
}