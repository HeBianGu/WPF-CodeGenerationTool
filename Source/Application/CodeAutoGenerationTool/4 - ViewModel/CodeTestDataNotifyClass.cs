using CodeAutoGenerationTool.Domain;
using CodeAutoGenerationTool.Provider;
using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CodeAutoGenerationTool.ViewModel
{

    partial class CodeTestDataNotifyClass
    {


        private string _leftPath = @"D:\HealthyCottage\Product\Debug\Libs\UserControls.Reports.dll";
        /// <summary> 说明  </summary>
        public string LeftPath
        {
            get { return _leftPath; }
            set
            {
                _leftPath = value;
                RaisePropertyChanged("LeftPath");
            }
        }


        private ICodeTestTemlate _selectItemplateCommand;
        /// <summary> 说明  </summary>
        public ICodeTestTemlate SelectITemplateCommand
        {
            get { return _selectItemplateCommand; }
            set
            {
                _selectItemplateCommand = value;
                RaisePropertyChanged("SelectITemplateCommand");
            }
        }

        private ObservableCollection<ICodeTestTemlate> _itemplateCommandcollection = new ObservableCollection<ICodeTestTemlate>();
        /// <summary> 说明  </summary>
        public ObservableCollection<ICodeTestTemlate> ITemplateCommandCollection
        {
            get { return _itemplateCommandcollection; }
            set
            {
                _itemplateCommandcollection = value;
                RaisePropertyChanged("ITemplateCommandCollection");
            }
        }

        public void RelayMethod(object obj)
        {
            string command = obj.ToString();

            //  Do：应用
            if (command == "LeftTextChanged")
            {
                this.RefreshLeftValue();
            }
            else if (command == "Init")
            {
                this.RefreshLeftValue();

                ITemplateCommandCollection = CodeAutoGenerationDomain.Instance.GetAllCodeTestTemlates();

            }
            //  Do：取消
            else if (command == "Generation")
            {
                this.Generation();
            }
            //  Do：取消
            else if (command == "LeftSelectChanged")
            {
                this.Calculate();

                this.Generation();
            }
            //  Do：取消
            else if (command == "Calculate")
            {
                this.Calculate();
            }
            //  Do：取消
            else if (command == "ComboboxSelectionChanged")
            {
                this.Generation();
            }
        }


        void Calculate()
        {
            var collection = this.GetAllCheckProperties(this.LeftCollection);


            Func<TypeNodeClass, TypeNodeClass, bool> fuction = (l, k) =>
            {
                return l.Name.ToUpper() == k.Name.ToUpper();
            };



            ObservableCollection<TypeNodeClass> temp = new ObservableCollection<TypeNodeClass>();

            foreach (var item in collection)
            {
                temp.Add(item);

                var result = this.ComboboxCollection.ToList().Find(l => fuction(item, l));

                item.MapNode = result;


            }

            this.SelectionCollection = temp;
        }


        private ObservableCollection<TypeNodeClass> _selectCollection = new ObservableCollection<TypeNodeClass>();
        /// <summary> 说明  </summary>
        public ObservableCollection<TypeNodeClass> SelectionCollection
        {
            get { return _selectCollection; }
            set
            {
                _selectCollection = value;
                RaisePropertyChanged("SelectionCollection");
            }
        }

        /// <summary> 获取所有勾选属性 </summary>
        List<TypeNodeClass> GetAllCheckProperties(ObservableCollection<TypeNodeClass> value)
        {
            List<TypeNodeClass> results = new List<TypeNodeClass>();

            var collection = value.ToList().FindAll(l => l.IsChecked);


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
                                results.Add(c);

                                ergodic(c);
                            }
                        }
                    }
                };

                ergodic(item);
            }

            return results;
        }


        private ObservableCollection<TypeNodeClass> _leftcollection = new ObservableCollection<TypeNodeClass>();
        /// <summary> 说明  </summary>
        public ObservableCollection<TypeNodeClass> LeftCollection
        {
            get { return _leftcollection; }
            set
            {
                _leftcollection = value;
                RaisePropertyChanged("Collection");
            }
        }


        private ObservableCollection<TypeNodeClass> _rightcollection = new ObservableCollection<TypeNodeClass>();
        /// <summary> 说明  </summary>
        public ObservableCollection<TypeNodeClass> RightCollection
        {
            get { return _rightcollection; }
            set
            {
                _rightcollection = value;
                RaisePropertyChanged("Collection");
            }
        }

        private ObservableCollection<TypeNodeClass> _comboboxCollection = new ObservableCollection<TypeNodeClass>();
        /// <summary> 说明  </summary>
        public ObservableCollection<TypeNodeClass> ComboboxCollection
        {
            get { return _comboboxCollection; }
            set
            {
                _comboboxCollection = value;
                RaisePropertyChanged("ComboboxCollection");
            }
        }

        void RefreshLeftValue()
        {
            if (this.LeftPath == null) return;

            this.LeftCollection.Clear();

            //  Message：获取所有类型

            if (!File.Exists(this.LeftPath)) return;
            

            var ass = Assembly.LoadFrom(this.LeftPath);

            var types = ass.GetTypes();

            foreach (var item in ass.GetTypes().OrderBy(l => l.Name))
            {

                if (item.MemberType == MemberTypes.NestedType) continue;

                //TypeNodeClass t = new TypeNodeClass();

                //t.FullPath += item.Name.ToLower();

                //t.Value = item;

                TypeNodeClass t = TypeNodeClass.CreateTypeNode(item, item.Name.ToLower());

                this.LeftCollection.Add(t);
            }

        }

        void Generation()
        {

            //  Do：查找类
            var collection = this.LeftCollection.ToList().FindAll(l => l.IsChecked);

            StringBuilder sb = new StringBuilder();

            var selectionPropertis = this.GetAllCheckProperties(this.LeftCollection).ToList();

            ICodeTestTemlate temlate = this.SelectITemplateCommand;


            foreach (var item in selectionPropertis)
            {
                //string result = item.FullPath + "=" + (item.MapNode == null ? "" : item.MapNode.FullPath) + ";";

                //sb.AppendLine(result);

                if (item.Value is Type)
                {
                    //  Message：说明

                    sb.AppendLine("//  开始自动生成测试数据 ：" + (item.Value as Type).Name);

                }
                else if (item.Value is PropertyInfo)
                {
                    var property = item.Value as PropertyInfo;

                    string result = item.FullPath + "=" + temlate.Template(property);

                    sb.AppendLine(result);

                    //var property = item.Value as PropertyInfo;

                    //Action<PropertyInfo> action = null;

                    //action = l =>
                    //  {
                    //      string result = item.FullPath + "=" + temlate.Template(l);

                    //      sb.AppendLine(result);

                    //      if (property.PropertyType.IsPrimitive || property.PropertyType == typeof(string)) return;

                    //      foreach (var current in l.PropertyType.GetProperties())
                    //      {
                    //          action(current);
                    //      }
                    //  };

                    //action(property);

                }

            }


            this.WriteText(sb.ToString());

        }


        string TypeToTemplate(Type type, List<TypeNodeClass> selectionPropertis)
        {
            StringBuilder sb = new StringBuilder();

            //var proprotis = type.GetProperties().ToList();

            ////  Message：包含的子类里面属性全部加载
            //var exsits = proprotis.FindAll(l => selectionPropertis.Exists(k => (PropertyInfo)k.Value == l));

            //List<string> define = new List<string>();

            //foreach (var e in exsits)
            //{
            //    if (e.PropertyType.IsPrimitive || e.PropertyType == typeof(string))
            //    {
            //        //  Do：基础类型直接生成
            //        //string result = this.PropertyToTemplate(e, e.Name, e.PropertyType.Name);


            //        sb.AppendLine(result);

            //    }
            //    else if (e.PropertyType.IsEnumerableType())
            //    {
            //        //  Do：集合类型生成集合
            //    }
            //    else
            //    {
            //        //  Do：自定义类型递归生成
            //        string result = this.PropertyToTemplate(e, e.Name, this.ToViewModel(e.Name));

            //        sb.AppendLine(result);

            //        define.Add(this.TypeToTemplate(e.PropertyType, selectionPropertis));
            //    }
            //}


            //foreach (var item in define)
            //{
            //    sb.AppendLine(item);
            //}

            return sb.ToString();


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
        void WriteText(params string[] text)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var item in text)
            {
                sb.AppendLine(item);
            }

            this.Result = sb.ToString();

            //File.WriteAllText(path, sb.ToString());

            //Process.Start(path);
        }

    }

    partial class CodeTestDataNotifyClass : INotifyPropertyChanged
    {
        public RelayCommand RelayCommand { get; set; }

        public CodeTestDataNotifyClass()
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
