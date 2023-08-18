using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using QuanLyNT;
using QuanLyNT.Class;

namespace QuanLyNT
{
    public partial class fDMTenThuoc : Form
    {
        DataTable tbTT;
        public fDMTenThuoc()
        {
            InitializeComponent();
        }

        private void fDMTenThuoc_Load(object sender, EventArgs e)
        {
            string sql;
            sql = "SELECT * From tbMucThuoc";
            txtMaThuoc.Enabled = false;
            btnLuu.Enabled = false;
            btnBoQua.Enabled = false;
            LoadDataGridView();
            Functions.FillCombo(sql, cboLoaiThuoc, "MaThuocA", "TenThuocA");
            cboLoaiThuoc.SelectedIndex = -1;
            ResetValues();
        }

        private void ResetValues()
        {
            txtMaThuoc.Text = "";
            txtTenThuoc.Text = "";
            cboLoaiThuoc.Text = "";
            txtSoLuong.Text = "0";
            txtDonGiaNhap.Text = "0";
            txtDonGiaBan.Text = "0";
            txtSoLuong.Enabled = true;
            txtDonGiaNhap.Enabled = false;
            txtDonGiaBan.Enabled = false;
            txtAnh.Text = "";
            picAnh.Image = null;
            txtGhiChu.Text = "";
        }
        private void LoadDataGridView()
        {
            string sql;
            sql = "SELECT * From tbThuoc";
            tbTT = Functions.GetDataToTable(sql);
            dgvTenThuoc.DataSource = tbTT;
            dgvTenThuoc.Columns[0].HeaderText = "Mã thuốc";
            dgvTenThuoc.Columns[1].HeaderText = "Tên thuốc";
            dgvTenThuoc.Columns[2].HeaderText = "Mã loại thuốc";
            dgvTenThuoc.Columns[3].HeaderText = "Số lượng";
            dgvTenThuoc.Columns[4].HeaderText = "Đơn giá nhập";
            dgvTenThuoc.Columns[5].HeaderText = "Đơn giá bán";
            dgvTenThuoc.Columns[6].HeaderText = "Ảnh";
            dgvTenThuoc.Columns[7].HeaderText = "Ghi chú";

            dgvTenThuoc.Columns[0].Width = 80;
            dgvTenThuoc.Columns[1].Width = 140;
            dgvTenThuoc.Columns[2].Width = 100;
            dgvTenThuoc.Columns[3].Width = 80;
            dgvTenThuoc.Columns[4].Width = 100;
            dgvTenThuoc.Columns[5].Width = 100;
            dgvTenThuoc.Columns[6].Width = 80;
            dgvTenThuoc.Columns[7].Width = 300;

            dgvTenThuoc.AllowUserToAddRows = false;
            dgvTenThuoc.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

      

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnBoQua.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            ResetValues();
            txtMaThuoc.Enabled = true;
            txtMaThuoc.Focus();
            txtSoLuong.Enabled = true;
            txtDonGiaNhap.Enabled = true;
            txtDonGiaBan.Enabled = true;
        }

        

        

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;
            if(tbTT.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if(txtMaThuoc.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn mục cần xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xóa mục này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                sql = "DELETE tbThuoc WHERE MaThuoc=N'" + txtMaThuoc.Text + "'";
                Functions.RunSQL(sql);
                LoadDataGridView();
                ResetValues();
            }
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            ResetValues();
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnThem.Enabled = true;
            btnBoQua.Enabled = false;
            btnLuu.Enabled = false;
            txtMaThuoc.Enabled = false;
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlgOpen = new OpenFileDialog();
            dlgOpen.Filter = "Bitmap(*Bitmap(*.bmp)|*.bmp|JPEG(*.jpg)|*.jpg|GIF(*.gif)|*.gif|All files(*.*)|*.*";
            dlgOpen.FilterIndex = 2;
            dlgOpen.Title = "Chọn ảnh thuốc";
            if(dlgOpen.ShowDialog() == DialogResult.OK)
            {
                picAnh.Image = Image.FromFile(dlgOpen.FileName);
                txtAnh.Text = dlgOpen.FileName;
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string sql;
            if ((txtMaThuoc.Text == "") && (txtTenThuoc.Text == "") && (cboLoaiThuoc.Text == ""))
            {
                MessageBox.Show("Bạn hãy nhập thông tin tìm kiếm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            sql = "SELECT * from tbThuoc WHERE 1=1";
            if (txtMaThuoc.Text != "")
                sql += " AND MaThuoc LIKE N'%" + txtMaThuoc.Text + "%'";
            if (txtTenThuoc.Text != "")
                sql += " AND TenThuoc LIKE N'%" + txtTenThuoc.Text + "%'";
            if (cboLoaiThuoc.Text != "")
                sql += " AND MaThuocA LIKE N'%" + cboLoaiThuoc.SelectedValue + "%'";
            tbTT = Functions.GetDataToTable(sql);
            if (tbTT.Rows.Count == 0)
                MessageBox.Show("Không có thuốc nào thỏa mãn thông tin tìm kiếm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else MessageBox.Show("Có " + tbTT.Rows.Count + " loại thuốc thoả mãn thông tin tìm kiếm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            dgvTenThuoc.DataSource = tbTT;
            ResetValues();
        }

        private void dgvThuoc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string MaThuocA;
            string sql;
            if (btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaThuoc.Focus();
                return;
            }
            if (tbTT.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            txtMaThuoc.Text = dgvTenThuoc.CurrentRow.Cells["MaThuoc"].Value.ToString();
            txtTenThuoc.Text = dgvTenThuoc.CurrentRow.Cells["TenThuoc"].Value.ToString();
            MaThuocA = dgvTenThuoc.CurrentRow.Cells["MaThuocA"].Value.ToString();
            sql = "SELECT TenThuocA FROM tbMucThuoc WHERE MaThuocA=N'" + MaThuocA + "'";
            cboLoaiThuoc.Text = Functions.GetFieldValues(sql);
            txtSoLuong.Text = dgvTenThuoc.CurrentRow.Cells["SoLuong"].Value.ToString();
            txtDonGiaNhap.Text = dgvTenThuoc.CurrentRow.Cells["DonGiaNhap"].Value.ToString();
            txtDonGiaBan.Text = dgvTenThuoc.CurrentRow.Cells["DonGiaBan"].Value.ToString();
            sql = "SELECT Anh FROM tbThuoc WHERE MaThuoc=N'" + txtMaThuoc.Text + "'";
            txtAnh.Text = Functions.GetFieldValues(sql);
           
            sql = "SELECT Ghichu FROM tbThuoc WHERE MaThuoc = N'" + txtMaThuoc.Text + "'";
            txtGhiChu.Text = Functions.GetFieldValues(sql);
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnBoQua.Enabled = true;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;
            if (txtMaThuoc.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã thuóc", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaThuoc.Focus();
                return;
            }
            if (txtTenThuoc.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên thuốc", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenThuoc.Focus();
                return;
            }
            if (cboLoaiThuoc.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải chọn loại thuốc", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboLoaiThuoc.Focus();
                return;
            }
            
           
            sql = "SELECT MaThuoc FROM tbThuoc WHERE MaThuoc=N'" + txtMaThuoc.Text.Trim() + "'";
            if (Functions.CheckKey(sql))
            {
                MessageBox.Show("Mã Thuốc này đã tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaThuoc.Focus();
                return;
            }
            sql = "INSERT INTO tbThuoc(MaThuoc,TenThuoc,MaThuocA,SoLuong,DonGiaNhap, DonGiaBan,Anh,Ghichu) VALUES(N'"
                + txtMaThuoc.Text.Trim() + "',N'" + txtTenThuoc.Text.Trim() +
                "',N'" + cboLoaiThuoc.SelectedValue.ToString() +
                "'," + txtSoLuong.Text.Trim() + "," + txtDonGiaNhap.Text +
                "," + txtDonGiaBan.Text + ",'" + txtAnh.Text + "',N'" + txtGhiChu.Text.Trim() + "')";

            Functions.RunSQL(sql);
            LoadDataGridView();
            //ResetValues();
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnBoQua.Enabled = false;
            btnLuu.Enabled = false;
            txtMaThuoc.Enabled = false;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sql;
            if (tbTT.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaThuoc.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn mục cần sửa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaThuoc.Focus();
                return;
            }
            if (txtTenThuoc.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên thuốc", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenThuoc.Focus();
                return;
            }
            if (cboLoaiThuoc.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải chọn loại thuốc", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboLoaiThuoc.Focus();
                return;
            }
            
            sql = "UPDATE tbThuoc SET TenThuoc=N'" + txtTenThuoc.Text.Trim().ToString() +
                "',MaThuocA=N'" + cboLoaiThuoc.SelectedValue.ToString() +
                "',SoLuong=" + txtSoLuong.Text +
                ",Anh='" + txtAnh.Text + "',GhiChu=N'" + txtGhiChu.Text + "' WHERE MaThuoc=N'" + txtMaThuoc.Text + "'";
            Functions.RunSQL(sql);
            LoadDataGridView();
            ResetValues();
            btnBoQua.Enabled = false;
        }

        private void btnHienThi_Click(object sender, EventArgs e)
        {
            string sql;
            sql = "SELECT MaThuoc,TenThuoc,MaThuocA,Soluong,DonGiaNhap,DonGiaBan,Anh,GhiChu FROM tbThuoc";
            tbTT = Functions.GetDataToTable(sql);
            dgvTenThuoc.DataSource = tbTT;
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cboLoaiThuoc_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
