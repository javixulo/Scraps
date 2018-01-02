using Scraps.Model;
using System.Configuration;
using System.Linq;
using System.Windows;

namespace Scraps.UI
{
	public partial class App : Application
	{
		public PicManagerContext Context { get; set; }

		public App()
		{
			((App)Application.Current).Context = new PicManagerContext(ConfigurationManager.ConnectionStrings[1].ConnectionString);
		}
	}


}
