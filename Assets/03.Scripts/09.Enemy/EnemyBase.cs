using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class EnemyBase : MonoBehaviour
{
    public enum eState
    {
        Alive,
        Die,
    }

    [Header("=== Stat ===")]
    [SerializeField] protected float _atkPower;
    public float _maxHp;
    [HideInInspector] public float _currHp;
    [SerializeField] protected int _maxAttackWaitTurn;
    public int _currAttackWaitTurn;
    public eState _state;

    [HideInInspector] public EnemyUI _ui;

    private void Start()
    {
        _state = eState.Alive;
        _currHp = _maxHp;

        _ui = this.GetComponent<EnemyUI>();
        _ui.UpdateHp(_currHp);
    }

    public virtual void DoMonsterAction(GameObject heroGroup)
    {
        
    }

    // �����ʿ��� ���Ϳ��� ������������ ���� �Լ�
    public virtual void DecreaseMonsterHP(float amount)
    {
        
    }

    public virtual void DieMonster()
    {
        
    }
}
