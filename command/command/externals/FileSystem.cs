using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace command.externals
{
	public class FileSystem : IFileSystem
	{
		public string ChangeExtension(string filePath, string newExt)
		{
			return Path.ChangeExtension(filePath, newExt);
		}

		public string CombinePath(params string[] values)
		{
			return Path.Combine(values);
		}

		public void CopyFiles(string from, string to)
		{
			File.Copy(from, to);
		}

		public void CreateDirectory(string path)
		{
			Directory.CreateDirectory(path);
		}

		public TextWriter CreateTextFile(string path)
		{
			return File.CreateText(path);
		}

		public void DeleteDirectory(string path)
		{
			Directory.Delete(path, true);
		}

		public void DeleteFile(string filePath)
		{
			File.Delete(filePath);
		}

		public bool DirectoryExists(string path)
		{
			return Directory.Exists(path);
		}

		public bool FileExists(string path)
		{
			return File.Exists(path);
		}

		public string GetAbsPath(string basePath, string relativePath)
		{
			if (IsAbsPath(relativePath))
			{
				return relativePath;
			}
			return Path.GetFullPath(Path.Combine(basePath, relativePath));
		}

		public string GetApplicationPath()
		{
			return Assembly.GetExecutingAssembly().Location;
		}

		public string GetDirectoryPath(string path)
		{
			return Path.GetDirectoryName(path);
		}

		public string GetExtension(string filePath)
		{
			return Path.GetExtension(filePath);
		}

		public string GetFileName(string filePath)
		{
			return Path.GetFileName(filePath);
		}

		public string GetRandomFileName(string prefix)
		{
			return prefix + Path.GetRandomFileName();
		}

		public string GetRelativePath(string basePath, string absPath)
		{
			Uri uriBase = new Uri(basePath);
			Uri uriAbs = new Uri(absPath);
			Uri relativeUri = uriAbs.MakeRelativeUri(uriBase);
			return relativeUri.ToString();
		}

		public string GetTempDirectoryPath()
		{
			return Path.GetTempPath();
		}

		public bool IsAbsPath(string path)
		{
			return Path.IsPathRooted(path);
		}
	}
}
