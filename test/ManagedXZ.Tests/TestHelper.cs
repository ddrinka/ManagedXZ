using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ManagedXZ.Tests
{
    public static class TestHelper
    {
		public static bool CompareBytes(byte[] arr1, byte[] arr2)
		{
			if (arr1.Length != arr2.Length) return false;
			for (int i = 0; i < arr1.Length; i++)
				if (arr1[i] != arr2[i]) return false;
			return true;
		}

		public static bool CompareFile(string file1, string file2)
		{
			var f1 = new FileInfo(file1);
			var f2 = new FileInfo(file2);
			if (f1.Length != f2.Length) return false;

			using (var fs1 = f1.OpenRead())
			using (var fs2 = f2.OpenRead())
			{
				const int SIZE = 1024 * 1024;
				var buffer1 = new byte[SIZE];
				var buffer2 = new byte[SIZE];
				while (true)
				{
					var cnt = fs1.Read(buffer1, 0, SIZE);
					fs2.Read(buffer2, 0, SIZE);
					if (!CompareBytes(buffer1, buffer2)) return false;
					if (cnt < SIZE) break;
				}
				return true;
			}
		}
	}
}
