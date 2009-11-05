using System;
using System.Windows.Forms;
using Security.TestRepository;
using StructureMap;
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
			ObjectFactory.Configure(c => c.AddRegistry<DefaultRegistry>());

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Form1());
		}
	}
}
