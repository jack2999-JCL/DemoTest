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
    private int _countSpider = 10;
    private float _speed = 2f;
    private int _indexPos;

    public void RandomSpider(int index, int indexPos)
    {
        _indexPos = indexPos;
        string path = string.Format(PATH_ITEM, index, _countSpider);
        _imageSpider.sprite = _spriteSpider.GetSprite(path);
        Run();
    }

    public void Run()
    {
        FollowPath(GameManager.Instance.ListPath[_indexPos].path);
    }

    public void FollowPath(List<Node> path)
    {
        StartCoroutine(FollowPathCoroutine(path));
    }

    private IEnumerator FollowPathCoroutine(List<Node> path)
    {
        int currentIndex = 0;
        while (currentIndex < path.Count)
        {
            Node currentNode = path[currentIndex];
            Vector3 currentPos = GameManager.Instance.Grid.WorldPointFromNode(currentNode);
            while (transform.position != currentPos)
            {
                transform.position = Vector3.MoveTowards(transform.position, currentPos, _speed * Time.deltaTime);
                yield return null;
            }
            currentIndex++;
        }
        GameManager.Instance.ReturnSpider(this);
    }


}
