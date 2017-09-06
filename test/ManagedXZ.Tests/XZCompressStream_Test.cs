using System;
using System.IO;
using Xunit;

namespace ManagedXZ.Tests
{
    public class XZCompressStream_Test
    {
		[Fact]
		public void CompressZeroByte()
		{
			TestCompressFile("0byte.bin", "0byte.bin.xz");
			TestCompressInMemory(new byte[0], "0byte.bin.xz");
		}

		[Fact]
		public void CompressOneByteZero()
		{
			TestCompressFile("1byte.0.bin", "1byte.0.bin.xz");
			TestCompressInMemory(new byte[1] { 0 }, "1byte.0.bin.xz");
		}

		[Fact]
		public void CompressOneByteOne()
		{
			TestCompressFile("1byte.1.bin", "1byte.1.bin.xz");
			TestCompressInMemory(new byte[1] { 1 }, "1byte.1.bin.xz");
		}

		[Fact]
		public void Dispose()
		{
			var c = new XZCompressStream("temp2.xz");
			c.Dispose();
			c.Dispose();
		}

		void TestCompressFile(string srcFilename, string xzFilename)
		{
			srcFilename = Path.Combine("Files", srcFilename);
			xzFilename = Path.Combine("Files", xzFilename);

			var tmpfile = Path.GetTempFileName();
			XZUtils.CompressFile(srcFilename, tmpfile, FileMode.Append);
			var isSame = TestHelper.CompareFile(xzFilename, tmpfile);
			File.Delete(tmpfile);
			Assert.True(isSame);
		}

		void TestCompressInMemory(byte[] input, string xzFilename)
		{
			xzFilename = Path.Combine("Files", xzFilename);

			var data1 = XZUtils.CompressBytes(input, 0, input.Length);
			var data2 = File.ReadAllBytes(xzFilename);
			Assert.True(TestHelper.CompareBytes(data1, data2));
		}
	}
}
