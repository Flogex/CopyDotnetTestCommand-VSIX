using System;

namespace CopyDotnetTestCommand.Model
{
    internal struct Project
    {
        public Project(string filePath)
        {
            this.FilePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
        }

        /// <summary>
        /// Path to .csproj file
        /// </summary>
        public string FilePath { get; }
    }
}
