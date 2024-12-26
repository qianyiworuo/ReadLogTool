using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using static System.Windows.Forms.DataFormats;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace ReadEElog
{
    public partial class FrmReadEElog : Form
    {
        private static string sDefaultSearchKey;
        private static string? sGoodJobs;
        private static Dictionary<string, string>? dicConfig;
        public FrmReadEElog()
        {
            InitializeComponent();
        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.InitialDirectory = "c:\\";
                    openFileDialog.Filter = "所有文件 (*.*)|*.*"; // 可以设置文件类型过滤
                    openFileDialog.FilterIndex = 1;
                    openFileDialog.RestoreDirectory = true;

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        // 获取选中的文件路径
                        string filePath = openFileDialog.FileName;
                        txtFilePath.Text = filePath; // 将文件路径显示在文本框中
                        // 保存文件路径到 config.json
                        SaveFilePathToConfig(filePath);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SaveFilePathToConfig(string filePath)
        {
            // 检查 config.json 文件存在
            if (File.Exists("config.json"))
            {
                // 读取 config.json 文件内容
                string json = File.ReadAllText("config.json");

                // 将 JSON 字符串反序列化为字典
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var config = JsonSerializer.Deserialize<Dictionary<string, object>>(json, options);

                // 更新 FilePath 值
                if (config != null && config.ContainsKey("FilePath"))
                {
                    config["FilePath"] = filePath; // 更新 FilePath 值
                }

                // 保存更新后的字典到 config.json 文件
                string output = JsonSerializer.Serialize(config, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText("config.json", output);
            }
        }

        private void FrmReadEElog_Load(object sender, EventArgs e)
        {
            // 初始化 lvMissions 控件
            InitUi();
            // 加载文件路径
            LoadFilePathFromConfig();
        }

        //初始化lvMissions控件
        private void InitUi()
        {
            ColumnHeader ch = new ColumnHeader();
            ch = lvMissions.Columns.Add("任务序号", 100, HorizontalAlignment.Left);
            ch = lvMissions.Columns.Add("任务名称", 200, HorizontalAlignment.Left);

            ch = lvMissionTime.Columns.Add("开始时间", 160, HorizontalAlignment.Left);
            ch = lvMissionTime.Columns.Add("结束时间", 160, HorizontalAlignment.Left);

            // 设置dtpStart格式为年-月-日 时:分
            dtpStart.Format = DateTimePickerFormat.Custom;
            dtpStart.CustomFormat = "yyyy-MM-dd HH:mm";

        }

        private void LoadFilePathFromConfig()
        {
            try
            {
                // 检查 config.json 文件是否存在
                if (File.Exists("config.json"))
                {
                    // 存在则读取配置
                    ReadConfig();
                }
                else
                {
                    // 不存在时创建默认配置
                    CreateDefaultConfig();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载配置时发生错误: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CreateDefaultConfig()
        {
            var config = new
            {
                FilePath = "C:\\Users\\***\\AppData\\Local\\Warframe\\EE.log",
                SearchString = "EidolonJobBoard.lua: Selected job with jobInfo:",
                DefaultSearchKey = "\"jobStages\":",
                JobStages = new[]
                {
                    new
                    {
                        DynamicAssassinate = "刺杀",
                        DynamicCapture = "捕获",
                        DynamicExterminate = "地上消灭敌人",
                        DynamicCaveExterminate = "地下消灭敌人",
                        DynamicHijack = "无人机",
                        DynamicRescue = "俘虏",
                        HiddenResourceCaches = "地上储存箱",
                        HiddenResourceCachesCave = "地下储存箱",
                        DynamicDefend = "解放营地",
                        DynamicResourceTheft = "重甲金库",
                        DynamicSabotage = "破坏设施"
                    }
                },
                GoodJobs = "DynamicAssassinate,DynamicCapture,DynamicHijack,DynamicRescue,HiddenResourceCaches,HiddenResourceCachesCave",
                StartTime = "2024-12-26 01:10" // 默认显示2024-12-26 01:10
            };

            // 将配置对象序列化为 JSON 字符串并保存
            SaveConfigToJson(config);
        }

        private void SaveConfigToJson(object config)
        {
            try
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                string json = JsonSerializer.Serialize(config, new JsonSerializerOptions
                {
                    WriteIndented = true,
                    Encoder = JavaScriptEncoder.Create(UnicodeRanges.All) // 允许所有Unicode字符
                });

                File.WriteAllText("config.json", json);
                ReadConfig();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"保存配置时发生错误: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ReadConfig()
        {
            // 读取 config.json 文件内容
            string json = File.ReadAllText("config.json");

            // 反序列化 JSON 字符串为 JsonDocument
            using (JsonDocument doc = JsonDocument.Parse(json))
            {
                JsonElement root = doc.RootElement;

                // 尝试获取 FilePath 属性
                if (root.TryGetProperty("FilePath", out JsonElement filePathElement))
                {
                    // 将 FilePath 值显示到文本框中
                    txtFilePath.Text = filePathElement.GetString();
                }
                //尝试获取SearchString属性
                if (root.TryGetProperty("SearchString", out JsonElement searchStringElement))
                {
                    //将SearchString值显示到文本框中
                    txtSearchString.Text = searchStringElement.GetString();
                }
                //尝试获取DefaultSearchKey属性
                if (root.TryGetProperty("DefaultSearchKey", out JsonElement defaultSearchKeyElement))
                {
                    if (!string.IsNullOrEmpty(defaultSearchKeyElement.GetString()))
                    {
                        sDefaultSearchKey = defaultSearchKeyElement.GetString();
                    }
                    else
                    {
                        throw new Exception("DefaultSearchKey属性值为空");
                    }
                }

                // 尝试获取 JobStages 属性
                if (root.TryGetProperty("JobStages", out JsonElement jobStagesElement) && jobStagesElement.ValueKind == JsonValueKind.Array)
                {
                    dicConfig = new Dictionary<string, string>();
                    foreach (var jobStage in jobStagesElement.EnumerateArray())
                    {
                        // 遍历每个 jobStage，并提取其键值对
                        foreach (var property in jobStage.EnumerateObject())
                        {
                            // 遍历每个键值对，并提取其键和值
                            string key = property.Name;
                            string? value = property.Value.GetString();
                            dicConfig.Add(key, value);
                        }
                    }
                }
                //尝试获取GoodJobs属性
                if (root.TryGetProperty("GoodJobs", out JsonElement goodJobsElement))
                {
                    // 将 JsonElement 转换为字符串
                    //string goodJobsJson = goodJobsElement.GetRawText();
                    sGoodJobs = goodJobsElement.GetString();
                }
                // 尝试获取StartTime属性
                if (root.TryGetProperty("StartTime", out JsonElement startTimeElement))
                {
                    // 将StartTime值显示到DateTimePicker中
                    // 指定日期时间的格式
                    string format = "yyyy-MM-dd HH:mm";
                    string ?startTimeString = startTimeElement.GetString();
                    if (DateTime.TryParseExact(startTimeString, format,System.Globalization.CultureInfo.InvariantCulture,System.Globalization.DateTimeStyles.None, out DateTime startTime))
                    {
                        dtpStart.Value = startTime;
                    }
                }
            }
        }

        private void btnSearchMission_Click(object sender, EventArgs e)
        {
            SearchMission();
        }

        private void SearchMission()
        {
            try
            {
                // 获取文件路径
                string filePath = txtFilePath.Text;
                // 获取搜索字符串
                string searchString = txtSearchString.Text;
                // 读取文件
                List<string> jobStages = ReadMission(filePath, searchString);
                // 显示结果
                if (jobStages != null)
                {
                    lvMissions.Items.Clear();
                    int index = 0;
                    foreach (var stage in jobStages)
                    {
                        // 添加到列表视图中
                        ListViewItem lvItem = new ListViewItem();
                        lvItem.Text = (1 + index++).ToString();
                        lvItem.SubItems.Add(stage);
                        lvMissions.Items.Add(lvItem);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private List<string> ReadMission(string filePath, string searchString)
        {
            try
            {
                using var file = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                long fileLength = file.Length;
                StringBuilder jsonContentBuilder = new StringBuilder();
                bool foundSearchString = false;

                byte[] buffer = new byte[1024]; // 使用更大的缓冲区
                long position = fileLength; // 从文件末尾开始

                while (position > 0)
                {
                    position -= buffer.Length;
                    if (position < 0) position = 0; // 确保不越界
                    // 定位到文件位置
                    file.Seek(position, SeekOrigin.Begin);
                    // 读取文件内容
                    int bytesRead = file.Read(buffer, 0, Math.Min(buffer.Length, (int)(file.Length - position)));
                    if (bytesRead <= 0) continue; // 若未读取到字节, 继续循环

                    string chunk = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                    if (ProcessChunk(chunk, searchString, ref foundSearchString, ref jsonContentBuilder))
                    {
                        if (foundSearchString && jsonContentBuilder.ToString().Contains("{"))
                        {
                            return ExtractJsonUntilClosingBracket(file, ref position, jsonContentBuilder);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return null;
        }

        private bool ProcessChunk(string chunk, string searchString, ref bool foundSearchString, ref StringBuilder jsonContentBuilder)
        {
            if (foundSearchString)
            {
                int openBracketIndex = chunk.LastIndexOf('{');
                if (openBracketIndex != -1)
                {
                    jsonContentBuilder.Append(chunk.Substring(openBracketIndex));
                    return true;
                }
            }
            else
            {
                int searchIndex = chunk.LastIndexOf(searchString);
                if (searchIndex != -1)
                {
                    foundSearchString = true;
                    jsonContentBuilder.Append(chunk.Substring(searchIndex));
                    return true;
                }
            }
            jsonContentBuilder.Append(chunk);
            return false;
        }

        private List<string> ExtractJsonUntilClosingBracket(FileStream file, ref long position, StringBuilder jsonContentBuilder)
        {
            byte[] buffer = new byte[1024]; // 更大的缓冲区

            while (position > 0)
            {
                position -= buffer.Length;
                if (position < 0) position = 0; // 确保不越界

                file.Seek(position, SeekOrigin.Begin);
                int bytesRead = file.Read(buffer, 0, Math.Min(buffer.Length, (int)(file.Length - position)));
                string chunk = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                int closeBracketIndex = chunk.LastIndexOf('}');
                if (closeBracketIndex != -1)
                {
                    jsonContentBuilder.Append(chunk.Substring(0, closeBracketIndex + 1));
                    return ExtractJobStagesFromJson(jsonContentBuilder.ToString());
                }
                jsonContentBuilder.Append(chunk);
            }

            return null;
        }

        private List<string> ExtractJobStagesFromJson(string jsonString)
        {
            List<string> jobStages = new List<string>();

            // 查找起始索引
            int startIndex = jsonString.IndexOf(sDefaultSearchKey);
            if (startIndex == -1) return null;

            // 查找开和闭括号
            int openBracketIndex = jsonString.IndexOf('[', startIndex);
            int closeBracketIndex = jsonString.IndexOf(']', openBracketIndex);

            // 检查括号是否存在
            if (openBracketIndex == -1 || closeBracketIndex == -1) return null;

            // 提取任务阶段 JSON 字符串
            string jobStagesJson = jsonString.Substring(openBracketIndex + 1, closeBracketIndex - openBracketIndex - 1).Trim();
            string[] stages = jobStagesJson.Split(',');

            foreach (var stage in stages)
            {
                // 提取阶段名称
                int lastSlashIndex = stage.LastIndexOf('/');
                if (lastSlashIndex != -1)
                {
                    // 截取阶段名称并去除前后的引号和斜杠
                    string result = stage.Substring(lastSlashIndex + 1).Trim('"', '/');
                    jobStages.Add(result);
                }
            }

            // 检查任务阶段与好任务的关系
            UpdateResultLabel(jobStages);

            // 替换阶段名称为中文
            ReplaceJobStagesWithChineseNames(jobStages);

            return jobStages;
        }

        private void UpdateResultLabel(List<string> jobStages)
        {
            if (string.IsNullOrEmpty(sGoodJobs)) return;

            bool allGoodJobsExist = jobStages.All(s => sGoodJobs.Contains(s));
            lblResult.Text = allGoodJobsExist ? "卡成功" : "卡失败";
            lblResult.ForeColor = allGoodJobsExist ? Color.Green : Color.Red;
        }

        private void ReplaceJobStagesWithChineseNames(List<string> jobStages)
        {
            if (dicConfig == null) return;

            for (int i = 0; i < jobStages.Count; i++)
            {
                if (dicConfig.TryGetValue(jobStages[i], out string chineseName))
                {
                    jobStages[i] = chineseName;
                }
            }
        }

        private void btnMissionTime_Click(object sender, EventArgs e)
        {
            GetMissionTime();
            SaveStartTimeToConfig();
        }

        private void GetMissionTime()
        {
            // 清空结果显示
            lvMissionTime.Items.Clear();

            // 获取起始日期时间
            DateTime startTime = dtpStart.Value; // 从DateTimePicker中获取值

            // 获取结束日期
            DateTime endDate = dtpTarget.Value.Date; // 从DateTimePicker中获取结束日期部分
            DateTime queryDate = endDate.AddDays(1); // 计算结束日期为第二天的开始

            List<DateTime> refreshTimes = new List<DateTime>();
            TimeSpan interval = TimeSpan.FromMinutes(150); // 150分钟的时间间隔

            // 计算刷新时刻，直到结束日期
            while (startTime < queryDate)
            {
                if (startTime.Date == endDate.Date)
                {
                    refreshTimes.Add(startTime);
                }
                startTime = startTime.Add(interval); // 添加时间间隔
            }

            // 显示特定查询日期的刷新时刻
            foreach (var time in refreshTimes)
            {
                if (time.Date == endDate) // 显示指定结束日期的任务刷新列表
                {
                    DateTime endTime = time.Add(interval); // 计算结束时间
                    ListViewItem lvItem = new ListViewItem();
                    lvItem.Text = time.ToString("yyyy-MM-dd HH:mm");
                    lvItem.SubItems.Add(endTime.ToString("yyyy-MM-dd HH:mm"));
                    lvMissionTime.Items.Add(lvItem);
                }
            }
            //判断当前时间是否在lvMissionTime之间
            DateTime nowTime = DateTime.Now;
            foreach (ListViewItem lvItem in lvMissionTime.Items)
            {
                DateTime dtStartTime = DateTime.Parse(lvItem.SubItems[0].Text);
                DateTime dtEndTime = DateTime.Parse(lvItem.SubItems[1].Text);
                if (nowTime >= dtStartTime && nowTime <= dtEndTime)
                {
                    //计算当前时间距离刷新时刻的剩余时间
                    TimeSpan timeSpan = dtEndTime - nowTime;
                    int hours = timeSpan.Hours;
                    int minutes = timeSpan.Minutes;
                    string timeLeft = string.Format("{0:D2}分", hours * 60 + minutes);
                    lblRefreshTime.Text = "任务还有:" + timeLeft;
                    break;
                }
            }

            if (lvMissionTime.Items.Count == 0)
            {
                MessageBox.Show("在指定日期没有找到任何刷新时刻。", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void SaveStartTimeToConfig()
        {
            // 检查 config.json 文件存在
            if (File.Exists("config.json"))
            {
                // 读取 config.json 文件内容
                string json = File.ReadAllText("config.json");

                // 将 JSON 字符串反序列化为字典
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var config = JsonSerializer.Deserialize<Dictionary<string, object>>(json, options);

                // 更新 StartTime 值
                if (config != null && config.ContainsKey("StartTime"))
                {
                    // 更新 StartTime 值
                    config["StartTime"] = lvMissionTime.Items.Count > 0 ? lvMissionTime.Items[0].SubItems[0].Text : dtpStart.Value.ToString("yyyy-MM-dd HH:mm");
                }

                // 保存更新后的字典到 config.json 文件
                string output = JsonSerializer.Serialize(config, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText("config.json", output);
            }
        }
    }
}

