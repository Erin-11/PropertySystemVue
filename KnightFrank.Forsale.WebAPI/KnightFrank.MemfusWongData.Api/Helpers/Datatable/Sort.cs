﻿using KnightFrank.MemfusWongData.Api.Helpers.Datatable.Core;

namespace KnightFrank.MemfusWongData.Api.Helpers.Datatable
{
    /// <summary>
    /// Represents sort/ordering for columns.
    /// </summary>
    public class Sort : ISort
    {
        /// <summary>
        /// Gets sort direction.
        /// </summary>
        public SortDirection Direction { get; private set; }
        /// <summary>
        /// Gets sort order.
        /// </summary>
        public int Order { get; private set; }



        /// <summary>
        /// Creates a new sort instance.
        /// </summary>
        /// <param name="field">Data field to be bound.</param>
        /// <param name="order">Sort order for multi-sorting.</param>
        /// <param name="direction">Sort direction</param>
        public Sort(int order, string direction)
        {
            Order = order;

            Direction = (direction ?? "").ToLowerInvariant().Equals(Configuration.Options.RequestNameConvention.SortDescending)
                ? SortDirection.Descending // Descending sort should be explicitly set.
                : SortDirection.Ascending; // Default (when set or not) is ascending sort.
        }
    }
}
