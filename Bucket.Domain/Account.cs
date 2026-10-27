namespace Bucket.Domain;

public class Account
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public decimal StartingBalance { get; private set; }

    public decimal CurrentBalance
        => StartingBalance + _transactions.Sum(t => t.Amount);

    public decimal CalculateBalance(DateOnly date)
    {
        return StartingBalance + _transactions
            .Where(t => t.Date <= date)
            .Sum(t => t.Amount);
    }

    private readonly List<Transaction> _transactions = [];
    public IReadOnlyCollection<Transaction> Transactions
        => _transactions.AsReadOnly();

    public Account(
        Guid id,
        string name,
        decimal startingBalance,
        List<Transaction> transactions)
    {
        if (id == Guid.Empty)
            throw new DomainException("Account id cannot be empty.");

        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Account name cannot be empty.");

        Id = id;
        Name = name.Trim();
        StartingBalance = startingBalance;
        _transactions = transactions;
    }

    public void Rename(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Account name cannot be empty.");

        Name = name.Trim();
    }

    public void UpdateStartingBalance(decimal startingBalance)
    {
        StartingBalance = startingBalance;
    }

    public void AddTransaction(Transaction transaction)
    {
        _transactions.Add(transaction);
    }

    public bool RemoveTransaction(Guid transactionId)
    {
        var transaction = GetTransaction(transactionId);
        if (transaction is null)
            return false;

        _transactions.Remove(transaction);
        return true;
    }

    public void ReconcileToCurrentBalance(decimal currentBalance)
    {
        StartingBalance = currentBalance - _transactions.Sum(t => t.Amount);
    }

    private Transaction? GetTransaction(Guid transactionId)
    {
        return _transactions.FirstOrDefault(t => t.Id == transactionId);
    }
}
