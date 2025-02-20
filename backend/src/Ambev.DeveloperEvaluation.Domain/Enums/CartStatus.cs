using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Enums
{
    /// <summary>
    /// Represents the possible statuses of a shopping cart.
    /// </summary>
    public enum CartStatus
    {
        /// <summary>
        /// The cart is active and can have items added or removed.
        /// </summary>
        Active = 1,

        /// <summary>
        /// The cart has been checked out but not yet processed.
        /// </summary>
        Pending = 2,

        /// <summary>
        /// The cart has been completed and cannot be modified.
        /// </summary>
        Completed = 3,

        /// <summary>
        /// The cart was canceled by the user or the system.
        /// </summary>
        Canceled = 4
    }
}
