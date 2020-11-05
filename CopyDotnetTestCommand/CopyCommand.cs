using System;
using System.ComponentModel.Design;
using EnvDTE;
using Microsoft.VisualStudio.Shell;
using Task = System.Threading.Tasks.Task;

namespace CopyDotnetTestCommand
{
    internal sealed class CopyCommand
    {
        private readonly DTE _dte;

        private CopyCommand(AsyncPackage package, DTE dte, IMenuCommandService commandService)
        {
            _dte = dte ?? throw new ArgumentNullException(nameof(dte));
            commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));

            var commandSetGuid = PackageGuids.guidExtensionPackageCmdSet;
            var commandId = PackageIds.CopyCommandId;
            var menuCommandID = new CommandID(commandSetGuid, commandId);

            var menuItem = new MenuCommand((sender, args) =>
            {
                ThreadHelper.ThrowIfNotOnUIThread();
                CopyCommandCallback.Execute(_dte);
            }, menuCommandID);

            commandService.AddCommand(menuItem);
        }

        public static CopyCommand Instance { get; private set; }

        public static async Task InitializeAsync(AsyncPackage package)
        {
            // Call to AddCommand in constructor requires UI thread
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);

            var dte = await package.GetServiceAsync(typeof(DTE)) as DTE;
            var commandService = await package.GetServiceAsync(typeof(IMenuCommandService)) as IMenuCommandService;

            Instance = new CopyCommand(package, dte, commandService);
        }
    }
}
