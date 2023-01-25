using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BottomUIManager : MonoBehaviour
{
    public Button _mainBtn;
    public Button _myCardBtn;
    public Button _summonBtn;
    public Button _socialBtn;
    public Button _settingBtn;

    void Start()
    {
        if (_mainBtn != null)
        {
            _mainBtn.onClick.AddListener(OnClickMainBtn);
        }
        if (_myCardBtn != null)
        {
            _myCardBtn.onClick.AddListener(OnClickMyCardBtn);
        }
        if (_summonBtn != null)
        {
            _summonBtn.onClick.AddListener(OnClickSummonBtn);
        }
        if (_socialBtn != null)
        {
            _socialBtn.onClick.AddListener(OnClickSocialBtn);
        }
        if (_settingBtn != null)
        {
            _settingBtn.onClick.AddListener(OnClickSettingBtn);
        }
    }

    /// <summary>
    /// �˾�â�� ����â�� ������ �ϴ� UI��ư���� Ŭ���� �����־��� ȭ���� ���ÿ� �־��ְ� �ش� ��ư�� �´� ȭ���� ����ش�
    /// </summary>
    void OnClickMainBtn()
    {
        // ���� ���� ���� ���ξ��� �ƴҽ� ���ι�ư�� Ŭ���ϸ� ����ȭ������ �� ��, ���� ���� ���� ���� ����Obj�� ����
        string currSceneName = MiddleSceneTree._middleUITreeInstance._currSceneName;
        if (currSceneName != SceneName.Main.ToString())
        {
            GameObject.Find("MiddleUIGroup").transform.Find(SceneName.Main.ToString()).gameObject.SetActive(true);
            GameObject.Find(currSceneName).SetActive(false); 
        }
        else
        {
            Debug.Log("���� ���ξ��̶� ���ι�ư�� ������ ������ �����ϴ�");
        }
    }

    void OnClickMyCardBtn()
    {
        // ������� MyCard���� �ƴ϶�� MyCard�� �̵����ְ�, MyCard��� Main���� �̵��Ѵ�
        // �ϴ�UI�� �� ī���ư�� ������ �� ������ ���̸��� ������ ������ ���ÿ� �־���
        //string currSceneName = MiddleSceneTree._middleUITreeInstance._currSceneName;
        //if (currSceneName != SceneName.MyCard.ToString())
        //{
        //    MiddleSceneTree._middleUITreeInstance._sceneStack.Push(currSceneName);

        //    // MiddleUIGroup�� �߿��� ������ �� ����ã�� ���ְ�, MyCard ����Obj�� ����
        //    GameObject.Find("MiddleUIGroup").transform.Find(currSceneName).gameObject.SetActive(false);
        //    MiddleSceneTree._middleUITreeInstance._myCard.SetActive(true); 
        //}
        //else
        //{
        //    GameObject.Find("MiddleUIGroup").transform.Find(SceneName.Main.ToString()).gameObject.SetActive(true);
        //    GameObject.Find(currSceneName).SetActive(false);
        //}
    }

    void OnClickSummonBtn()
    {
        // ������� Summon���� �ƴ϶�� Summon���� �̵����ְ�, Summon�̶�� Main���� �̵��Ѵ�
        // �ϴ�UI�� �� ī���ư�� ���������� ���̸��� ������ ������ ���ÿ� �־���
        //string currSceneName = MiddleSceneTree._middleUITreeInstance._currSceneName;
        //if (currSceneName != SceneName.Summon.ToString())
        //{
        //    MiddleSceneTree._middleUITreeInstance._sceneStack.Push(currSceneName);

        //    // MiddleUIGroup�� �߿��� ������ �� ����ã�� ���ְ�, Summon ����Obj�� ����
        //    GameObject.Find("MiddleUIGroup").transform.Find(currSceneName).gameObject.SetActive(false);
        //    MiddleSceneTree._middleUITreeInstance._summon.SetActive(true); // Summon���� �̵�
        //}
        //else
        //{
        //    GameObject.Find("MiddleUIGroup").transform.Find(SceneName.Main.ToString()).gameObject.SetActive(true);
        //    GameObject.Find(currSceneName).SetActive(false);
        //}
    }

    void OnClickSocialBtn()
    {
        // �˾�â
    }

    void OnClickSettingBtn()
    {
        // �˾�â
    }

}
