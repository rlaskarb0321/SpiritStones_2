using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IGameFlow
{
    public void DoGameFlowAction();
}

// ������ �帧�� ���
public enum eGameFlow
{
    Idle, // ��� ����
    LoadDamage, // �������� �װ��ִ� ����
    GenerateSpecialItemBlock, // ����Ⱥ�������ܰ�
    HeroAttack,
    EnemyTurn,
    StageClear,
    InStageClear,
    BackToIdle, // �����·� ���ư��� �� üũ
    BossStageClear,
    InProgress, // ����ȣ���� �����ϱ����� ������ ����
}

public class GameManager : MonoBehaviour, IGameFlow
{
    public static GameManager _instance = null;

    [Header("=== Block ===")]
    public int _dockedCount;
    public List<GameObject> _blockMgrList; // �������� ���� �����ϱ� ���� �޸� ����Ʈ
    public List<BlockBase> _breakList; // ������ �ı� ���� ����Ʈ
    public List<GameObject> _obstacleBlockList; // ���ع� ���� ���� ����Ʈ

    [Header("=== Game ===")]
    [SerializeField] private float _delayTime;
    public int _playerComboCount;
    private float _initDelayTimeValue;
    public eGameFlow _gameFlow;
    public Image _resultUI;

    #region 23/03/02 GameOver ���� ����� GameOverScript�� �ű�
    //[Header("=== GameOver ===")]
    // public eHeroLife _heroLife;
    //public Image[] _soulessBlocks;
    // public Image _gameOverPanel;
    #endregion

