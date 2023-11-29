namespace Frontend.Validators
{
    public class ValidationResults
    {
        public bool Success { get; }
        public string Message { get; }

        public ValidationResults() 
        {
            Message = "";
            Success = true;
        }
        public ValidationResults(string message) 
        { 
            Message = message; 
            Success = false;
        }
    }
}
