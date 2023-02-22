using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class EnemyBase : MonoBehaviour
{
    public enum eState
    {
        Alive,
        Attack,
        EndTurn,
        Die,
        Acting,
    }

    [Header("=== Stat ===")]
    public float _atkPower;
    public float _maxHp;
    public float _currHp;
    [SerializeField] public int _maxAttackWaitTurn;
    public int _currAttackWaitTurn;
    public eState _state;
    public float _movSpeed;
    [HideInInspector] public bool _isActive;
    [HideInInspector] public WaitForSeconds _ws;

    [Header("=== Composition ===")]
    [HideInInspector] public EnemyUI _enemyUI;

    private void OnEnable()
    {
        _state = eState.Alive;
        _currHp = _maxHp;
        _currAttackWaitTurn = _maxAttackWaitTurn;

        _enemyUI = this.GetComponent<EnemyUI>();
        _enemyUI.SetInitValue(this);
        _enemyUI.UpdateHp(_currHp);
        _enemyUI.UpdateAttackWaitTxt(_currAttackWaitTurn);
        _ws = new WaitForSeconds(0.1f);
    }

    public virtual void DoMonsterAction(GameObject heroGroup)
    {
    }

    // �����ʿ��� ���Ϳ��� ������������ ���� �Լ�
    public virtual void DecreaseMonsterHP(float amount, HeroBase hero)
    {
        amount = Mathf.Floor(amount);
        if (amount == 0)
            return;

        GameObject txt = Instantiate(_enemyUI._hitDmgTxt, _enemyUI._dmgSpawnPos.transform.position,
            Quaternion.identity, _enemyUI._dmgSpawnPos.transform) as GameObject;

        //_hitDmgTxt.GetComponent<DmgTxt>().SetColor(SetColor(hero._job[0]));
        txt.GetComponent<Text>().text = $"- {amount}";
    }

    public virtual void DieMonster()
    {
    }

    public virtual IEnumerator EnemyRoutine(GameObject heroGroup)
    {
        yield return null;
    }
}