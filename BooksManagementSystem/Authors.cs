using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BooksManagementSystem
{
    public partial class frmAuthors : Form
    {
        public frmAuthors()
        {
            InitializeComponent();
        }

        private void frmAuthors_Load(object sender, EventArgs e)
        {
            try
            {
                bookscontext context = new bookscontext();
                context.Authors.ToList();
                this.authorBindingSource.DataSource = context.Authors.Local.ToBindingList();
                SetAppState("View");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Database Binding Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            authorBindingSource.MovePrevious();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            authorBindingSource.MoveNext();
        }
        private void btnFirst_Click(object sender, EventArgs e)
        {
            authorBindingSource.MoveFirst();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            authorBindingSource.MoveLast();
        }
        private void btnAddNew_Click(object sender, EventArgs e)
        {
            
            SetAppState("Add");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {                
                using (bookscontext context = new bookscontext())
                {
                    Author author = new Author();
                    Boolean isNew = false;
                    Boolean result = true;

                        if (txtAuthorID.Text == "")
                        {
                            var authors = context.Authors.ToList();
                            var lid = authors.Last().Au_ID += 1;
                            author.Au_ID = lid;
                            author.Author1 = txtName.Text;
                            author.Year_Born = Convert.ToInt16(txtBirthYear.Text);
                            isNew = true;
                            this.txtName.DataBindings.Add(new Binding("Text", this.authorBindingSource, "Author1", true));
                            this.txtBirthYear.DataBindings.Add(new Binding("Text", this.authorBindingSource, "Year_Born", true));
                            this.txtAuthorID.DataBindings.Add(new Binding("Text", this.authorBindingSource, "Au_ID", true));
                        }
                        else
                        {
                            author.Author1 = txtName.Text;
                            author.Year_Born = Convert.ToInt16(txtBirthYear.Text);
                            author.Au_ID = Convert.ToInt16(txtAuthorID.Text);
                        }

                    result = SaveAuthorDetails(author, isNew);

                    SetAppState("View");
                    MessageBox.Show("Record Saved", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Data Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public bool SaveAuthorDetails(Author author, Boolean isNew)
        {
            bool result = false;
            using (bookscontext context = new bookscontext())
            {
                if (isNew)
                {
                    context.Authors.Add(author);
                    context.SaveChanges();
                }
                else
                {
                    var record = context.Authors.Find(author.Au_ID);
                    record.Author1 = txtName.Text;
                    record.Year_Born = Convert.ToInt16(txtBirthYear.Text);
                    context.SaveChanges();
                }

                result = true;
                SetAppState("View");
                MessageBox.Show("Record Saved", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return result;

            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            SetAppState("Update");            
        }
        private void btnDone_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult response;
            response = MessageBox.Show("Are you sure you want to delete the record?", "Delete", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (response == DialogResult.No)
            {
                return;
            }
            try
            {
                var record = new Author { Au_ID = Convert.ToInt16(txtAuthorID.Text) };
                using (bookscontext context = new bookscontext())
                { 
                    context.Authors.Attach(record);
                    context.Authors.Remove(record);
                    context.SaveChanges();
                    authorBindingSource.Remove(authorBindingSource.Current);
                    SetAppState("View");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error deleting record", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SetAppState(string appState)
        {
            switch (appState)
            {
                case "View":
                    txtName.TabStop = false;
                    txtBirthYear.TabStop = false;
                    txtAuthorID.BackColor = Color.Red;
                    txtAuthorID.ForeColor = Color.Gray;
                    txtName.ReadOnly = true;
                    txtBirthYear.ReadOnly = true;
                    btnPrevious.Enabled = true;
                    btnUpdate.Enabled = true;
                    btnNext.Enabled = true;
                    btnSave.Enabled = false;
                    btnCancel.Enabled = false;
                    btnAddNew.Enabled = true;
                    btnDelete.Enabled = true;
                    btnDone.Enabled = true;
                    break;
                case "Add":
                    txtAuthorID.BackColor = Color.Red;
                    txtAuthorID.ForeColor = Color.White;
                    txtAuthorID.ReadOnly = true;
                    txtName.ReadOnly = false;
                    txtBirthYear.ReadOnly = false;
                    btnPrevious.Enabled = false;
                    btnNext.Enabled = false;
                    btnSave.Enabled = true;
                    btnCancel.Enabled = true;
                    btnAddNew.Enabled = false;
                    btnDelete.Enabled = false;
                    btnUpdate.Enabled = false;
                    btnDone.Enabled = false;
                    txtName.Focus();
                    txtAuthorID.Clear();
                    txtName.Clear();
                    txtBirthYear.Clear();
                    txtAuthorID.DataBindings.Clear();
                    txtBirthYear.DataBindings.Clear();
                    txtName.DataBindings.Clear();
                    break;
                case "Update":
                    txtAuthorID.BackColor = Color.Red;
                    txtAuthorID.ForeColor = Color.White;
                    txtAuthorID.ReadOnly = true;
                    txtName.ReadOnly = false;
                    txtBirthYear.ReadOnly = false;
                    btnPrevious.Enabled = false;
                    btnNext.Enabled = false;
                    btnSave.Enabled = true;
                    btnCancel.Enabled = true;
                    btnAddNew.Enabled = false;
                    btnDelete.Enabled = false;
                    btnDone.Enabled = true;
                    txtName.Focus();
                    break;
                default:
                    txtAuthorID.BackColor = Color.Red;
                    txtAuthorID.ForeColor = Color.White;
                    txtName.ReadOnly = false;
                    txtBirthYear.ReadOnly = false;
                    btnPrevious.Enabled = false;
                    btnNext.Enabled = false;
                    btnSave.Enabled = true;
                    btnCancel.Enabled = true;
                    btnAddNew.Enabled = false;
                    btnDelete.Enabled = false;
                    btnDone.Enabled = false;
                    txtName.Focus();
                    break;
            }
        }        
        public bool ValidateInput()
        {
            string message = "";
            int inputYear, currentYear;
            bool allOK = true;

            if (txtName.Text.Trim().Equals(""))
            {
                message = "Author's name is required!" + "\r\n";
                txtName.Focus();
                allOK = false;
            }

            if (!txtBirthYear.Text.Trim().Equals(""))
            {
                inputYear = Convert.ToInt32(txtBirthYear.Text);
                currentYear = DateTime.Now.Year;

                if (inputYear >= currentYear)
                {
                    message += "Invalid Year!";
                    txtBirthYear.Focus();
                    allOK = false;
                }
            }

            if (!allOK)
            {
                MessageBox.Show(message, "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return allOK;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            using (bookscontext context = new bookscontext())
            {
                var authorList = context.Authors.ToList();
                var firstAuthorMatchIndex = authorList.FindIndex(a => a.Author1.Contains(txtSearch.Text));
                // MessageBox.Show(firstAuthorMatchIndex.ToString() + authorList[firstAuthorMatchIndex].Author1);
                if (firstAuthorMatchIndex >= 0)
                    authorBindingSource.Position = firstAuthorMatchIndex;
            }
        }
    }
}
