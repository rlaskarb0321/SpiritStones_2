using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockAlphaController
{
    // ���� ���İ��� ��Ӱ� ��
    public void DarkenBlockAlphaValue(BlockBase block, float lowAlpha)
    {
        Image image = block.GetComponent<Image>();
        Color color = image.color;
        color.a = lowAlpha;
        image.color = color;
    }

    // ����Ʈ �� ������ ���İ��� �����
    public void BrightenBlockAlphaValue(List<GameObject> list)
    {
        for (int i = 0; i < list.Capacity; i++)
        {
            if (list[i] == null)
                continue;

            Image blockImage = list[i].GetComponent<Image>();
            Color color = blockImage.color;
            if (color.a != 1)
            {
                color.a = 1;
                blockImage.color = color;
            }
        }
    }
}
