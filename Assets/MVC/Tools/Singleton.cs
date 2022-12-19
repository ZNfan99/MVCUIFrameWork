using System;

using UnityEngine;

namespace MVC2
{
	/// <summary>
	/// 线程不安全
	/// if(dog!=null) dog. eat(); ===   dog?.eat()
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public abstract class Singleton<T> where T : class, new()
	{
		
		private static T m_instance;
		public static T Instance
		{
			get
			{
				if (m_instance != null) return m_instance;
				m_instance = new T();
				// m_instance = Activator.CreateInstance<T>(); // new T()

				


				return m_instance;
			}
		}

		public static void Release()
		{
			if (m_instance != null)
			{
				m_instance = (T)((object)null);
			}
		}

		public abstract void Dispose();

	}
}

