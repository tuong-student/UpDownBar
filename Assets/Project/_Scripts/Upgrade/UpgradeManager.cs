using System;
using System.Collections.Generic;
using NOOD;

namespace Game
{
    public class UpgradeManager : MonoBehaviorInstance<UpgradeManager>
    {
        private const string DO_NOT_ENOUGH_MONEY_ID = "DontHaveEnoughMoney";
        private List<UpgradeBase> _upgradeBaseList = new List<UpgradeBase>();

        protected override void ChildAwake()
        {
            _upgradeBaseList = new List<UpgradeBase>();
        }

        public void Upgrade(UpgradeBase upgradeBase)
        {
            if (MoneyManager.Instance.PayMoney(upgradeBase.Price))
            {
                upgradeBase.Upgrade();
                if(_upgradeBaseList.Contains(upgradeBase))
                {
                    int index = _upgradeBaseList.IndexOf(upgradeBase);
                    upgradeBase.UpdatePrice();
                }
                else
                {
                    _upgradeBaseList.Add(upgradeBase);
                    upgradeBase.UpdatePrice();
                }
            }
            else
            {
                NotifyManager.Instance.Show(DO_NOT_ENOUGH_MONEY_ID.GetText());
            }
        }
    }

}
