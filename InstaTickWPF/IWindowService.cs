using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaTickWPF
{
    public interface IWindowService
    {
        void OpenWindow(IWindowViewModel viewModel);
        void CloseWindow(IWindowViewModel viewModel);
    }

}
