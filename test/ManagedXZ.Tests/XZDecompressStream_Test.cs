using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace ManagedXZ.Tests
{
    public class XZDecompressStream_Test
    {
		[Fact]
		public void DecompressZeroByte()
		{
			TestDecompressFile("0byte.bin.xz", "0byte.bin");
			TestDecompressInMemory(XZUtils.CompressBytes(new byte[0], 0, 0), "0byte.bin");
		}

		[Fact]
		public void DecompressOneByteZero()
		{
			TestDecompressFile("1byte.0.bin.xz", "1byte.0.bin");
			TestDecompressInMemory(XZUtils.CompressBytes(new byte[1] { 0 }, 0, 1), "1byte.0.bin");
		}

		[Fact]
		public void DecompressOneByteOne()
		{
			TestDecompressFile("1byte.1.bin.xz", "1byte.1.bin");
			TestDecompressInMemory(XZUtils.CompressBytes(new byte[1] { 1 }, 0, 1), "1byte.1.bin");
		}

		[Fact]
		public void Dispose()
		{
			var c = new XZDecompressStream("temp2.xz");
			c.Dispose();
			c.Dispose();
		}

		void TestDecompressFile(string xzFilename, string binFilename)
		{
			xzFilename = Path.Combine("Files", xzFilename);
			binFilename = Path.Combine("Files", binFilename);

			var tmpfile = Path.GetTempFileName();
			File.Delete(tmpfile);
			XZUtils.DecompressFile(xzFilename, tmpfile);
			var isSame = TestHelper.CompareFile(binFilename, tmpfile);
			File.Delete(tmpfile);
			Assert.True(isSame);
		}

		void TestDecompressInMemory(byte[] input, string binFilename)
		{
			binFilename = Path.Combine("Files", binFilename);

			var data1 = XZUtils.DecompressBytes(input, 0, input.Length);
			var data2 = File.ReadAllBytes(binFilename);
			Assert.True(TestHelper.CompareBytes(data1, data2));
		}
	}
}
