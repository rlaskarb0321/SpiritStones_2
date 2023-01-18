using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGenerator : MonoBehaviour, IGameFlow
{
    public GameObject _combatMgrObj;
    private CombatSceneMgr _combatMgr;

    public GameObject[] _normalBlockPrefabsArr;
    public GameObject[] _normalItemBlockPrefabsArr;
    public GameObject[] _specialItemBlockPrefabsArr;
    public GameObject[] _obstacleBlockPrefabsArr;

    private void Start()
    {
        _combatMgr = _combatMgrObj.GetComponent<CombatSceneMgr>();
        
    }

    /*
     * ���������� obstacleBlock�� �����ȴ�.
     */
    eBlockType DetermineBlockType()
    {
        eBlockType blockType;
        int randomVal = Random.Range(0, 100);

        // ���簡 ������������ �ƴ��� Ȯ��
        if (_combatMgr._currLevel == _combatMgr._maxLevelValue)
        {
            if (randomVal <= _combatMgr._obstacleBlockPercentage)
            {
                blockType = eBlockType.Obstacle;
            }
            else if (randomVal <= _combatMgr._itemBlockPercentage)
            {
                blockType = eBlockType.Item;
            }
            else
            {
                blockType = eBlockType.Normal;
            }
        }
        else
        {
            if (randomVal <=
                (_combatMgr._itemBlockPercentage - _combatMgr._obstacleBlockPercentage))
            {
                blockType = eBlockType.Item;
            }
            else
            {
                blockType = eBlockType.Normal;
            }
        }

        return blockType;
    }

    public void GenerateBlock(Vector2 spawnPos, Transform parent)
    {
        eBlockType blockType = DetermineBlockType();
        int randomValue;
        switch (blockType)
        {
            case eBlockType.Normal:
                randomValue = 
                    Random.Range(0, _normalBlockPrefabsArr.Length);
                GameObject normalBlock = Instantiate
                    (_normalBlockPrefabsArr[(int)randomValue], spawnPos, Quaternion.identity, parent) as GameObject;
                break;

            case eBlockType.Item:
                randomValue =
                    Random.Range(0, _normalItemBlockPrefabsArr.Length);
                GameObject itemBlock = Instantiate
                    (_normalItemBlockPrefabsArr[(int)randomValue], spawnPos, Quaternion.identity, parent) as GameObject;
                break;

            case eBlockType.Obstacle:
                break;
        }
    }

    public void DoGameFlowAction()
    {
        if (GameManager._instance._playerComboCount >= 3)
        {
            int randomValue;

            // ����Ⱦ����� �������߿��� ��������
            randomValue = Random.Range(0, _specialItemBlockPrefabsArr.Length);
            GameObject specialItemBlock = _specialItemBlockPrefabsArr[randomValue];

            // �ΰ��� �����߿��� normalBlock �� ��������, ItemBlock�� ����Ⱦ��������� �ٲ����� ����
            randomValue = Random.Range(0, GameManager._instance._blockMgrList.Capacity);
            while (GameManager._instance._blockMgrList[randomValue].tag == "ItemBlock")
                randomValue = Random.Range(0, GameManager._instance._blockMgrList.Capacity);

            // ���õ� normalBlock�� ����Ⱦ��������� �ٲ�ġ��
            GameObject normalBlock = GameManager._instance._blockMgrList[randomValue];
            Transform parentColum = normalBlock.transform.parent;

            normalBlock.SetActive(false);
            normalBlock.GetComponent<BlockBase>().RemoveFromMemoryList();

            Instantiate(specialItemBlock, normalBlock.transform.position, Quaternion.identity, parentColum);
            Destroy(normalBlock);
        }

        GameManager._instance._playerComboCount = 0;
        GameManager._instance._gameFlow++;
    }
}
