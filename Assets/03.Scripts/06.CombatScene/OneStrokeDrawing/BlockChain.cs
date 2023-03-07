using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockChain : MonoBehaviour
{
    private LineRenderer _lr;
    private Color _initMatColor;
    public Material _lineRendererMat;

    private void Awake()
    {
        _lr = GetComponent<LineRenderer>();
        _initMatColor = _lineRendererMat.color;
    }

    private void Update()
    {
        #region 23/03/02 ���η����� ������ī��Ʈ ���� �̸� �����ؼ� ���� ����
        //if (GameManager._instance._breakList.Count == 2)
        //{
        //    _lr.positionCount = 6;

        //    _lr.SetPosition(0, GameManager._instance._breakList[0].transform.position);
        //    _lr.SetPosition(1, GameManager._instance._breakList[1].transform.position);
        //}  <-- �̰� 0��° �ε����� �������ʾƼ� �׷��ǰ� �; �׽�Ʈ�뵵
        // ������ī��Ʈ�� �˳��ϰ� ������ �ش� ������Ʈ�� ���� Obj�� (0, 0, 0)�� ���� �׸�
        #endregion

        if (Input.GetMouseButton(0))
        {
            if (GameManager._instance._breakList.Count == 0)
                return;

            // ���η����� Mat ���������ϱ�
            BlockBase block = GameManager._instance._breakList[0].GetComponent<BlockBase>();
            Color color;
            switch (block.NormalType)
            {
                case eNormalBlockType.Warrior:
                    ColorUtility.TryParseHtmlString("#FF9494", out color);
                    color.a = _initMatColor.a;
                    _lineRendererMat.color = color;
                    break;
                case eNormalBlockType.Archer:
                    ColorUtility.TryParseHtmlString("#2AFD2A", out color);
                    color.a = _initMatColor.a;
                    _lineRendererMat.color = color;
                    break;
                case eNormalBlockType.Thief:
                    ColorUtility.TryParseHtmlString("#F5FF00", out color);
                    color.a = _initMatColor.a;
                    _lineRendererMat.color = color;
                    break;
                case eNormalBlockType.Magician:
                    ColorUtility.TryParseHtmlString("#1ED6FF", out color);
                    color.a = _initMatColor.a;
                    _lineRendererMat.color = color;
                    break;
            }

            _lr.positionCount = GameManager._instance._breakList.Count;
            int index = GameManager._instance._breakList.Count - 1;
            _lr.SetPosition(index, GameManager._instance._breakList[index].transform.position);
        }

        if (Input.GetMouseButtonUp(0))
        {
            _lr.positionCount = 0; 
            _lineRendererMat.color = _initMatColor;
        }
    }
}