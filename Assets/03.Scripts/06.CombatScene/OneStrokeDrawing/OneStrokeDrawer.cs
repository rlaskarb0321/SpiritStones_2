using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneStrokeDrawer : MonoBehaviour
{
    // Composition
    private BlockAlphaController _ctrlBlockAlpha;
    private BlockBreaker _breakBlock;
    [SerializeField] private CanvasRayCaster _canvasRayCaster;

    // Field
    public Stack<BlockBase> _breakStack; // Block�鸸 ���� �� �ִ� ����
    [SerializeField] private float _lowAlpha;
    [SerializeField] private List<string> _normalList;
    [SerializeField] private List<string> _itemList;
    [SerializeField] private int _normalCount; // �븻������ �� ���� ������ �� �ִ� ��ȸ
    [SerializeField] private int _itemCount; // �����ۺ����� �� ���� ������ �� �ִ� ��ȸ

    private void Start()
    {
        _ctrlBlockAlpha = new BlockAlphaController();
        _breakBlock = new BlockBreaker();
        _breakStack = new Stack<BlockBase>();
        _normalList = new List<string>();
        _itemList = new List<string>();
        _canvasRayCaster = GetComponent<CanvasRayCaster>();

        InitBlockList(_normalList, _itemList);
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            _canvasRayCaster.CanvasRaycast();
            GameObject block = _canvasRayCaster.ReturnRayResult();
            if (block == null)
                return;

            BlockBase blockBase = block.GetComponent<BlockBase>();
            if (blockBase == null)
                return;

            // �������� �±װ��� ���� �ൿ
            switch (block.tag)
            {
                case "NormalBlock":
                    if (_normalCount > 0)
                    {
                        RemoveNormalTypeAtList(blockBase, _normalList);
                        CheckNormalBlockToDarken(); // �������� ��� �����ϰ��ִ� �迭���� ��Ӱ��ؾ��ϴ� ������ üũ�� ��Ӱ� ��
                    }
                    else
                    {
                        if (!_normalList.Contains(blockBase._normalType.ToString()))
                        {
                            _breakBlock.PushBlockToList(GameManager._instance._breakList, blockBase);  
                        }
                    }
                    break;

                case "ItemBlock":
                    if (_itemCount > 0)
                    {
                        RemoveItemTypeAtList(blockBase, _itemList);
                        CheckItemBlockToDarken();
                    }
                    else
                    {
                        if (!_itemList.Contains(blockBase._specialType.ToString()))
                        {
                            _breakBlock.PushBlockToList(GameManager._instance._breakList, blockBase);
                        }
                    }
                    break;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            InitBlockList(_normalList, _itemList);
            _ctrlBlockAlpha.BrightenBlockAlphaValue(GameManager._instance._blockMgrList);
            _breakBlock.BreakBlock(GameManager._instance._breakList);
        }
    }

    void InitBlockList(List<string> normalList, List<string> itemList)
    {
        normalList.Capacity = Enum.GetValues(typeof(eNormalBlockType)).Length;
        itemList.Capacity = Enum.GetValues(typeof(eSpecialBlockType)).Length;

        foreach (eNormalBlockType unreachable in Enum.GetValues(typeof(eNormalBlockType)))
        {
            if (unreachable == eNormalBlockType.None)
                continue;

            if (!normalList.Contains(unreachable.ToString()))
            {
                normalList.Add(unreachable.ToString());
            }
        }

        foreach (eSpecialBlockType unreachable in Enum.GetValues(typeof(eSpecialBlockType)))
        {
            if (unreachable == eSpecialBlockType.None)
                continue;

            if (!itemList.Contains(unreachable.ToString()))
            {
                itemList.Add(unreachable.ToString());
            }
        }

        _normalCount = 1;
        _itemCount = 1;
    }


    /// <summary>
    /// �� ������ ���� �븻�������� ����Ʈ�� ���ܳ���
    /// </summary>
    /// <param name="block">NormalBlock�� enum������ �����ϱ� ���� ����</param>
    /// <param name="unreachableNormalBlock">������ enum�� ����Ʈ���� ������</param>
    void RemoveNormalTypeAtList(BlockBase block, List<string> unreachableNormalBlock)
    {
        _normalCount--;

        string blockType = block._normalType.ToString();
        if (unreachableNormalBlock.Contains(blockType))
        {
            unreachableNormalBlock.Remove(blockType);
        }
    }


    /// <summary>
    /// �� ������ ���� �����ۺ������� ����Ʈ�� ���ܳ���
    /// </summary>
    /// <param name="block">ItemBlock�� enum������ �����ϱ� ���� ����</param>
    /// <param name="unreachableItemBlock">������ enum�� ����Ʈ���� ������</param>
    void RemoveItemTypeAtList(BlockBase block, List<string> unreachableItemBlock)
    {
        _itemCount--;

        string[] itemTag = block._specialType.ToString().Split('_');
        string itemType = itemTag[itemTag.Length - 1];
        for (int i = unreachableItemBlock.Count - 1; i >= 0; i--)
        {
            if (unreachableItemBlock[i].Contains(itemType))
            {
                unreachableItemBlock.RemoveAt(i);
            }
        }
    }

    void CheckNormalBlockToDarken()
    {
        for (int i = 0; i < GameManager._instance._blockMgrList.Capacity; i++)
        {
            GameObject blockMember = GameManager._instance._blockMgrList[i];
            if (blockMember == null)
                continue;

            NormalBlock normalMember = blockMember.GetComponent<NormalBlock>();
            if (normalMember == null)
                continue;

            if (_normalList.Contains(normalMember.NormalBlockType.ToString()))
            {
                _ctrlBlockAlpha.DarkenBlockAlphaValue(normalMember, _lowAlpha);
            }
        }
    }

    void CheckItemBlockToDarken()
    {
        for (int i = 0; i < GameManager._instance._blockMgrList.Capacity; i++)
        {
            GameObject blockMember = GameManager._instance._blockMgrList[i];
            if (blockMember == null)
                continue;

            ItemBlock itemMember = blockMember.GetComponent<ItemBlock>();
            if (itemMember == null)
                continue;

            if (_itemList.Contains(itemMember.ItemBlockType.ToString()))
            {
                _ctrlBlockAlpha.DarkenBlockAlphaValue(itemMember, _lowAlpha);
            }
        }
    }
}