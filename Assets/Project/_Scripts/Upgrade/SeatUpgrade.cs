using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeatUpgrade : UpgradeBase
{
    private Table _table;

    protected override void ChildAwake()
    {
        _table = GetComponent<Table>();
        _upgradeAction = new UpgradeAction(() => _table.UnlockSeat());
    }
    protected override void ChildOnEnable()
    {
        _upgradeAction.OnComplete += SetNewPosition;
    }
    protected override void ChildOnDisable()
    {
        _upgradeAction.OnComplete -= SetNewPosition;
    }

    public void SetNewPosition()
    {
        Vector3 tempPos = _table.GetUpgradePosition();
        Vector3 newPos = new Vector3(tempPos.x, this.transform.position.y, tempPos.z);
        _upgradeUI.SetPosition(newPos);
    }
    protected override void OnStorePhaseHandler()
    {
        SetNewPosition();
    }
    public override void AutoSetNewPrice(int upgradeTime)
    {
        base.AutoSetNewPrice(upgradeTime);
    }
}
