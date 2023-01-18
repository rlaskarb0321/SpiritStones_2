using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class HeroBase : MonoBehaviour
{
    [Header("=== Stat ===")]
    [SerializeField] protected string _rank;
    [SerializeField] protected float _atkPower;

    [SerializeField] public float _hp;

    [SerializeField] protected float _maxMp;
    [HideInInspector] public float _currMp;
    [SerializeField] public eNormalBlockType[] _job;

    [Header("=== Combat ===")]
    public float _loadedDamage;
    public GameObject _txtObj;
    private HeroDmgText _txt;

    private void Start()
    {
        _txt = _txtObj.GetComponent<HeroDmgText>();
    }

    #region CombatMethod
    public virtual void DevelopLoadedDamage()
    {
        _loadedDamage += _atkPower;
        _txt.UpdateText(_loadedDamage);
    }

    public virtual void Attack(GameObject enemyFormation)
    {
        Debug.Log("�����մϴ�");

        _txt.UpdateText(0);
    }

    // ���� �ʿ��� ȣ���ϴ� �������� ������ ������ �Լ�
    public void DecreaseHeroHP(float amount)
    {

    }
    #endregion CombatMethod
}
