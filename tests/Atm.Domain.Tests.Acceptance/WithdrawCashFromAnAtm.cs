using TestStack.BDDfy;
using TestStack.BDDfy.Configuration;
using TestStack.BDDfy.Reporters.Html;

namespace Atm.Domain.Tests.Acceptance
{
    [Story(AsA = "Account Holder",IWant = "to withdraw cash from an ATM", SoThat = "I can get money when the bank is closed")]
    public class WithdrawCashFromAnAtm
    {
        private readonly Steps __;

        public WithdrawCashFromAnAtm()
        {
            Configurator.BatchProcessors.Add(new HtmlReporter(new CustomHtmlReport(), new MetroReportBuilder()));
            __ = new Steps();
        }
        

        [Fact]
        public void AccountHasInsufficientFunds()
        {
            this.Given(_ => __.TheAccountBalanceIs(10), "Given the account balance is $10")
                    .And(_ => __.TheCardIsValid(true), "And Given The Card Is Valid")
                    .And(_ => __.TheMachineContainsEnoughMoney(), "And Given The Machine Contains Enough Money")
                .When(_ => __.TheAccountHolderRequests(20), "When The Account Holder Requests $20")
                .Then(_ => __.TheAtmShouldNotDispenseAnyMoney(), "Then The Atm Should Not Dispense Any Money")
                    .And(_ => __.TheAtmShouldSay(DisplayMessage.InsufficientFunds), "And The Atm Should Say There Are Insufficient Funds")
                    .And(_ => __.TheCardShouldBeReturned(), "And The Card Should Be Returned")
                .BDDfy();
        }

        [Fact]
        public void AtmRetainsTheCard()
        {
            this.Given(_ => __.TheAccountBalanceIs(10), "Given The Account Balance Is $10")
                    .And(_ => __.TheCardIsValid(false), "And The Card Is Invalid")
                    .And(_ => __.TheMachineContainsEnoughMoney(), "And The Machine Contains Enough Money")
                .When(_ => __.TheAccountHolderRequests(20), "When The Account Holder Requests $20")
                .Then(_ => __.TheAtmShouldNotDispenseAnyMoney(), "Then The Atm Should Not Dispense Any Money")
                    .And(_ => __.TheAtmShouldSay(DisplayMessage.CardIsRetained), "And The Atm Should Say Card Is Retained")
                    .And(_ => __.TheCardShouldBeRetained(), "And The Card Should Be Retained")
                .BDDfy();
        }

        [Fact]
        public void AccountHolderCanWithdrawCashFromAtm()
        {
            this.Given(_ => __.TheAccountBalanceIs(20), "Given The Account Balance Is $20")
                .And(_ => __.TheCardIsValid(true), "And The Card Is Invalid")
                .And(_ => __.TheMachineContainsEnoughMoney(), "And The Machine Contains Enough Money")
                .When(_ => __.TheAccountHolderRequests(16), "When The Account Holder Requests $16")
                .Then(_ => __.TheAtmShouldDispense(16), "Then The Atm Should Dispense $16")
                .And(_ => __.TheAccountBalanceShouldBecome(4), "And The Account Balance Should Become $4")
                .And(_ => __.TheCardShouldBeReturned(), "And The Card Should Be Returned")
                .BDDfy();
        }
    }
}
