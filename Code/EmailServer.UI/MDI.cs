using System;
using System.Windows.Forms;

namespace EmailServer.UI
{
    public partial class MDI : Form
    {
        ActivityForm activityForm; 

        public MDI()
        {
            InitializeComponent();
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Archivos de texto (*.txt)|*.txt|Todos los archivos (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void activityToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void MDI_Load(object sender, EventArgs e)
        {
            activityForm = new ActivityForm();
            activityForm.MdiParent = this;
            activityForm.Show();
        }

        public void EnableToolbar(bool saveButtonEnabled, bool startButtonEnabled, bool pauseButtonEnabled)
        {
            this.SaveButton.Enabled = saveButtonEnabled;
            this.StartButton.Enabled = startButtonEnabled;
            this.PauseButton.Enabled = pauseButtonEnabled;
        }

        private void PauseButton_Click(object sender, EventArgs e)
        {
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            IForm form = (IForm)this.ActiveMdiChild;
            form.Start();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            IForm form = (IForm)this.ActiveMdiChild;
            form.Save();
        }

        private void emailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConfigurationForm configurationForm = new ConfigurationForm();
            configurationForm.MdiParent = this;
            configurationForm.Show();
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UserForm userForm = new UserForm();
            userForm.MdiParent = this;
            userForm.Show();
        }

        public void SetStatus(string status)
        {
            this.toolStripStatusLabel.Text = "Status: " + status;
        }

        private void cascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void tileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void tileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }
    }
}