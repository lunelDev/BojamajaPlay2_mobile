using System.Collections.Generic;
using System.Collections;
using UnityEngine;

namespace Pizza
{
    public class OrderManager : MonoBehaviour
    {
        public GameObject[] leafletPrefabs; //주문서 종류
        public GameObject[] sizeRefs; //피자 도우 종류
        public Punisher penaltyOverlay;
        public GameObject fakePizza;
        public GameObject failParticle;
        int addcore;
        public List<Vector3> Spots { get { return _spots; } }
        List<Vector3> _spots = new List<Vector3>();
        Queue<Leaflet> _orders = new Queue<Leaflet>();
     
        int _lastOrder;
        private object currentPizzaSize;
        const float SMALL = 0.5f;
        const float MEDIUM = 0.65f;
        const float LARGE = 0.8f;
        const float LEEWAY = 0.03f;

        void Start()
        {
            foreach (Transform t in transform)
            {
                _spots.Add(t.position);
            }
        }

        public void StartQueue()
        {
            StartCoroutine("_FillQueue", 6);
        }

        public void StopQueue()
        {
            StopCoroutine("_FillQueue");
        }
        //6개의 주문서 3초 마다 생성
        IEnumerator _FillQueue(int amt)
        {
            float timer = 0f;

            while (true)
            {
                timer = 0f;
                if (_orders.Count < 6)
                    SpawnBackOrder();
                yield return null;

                while (timer < 1.5f)
                {
                    timer += Time.deltaTime;
                    yield return null;
                }
            }
        }
        Leaflet or;
        public void EvaluatePizza(PizzaDough pizza)
        {
            //주문이 있을 경우 
            if (_orders.Count > 0)
            {
                switch (_orders.Peek().type)
                {   //오차 0.003
                    
                    case Leaflet.Type.S when pizza.size > SMALL - LEEWAY && pizza.size < MEDIUM + LEEWAY:
                    case Leaflet.Type.M when pizza.size > MEDIUM - LEEWAY && pizza.size < LARGE + LEEWAY:
                    case Leaflet.Type.L when pizza.size > LARGE - LEEWAY && pizza.size < LARGE + 0.15f + LEEWAY:
                      
                        pizza.Serve(fakePizza); //성공한 피자
                        OnComplete(); //주문번호                                                          
                        DataManager.Instance.scoreManager.Add(addcore);
                        SoundManager.Instance.PlaySFX("10 finished pizza", 1.5f); //완성 사운드
                        break;
                    default:
                        pizza.ThrowAway(); //망한 피자
                                           //  StartCoroutine(_failPizzaParticle());
                        penaltyOverlay.ExecutePenalty();
                        DataManager.Instance.scoreManager.Subtract(100);//점수 감소
                        SoundManager.Instance.PlaySFX("17 assembly failure", 2f);
                        SoundManager.Instance.PlaySFX("ScoreDown");
                        break;
                }
            }
              
            else
            {
                pizza.ThrowAway();
                StartCoroutine(_failPizzaParticle());
            }
               
          
        }
        IEnumerator _failPizzaParticle()
        {
            failParticle.SetActive(true);
            yield return new WaitForSeconds(1f);
            failParticle.SetActive(false);
            yield return null;
        }
        void MoveUpAll()
        {
            foreach (var c in _orders)
            {
                c.MoveUpOneSpot();
            }
            UpdateSizeRef();
        }
        //주문서 다음 사이즈 확인해서 활성화 시킴
        private void UpdateSizeRef()
        {
            if (_orders.Count > 0)
                sizeRefs[(int)_orders.Peek().type].SetActive(true);
        }

        Leaflet SpawnBackOrder()
        {
            var nonrepeatingOrders = new List<GameObject>();
            foreach (var l in leafletPrefabs)
            {
                if (l.GetHashCode() == _lastOrder)
                    continue;

                nonrepeatingOrders.Add(l);
            }

            GameObject spawnable = nonrepeatingOrders[Random.Range(0, nonrepeatingOrders.Count)];
            Leaflet leaflet = Instantiate(spawnable).GetComponent<Leaflet>();
            leaflet.transform.position = _spots[_spots.Count - 1];
            leaflet.MoveTo(_orders.Count);

            SoundManager.Instance.PlaySFX("06 Order paper");
            _orders.Enqueue(leaflet);
            _lastOrder = spawnable.GetHashCode();
            UpdateSizeRef();

            return leaflet;
        }
        //피자 완성시, 주문번호 제거
        void RemoveFrontOrder()
        {
            addcore = this._orders.Peek().GetComponent<Leaflet>().getScore;
            sizeRefs[(int)_orders.Peek().type].SetActive(false);
            SoundManager.Instance.PlaySFX("15 Order paper cut");
            _orders.Dequeue().Despawn();
        }
        //피자완성, 주문번호
        void OnComplete()
        {
            RemoveFrontOrder();//처음 주문번호 제거
            MoveUpAll(); //나머지 주문번호 앞으로 이동
        }
    }
}