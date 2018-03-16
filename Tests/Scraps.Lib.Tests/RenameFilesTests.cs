using System.Collections.Generic;
using System.IO.Abstractions.TestingHelpers;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Scraps.Lib.Tests
{
	[TestClass]
	public class RenameFilesTests
	{
		[TestMethod]
		public void RenameFilesSimpleTest()
		{
			const string pattern = @"c:\test\";

			var files = new List<string>
			{
				@"c:\myfile.txt"
			};

			var tokens = new List<string>
			{
				""
			};

			var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
			{
				{ files[0], new MockFileData("Testing is meh.") }
			});

			FileHelper.RenameFiles(fileSystem, files, pattern, tokens);

			Assert.AreEqual(1, fileSystem.AllFiles.Count());

			Assert.AreEqual(@"c:\test\myfile.txt", fileSystem.AllFiles.First());
		}
	}
}