[BDD_Example_README.md](https://github.com/user-attachments/files/33243079/BDD_Example_README.md)
# BDD Example — ATM Domain

A small .NET 10 project I built to practice **Behavior-Driven Development (BDD)**. I modeled a few ATM cash-withdrawal behaviors and used scenario-based tests to describe what should happen from an account holder's point of view.

The purpose is not to build a complete banking system. Instead, this is a focused exercise in translating requirements into readable, executable scenarios and checking the observable outcomes of a domain operation.

## What This Project Demonstrates

- Writing scenarios with **Given / When / Then** steps using [TestStack.BDDfy](https://github.com/TestStack/BDDfy).
- Running scenarios with **xUnit** and expressing expectations with **FluentAssertions**.
- Keeping the ATM domain model separate from the test scenarios.
- Generating a custom HTML report for the BDD scenarios.

## Scenarios

The project describes three cash-withdrawal scenarios:

1. **Insufficient account balance** — the account has `$10` and the user requests `$20`; the ATM should dispense no cash, report insufficient funds, and return the card.
2. **Invalid card** — the card is invalid; the ATM should dispense no cash, display a card-retained message, and retain the card.
3. **Successful withdrawal** — the account has `$20` and the user requests `$16`; the ATM should dispense `$16`, reduce the account balance to `$4`, and return the card.

The same scenarios are documented in `AtmScenario.txt` and implemented as executable BDDfy scenarios in `WithdrawCashFromAnAtm.cs`.

## Domain Model

The domain project contains three small types:

- **`Atm`** — handles a cash request and exposes the resulting state, including the dispensed amount, whether the card is retained, and the display message.
- **`Card`** — holds the account balance and whether the card is enabled/valid.
- **`DisplayMessage`** — defines the display outcomes, including `InsufficientFunds` and `CardIsRetained`.

The main behavior is implemented in `Atm.RequestMoney(...)`. A successful request sets the dispensed amount and decreases the account balance. An invalid card causes the ATM to retain the card, while an insufficient account balance prevents the withdrawal and sets the corresponding message.

**Current scope limitation:** the ATM stores an `ExistingCash` value, and the scenarios initialize it with cash, but `RequestMoney(...)` does not currently validate or decrement the ATM's cash reserve. The example focuses on card state and the account balance rather than modeling the full cash-inventory rules of a real ATM.

## How BDD Is Implemented

`WithdrawCashFromAnAtm.cs` describes the overall story and uses BDDfy's fluent API to define the `Given`, `When`, `Then`, and `And` steps. The reusable methods in `Steps.cs` arrange the initial state and assert the observable result, using FluentAssertions for readable expectations.

`CustomHtmlReport.cs` customizes the HTML report header, description, and output filename (`Atm.html`). The report is registered with BDDfy using `HtmlReporter` and `MetroReportBuilder`.

The test project uses:

- **TestStack.BDDfy 8.0.1.3** — organizes scenarios and generates BDD reports.
- **xUnit 2.9.3** — discovers and runs the scenarios.
- **FluentAssertions 8.10.0** — provides expressive assertions.
- **Microsoft.NET.Test.Sdk 17.14.1** — integrates with the .NET test runner.
- **coverlet.collector 6.0.4** — provides test-coverage collection support.

> **Scope note:** The test project is named `Acceptance`, but its scenarios instantiate the domain objects directly. These are acceptance-style behavioral tests of the domain model, not full end-to-end tests through a UI, HTTP API, database, or external service.

## Solution Structure

```text
BDD_Example/
├── src/
│   └── Atm.Domain/
│       ├── Atm.cs
│       ├── Card.cs
│       ├── DisplayMessage.cs
│       └── Atm.Domain.csproj
├── tests/
│   └── Atm.Domain.Tests.Acceptance/
│       ├── AtmScenario.txt
│       ├── CustomHtmlReport.cs
│       ├── Steps.cs
│       ├── WithdrawCashFromAnAtm.cs
│       └── Atm.Domain.Tests.Acceptance.csproj
└── Bdd_Example.slnx
```

## Tech Stack

- C# and .NET 10
- TestStack.BDDfy
- xUnit
- FluentAssertions
- Microsoft.NET.Test.Sdk
- Coverlet Collector

## Getting Started

### Prerequisites

- .NET 10 SDK

### Run the tests

From the repository root, run:

```bash
dotnet restore Bdd_Example.slnx
dotnet test Bdd_Example.slnx
```

## What I Wanted to Learn

I created this project as a focused exercise in thinking from behavior and expected outcomes first. It gave me hands-on practice expressing requirements as scenarios, separating setup/actions/assertions into readable steps, and using BDDfy reports to make test results easier to review.
