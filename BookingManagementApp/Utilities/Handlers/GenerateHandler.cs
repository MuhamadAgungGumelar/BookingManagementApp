using BookingManagementApp.Data;

public class GenerateHandler
{
    //Injeksi untuk membuat variable context dari BookingManagementDbContext
    private readonly BookingManagementDbContext _context;

    //Membuat Constructor
    public GenerateHandler(BookingManagementDbContext context)
    {
        _context = context;
    }

    //Membuat Method NIK Untuk Generate NIK secara otomatis
    public static string NIK(string? nik = null)
    {
        //Pengondisian, apabila nik nya kosong, maka nik default yaitu 111111
        if(nik is null)
        {
            return "111111";
        }

        //Apabila nik sudah ada, maka nik akan ditambah 1
        var generateNik = int.Parse(nik) + 1;
        //Mengembalikan nik yang sudah ditambah 1 dan diubah ke bentuk string
        return generateNik.ToString();
    }
}
