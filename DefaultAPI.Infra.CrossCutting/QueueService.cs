using System;
using System.Collections.Generic;
using System.Text;

namespace DefaultAPI.Infra.CrossCutting
{
    /// <summary>
    /// Serviço de Fila (Recomendado para sites de pedidos de compra)
    /// </summary>
    public static class QueueService
    {
        public static T GenericDequeue<T>(this Queue<T> queue)
        {
            return queue.Dequeue();
        }

        public static T GenericPeek<T>(this Queue<T> queue)
        {
            return queue.Peek();
        }

        public static Queue<T> GenericEnqueue<T>(this Queue<T> queue, T item)
        {
            queue.Enqueue(item);
            return queue;
        }

        public static void GenericClear<T>(this Queue<T> queue)
        {
            queue.Clear();
        }

        public static bool GenericContains<T>(this Queue<T> queue, T item)
        {
            return queue.Contains(item);
        }
    }
}
