using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.U2D;

public class UnitSpider : MonoBehaviour
{
    public const string PATH_ITEM = "spider-{0}_{1}";
    [SerializeField] private SpriteRenderer _imageSpider;
    [SerializeField] private SpriteAtlas _spriteSpider;
    [SerializeField] private GameObject _target;
    private NavMeshAgent _unitSpider;
    private int _indexSpider = 10;
    private int _posY = 3;
    [SerializeField] private bool _isRun;
    public void RandomSpider(int index)
    {
        string path = string.Format(PATH_ITEM, index, _indexSpider);
        _imageSpider.sprite = _spriteSpider.GetSprite(path);
        _isRun = true;
    }

    private void Awake()
    {
        _unitSpider = GetComponent<NavMeshAgent>();
        _unitSpider.updateRotation = false;
        _unitSpider.updateUpAxis = false;
        _unitSpider.speed = 2.5f;
        _isRun = false;
    }
    public void SetTarget(GameObject go)
    {
        _target = go;
    } 
    private void Update()
    {
        if (_isRun)
        {
            SetSpiderPostion();
            if (this.transform.position.x >= _target.transform.position.x && this.transform.position.y > _posY )
            {
                _isRun = false;
                GameManager.Instance.ReturnSpider(this);
            }
        }
    }
    private void SetSpiderPostion()
    {
        _unitSpider.SetDestination(_target.transform.position);
    }

}
