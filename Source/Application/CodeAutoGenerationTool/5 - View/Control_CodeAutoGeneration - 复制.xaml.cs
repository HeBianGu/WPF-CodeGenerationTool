using CodeAutoGenerationTool.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CodeAutoGenerationTool.View
{
    /// <summary>
    /// Control_CodeAutoGeneration.xaml 的交互逻辑
    /// </summary>
    public partial class Control_CodeAutoGeneration : UserControl
    {

        CodeAutoGenNotifyClass _vm = new CodeAutoGenNotifyClass();

        public Control_CodeAutoGeneration()
        {
            InitializeComponent();

            this.DataContext = _vm;
        }
    }
}
