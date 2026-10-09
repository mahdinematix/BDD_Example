using FluentAssertions;

namespace Atm.Domain.Tests.Acceptance
{
    public class Steps
    {
        private Card _card;
        private Atm _atm;

        public void TheAccountBalanceIs(int balance)
        {
            _card = new Card(balance);
        }

        public void TheCardIsValid(bool isValid)
        {
            _card.IsValid(isValid);
        }

        public void TheMachineContainsEnoughMoney()
        {
            _atm = new Atm(50);
        }

        public void TheAccountHolderRequests(int request)
        {
            _atm.RequestMoney(_card, request);
        }

        public void TheAtmShouldNotDispenseAnyMoney()
        {
            _atm.DispenseValue.Should().Be(0);
        }

        public void TheAtmShouldSay(DisplayMessage message)
        {
            _atm.Message.Should().Be(message);
        }

        public void TheCardShouldBeReturned()
        {
            _atm.CardIsRetained.Should().BeFalse();
        }


        public void TheCardShouldBeRetained()
        {
            _atm.CardIsRetained.Should().BeTrue();
        }

        public void TheAtmShouldDispense(int amount)
        {
            _atm.DispenseValue.Should().Be(amount);
        }

        public void TheAccountBalanceShouldBecome(int amount)
        {
            _card.AccountBalance.Should().Be(amount);
        }
    }
}
