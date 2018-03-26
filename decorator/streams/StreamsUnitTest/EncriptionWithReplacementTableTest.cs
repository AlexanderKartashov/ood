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
			EncryptionWithReplacementTable encription = new EncryptionWithReplacementTable(dictionary);

			Assert.That(() => { encription.Decrypt(byte.MaxValue); }, Throws.ArgumentException);
			Assert.That(() => { encription.Decrypt(0); }, Throws.ArgumentException);
			Assert.That(() => { encription.Encrypt(byte.MaxValue); }, Throws.TypeOf<KeyNotFoundException>());
		}
	}

	[TestFixture]
	[Parallelizable]
	public class EncryptionWithReplacementTableEncriptionTest
	{
		private Dictionary<byte, byte> _dictionary;
		private const byte _maxValue = 100;
		private EncryptionWithReplacementTable _encription;

		[SetUp]
		public void Setup()
		{
			_dictionary = new Dictionary<byte, byte>();
			Enumerable.Range(0, _maxValue).ToList().ForEach(x => _dictionary.Add((byte)x, (byte)(x + 1)));
			_encription = new EncryptionWithReplacementTable(_dictionary);
		}

		[Test]
		public void TestEncription([Range(0, _maxValue - 1)] byte i)
		{
			Assert.That(_encription.Encrypt(i), Is.EqualTo((byte)(i + 1)));
		}
	}

	[TestFixture]
	[Parallelizable]
	public class EncryptionWithReplacementTableDecriptionTest
	{
		private Dictionary<byte, byte> _dictionary;
		private const byte _maxValue = 100;
		private EncryptionWithReplacementTable _encription;

		[SetUp]
		public void Setup()
		{
			_dictionary = new Dictionary<byte, byte>();
			Enumerable.Range(0, _maxValue).ToList().ForEach(x => _dictionary.Add((byte)x, (byte)(x + 1)));
			_encription = new EncryptionWithReplacementTable(_dictionary);
		}

		[Test]
		public void TestDecription([Range(1, _maxValue)] byte i)
		{
			Assert.That(_encription.Decrypt(i), Is.EqualTo((byte)(i - 1)));
		}
	}
}