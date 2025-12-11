using UnityEngine;
using TMPro;

public class IncomeManager : Singleton<IncomeManager>
{
    [Header("Balance Settings")]
    [SerializeField] private TextMeshProUGUI balanceText;
    [SerializeField] private int startingBalance = 0;

    private Cell[] allCells;
    private int currentBalance;
    private float incomeTimer = 0f;
    private const float INCOME_INTERVAL = 1f; // Add income every second

    protected override void Awake()
    {
        base.Awake();
        currentBalance = startingBalance;
    }

    private void Start()
    {
        // Find all cells in the scene
        FindAllCells();

        // Update UI with starting balance
        UpdateBalanceDisplay();
    }

    private void Update()
    {
        // Increment timer
        incomeTimer += Time.deltaTime;

        // Check if a second has passed
        if (incomeTimer >= INCOME_INTERVAL)
        {
            // Add income to balance
            AddIncomeToBalance();

            // Reset timer
            incomeTimer = 0f;
        }
    }

    /// <summary>
    /// Add current income from all birds to the balance
    /// </summary>
    private void AddIncomeToBalance()
    {
        int income = GetTotalIncome();

        if (income > 0)
        {
            currentBalance += income;
            UpdateBalanceDisplay();
        }
    }

    /// <summary>
    /// Update the balance text UI
    /// </summary>
    private void UpdateBalanceDisplay()
    {
        if (balanceText != null)
        {
            balanceText.text = $"${currentBalance}";
        }
    }

    /// <summary>
    /// Get current balance
    /// </summary>
    public int GetBalance()
    {
        return currentBalance;
    }

    /// <summary>
    /// Add money to balance manually
    /// </summary>
    public void AddBalance(int amount)
    {
        currentBalance += amount;
        UpdateBalanceDisplay();
    }

    /// <summary>
    /// Remove money from balance (returns true if successful)
    /// </summary>
    public bool RemoveBalance(int amount)
    {
        if (currentBalance >= amount)
        {
            currentBalance -= amount;
            UpdateBalanceDisplay();
            return true;
        }
        return false;
    }

    /// <summary>
    /// Set balance to specific amount
    /// </summary>
    public void SetBalance(int amount)
    {
        currentBalance = amount;
        UpdateBalanceDisplay();
    }

    public void FindAllCells()
    {
        allCells = FindObjectsOfType<Cell>();
        Debug.Log($"Found {allCells.Length} cells on the board");
    }

    /// <summary>
    /// Calculate total income from all birds currently on the board
    /// </summary>
    /// <returns>Total income value from all birds</returns>
    public int GetTotalIncome()
    {
        if (allCells == null || allCells.Length == 0)
        {
            FindAllCells();
        }

        int totalIncome = 0;

        foreach (Cell cell in allCells)
        {
            if (!cell.IsFree() && cell.currentItem != null)
            {
                totalIncome += cell.currentItem.income;
            }
        }

        return totalIncome;
    }

    /// <summary>
    /// Get count of birds currently on the board
    /// </summary>
    /// <returns>Number of birds on the board</returns>
    public int GetBirdCount()
    {
        if (allCells == null || allCells.Length == 0)
        {
            FindAllCells();
        }

        int birdCount = 0;

        foreach (Cell cell in allCells)
        {
            if (!cell.IsFree())
            {
                birdCount++;
            }
        }

        return birdCount;
    }

    /// <summary>
    /// Get income breakdown by bird type
    /// </summary>
    public void LogIncomeBreakdown()
    {
        if (allCells == null || allCells.Length == 0)
        {
            FindAllCells();
        }

        Debug.Log("=== Income Breakdown ===");
        int totalIncome = 0;

        foreach (Cell cell in allCells)
        {
            if (!cell.IsFree() && cell.currentItem != null)
            {
                Debug.Log($"Bird level {cell.currentItem.number}: Income = {cell.currentItem.income}");
                totalIncome += cell.currentItem.income;
            }
        }

        Debug.Log($"Total Income: {totalIncome}");
    }
}
