using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopUIManager : MonoBehaviour
{
    [Header("=== Back Button ===")]
    public Button _backBtn;

    [Header("=== Plus Button ===")]
    public Button _energyPlusBtn;
    public Button _battleEnergyPlusBtn;
    public Button _goldPlusBtn;
    public Button _gemPlusBtn;


    void Start()
    {
        if (_backBtn != null)
        {
            _backBtn.onClick.AddListener(OnClickBackBtn);
        }
    }

    /// <summary>
    /// Mainȭ�� ���� ȭ�鿡�� �ش� ��ưŬ���� ������ �湮�ߴ� ȭ������ �̵���, Mainȭ�鿡�� �ƹ��ϵ� �Ͼ�� ����
    /// </summary>
    void OnClickBackBtn()
    {
        // ���� ������ ��ġ�ϰ��ִ� ���� �̸��� �����ϴ� ����
        string currScene = MiddleSceneTree._middleUITreeInstance._currSceneName;
        if (currScene != SceneName.Main.ToString())
        {
            GameObject.Find(currScene).gameObject.SetActive(false);

            string lastSceneName = MiddleSceneTree._middleUITreeInstance._sceneStack.Pop();
            GameObject.Find("MiddleUIGroup").transform.Find(lastSceneName).gameObject.SetActive(true); 
        }
        else
        {
            Debug.Log("���� ���ξ��̶� �ڷ� �� ���� �����ϴ�.");
        }
    }
}
