namespace Store.Domain.Enums
{
    public enum EOrderStatus : byte
    {
        WaitingPayment,
        WaitingDelivery,
        Canceled
    }
}