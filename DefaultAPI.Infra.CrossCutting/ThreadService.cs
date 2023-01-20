using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace DefaultAPI.Infra.CrossCutting
{
    public sealed class ThreadService : IThreadService
    {
        public bool RunMethodWithThreadPool(int value)
        {
            bool result = false;

            for (int i = 0; i <= 10; i++)
            {
                result = ThreadPool.QueueUserWorkItem(new WaitCallback(MetodoExecutado), value);
                if (result == false)
                    break;
            }

            return result;
        }

        public bool RunMethodWithThreadParallel(List<int> list)
        {
            bool result = false;

            Parallel.ForEach(list, GetParallelOptions(), (number, loopState) =>
            {
                try
                {
                    MetodoExecutado(number);
                    result = true;
                }
                catch (Exception ex)
                {
                    loopState.Break();
                    result = false;
                }
            });

            return result;
        }

        private ParallelOptions GetParallelOptions()
        {
            return new ParallelOptions { MaxDegreeOfParallelism = Convert.ToInt32(Math.Ceiling((Environment.ProcessorCount * 0.50) * 2.0)) };
        }

        private void MetodoExecutado(object value)
        {
            int limit = (int)value;
            int total = 0;

            for (int i = 0; i <= limit; i++)
            {
                total = limit * i;
                Console.WriteLine($"Tabuada do {limit} e : \n ");
                Console.WriteLine($"{limit} * {i} = {total} \t");
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
