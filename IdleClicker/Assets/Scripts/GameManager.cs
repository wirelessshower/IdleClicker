using UnityEngine;
using TMPro;
using YG;
using PlayerPrefs = RedefineYG.PlayerPrefs;


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
        LoadData();
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
        SaveData();
    }

    public bool PurchaseAction(int cost)
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

   private void SaveData()
       {
           PlayerPrefs.SetFloat("GameCount", count); // Сохраняем общий счетчик
           for (int i = 0; i < storeUpgrades.Length; i++)
           {
               PlayerPrefs.SetInt("StoreUpgrade_" + i + "_Level", storeUpgrades[i].GetLevel()); // Сохраняем уровень каждого улучшения
           }
           PlayerPrefs.Save(); // Записываем данные на диск
       }
   
       private void LoadData()
       {
           count = PlayerPrefs.GetFloat("GameCount", 0f); // Загружаем счетчик, по умолчанию 0
           for (int i = 0; i < storeUpgrades.Length; i++)
           {
               int level = PlayerPrefs.GetInt("StoreUpgrade_" + i + "_Level", 0); // Загружаем уровень
               storeUpgrades[i].SetLevel(level); // Устанавливаем уровень
           }
       }
}
