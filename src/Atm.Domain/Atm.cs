namespace Atm.Domain
{
    public class Atm
    {
        public int ExistingCash { get; set; }
        public int DispenseValue { get; set; }
        public bool CardIsRetained { get; set; }
        public DisplayMessage Message { get; set; }

        public Atm(int existingCash)
        {
            ExistingCash = existingCash;
        }

        public void RequestMoney(Card card, int request)
        {
            if (!card.Enabled)
            {
                CardIsRetained = true;
                Message = DisplayMessage.CardIsRetained;
                return;
            }

            if (card.AccountBalance < request)
            {
                Message = DisplayMessage.InsufficientFunds;
                return;
            }

            DispenseValue = request;
            card.AccountBalance -= request;

        }
    }
}
