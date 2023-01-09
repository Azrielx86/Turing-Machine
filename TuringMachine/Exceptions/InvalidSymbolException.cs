namespace TuringMachine.Exceptions;

public class InvalidSymbolException : Exception
{
    public override string Message => "Symbol not in Turing Machine language.";
}
