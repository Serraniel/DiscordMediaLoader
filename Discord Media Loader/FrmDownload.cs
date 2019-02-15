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
 * Users who edited FrmDownload.cs under the condition of the used license:
 * - Serraniel (https://github.com/Serraniel)
 **********************************************************************************************/
#endregion

using System;
using System.ComponentModel;
using System.Globalization;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Discord_Media_Loader
{
    internal partial class FrmDownload : Form
    {
        private string FileName { get; }
        private string Source { get; }
        private bool Finished { get; set; } = false;

        internal FrmDownload(string fileName, string source)
        {
            InitializeComponent();

            FileName = fileName;
            Source = source;
        }

        internal void StartDownload(bool waitForDialog = true)
        {
            Task.Run(() =>
            {
                while (waitForDialog && !Visible) { }

                var wc = new WebClient
                {
                    CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.NoCacheNoStore)
                };

                SetStatus($"Downloading {FileName}...");

                SetProgress(0, 100);

                wc.DownloadProgressChanged += Wc_DownloadProgressChanged;
                wc.DownloadFileCompleted += Wc_DownloadFileCompleted;

                wc.DownloadFileAsync(new Uri(Source), FileName);

                while (!Finished)
                {
                    // wait for download
                }

                RequestClose();
            });
        }

        private void Wc_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            SetStatus("Download finished");
            Finished = true;
        }

        private void Wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            var bytesIn = double.Parse(e.BytesReceived.ToString(CultureInfo.InvariantCulture));
            var totalBytes = double.Parse(e.TotalBytesToReceive.ToString(CultureInfo.InvariantCulture));
            var percentage = bytesIn / totalBytes * 100;
            SetProgress(percentage, 100);
        }

        delegate void SetStatusCallback(String status);
        private void SetStatus(String status)
        {
            if (InvokeRequired)
            {
                var callback = new SetStatusCallback(SetStatus);
                Invoke(callback, status);
            }
            else
            {
                lbStatus.Text = status;
            }
        }

        delegate void SetProgressCallback(double current, int max);
        private void SetProgress(double current, int max)
        {
            if (InvokeRequired)
            {
                var callback = new SetProgressCallback(SetProgress);
                Invoke(callback, current, max);
            }
            else
            {
                pgbProgress.Maximum = max;
                pgbProgress.Value = (int)current;
                Helper.TaskBarProgress.SetState(Handle, Helper.TaskBarProgress.TaskbarStates.Normal);
                Helper.TaskBarProgress.SetValue(Handle, (int)current, max);
            }
        }

        delegate void RequestCloseCallback();
        private void RequestClose()
        {
            if (InvokeRequired)
            {
                var callback = new RequestCloseCallback(RequestClose);
                Invoke(callback, new object[] { });
            }
            else
            {
                Close();
            }
        }
    }
}
