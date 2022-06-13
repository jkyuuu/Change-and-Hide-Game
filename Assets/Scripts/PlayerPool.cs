using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPool : MonoBehaviour
{
    public static PlayerTransformation playerTransformation;
    public static PlayerPool _playerPool;

    [SerializeField]
    private GameObject poolingPrefab;

    Queue<Player> poolingObjectQueue = new Queue<Player>();

    private void Awake()
    {
        _playerPool = this;
        poolingObjectQueue.Enqueue(CreateNewPlayer());
        //Initialize(1);
    }
    private void Start()
    {
        playerTransformation = FindObjectOfType<PlayerTransformation>();
    }
    //�Ű������� �޴� �ʱ�ȭ ������ �÷��̾� �Ѱ� �̹Ƿ� ���⼭�� �Լ� ��� ���Ѵ�.
    //private void Initialize(int initCount)
    //{
    //    for (int i = 0; i < initCount; i++)
    //    {
    //        poolingObjectQueue.Enqueue(CreateNewPlayer());
    //    }
    //}

    private Player CreateNewPlayer()
    {
        var newPlayer = Instantiate(poolingPrefab).GetComponent<Player>();
        newPlayer.gameObject.SetActive(false);
        newPlayer.transform.SetParent(transform);
        return newPlayer;
    }

    public static Player GetPlayer()
    {
        var obj = _playerPool.poolingObjectQueue.Dequeue();
        obj.transform.SetParent(null);
        obj.gameObject.SetActive(true);
        return obj;

        // ���⼭�� Queue ī��Ʈ �ϳ��� ����ϹǷ� �Ʒ�ó�� ������Ʈ ������ ��� ������ �ڵ� �ʿ� ����.

        //if (_playerPool.poolingObjectQueue.Count > 0)
        //{
        //    var obj = _playerPool.poolingObjectQueue.Dequeue();
        //    obj.gameObject.SetActive(true);
        //    obj.transform.SetParent(null);
        //    //obj.transform.position = playerTransformation.hitObject.transform.position;
        //    return obj;
        //}
        //else
        //{
        //    var newObj = _playerPool.CreateNewPlayer();
        //    newObj.gameObject.SetActive(true);
        //    newObj.transform.SetParent(null);
        //    //newObj.transform.position = playerTransformation.hitObject.transform.position;
        //    return newObj;
        //}
    }
    public static void ReturnPool(Player playerObj)
    {
        playerObj.gameObject.SetActive(false);
        playerObj.gameObject.transform.position = _playerPool.transform.position;
        playerObj.transform.SetParent(_playerPool.transform);
        _playerPool.poolingObjectQueue.Enqueue(playerObj);
        Debug.Log("Ǯ�� ���ư�");
    }


}
