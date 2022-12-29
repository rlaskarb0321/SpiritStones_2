using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spirit : MonoBehaviour
{
    public float _movSpeed;
    public float _breakTime;

    private float _xDir;
    private float _yDir;
    private Vector2 _dir;

    private void Start()
    {
        _xDir = Random.Range(-1.0f, 1.0f);
        _yDir = Random.Range(-1.0f, 0.0f);
        _dir = Vector2.right * _xDir + Vector2.up * _yDir;
    }

    private void Update()
    {
        if (_breakTime > 0.0f)
        {
            IdleRun();
        }
        else
        {
            GoToHero();
        }
    }

    void IdleRun()
    {
        transform.Translate(_dir.normalized * (_movSpeed * _breakTime) * Time.deltaTime);
        _breakTime -= Time.deltaTime;
    }

    void GoToHero() // �Ű������� GameObject target�� �߰��ϰ� Hero��ũ��Ʈ���� this�� ���� ����
    {
        _breakTime = 0.0f;
        // Debug.Log("�˸´� �������� ���� �� �ı��˴ϴ�");

        // �������� ����Ǵ°� �ϴ��� �̷��� ǥ��
        Destroy(gameObject, 1.0f);
    }
}
