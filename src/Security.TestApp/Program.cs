using System;
using System.Windows.Forms;
using Security.Repository;
using UoW;

namespace Security.TestApp
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			UnitOfWork.Configure(RepoConfig.GetConfiguration());

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Form1());
		}
	}
}
