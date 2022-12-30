using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ��� ������ �⺻������ ������ �־�� �ϴ� ��ũ��Ʈ
 */

public class BlockBase : MonoBehaviour
{
    public float _movSpeed;
    public eNormalBlockType _normalType;
    public eSpecialBlockType _specialType;

    public bool _isDocked;
    private WaitUntil _wu;

    private void Start()
    {
        GameManager._instance._blockMgrList.Add(this.gameObject);
        MoveBlock(this.gameObject);
        _wu = new WaitUntil(() => !_isDocked);
    }

    public void DoNormalBlockAction()
    {
        Debug.Log("NormalBlock �ı�Sound, Animation ���");
    }

    public void DoItemBlockAction(SpecialBlock specialBlock)
    {
        specialBlock.DoAction();
    }

    public void DoObstacleBlockAction()
    {
        Debug.Log("Do ObstacleBlock Action!!!");
    }

    public void MoveBlock(GameObject block)
    {
        StartCoroutine(MovePosition(block));
    }

    IEnumerator MovePosition(GameObject block)
    {
        yield return _wu; // !_isDocked �̸� ����

        // ���� ����
        block.transform.Translate(Vector2.down * _movSpeed * Time.deltaTime);

        StartCoroutine(MovePosition(block));
    }
}
