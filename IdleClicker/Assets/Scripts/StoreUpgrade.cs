using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class StoreUpgrade : MonoBehaviour
{
    [Header("Components")]
    public TMP_Text priceText;
    public TMP_Text incomeInfoText;
    public TMP_Text upgradeNameText;
    public Button button;
    public Image characterImage;
    
    [Header("Generator values")]
    public string upgradeName;
    public int startPrice;
    public float upgradePriceMultiplier;
    public float cookesPerUpgrade = 0.1f;

    [Header("Managers")]
    public GameManager gameManager;
    
    private int level = 0;

    private void Start()
    {
        UpgradeUI();
    }

    public void ClickAction()
    {
        int price = calculatePrice();
        bool purchaseSuccess = gameManager.PurchaseAction(price);
        if (purchaseSuccess)
        {
            level++;
            UpgradeUI();
        }
    }
    

    public void UpgradeUI()
    {
        priceText.text = calculatePrice().ToString();
        
        bool canAfford = gameManager.count >= calculatePrice();
        button.interactable = canAfford;
        
        bool isPurchased = level > 0;
        incomeInfoText.text = isPurchased ? level.ToString() + " X " + cookesPerUpgrade + "/S" :
            level.ToString() + " X " + "?" + "/S" ;
        characterImage.color = isPurchased ? Color.white : Color.black;
        upgradeNameText.text = isPurchased ? upgradeName : "???";
    }

    int calculatePrice()
    {
        int price = Mathf.RoundToInt(startPrice * Mathf.Pow(upgradePriceMultiplier, level));
        return price;
    }

    public float CalculateIncomePerSecond()
    {
        return cookesPerUpgrade * level;
    }

    public int GetLevel()
    {
        return level;
    }

    public void SetLevel(int newLevel)
    {
        level = newLevel;
        UpgradeUI();
    }
    
}
