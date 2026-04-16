using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

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
                                    // store directory modified ticks in Tag for comparison
                                    lvi.Tag = dirInfo.LastWriteTime.Ticks;
                                    // mark directory visually with prefix (emoji + space)
                                    lvi.Text = "📁 " + lvi.Text;
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
                                    // mark as file (optional)
                                    // no prefix for files
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

        private string GetRawName(string displayedText)
        {
            if (string.IsNullOrEmpty(displayedText)) return displayedText;
            var folderPrefix = "📁 ";
            if (displayedText.StartsWith(folderPrefix))
            {
                return displayedText.Substring(folderPrefix.Length);
            }
            return displayedText;
        }

        private void CopyDirectoryRecursive(string sourceDir, string targetDir)
        {
            // create target directory if not exists
            try
            {
                Directory.CreateDirectory(targetDir);
            }
            catch (Exception ex)
            {
                throw new IOException($"대상 디렉터리를 만들 수 없습니다: {ex.Message}");
            }

            // copy files
            string[] files = Array.Empty<string>();
            try
            {
                files = Directory.GetFiles(sourceDir);
            }
            catch (UnauthorizedAccessException) { return; }
            catch (Exception ex)
            {
                throw new IOException($"소스 디렉터리 파일을 열 수 없습니다: {ex.Message}");
            }

            foreach (var filePath in files)
            {
                var fileName = Path.GetFileName(filePath);
                var destFile = Path.Combine(targetDir, fileName);
                try
                {
                    if (File.Exists(destFile))
                    {
                        var srcTime = File.GetLastWriteTime(filePath);
                        var dstTime = File.GetLastWriteTime(destFile);
                        if (dstTime > srcTime)
                        {
                            var dr = MessageBox.Show($"대상 파일이 더 최신입니다. 덮어씌우시겠습니까?\n{destFile}", "경고", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            if (dr != DialogResult.Yes) continue;
                        }
                    }
                    File.Copy(filePath, destFile, true);
                    File.SetLastWriteTime(destFile, File.GetLastWriteTime(filePath));
                }
                catch (UnauthorizedAccessException)
                {
                    // skip file
                    continue;
                }
                catch (Exception ex)
                {
                    // continue on error
                    MessageBox.Show($"파일 복사 중 오류가 발생했습니다: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    continue;
                }
            }

            // recurse directories
            string[] dirs = Array.Empty<string>();
            try
            {
                dirs = Directory.GetDirectories(sourceDir);
            }
            catch (UnauthorizedAccessException) { return; }
            catch (Exception ex)
            {
                throw new IOException($"하위 디렉터리를 열 수 없습니다: {ex.Message}");
            }

            foreach (var dir in dirs)
            {
                var dirName = Path.GetFileName(dir);
                var destSub = Path.Combine(targetDir, dirName);
                try
                {
                    CopyDirectoryRecursive(dir, destSub);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"하위 폴더 복사 중 오류가 발생했습니다: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    continue;
                }
            }
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
                MessageBox.Show("복사할 파일 또는 폴더를 선택하세요.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            foreach (ListViewItem item in srcLv.SelectedItems)
            {
                var rawName = GetRawName(item.Text);
                var srcFull = Path.Combine(srcDir, rawName);
                var dstFull = Path.Combine(dstDir, rawName);

                try
                {
                    // Directory copy
                    if (Directory.Exists(srcFull))
                    {
                        // cannot overwrite file with directory
                        if (File.Exists(dstFull))
                        {
                            MessageBox.Show($"대상에 동일 이름의 파일이 있어 폴더를 복사할 수 없습니다: {dstFull}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            continue;
                        }

                        if (Directory.Exists(dstFull))
                        {
                            var srcDirInfo = new DirectoryInfo(srcFull);
                            var dstDirInfo = new DirectoryInfo(dstFull);
                            if (dstDirInfo.LastWriteTime > srcDirInfo.LastWriteTime)
                            {
                                var dr = MessageBox.Show($"대상 폴더에 더 최신 파일이 있습니다. 덮어씌우시겠습니까?\n{dstFull}", "경고", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                if (dr != DialogResult.Yes) continue;
                            }
                        }

                        try
                        {
                            CopyDirectoryRecursive(srcFull, dstFull);
                            // update destination list item
                            var dstItem = dstLv.Items.Cast<ListViewItem>().FirstOrDefault(i => string.Equals(GetRawName(i.Text), rawName, StringComparison.CurrentCultureIgnoreCase));
                            if (dstItem == null)
                            {
                                var newItem = new ListViewItem("📁 " + rawName);
                                newItem.SubItems.Add(Directory.GetLastWriteTime(dstFull).ToString("yyyy-MM-dd HH:mm:ss"));
                                newItem.SubItems.Add("<DIR>");
                                newItem.SubItems.Add(string.Empty);
                                newItem.Tag = Directory.GetLastWriteTime(dstFull).Ticks;
                                dstLv.Items.Add(newItem);
                                dstItem = newItem;
                            }
                            SetItemStatus(item, "동일", Color.Black);
                            SetItemStatus(dstItem, "동일", Color.Black);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"폴더 복사 중 오류가 발생했습니다: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        continue;
                    }

                    // File copy
                    if (!File.Exists(srcFull)) continue;

                    var dstItem2 = dstLv.Items.Cast<ListViewItem>().FirstOrDefault(i => string.Equals(GetRawName(i.Text), rawName, StringComparison.CurrentCultureIgnoreCase));
                    var srcColor = item.ForeColor;
                    var dstColor = dstItem2?.ForeColor ?? Color.Transparent;

                    // red/gray conflict: only ask when overwriting a red (newer) file with a gray (older) file
                    if (dstItem2 != null && dstColor == Color.Red && srcColor == Color.Gray)
                    {
                        var dr = MessageBox.Show($"대상 파일이 더 새 파일(빨간색)입니다. 덮어씌우시겠습니까?\n{dstFull}", "경고", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (dr != DialogResult.Yes) continue;
                    }

                    // date check
                    if (File.Exists(dstFull))
                    {
                        var srcTime = File.GetLastWriteTime(srcFull);
                        var dstTime = File.GetLastWriteTime(dstFull);
                        if (dstTime > srcTime)
                        {
                            var dr2 = MessageBox.Show($"대상 파일이 더 최신입니다. 덮어씌우시겠습니까?\n{dstFull}", "경고", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            if (dr2 != DialogResult.Yes) continue;
                        }
                    }

                    File.Copy(srcFull, dstFull, true);
                    var srcTime2 = File.GetLastWriteTime(srcFull);
                    File.SetLastWriteTime(dstFull, srcTime2);

                    var fi = new FileInfo(srcFull);
                    var modified = fi.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss");
                    string sizeText = fi.Length < 1024 ? "<1 KB" : string.Format("{0:N0} KB", Math.Round(fi.Length / 1024.0));

                    if (dstItem2 == null)
                    {
                        var newItem = new ListViewItem(rawName);
                        newItem.SubItems.Add(modified);
                        newItem.SubItems.Add(sizeText);
                        newItem.SubItems.Add(string.Empty);
                        newItem.Tag = fi.LastWriteTime.Ticks;
                        dstLv.Items.Add(newItem);
                        dstItem2 = newItem;
                    }
                    else
                    {
                        dstItem2.SubItems[1].Text = modified;
                        dstItem2.SubItems[2].Text = sizeText;
                        dstItem2.Tag = fi.LastWriteTime.Ticks;
                    }

                    item.SubItems[1].Text = modified;
                    item.SubItems[2].Text = sizeText;
                    item.Tag = fi.LastWriteTime.Ticks;

                    SetItemStatus(item, "동일", Color.Black);
                    SetItemStatus(dstItem2, "동일", Color.Black);
                }
                catch (UnauthorizedAccessException uaex)
                {
                    MessageBox.Show($"권한 오류: {uaex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (PathTooLongException ptex)
                {
                    MessageBox.Show($"경로가 너무 깁니다: {ptex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"복사 중 오류가 발생했습니다: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
