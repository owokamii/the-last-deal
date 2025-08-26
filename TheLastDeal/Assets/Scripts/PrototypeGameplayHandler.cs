using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PrototypeGameplayHandler : MonoBehaviour
{
    [Header("UI Text")]
    [SerializeField] private TextMeshProUGUI reputationPointsText;
    [SerializeField] private TextMeshProUGUI moneyPointsText;
    [SerializeField] private TextMeshProUGUI inventorySlotText;

    [Space(10f)] [SerializeField] private Button appraiseButton;
    
    [Header("Initial Settings")]
    [SerializeField] private int initialReputationPoints;   // 10
    [SerializeField] private int initialMoneyPoints;        // 10
    private int initialInventorySlot;
    
    [Header("Maximum Settings")]
    [SerializeField] private int maximumInventorySlots;
    
    //private Dictionary<bool, int> itemValueList = new Dictionary<bool, int>();
    private List<int> itemValueList = new List<int>();
    private int currentItemValue;
    
    void Start()
    {
        //Debug.Log("<color=green>Initializing...</color>");
        
        InitializeText();
        
        //Debug.Log("<color=green>Gameplay Starts</color>");
    }

    private void InitializeText()
    {
        reputationPointsText.text = initialReputationPoints.ToString();
        moneyPointsText.text = initialMoneyPoints.ToString();
        inventorySlotText.text = initialInventorySlot.ToString();
    }

    private void Buy()
    {
        GetItemType();
        DecreaseMoneyPoints(currentItemValue);
        AddToInventory(currentItemValue);
        IncreaseInventory();
    }

    private void Pass()
    {
        Debug.Log("<color=green>Passed a customer, lost rep...</color>");
        
        DecreaseReputationPoints(1);
    }

    private void Appraise(int slotNum)
    {
        if (itemValueList.Count < slotNum+1)
        {
            Debug.Log($"<color=red>No item to sell in slot no.{slotNum}</color>");
            return;
        }

        if (itemValueList[slotNum] == 0)
        {
            Debug.Log($"<color=red>No item to sell in slot no.{slotNum}</color>");
            return;
        }
        
        // randomize a value true or false
        int realItem = Random.Range(0, 2);
        int itemValue = itemValueList[slotNum];
        itemValueList[slotNum] = 0;
        
        Debug.Log($"<color=green>Appraising no.{slotNum} for {itemValue}G...</color>");
        
        // based on if true or false, reward or penalize player
        if (realItem == 0)
        {
            Debug.Log($"<color=red>FAKE</color>");
            DecreaseReputationPoints(itemValue);
        }
        else
        {
            Debug.Log($"<color=lime>REAL</color>");
            IncreaseMoneyPoints((itemValue*2));
        }
        
        DecreaseInventory();
    }

    private void BuyRep()
    {
        if (initialMoneyPoints > 0)
        {
            DecreaseMoneyPoints(1);
            IncreaseReputationPoints(1);
            Debug.Log($"<color=green>Bought rep point...</color>");
        }
        else
            Debug.Log($"<color=red>Could not buy rep point...</color>");
    }

    private void AddToInventory(int newValue)
    {
        for (int i = 0; i < itemValueList.Count; i++)
            if (itemValueList[i].Equals(0))
            {
                Debug.Log($"<color=green>Added to slot no.{i}</color>");
                itemValueList[i] = newValue;
                break;
            }
    }
    
    private void IncreaseInventory()
    {
        if (initialInventorySlot <= maximumInventorySlots)
        {
            initialInventorySlot++;
            inventorySlotText.text = initialInventorySlot.ToString();
        }
    }

    private void DecreaseInventory()
    {
        if (initialInventorySlot > 0)
        {
            initialInventorySlot--;
            inventorySlotText.text = initialInventorySlot.ToString();
        }
    }

    private void IncreaseReputationPoints(int value)
    {
        initialReputationPoints += value;
        reputationPointsText.text = initialReputationPoints.ToString();
    }

    private void DecreaseReputationPoints(int value)
    { 
        initialReputationPoints -= value; 
        reputationPointsText.text = initialReputationPoints.ToString();
    }

    private void IncreaseMoneyPoints(int value)
    {
        initialMoneyPoints += value;
        moneyPointsText.text = initialMoneyPoints.ToString();
    }

    private void DecreaseMoneyPoints(int value)
    {
        initialMoneyPoints -= value;
        moneyPointsText.text = initialMoneyPoints.ToString();
    }
    
    /*private void HandleCustomer()
    {
        Debug.Log("<color=green>A customer has arrived.</color>");
        Debug.Log("<color=green>Buy or nah?</color>");
    }*/

    private void GetItemType()
    {
        // randomize a num to get item's worth
        int itemValue = Random.Range(1, 3);
        currentItemValue = itemValue;
        itemValueList.Add(itemValue);
        
        Debug.Log($"<color=green>Bought item for {itemValue}G.</color>");
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) // buy
        {
            if (initialInventorySlot != maximumInventorySlots)
                Buy();
            else
                Debug.Log("<color=red>Cannot buy anymore items...</color>");
        }

        if (Input.GetKeyDown(KeyCode.Alpha2)) // pass
            Pass();

        if (Input.GetKeyDown(KeyCode.Alpha3)) // appraise 1
            Appraise(0);

        if (Input.GetKeyDown(KeyCode.Alpha4)) // appraise 2
            Appraise(1);
        
        if (Input.GetKeyDown(KeyCode.Alpha5)) // appraise 3
            Appraise(2);
        
        if (Input.GetKeyDown(KeyCode.Alpha6)) // appraise 3
            BuyRep();
    }
}
