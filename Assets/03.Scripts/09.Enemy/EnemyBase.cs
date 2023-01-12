using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    [Header("=== Stat ===")]
    [SerializeField] protected float _atkPower;
    [SerializeField] protected float _hp;
    [HideInInspector] public float _currHp;
    [SerializeField] protected int _maxAttackWaitTurn;
    [HideInInspector] public int _currAttackWaitTurn;

    public virtual void DoMonsterAction(GameObject heroGroup)
    {
        // �������� 0�̵Ǿ����� ��������Ʈ�� ����ؼ� ���۱����� ������
    }

    public virtual void DieMonster()
    {

    }
}
