namespace ViewModel.Dtos
{
    public class ErrorDto
    {
        private string fieldName = "FieldName";
        private string message = "Message";

        public string FieldName { get => fieldName; set => fieldName = value; }
        public string Message { get => message; set => message = value; }
    }
}
