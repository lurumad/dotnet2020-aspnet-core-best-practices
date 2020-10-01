using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IAsyncEnumerableConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("GetData: Pulse cualquier tecla para comenzar");
            Console.ReadKey();
            Console.WriteLine("GetData: Comenzado");

            foreach (var data in await GetData())
            {
                Console.WriteLine(data);
            }

            Console.WriteLine("GetData: Finalizado");

            Console.WriteLine("GetDataAsyncEnum: Pulse cualquier tecla para comenzar");
            Console.ReadKey();
            Console.WriteLine("GetDataAsyncEnum: Comenzado");

            await foreach(var data in GetDataAsyncEnum())
            {
                Console.WriteLine(data);
            }
            Console.WriteLine("GetDataAsyncEnum: Finalizado");

            Console.ReadLine();
        }

        static async Task<IEnumerable<int>> GetData()
        {
            var datas = new List<int>();
            for (int i = 1; i <= 10; i++)
            {
                await Task.Delay(500);
                datas.Add(i);
            }

            return datas;
        }

        static async IAsyncEnumerable<int> GetDataAsyncEnum()
        {
            for (int i = 1; i <= 10; i++)
            {
                await Task.Delay(500);
                yield return i;
            }
        }
    }
}
