namespace Ambev.DeveloperEvaluation.Domain.Enums
{
    /// <summary>
    /// Enum representing the status of a cart item
    /// </summary>
    public enum CartItemStatus
    {
        /// <summary>
        /// Cart item is pending
        /// </summary>
        Pending,

        /// <summary>
        /// Cart item is in progress
        /// </summary>
        InProgress,

        /// <summary>
        /// Cart item is completed
        /// </summary>
        Completed,

        /// <summary>
        /// Cart item is canceled
        /// </summary>
        Canceled
    }
}
