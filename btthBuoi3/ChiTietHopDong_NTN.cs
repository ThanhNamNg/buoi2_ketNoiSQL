using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace btthBuoi3
{
    public partial class ChiTietHopDong_NTN : Form
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["QLBN"].ConnectionString;
        private string duLieuNhanTuA;
        private string ngay;
        private string ma;
        private string dichVu;
        

           // Tạo một sự kiện để chuyển dữ liệu đã chọn về Form1
    public event EventHandler<DataSelectedEventArgs> DataSelected;

        public ChiTietHopDong_NTN()
        {
            InitializeComponent();
        }

        public ChiTietHopDong_NTN(string maBanhNhanTuA)
        {
            InitializeComponent();
            duLieuNhanTuA = maBanhNhanTuA;
            LoadData();

            
        }

        private void LoadData()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM tblHopDong WHERE MaBN = @MaBN ORDER BY CONVERT(date, Ngay, 103) ASC";
                    // Sắp xếp tăng dần theo trường Ngay sau khi chuyển đổi sang định dạng có thể sắp xếp được (dd/MM/yyyy)
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@MaBN", duLieuNhanTuA);
                    // Sử dụng SqlDataAdapter để thực thi câu truy vấn và lấy dữ liệu từ cơ sở dữ liệu
                    SqlDataAdapter adapter = new SqlDataAdapter(command);

                    DataTable dataTable = new DataTable();
                    // Đổ dữ liệu từ SqlDataAdapter vào DataTable
                    adapter.Fill(dataTable);
                    if (dataTable.Rows.Count > 0)
                    {
                        dataGridView1.DataSource = dataTable;
                    }
                    else
                    {
                        label.Text = "Không có lịch sử khám.";
                        dataGridView1.Visible = false;

                    }


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ngay = dataGridView1.CurrentRow.Cells["Ngay"].Value.ToString();
            ma = dataGridView1.CurrentRow.Cells["maBN"].Value.ToString();
            dichVu = dataGridView1.CurrentRow.Cells["dichVu"].Value.ToString();

            // Gửi dữ liệu về Form1 và đóng form ChiTietHopDong_NTN
            SendDataToForm1();
            this.Close();

        }

        private void SendDataToForm1()
        {
            // Kiểm tra xem sự kiện đã được đăng ký và có dữ liệu cần gửi đi hay không
            if (DataSelected != null && !string.IsNullOrEmpty(ngay) && !string.IsNullOrEmpty(ma) && !string.IsNullOrEmpty(dichVu))
            {
                // Gửi dữ liệu về Form1 thông qua sự kiện DataSelected
                DataSelected(this, new DataSelectedEventArgs(ngay, ma, dichVu));
            }
        }


       
    }
}
