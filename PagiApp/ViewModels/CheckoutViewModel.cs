namespace PagiApp.ViewModels;
public class CheckoutViewModel
{
    public int[] IdProduct { get; set; }
    public int[] Qty { get; set; }
    public int Alamat { get; set; }
    public string Action { get; set; }
    public string? Note { get; set; }
}