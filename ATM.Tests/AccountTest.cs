namespace ATM.ATM.Tests;

using Xunit;

public class AccountTest
{
    

    [Fact]
    public void IsAccountTest()
    {
        int accountBalance = 5000;
        Account account = new Account(accountBalance);
        Assert.IsType<Account>(account);
        Assert.Equal(accountBalance, accountBalance);
    }

    [Fact]
    public void WithdrawTest()
    {
        // set up
        int accountBalance = 5000;
        Account account = new Account(accountBalance);
        // test
        int withdrawal = 1000;
        account.Withdraw(withdrawal);

        int expectedBalance = 4000;
        Assert.Equal(expectedBalance, accountBalance);
    }

    [Fact]
    public void WithdrawNotNegativeTest()
    {
        // set up
        int accountBalance = 1000;
        Account account = new Account(accountBalance);
        // test
        int withdrawal = 2000;
        account.Withdraw(withdrawal);

        int expectedBalance = 1000;
        Assert.Equal(expectedBalance, accountBalance);
    }

}
