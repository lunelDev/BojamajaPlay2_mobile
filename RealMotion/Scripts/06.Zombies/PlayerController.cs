using UnityEngine;

namespace Zombies
{
    public class PlayerController : MonoBehaviour
    {
        public Gun leftGun;
        public Gun rightGun;

        public bool _shootingAllowed;
        private int _touchesPreviousFrame;
        [SerializeField] ZombieSpawner _zombieSpawner;
        public static PlayerController Instance { get; private set; }
        private void Awake()
        {
            if (Instance != null)
                Destroy(this);
            else Instance = this;
        }
        private void Update()
        {
            if (AppManager.Instance.gameRunning.Equals(true))
            {
                if (_zombieSpawner._pool.Count > 0)
                {
                    _shootingAllowed = true;
                }
                else
                {
                    _shootingAllowed = false;
                }
            }
        }
        void OnEnable()
        {
            Timer.RoundStart += OnRoundStart;
            Timer.RoundEnd += OnRoundEnd;
        }

        void OnDisable()
        {
            Timer.RoundStart -= OnRoundStart;
            Timer.RoundEnd -= OnRoundEnd;
        }

        void OnRoundStart()
        {
           
            leftGun.enabled = true;
            rightGun.enabled = true;
        }

        void OnRoundEnd()
        {
            _shootingAllowed = false;
            leftGun.enabled = false;
            rightGun.enabled = false;
        }
    }
}
