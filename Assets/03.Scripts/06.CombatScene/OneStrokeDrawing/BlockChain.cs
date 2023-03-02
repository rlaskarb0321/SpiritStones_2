using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockChain : MonoBehaviour
{
    private LineRenderer _lr;

    private void Awake()
    {
        _lr = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        //if (GameManager._instance._breakList.Count == 2)
        //{
        //    _lr.positionCount = 6;

        //    _lr.SetPosition(0, GameManager._instance._breakList[0].transform.position);
        //    _lr.SetPosition(1, GameManager._instance._breakList[1].transform.position);
        //}  <-- �̰� 0��° �ε����� �������ʾƼ� �׷��ǰ� �; �׽�Ʈ�뵵

        if (Input.GetMouseButton(0))
        {
            if (GameManager._instance._breakList.Count == 0)
                return;

            // _lr.positionCount = 35; <-- �갡 �������� (������ī��Ʈ�� �˳��ϰ� ������ �ش� ������Ʈ�� ���� Obj�� (0, 0, 0)�� ���� �׸�
            _lr.positionCount = GameManager._instance._breakList.Count;
            int index = GameManager._instance._breakList.Count - 1;
            _lr.SetPosition(index, GameManager._instance._breakList[index].transform.position);
        }

        if (Input.GetMouseButtonUp(0))
        {
            _lr.positionCount = 0;
        }
    }
}