﻿using System.IO;

namespace Explorer.Shared.ViewModels
{
    public sealed class DirectoryViewModel : FileEntityViewModel
    {
        public DirectoryViewModel(string directoryName) : base(directoryName, directoryName)
        {

        }

        public DirectoryViewModel(DirectoryInfo directory) : base(directory.Name, directory.FullName)
        {
            
        }
    }
}