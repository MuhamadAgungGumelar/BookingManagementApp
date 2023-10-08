using System.Net;

namespace BookingManagementApp.Utilities.Handlers
{
    //Membuat class handler untuk penanganan response apabila berhasil
    public class ResponseOKHandler<TEntity> //Membuat Generic untuk banyak models
    {
        //Membuat atribut response yang ditampilkan ke user
        public int Code { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public object Token { get; set; }
        public TEntity? Data { get; set; }

        //Constructor untuk respons apabila berhasil mengirimkan dan menampilkan data 
        public ResponseOKHandler(TEntity? data)
        {
            Code = StatusCodes.Status200OK;
            Status = HttpStatusCode.OK.ToString();
            Message = "Success to Retrieve Data";
            Data = data;
        }

        //Constructor untuk respons apabila berhasil mengubah dan menghapus data
        public ResponseOKHandler(string message)
        {
            Code = StatusCodes.Status200OK;
            Status = HttpStatusCode.OK.ToString();
            Message = message;
        }

        public ResponseOKHandler(string message, object token)
        {
            Code = StatusCodes.Status200OK;
            Status = HttpStatusCode.OK.ToString();
            Message = message;
            Token = token;
        }
    }
}
