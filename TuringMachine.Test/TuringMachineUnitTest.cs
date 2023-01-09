namespace TuringMachine.Test;

public class TuringMachineUnitTest
{
    private TuringMachine _tm;

    public TuringMachineUnitTest()
    {
        _tm = new TuringMachine();
    }

    [Fact]
    public void Test1()
    {
        // 13 + 5 = 18
        var result = _tm.Solve($"{Convert.ToString(13, 2)}_{Convert.ToString(5, 2)}");
        Console.WriteLine(result);
        Assert.True(18 == Convert.ToInt16(result, 2));
    }

    [Fact]
    public void Test2()
    {
        // 20 + 42 = 62
        var result = _tm.Solve($"{Convert.ToString(20, 2)}_{Convert.ToString(42, 2)}");
        Console.WriteLine(result);
        Assert.True(62 == Convert.ToInt16(result, 2));
    }

    [Fact]
    public void Test3()
    {
        // 9 + 40 = 49
        var result = _tm.Solve($"{Convert.ToString(9, 2)}_{Convert.ToString(40, 2)}");
        Console.WriteLine(result);
        Assert.True(49 == Convert.ToInt16(result, 2));
    }
}