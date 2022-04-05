namespace PagiApp.ViewModels;
public class OrderViewModel {
    public OrderViewModel()
    {
        Details = new List<OrderDetailViewModel>();
    }
    public int IdOrder { get; set; }
    public DateTime TglOrder { get; set; }
    public int TotalQty { get {
        return (Details == null || !Details.Any()) ? 0 : Details.Sum(x=>x.Qty);
    }}
    public decimal TotalBayar { get; set; }
    public string Status { get; set; }

    public List<OrderDetailViewModel> Details { get; set; }
}