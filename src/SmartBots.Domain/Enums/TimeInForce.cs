namespace SmartBots.Domain.Enums
{
    public enum TimeInForce
    {
        /// <summary>
        /// GoodTillCanceled orders will stay active until they are filled or canceled
        /// </summary>
        GTC,

        /// <summary>
        /// ImmediateOrCancel orders have to be at least partially filled upon placing or will be automatically canceled
        /// </summary>
        IOC,

        /// <summary>
        /// FillOrKill orders have to be entirely filled upon placing or will be automatically canceled
        /// </summary>
        FOK,

        /// <summary>
        /// GoodTillCrossing orders will post only
        /// </summary>
        GTX,

        /// <summary>
        /// Good til the order expires or is canceled
        /// </summary>
        GTE_GTC,

        /// <summary>
        /// Good til date
        /// </summary>
        GTD
    }
}
