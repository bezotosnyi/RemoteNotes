using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace RemoteNotes.UI.Shell
{
    public class ControlManager
    {
        private readonly Dictionary<string, UIElement> _controlDictionary =
            new Dictionary<string, UIElement>();

        public void Register<T>(string key, T element) where T : UIElement
        {
            var userControl = (UIElement)element;
            _controlDictionary.Add(key, userControl);
        }

        public void Place(string containerName, string regionName, string elementName)
        {
            var containerControl = GetControl(containerName) as ContentControl;
            var elementControl = GetControl(elementName) as ContentControl;
            var region = containerControl?.FindName(regionName);

            if (region is DockPanel dockPanel)
            {
                dockPanel.Children.Clear();
                if (elementControl != null)
                {
                    dockPanel.Children.Add(elementControl);
                }
            }
            else if (region is Grid grid)
            {
                grid.Children.Clear();
                if (elementControl != null)
                {
                    grid.Children.Add(elementControl);
                }
            }
            else
            {
                if (region is ContentControl regionControl)
                {
                    if (elementControl != null)
                    {
                        regionControl.Content = elementControl;
                    }
                }
            }
        }

        public UIElement GetControl(string key)
        {
            UIElement userControl;

            if (_controlDictionary.ContainsKey(key))
            {
                userControl = _controlDictionary[key];
            }
            else
            {
                throw new Exception($"The control '{key}' is missing");
            }

            return userControl;
        }
    }
}