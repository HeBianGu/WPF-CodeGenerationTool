using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CodeAutoGenerationTool.Provider
{
    interface ITemplateCommand
    {
        /// <summary> 模板名称 </summary>
        string Name { get; }

        /// <summary> 生成属性模板 </summary>
        string Template(string l, string k, string type = "string");

        /// <summary> 模板抬头 </summary>
        string ToStart(string className);

        /// <summary> 模板结尾 </summary>
        string ToEnd(string className);


        /// <summary> 处理模板第一项 </summary>
        string ToFirst(string item);

        /// <summary> 处理模板最后一项 </summary>
        string ToLast(string item);
    }
}
