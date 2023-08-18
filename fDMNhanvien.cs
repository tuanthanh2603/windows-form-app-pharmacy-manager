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
    public partial class fDMNhanvien : Form
    {
        DataTable tbNV;
        public fDMNhanvien()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void fDMNhanvien_Load(object sender, EventArgs e)
        {
            txtMaDuocSy.Enabled = false;
            btnLuu.Enabled = false;
            btnBoQua.Enabled = false;
            LoadDataGridView();
        }
        public void LoadDataGridView()
        {
            string sql;
            sql = "SELECT MaDuocSy, TenDuocSy, GioiTinh, DiaChi, DienThoai, NgaySinh From tbNhanVien";
            tbNV = Functions.GetDataToTable(sql);
            dgvNhanVien.DataSource = tbNV;
            dgvNhanVien.Columns[0].HeaderText = "Mã dược sỹ";
            dgvNhanVien.Columns[1].HeaderText = "Tên dược sỹ";
            dgvNhanVien.Columns[2].HeaderText = "Giới tính";
            dgvNhanVien.Columns[3].HeaderText = "Địa chỉ";
            dgvNhanVien.Columns[4].HeaderText = "Điện thoại";
            dgvNhanVien.Columns[5].HeaderText = "Ngày sinh";
            dgvNhanVien.Columns[0].Width = 100;
            dgvNhanVien.Columns[1].Width = 150;
            dgvNhanVien.Columns[2].Width = 70;
            dgvNhanVien.Columns[3].Width = 150;
            dgvNhanVien.Columns[4].Width = 100;
            dgvNhanVien.Columns[5].Width = 100;
            dgvNhanVien.AllowUserToAddRows = false;
            dgvNhanVien.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void dgvNhanVien_Click(object sender, EventArgs e)
        {
            if(btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaDuocSy.Focus();
                return;
            }
            if(tbNV.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            txtMaDuocSy.Text = dgvNhanVien.CurrentRow.Cells["MaDuocSy"].Value.ToString();
            txtTenDuocSy.Text = dgvNhanVien.CurrentRow.Cells["TenDuocSy"].Value.ToString();
            if (dgvNhanVien.CurrentRow.Cells["GioiTinh"].Value.ToString() == "Nam") chkGioiTinh.Checked = true;
            else chkGioiTinh.Checked = false; // cần coi lại  
            txtDiaChi.Text = dgvNhanVien.CurrentRow.Cells["DiaChi"].Value.ToString();
            txtDienThoai.Text = dgvNhanVien.CurrentRow.Cells["DienThoai"].Value.ToString();
            dtpNgaySinh.Text = dgvNhanVien.CurrentRow.Cells["NgaySinh"].Value.ToString();
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnBoQua.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = true;
            ResetValues();
            txtMaDuocSy.Enabled = true;
            txtMaDuocSy.Focus();
        }

        private void ResetValues()
        {
            txtMaDuocSy.Text = "";
            txtTenDuocSy.Text = "";
            chkGioiTinh.Text = "";
            txtDiaChi.Text = "";
            dtpNgaySinh.Text = "";
            txtDienThoai.Text = "";
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql, gt;
            if(txtMaDuocSy.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập Mã dược sỹ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaDuocSy.Focus();
                return;
            }
            if (txtTenDuocSy.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập Tên dược sỹ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenDuocSy.Focus();
                return;
            }
            if (txtDiaChi.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập Địa chỉ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiaChi.Focus();
                return;
            }
            if (txtDienThoai.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập Số điện thoại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDienThoai.Focus();
                return;
            }
         
            if (chkGioiTinh.Checked == true)
                gt = "Nam";
            else
                gt = "Nữ";
            sql = "SELECT MaDuocSy FROM tbNhanVien WHERE MaDuocSy=N'" + txtMaDuocSy.Text.Trim() + "'";
            if(Functions.CheckKey(sql))
            {
                MessageBox.Show("Mã dược sỹ này đã có, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaDuocSy.Focus();
                txtMaDuocSy.Text = "";
                return;
            }
            sql = "INSERT INTO tbNhanVien(MaDuocSy, TenDuocSy, GioiTinh, DiaChi, DienThoai, NgaySinh) VALUES (N'" + txtMaDuocSy.Text.Trim() + "',N'" + txtTenDuocSy.Text.Trim() + "',N'" + gt + "',N'" + txtDiaChi.Text.Trim() + "','" + txtDienThoai.Text + "','" + Functions.ConvertDateTime(dtpNgaySinh.Text) + "')";
            Functions.RunSQL(sql);
            LoadDataGridView();
            ResetValues();
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnBoQua.Enabled = false;
            btnLuu.Enabled = false;
            txtMaDuocSy.Enabled = false;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sql, gt;
            if(tbNV.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if(txtMaDuocSy.Text.Trim() == "")
            {
                MessageBox.Show("Bạn chưa chọn mục cần sửa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if(txtTenDuocSy.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên Dược sỹ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenDuocSy.Focus();
                return;
            }
            if(txtDiaChi.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập địa chỉ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiaChi.Focus();
                return;
            }
            if(txtDienThoai.Text == "")
            {
                MessageBox.Show("Bạn phải nhập số điện thoại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDienThoai.Focus();
                return;
            }
            
            if (chkGioiTinh.Checked == true)
                gt = "Nam";
            else
                gt = "Nữ";
            sql = "UPDATE tbNhanVien SET TenDuocSy=N'" + txtTenDuocSy.Text.Trim().ToString() + "',DiaChi=N'" + txtDiaChi.Text.Trim().ToString() + "',DienThoai=N'" + txtDienThoai.Text.ToString() + "',GioiTinh=N'" + gt + "',NgaySinh='" + Functions.ConvertDateTime(dtpNgaySinh.Text) + "' WHERE MaDuocSy=N'" + txtMaDuocSy.Text + "'";
            Functions.RunSQL(sql);
            LoadDataGridView();
            ResetValues();
            btnBoQua.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;
            if(tbNV.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if(txtMaDuocSy.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn mục nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if(MessageBox.Show("Bạn có muốn xóa không", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                sql = "DELETE tbNhanVien WHERE MaDuocSy=N'" + txtMaDuocSy.Text + "'";
                Functions.RunSQL(sql);
                LoadDataGridView();
                ResetValues();
            }
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            ResetValues();
            btnBoQua.Enabled = false;
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnLuu.Enabled = true;
            txtMaDuocSy.Enabled = false;
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
            
        }
    }
}
