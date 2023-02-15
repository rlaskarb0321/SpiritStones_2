using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageMgr : MonoBehaviour, IGameFlow
{
    [Header("=== Stage ===")]
    public int _maxLevelValue;
    public int _currLevel;
    public float _itemBlockPercentage;
    public bool _isBossStageClear;
    public List<bool> _isStageClear;

    [Header("=== UI ===")]
    public Image _stageBackGroundImg;
    public Image _fadeInOutPanel;
    public Text _stageAlarmTxt;
    private CombatSceneMgr _combatMgr;

    Animator _anim;
    private int _hashToUp = Animator.StringToHash("isTimeToUp");

    private void Awake()
    {
        _combatMgr = GetComponent<CombatSceneMgr>();
        _anim = _stageAlarmTxt.GetComponent<Animator>();
    }

    public void DoGameFlowAction()
    {
        GameManager._instance._gameFlow = eGameFlow.InStageClear;
        StartCoroutine(DoStageClearAction());
    }

    IEnumerator DoStageClearAction()
    {
        _stageAlarmTxt.text = $"STAGE 1\n{_currLevel} / {_maxLevelValue}";
        HeroTeamMgr heroTeamMgr = _combatMgr._heroGroup.GetComponent<HeroTeamMgr>();
        heroTeamMgr.LooseHeroDmg();

        while (GameManager._instance._gameFlow == eGameFlow.InStageClear)
        {
            // ui���� ���߿� ���ο� ��ũ��Ʈ�� �и�
            // �гη� Fade in&out ȿ���� �ְ� �������� �̵��� �ʿ��� �۾��� ��
            yield return new WaitForSeconds(2.5f);
            _combatMgr.GoToNextStage();
            GameManager._instance._gameFlow = eGameFlow.Idle;

            /* �г�
             * �г��� ���ÿ� activeSelf = false�̰�, InStageClear�϶� true�� ����, �����ۺ��� ������ ������� �ִ����
             * �гη� Fade in&out ȿ���� �� �� stage �˸� ui �ִϸ��̼ǵ� �������, ���� ��ο� ������ event�� �޾Ƴ��´�
             * �ش� event�� �������� �̵� �ڵ��۾�
             */
            yield return null;
        }
    }
}
