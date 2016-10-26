namespace Game.Utils
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public static class CollectionHelper
    {
        public static void ActionOnEach<TObject>(IEnumerable<TObject> collection, Action<TObject> action)
        {
            ActionOnSpecification<TObject>(collection, action, null);
        }

        public static void ActionOnSpecification<TObject>(IEnumerable<TObject> collection, Action<TObject> action, Predicate<TObject> predicate)
        {
            if (collection != null)
            {
                if (predicate == null)
                {
                    foreach (TObject local in collection)
                    {
                        action(local);
                    }
                }
                else
                {
                    foreach (TObject local in collection)
                    {
                        if (predicate(local))
                        {
                            action(local);
                        }
                    }
                }
            }
        }

        public static bool BinarySearch<T>(IList<T> sortedList, T target, out int minIndex) where T: IComparable
        {
            if (target.CompareTo(sortedList[0]) == 0)
            {
                minIndex = 0;
                return true;
            }
            if (target.CompareTo(sortedList[0]) < 0)
            {
                minIndex = -1;
                return false;
            }
            if (target.CompareTo(sortedList[sortedList.Count - 1]) == 0)
            {
                minIndex = sortedList.Count - 1;
                return true;
            }
            if (target.CompareTo(sortedList[sortedList.Count - 1]) > 0)
            {
                minIndex = sortedList.Count - 1;
                return false;
            }
            int num = 0;
            int num2 = sortedList.Count - 1;
            while ((num2 - num) > 1)
            {
                int num3 = (num + num2) / 2;
                if (target.CompareTo(sortedList[num3]) == 0)
                {
                    minIndex = num3;
                    return true;
                }
                if (target.CompareTo(sortedList[num3]) < 0)
                {
                    num2 = num3;
                }
                else
                {
                    num = num3;
                }
            }
            minIndex = num;
            return false;
        }

        public static bool Contains<TObject>(IEnumerable<TObject> source, Predicate<TObject> predicate)
        {
            TObject local;
            return Contains<TObject>(source, predicate, out local);
        }

        public static bool Contains<TObject>(IEnumerable<TObject> source, Predicate<TObject> predicate, out TObject specification)
        {
            specification = default(TObject);
            foreach (TObject local in source)
            {
                if (predicate(local))
                {
                    specification = local;
                    return true;
                }
            }
            return false;
        }

        public static IList<TObject> Find<TObject>(IEnumerable<TObject> source, Predicate<TObject> predicate)
        {
            IList<TObject> list = new List<TObject>();
            ActionOnSpecification<TObject>(source, delegate (TObject ele) {
                list.Add(ele);
            }, predicate);
            return list;
        }

        public static TObject FindFirst<TObject>(IEnumerable<TObject> source, Predicate<TObject> predicate)
        {
            foreach (TObject local in source)
            {
                if (predicate(local))
                {
                    return local;
                }
            }
            return default(TObject);
        }

        public static T[] GetPart<T>(T[] ary, int startIndex, int count)
        {
            return GetPart<T>(ary, startIndex, count, false);
        }

        public static T[] GetPart<T>(T[] ary, int startIndex, int count, bool reverse)
        {
            int num;
            if (startIndex >= ary.Length)
            {
                return null;
            }
            if (ary.Length < (startIndex + count))
            {
                count = ary.Length - startIndex;
            }
            T[] localArray = new T[count];
            if (!reverse)
            {
                for (num = 0; num < count; num++)
                {
                    localArray[num] = ary[startIndex + num];
                }
                return localArray;
            }
            for (num = 0; num < count; num++)
            {
                localArray[num] = ary[((ary.Length - startIndex) - 1) - num];
            }
            return localArray;
        }

        public static bool IsNullOrEmpty<T>(ICollection<T> collection)
        {
            if (collection != null)
            {
                return (collection.Count == 0);
            }
            return true;
        }

        public static bool IsNullOrEmpty(ICollection collection)
        {
            if (collection != null)
            {
                return (collection.Count == 0);
            }
            return true;
        }
    }
}

