namespace Bucket.Core;

public class Bucket {
    private List<Transaction> _transactions;
    private List<Allocation> _allocations;

    public decimal GetBalance(DateOnly date) {
        decimal transactions = _transactions
            .Where(t => t.Date < date)
            .Sum(t => t.Amount);
        decimal allocations = _allocations
            .Where(t => t.Date < date)
            .Sum(t => t.Amount);

        return allocations - transactions;       
    }
}