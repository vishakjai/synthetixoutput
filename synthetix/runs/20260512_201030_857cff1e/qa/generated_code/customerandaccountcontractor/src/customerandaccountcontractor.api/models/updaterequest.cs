namespace CustomerAndAccountContractor.Api.Models;

public class UpdateRequest
{
    public int CustomerId { get; set; }
    public string AccountNo { get; set; }
    public string Payload { get; set; }
}