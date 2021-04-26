using System.ComponentModel;
public enum Status
{
    [Description("Success")]
    Success,
    [Description("Failure")]
    Failure,
    [Description("Still In Danger")]
    In_Danger,
    [Description("Bro! you have crossed line")]
    Crossed_line
}