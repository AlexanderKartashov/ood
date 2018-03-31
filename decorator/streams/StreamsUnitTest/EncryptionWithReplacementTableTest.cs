using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using streams;
using System.Collections;
using System;

namespace StreamsUnitTest
{
	[TestFixture]
	[Parallelizable]
	public class EncryptionWithReplacementTableInitialilzationTest
	{
		[Test]
		public void TestInitialization()
		{
			Assert.That(() => new EncryptionWithReplacementTable(null), Throws.ArgumentNullException);
			Assert.That(() => new EncryptionWithReplacementTable(new Dictionary<byte, byte>()), Throws.Nothing);
		}

		[Test]
		public void TestIncorrectArguments()
		{
			const byte maxValue = 100;
			Dictionary<byte, byte> dictionary = new Dictionary<byte, byte>(maxValue);
			Enumerable.Range(0, maxValue).ToList().ForEach(x => dictionary.Add((byte)x, (byte)(x + 1)));
			EncryptionWithReplacementTable encryption = new EncryptionWithReplacementTable(dictionary);

			Assert.That(() => { encryption.Decrypt(byte.MaxValue); }, Throws.ArgumentException);
			Assert.That(() => { encryption.Decrypt(0); }, Throws.ArgumentException);
			Assert.That(() => { encryption.Encrypt(byte.MaxValue); }, Throws.TypeOf<KeyNotFoundException>());
		}
	}

	[TestFixture]
	[Parallelizable]
	public class EncryptionWithReplacementTableEncryptionTest
	{
		private Dictionary<byte, byte> _dictionary;
		private const byte _maxValue = 100;
		private EncryptionWithReplacementTable _encryption;

		[SetUp]
		public void Setup()
		{
			_dictionary = new Dictionary<byte, byte>();
			Enumerable.Range(0, _maxValue).ToList().ForEach(x => _dictionary.Add((byte)x, (byte)(x + 1)));
			_encryption = new EncryptionWithReplacementTable(_dictionary);
		}

		[Test]
		public void TestEncryption([Range(0, _maxValue - 1)] byte i)
		{
			Assert.That(_encryption.Encrypt(i), Is.EqualTo((byte)(i + 1)));
		}
	}

	[TestFixture]
	[Parallelizable]
	public class EncryptionWithReplacementTableDecryptionTest
	{
		private Dictionary<byte, byte> _dictionary;
		private const byte _maxValue = 100;
		private EncryptionWithReplacementTable _encryption;

		[SetUp]
		public void Setup()
		{
			_dictionary = new Dictionary<byte, byte>();
			Enumerable.Range(0, _maxValue).ToList().ForEach(x => _dictionary.Add((byte)x, (byte)(x + 1)));
			_encryption = new EncryptionWithReplacementTable(_dictionary);
		}

		[Test]
		public void TestDecryption([Range(1, _maxValue)] byte i)
		{
			Assert.That(_encryption.Decrypt(i), Is.EqualTo((byte)(i - 1)));
		}
	}
}