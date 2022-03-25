namespace ASPNetCoreMastersTodoList.Api.BindingModels
{
    public class ApiExceptionModel
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string Details { get; set; }

        public ApiExceptionModel(int StatusCode, string Message, string Details)
        {
            this.StatusCode = StatusCode;
            this.Message = Message;
            this.Details = Details;
        }
    }
}
