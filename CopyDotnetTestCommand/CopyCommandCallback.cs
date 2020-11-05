using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;
using EnvDTE;
using static EnvDTE.vsCMElement;

namespace CopyDotnetTestCommand
{
    [SuppressMessage("Usage", "VSTHRD010:Invoke single-threaded types on Main thread", Justification = "Caller makes sure that we are on UI thread")]
    internal static class CopyCommandCallback
    {
        public static void Execute(DTE dte)
        {
            var selectedElement = GetActiveCodeElement(dte)?.ToInternalCodeElement();

            if (!selectedElement.HasValue)
            {
                dte.StatusBar.Text = "Nothing copied to clipboard.";
                return;
            }

            var command = Model.FilterCommandBuilder.Create(selectedElement.Value).ToString();

            Clipboard.SetText(command);
            dte.StatusBar.Text = $"Copied '{command}' to clipboard.";
        }

        private static CodeElement GetActiveCodeElement(DTE dte)
        {
            var activeDoc = dte.ActiveDocument.Object("TextDocument") as TextDocument;
            var activePoint = activeDoc.Selection.ActivePoint;

            return activePoint.CodeElement[vsCMElementFunction]
                ?? activePoint.CodeElement[vsCMElementClass]
                ?? activePoint.CodeElement[vsCMElementNamespace];
        }
    }
}
