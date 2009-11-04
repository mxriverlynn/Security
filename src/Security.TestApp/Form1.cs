using System;
using System.Windows.Forms;
using Security.Model;
using UoW;

namespace Security.TestApp
{
	public partial class Form1 : Form
	{
		private bool Allowed { get; set; }
		private User CurrentUser { get; set; }

		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			LoginAs("test");
		}

		private void button1_Click(object sender, EventArgs e)
		{
			LoginAs("test");
		}

		private void button2_Click(object sender, EventArgs e)
		{
			LoginAs("fail");
		}

		private void button3_Click(object sender, EventArgs e)
		{
			MessageBox.Show("You obviously have permission to click this!");
		}

		private void LoginAs(string name)
		{
			UnitOfWork.Start();
			CurrentUser = Repository<IUserRepository>.Do.GetUser(name);
			Allowed = Repository<ISecurityService>.Do.IsAllowed(CurrentUser, "ButtonClick");
			UnitOfWork.Stop();

			CurrentUserLabel.Text = CurrentUser.Name;
			button3.Enabled = Allowed;
		}
	}
}
