using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colum : MonoBehaviour
{
    [SerializeField] private BlockGenerator _blockGenerator;
    [SerializeField] private GameObject _spiritGenerator;
    [SerializeField] private GameObject _spawnPos;

    void Start()
    {
        StartCoroutine(GenerateBlock());
    }

    IEnumerator GenerateBlock()
    {
        // Colum�� childCount < 5 �̰�, ��ȥ�����⿡ ��ȥ�� �����Ǹ� �ؿ� ������ ����
        yield return new WaitUntil(() => (this.transform.childCount < 5) && (_spiritGenerator.transform.childCount >= 1));

        // �� ��, ��ȥ�� ����� ��� ������ �ؿ� ������ ����
        yield return new WaitUntil(() => _spiritGenerator.transform.childCount == 0);

        int needBlockCount = 5 - this.transform.childCount;
        for (int i = 0; i < needBlockCount; i++)
        {
            Vector2 spawnPos = _spawnPos.transform.GetChild(5 - (i + 1)).position;
            _blockGenerator.GenerateBlock(spawnPos, this.transform);
        }

        StartCoroutine(GenerateBlock());
    }
}
