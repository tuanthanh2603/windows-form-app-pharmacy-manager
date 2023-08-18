using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using QuanLyNT.Class;

namespace QuanLyNT
{
    public partial class fDMThuoc : Form
    {
        DataTable tbMT;
        public fDMThuoc()
        {
            InitializeComponent();
        }

        

        private void fDMThuoc_Load(object sender, EventArgs e)
        {
            txtMaThuocA.Enabled = false;
            btnLuu.Enabled = false;
            btnBoQua.Enabled = false;
            LoadDataGridView();
        }
        private void LoadDataGridView()
        {
            string sql;
            sql = "SELECT MaThuocA, TenThuocA FROM tbMucThuoc";
            tbMT = Class.Functions.GetDataToTable(sql);
            dgvThuoc.DataSource = tbMT;
            dgvThuoc.Columns[0].HeaderText = "Mã loại thuốc";
            dgvThuoc.Columns[1].HeaderText = "Loại thuốc";
            dgvThuoc.Columns[0].Width = 150;
            dgvThuoc.Columns[1].Width = 400;
            dgvThuoc.AllowUserToAddRows = false;

            dgvThuoc.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void dgvThuoc_Click(object sender, EventArgs e)
        {
            if(btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaThuocA.Focus();
                return;
            }
            if (tbMT.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            txtMaThuocA.Text = dgvThuoc.CurrentRow.Cells["MaThuocA"].Value.ToString();
            txtTenThuocA.Text = dgvThuoc.CurrentRow.Cells["TenThuocA"].Value.ToString();
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnBoQua.Enabled = true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnBoQua.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            ResetValue();
            txtMaThuocA.Enabled = true;
            txtMaThuocA.Focus();
            
        }

        private void ResetValue()
        {
            txtMaThuocA.Text = "";
            txtTenThuocA.Text = "";
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;
            if(txtMaThuocA.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập Mã loại thuốc", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaThuocA.Focus();
                return;
            }
            if(txtTenThuocA.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập Loại thuốc", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenThuocA.Focus();
                return;
            }
            sql = "Select MaThuocA From tbMucThuoc where MaThuocA=N'" + txtMaThuocA.Text.Trim() + "'";
            if(Class.Functions.CheckKey(sql))
            {
                MessageBox.Show("Mã loại thuốc này đã có, hãy nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaThuocA.Focus();
                return;
            }
            sql = "INSERT INTO tbMucThuoc VALUES(N'" + txtMaThuocA.Text + "',N'" + txtTenThuocA.Text + "')";
            Class.Functions.RunSQL(sql);
            LoadDataGridView();
            ResetValue();
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnBoQua.Enabled = false;
            btnLuu.Enabled = false;
            txtMaThuocA.Enabled = false;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sql;
            if(tbMT.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if(txtMaThuocA.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn mục cần sửa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if(txtTenThuocA.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn chưa nhập Loại thuốc", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            sql = "UPDATE tbMucThuoc SET TenThuocA=N'" + txtTenThuocA.Text.ToString() + "' WHERE MaThuocA=N'" + txtMaThuocA.Text + "'";
            Class.Functions.RunSQL(sql);
            LoadDataGridView();
            ResetValue();

            btnBoQua.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;
            if(tbMT.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if(txtMaThuocA.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn mục cần xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if(MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                sql = "DELETE tbMucThuoc WHERE MaThuocA=N'" + txtMaThuocA.Text + "'";
                Class.Functions.RunSQL(sql);
                LoadDataGridView();
                ResetValue();
            }
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            ResetValue();
            btnBoQua.Enabled = false;
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnLuu.Enabled = false;
            txtMaThuocA.Enabled = false;
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
