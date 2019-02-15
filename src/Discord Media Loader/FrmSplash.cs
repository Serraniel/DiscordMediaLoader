#region LICENSE
/**********************************************************************************************
 * Copyright (C) 2019-2019 - All Rights Reserved
 * 
 * This file is part of "Discord Media Loader".
 * The official repository is hosted at https://github.com/Serraniel/Darkorbit-Helper-Program
 * 
 * "Discord Media Loader" is licensed under European Union Public Licence V. 1.2.
 * Full license is included in the project repository.
 * 
 * Users who edited FrmSplash.cs under the condition of the used license:
 * - Serraniel (https://github.com/Serraniel)
 **********************************************************************************************/
#endregion

using System;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;
using Discord_Media_Loader.Helper;

namespace Discord_Media_Loader
{
    public partial class FrmSplash : Form
    {
        public FrmSplash()
        {
            InitializeComponent();
        }

        private async void FrmSplash_Shown(object sender, EventArgs e)
        {
#if !DEBUG
            UseWaitCursor = true;
            try
            {
                var releaseVersion = await VersionHelper.GetReleaseVersion();
                if (releaseVersion > VersionHelper.AppVersion)
                {
                    var tmpFile = Path.GetTempFileName();
                    var downloadManager = new FrmDownload(tmpFile, await VersionHelper.DownloadVersion(releaseVersion));

                    downloadManager.StartDownload();
                    downloadManager.ShowDialog();

                    var tmpFolder = Path.GetTempFileName();
                    tmpFolder = Path.Combine(Path.GetFullPath(tmpFolder).Replace(Path.GetFileName(tmpFolder), ""), Path.GetFileNameWithoutExtension(tmpFolder));

                    var di = Directory.CreateDirectory(tmpFolder);

                    ZipFile.ExtractToDirectory(tmpFile, tmpFolder);

                    foreach (var f in di.GetFiles())
                    {
                        try
                        {
                            var fname = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, f.Name);
                            File.Copy(f.FullName, fname, true);
                        }
                        catch (Exception) { }
                    }

                    File.Delete(tmpFile);
                    DialogResult = DialogResult.Cancel;
                }
            }
            finally
            {
                UseWaitCursor = false;
            }
#endif

            DialogResult = DialogResult.OK;
        }
    }
}
