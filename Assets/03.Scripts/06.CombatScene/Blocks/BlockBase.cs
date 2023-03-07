using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BlockBase : MonoBehaviour
{
    [SerializeField] protected float _movSpeed;
    [SerializeField] protected bool _isDocked;
    public GameObject _highlightedParticleObj;
    public virtual eNormalBlockType NormalType { get; set; }
    public virtual eSpecialBlockType SpecialType { get; set; }
    public virtual float MovSpeed { get { return _movSpeed; } set { _movSpeed = value; } }
    public virtual bool IsDocked { get { return _isDocked; } set { _isDocked = value; } }

    [Header("=== Composition ===")]
    [HideInInspector] public BlockSound _blockSound;

    private void Awake()
    {
        _blockSound = GetComponent<BlockSound>();
    }

    private void FixedUpdate()
    {
        MoveBlock(this.gameObject);
    }

    public virtual void DoAction() { }
    
    public virtual void AddToMemoryList()
    {
        GameManager._instance._blockMgrList.Add(this.gameObject);
    }

    public virtual void RemoveFromMemoryList()
    {
        GameManager._instance._blockMgrList.Remove(this.gameObject);
    }

    public virtual void MoveBlock(GameObject block)
    {
        if (IsDocked)
            return;

        block.transform.Translate(Vector2.down * MovSpeed * Time.deltaTime);
    }

    public void ConvertBlockType(string keyWord, Sprite image)
    {
        var component = gameObject.GetComponent<BlockBase>();
        component.RemoveFromMemoryList();
        switch (keyWord)
        {
            case "SkullBlock_Obstacle":
                // Image관련 작업
                Image skullImg = GetComponent<Image>();
                Color color;
                skullImg.sprite = image;
                ColorUtility.TryParseHtmlString("#ADADAD", out color);
                skullImg.color = color;

                // 그 외
                gameObject.tag = "ObstacleBlock";
                gameObject.name = "SkullBlock_Obstacle (Clone)";
                gameObject.AddComponent<SkullBlock>();
                break;
        }
        Destroy(component);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("BlockBorder"))
        {
            IsDocked = true;
            GameManager._instance._dockedCount++;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("BlockBorder"))
        {
            IsDocked = false;
            GameManager._instance._dockedCount--;
        }
    }

    public void RestoreBlockSoul()
    {
        Image[] imges = this.GetComponentsInChildren<Image>();
        for (int i = 0; i < imges.Length; i++)
        {
            Color color = imges[i].color;
            color.a = 1;
            imges[i].color = color;
        }
    }

    public IEnumerator ExtractBlockSoul()
    {
        Image[] imges = this.GetComponentsInChildren<Image>();
        for (int i = 0; i < imges.Length; i++)
        {
            StartCoroutine(AshenBlockImg(imges[i], 0.006f));
            yield return null;
        }
    }

    IEnumerator AshenBlockImg(Image sourceImg, float fadeValue)
    {
        Color color = sourceImg.color;
        while (sourceImg.color.a > 0.05f)
        {
            color.a -= fadeValue;
            sourceImg.color = color;
            yield return null;
        }
    }
}
