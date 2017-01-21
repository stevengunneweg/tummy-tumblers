using System;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

namespace SLinq
{
	public static class SLinq
	{
		#region Random
		/// <summary>
		/// Weighted random item out of an collection using a weight selector.
		/// </summary>
		/// <returns>
		/// The random item or default(T) if no items found that weight anything
		/// </returns>
		/// <param name='collection'>
		/// Collection from where to chose an item.
		/// </param>
		/// <param name='weight'>
		/// The function that calculates the weight for each item.
		/// Weight will be clamped between 0 and positive infinity
		/// </param>
		/// <typeparam name='T'>
		/// The type of the collection.
		/// </typeparam>
		public static T WeightedRandom<T>(this IEnumerable<T> collection, Func<T, float> weight)
		{
			float 	count = 0;
			float	c;
			foreach (T t in collection)
			{
				c = weight(t);
				count += (c <= 0) ? 0 : c;
			}
			
			if (count == 0)
				return default(T);
			
			float val = UnityEngine.Random.Range(0, count);
			foreach (T t in collection)
			{
				val -= weight(t);
				if (val <= 0)
					return t;
			}
			throw new Exception("Something horrible happend in weighted random implementation");
		}

        public static T Random<T>(this IEnumerable<T> collection) {
            int count = collection.Count();
            if (count == 0)
                return default(T);

            float val = UnityEngine.Random.Range(0, count);
            foreach (T t in collection) {
                if (val <= 0)
                    return t;
                val -= 1;
            }
            throw new Exception("Something horrible happend in random implementation");
        }

        public static IEnumerable<T> Random<T>(this IEnumerable<T> col, int numberOfItems)
		{
			if (numberOfItems < 1)
				yield break;
			
			List<T> collection = col.ToList();
			int count = collection.Count;
			if (count == 0)
				yield break;
			
			if (count <= numberOfItems)
			{
				foreach (T t in collection)
					yield return t;
				yield break;
			}
			
			int[] indices = new int[numberOfItems];
			for (int i = 0; i < numberOfItems; i++)
				indices[i] = -1;
			
			for (int i = 0; i < numberOfItems; i++)
			{
				indices[i] = UnityEngine.Random.Range(0, count - i);
				bool hasEqual = true;
				while(hasEqual)
				{
					for (int j = 0; j < i; j++)
					{
						if (indices[j] == indices[i])
						{
							indices[i] = (indices[i] + 1) % count;
							continue;
						}
					}
					hasEqual = false;
				}
			}
			
			for (int i = 0; i < numberOfItems; i++)
				yield return collection[indices[i]];
		}
		#endregion
		#region Count
		public static int Count<T>(this IEnumerable<T> collection)
		{
			int c = 0;
			IEnumerator<T> enumerator = collection.GetEnumerator();
			while (enumerator.MoveNext())
				c++;
			return c;
		}
		
		public static Dictionary<R, int> Count<T, R>(this IEnumerable<T> collection, Func<T, R> selector)
		{
			Dictionary<R, int> dict = new Dictionary<R, int>();
			foreach (T item in collection) {
				R key = selector(item);
				if (dict.ContainsKey(key))
					dict[key]++;
				else
					dict.Add(key, 1);
			}
			return dict;
		}
		#endregion
		#region Skip
		public static IEnumerable<T> Skip<T>(this IEnumerable<T> collection, int amout)
		{
			int count = 0;
			foreach (T item in collection)
				if (++count > amout)
					yield return item;
		}
		#endregion
		#region Take
		public static IEnumerable<T> Take<T>(this IEnumerable<T> collection, int amout)
		{
			int count = 0;
			foreach (T item in collection)
				if (++count <= amout)
					yield return item;
		}
		#endregion
		#region Sum
		public static int Sum<T>(this IEnumerable<T> collection, Func<T, int> selector)
		{
			int c = 0;
			foreach (T item in collection)
				c += selector(item);
			return c;
		}
		
		public static float Sum<T>(this IEnumerable<T> collection, Func<T, float> selector)
		{
			float c = 0;
			foreach (T item in collection)
				c += selector(item);
			return c;
		}
		
