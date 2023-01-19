using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Creep : EnemyBase // Creep�� ����̶�� ���ΰ� ���Ƽ� �̸�����
{
    public override void DoMonsterAction(GameObject heroGroup)
    {
        base.DoMonsterAction(heroGroup);

        --_currAttackWaitTurn;
        if (_currAttackWaitTurn == 0)
        {
            HeroTeamMgr heroTeam = heroGroup.GetComponent<HeroTeamMgr>();
            heroTeam.DecreaseHp(_atkPower);

            _currAttackWaitTurn = _maxAttackWaitTurn;
            _ui.UpdateAttackWaitTxt(_currAttackWaitTurn);
            return;
        }

        if (_currAttackWaitTurn == 1)
        {
            _ui.UpdateAttackWaitTxt(_currAttackWaitTurn, Color.red);
            return;
        }

        _ui.UpdateAttackWaitTxt(_currAttackWaitTurn);
    }

    public override void DecreaseMonsterHP(float amount)
    {
        base.DecreaseMonsterHP(amount);

        if (amount == 0)
            return;

        _currHp -= amount;
        if (_currHp <= 0.0f)
        {
            _currHp = 0.0f;
            _ui.UpdateHp(_currHp);
            DieMonster();
            return;
        }
        _ui.UpdateHp(_currHp);
    }

    public override void DieMonster()
    {
        base.DieMonster();

        _state = eState.Die;
        MonsterFormation monsterFormMgr = transform.parent.parent.GetComponent<MonsterFormation>();
        monsterFormMgr.UpdateDieCount();
        gameObject.SetActive(false);
    }
}
