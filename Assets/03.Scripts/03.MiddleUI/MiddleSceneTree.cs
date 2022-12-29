using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SceneName { Main, Story, Section1, Section2, Section3, Section4, MyCard, BattleWaiting, Summon, Count }

/// <summary>
/// MiddleUI �׷쿡 �ִ� ������ ������ ������ Ŭ����
/// </summary>
public class MiddleSceneTree : MonoBehaviour
{
    public static MiddleSceneTree _middleUITreeInstance = null;

    [Header("=== BackGround ===")]
    public GameObject _main;
    public GameObject _story;
    public GameObject _myCard;
    public GameObject _battleWaiting;
    public GameObject _summon;

    [Header("=== Section Number ===")]
    public GameObject _sectionStage_1;
    public GameObject _sectionStage_2;
    public GameObject _sectionStage_3;
    public GameObject _sectionStage_4;

    public string _currSceneName; // ���� �����ִ� ���� ���ӿ�����Ʈ �̸��� �������ִ� ����
    public Stack<string> _sceneStack; // ���� �ٳణ ���� ������ ������ ����

    private void Awake()
    {
        if (_middleUITreeInstance == null)
        {
            _middleUITreeInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (_middleUITreeInstance != this)
                Destroy(this.gameObject);
        }
        _sceneStack = new Stack<string>();
    }

    void Update()
    {
    }
}
