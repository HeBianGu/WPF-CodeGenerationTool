using CodeAutoGenerationTool.Domain;
using CodeAutoGenerationTool.Provider;
using HeBianGu.Base.WpfBase;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CodeAutoGenerationTool.ViewModel
{


    partial class CodeAutoGenNotifyClass
    {


        private string _dllPath;
        /// <summary> 说明  </summary>
        public string DllPath
        {
            get { return _dllPath; }
            set
            {
                _dllPath = value;
                RaisePropertyChanged("DllPath");
            }
        }


        private string _pdbPath;
        /// <summary> 说明  </summary>
        public string PdbPath
        {
            get { return _pdbPath; }
            set
            {
                _pdbPath = value;
                RaisePropertyChanged("PdbPath");
            }
        }


        private ObservableCollection<TypeNodeClass> _collection = new ObservableCollection<TypeNodeClass>();
        /// <summary> 说明  </summary>
        public ObservableCollection<TypeNodeClass> Collection
        {
            get { return _collection; }
            set
            {
                _collection = value;
                RaisePropertyChanged("Collection");
            }
        }


        public void RelayMethod(object obj)
        {
            string command = obj.ToString();

            Debug.WriteLine(command);


            //  Do：应用
            if (command == "TextChanged")
            {
                this.RefreshValue();
            }
            //  Do：取消
            else if (command == "Generation")
            {
                this.Generation();
            }
            else if (command == "Init")
            {
                ITemplateCommandCollection = CodeAutoGenerationDomain.Instance.GetAllTemplateCommand();
            }
            else if (command == "TemplateSelectionChanged")
            {
                if (this.SelectITemplateCommand == null) return;

                this.TemplateText = this.SelectITemplateCommand.Template("propertyName", "说明");

                this.Generation();
            }
            else if (command == "Expanded")
            {
                if (this.SelectITemplateCommand == null) return;

                this.TemplateText = this.SelectITemplateCommand.Template("propertyName", "说明");
            }

        }



        private ITemplateCommand _selectItemplateCommand;
        /// <summary> 说明  </summary>
        public ITemplateCommand SelectITemplateCommand
        {
            get { return _selectItemplateCommand; }
            set
            {
                _selectItemplateCommand = value;
                RaisePropertyChanged("SelectITemplateCommand");
            }
        }


        private ObservableCollection<ITemplateCommand> _itemplateCommandcollection = new ObservableCollection<ITemplateCommand>();
        /// <summary> 说明  </summary>
        public ObservableCollection<ITemplateCommand> ITemplateCommandCollection
        {
            get { return _itemplateCommandcollection; }
            set
            {
                _itemplateCommandcollection = value;
                RaisePropertyChanged("ITemplateCommandCollection");
            }
        }

        void Generation()
        {
            Stopwatch watch = new Stopwatch();

            watch.Start();

            //  Do：查找类
            var collection = this.Collection.ToList().FindAll(l => l.IsChecked);

            StringBuilder sb = new StringBuilder();

            var selectionPropertis = this.GetAllCheckProperties().ToList();

            foreach (var item in collection)
            {
                string result = this.TypeToTemplate((item.Value as Type), selectionPropertis);

                sb.AppendLine(result);
            }


            watch.Stop();


            Debug.WriteLine("更新数据用时：" + watch.Elapsed.TotalSeconds);

            watch.Restart();
            this.WriteText(sb.ToString());

            watch.Stop();

            Debug.WriteLine("显示记事本用时：" + watch.Elapsed.TotalSeconds);

        }

        string TypeToTemplate(Type type, List<PropertyInfo> selectionPropertis)
        {
            StringBuilder sb = new StringBuilder();



            var proprotis = type.GetProperties().ToList();

            //  Message：包含的子类里面属性全部加载
            var exsits = proprotis.FindAll(l => selectionPropertis.Exists(k => k == l));

            List<string> define = new List<string>();

            //sb.AppendLine("class " + this.ToViewModel(type.Name));

            //sb.AppendLine("{");

            sb.AppendLine(this.SelectITemplateCommand.ToStart(type.Name));

            for (int i = 0; i < exsits.Count; i++)
            {
                var e = exsits[i];

                if (e.PropertyType.IsPrimitive || e.PropertyType == typeof(string)|| e.PropertyType.IsEnumerableType())
                {
                    //  Do：基础类型直接生成
                    string result = this.PropertyToTemplate(e, e.Name, e.PropertyType.Name);

                    if (i == 0)
                        result = this.SelectITemplateCommand.ToFirst(result);

                    if (i == exsits.Count - 1)
                        result = this.SelectITemplateCommand.ToLast(result);

                    sb.AppendLine(result);

                }
                //else if (e.PropertyType.IsEnumerableType())
                //{
                //    //  Do：集合类型生成集合
                //}
                else
                {
                    //  Do：自定义类型递归生成
                    string result = this.PropertyToTemplate(e, e.Name, this.ToViewModel(e.Name));


                    if (i == 0)
                        result = this.SelectITemplateCommand.ToFirst(result);

                    if (i == exsits.Count - 1)
                        result = this.SelectITemplateCommand.ToLast(result);

                    sb.AppendLine(result);

                    define.Add(this.TypeToTemplate(e.PropertyType, selectionPropertis));
                }
            }


            sb.AppendLine(this.SelectITemplateCommand.ToEnd(type.Name));

            return sb.ToString();


        }

        /// <summary> 获取所有勾选属性 </summary>
        List<PropertyInfo> GetAllCheckProperties()
        {
            List<PropertyInfo> results = new List<PropertyInfo>();

            var collection = this.Collection.ToList().FindAll(l => l.IsChecked);


            foreach (var item in collection)
            {
                Action<TypeNodeClass> ergodic = null;

                ergodic = l =>
                  {
                      if (l.Children != null && l.Children.Count > 0)
                      {
                          foreach (var c in l.Children)
                          {
                              if (c.IsChecked)
                              {
                                  results.Add(c.Value as PropertyInfo);

                                  ergodic(c);
                              }


                          }
                      }
                  };

                ergodic(item);
            }

            return results;
        }
        string ToViewModel(string value)
        {
            return value.Substring(0, 1).ToUpper() + value.Substring(1) + "_ViewModel";
        }

        void OnlyChildGeneration()
        {
            //StringBuilder sb = new StringBuilder();

            //var ps = this.Collection.ToList().FindAll(l => l.Item1);

            //foreach (var item in ps)
            //{
            //    string pn = this.PropertyToTemplate(item.Item2, item.Item2.Name, item.Item2.Name);

            //    sb.AppendLine(pn);
            //}

            //this.WriteText(sb.ToString());

        }

        void WriteText(params string[] text)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var item in text)
            {
                sb.AppendLine(item);
            }

            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "GenerationText.txt");

            this.Result = sb.ToString();

            //File.WriteAllText(path, sb.ToString());

            //Process.Start(path);
        }

        /// <summary> 属性生成模板 </summary>
        string PropertyToTemplate(PropertyInfo property, string propertyName, string propertyTypeName)
        {
            var s = System.Convert.ToString(18, 2).PadLeft(6, '0');

            StringBuilder sb = new StringBuilder();

            Func<string, string, string> fun = (l, k) =>
            {
                //  Message：应用模板
                return this.SelectITemplateCommand.Template(l, k, propertyTypeName);
            };

            string d = "说明";

            if (File.Exists(this.PdbPath))
            {
                if (XmlTools.filePathStr != this.PdbPath)
                {
                    XmlTools.Load(PdbPath);
                }

                var v = XmlTools.GetNodes("member");

                //foreach (var item in v)
                //{
                //    Debug.WriteLine(item);
                //}

                string format = "P:{0}.{1}";

                string pName = string.Format(format, property.DeclaringType.FullName, propertyName);

                var f = XmlTools.FindNode("member", l => l.Attributes.Find(k => k.Name == "name" && k.InnerText == pName) != null);

                if (f != null) d = f.InnerText.Replace("\r\n", "").Trim();
            }

            if (property.ReflectedType.IsEnumerableType()) return null;

            Debug.WriteLine(fun.Invoke(propertyName.Substring(0, 1).ToUpper() + propertyName.Substring(1), d));

            sb.AppendLine(fun.Invoke(propertyName.Substring(0, 1).ToUpper() + propertyName.Substring(1), d));

            return sb.ToString();
        }

        void RefreshValue()
        {
            if (this.DllPath == null) return;

            this.Collection.Clear();

            //  Message：刷新pdb路径
            string pdb = Path.Combine(Path.GetDirectoryName(this.DllPath), Path.GetFileNameWithoutExtension(this.DllPath) + ".XML");

            if (File.Exists(pdb))
            {
                this.PdbPath = pdb;
            }
            else
            {
                this.PdbPath = string.Empty;
            }

            //  Message：获取所有类型

            byte[] fileData = File.ReadAllBytes(this.DllPath);

            var ass = Assembly.Load(fileData);

            var types = ass.GetTypes();

            foreach (var item in ass.GetTypes().OrderBy(l => l.Name))
            {

                if (item.MemberType == MemberTypes.NestedType) continue;

                TypeNodeClass t = TypeNodeClass.CreateTypeNode(item, item.Name.ToLower());

                this.Collection.Add(t);
            }
        }



        private string _templateText;
        /// <summary> 说明  </summary>
        public string TemplateText
        {
            get { return _templateText; }
            set
            {
                _templateText = value;
                RaisePropertyChanged("TemplateText");
            }
        }



        private string _result;
        /// <summary> 说明  </summary>
        public string Result
        {
            get { return _result; }
            set
            {
                _result = value;
                RaisePropertyChanged("Result");
            }
        }

    }

    partial class CodeAutoGenNotifyClass : INotifyPropertyChanged
    {
        public RelayCommand RelayCommand { get; set; }

        public CodeAutoGenNotifyClass()
        {
            RelayCommand = new RelayCommand(RelayMethod);


            RelayMethod("Init");

        }
        #region - MVVM -

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }





}
