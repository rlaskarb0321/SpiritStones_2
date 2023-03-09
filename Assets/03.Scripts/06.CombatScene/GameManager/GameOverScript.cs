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
    public Image _deathBlack;
    public Image[] _soulessBlocks;
    public Image _gameOverPanel;
    public float _reviveHpPercentage;
    WaitUntil _wu;

    [Header("=== Ressurrect ===")]
    public GameObject _resurrectParticleObj;
    private RessurectParticle _ressurectParticle;
    private AudioSource _audioSource;
    public AudioClip _ressurectSound;

    public Button _retryBtn;
    public Button _exitBtn;

    private void Awake()
    {
        for (int i = 0; i < _soulessBlocks.Length; i++)
        {
            _soulessBlocks[i] = _soulessBlocks[i].GetComponent<Image>();
        }

        _audioSource = GetComponent<AudioSource>();
        _ressurectParticle = _resurrectParticleObj.GetComponent<RessurectParticle>();
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
        _deathBlack.enabled = true;

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
        _deathBlack.enabled = false;

        HeroTeamUI heroTeamUI = GameManager._instance._heroTeamMgrObj.GetComponent<HeroTeamUI>();

        _ressurectParticle.SetRevive();
        _audioSource.PlayOneShot(_ressurectSound);
        float amount = heroTeamUI._totalHp * _reviveHpPercentage;
        heroTeamUI.IncreaseHp(amount, true);
        _heroLifeState = eHeroLife.Alive;

        for (int i = 0; i < _soulessBlocks.Length; i++)
        {
            _soulessBlocks[i].enabled = false;
        }

        // ���� �ǻ츮��
        for (int i = 0; i < GameManager._instance._blockMgrList.Count; i++)
        {
            GameObject blockBase = GameManager._instance._blockMgrList[i];
            blockBase.SendMessage("RestoreBlockSoul");
        }
    }
}
