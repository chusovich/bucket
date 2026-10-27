namespace Bucket.Domain;

public class Budget {
    private readonly List<Account> _accounts = [];
    private readonly List<Bucket> _buckets = [];

    public decimal UnallocatedFunds(DateOnly date)
    {
        decimal availableFunds = _accounts.Sum(a => a.CalculateBalance(date));
        decimal allocatedFunds = _buckets.Sum(b => b.CalculateBalance(date));
        return availableFunds - allocatedFunds;
    }
}