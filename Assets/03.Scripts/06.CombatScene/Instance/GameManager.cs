using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance = null;

    [Header("=== Block ===")]
    public int _itemBlockPercentage; // �����ۺ���� ����Ȯ��
    public int _obstacleBlockPercentage; // ������ �����϶� ��ֹ� ���� ���� Ȯ��
    public bool _isLastRound; // ���簡 ������ ��������
    private int _currRound; // ���� ���带 Ȯ��
    public int _dockedCount;
    public List<GameObject> _blockMgrList; // ������ ���� �����ϱ� ���� �޸� ����Ʈ
    public List<BlockBase> _breakList; // ���� �ı� ���� ����Ʈ
    public bool _canGenerateBlock;
    public GameObject _spiritGenerator;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (_instance != this)
                Destroy(this.gameObject);
        }

        _isLastRound = false;
        _blockMgrList = new List<GameObject>();
        _blockMgrList.Capacity = 35;
        _breakList = new List<BlockBase>();
        _breakList.Capacity = 35;
        _dockedCount = 0;

        // canGenerateBlock �� ���� ���غ���

        /*
         * case 1: �븻���� �̾�����
         * case 2: �븻�� + �����ۺ�
         * case 3: �����ۺ��� 3���̻� �̾�����
         */
    }
}
