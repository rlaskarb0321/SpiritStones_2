using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour
{
    private bool _isAnimEnd = false;
    private AudioSource _audio;
    public AudioClip _audioClip;

    void Awake()
    {
        // ������ ó�� ������ �� �������� 60���� ������Ŵ
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
    }

    void Start()
    {
        _audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (_isAnimEnd && Input.GetMouseButtonDown(0))
        {
            _audio.clip = _audioClip;
            _audio.volume = 0.1f;
            _audio.Play();

            _isAnimEnd = false;

            StartCoroutine(LoadingSceneLoad());
        }
    }

    public void StartSceneUIAnimEnd()
    {
        _isAnimEnd = true;
    }

    IEnumerator LoadingSceneLoad()
    {
        yield return new WaitForSeconds(3.3f);

        LoadingSceneManager.LoadScene("MainScene");
    }
}
