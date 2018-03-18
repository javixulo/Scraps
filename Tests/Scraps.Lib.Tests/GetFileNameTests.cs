using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Scraps.Lib.Tests
{
	[TestClass]
	public class GetFileNameTests
	{
		[TestMethod]
		public void GetFileNamesWith1ElementTest()
		{
			var names = new List<string> { "file1.txt" };

			var result = FileHelper.GetFileNames(names, "prefix");

			Assert.AreEqual("prefix 1.txt", result["file1.txt"]);
		}

		[TestMethod]
		public void GetFileNamesWith1ElementWiWthoutExtensionTest()
		{
			var names = new List<string> { "file1" };

			var result = FileHelper.GetFileNames(names, "prefix");

			Assert.AreEqual("prefix 1", result["file1"]);
		}

		[TestMethod]
		public void GetFileNamesWith2ElementsTest()
		{
			var names = new List<string> { "file1.txt", "file2.txt" };

			var result = FileHelper.GetFileNames(names, "prefix");

			Assert.AreEqual("prefix 1.txt", result["file1.txt"]);
			Assert.AreEqual("prefix 2.txt", result["file2.txt"]);
		}

		[TestMethod]
		public void GetFileNamesWith11ElementsTest()
		{
			var names = new List<string>();

			for (int i = 1; i < 12; i++)
				names.Add("file" + i + ".txt");

			var result = FileHelper.GetFileNames(names, "prefix");

			Assert.AreEqual("prefix 01.txt", result["file1.txt"]);
			Assert.AreEqual("prefix 11.txt", result["file11.txt"]);
		}

		[TestMethod]
		public void GetFileNamesWith110ElementsTest()
		{
			var names = new List<string>();

			for (int i = 1; i < 111; i++)
				names.Add("file" + i + ".txt");

			var result = FileHelper.GetFileNames(names, "prefix");

			Assert.AreEqual("prefix 001.txt", result["file1.txt"]);
			Assert.AreEqual("prefix 009.txt", result["file9.txt"]);
			Assert.AreEqual("prefix 010.txt", result["file10.txt"]);
			Assert.AreEqual("prefix 011.txt", result["file11.txt"]);
			Assert.AreEqual("prefix 099.txt", result["file99.txt"]);
			Assert.AreEqual("prefix 100.txt", result["file100.txt"]);
			Assert.AreEqual("prefix 101.txt", result["file101.txt"]);
		}
	}
}
