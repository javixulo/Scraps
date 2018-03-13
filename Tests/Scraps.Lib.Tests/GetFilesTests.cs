using System.Collections.Generic;
using System.IO.Abstractions.TestingHelpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Scraps.Lib.Tests
{
	[TestClass]
	public class GetFilesTests
	{
		[TestMethod]
		public void GetFilesSimpleTest()
		{
			var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
			{
				{ @"c:\myfile.txt", new MockFileData("Testing is meh.") },
				{ @"c:\demo\jQuery.js", new MockFileData("some js") },
				{ @"c:\demo\image.gif", new MockFileData(new byte[] { 0x12, 0x34, 0x56, 0xd2 }) }
			});

			var files = FileHelper.GetFiles(fileSystem, @"C:\");

			Assert.AreEqual(3, files.Count);

			Assert.AreEqual(@"c:\myfile.txt", files[0]);
			Assert.AreEqual(@"c:\demo\jQuery.js", files[1]);
			Assert.AreEqual(@"c:\demo\image.gif", files[2]);
		}

		[TestMethod]
		public void GetFilesFilterTest()
		{
			var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
			{
				{ @"c:\myfile.txt", new MockFileData("Testing is meh.") },
				{ @"c:\demo\jQuery.js", new MockFileData("some js") },
				{ @"c:\demo\myfile.txt", new MockFileData("some js") },
				{ @"c:\demo\myfile.jx", new MockFileData("some js") },
				{ @"c:\demo\image.gif", new MockFileData(new byte[] { 0x12, 0x34, 0x56, 0xd2 }) }
			});

			var files = FileHelper.GetFiles(fileSystem, @"C:\", new[] { ".txt", ".jx" });

			Assert.AreEqual(3, files.Count);

			Assert.AreEqual(@"c:\myfile.txt", files[0]);
			Assert.AreEqual(@"c:\demo\myfile.txt", files[1]);
			Assert.AreEqual(@"c:\demo\myfile.jx", files[2]);
		}

		[TestMethod]
		public void GetFilesNoFilesTest()
		{
			var fileSystem = new MockFileSystem();

			fileSystem.AddDirectory(@"C:\demo");

			var files = FileHelper.GetFiles(fileSystem, @"C:\demo");

			Assert.AreEqual(0, files.Count);
		}

		[ExpectedException(typeof(System.IO.DirectoryNotFoundException))]
		[TestMethod]
		public void GetFilesNonExistentFolderTest()
		{
			var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
			{
				{ @"c:\myfile.txt", new MockFileData("Testing is meh.") }
			});

			var files = FileHelper.GetFiles(fileSystem, @"C:\what");

			Assert.AreEqual(0, files.Count);
		}

		[TestMethod]
		public void GetFilesStartsInCorrectLocationTest()
		{
			var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
			{
				{ @"c:\ignore.txt", new MockFileData("Testing is meh.") },
				{ @"c:\folder\myfile.txt", new MockFileData("Testing is meh.") },
				{ @"c:\folder\demo\jQuery.js", new MockFileData("some js") },
				{ @"c:\folder\demo\image.gif", new MockFileData(new byte[] { 0x12, 0x34, 0x56, 0xd2 }) }
			});

			var files = FileHelper.GetFiles(fileSystem, @"C:\folder");

			Assert.AreEqual(3, files.Count);

			Assert.AreEqual(@"c:\folder\myfile.txt", files[0]);
			Assert.AreEqual(@"c:\folder\demo\jQuery.js", files[1]);
			Assert.AreEqual(@"c:\folder\demo\image.gif", files[2]);
		}

		[TestMethod]
		public void GetFilesComplexTest()
		{
			var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
			{
				{ @"c:\myfile.txt", new MockFileData("Text") },
				{ @"c:\myfile2.txt", new MockFileData("Text") },
				{ @"c:\demo\jQuery.js", new MockFileData("Text") },
				{ @"c:\myfile3.txt", new MockFileData("Text") },
				{ @"c:\demo\image.gif", new MockFileData(new byte[] { 0x12, 0x34, 0x56, 0xd2 }) },
				{ @"c:\demo\demo\level3_1.txt", new MockFileData("Text") },
				{ @"c:\demo\demo\level3_2.txt", new MockFileData("Text") },
				{ @"c:\demo\image2.gif", new MockFileData(new byte[] { 0x12, 0x34, 0x56, 0xd2 }) },
				{ @"c:\myfile4.txt", new MockFileData("Text") },
			});

			var files = FileHelper.GetFiles(fileSystem, @"C:\");

			Assert.AreEqual(9, files.Count);

			Assert.AreEqual(@"c:\myfile.txt", files[0]);
			Assert.AreEqual(@"c:\myfile2.txt", files[1]);
			Assert.AreEqual(@"c:\myfile3.txt", files[2]);
			Assert.AreEqual(@"c:\myfile4.txt", files[3]);
			Assert.AreEqual(@"c:\demo\jQuery.js", files[4]);
			Assert.AreEqual(@"c:\demo\image.gif", files[5]);
			Assert.AreEqual(@"c:\demo\image2.gif", files[6]);
			Assert.AreEqual(@"c:\demo\demo\level3_1.txt", files[7]);
			Assert.AreEqual(@"c:\demo\demo\level3_2.txt", files[8]);
		}

		[TestMethod]
		public void GetFilesSimpleExcludeTest()
		{
			var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
			{
				{ @"c:\myfile.txt", new MockFileData("Testing is meh.") },
				{ @"c:\demo\jQuery.js", new MockFileData("some js") },
				{ @"c:\demo\image.gif", new MockFileData(new byte[] { 0x12, 0x34, 0x56, 0xd2 }) },
				{ @"c:\exclude\excluded.txt", new MockFileData("Testing is meh.") },

			});

			var files = FileHelper.GetFiles(fileSystem, @"C:\", null, new string[] { @"C:\exclude" });

			Assert.AreEqual(3, files.Count);

			Assert.AreEqual(@"c:\myfile.txt", files[0]);
			Assert.AreEqual(@"c:\demo\jQuery.js", files[1]);
			Assert.AreEqual(@"c:\demo\image.gif", files[2]);
		}
	}

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

			Assert.AreEqual(1, files.Count);

			Assert.AreEqual(@"c:\test\myfile.txt", files[0]);
		}
	}
}
