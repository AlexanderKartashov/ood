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
		public (IInputDataStream input, IOutputDataStream output) ProccessCommandLineOptions(CommandLineOptions options)
		{
			string outputFilePath = GetAbsolutePath(options.Output);
			IOutputDataStream output = new OutputFileStream(outputFilePath);
			if (options.Compress)
			{
				output = AddCompression(output);
			}

			if (options.Encrypt.HasValue)
			{
				output = AddEncryption(output, options.Encrypt.Value);
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

				if (options.Decrypt.HasValue)
				{
					input = AddDecryption(input, options.Decrypt.Value);
				}
			}
			else
			{
				input = new RandomStream();
			}

			return (input, output);
		}

		IInputDataStream AddDecryption(IInputDataStream input, int key)
		{
			return new DecryptionDecorator(input, new EncryptionWithReplacementTable(new ReplacementTableGenerator().GenerateReplacementTable(key)));
		}

		IInputDataStream AddDecompression(IInputDataStream input)
		{
			return new DecompressionDecorator(input, new RLECompressionStrategy());
		}

		IOutputDataStream AddCompression(IOutputDataStream output)
		{
			return new CompressionDecorator(output, new RLECompressionStrategy());
		}

		IOutputDataStream AddEncryption(IOutputDataStream output, int key)
		{
			return new EncryptionDecorator(output, new EncryptionWithReplacementTable(new ReplacementTableGenerator().GenerateReplacementTable(key)));
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
