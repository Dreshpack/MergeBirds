using UnityEngine;

public class IncomeManager : Singleton<IncomeManager>
{
    private Cell[] allCells;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        // Find all cells in the scene
        FindAllCells();
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
