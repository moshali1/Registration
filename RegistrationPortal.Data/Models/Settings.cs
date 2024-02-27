namespace RegistrationPortal.Data.Models;
public class Settings
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public bool CreateFunction { get; set; }
    public bool UpdateFunction { get; set; }
    public bool WithdrawFunctionForMB { get; set; }
    public bool WithdrawFunctionForT { get; set; }
}
