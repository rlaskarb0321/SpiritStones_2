using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������ �帧�� ���
public enum eGameFlow
{

}

public class GameManager : MonoBehaviour
{
    public static GameManager _instance = null;

    [Header("=== Block ===")]
    public int _dockedCount;
    public List<GameObject> _blockMgrList; // ������ ���� �����ϱ� ���� �޸� ����Ʈ
    public List<BlockBase> _breakList; // ���� �ı� ���� ����Ʈ

    [Header("=== Game ===")]
    public Queue<eGameFlow> _gameFlowQueue;
    [SerializeField] private float _delayTime;
    public bool _isPlayerAttackTurn;
    public int _playerComboCount;
    private float _initDelayTimeValue;

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

        _gameFlowQueue = new Queue<eGameFlow>();
        _blockMgrList = new List<GameObject>();
        _blockMgrList.Capacity = 35;
        _breakList = new List<BlockBase>();
        _breakList.Capacity = 35;
        _dockedCount = 0;
        _initDelayTimeValue = _delayTime;
        _playerComboCount = 0;

        StartCoroutine(ManagePlayerCombo());
    }

    IEnumerator ManagePlayerCombo()
    {
        yield return new WaitUntil(() => _isPlayerAttackTurn && _dockedCount == 63);

        _delayTime -= Time.deltaTime;
        if (_delayTime <= 0.0f)
        {
            _delayTime = _initDelayTimeValue;
            _isPlayerAttackTurn = false;
            _playerComboCount = 0;
        }

        if (_breakList.Count != 0)
        {
            _delayTime = _initDelayTimeValue;

            yield return new WaitUntil(() => _breakList.Count == 0);
            _playerComboCount++;
        }
        StartCoroutine(ManagePlayerCombo());
    }
}