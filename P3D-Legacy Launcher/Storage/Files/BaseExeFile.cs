﻿using System.Diagnostics;

using P3D.Legacy.Shared.Storage.Folders;

using PCLExt.FileStorage;

namespace P3D.Legacy.Launcher.Storage.Files
{
    public abstract class BaseExeFile : BaseFile
    {
        protected bool Exist { get; }

        public BaseExeFile(string fileName, IFolder folder = null) : base(NameFunc(fileName, folder))
        {
            if(folder == null)
                folder = new MainFolder();
            Exist = folder.CheckExists(fileName) == ExistenceCheckResult.FileExists;
        }
        private static IFile NameFunc(string fileName, IFolder folder = null)
        {
            if (folder == null)
                folder = new MainFolder();

            return folder.CheckExists(fileName) == ExistenceCheckResult.FileExists ? folder.GetFile(fileName) : null;
        }

        public virtual bool Start(string args = "", bool useShellExecute = false, bool createNoWindow = false)
        {
            if (!Exist)
                return false;

            return new Process
            {
                StartInfo =
                {
                    FileName = Path,
                    Arguments = args,
                    UseShellExecute = useShellExecute,
                    CreateNoWindow = createNoWindow
                }
            }.Start();
        }
    }
}
