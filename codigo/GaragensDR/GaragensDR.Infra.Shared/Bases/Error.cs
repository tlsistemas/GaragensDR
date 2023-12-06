namespace GaragensDR.Infra.Shared.Bases
{
    public class Error
    {
        public String Message { get; set; }
        public Error(String message)
        {
            Message = message;
        }
    }
}
