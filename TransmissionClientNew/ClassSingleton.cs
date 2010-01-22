using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace TransmissionRemoteDotnet
{
    public class ClassSingleton<T> where T : Form
    {
        private static T instance = null;
        private static readonly object padlock = new object();

        public static T Instance
        {
            get
            {
                lock (padlock)
                {
                    if (!IsActive())
                    {
                        ConstructorInfo constructor = null;

                        try
                        {
                            // Binding flags exclude public constructors.
                            constructor = typeof(T).GetConstructor(BindingFlags.Instance |
                                          BindingFlags.NonPublic, null, new Type[0], null);
                        }
                        catch (Exception)
                        {
                            throw ;
                        }

                        if (constructor == null || constructor.IsAssembly)
                            // Also exclude internal constructors.
                            throw new Exception(string.Format("A private or " +
                                  "protected constructor is missing for '{0}'.", typeof(T).Name));

                        instance = (T)constructor.Invoke(null);
                    }
                }
                return instance;
            }
        }

        public static bool IsActive()
        {
            return instance != null && !instance.IsDisposed;
        }
    }
}
