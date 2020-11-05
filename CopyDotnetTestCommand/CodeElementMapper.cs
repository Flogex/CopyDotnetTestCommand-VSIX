using System;
using System.Diagnostics.CodeAnalysis;
using CopyDotnetTestCommand.Model;
using static EnvDTE.vsCMElement;

namespace CopyDotnetTestCommand
{
    [SuppressMessage("Usage", "VSTHRD010:Invoke single-threaded types on Main thread", Justification = "Caller makes sure that we are on UI thread")]
    internal static class CodeElementMapper
    {
        public static CodeElement ToInternalCodeElement(this EnvDTE.CodeElement vsCodeElement)
        {
            var vsContainingProject = vsCodeElement.ProjectItem?.ContainingProject;
            var containingProject = vsContainingProject != null
                ? new Project(vsContainingProject.FullName)
                : (Project?)null;

            var codeElementKind = vsCodeElement.Kind.ToInternalKind();

            return new CodeElement(vsCodeElement.FullName, codeElementKind, containingProject);
        }

        private static CodeElementKind ToInternalKind(this EnvDTE.vsCMElement vsKind)
        {
            switch (vsKind)
            {
                case vsCMElementFunction: return CodeElementKind.Method;
                case vsCMElementClass: return CodeElementKind.Class;
                case vsCMElementNamespace: return CodeElementKind.Namespace;
                default: throw new NotSupportedException("Unknown code element kind.");
            }
        }
    }
}
