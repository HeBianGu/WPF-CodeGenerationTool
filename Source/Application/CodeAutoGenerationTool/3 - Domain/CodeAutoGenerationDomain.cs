using CodeAutoGenerationTool.Provider;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeAutoGenerationTool.Domain
{
    class CodeAutoGenerationDomain
    {

        public static CodeAutoGenerationDomain Instance = new CodeAutoGenerationDomain();


        public ObservableCollection<ITemplateCommand> GetAllTemplateCommand()
        {
            ObservableCollection<ITemplateCommand> collection = new ObservableCollection<ITemplateCommand>();

            collection.Add(new CopyPropertyToViewModelCommand());
            collection.Add(new CopyPropertyToCreateTableCommand());
            return collection;
        }

        public ObservableCollection<ICodeTestTemlate> GetAllCodeTestTemlates()
        {
            ObservableCollection<ICodeTestTemlate> collection = new ObservableCollection<ICodeTestTemlate>();

            collection.Add(new CommonCodeTestTemlate());
            collection.Add(new CommonCodeTestTemlate());
            return collection;
        }
    }
}
