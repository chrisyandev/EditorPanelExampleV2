using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using EditorPanelExampleV2.Models;
using ReactiveUI;
using System.Reactive;
using EditorPanelExampleV2.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Diagnostics;
using System.Windows.Input;

namespace EditorPanelExampleV2.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private Dictionary<string, Type> _componentNameToTypeMap = new Dictionary<string, Type>()
        {
            ["Material"] = typeof(MaterialViewModel),
            ["Material List"] = typeof(MaterialListViewModel),
            ["Transform"] = typeof(TransformViewModel),
            ["Animator"] = typeof(AnimatorViewModel),
            ["Light"] = typeof(LightViewModel)
        };

        public MainWindowViewModel()
        {
            Components = new ObservableCollection<ComponentViewModelBase>();
            SetupMockData();

            ReactiveCommand<Tuple<ComponentViewModelBase, ComponentViewModelBase>, string> getDragDirectionCommand
                = ReactiveCommand.Create<Tuple<ComponentViewModelBase, ComponentViewModelBase>, string>(_ =>
                {
                    (ComponentViewModelBase target, ComponentViewModelBase source) = _;

                    int indexOfSource = Components.IndexOf(source);
                    int indexOfTarget = Components.IndexOf(target);

                    if (indexOfSource < indexOfTarget)
                    {
                        return "down";
                    }
                    else if (indexOfSource > indexOfTarget)
                    {
                        return "up";
                    }
                    else
                    {
                        return "none";
                    }
                });

            ReactiveCommand<Tuple<ComponentViewModelBase, ComponentViewModelBase>, Unit> insertComponentCommand
                = ReactiveCommand.Create<Tuple<ComponentViewModelBase, ComponentViewModelBase>>(_ =>
                {
                    (ComponentViewModelBase target, ComponentViewModelBase source) = _;

                    if (target != source)
                    {
                        string dragDirection = Components.IndexOf(source) < Components.IndexOf(target) ? "down" : "up";

                        // Must remove before inserting or indices will be off
                        Components.Remove(source);

                        if (dragDirection == "down")
                        {
                            Components.Insert(Components.IndexOf(target) + 1, source);
                        }
                        else if (dragDirection == "up")
                        {
                            Components.Insert(Components.IndexOf(target), source);
                        }
                    }
                });

            ReactiveCommand<string, Unit> selectMenuItemCommand
                = ReactiveCommand.Create<string>(selectedItem =>
                {
                    SelectedComponentName = selectedItem;
                });
            SelectMenuItemCommand = selectMenuItemCommand;

            IComponentDragService cmptDragService = App.Current?.Services?.GetService<IComponentDragService>()!;
            cmptDragService.GetDragDirectionCommand = getDragDirectionCommand;
            cmptDragService.InsertComponentCommand = insertComponentCommand;
        }

        public ObservableCollection<ComponentViewModelBase> Components { get; }

        public ICommand SelectMenuItemCommand { get; }

        #region Add Component Logic
        private string _selectedComponentName;
        public string SelectedComponentName
        {
            get => _selectedComponentName;
            set
            {
                if (value != null && _componentNameToTypeMap.ContainsKey(value))
                {
                    AddComponent(value);
                }
                else
                {
                    Debug.WriteLine("Could not create component");
                    return;
                }
                _selectedComponentName = value;
                this.RaisePropertyChanged(nameof(SelectedComponentName));
            }
        }
        private void AddComponent(string componentName)
        {
            Type viewModelType = _componentNameToTypeMap[componentName];
            ComponentViewModelBase newComponent = Activator.CreateInstance(viewModelType) as ComponentViewModelBase;
            Components.Add(newComponent);

            SetContextMenuSelectedCommand(newComponent);

            Debug.WriteLine($"Added {componentName}");
        }
        #endregion

        public void ExpandAll()
        {
            foreach (ComponentViewModelBase component in Components)
            {
                component.IsCollapsed = false;
            }
        }

        public void CollapseAll()
        {
            foreach (ComponentViewModelBase component in Components)
            {
                component.IsCollapsed = true;
            }
        }

        private void SetContextMenuSelectedCommand(ComponentViewModelBase component)
        {
            ReactiveCommand<string, Unit> contextMenuSelectedCommand =
                ReactiveCommand.Create<string>(selected =>
                {
                    int currentIndex = Components.IndexOf(component);

                    switch (selected)
                    {
                        case "remove component":
                            Components.Remove(component);
                            break;
                        case "move up":
                            int previousIndex = currentIndex - 1;
                            if (previousIndex >= 0)
                            {
                                ComponentViewModelBase temp = Components[previousIndex];
                                Components[previousIndex] = Components[currentIndex];
                                Components[currentIndex] = temp;
                            }
                            break;
                        case "move down":
                            int nextIndex = currentIndex + 1;
                            if (nextIndex < Components.Count)
                            {
                                ComponentViewModelBase temp = Components[nextIndex];
                                Components[nextIndex] = Components[currentIndex];
                                Components[currentIndex] = temp;
                            }
                            break;
                        default:
                            break;
                    }
                });
            component.ContextMenuSelectedCommand = contextMenuSelectedCommand;
        }

        private void SetupMockData()
        {
            Components.Add(new MaterialViewModel(new Material("ExampleMaterial.mat")));
            MaterialList materialList = new MaterialList
            {
                new Material("ExampleMaterial.mat"),
                new Material("ExampleMaterial1.mat"),
                new Material("ExampleMaterial2.mat"),
                new Material("ExampleMaterial3.mat")
            };
            Components.Add(new MaterialListViewModel(materialList));
            Components.Add(new TransformViewModel(new Transform(24, 30, 55)));
            Components.Add(new AnimatorViewModel(new Animator("KnightController.controller", "KnightAvatar.avatar")));
            Components.Add(new LightViewModel());
            foreach (ComponentViewModelBase component in Components)
            {
                SetContextMenuSelectedCommand(component);
            }
        }
    }
}