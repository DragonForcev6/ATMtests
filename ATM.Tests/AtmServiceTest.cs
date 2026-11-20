using Xunit;
using ATM;
using Xunit.Sdk;
using System.Runtime;

public class AtmServiceTest
{
    Card card;
    AtmService atm;

    void setupService()
    {
        Account account = new Account(5000);
        card = new Card("1234-5678", "1234", account);
        atm = new AtmService(7000);
    }

    [Fact]
    public void InsertCardTest()
    {
        // setup
        Account account = new Account(5000);
        Card card = new Card("1234-5678", "1234", account);
        AtmService atm = new AtmService(5000);
        // test
        atm.InsertCard(card);
        Assert.True(atm.HasCardInserted);

    }

    [Theory]
    [InlineData("1234", true)]
    [InlineData("0123", false)]
    [InlineData("12345", false)]
    [InlineData("01234", false)]
    [InlineData("123", false)]

    public void EnterPinTest(string pin, bool expected)
    {
        // setup
        setupService();
        atm.InsertCard(card);

        bool actual = atm.EnterPin(pin);
        Assert.Equal(expected, actual);

    }

    [Fact]
    public void EjectCardTest()
    {
        // setup
        setupService();
        atm.InsertCard(card);
        atm.EnterPin("1234");
        // test
        atm.EjectCard();
        Assert.False(atm.IsAuthenticated);
        Assert.False(atm.HasCardInserted);
    }

    [Fact]
    public void GetBalanceTest()
    {
        // setup
        setupService();
        atm.InsertCard(card);
        atm.EnterPin("1234");

        //test
        int actual = atm.GetBalance();
        Assert.Equal(5000, actual);

    }

    [Fact]
    public void GetBalanceWithoutPinTest()
    {
        //setup
        setupService();
        atm.InsertCard(card);

        // test
        Assert.Throws<InvalidOperationException>(() => atm.GetBalance());

    }

    [Fact]
    public void GetBalanceWithoutCardTest()
    {
        // setup
        setupService();

        // test
        Assert.Throws<InvalidOperationException>(() => atm.GetBalance());
    }

    [Fact]
    public void WithdrawTest()
    {
        // setup
        setupService();
        atm.InsertCard(card);
        atm.EnterPin("1234");

        // test
        bool success = atm.Withdraw(5000);

        //assert
        Assert.True(success); // uttag lyckades
        Assert.Equal(0, atm.GetBalance()); // kontot 5000 - 5000 = 0
        Assert.Equal(2000, atm.AtmBalance); // bankomat: 7000 - 5000 = 2000

    }

    [Fact]
    public void WithdrawInsufficientAccountTest()
    {
        setupService();
        atm.InsertCard(card);
        atm.EnterPin("1234");

        // test
        bool success = atm.Withdraw(6000);

        //assert
        Assert.False(success); // uttaget ska inte lyckats
        Assert.Equal(5000, atm.GetBalance()); // kontot 5000 det går inte att överskrida kontosaldo.
        Assert.Equal(7000, atm.AtmBalance); // bankatomaten 7000
    }

    [Fact]
    public void WithdrawInsufficientAtmTest()
    {
        setupService();
        atm.InsertCard(card);
        atm.EnterPin("1234");
        card.Account.Deposit(3000);
        

        // test
        bool success = atm.Withdraw(8000);

        //assert
        Assert.False(success); // uttaget ska inte lyckats
        Assert.Equal(8000, atm.GetBalance()); // kontot 8000 det går inte att överskrida bankomatsaldo.
        Assert.Equal(7000, atm.AtmBalance); // bankomat: 7000
    }


}


