using System;
using System.IO;
using System.Linq;

namespace Scraps.Model.Other
{
	public class ScrapsSettings
	{
		private readonly IQueryable<Settings> _dbSettings;
		private string _rootFoler;

		public string RootFoler
		{
			get => _rootFoler;
			set
			{
				if (value.IndexOfAny(Path.GetInvalidPathChars()) != -1)
					throw new ApplicationException($"Invalid characters (any of {string.Join(", ", Path.GetInvalidPathChars())}) found in directory name: '{value}'");

				_dbSettings.First(x => x.Key == "RootFolder").Value = value;
				_rootFoler = value;
			}
		}

		public ScrapsSettings (IQueryable<Settings> dbSettings)
		{
			_dbSettings = dbSettings;
			RootFoler = dbSettings.First(x => x.Key == "RootFolder").Value;
		}
	}
}
