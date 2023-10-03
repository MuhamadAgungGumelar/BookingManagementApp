namespace BookingManagementApp.Utilities.Handlers
{
    //Inheritence ke class exception, supaya bisa menggunakan atribut dan method bawaan message
    public class ExceptionHandler : Exception
    {
        public ExceptionHandler(string message) : base(message) { }
    }
}
