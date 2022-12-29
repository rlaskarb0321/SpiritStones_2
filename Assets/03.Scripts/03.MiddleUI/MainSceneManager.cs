using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneManager : MonoBehaviour
{
    [Header("=== MainScene Button ===")]
    public Button _storyBtn;
    public Button _pvpBtn;
    public Button _mailBtn;
    public Button _myCardBtn;
    public Button _backpackBtn;

    [Header("=== PopUp Window ===")]
    public GameObject _mailPopUp;

    void OnEnable()
    {
        // Main������ �̵��ϰ� �ȴٸ�, �湮�ߴ� Scene���� �׾Ƴ��� ������ �ʱ�ȭ�� �� �ٸ������ ���������� ������� Main���� �˸�
        MiddleSceneTree._middleUITreeInstance._currSceneName = SceneName.Main.ToString();
        MiddleSceneTree._middleUITreeInstance._sceneStack.Clear();
    }

    void Start()
    {
        if (_storyBtn != null)
        {
            _storyBtn.onClick.AddListener(OnStoryBtnClick);
        }

        if (_pvpBtn != null)
        {
            _pvpBtn.onClick.AddListener(OnPvPBtnClick);
        }

        if (_myCardBtn != null)
        {
            _myCardBtn.onClick.AddListener(OnMyCardBtnClick);
        }

        if (_mailBtn != null)
        {
            _mailBtn.onClick.AddListener(OnMailBtnClick);
        }
    }


    /// <summary>
    /// Story��ư Ŭ���� Story��ư�� �����ִ� Scene�� Main���� ���ÿ��ְ�, Story���� Ȱ��ȭ & Main���� ��Ȱ��ȭ
    /// </summary>
    void OnStoryBtnClick()
    {
        MiddleSceneTree._middleUITreeInstance._sceneStack.Push(this.gameObject.name);

        MiddleSceneTree._middleUITreeInstance._story.SetActive(true);
        MiddleSceneTree._middleUITreeInstance._main.SetActive(false);
    }

    void OnPvPBtnClick()
    {
        MiddleSceneTree._middleUITreeInstance._sceneStack.Push(this.gameObject.name);

        MiddleSceneTree._middleUITreeInstance._battleWaiting.SetActive(true);
        MiddleSceneTree._middleUITreeInstance._main.SetActive(false);
    }

    void OnMyCardBtnClick()
    {
        MiddleSceneTree._middleUITreeInstance._sceneStack.Push(this.gameObject.name);

        MiddleSceneTree._middleUITreeInstance._myCard.SetActive(true);
        MiddleSceneTree._middleUITreeInstance._main.SetActive(false);
    }

    // �˾�â ����
    void OnMailBtnClick()
    {
        _mailPopUp.SetActive(true);
    }
}
