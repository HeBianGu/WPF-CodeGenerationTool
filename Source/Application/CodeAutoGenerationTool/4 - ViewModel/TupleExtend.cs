using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CodeAutoGenerationTool.ViewModel
{
    partial class TupleExtend<T, R>
    {

        private T _item1;
        /// <summary> 说明  </summary>
        public T Item1
        {
            get { return _item1; }
            set
            {
                _item1 = value;
                RaisePropertyChanged("Item1");
            }
        }


        private R _item2;
        /// <summary> 说明  </summary>
        public R Item2
        {
            get { return _item2; }
            set
            {
                _item2 = value;
                RaisePropertyChanged("Item2");
            }
        }



        private string _valueType;
        /// <summary> 说明  </summary>
        public string ValueType
        {
            get { return _item2.GetType().FullName; }
        }


        public void RelayMethod(object obj)
        {
            string command = obj.ToString();

            //  Do：应用
            if (command == "Sumit")
            {


            }
            //  Do：取消
            else if (command == "Cancel")
            {


            }
        }
    }

    partial class TupleExtend<T, R> : INotifyPropertyChanged
    {
        public RelayCommand RelayCommand { get; set; }

        public TupleExtend()
        {
            RelayCommand = new RelayCommand(RelayMethod);

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
