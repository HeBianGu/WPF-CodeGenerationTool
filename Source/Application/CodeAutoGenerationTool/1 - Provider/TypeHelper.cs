using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Linq
{
    public static class TypeHelper
    {
        /// <summary> 检查指定类型是否有构造函数 </summary>
        public static bool IsHaveNoParamConstruct(this Type t)
        {
            return t.IsHaveNoParamConstruct(Type.EmptyTypes);
        }

        /// <summary> 检查指定类型中是否包含指定构造函数 Type[] parameters = { typeof(string),typeof(DataTable) }</summary>
        public static bool IsHaveNoParamConstruct(this Type t, Type[] parameters)
        {
            System.Reflection.ConstructorInfo ci = t.GetConstructor(parameters);

            return ci != null;

        }

   

        /// <summary> 是否为可空类型 </summary>
        public static bool IsNullableType(this Type type)
        {
            return (((type != null) && type.IsGenericType) &&
                (type.GetGenericTypeDefinition() == typeof(Nullable<>)));
        }

        /// <summary> 获取不为空的类型 </summary>
        public static Type GetNonNullableType(this Type type)
        {
            if (IsNullableType(type))
            {
                return type.GetGenericArguments()[0];
            }
            return type;
        }

        /// <summary> 是否是IEnumerable<T>类型 </summary>
        public static bool IsEnumerableType(this Type enumerableType)
        {
            return (FindGenericType(typeof(IEnumerable<>), enumerableType) != null);
        }

        /// <summary> 获取泛型集合泛型类型 </summary>
        public static Type GetElementType(this Type enumerableType)
        {
            Type type = FindGenericType(typeof(IEnumerable<>), enumerableType);
            if (type != null)
            {
                return type.GetGenericArguments()[0];
            }
            return enumerableType;
        }

        /// <summary> 是否是从指定类型继承 </summary>
        public static bool IsKindOfGeneric(this Type type, Type definition)
        {
            return (FindGenericType(definition, type) != null);
        }

        /// <summary> 查找指定类型 </summary>
        public static Type FindGenericType(this Type definition, Type type)
        {
            while ((type != null) && (type != typeof(object)))
            {
                if (type.IsGenericType && (type.GetGenericTypeDefinition() == definition))
                {
                    return type;
                }
                if (definition.IsInterface)
                {
                    foreach (Type type2 in type.GetInterfaces())
                    {
                        Type type3 = FindGenericType(definition, type2);
                        if (type3 != null)
                        {
                            return type3;
                        }
                    }
                }
                type = type.BaseType;
            }
            return null;
        }

        /// <summary> 获取默认值 </summary>
        public static object DefaultForType(this Type targetType)
        {
            return targetType.IsValueType ? Activator.CreateInstance(targetType) : null;

        }
    }
}
