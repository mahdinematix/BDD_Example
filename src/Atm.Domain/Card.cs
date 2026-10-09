namespace Atm.Domain;

public class Card
{
    public int AccountBalance { get; set; }
    public bool Enabled { get; set; }

    public Card(int accountBalance)
    {
        AccountBalance = accountBalance;
    }

    public void IsValid(bool isValid)
    {
        Enabled = isValid;
    }
}

