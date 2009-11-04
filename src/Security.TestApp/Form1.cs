using System;
using System.Windows.Forms;
using UoW;

namespace Security.TestApp
{
	public partial class Form1 : Form
	{
		private User CurrentUser { get; set; }

		public Form1()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			UnitOfWork.Start();
			CurrentUser = Repository<ISecurityRepository>.Do.GetUser("test");
			UnitOfWork.Stop();
			ShowCurrentUser();
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			UnitOfWork.Start();
			CurrentUser = Repository<ISecurityRepository>.Do.GetUser("fail");
			UnitOfWork.Stop();
			ShowCurrentUser();
		}

		private void button3_Click(object sender, System.EventArgs e)
		{
			UnitOfWork.Start();
			bool allowed = Repository<ISecurityService>.Do.IsAllowed(CurrentUser, "ButtonClick");
			UnitOfWork.Stop();
			
			MessageBox.Show("Are you allowed to click this? " + allowed);
		}

		private void ShowCurrentUser()
		{
			CurrentUserLabel.Text = CurrentUser.Name;
		}

		private void Form1_Load(object sender, EventArgs e)
		{

		}
	}
}
