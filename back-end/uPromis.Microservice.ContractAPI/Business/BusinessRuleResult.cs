namespace uPromis.Microservice.ContractAPI.Business
{
    public class BusinessRuleResult
    {
        public string Property { get; set; }
        public string Message { get; set; }
        public BusinessRuleResultSeverity Severity { get; set; }
    }
}
