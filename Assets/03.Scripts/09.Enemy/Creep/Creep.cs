using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creep : EnemyBase // Creep�� ����̶�� ���ΰ� ���Ƽ� �̸�����
{
    public override void DoMonsterAction(GameObject heroGroup)
    {
        --_currAttackWaitTurn;

        if (_currAttackWaitTurn == 0)
        {
            // Debug.Log(_atkPower + "��ŭ �÷��̾� Hp ����");
            _currAttackWaitTurn = _maxAttackWaitTurn;
            _ui.UpdateTxt(_currAttackWaitTurn);
            return;
        }

        if (_currAttackWaitTurn == 1)
        {
            _ui.UpdateTxt(_currAttackWaitTurn, Color.red);
            return;
        }

        _ui.UpdateTxt(_currAttackWaitTurn);
    }

    public override void DecreaseMonsterHP(float amount)
    {
        _currHp -= amount;
        if (_currHp <= 0.0f)
        {
            _currHp = 0.0f;
            DieMonster();
        }
    }

    public override void DieMonster()
    {
        _state = eState.Die;
    }
}
