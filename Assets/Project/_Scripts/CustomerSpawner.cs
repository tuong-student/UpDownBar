using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    [SerializeField] private List<Table> _tableList = new List<Table>();
    [SerializeField] private Transform _customerPref;
    [SerializeField] private float _spawnTime;

    private void Start()
    {
        StartCoroutine(SpawnerCustomer());
    }

    IEnumerator SpawnerCustomer()
    {
        while(true)
        {
            yield return new WaitForSeconds(_spawnTime);

            List<Table> availableTable = _tableList.Where(table => table.IsAvailable() == true).ToList();
            if (availableTable != null && availableTable.Count > 0)
            {
                Customer customer = Instantiate(_customerPref, this.transform.position, Quaternion.identity).GetComponent<Customer>();

                // Get random seat
                int r = Random.Range(0, availableTable.Count - 1);
                Transform seat = availableTable[r].GetSeatForCustomer(customer);
                // Check if seat valid
                customer.SetTargetSeat(seat.position);
            }
        }
    }
}