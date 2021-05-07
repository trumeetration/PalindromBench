using System;
using System.Linq;
using System.Security.Cryptography;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace PalindromBench
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<PalindromBench>();
        }
    }

    public class PalindromBench
    {
        private PalindromChecker _palChecker;
        private int number;

        public PalindromBench()
        {
            _palChecker = new PalindromChecker();
            number = new Random(GetSeed()).Next();
        }

        public int GetSeed()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var intBytes = new byte[4];
                rng.GetBytes(intBytes);
                return BitConverter.ToInt32(intBytes, 0);
            }
        }

        [Benchmark]
        public bool FirstMethod() => _palChecker.PalindromCheck1(number);

        [Benchmark]
        public bool SecondMethod() => _palChecker.PalindromCheck2(number);

        [Benchmark]
        public bool ThirdMethod() => _palChecker.PalindromCheck3(number);

        [Benchmark]
        public bool FourthMethod() => _palChecker.PalindromCheck4(number);
    }

    public class PalindromChecker
    {

        public bool PalindromCheck1(int number)
        {
            char[] cArray = number.ToString().ToCharArray();
            string reverse = String.Empty;
            for (int i = cArray.Length - 1; i > -1; i--)
            {
                reverse += cArray[i];
            }
            return number.ToString() == reverse;
        }

        public bool PalindromCheck2(int number)
        {
            int length = number.ToString().Length;
            string numString = number.ToString();
            for (int i = 0; i < length / 2; i++)
            {
                if (numString[i] != numString[numString.Length - i - 1])
                {
                    return false;
                }
            }

            return true;

        }

        public bool PalindromCheck3(int number)
        {
            int ReverseNumber = 0;
            int Number = number;
            while (Number > 0)
            {
                ReverseNumber = (ReverseNumber * 10) + (Number % 10);
                Number = Number / 10;
            }

            return number == ReverseNumber;
        }

        public bool PalindromCheck4(int number)
        {
            int notreversed = number;
            int rem, sum = 0;
            while (number > 0)
            {
                rem = number % 10; 
                number = number / 10;    
                sum = sum * 10 + rem;
            }

            return notreversed == sum;
        }
    }
}
