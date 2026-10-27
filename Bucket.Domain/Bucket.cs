using System.Transactions;

namespace Bucket.Domain;

public class Bucket(
    Guid id,
    string name,
    List<Allocation> allocations,
    List<Transaction> transactions)
{
    public Guid Id { get; private set; } = id;
    public string Name { get; private set; } = name;

    private readonly List<Allocation> _allocations = allocations;
    public IReadOnlyCollection<Allocation> Allocations
        => _allocations.AsReadOnly();

    private readonly List<Transaction> _transactions = transactions;
    public IReadOnlyCollection<Transaction> Transactions
        => _transactions.AsReadOnly();

    public decimal CalculateBalance(DateOnly date)
    {
        var allocated = _allocations
            .Where(a => a.Date <= date)
            .Sum(a => a.Amount);

        var spent = _transactions
            .Where(t => t.Date <= date)
            .Sum(t => t.Amount);

        return allocated - spent;
    }

}
