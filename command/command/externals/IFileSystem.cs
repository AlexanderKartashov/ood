using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace command.externals
{
	public interface IFileSystem
	{
		TextWriter CreateTextFile(string path);

		#region filesystem utils
		void CopyFiles(string from, string to);
		void CreateDirectory(string path);
		void DeleteDirectory(string path);
		void DeleteFile(string filePath);
		#endregion

		#region path checkings
		bool IsAbsPath(string path);
		bool FileExists(string path);
		bool DirectoryExists(string path);
		#endregion

		#region path provider
		string GetTempDirectoryPath();
		string GetApplicationPath();
		#endregion

		#region file name generator
		string GetRandomFileName(string prefix);
		#endregion

		#region path utils
		string GetDirectoryPath(string path);
		string GetRelativePath(string basePath, string absPath);
		string GetAbsPath(string basePath, string relativePath);
		string CombinePath(params string[] values);
		string GetFileName(string filePath);
		string ChangeExtension(string filePath, string newExt);
		string GetExtension(string filePath);
		#endregion
	}
}
