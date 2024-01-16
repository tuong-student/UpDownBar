using Unity.VisualScripting;
using UnityEngine;

public class Customer : MonoBehaviour
{
    private Vector3 _targetSeat;
    private float _speed = 4f;
    private bool _isRequestBeer;

    private void Update()
    {
        Move();    
    }

    private void Move()
    {
        float distance = Vector3.Distance(this.transform.position, _targetSeat);
        if(distance > 0.1f)
        {
            Vector3 direction = _targetSeat - this.transform.position;
            direction = Vector3.Normalize(direction);
            this.transform.position += direction * _speed * Time.deltaTime;
        }
        else
        {
            // Get to seat
            if(!_isRequestBeer)
            {
                TableManager.Instance.RequestBeer(this);
                _isRequestBeer = true;
            }
        }
    }
    public void SetTargetSeat(Vector3 position)
    {
        _targetSeat = position;
    }

    public void Complete()
    {
        TableManager.Instance.CustomerComplete(this);
    }
}
