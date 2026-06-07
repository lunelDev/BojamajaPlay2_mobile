using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerQueueManager : MonoBehaviour
{
    public GameObject[] customerPrefabs;
    public List<Vector3> QueueSpots { get { return _queueSpots; } }

    public Queue<Customer> _queue = new Queue<Customer>(); //큐 생성
                                                           // public List<Customer> _List = new List<Customer>(); //큐 생성
    List<Vector3> _queueSpots = new List<Vector3>();
    Queue<GameObject> _customerList = new Queue<GameObject>();
    string _lastCustomer = "";

    //게임 시작시 손님 
    private void Start()
    {
        foreach (Transform t in transform)
        {
            _queueSpots.Add(t.position);
        }
    }
    //처음 시작할 때 리스트를 채워줌
    public void FillQueue()
    {
        StartCoroutine("_FillQueue");
    }

    IEnumerator _FillQueue()
    {
        Customer customerInstance;

        for (int i = 1; i < 4; i++)
        {
            customerInstance = SpawnBackCustomer(); // 손님 생성
            customerInstance.MoveTo(i); // 손님마다 번호를 정해줌

            yield return new WaitForSeconds(0.1f); //1초마다 생성
        }
    }
    //주문이 완료 됬을 떄 
    public void OnOrderComplete()
    {
        MoveUpAll(); // 주문완료 되고 오른쪽으로 이동한 후, 사라짐
        RemoveFrontCustomer(); //큐의 맨앞의 리스트를 지워준다.
        SpawnBackCustomer(); //새로운 손님 생성
    }
    //나머지 손님들을 한칸씩 이동한다.
    public void MoveUpAll()
    {
        foreach (var c in _queue)
        {
            c.MoveUpOneSpot();
        }
    }
    //새로운 손님 리스트를 만들어줌
    Customer SpawnBackCustomer()
    {
        var cs = new List<GameObject>(customerPrefabs);
        foreach (var c in customerPrefabs)
        { //마지막 손님 리스트와 이름 같거나 T1이 리스트 안에 존재 할떄 생성하지 않게 한다. 
            if (c.name == _lastCustomer || (c.name == "T1" && _queue.Count > 0 && _queue.Peek().name == "T1(Clone)"))
            {
                cs.Remove(c);
            }
        }
        //새로운 손님을 생성해주고 마지막 큐에 생성된 손님을 넣어준다
        GameObject spawnable = cs[Random.Range(0, cs.Count)];
        Customer customerInstance = Instantiate(spawnable).GetComponent<Customer>();
        customerInstance.transform.position = _queueSpots[4];
        customerInstance.MoveUpOneSpot(); //손님들 앞으로 이동

        _queue.Enqueue(customerInstance); //큐에 넣어줌
        _lastCustomer = spawnable.name; //마지막 큐의 이름을 지금 생성된 이름으로 바꿔줌
        return customerInstance;
    }
    //제일 첫 번쨰 손님을 제거한다. 
    public void RemoveFrontCustomer()
    {
        _queue.Dequeue();

    }
}
