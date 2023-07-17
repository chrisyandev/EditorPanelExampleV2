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

namespace EditorPanelExampleV2.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private Dictionary<string, Type> _componentNameToTypeMap = new Dictionary<string, Type>()
        {
            ["Material"] = typeof(MaterialViewModel),
        };

        public MainWindowViewModel()
        {
            Components = new ObservableCollection<ComponentViewModelBase>
            {
                new MaterialViewModel(new Material("ONE.mat")),
                new MaterialViewModel(new Material("TWO.mat")),
                new MaterialViewModel(new Material("THREE.mat")),
                new MaterialViewModel(new Material("FOUR.mat")),
                new MaterialViewModel(new Material("FIVE.mat")),
            };

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

            ComponentDragService cmptDragService = (ComponentDragService)App.Current!.Services!
                .GetServices<IDragService>().First(s => s.GetType() == typeof(ComponentDragService));
            cmptDragService.GetDragDirectionCommand = getDragDirectionCommand;
            cmptDragService.InsertComponentCommand = insertComponentCommand;
        }

        

        public ObservableCollection<ComponentViewModelBase> Components { get; }

        //public ReactiveCommand<Tuple<ComponentViewModelBase, ComponentViewModelBase>, Unit>? InsertComponentCommand { get; }

        //public ReactiveCommand<Tuple<ComponentViewModelBase, ComponentViewModelBase>, string>? GetDragDirectionCommand { get; }
    }
}