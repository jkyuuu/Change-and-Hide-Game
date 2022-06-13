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
    //매개변수로 받는 초기화 갯수가 플레이어 한개 이므로 여기서는 함수 사용 안한다.
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

        // 여기서는 Queue 카운트 하나만 사용하므로 아래처럼 오브젝트 부족할 경우 생성할 코드 필요 없다.

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
        Debug.Log("풀로 돌아감");
    }


}
