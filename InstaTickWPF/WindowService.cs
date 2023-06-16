using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InstaTickWPF
{
    public class WindowService : IWindowService
    {
        private readonly Dictionary<IWindowViewModel, Window> _openWindows = new Dictionary<IWindowViewModel, Window>();

        public void OpenWindow(IWindowViewModel viewModel)
        {
            // Create a new window and set its DataContext to the viewModel
            var window = new AddTaskWindow { DataContext = viewModel };

            // Subscribe to the RequestClose event
            viewModel.RequestClose += () => CloseWindow(viewModel);

            // Add the window to the dictionary of open windows
            _openWindows.Add(viewModel, window);

            // Show the window
            window.Show();
        }

        public void CloseWindow(IWindowViewModel viewModel)
        {
            // Check if the window is in the dictionary of open windows
            if (_openWindows.TryGetValue(viewModel, out var window))
            {
                // Unsubscribe from the RequestClose event
                viewModel.RequestClose -= () => CloseWindow(viewModel);

                // Remove the window from the dictionary of open windows
                _openWindows.Remove(viewModel);

                // Close the window
                window.Close();
            }
        }
    }

}
