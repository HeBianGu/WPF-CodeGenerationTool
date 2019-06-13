using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CodeAutoGenerationTool.Provider
{
    class CopyPropertyToViewModelCommand : ITemplateCommand
    {
        public string Name { get => "生成ViewModel"; }

        public string Template(string l, string k, string type = "string")
        {
            string ss = @"            " + type + " _" + l.ToLower() + @";
            /// <summary> " + k + @" </summary>
            public " + type + " " + l + @"
            {
                get
                {
                    return _" + l.ToLower() + @";
                }
                set
                {
                    _" + l.ToLower() + @" = value;
                    RaisePropertyChanged();
                }
            }";

            return ss;
        }

        public string ToEnd(string className)
        {
            return "}";
        }

        public string ToFirst(string item)
        {
            return item;
        }

        public string ToLast(string item)
        {
            return item;
        }

        public string ToStart(string className)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("class " + className.Substring(0, 1).ToUpper() + className.Substring(1) + "_ViewModel"); 

            sb.AppendLine("{");


            return sb.ToString();

        }
    }
}
