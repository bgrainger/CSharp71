using System;
using System.IO;
using System.IO.Compression;
using System.Net.Sockets;

namespace CSharp9Framework
{
	class PatternMatching
	{
		public bool SwitchOnTypeCSharp8(Stream stream) =>
			stream switch
			{
				FileStream f when f.IsAsync => true,
				MemoryStream _ => false,
				NetworkStream _ => true,
				DeflateStream d => SwitchOnTypeCSharp8(d.BaseStream),
				_ => false,
			};

		// uses a relational pattern for a property; no variable names
		public bool SwitchOnType(Stream stream) =>
			stream switch
			{
				FileStream { IsAsync: true } => true,
				MemoryStream => false,
				NetworkStream => true,
				DeflateStream d => SwitchOnType(d.BaseStream),
				_ => false,
			};

		// relational operators on constants
		public string Describe(int num) =>
			num switch
			{
				0 => "zero",
				< 0 => "negative",
				> 0 => "positive",
			};

		public string SwitchOnTupleCSharp8(string input)
		{
			var success = int.TryParse(input, out var value);
			return (success, value) switch
			{
				(false, _) => "invalid input",
				var (_, v) when v == 0 => "zero",
				var (_, v) when v < 0 => "negative",
				var (_, v) when v > 0 => "positive",
				// warning CS8846: The switch expression does not handle all possible values of its input type (it is not exhaustive). For example, the pattern '(true, _)' is not covered. However, a pattern with a 'when' clause might successfully match this value.
				// _ => "other",
			};
		}

		// patterns can compare to constants
		public string RelationalPatterns(string input)
		{
			var success = int.TryParse(input, out var value);
			return (success, value) switch
			{
				(false, _) => "invalid input",
				(_, 0) => "zero",
				(_, < 0) => "negative",
				(_, > 0) => "positive",
			};
		}

		public void IsNotNull(string input)
		{
			if (input is not null)
				Console.WriteLine("Don't have to write 'is object' any more.");
		}

		public static bool IsLetterOrSeparator(char c) =>
			c is (>= 'a' and <= 'z') or (>= 'A' and <= 'Z') or '.' or ',';

		public bool IsValidIndex(int index, object[] array)
		{
			// error CS0150: A constant value is expected
			// return index is > 0 and < array.Length;

			return index > 0 && index < array.Length;
		}

		public bool IsValidPercentage(object value) => value is > 0 and < 100;

		public void ImplicitTypeTest()
		{
			IsValidPercentage(1); // True
			IsValidPercentage(1.0); // False
		}

		public bool IsValidPercentage2(double value) => value is > 0 and < 100;

		public void TypePromotionCanHappen()
		{
			IsValidPercentage2(1); // True
			IsValidPercentage2(1.0); // True
		}
	}
}
