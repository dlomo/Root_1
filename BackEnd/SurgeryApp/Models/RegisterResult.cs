namespace SurgeryApp.Models
{
    public enum RegisterResult
    {
        Success = 1,
        UserExists = 2,
        Failed = 3,
        InvalidInput = -3,
        InvalidInput2 = -4
    }
}