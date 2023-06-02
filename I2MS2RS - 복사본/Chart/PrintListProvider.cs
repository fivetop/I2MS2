using I2MS2.Windows;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace I2MS2
{
    /// <summary>
    /// Demo implementation of IItemsProvider returning dummy customer items after
    /// a pause to simulate network/disk latency.
    /// </summary>
    public class PrintListProvider : IItemsProvider<EventPrintList>
    {
        private readonly int _count;
        private readonly int _fetchDelay;
        private List<EventPrintList> list1;

        /// <summary>
        /// Initializes a new instance of the <see cref="AssetPrintListProvider"/> class.
        /// </summary>
        /// <param name="count">The count.</param>
        /// <param name="fetchDelay">The fetch delay.</param>
        public PrintListProvider(int count, int fetchDelay)
        {
            _count = count;
            _fetchDelay = fetchDelay;
        }

        public PrintListProvider(int p1, int p2, List<EventPrintList> list1)
        {
            // TODO: Complete member initialization
            this._count = p1;
            this._fetchDelay = p2;
            this.list1 = list1;
        }

        /// <summary>
        /// Fetches the total number of items available.
        /// </summary>
        /// <returns></returns>
        public int FetchCount()
        {
            Trace.WriteLine("FetchCount");
            Thread.Sleep(_fetchDelay);
            return _count;
        }

        /// <summary>
        /// Fetches a range of items.
        /// </summary>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The number of items to fetch.</param>
        /// <returns></returns>
        public IList<EventPrintList> FetchRange(int startIndex, int count)
        {
            Trace.WriteLine("FetchRange: " + startIndex + "," + count);
            Thread.Sleep(_fetchDelay);

            List<EventPrintList> list = new List<EventPrintList>();

            try
            {
                int t1 = startIndex + count;

                if (t1 > _count)
                {
                    count = _count - startIndex;
                    list = list1.GetRange(startIndex, count);

                    for (int i = startIndex; i < startIndex + (100 - count); i++)
                    {
                        EventPrintList customer = new EventPrintList { RowNumber = i };
                        list.Add(customer);
                        // Trace.WriteLine("loop: " + i);
                    }
                }
                else
                {
                    list = list1.GetRange(startIndex, count);
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine("loop: " + (startIndex + 100));
            }
            return list;
        }
    }
}
