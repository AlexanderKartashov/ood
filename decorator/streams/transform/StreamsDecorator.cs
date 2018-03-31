using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using streams;

namespace transform
{
	class StreamsDecorator
	{
		public class Streams : IDisposable
		{
			public IInputDataStream InputStream { get; set; }
			public IOutputDataStream OutputStream { get; set; }

			public void Dispose()
			{
				OutputStream.Dispose();
			}
		}


		public Streams ProccessCommandLineOptions(CommandLineOptions options)
		{
			string outputFilePath = GetAbsolutePath(options.Output);
			IOutputDataStream output = new OutputFileStream(outputFilePath);
			if (options.Compress)
			{
				output = AddCompression(output);
			}

			if (options.Encrypt != null)
			{
				output = AddEncryption(output, options.Encrypt);
			}

			IInputDataStream input = null;
			if (options.Input != null)
			{
				string inputFilePath = GetAbsolutePath(options.Input);
				input = new InputFileStream(inputFilePath);

				if (options.Decompress)
				{
					input = AddDecompression(input);
				}

				if (options.Decrypt != null)
				{
					input = AddDecryption(input, options.Decrypt);
				}
			}
			else
			{
				input = new RandomStream();
			}

			return new Streams{ InputStream = input, OutputStream = output };
		}

		IInputDataStream AddDecryption(IInputDataStream input, IEnumerable<int> keys)
		{
			IInputDataStream stream = input;
			keys.ToList().ForEach(x => {
				stream = new DecryptionDecorator(stream, new EncryptionWithReplacementTable(new ReplacementTableGenerator().GenerateReplacementTable(x)));
			});
			return stream;
		}

		IInputDataStream AddDecompression(IInputDataStream input)
		{
			return new RLEDecompressionDecorator(input);
		}

		IOutputDataStream AddCompression(IOutputDataStream output)
		{
			return new RLECompressionDectorator(output);
		}

		IOutputDataStream AddEncryption(IOutputDataStream output, IEnumerable<int> keys)
		{
			IOutputDataStream stream = output;
			keys.ToList().ForEach(x => {
				stream = new EncryptionDecorator(stream, new EncryptionWithReplacementTable(new ReplacementTableGenerator().GenerateReplacementTable(x)));
			});
			return stream;
		}

		string GetAbsolutePath(string filePath)
		{
			if (Path.IsPathRooted(filePath))
			{
				return filePath;
			}

			return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filePath);
		}
	}
}
