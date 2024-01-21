using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

namespace Game
{

    public class Customer : MonoBehaviour
    {
        [SerializeField] private int _money = 5;
        private Vector3 _targetSeat;
        private float _speed = 4f;
        private float _rotateSpeed = 10;
        private bool _isRequestBeer;
        private bool _isServed;

        #region Unity functions
        void OnEnable()
        {
            GameplayManager.Instance.OnEndDay += OnEndDayHandler;
        }
        void OnDisable()
        {
            GameplayManager.Instance.OnEndDay -= OnEndDayHandler;
        }
        void Start()
        {
        }
        private void Update()
        {
            Move();    
        }
        private void OnTriggerEnter(Collider other)
        {
            if(other.transform.TryGetComponent<BeerCup>(out BeerCup beerCup) && _isServed == false && _isRequestBeer == true)
            {
                Destroy(other.gameObject);
                Complete();
            }
        }
        #endregion

        #region Event functions
        private void OnEndDayHandler()
        {
            Return();
        }
        #endregion

        private void Move()
        {
            float distance = Vector3.Distance(this.transform.position, _targetSeat);
            if(distance > 0.1f)
            {
                Vector3 direction = _targetSeat - this.transform.position;
                direction = Vector3.Normalize(direction);
                this.transform.position += direction * _speed * TimeManager.UnScaledDeltaTime;
                this.transform.DOKill(); // To stop rotate
                this.transform.forward = Vector3.Lerp(this.transform.forward, direction, TimeManager.UnScaledDeltaTime* _rotateSpeed);
            }
            else
            {
                this.transform.DORotate(Vector3.zero, 0.5f); 
                // Get to seat
                if(!_isRequestBeer)
                {
                    TableManager.Instance.RequestBeer(this);
                    _isRequestBeer = true;
                }
            }
        }
        public void SetTargetPosition(Vector3 position)
        {
            _targetSeat = position;
        }

        public void Complete()
        {
            _isServed = true;
            TableManager.Instance.CustomerComplete(this); // Return seat
            SetTargetPosition(CustomerSpawner.Instance.transform.position); // Move out
            Destroy(this.gameObject, 4f);
            // Pay money
            MoneyManager.Instance.AddMoney(_money);
        }
        private void Return()
        {
            _isServed = true;
            TableManager.Instance.CustomerComplete(this); // Return seat
            SetTargetPosition(CustomerSpawner.Instance.transform.position); // Move out
            Destroy(this.gameObject, 2f);
        }
    }
}
