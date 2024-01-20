using System.Collections;
using System.Collections.Generic;
using NOOD;
using UnityEngine;

public class UpgradeManager : MonoBehaviorInstance<UpgradeManager>
{
    private Dictionary<UpgradeBase, int> _upgradeTimeDic = new Dictionary<UpgradeBase, int>();

    public void Upgrade(UpgradeBase upgradeBase)
    {
        if (MoneyManager.Instance.PayMoney(upgradeBase.Price))
        {
            upgradeBase.Upgrade();
            if(_upgradeTimeDic.ContainsKey(upgradeBase))
            {
                _upgradeTimeDic[upgradeBase] += 1;
                upgradeBase.AutoSetNewPrice(_upgradeTimeDic[upgradeBase]);
            }
            else
            {
                _upgradeTimeDic.Add(upgradeBase, 2);
                upgradeBase.AutoSetNewPrice(_upgradeTimeDic[upgradeBase]);
            }
        }
        else
            Debug.Log("Do not enough money");
    }

    
}
