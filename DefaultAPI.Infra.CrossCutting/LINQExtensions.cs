using DefaultAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DefaultAPI.Infra.CrossCutting
{
    public sealed class LINQExtensions
    {
        #region Função de agregação do LINQ

        public string AgregateStrings(List<string> source)
        {
            return source.Aggregate((item, itemNext) => item + "," + itemNext);
        }

        public string JoinStrings(List<string> source)
        {
            return string.Join(",", source);
        }

        public int AgregateSum(List<int> source)
        {
            return source.Aggregate((item, itemNext) => item + itemNext);
        }

        public decimal AgregateAverage(List<int> source)
        {
            return source.Aggregate(
                seed: 0,
                func: (result, item) => result + item,
                resultSelector: result => (decimal)(result / source.Count)
            );
        }

        #endregion

        #region Funções de Quantificadores 

        public bool ValidateAllElements<T>(List<T> source, Func<T, bool> predicate)
        {
            // Se todos os Elementos Atender a condição predicate, será retornado TRUE. Senão FALSE
            return source.All(predicate); // predicate => x => x % 2 == 0
        }

        public bool ExistAnyElements<T>(List<T> source, Func<T, bool> predicate)
        {
            // Se existir um Elemento que Atenda a condição predicate, será retornado TRUE. Senão FALSE
            return source.Any(predicate); // predicate => x => x % 2 == 0
        }

        #endregion

        public Dictionary<long, string> ConvertListToDictionary(List<DropDownList> list)
        {
            return list.ToDictionary(item => item.Id, item => item.Description);
        }

        public T GetFirstItemFromList<T>(List<T> list, Func<T, bool> predicate) where T : class
        {
            if (predicate is null)
                return list.FirstOrDefault();
            else
                return list.FirstOrDefault(predicate);
        }

        public T GetLastItemFromList<T>(List<T> list, Func<T, bool> predicate) where T : class
        {
            if (predicate is null)
                return list.LastOrDefault();
            else
                return list.LastOrDefault(predicate);
        }

        public int GetQtdItensFromList<T>(List<T> list, Func<T, bool> predicate) where T : class
        {
            if (predicate is null)
                return list.Count();
            else
                return list.Count(predicate);
        }

        public long GetQtdItensFromBigList<T>(List<T> list, Func<T, bool> predicate) where T : class
        {
            if (predicate is null)
                return list.LongCount();
            else
                return list.LongCount(predicate);
        }

        public decimal GetTotalItensFromList<T>(List<T> list, Func<T, decimal> predicate) where T : class
        {
            return list.Sum(predicate);
        }

        public List<T> GetFirstItensFromList<T>(List<T> list, int qtyItens) where T : class
        {
            return list.Take(qtyItens) as List<T>;
        }

        public List<T> GetLastItensFromList<T>(List<T> list, int qtyItens) where T : class
        {
            return list.TakeLast(qtyItens) as List<T>;
        }

        public List<T> RemoveItemFromList<T>(List<T> list, T item) where T : class
        {
            list.Remove(item);
            return list;
        }

        public List<T> RemoveAtItemFromList<T>(List<T> list, Predicate<T> predicate) where T : class
        {
            list.RemoveAll(predicate);
            return list;
        }

        public List<T> AddItemOnFirstPlaceOfList<T>(List<T> source, T item)
        {
            var newSource = source.Prepend<T>(item).ToList();
            return newSource;
        }

        public List<T> AddItemOnLastPlaceOfList<T>(List<T> source, T item)
        {
            var newSource = source.Append<T>(item).ToList();
            return newSource;
        }

        public List<string> ZipList(List<int> sourceId, List<string> sourceText)
        {
            // Se tiver a mesma quantidade de itens uma lista e a outra, vai combinar um resultado final numa nova lista
            var newSource = sourceId.Zip(sourceText, (Id, Text) => Id + " - " + Text).ToList();
            return newSource;
        }
    }
}
