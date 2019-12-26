using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace BooksManagementSystem
{
    public partial class frmPublishers : Form
    {
        public frmPublishers()
        {
            InitializeComponent();
        }

        private void frmPublishers_Load(object sender, EventArgs e)
        {
            try
            {
                bookscontext context = new bookscontext();
                context.Publishers.ToList();
                this.publisherBindingSource.DataSource = context.Publishers.Local.ToBindingList();
                SetAppState("View");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Data binding error"+ex, "Binding Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }        
        private void btnPrevious_Click(object sender, EventArgs e)
        {
            publisherBindingSource.MovePrevious();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            publisherBindingSource.MoveNext();
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            publisherBindingSource.MoveFirst();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            publisherBindingSource.MoveLast();
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            SetAppState("Add");         
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (bookscontext context = new bookscontext())
            {
                Publisher publisher = new Publisher();
                Boolean isNew = false;
                Boolean result = true;

                if (txtPubID.Text == "")
                {
                    var publishers = context.Publishers.ToList();
                    var lid = publishers.Last().PubID;

                    publisher.PubID = ++lid;
                    publisher.Address = txtAddress.Text;
                    publisher.City = txtAddress.Text;
                    publisher.Comments = txtComments.Text;
                    publisher.Company_Name = txtCompanyName.Text;
                    publisher.Name = txtName.Text;
                    publisher.Fax = txtFax.Text;
                    publisher.State = txtState.Text;
                    publisher.Telephone = txtPhone.Text;
                    publisher.Zip = txtPhone.Text;
                    this.txtComments.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.publisherBindingSource, "Comments", true));
                    this.txtCompanyName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.publisherBindingSource, "Company_Name", true));
                    this.txtFax.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.publisherBindingSource, "Fax", true));
                    this.txtPhone.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.publisherBindingSource, "Telephone", true));
                    this.txtZip.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.publisherBindingSource, "Zip", true));
                    this.txtState.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.publisherBindingSource, "State", true));
                    this.txtCity.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.publisherBindingSource, "City", true));
                    this.txtAddress.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.publisherBindingSource, "Address", true));
                    this.txtName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.publisherBindingSource, "Name", true));
                    this.txtPubID.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.publisherBindingSource, "PubID", true));
                    isNew = true;
                }
                else
                {
                    publisher.PubID = Convert.ToInt16(txtPubID.Text);
                    publisher.Address = txtAddress.Text;
                    publisher.City = txtAddress.Text;
                    publisher.Comments = txtComments.Text;
                    publisher.Company_Name = txtCompanyName.Text;
                    publisher.Name = txtName.Text;
                    publisher.Fax = txtFax.Text;
                    publisher.State = txtState.Text;
                    publisher.Telephone = txtPhone.Text;
                    publisher.Zip = txtPhone.Text;
                }
                result = SavePublisherDetails(publisher, isNew);
            }
        }
        public bool SavePublisherDetails(Publisher publisher, Boolean isNew)
        {
            Boolean result = false;

            using (bookscontext context = new bookscontext())
            {
                if (isNew)
                {
                    context.Publishers.Add(publisher);
                    context.SaveChanges();
                }
                else
                {
                    var record = context.Publishers.Find(publisher.PubID);
                    record.PubID = Convert.ToInt16(txtPubID.Text);
                    record.Address = txtAddress.Text;
                    record.City = txtCity.Text;
                    record.Comments = txtComments.Text;
                    record.Company_Name = txtCompanyName.Text;
                    record.Name = txtName.Text;
                    record.Fax = txtFax.Text;
                    record.State = txtState.Text;
                    record.Telephone = txtPhone.Text;
                    record.Zip = txtZip.Text;
                    context.SaveChanges();
                }
            }
            SetAppState("View");
            return result;
        }
        private void btnDone_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult response;
            response = MessageBox.Show("Are you sure you want to delete this publisher?", "Delete", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (response == DialogResult.No)
            {
                return;
            }

            try
            {
                Publisher record = new Publisher { PubID = Convert.ToInt16(txtPubID.Text) };

                using (bookscontext context = new bookscontext())
                {
                    context.Publishers.Attach(record);
                    context.Publishers.Remove(record);
                    context.SaveChanges();
                    publisherBindingSource.Remove(publisherBindingSource.Current);
                    SetAppState("View");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("The delete function is broken!"+ex, "Delete Broken", MessageBoxButtons.OK, MessageBoxIcon.Error);                
            }
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            SetAppState("");
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text.Equals("") || txtSearch.Text.Length < 3)
            {
                MessageBox.Show("Invalid Search", "Invalid Search", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            using (bookscontext context = new bookscontext())
            {
                var publist = context.Publishers.ToList();
                var searchText = txtSearch.Text;

                var firstPubMatchIndex = publist.FindIndex(p => p.Name.Contains(searchText));

                MessageBox.Show(firstPubMatchIndex.ToString(), "Found", MessageBoxButtons.OK);
                if (firstPubMatchIndex >= 0)
                    publisherBindingSource.Position = firstPubMatchIndex;
            }
        }
        private void SetAppState(string state)
        {
            switch (state)
            {
                case "View":
                    txtPubID.BackColor = Color.Red;
                    txtPubID.ForeColor = Color.Black;                   
                    btnAddNew.Enabled = true;
                    btnDelete.Enabled = true;
                    btnDone.Enabled = true;
                    btnFirst.Enabled = true;
                    btnLast.Enabled = true;
                    btnNext.Enabled = true;
                    btnPrevious.Enabled = true;
                    btnSave.Enabled = true;
                    btnUpdate.Enabled = true;
                    txtAddress.ReadOnly = true;
                    txtCity.ReadOnly = true;
                    txtComments.ReadOnly = true;
                    txtCompanyName.ReadOnly = true;
                    txtFax.ReadOnly = true;
                    txtName.ReadOnly = true;
                    txtPhone.ReadOnly = true;
                    txtPubID.ReadOnly = true;
                    txtState.ReadOnly = true;
                    txtZip.ReadOnly = true;
                    break;
                case "Add":
                    txtPubID.BackColor = Color.Red;
                    txtPubID.ForeColor = Color.Black;
                    txtPubID.ReadOnly = true;
                    txtName.Focus();
                    btnAddNew.Enabled = false;
                    btnDelete.Enabled = false;
                    btnFirst.Enabled = false;
                    btnAddNew.Enabled = true;
                    btnDelete.Enabled = true;
                    btnDone.Enabled = true;
                    btnFirst.Enabled = true;
                    btnLast.Enabled = true;
                    btnNext.Enabled = true;
                    btnPrevious.Enabled = true;
                    btnSave.Enabled = true;
                    btnUpdate.Enabled = true;
                    txtAddress.ReadOnly = false;
                    txtCity.ReadOnly = false;
                    txtComments.ReadOnly = false;
                    txtCompanyName.ReadOnly = false;
                    txtFax.ReadOnly = false;
                    txtName.ReadOnly = false;
                    txtPhone.ReadOnly = false;
                    txtPubID.ReadOnly = false;
                    txtState.ReadOnly = false;
                    txtZip.ReadOnly = false;
                    txtAddress.Clear();
                    txtCity.Clear();
                    txtComments.Clear();
                    txtCompanyName.Clear();
                    txtFax.Clear();
                    txtName.Clear();
                    txtPhone.Clear();
                    txtPubID.Clear();
                    txtState.Clear();
                    txtZip.Clear();
                    txtAddress.DataBindings.Clear();
                    txtCity.DataBindings.Clear();
                    txtComments.DataBindings.Clear();
                    txtCompanyName.DataBindings.Clear();
                    txtFax.DataBindings.Clear();
                    txtName.DataBindings.Clear();
                    txtPhone.DataBindings.Clear();
                    txtPubID.DataBindings.Clear();
                    txtState.DataBindings.Clear();
                    txtZip.DataBindings.Clear();
                    break;
                default:
                    txtPubID.BackColor = Color.Red;
                    txtPubID.ForeColor = Color.Black;
                    txtPubID.ReadOnly = true;
                    txtAddress.ReadOnly = false;
                    txtCity.ReadOnly = false;
                    txtComments.ReadOnly = false;
                    txtCompanyName.ReadOnly = false;
                    txtFax.ReadOnly = false;
                    txtName.ReadOnly = false;
                    txtPhone.ReadOnly = false;
                    txtPubID.ReadOnly = false;
                    txtState.ReadOnly = false;
                    txtZip.ReadOnly = false;
                    btnAddNew.Enabled = false;
                    btnDelete.Enabled = false;
                    btnFirst.Enabled = false;
                    btnAddNew.Enabled = false;
                    btnDelete.Enabled = false;
                    btnDone.Enabled = true;
                    btnFirst.Enabled = false;
                    btnLast.Enabled = false;
                    btnNext.Enabled = false;
                    btnPrevious.Enabled = false;
                    btnSave.Enabled = true;
                    btnUpdate.Enabled = false;
                    break;
            }
        }
    }
}
