using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BooksManagementSystem
{
    public partial class frmSearch : Form
    {
        private ICollection<Title> _titles;
        private string _formUsed;
        public Title selection = new Title();
        public frmSearch(ICollection<Title> titles, string formUsed)
        {
            _titles = titles;
            _formUsed = formUsed;
            InitializeComponent();
        }

        private void frmSearch_Load(object sender, EventArgs e)
        {            
            foreach (var title in _titles)
            {
                lstSearchResults.Items.Add(title.Title1);
            }
        }

        private void lstSearchResults_SelectedIndexChanged(object sender, EventArgs e)
        {
            var record = lstSearchResults.SelectedIndex;
            selection = _titles.ElementAt(record);
            Close();
        }
    }
}
