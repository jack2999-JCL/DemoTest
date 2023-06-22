using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private UnitSpider _prefabSpider;
    [SerializeField] private int poolSize = 300;
    [SerializeField] private List<Vector3> _listPosSpawn;
    [SerializeField] private List<UnitSpider> objectPool;
    [SerializeField] private GameObject _posPool;
    [SerializeField] private GameObject _posEnd;
    [SerializeField] private float _distance = 1;
    [SerializeField] private GridManager _grid;
    [SerializeField] private Pathfinding _pathFinding;
    [SerializeField] private List<Pathfinding> _listPath;

    private int _countItem = 10;
    private int _countSpider = 8;
    public GridManager Grid { get => _grid; }
    public List<Pathfinding> ListPath { get => _listPath; }

    private ESpawnUnits _currentTab;

    public ESpawnUnits CurrentTab
    {
        get => _currentTab;
        set
        {
            _currentTab = value;
            switch (_currentTab)
            {
                case ESpawnUnits.units20:
                    StartCoroutine("SpawnRandomItems", 20);
                    break;
                case ESpawnUnits.units50:
                    StartCoroutine("SpawnRandomItems", 50);
                    break;
                case ESpawnUnits.units100:
                    StartCoroutine("SpawnRandomItems", 100);
                    break;
            }
        }
    }
    private void Awake()
    {
        Instance = this;
        SetListPos();
    }
    private void Start()
    {
        objectPool = new List<UnitSpider>();
        for (int i = 0; i < poolSize; i++)
        {
            UnitSpider spider = Instantiate(_prefabSpider, _listPosSpawn[0], Quaternion.identity, _posPool.transform);
            spider.gameObject.SetActive(false);
            objectPool.Add(spider);
        }
        for (int i = 0; i < _listPosSpawn.Count; i++)
        {
            Pathfinding path = Instantiate(_pathFinding); ;
            _listPath.Add(path);
            path.FindPath(_listPosSpawn[i], _posEnd.transform.position);
        }
    }

    public UnitSpider GetObject()
    {
        for (int i = 0; i < objectPool.Count; i++)
        {
            if (!objectPool[i].gameObject.activeInHierarchy)
            {
                objectPool[i].gameObject.SetActive(true);
                return objectPool[i];
            }
        }
        UnitSpider spider = Instantiate(_prefabSpider, _posPool.transform);
        spider.gameObject.SetActive(true);
        objectPool.Add(spider);
        return spider;
    }
    private IEnumerator SpawnRandomItems(int count)
    {
        for (int i = 0; i < count; i++)
        {
            int numberPos = Random.Range(0, _countItem);
            int numberIndexSpider = Random.Range(0, _countSpider);
            UnitSpider item = GetObject();
            item.transform.position = _listPosSpawn[numberPos];
            item.RandomSpider(numberIndexSpider + 1, numberPos);

            yield return new WaitForSeconds(0.1f);
        }
    }
    public void ReturnSpider(UnitSpider spider)
    {
        if (objectPool.Contains(spider))
        {
            spider.gameObject.SetActive(false);
        }
    }
    public void SetListPos()
    {
        if (_countItem % 2 == 0)
        {
            for (int i = 0; i < _countItem / 2; i++)
            {
                _listPosSpawn.Add(_prefabSpider.transform.position + Vector3.left * _distance * ((_countItem / 2f - i) - 0.5f));
            }
            for (int i = _countItem / 2; i < _countItem; i++)
            {
                _listPosSpawn.Add(_prefabSpider.transform.position + Vector3.right * _distance * ((i - _countItem / 2f) + 0.5f));
            }
        }
        else
        {
            for (int i = 0; i < _countItem / 2; i++)
            {
                _listPosSpawn.Add(_prefabSpider.transform.position + Vector3.left * _distance * (_countItem / 2 - i));
            }
            _listPosSpawn.Add(this.transform.position + Vector3.up);
            for (int i = _countItem / 2 + 1; i < _countItem; i++)
            {
                _listPosSpawn.Add(_prefabSpider.transform.position + Vector3.right * _distance * ((i - _countItem / 2f) + 0.5f));
            }
        }
    }

}
public enum ESpawnUnits
{
    units20,
    units50,
    units100,
}
