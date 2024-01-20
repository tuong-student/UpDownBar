using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeerCup : MonoBehaviour
{
    private string PUNISH_TAG = "PunishCollider";
    private Customer _customer;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag(PUNISH_TAG))
        {
            Destroy(this.gameObject);
            BeerServeManager.Instance.OnServerFail?.Invoke();            
        }
    }
    public void SetCustomer(Customer customer)
    {
        _customer = customer;
    }
}
