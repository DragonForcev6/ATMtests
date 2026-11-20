namespace ATM.ATM.Tests;

using System.Diagnostics.CodeAnalysis;
using Xunit;

public class CardTest
{
    Account account;
    Card card;
    
    void setupService()
    {
        account = new Account(5000);
        card = new Card("1234-5678", "1234", account);
    }

    [Fact]
    public void RightCardNumberTest()
    {
        // setup
        setupService();
        

        Assert.Equal("1234-5678", card.CardNumber);

    }

    [Fact]
    public void RightPinCodeTest()
    {
        // setup
        setupService();
        

        Assert.Equal("1234", card.PinCode);
    }

    [Fact]
    public void RightCardAccount()
    {
        // setup
        setupService();


        Assert.Same(account, card.Account);
    }

    [Theory]
    [InlineData("1234", true)]
    [InlineData("0123", false)]
    [InlineData("12345", false)]
    [InlineData("0000", false)]
    [InlineData("01234", false)]
    [InlineData("", false)]
    [InlineData("123", false)]
    public void MatchesPinTest(string pin, bool expected)
    {
        // setuo
        setupService();

        // test
        bool actual = card.MatchesPin(pin);
        Assert.Equal(expected, actual);

    }





}