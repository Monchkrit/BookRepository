using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BooksManagementSystem
{
    public partial class frmTitles : Form
    {

        ComboBox[] authorCombo = new ComboBox[4];
        private Title _title = new Title();
        public frmTitles()
        {
            InitializeComponent();
        }
                
        private void frmTitles_Load(object sender, EventArgs e)
        {

            authorCombo[0] = cboAuthor1;
            authorCombo[1] = cboAuthor2;
            authorCombo[2] = cboAuthor3;
            authorCombo[3] = cboAuthor4;
            
            using (bookscontext context = new bookscontext())
            {
                try
                {
                    context.Titles.ToList();
                    context.Authors.ToList();
                    context.Publishers.ToList();

                    this.titleBindingSource.DataSource = context.Titles.Local.ToBindingList();
                    this.authorBindingSource.DataSource = context.Authors.Local.ToBindingList();
                    this.publisherBindingSource.DataSource = context.Publishers.Local.ToBindingList();

                    var publisherList = context.Publishers.ToList();                    

                    var firstMatchTitle = publisherList.FindIndex(p => p.PubID == Convert.ToInt32(txtPubID.Text));
                    if (firstMatchTitle >= 0)
                        publisherBindingSource.Position = firstMatchTitle;

                    var title = context.Titles.Find(txtISBN.Text);

                    var titleAuthors = title.Authors;

                    List<string> authorNames = new List<string>();
                    foreach (var t in titleAuthors)
                    {
                        authorNames.Add(t.Author1);
                    }                    

                    for (int i = 0; i < authorNames.Count; ++i)
                    {
                        authorCombo[i].Items.Add(authorNames[i]);
                        authorCombo[i].SelectedIndex = 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex + " " + "There is a problem with data binding",
                        "Problem with Load", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            SetAppState("View");            
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            titleBindingSource.MoveFirst();

            authorCombo[0] = cboAuthor1;
            authorCombo[1] = cboAuthor2;
            authorCombo[2] = cboAuthor3;
            authorCombo[3] = cboAuthor4;

            using (bookscontext context = new bookscontext())
            {                
                var publisherList = context.Publishers.ToList();
                var authorList = context.Titles.ToList();
                

                var firstMatchPublisher = publisherList.FindIndex(p => p.PubID == Convert.ToInt32(txtPubID.Text));
                if (firstMatchPublisher >= 0)
                    publisherBindingSource.Position = firstMatchPublisher;

                var title = context.Titles.Find(txtISBN.Text);

                var titleAuthors = title.Authors;

                List<string> authorNames = new List<string>();
                foreach (var t in titleAuthors)
                {
                    authorNames.Add(t.Author1);
                }

                for (int c = 0; c < 4; c++)
                {
                    authorCombo[c].Items.Clear();
                }
                for (int i = 0; i < authorNames.Count; ++i)
                {
                    authorCombo[i].Items.Add(authorNames[i]);
                    authorCombo[i].SelectedIndex = 0;
                }
            }
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            titleBindingSource.MovePrevious();

            authorCombo[0] = cboAuthor1;
            authorCombo[1] = cboAuthor2;
            authorCombo[2] = cboAuthor3;
            authorCombo[3] = cboAuthor4;

            for (int c = 0; c < 4; c++)
            {
                authorCombo[c].Items.Clear();
                authorCombo[c].Text = "";
            }

            using (bookscontext context = new bookscontext())
            {
                var publisherList = context.Publishers.ToList();

                var firstMatchTitle = publisherList.FindIndex(p => p.PubID == Convert.ToInt32(txtPubID.Text));
                if (firstMatchTitle >= 0)
                    publisherBindingSource.Position = firstMatchTitle;

                var title = context.Titles.Find(txtISBN.Text);

                var titleAuthors = title.Authors;

                List<string> authorNames = new List<string>();

                foreach (var t in titleAuthors)
                {
                    authorNames.Add(t.Author1);
                }

                
                for (int i = 0; i < authorNames.Count; ++i)
                {                    
                    authorCombo[i].Items.Add(authorNames[i]);
                    authorCombo[i].SelectedIndex = 0;
                }
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            titleBindingSource.MoveNext();
            authorCombo[0] = cboAuthor1;
            authorCombo[1] = cboAuthor2;
            authorCombo[2] = cboAuthor3;
            authorCombo[3] = cboAuthor4;

            for (int c = 0; c < 4; c++)
            {
                authorCombo[c].Items.Clear();
                authorCombo[c].Text = "";
            }

            using (bookscontext context = new bookscontext())
            {
                var publisherList = context.Publishers.ToList();

                var firstMatchTitle = publisherList.FindIndex(p => p.PubID == Convert.ToInt32(txtPubID.Text));
                if (firstMatchTitle >= 0)
                    publisherBindingSource.Position = firstMatchTitle;

                var title = context.Titles.Find(txtISBN.Text);

                var titleAuthors = title.Authors;

                List<string> authorNames = new List<string>();
                foreach (var t in titleAuthors)
                {
                    authorNames.Add(t.Author1);
                }

                for (int i = 0; i < authorNames.Count; ++i)
                {
                    authorCombo[i].Items.Add(authorNames[i]);
                    authorCombo[i].SelectedIndex = 0;
                }
            }
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            titleBindingSource.MoveLast();

            authorCombo[0] = cboAuthor1;
            authorCombo[1] = cboAuthor2;
            authorCombo[2] = cboAuthor3;
            authorCombo[3] = cboAuthor4;

            for (int c = 0; c < 4; c++)
            {
                authorCombo[c].Items.Clear();
                authorCombo[c].Text = "";
            }

            using (bookscontext context = new bookscontext())
            {
                var publisherList = context.Publishers.ToList();

                var firstMatchTitle = publisherList.FindIndex(p => p.PubID == Convert.ToInt32(txtPubID.Text));
                if (firstMatchTitle >= 0)
                    publisherBindingSource.Position = firstMatchTitle;

                var title = context.Titles.Find(txtISBN.Text);

                var titleAuthors = title.Authors;

                List<string> authorNames = new List<string>();
                foreach (var t in titleAuthors)
                {
                    authorNames.Add(t.Author1);
                }

                for (int i = 0; i < authorNames.Count; ++i)
                {                    
                    authorCombo[i].Items.Add(authorNames[i]);
                    authorCombo[i].SelectedIndex = 0;
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            SetAppState("");
            txtYearPublished.Focus();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            SetAppState("Add");

            authorCombo[0] = cboAuthor1;
            authorCombo[1] = cboAuthor2;
            authorCombo[2] = cboAuthor3;
            authorCombo[3] = cboAuthor4;

            for (int c = 0; c < 4; c++)
            {
                authorCombo[c].Items.Clear();
                authorCombo[c].Text = "";
            }

            using (bookscontext context = new bookscontext())
            {
                var authors1 = context.Authors.ToList();
                var authors2 = context.Authors.ToList();
                var authors3 = context.Authors.ToList();
                var authors4 = context.Authors.ToList();
                var publishers = context.Publishers.ToList();

                cboPublishers.DataSource = publishers;
                cboPublishers.DisplayMember = "Company_Name";
                cboPublishers.ValueMember = "PubID";
                authorCombo[0].DataSource = authors1;
                authorCombo[0].DisplayMember = "Author1";
                authorCombo[0].ValueMember = "Au_ID";
                authorCombo[1].DataSource = authors2;
                authorCombo[1].DisplayMember = "Author1";
                authorCombo[1].ValueMember = "Au_ID";
                authorCombo[2].DataSource = authors3;
                authorCombo[2].DisplayMember = "Author1";
                authorCombo[2].ValueMember = "Au_ID";
                authorCombo[3].DataSource = authors4;
                authorCombo[3].DisplayMember = "Author1";
                authorCombo[3].ValueMember = "Au_ID";
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            authorCombo[0] = cboAuthor1;
            authorCombo[1] = cboAuthor2;
            authorCombo[2] = cboAuthor3;
            authorCombo[3] = cboAuthor4;

            Title title = new Title();
            List<Title_Author> titleAuthors = new List<Title_Author>();

            bool isNew = false;
            bool isValid = false;
            using (bookscontext context = new bookscontext())
            {
                var publisher = (Publisher)cboPublishers.SelectedItem;
                var pubID = publisher.PubID;

                if (txtPubID.Text == "")
                    isNew = true;

                if (isNew)
                {
                    

                    title.Year_Published = Convert.ToInt32(txtYearPublished.Text);
                    title.Commets = txtComments.Text;
                    title.Description = txtDescription.Text;
                    title.ISBN = txtISBN.Text;
                    title.Notes = txtNotes.Text;
                    title.Subject = txtSubject.Text;
                    title.Title1 = txtTitle.Text;
                    title.PubID = pubID;

                    for (int i = 0; i < 4; i++)
                    {
                        var A1 = (Author)authorCombo[i].SelectedItem;
                        if (A1 != null)
                        {
                            var author = context.Authors.Find(A1.Au_ID);
                            Title_Author title_Author = new Title_Author();
                            title_Author.AU_ID = A1.Au_ID;
                            title_Author.ISBN = txtISBN.Text;
                            titleAuthors.Add(title_Author);
                        }
                    }

                    foreach (var author in titleAuthors)
                    {
                        Title_Author auth = new Title_Author();
                        auth = author;

                        context.Database.ExecuteSqlCommand("INSERT INTO [Books].[dbo].[Title_Author]" +
                            "(ISBN, AU_ID)" + "VALUES" + "(" + "'" + txtISBN.Text + "'" + "," + auth.AU_ID + ")");
                    }

                    isValid = ValidateInput();
                    if (!isValid)
                    {
                        return;
                    }
                    context.Titles.Add(title);
                    context.SaveChanges();
                }
                else
                {
                    try
                    {
                        var record = context.Titles.Find(txtISBN.Text);
                        record.Commets = txtComments.Text;
                        record.Description = txtDescription.Text;
                        record.ISBN = txtISBN.Text;
                        record.Notes = txtNotes.Text;
                        record.PubID = Convert.ToInt16(txtPubID.Text);
                        record.Subject = txtSubject.Text;
                        record.Title1 = txtTitle.Text;
                        record.Year_Published = Convert.ToInt32(txtYearPublished.Text);
                        context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Failed to Save", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }              
            }
            ResolveBindings();
            SetAppState("View");            
            // result = SaveTitleDetails(title, isNew);
        }

        private void ResolveBindings()
        {
            titleBindingSource.EndEdit();
            authorBindingSource.EndEdit();
            publisherBindingSource.EndEdit();

            using (bookscontext context = new bookscontext())
            {
                try
                {
                    context.Titles.ToList();
                    context.Authors.ToList();
                    context.Publishers.ToList();

                    this.titleBindingSource.DataSource = context.Titles.Local.ToBindingList();
                    this.authorBindingSource.DataSource = context.Authors.Local.ToBindingList();
                    this.publisherBindingSource.DataSource = context.Publishers.Local.ToBindingList();

                    var publisherList = context.Publishers.ToList();
                    var pubIndex = publisherList.FindIndex(p => p.Name.Equals(cboPublishers.Text));
                    var pub = publisherList.ElementAt(pubIndex);
                    var title = context.Titles.Find(txtISBN.Text);

                    var firstMatchTitle = pub;
                    if (pubIndex >= 0)
                    {
                        publisherBindingSource.Position = pubIndex;
                        txtPubID.Text = pub.PubID.ToString();
                    }

                    List<Author> authors = title.Authors.ToList();

                    List<string> authorNames = new List<string>();
                    foreach (var t in authors)
                    {
                        authorNames.Add(t.Author1);
                    }

                    //for (int i = 0; i < 4; ++i)
                    //{
                    //    authorCombo[i].DataBindings.Clear();
                    //    authorCombo[i].DataSource = null;
                    //    //    authorCombo[i].DisplayMember = "Author1";
                    //    //    authorCombo[i].ValueMember = "Au_ID";
                    //    //    //authorCombo[i].Items.Clear();
                    //    //authorCombo[i].Items.Add(authorNames[i]);
                    //    //authorCombo[i].SelectedIndex = 0;
                    //}
                    for (int i = 0; i < authorNames.Count; ++i)
                    {
                        authorCombo[i].Items.Add(authorNames[i]);
                        authorCombo[i].DisplayMember = "Author1";
                        authorCombo[i].ValueMember = "Au_ID";
                        authorCombo[i].SelectedIndex = 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex + " " + "There is a problem with data binding",
                        "Problem with Load", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
        private void btnDone_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            authorCombo[0] = cboAuthor1;
            authorCombo[1] = cboAuthor2;
            authorCombo[2] = cboAuthor3;
            authorCombo[3] = cboAuthor4;

            //using (bookscontext context = new bookscontext())
            //{
            //    context.Titles.ToList();
            //    context.Authors.ToList();
            //    context.Publishers.ToList();

            //    this.titleBindingSource.DataSource = context.Titles.Local.ToBindingList();
            //    this.authorBindingSource.DataSource = context.Authors.Local.ToBindingList();
            //    this.publisherBindingSource.DataSource = context.Publishers.Local.ToBindingList();

            //    titleBindingSource.Position = 0;
            //    authorBindingSource.Position = 0;
            //    publisherBindingSource.Position = 0;                
            //}
            //cboPublishers.DataSource = publisherBindingSource.DataSource;
            //cboPublishers.DisplayMember = "Company_Name";
            //cboPublishers.ValueMember = "PubID";
            //authorCombo[0].DataSource = authorBindingSource.DataSource;
            //authorCombo[0].DisplayMember = "Author1";
            //authorCombo[0].ValueMember = "Au_ID";
            //authorCombo[1].DataSource = authorBindingSource.DataSource;
            //authorCombo[1].DisplayMember = "Author1";
            //authorCombo[1].ValueMember = "Au_ID";
            //authorCombo[2].DataSource = authorBindingSource.DataSource;
            //authorCombo[2].DisplayMember = "Author1";
            //authorCombo[2].ValueMember = "Au_ID";
            //authorCombo[3].DataSource = authorBindingSource.DataSource;
            //authorCombo[3].DisplayMember = "Author1";
            //authorCombo[3].ValueMember = "Au_ID";
            SetAppState("View");
        }

        
        private bool ValidateInput()
        {
            string message = "";
            bool isOK = true;
            int inputYear, currentYear;

            if (txtTitle.Text.Equals(""))
            {
                message = "You must enter a title.\r\n";
                isOK = false;
            }
            if (!txtYearPublished.Text.Trim().Equals(""))
            {
                inputYear = Convert.ToInt32(txtYearPublished.Text);
                currentYear = DateTime.Now.Year;

                if (inputYear > currentYear)
                {
                    message += "Year published cannot be greater than current year\r\n";
                    isOK = false;
                }
            }
            if (!(txtISBN.Text.Length == 13))
            {
                message += "Incomplete ISBN\r\n";
                isOK = false;
            }
            //TO DO validate publisher
            if (!isOK)
            {
                MessageBox.Show(message, "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return isOK;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult response;
            response = MessageBox.Show("Are you sure you want to delete this title?", "Delete Record", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (response == DialogResult.No)
                return;

            try
            {
                Title title = new Title { ISBN = txtISBN.Text };

                using (bookscontext context = new bookscontext())
                {
                    context.Titles.ToList();
                    context.Titles.Attach(title);
                    context.Titles.Remove(title);
                    context.SaveChanges();
                    titleBindingSource.Remove(titleBindingSource.Current);
                    SetAppState("View");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Deleting Record", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text == "" || txtSearch.Text.Length < 3)
            {
                MessageBox.Show("Invalid Search", "Invalid Search", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (bookscontext context = new bookscontext())
            {
                authorCombo[0] = cboAuthor1;
                authorCombo[1] = cboAuthor2;
                authorCombo[2] = cboAuthor3;
                authorCombo[3] = cboAuthor4;

                var titleList = context.Titles.ToList();
                var searchText = txtSearch.Text;
                var results = titleList.FindAll(t => t.Title1.Contains(searchText));

                frmSearch searchForm = new frmSearch(results, "Titles");
                searchForm.ShowDialog();
                searchForm.Dispose();

                var title = searchForm.selection;
                var index = titleList.FindIndex(t => t.ISBN == title.ISBN);
                titleBindingSource.Position = index;
                publisherBindingSource.Position = index;

                var titleAuthors = title.Authors;

                List<string> authorNames = new List<string>();
                foreach (var t in titleAuthors)
                {
                    authorNames.Add(t.Author1);
                }
                for (int c = 0; c < 4; c++)
                {
                    authorCombo[c].Items.Clear();
                }
                for (int i = 0; i < authorNames.Count; ++i)
                {
                    authorCombo[i].Items.Add(authorNames[i]);
                    authorCombo[i].SelectedIndex = 0;
                }
            }

        }

        private void txtYear_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
        private void SetAppState(string state)
        {
            switch (state)
            {
                case "View":
                    txtTitle.BackColor = Color.Red;
                    txtTitle.ForeColor = Color.Black;
                    txtComments.TabStop = false;
                    txtComments.Enabled = false;
                    txtComments.BackColor = Color.White;
                    txtDescription.TabStop = false;
                    txtISBN.TabStop = false;
                    txtNotes.TabStop = false;
                    txtSubject.TabStop = false;
                    txtTitle.TabStop = false;
                    txtSearch.Enabled = true;
                    txtYearPublished.TabStop = false;
                    txtYearPublished.BackColor = Color.White;
                    btnAuthors.Enabled = true;
                    btnCancel.Enabled = true;
                    btnDelete.Enabled = true;
                    btnDone.Enabled = true;
                    btnEdit.Enabled = true;
                    btnFind.Enabled = true;
                    btnFirst.Enabled = true;
                    btnLast.Enabled = true;
                    btnNext.Enabled = true;
                    btnPrevious.Enabled = true;
                    btnPublishers.Enabled = true;
                    btnSave.Enabled = true;
                    break;
                case "Add":
                    txtTitle.Focus();
                    txtComments.TabStop = true;
                    txtDescription.TabStop = true;
                    txtISBN.TabStop = true;
                    txtNotes.TabStop = true;
                    txtSubject.TabStop = true;
                    txtTitle.TabStop = true;
                    txtYearPublished.TabStop = true;
                    txtSearch.Enabled = false;
                    txtSearch.TabStop = false;
                    btnAuthors.Enabled = false;
                    btnCancel.Enabled = true;
                    btnDelete.Enabled = false;
                    btnDone.Enabled = true;
                    btnEdit.Enabled = false;
                    btnFind.Enabled = false;
                    btnFirst.Enabled = false;
                    btnLast.Enabled = false;
                    btnNext.Enabled = false;
                    btnPrevious.Enabled = false;
                    btnPublishers.Enabled = false;
                    btnSave.Enabled = true;
                    txtComments.Clear();
                    txtDescription.Clear();
                    txtISBN.Clear();
                    txtNotes.Clear();
                    txtPubID.Clear();
                    txtSearch.Clear();
                    txtSubject.Clear();
                    txtTitle.Clear();
                    txtYearPublished.Clear();
                    txtComments.DataBindings.Clear();
                    txtDescription.DataBindings.Clear();
                    txtISBN.DataBindings.Clear();
                    txtNotes.DataBindings.Clear();
                    txtPubID.DataBindings.Clear();
                    txtSubject.DataBindings.Clear();
                    txtTitle.DataBindings.Clear();
                    txtYearPublished.DataBindings.Clear();
                    break;
                default:
                    txtTitle.BackColor = Color.Red;
                    txtTitle.ForeColor = Color.Black;
                    txtComments.TabStop = true;
                    txtDescription.TabStop = true;
                    txtISBN.TabStop = false;
                    txtNotes.TabStop = true;
                    txtSubject.TabStop = true;
                    txtTitle.TabStop = false;
                    txtYearPublished.TabStop = true;
                    txtSearch.Enabled = false;
                    txtSearch.TabStop = false;
                    btnAuthors.Enabled = false;
                    btnCancel.Enabled = true;
                    btnDelete.Enabled = false;
                    btnDone.Enabled = true;
                    btnEdit.Enabled = false;
                    btnFind.Enabled = false;
                    btnFirst.Enabled = false;
                    btnLast.Enabled = false;
                    btnNext.Enabled = false;
                    btnPrevious.Enabled = false;
                    btnPublishers.Enabled = false;
                    btnSave.Enabled = true;
                    break;
            }
        }

        private void btnAuthors_Click(object sender, EventArgs e)
        {            
            frmAuthors authorForm = new frmAuthors();
            authorForm.ShowDialog();
            authorForm.Dispose();
        }

        private void btnPublishers_Click(object sender, EventArgs e)
        {
            frmPublishers publisherForm = new frmPublishers();
            publisherForm.ShowDialog();
            publisherForm.Dispose();
        }

    }
}
