using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskAsync1
{
    class Program
    {
        static void Main(string[] args)
        {
            async void GetCount(string myInput, CancellationToken token)
            {
                var result = await StartCounting(myInput, token);
                Console.WriteLine(result);
            }

            Console.WriteLine("Please enter the number: (Type Z for cancellation)");

            var input = Console.ReadLine();
            var cancellationTokenSource = new CancellationTokenSource();

            while (input != null && !input.ToUpper().StartsWith("Z"))
            {
                GetCount(input, cancellationTokenSource.Token);
                input = Console.ReadLine();
                cancellationTokenSource.Cancel();
                cancellationTokenSource.Dispose();
                cancellationTokenSource = new CancellationTokenSource();
            }
        }

        public static async Task<string> StartCounting(string input, CancellationToken token)
        {
            try
            {
                return await Task<string>.Factory.StartNew(() => GetCounter(input, token), token);
            }
            catch 
            {
                return "Calceled";
            }
        }

        public static string GetCounter(string input, CancellationToken token)
        {
            long number = 0;
            long.TryParse(input, out number);

            return $"number = {number}, sum numbers = {Count(number, token)}";
        }

        public static long Count(long number, CancellationToken token)
        {
            long result = 0;
            for (var i = 0; i <= number; i++)
            {
                token.ThrowIfCancellationRequested();
                result += i;
            }

            return result;
        }
    }
}
