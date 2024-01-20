using UnityEditor;
using UnityEngine;

public abstract class UpgradeBase : MonoBehaviour
{
    public int Price;
    public float PriceMultipler = 100;
 
    [SerializeField] protected UpgradeUI _upgradeUI;
    [SerializeField] protected UpgradeAction _upgradeAction;

    #region Unity functions
    protected void Awake()
    {
        _upgradeUI.SetParent(this);
        Price = (int)PriceMultipler;
        HideUI();
        ChildAwake();
    }
    protected virtual void ChildAwake(){}
    protected void OnEnable()
    {
        _upgradeUI.OnUpgradeButtonClick += OnUpgradeButtonClickHandler;
        GameplayManager.Instance.OnNextDay += HideUI;
        UIManager.Instance.OnStorePhase += ShowUI;
        UIManager.Instance.OnStorePhase += OnStorePhaseHandler;
        ChildOnEnable();
    }
    protected void OnDisable()
    {
        _upgradeUI.OnUpgradeButtonClick -= OnUpgradeButtonClickHandler;
        GameplayManager.Instance.OnNextDay -= HideUI;
        UIManager.Instance.OnStorePhase -= ShowUI;
        UIManager.Instance.OnStorePhase -= OnStorePhaseHandler;
        ChildOnDisable();
    }
    protected virtual void ChildOnDisable(){}
    protected virtual void ChildOnEnable(){}
    #endregion

    private void OnUpgradeButtonClickHandler()
    {
        UpgradeManager.Instance.Upgrade(this);
    }

    protected virtual void OnStorePhaseHandler(){}
    public void Upgrade()
    {
        _upgradeAction.Invoke();
    }

    public virtual void AutoSetNewPrice(int upgradeTime)
    {
        Price += (int) (upgradeTime * PriceMultipler);
    }

    public void ShowUI()
    {
        _upgradeUI.gameObject.SetActive(true);
        _upgradeUI.UpdateMoneyText();
    }
    public void HideUI()
    {
        _upgradeUI.gameObject.SetActive(false);
    }
}
