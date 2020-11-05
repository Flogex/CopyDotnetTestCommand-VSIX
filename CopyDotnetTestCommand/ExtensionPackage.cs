using System;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Task = System.Threading.Tasks.Task;

namespace CopyDotnetTestCommand
{
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [InstalledProductRegistration(Vsix.Name, Vsix.Description, Vsix.Version)]
    [Guid(ExtensionPackage.PackageGuidString)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [ProvideUIContextRule(
        contextGuid: PackageGuids.guidTestProjectUIContextString,
        name: "TestProjectUIContext",
        expression: "(CSharpProj | FSharpProj) & TestProj",
        termNames: new[] { "CSharpProj", "FSharpProj", "TestProj" },
        termValues: new[]
        {
            VSConstants.UICONTEXT.CSharpProject_string,
            VSConstants.UICONTEXT.FSharpProject_string,
            "ActiveProjectCapability:TestContainer"
        })]
    public sealed class ExtensionPackage : AsyncPackage
    {
        public const string PackageGuidString = "2fdf8c53-bdfb-4e73-a913-55028db0bcc7";

        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            await this.JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);
            await CopyCommand.InitializeAsync(this);
        }
    }
}
