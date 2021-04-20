using OMS.IDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace OMS.DataFactory
{
    public static class DataFactory
    {
        private static ICustomerDataAccess CustomerDataAccess = null;

        public static ICustomerDataAccess GetInstance(String InstanceType)
        {
            ICustomerDataAccess Instances = GetCustomerDataAccessInstance(InstanceType);

            if (Instances != null)
                return Instances;
            else
                return null;
        }

        private static T GetCustomerDataAccessInstance<T>(string InstanceType)
        {
            var value = typeof(ICustomerDataAccess);
            Type classType = typeof(ICustomerDataAccess);

            if (CustomerDataAccess == null || (CustomerDataAccess != null))
            {
                Type[] Types = Assembly.GetExecutingAssembly().GetTypes();
                foreach (Type t in Types)
                {
                    if (classType.IsAssignableFrom(t))
                    {
                        if (!t.GetTypeInfo().IsAbstract && t.GetInterfaces().Contains(classType))
                        {
                            //T Instance = Activator.CreateInstance(t) as ICustomerDataAccess;
                            //T Instance = (T)Convert.ChangeType(value, typeof(T));
                            CustomerDataAccess = (T)Convert.ChangeType(value, typeof(T)); ;
                        }
                    }
                }
            }
            return CustomerDataAccess;
        }
    }
}
