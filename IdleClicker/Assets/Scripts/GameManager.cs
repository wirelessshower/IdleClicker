using UnityEngine;
using TMPro;


public class GameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text countText;
    [SerializeField] private TMP_Text incomeText;
    [SerializeField] private StoreUpgrade[] storeUpgrades;
    [SerializeField] private int updatesPerSecond = 5; 
    
    [HideInInspector] public float count = 0;
    private float nextTimeCheck = 1f;
    private float lastIncomeValue = 0;

    private void Start()
    {
        UpdateUI();
    }

    private void Update()
    {
        if (nextTimeCheck < Time.timeSinceLevelLoad)
        {
            IdleCalculate();
            nextTimeCheck = Time.timeSinceLevelLoad + (1f / updatesPerSecond);
        }

    }

    private void IdleCalculate()
    {
        float sum = 0;
        foreach (var storeUpgrade in storeUpgrades)
        {
            sum += storeUpgrade.CalculateIncomePerSecond();
            storeUpgrade.UpgradeUI();
        }
        lastIncomeValue = sum;
        count += sum / updatesPerSecond;
        UpdateUI();
    }
    
    public void ClickAction()
    {
        count++;
        count += lastIncomeValue * .02f;
        UpdateUI();
    }

    public bool PurchaceAction(int cost)
    {
        if (count >= cost)
        {
            count -= cost;
            UpdateUI();
            return true;
        }
        return false;
    }
    
    private void UpdateUI()
    {
        countText.text = count.ToString("F0");
        incomeText.text = lastIncomeValue.ToString() + "/s";
    }
}
