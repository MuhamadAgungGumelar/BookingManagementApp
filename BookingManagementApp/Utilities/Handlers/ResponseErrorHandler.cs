namespace BookingManagementApp.Utilities.Handlers
{
    //Membuat class handler untuk penanganan error
    public class ResponseErrorHandler
    {
        public int Code { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public string? Error { get; set; }
    }
}
