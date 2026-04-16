using System.IO;
using System.Linq;
using System.Threading.Tasks;
// placeholder
using System.Drawing;

namespace FileCompare
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCopyFromLeft_Click(object sender, EventArgs e)
        {
            CopySelected(fromLeft: true);
        }

        private void btnCopyFromRight_Click(object sender, EventArgs e)
        {
            CopySelected(fromLeft: false);
        }

        private void CopySelected(bool fromLeft)
        {
            var srcLv = fromLeft ? lvwLeftDir : lvwRightDir;
            var dstLv = fromLeft ? lvwRightDir : lvwLeftDir;
            var srcDir = fromLeft ? txtLeftDir.Text : txtRightDir.Text;
            var dstDir = fromLeft ? txtRightDir.Text : txtLeftDir.Text;

            if (string.IsNullOrWhiteSpace(srcDir) || !Directory.Exists(srcDir))
            {
                MessageBox.Show("복사할 폴더가 설정되어 있지 않습니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(dstDir) || !Directory.Exists(dstDir))
            {
                MessageBox.Show("대상 폴더가 설정되어 있지 않습니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (srcLv.SelectedItems.Count == 0)
            {
                MessageBox.Show("복사할 파일을 선택하세요.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            foreach (ListViewItem item in srcLv.SelectedItems)
            {
                var name = item.Text;
                var srcFull = Path.Combine(srcDir, name);
                var dstFull = Path.Combine(dstDir, name);

                if (!File.Exists(srcFull))
                {
                    // skip directories or missing files
                    continue;
                }

                var dstItem = dstLv.Items.Cast<ListViewItem>().FirstOrDefault(i => string.Equals(i.Text, name, StringComparison.CurrentCultureIgnoreCase));

                var srcColor = item.ForeColor;
                var dstColor = dstItem?.ForeColor ?? Color.Transparent;

                // If destination exists and is gray and source is red => ask confirmation
                if (dstItem != null && dstColor == Color.Gray && srcColor == Color.Red)
                {
                    var dr = MessageBox.Show($"대상에 있는 파일은 오래된 파일(회색)입니다. 덮어씌우시겠습니까?\n{dstFull}", "경고", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dr != DialogResult.Yes) continue;
                }

                // perform copy (overwrite)
                try
                {
                    File.Copy(srcFull, dstFull, true);
                    // ensure write time matches
                    var srcTime = File.GetLastWriteTime(srcFull);
                    File.SetLastWriteTime(dstFull, srcTime);

                    // update or add destination item
                    var fi = new FileInfo(srcFull);
                    var modified = fi.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss");
                    string sizeText = fi.Length < 1024 ? "<1 KB" : string.Format("{0:N0} KB", Math.Round(fi.Length / 1024.0));

                    if (dstItem == null)
                    {
                        var newItem = new ListViewItem(name);
                        newItem.SubItems.Add(modified);
                        newItem.SubItems.Add(sizeText);
                        newItem.SubItems.Add(string.Empty);
                        newItem.Tag = fi.LastWriteTime.Ticks;
                        dstLv.Items.Add(newItem);
                        dstItem = newItem;
                    }
                    else
                    {
                        dstItem.SubItems[1].Text = modified;
                        dstItem.SubItems[2].Text = sizeText;
                        dstItem.Tag = fi.LastWriteTime.Ticks;
                    }

              
                    item.SubItems[1].Text = modified;
                    item.SubItems[2].Text = sizeText;
                    item.Tag = fi.LastWriteTime.Ticks;

     
                    SetItemStatus(item, "동일", Color.Black);
                    SetItemStatus(dstItem, "동일", Color.Black);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"파일 복사 중 오류가 발생했습니다: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
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
