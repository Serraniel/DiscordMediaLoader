using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            UseWaitCursor = true;
            try
            {
                var releaseVersion = await VersionHelper.GetReleaseVersion();
                if (releaseVersion > VersionHelper.CurrentVersion)
                {
                    var tmpFile = Path.GetTempFileName();
                    var downloadManager = new FrmDownload(tmpFile, await VersionHelper.DownloadVersion(releaseVersion));

                    downloadManager.StartDownload();
                    downloadManager.ShowDialog();

                    ZipFile.ExtractToDirectory(tmpFile, AppDomain.CurrentDomain.BaseDirectory);

                    File.Delete(tmpFile);
                }
            }
            finally
            {
                UseWaitCursor = false;
            }

            Close();
        }
    }
}
