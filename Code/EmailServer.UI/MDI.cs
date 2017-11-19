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

        private void configurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConfigurationForm configurationForm = new ConfigurationForm();
            configurationForm.MdiParent = this;
            configurationForm.Show();
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
    }
}