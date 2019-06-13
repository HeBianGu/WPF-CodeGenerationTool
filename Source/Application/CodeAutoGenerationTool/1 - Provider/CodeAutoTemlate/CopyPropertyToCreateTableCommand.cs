using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CodeAutoGenerationTool.Provider
{
    class CopyPropertyToCreateTableCommand : ITemplateCommand
    {
        public string Name { get => "生成创建表Sql"; }

        public string Template(string l, string k, string type = "string")
        {

            if (type == typeof(string).Name)
            {
                string format = "            {0} varchar(50),";

                return string.Format(format, l);
            }
            else if (type == typeof(int).Name)
            {
                string format = "            {0} int,";

                return string.Format(format, l);
            }
            else if(type== "List`1")
            {
                string format = "            {0} text,";

                return string.Format(format, l);
            }
            else
            {
                string format = "            {0} 未识别类型,";

                return string.Format(format, l);
            }

        }

        public string ToEnd(string className)
        {
            return ");";
        }

        public string ToFirst(string item)
        {
            return item;
        }

        public string ToLast(string item)
        {
            return item.Trim(',','\n','\r');
        }

        public string ToStart(string className)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("CREATE TABLE  " + className.Substring(0, 1).ToUpper() + className.Substring(1));

            sb.AppendLine("(");


            return sb.ToString();
        }

    }
}
