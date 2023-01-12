using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HeroBase : MonoBehaviour
{
    [Header("=== Stat ===")]
    [SerializeField] protected string _rank;
    [SerializeField] protected float _atkPower;
    [SerializeField] protected float _hp;
    [SerializeField] protected float _maxMp;
    [HideInInspector] public float _currMp;
    [SerializeField] public eNormalBlockType[] _job;

    [Header("=== Combat ===")]
    [HideInInspector] public float _loadedDamage;

    #region CombatMethod
    public virtual void DevelopLoadedDamage()
    {
        _loadedDamage += _atkPower;
    }

    public virtual void Attack(GameObject enemyFormation)
    {
        // �������� ������ �ʿ���
    }
    #endregion CombatMethod
}
