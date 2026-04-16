using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;

namespace FileCompare
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnLeftDir_Click(object sender, EventArgs e)
        {
            using (var dlg = new FolderBrowserDialog())
            {
                dlg.Description = "폴더를 선택하세요.";
                if (!string.IsNullOrWhiteSpace(txtLeftDir.Text) && Directory.Exists(txtLeftDir.Text))
                {
                    dlg.SelectedPath = txtLeftDir.Text;
                }
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    txtLeftDir.Text = dlg.SelectedPath;
                    PopulateListView(lvwLeftDir, dlg.SelectedPath);
                }
            }
        }

        private void btnRightDir_Click(object sender, EventArgs e)
        {
            using (var dlg = new FolderBrowserDialog())
            {
                dlg.Description = "폴더를 선택하세요.";
                if (!string.IsNullOrWhiteSpace(txtRightDir.Text) && Directory.Exists(txtRightDir.Text))
                {
                    dlg.SelectedPath = txtRightDir.Text;
                }
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    txtRightDir.Text = dlg.SelectedPath;
                    PopulateListView(lvwRightDir, dlg.SelectedPath);
                }
            }
        }

        private async void PopulateListView(ListView lv, string path)
        {
            lv.Items.Clear();
            if (!Directory.Exists(path)) return;

            lv.BeginUpdate();
            try
            {

                await Task.Run(() =>
                {
                    try
                    {
                        var dirs = Directory.EnumerateDirectories(path).OrderBy(d => d, StringComparer.CurrentCultureIgnoreCase);
                        foreach (var d in dirs)
                        {
                            try
                            {
                                var dirInfo = new DirectoryInfo(d);
                                var name = dirInfo.Name;
                                var modified = dirInfo.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss");
                               
                                var sizeText = "<DIR>";
                                lv.Invoke(() =>
                                {
                                    var lvi = new ListViewItem(name);
                                    lvi.SubItems.Add(modified);
                                    lvi.SubItems.Add(sizeText);
                                    // 상태 칸(place holder)
                                    lvi.SubItems.Add(string.Empty);
                                   
                                    lv.Items.Add(lvi);
                                });
                            }
                            catch { }
                        }

                        var files = Directory.EnumerateFiles(path).OrderBy(f => f, StringComparer.CurrentCultureIgnoreCase);
                        foreach (var f in files)
                        {
                            try
                            {
                                var fi = new FileInfo(f);
                                var name = fi.Name;
                                var modified = fi.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss");
                                string sizeText;
                                if (fi.Length < 1024)
                                {
                                    sizeText = "<1 KB";
                                }
                                else
                                {
                                    double kb = fi.Length / 1024.0;
                                    sizeText = string.Format("{0:N0} KB", Math.Round(kb));
                                }
                                lv.Invoke(() =>
                                {
                                    var lvi = new ListViewItem(name);
                                    lvi.SubItems.Add(modified);
                                    lvi.SubItems.Add(sizeText);
                                    // 상태 칸
                                    lvi.SubItems.Add(string.Empty);
                                
                                    lvi.Tag = fi.LastWriteTime.Ticks;
                                    lv.Items.Add(lvi);
                                });
                            }
                            catch { }
                        }
                    }
                    catch { }
                });
            }
            finally
            {
                lv.EndUpdate();
            }
            // 비교 실행 (UI 스레드)
            try
            {
                CompareLists();
            }
            catch { }
        }

        private void SetItemStatus(ListViewItem item, string status, Color color)
        {
            if (item == null) return;
            if (item.SubItems.Count < 4) item.SubItems.Add(status);
            else item.SubItems[3].Text = status;
            item.ForeColor = color;
        }

        private void CompareLists()
        {
            var leftDict = lvwLeftDir.Items.Cast<ListViewItem>().ToDictionary(i => i.Text, StringComparer.CurrentCultureIgnoreCase);
            var rightDict = lvwRightDir.Items.Cast<ListViewItem>().ToDictionary(i => i.Text, StringComparer.CurrentCultureIgnoreCase);
            var allNames = new HashSet<string>(leftDict.Keys, StringComparer.CurrentCultureIgnoreCase);
            allNames.UnionWith(rightDict.Keys);

            foreach (var name in allNames)
            {
                var inLeft = leftDict.TryGetValue(name, out var li);
                var inRight = rightDict.TryGetValue(name, out var ri);
                if (inLeft && inRight)
                {
                    // 파일만 비교: Tag에 ticks가 있으면 파일
                    var ltag = li.Tag;
                    var rtag = ri.Tag;
                    if (ltag == null || rtag == null)
                    {
                        // 디렉터리이거나 메타데이터가 없으면 동일 처리
                        SetItemStatus(li, "동일", Color.Black);
                        SetItemStatus(ri, "동일", Color.Black);
                        continue;
                    }
                    long lTicks = (long)ltag;
                    long rTicks = (long)rtag;
                    if (lTicks == rTicks)
                    {
                        SetItemStatus(li, "동일", Color.Black);
                        SetItemStatus(ri, "동일", Color.Black);
                    }
                    else if (lTicks > rTicks)
                    {
                        SetItemStatus(li, "New", Color.Red);
                        SetItemStatus(ri, "Old", Color.Gray);
                    }
                    else
                    {
                        SetItemStatus(li, "Old", Color.Gray);
                        SetItemStatus(ri, "New", Color.Red);
                    }
                }
                else if (inLeft)
                {
                    SetItemStatus(leftDict[name], "단독파일", Color.Purple);
                }
                else if (inRight)
                {
                    SetItemStatus(rightDict[name], "단독파일", Color.Purple);
                }
            }
        }
    }
}