    [Header("=== Composition ===")]
    public GameObject _blockGeneratorObj;
    public GameObject _heroTeamMgrObj;
    public GameObject _combatSceneMgrObj;
    public GameObject _hpBarObj;
    [HideInInspector] public GameOverScript _gameOverMgr;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            if (_instance != this)
                Destroy(this.gameObject);
        }
        _gameFlow = eGameFlow.Idle;

        #region 23/03/02 GameOver ���� ����� GameOverScript�� �ű�
        //_heroLife = eHeroLife.Alive;
        #endregion

        _dockedCount = 0;
        _initDelayTimeValue = _delayTime;
        _playerComboCount = 0;

        _obstacleBlockList = new List<GameObject>();
        _obstacleBlockList.Capacity = 35;
        _blockMgrList = new List<GameObject>();
        _blockMgrList.Capacity = 35;
        
        _breakList = new List<BlockBase>();
        _breakList.Capacity = 35;

        _gameOverMgr = GetComponent<GameOverScript>();
        StartCoroutine(InGameMainFlow());

        #region 23/03/02 GameOver ���� ����� GameOverScript�� �ű�
        //for (int i = 0; i < _soulessBlocks.Length; i++)
        //{
        //    _soulessBlocks[i] = _soulessBlocks[i].GetComponent<Image>();
        //}
        #endregion
    }

    IEnumerator InGameMainFlow()
    {
        #region 23/02/15 GetComponent �ּ�ȭ
        //IGameFlow gameFlowSub;
        #endregion
        BlockGenerator blockGenerator = _blockGeneratorObj.GetComponent<BlockGenerator>();
        HeroTeamMgr heroTeam = _heroTeamMgrObj.GetComponent<HeroTeamMgr>();
        HeroTeamUI heroTeamUI = _heroTeamMgrObj.GetComponent<HeroTeamUI>();
        StageMgr stageMgr = _combatSceneMgrObj.GetComponent<StageMgr>();
        CombatSceneMgr combatScene = _combatSceneMgrObj.GetComponent<CombatSceneMgr>();
        HpBarEffect hpBarEffect = _hpBarObj.GetComponent<HpBarEffect>();

        // ������ ���� �ʾƼ� �������� �������϶� ���� ����
        while (_gameFlow != eGameFlow.BossStageClear)
        {
            switch (_gameFlow)
            {
                case eGameFlow.LoadDamage:
                    this.DoGameFlowAction();
                    break;
                case eGameFlow.GenerateSpecialItemBlock:
                    blockGenerator.DoGameFlowAction();
                    #region 23/02/15 GetComponent �ּ�ȭ
                    //gameFlowSub = _blockGeneratorObj.GetComponent<BlockGenerator>();
                    //gameFlowSub.DoGameFlowAction();
                    #endregion
                    break;
                case eGameFlow.HeroAttack:
                    heroTeam.DoGameFlowAction();
                    #region 23/02/15 GetComponent �ּ�ȭ
                    //gameFlowSub = _heroTeamMgrObj.GetComponent<HeroTeamMgr>();
                    //gameFlowSub.DoGameFlowAction();
                    #endregion
                    break;
                case eGameFlow.StageClear:
                    stageMgr.DoGameFlowAction();
                    #region 23/02/15 GetComponent �ּ�ȭ
                    //gameFlowSub = _combatSceneMgrObj.GetComponent<StageMgr>();
                    //gameFlowSub.DoGameFlowAction();
                    #endregion
                    break;
                case eGameFlow.EnemyTurn:
                    combatScene.DoGameFlowAction();
                    #region 23/02/15 GetComponent �ּ�ȭ
                    //gameFlowSub = _combatSceneMgrObj.GetComponent<CombatSceneMgr>();
                    //gameFlowSub.DoGameFlowAction();
                    #endregion
                    break;
                case eGameFlow.BackToIdle:
                    for (int i = 0; i < _obstacleBlockList.Count; i++)
                    {
                        ObstacleBlock obstacleBlock = _obstacleBlockList[i].GetComponent<ObstacleBlock>();
                        obstacleBlock.DoHarmfulAction();
                    }
                    StartCoroutine(hpBarEffect.MatchRedHpFill(heroTeamUI._currHp, heroTeamUI._totalHp, 0.355f));
                    _gameFlow = eGameFlow.Idle;
                    break;
            }

            #region 23/03/02 GameOver ���� ����� GameOverScript�� �ű�
            //switch (_heroLife)
            //{
            //    case eHeroLife.Dead:
            //        DoGameOverAction();
            //        break;
            //}
            #endregion
            yield return null;
        }
    }

    public void DoGameFlowAction()
    {
        _gameFlow = eGameFlow.InProgress;
        StartCoroutine(ManagePlayerCombo());
    }

    IEnumerator ManagePlayerCombo()
    {
        while (_gameFlow == eGameFlow.InProgress)
        {
            yield return new WaitUntil(() => _dockedCount == 63);

            _delayTime -= Time.deltaTime;
            yield return null;

            if (_delayTime <= 0.0f)
            {
                _delayTime = _initDelayTimeValue;
                _gameFlow = eGameFlow.LoadDamage;
                _gameFlow++;
            }

            if (_breakList.Count != 0)
            {
                _delayTime = _initDelayTimeValue;

                yield return new WaitUntil(() => _breakList.Count == 0);
                _playerComboCount++;
            } 
        }
    }

    #region 23/03/02 GameOver ���� ����� GameOverScript�� �ű�
    //void DoGameOverAction()
    //{
    //    _heroLife = eHeroLife.DeadSceneProgress;

    //    // ���ӿ����� ������ �ִ� �������� �ǹ̾��� ������ ó�� ���̱� ���� �۾�
    //    for (int i = 0; i < _soulessBlocks.Length; i++)
    //    {
    //        _soulessBlocks[i].enabled = true;
    //    }

    //    /* ���ӿ����� �����鿡�� SendMessage�Լ��� �̿��� ���İ��� ������ ���̴� �Լ��� ȣ��
    //     * SendMessage�� �� ����
    //     * 1. ���ӿ����� ���ӵ��� 1�� �߻��� ���̶� BlockBase�Լ����� GameOver��Ȳ�� ��� üũ�ϰ� ���� �ʱ�����
    //     * 2. ���ʿ��� ������ ������ �ʱ�����
    //     * 3. ����� ����� �� �ִٴ� ���� �ذ��� ���� �ش��Լ��� �����ϴ� Ŭ���� ������ GameObject�� ������
    //     */
    //    for (int i = 0; i < _blockMgrList.Capacity; i++)
    //    {
    //        GameObject blockBase = _blockMgrList[i];
    //        blockBase.SendMessage("ExtractBlockSoul");
    //    }
    //    StartCoroutine(ShowGameOverPanel());
    //}

    //IEnumerator ShowGameOverPanel()
    //{
    //    yield return new WaitForSeconds(3.0f);
    //    _gameOverPanel.gameObject.SetActive(true);
    //}
    #endregion
}