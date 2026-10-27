namespace Bucket.Domain;

public class Transaction
{
    public Guid Id { get; private set; }
    public DateOnly Date { get; private set; }
    public string Payee { get; private set; }
    public string Notes { get; private set; }
    public decimal Amount { get; private set; }

    public Transaction(
        Guid id,
        DateOnly date,
        string payee,
        string? notes,
        decimal amount)
    {
        if (id == Guid.Empty)
            throw new DomainException("Transaction id cannot be empty.");

        if (string.IsNullOrWhiteSpace(payee))
            throw new DomainException("Payee cannot be empty.");

        Id = id;
        Date = date;
        Payee = payee.Trim();
        Notes = notes ?? string.Empty;
        Amount = amount;
    }

    public void UpdateNotes(string notes) {
        Notes = notes.Trim() ?? string.Empty;
    }

}
