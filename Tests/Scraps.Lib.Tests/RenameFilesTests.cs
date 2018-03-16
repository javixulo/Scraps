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

			var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
			{
				{ files[0], new MockFileData("Testing is meh.") }
			});

			fileSystem.Directory.CreateDirectory(@"c:\test");

			FileHelper.RenameFiles(fileSystem, files, pattern, new Dictionary<string, string>());

			Assert.AreEqual(1, fileSystem.AllFiles.Count());

			Assert.AreEqual(@"c:\test\myfile.txt", fileSystem.AllFiles.First());
		}

		[TestMethod]
		public void RenameFilesCreateFolderWhenItDoesntExistTest()
		{
			const string pattern = @"c:\test1\test2";

			var files = new List<string>
			{
				@"c:\myfile.txt"
			};

			var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
			{
				{ files[0], new MockFileData("Testing is meh.") }
			});

			FileHelper.RenameFiles(fileSystem, files, pattern, new Dictionary<string, string>());

			Assert.AreEqual(1, fileSystem.AllFiles.Count());

			Assert.AreEqual(@"c:\test1\test2\myfile.txt", fileSystem.AllFiles.First());
		}

		[TestMethod]
		public void RenameFilesSubstituteTokenSimpleTest()
		{
			const string pattern = @"c:\test1\<token1>";

			var files = new List<string>
			{
				@"c:\myfile.txt"
			};

			var tokens = new Dictionary<string, string>
			{
				{"token1", "value1"}
			};

			var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
			{
				{ files[0], new MockFileData("Testing is meh.") }
			});

			FileHelper.RenameFiles(fileSystem, files, pattern, tokens);

			Assert.AreEqual(1, fileSystem.AllFiles.Count());

			Assert.AreEqual(@"c:\test1\value1\myfile.txt", fileSystem.AllFiles.First());
		}
	}
}