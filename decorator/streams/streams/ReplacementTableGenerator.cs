﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace streams
{
	public class ReplacementTableGenerator
	{
		public Dictionary<byte, byte> GenerateReplacementTable(int key)
		{
			const int rangeElementsCount = byte.MaxValue + 1;
			Dictionary<byte, byte> replacementTable = new Dictionary<byte, byte>(rangeElementsCount);

			var rand = new Random(key);
			var shuffledIndecies = Enumerable.Range(0, rangeElementsCount).OrderBy(x => rand.Next()).Select(x => (byte)x).ToArray();

			Enumerable.Range(0, rangeElementsCount).Select(i => (byte)i).ToList().ForEach(x => {
				replacementTable[x] = shuffledIndecies[x];
			});

			return replacementTable;
		}
			
	}
}
