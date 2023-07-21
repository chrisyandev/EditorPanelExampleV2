using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using EditorPanelExampleV2.ViewModels;
using ReactiveUI;
using System;
using System.Threading.Tasks;

namespace EditorPanelExampleV2.Views;

public partial class MaterialListView : UserControl
{
    public MaterialListView()
    {
        InitializeComponent();
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        (DataContext as MaterialListViewModel).ShowNewMaterialDialog.RegisterHandler(DoShowNewMaterialDialog);
    }

    private async Task DoShowNewMaterialDialog(InteractionContext<NewMaterialViewModel, string> interaction)
    {
        NewMaterialWindow dialogWindow = new NewMaterialWindow
        {
            DataContext = interaction.Input
        };

        string result = await dialogWindow.ShowDialog<string>(GetWindow());

        interaction.SetOutput(result);
    }

    Window GetWindow() => TopLevel.GetTopLevel(this) as Window ?? throw new NullReferenceException("Invalid Owner");
}