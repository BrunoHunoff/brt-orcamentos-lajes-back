public class BudgetSlab {
    public int Id { get; init; }
    public int SlabId { get; private set; }
    public int BudgetId { get; private set; }
    public int SlabsNumber { get; private set; }
    public double Length { get; private set; }
    public double Width { get; private set; }

    public BudgetSlab(){}

    public BudgetSlab(int slabId, int budgetId, int slabsNumber ,double length, double width) {
        SlabId = slabId;
        BudgetId = budgetId;
        SlabsNumber = slabsNumber;
        Length = length;
        Width = width;
    }

    public void UpdateBudgetSlab(int slabId, int slabsNumber ,double length, double width) {
        SlabId = slabId;
        SlabsNumber = slabsNumber;
        Length = length;
        Width = width;
    }
}