namespace TuringMachine.Exceptions;

public class TimeExceededException : Exception
{
    public override string Message => "Time exceded, Turing Machine could not solve.";
}