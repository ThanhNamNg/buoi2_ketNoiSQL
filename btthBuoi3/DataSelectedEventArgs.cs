    using System;

    namespace btthBuoi3
    {
        public class DataSelectedEventArgs : EventArgs
        {
            public string Ngay { get; set; }
            public string Ma { get; set; }
            public string DichVu { get; set; }

            public DataSelectedEventArgs(string ngay, string ma, string dichVu)
            {
                Ngay = ngay;
                Ma = ma;
                DichVu = dichVu;
            }
        }

    }