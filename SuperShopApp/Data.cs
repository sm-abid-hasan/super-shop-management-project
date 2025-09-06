using System;
using System.Data;

namespace SuperShopApp
{
    internal static class Data
    {
        public static DataTable customerdata;
        public static int OrderId;
        public static float OrderTotal;
        public static float PaymentAmount;
        internal static void ClearAll()
        {
            customerdata = null;
            OrderId = 0;
            OrderTotal = 0;
            PaymentAmount = 0;
        }
    }
}