		public static Vector3 Sum<T>(this IEnumerable<T> collection, Func<T, Vector3> selector)
		{
			Vector3 c = Vector3.zero;
			foreach (T item in collection)
				c += selector(item);
			return c;
		}
		#endregion
		#region Average
		public static int Average<T>(this IEnumerable<T> collection, Func<T, int> selector)
		{
			int c = 0;
			int count = 0;
			foreach (T item in collection)
			{
				c += selector(item);
				count++;
			}
			return c / count;
		}
		
		public static float Average<T>(this IEnumerable<T> collection, Func<T, float> selector)
		{
			float c = 0f;
			int count = 0;
			foreach (T item in collection)
			{
				c += selector(item);
				count++;
			}
			return c / count;
		}
		
		public static Vector3 Average<T>(this IEnumerable<T> collection, Func<T, Vector3> selector)
		{
			Vector3 c = Vector3.zero;
			int count = 0;
			foreach (T item in collection)
			{
				c += selector(item);
				count++;
			}
			return c / count;
		}
		#endregion
		#region Select
		public static IEnumerable<R> Select<T, R>(this IEnumerable<T> collection, Func<T, R> selector)
		{
			foreach (T t in collection)
				yield return selector(t);
		}
		#endregion
		#region Where
		public static IEnumerable<T> Where<T>(this IEnumerable<T> collection, Predicate<T> selector)
		{
			foreach (T t in collection)
				if (selector(t))
					yield return t;
		}
		public static IEnumerable<R> WhereIsAs<T, R>(this IEnumerable<T> collection)
			where T : class
			where R : class
		{
			foreach (T t in collection)
				if (t is R)
					yield return (t as R);
		}
		#endregion
		#region Any
		public static bool Any<T>(this IEnumerable<T> collection, Predicate<T> selector)
		{
			foreach (T t in collection)
				if (selector(t))
					return true;
			return false;
		}
		public static bool Any<T>(this IEnumerable<T> collection)
		{
			IEnumerator<T> enumerator = collection.GetEnumerator();
			return enumerator.MoveNext();
		}
		#endregion
		#region First
		public static T First<T>(this IEnumerable<T> collection)
		{
			foreach (T t in collection)
				return t;
			throw new InvalidOperationException();
		}
		public static T First<T>(this IEnumerable<T> collection, Predicate<T> selector)
		{
			foreach (T t in collection)
				if (selector(t))
					return t;
			throw new InvalidOperationException();
		}
		public static T FirstOrDefault<T>(this IEnumerable<T> collection)
		{
			foreach (T t in collection)
				return t;
			return default(T);
		}
		public static T FirstOrDefault<T>(this IEnumerable<T> collection, Predicate<T> selector)
		{
			foreach (T t in collection)
				if (selector(t))
					return t;
			return default(T);
		}
		#endregion
		#region To
		public static List<T> ToList<T>(this IEnumerable<T> collection)
		{
			return new List<T>(collection);
		}
		public static T[] ToArray<T>(this IEnumerable<T> collection)
		{
			T[] tArray = new T[collection.Count()];
			int index = 0;
			foreach (T t in collection)
				tArray[index++] = t;
			return tArray;
		}
		public static string ToString<T>(this IEnumerable<T> collection, string seperator)
		{
			return collection.ToString(seperator, (t) => t.ToString());
		}
		public static string ToString<T>(this IEnumerable<T> collection, string seperator, Func<T, string> toString)
		{
			StringBuilder sb = new StringBuilder();
			bool first = true;
			foreach (T t in collection)
			{
				if (first)
					first = false;
				else
					sb.Append(seperator);
				sb.Append(toString(t));
			}
			return sb.ToString();
		}
		#endregion
		#region ForEach
		public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
		{
			foreach (T item in collection)
				action(item);
		}
		#endregion
		public static IEnumerable<T> Include<T>(this IEnumerable<T> collection, T extraItem)
		{
			foreach (T item in collection)
				yield return item;
			yield return extraItem;
		}
		#region Include
		public static IEnumerable<T> Include<T>(this IEnumerable<T> collection, IEnumerable<T> collection2)
		{
			foreach (T item in collection)
				yield return item;
			foreach (T item in collection2)
				yield return item;
		}
		#endregion
		#region Implode
		public static IEnumerable<R> Implode<T, R>(this IEnumerable<T> collection, Func<T, IEnumerable<R>> selector)
		{
			foreach (T item in collection)
				foreach (R subItem in selector(item))
					yield return subItem;
		}
		#endregion
	}
}