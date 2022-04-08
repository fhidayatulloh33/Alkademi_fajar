namespace PagiApp.ViewModels;
public class PembayaranViewModel {
    public PembayaranViewModel()
    {
        
    }
    
        public int IdPembayaran { get; set; }
        public string MetodeBayar { get; set; } 
        public int JumlahBayar { get; set; }
        public DateOnly TglBayar { get; set; }
        public int Pajak { get; set; }
        public string Tujuan { get; set; }
        public string FileBuktiPembayaran { get; set; }
        public int Status { get; set; }
}