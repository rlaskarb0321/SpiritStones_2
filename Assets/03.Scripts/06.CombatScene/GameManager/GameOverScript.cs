using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum eHeroLife
{
    Alive,
    Dead,
    DeadSceneProgress,
}

public class GameOverScript : MonoBehaviour
{
    public eHeroLife _heroLifeState;
    public Image[] _soulessBlocks;
    public Image _gameOverPanel;
    public float _reviveHpPercentage;
    WaitUntil _wu;

    public Button _retryBtn;
    public Button _exitBtn;

    private void Awake()
    {
        for (int i = 0; i < _soulessBlocks.Length; i++)
        {
            _soulessBlocks[i] = _soulessBlocks[i].GetComponent<Image>();
        }

        _wu = new WaitUntil(() => _heroLifeState == eHeroLife.Dead);
        StartCoroutine(CheckGameOver());
    }

    IEnumerator CheckGameOver()
    {
        yield return _wu;
        DoGameOverAction();

        StartCoroutine(CheckGameOver());
    }

    void DoGameOverAction()
    {
        _heroLifeState = eHeroLife.DeadSceneProgress;

        // ���ӿ����� ������ �ִ� ������ �ǹ̾��� ������ ó�� ���̱� ���� �۾�
        for (int i = 0; i < _soulessBlocks.Length; i++)
        {
            _soulessBlocks[i].enabled = true;
        }

        /* ���ӿ����� ���鿡�� SendMessage�Լ��� �̿��� ���İ��� ������ ���̴� �Լ��� ȣ��
         * SendMessage�� �� ����
         * 1. ���ӿ����� ���ӵ��� 1�� �߻��� ���̶� BlockBase�Լ����� GameOver��Ȳ�� ��� üũ�ϰ� ���� �ʱ�����
         * 2. ���ʿ��� ������ ������ �ʱ�����
         * 3. ����� ����� �� �ִٴ� ���� �ذ��� ���� �ش��Լ��� �����ϴ� Ŭ���� ������ GameObject�� ������
         */
        for (int i = 0; i < GameManager._instance._blockMgrList.Capacity; i++)
        {
            GameObject blockBase = GameManager._instance._blockMgrList[i];
            blockBase.SendMessage("ExtractBlockSoul");
        }
        StartCoroutine(ShowGameOverPanel());
    }

    IEnumerator ShowGameOverPanel()
    {
        yield return new WaitForSeconds(3.5f);
        _gameOverPanel.gameObject.SetActive(true);
    }

    public void OnClickExitBtn()
    {
        LoadingSceneManager.LoadScene("MainScene");
    }

    public void OnClickReviveBtn()
    {
        _gameOverPanel.gameObject.SetActive(false);

        HeroTeamUI heroTeam = GameManager._instance._heroTeamMgrObj.GetComponent<HeroTeamUI>();
        float amount = heroTeam._totalHp * _reviveHpPercentage;
        heroTeam.IncreaseHp(amount, true);
        _heroLifeState = eHeroLife.Alive;

        for (int i = 0; i < _soulessBlocks.Length; i++)
        {
            _soulessBlocks[i].enabled = false;
        }

        for (int i = 0; i < GameManager._instance._blockMgrList.Count; i++)
        {
            GameObject blockBase = GameManager._instance._blockMgrList[i];
            blockBase.SendMessage("RestoreBlockSoul");
        }
    }
}
