using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CodeAutoGenerationTool.Provider
{
    class CommonCodeTestTemlate : ICodeTestTemlate
    {
        public string Name => "生成默认数据";

        Random random = new Random();

        public string Template(PropertyInfo item)
        {

            if (!item.CanWrite) return "/* 属性为只读属性 */";

            if (item.PropertyType.IsPrimitive)
            {

                return Math.Round((random.NextDouble() * 100), 1).ToString() + ";";
            }
            else if (item.PropertyType == typeof(string))
            {
                return "\""+item.Name+ "\";";
            }
            else if(item.PropertyType.IsEnumerableType())
            {
                return "/* 集合函数目前没有自动生成方法 */";
            }
            else
            {
                return "new " + item.PropertyType + "();/* 默认调用无参数构造函数 */";
            }
        }
    }
}
