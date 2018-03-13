using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Linq;

namespace Scraps.Lib
{
	public static class FileHelper
	{
		public static List<string> GetFiles(IFileSystem system, string folder, IEnumerable<string> searchPatterns = null, IEnumerable<string> foldersToExclude = null)
		{
			var files = new List<string>();
			
			if (foldersToExclude != null && foldersToExclude.Any(x => x.TrimEnd(new[] { '/', '\\' }).Equals(folder.TrimEnd(new[] { '/', '\\' }), StringComparison.InvariantCultureIgnoreCase)))
				return files;

			if (searchPatterns == null || !searchPatterns.Any())
				files.AddRange(system.Directory.GetFiles(folder));
			else
				files.AddRange(system.Directory.GetFiles(folder).Where(x=> searchPatterns.Any(y=> x.EndsWith(y, StringComparison.InvariantCultureIgnoreCase))));

			foreach (string directory in system.Directory.GetDirectories(folder))
				files.AddRange(GetFiles(system, directory, searchPatterns, foldersToExclude));

			return files;
		}

		public static Dictionary<string, string> GetFileNames(List<string> files, string prefix)
		{
			var result = new Dictionary<string, string>();
			int i = 1;

			double maxZeros = Math.Truncate(Math.Log10(files.Count));

			foreach (string file in files)
			{
				string zeros = string.Empty;
				zeros = zeros.PadLeft(Convert.ToInt32(maxZeros - Math.Truncate(Math.Log10(i))), '0');
				result.Add(file, string.Format("{0} {1}{2}{3}", prefix, zeros, i++, Path.GetExtension(file)));
			}

			return result;
		}

		public static void RenameFiles(IFileSystem system, IEnumerable<string> files, string pattern, IEnumerable<string> tokens)
		{
			Dictionary<string, string> nameSubstitution = SubstituteNames(files, pattern, tokens);

			var existent = nameSubstitution.Values.Where(x => system.File.Exists(x));

			if (existent.Any())
			{
				throw new ApplicationException($"Files {string.Join(", ", existent)} already exist");
			}

			foreach(var newName in nameSubstitution)
			{
				system.File.Move(newName.Key, newName.Value);
			}
		}

		private static Dictionary<string, string> SubstituteNames(IEnumerable<string> files, string pattern, IEnumerable<string> tokens)
		{
			var result = new Dictionary<string, string>();

			foreach(string file in files)
			{
				result.Add(file, GetSubstitutedFileName(file, pattern, tokens));
			}

			return result;
		}

		private static string GetSubstitutedFileName(string file, string pattern, IEnumerable<string> tokens)
		{
			return Path.Combine(string.Format(pattern, tokens), Path.GetFileName(file));
		}

	}
}
