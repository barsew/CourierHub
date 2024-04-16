namespace CourierAPI.Models.OrderControllerModels
{
    public class OrderRequest
    {
        public Guid OfferId { get; set; }
        public string SenderName { get; set; }
        public string SenderSurname { get; set; }
        public string SenderEmail { get; set; }
        public string SenderPhone { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverSurname { get; set; }
        public string ReceiverEmail { get; set; }
        public string ReceiverPhone { get; set; }
    }
}
