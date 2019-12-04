using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace comp110_worksheet_7
{
	public static class DirectoryUtils
	{
		// Return the size, in bytes, of the given file
		public static long GetFileSize(string filePath)
		{
			return new FileInfo(filePath).Length;
		}

		// Return true if the given path points to a directory, false if it points to a file
		public static bool IsDirectory(string path)
		{
			return File.GetAttributes(path).HasFlag(FileAttributes.Directory);
		}

		// Return the total size, in bytes, of all the files below the given directory
		public static long GetTotalSize(string directory)
		{
            long size = 0;
            string[] files = Directory.GetFiles(directory, ".", SearchOption.AllDirectories);
            foreach (string file in files) 
            {
                size += GetFileSize(file);
            }
            return size;
		}

		// Return the number of files (not counting directories) below the given directory
		public static int CountFiles(string directory)
		{
            string[] files = Directory.GetFiles(directory, ".", SearchOption.AllDirectories);
            return files.Length;
        }

        // Return the nesting depth of the given directory. A directory containing only files (no subdirectories) has a depth of 0.
        public static int GetDepth(string directory)
        {
            int DirDepth = 0;
            string depth = Directory.GetDirectories(directory);
            foreach (string Dir in depth) 
            {
                DirDepth++;
            }
            return DirDepth;
		}

        // Get the path and size (in bytes) of the smallest file below the given directory
        public static Tuple<string, long> GetSmallestFile(string directory)
        {
            string smallestFile = "";
            string[] filePaths = Directory.GetFiles(directory, "*", SearchOption.AllDirectories);
            foreach (string q in filePaths)
            {
                if (smallestFile == "")
                {
                    smallestFile = q;
                }
                else if (new FileInfo(smallestFile).Length > new FileInfo(q).Length)
                {
                    smallestFile = q;
                }
            }
            return new Tuple<string, long>(smallestFile, new FileInfo(smallestFile).Length);
        }
            // Get the path and size (in bytes) of the largest file below the given directory
            public static Tuple<string, long> GetLargestFile(string directory)
        {
            string largestFile = "";
            string[] filePaths = Directory.GetFiles(directory, "*", SearchOption.AllDirectories);
            foreach (string q in filePaths)
            {
                if (largestFile == "")
                {
                    largestFile = q;
                }
                else if (new FileInfo(largestFile).Length < new FileInfo(q).Length)
                {
                    largestFile = q;
                }
            }
            return new Tuple<string, long>(largestFile, new FileInfo(largestFile).Length);
        }
        // Get all files whose size is equal to the given value (in bytes) below the given directory
        public static IEnumerable<string> GetFilesOfSize(string directory, long size)
		{
            string[] filePaths = Directory.GetFiles(directory, "*", SearchOption.AllDirectories);
            foreach (string q in filePaths)
            {
                if (new FileInfo(q).Length == size)
                {
                    yield return q;
                }
            }
        }
	}
}
