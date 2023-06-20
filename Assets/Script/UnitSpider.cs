using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;

public class UnitSpider : MonoBehaviour
{
    public const string PATH_ITEM = "spider-{0}_{1}";
    [SerializeField] private SpriteRenderer _imageSpider;
    [SerializeField] private SpriteAtlas _spriteSpider;
    private List<Vector3> _listPosSpawn;
    private int _indexSpider = 10;
    // public int time = 0;
    public void RandomSpider(int index)
    {
        string path = string.Format(PATH_ITEM, index, _indexSpider);
        _imageSpider.sprite = _spriteSpider.GetSprite(path);
    }
    // private void Update()
    // {
    //     time++;
    //     if (time > 1000)
    //     {
    //         GameManager.Instance.ReturnSpider(this);
    //         time = 0;
    //     }
    // }
}
