using Newtonsoft.Json;
using Newtonsoft.Json.Linq;



namespace FuZhu_Data
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            // panel1.Paint += panel1_Paint;
        }

        public async Task GetDataAsync(int pageNum)
        {
            // 目标URL
            string url = "https://book.heao.com.cn/prod-api/school/plan/selectSchoolPlan?pcdm=I&kldm=1&pageSize=5&pageNum=" + pageNum;
            // 使用 HttpClient 发送 GET 请求
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // 添加 Authorization 头
                    string token = textBox1.Text; // 替换为实际的 token
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                    // 获取响应
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode(); // 确保请求成功
                    // 读取响应内容
                    string jsonResponse = await response.Content.ReadAsStringAsync();

                    // 将字符串转换为 JObject
                    JObject jsonObject = JObject.Parse(jsonResponse);
                    // 获取 rows 数组
                    JArray rows = (JArray)jsonObject["rows"];
                    // 遍历 rows
                    Console.WriteLine("Rows:");
                    foreach (var row in rows)
                    {
                        //  string id = (string)row["yxdh"];
                        string yxdh = (string)row["yxdh"];   //院校代号
                        string name = (string)row["schoolName"];  //院校名称
                        string schoolJhrs = (string)row["schoolJhrs"];//共多少人

                        string kskmyqzw = (string)row["kskmyqzw"];  //选考科目要求及专业备注

                        string zyfx = (string)row["zyfx"];    //专业方向
                        string zymc = (string)row["zymc"];   //专业名称
                        string jhrs = (string)row["jhrs"];   //计划人数

                        string zyzhmc = (string)row["zyzhmc"];
                        string xznx = (string)row["xznx"];    //学制/年

                        string zyzh = (string)row["zyzh"];  //专业组代号
                        string zyzJhrs = (string)row["zyzJhrs"];  //计划数
                        string zydh = (string)row["zydh"];   //专业代号

                        string sfbz = (string)row["sfbz"];   //学费标准
                        string kskmyq = (string)row["kskmyq"];


                        listBox1.Items.Add($"ID: {yxdh}, Name: {name}");
                        Common.WriteTxt($"ID: {yxdh}, Name: {name}");
                    }

                }
                catch (HttpRequestException ex)
                {

                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 1; i < 3; i++)
            {
                GetDataAsync(i);
            }

        }


        // 省市县数据结构


        // 1. 修改 AreaNode，增加 Code 属性
        public class AreaNode
        {
            public string Name { get; set; }
            public string Code { get; set; }
            public List<AreaNode> Children { get; set; } = new List<AreaNode>();
        }

        // 2. 修改 LoadAreaTree，给每个节点赋予行政代码，并将代码存入 TreeNode 的 Tag 属性
        private void LoadAreaTree()
        {
            // 示例数据：河南、河北，省-市-县三级，带行政代码
            var rootAreas = new List<AreaNode>
            {
new AreaNode
{
    Name = "河南省",
    Code = "41",
    Children = new List<AreaNode>
    {
        new AreaNode
        {
            Name = "郑州市",
            Code = "4101",
            Children = new List<AreaNode>
            {
                new AreaNode { Name = "中原区", Code = "410102" },
                new AreaNode { Name = "金水区", Code = "410105" },
                new AreaNode { Name = "二七区", Code = "410103" }
            }
        },
        new AreaNode
        {
            Name = "洛阳市",
            Code = "4103",
            Children = new List<AreaNode>
            {
                new AreaNode { Name = "涧西区", Code = "410305" },
                new AreaNode { Name = "老城区", Code = "410302" }
            }
        },
        new AreaNode
        {
            Name = "焦作市",
            Code = "4108",
            Children = new List<AreaNode>
            {
                new AreaNode { Name = "解放区", Code = "410802" },
                new AreaNode { Name = "中站区", Code = "410803" },
                new AreaNode { Name = "马村区", Code = "410804" },
                new AreaNode { Name = "山阳区", Code = "410811" },
                new AreaNode { Name = "修武县", Code = "410821" },
                new AreaNode { Name = "博爱县", Code = "410822" },
                new AreaNode { Name = "武陟县", Code = "410823" },
                new AreaNode { Name = "温县", Code = "410825" },
                new AreaNode { Name = "沁阳市", Code = "410882" },
                new AreaNode { Name = "孟州市", Code = "410883" }
            }
        }
    }
},
                new AreaNode
                {
                    Name = "河北省",
                    Code = "13",
                    Children = new List<AreaNode>
                    {
                        new AreaNode
                        {
                            Name = "石家庄市",
                            Code = "1301",
                            Children = new List<AreaNode>
                            {
                                new AreaNode { Name = "长安区", Code = "130102" },
                                new AreaNode { Name = "桥西区", Code = "130104" }
                            }
                        },
                        new AreaNode
                        {
                            Name = "唐山市",
                            Code = "1302",
                            Children = new List<AreaNode>
                            {
                                new AreaNode { Name = "路南区", Code = "130202" },
                                new AreaNode { Name = "路北区", Code = "130203" }
                            }
                        }
                    }
                }
            };

            textBox3.Text = JsonConvert.SerializeObject(rootAreas);

            treeView1.Nodes.Clear();
            foreach (var province in rootAreas)
            {
                TreeNode provinceNode = new TreeNode(province.Name);
                provinceNode.Tag = province; // 存储 AreaNode 对象
                AddChildNodes(provinceNode, province.Children);
                treeView1.Nodes.Add(provinceNode);
            }
            treeView1.ExpandAll();
        }

        // 3. 修改 AddChildNodes，设置 Tag
        private void AddChildNodes(TreeNode parentNode, List<AreaNode> children)
        {
            foreach (var child in children)
            {
                TreeNode childNode = new TreeNode(child.Name);
                childNode.Tag = child; // 存储 AreaNode 对象
                parentNode.Nodes.Add(childNode);
                if (child.Children != null && child.Children.Count > 0)
                {
                    AddChildNodes(childNode, child.Children);
                }
            }
        }

        // 4. 订阅 treeView1 的 AfterSelect 事件
        private void Form1_Load(object sender, EventArgs e)
        {
            treeView1.AfterSelect += treeView1_AfterSelect;
        }

        // 5. 实现 treeView1_AfterSelect 事件处理，显示代码和名称
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag is AreaNode area)
            {
                label4.Text = $"代码: {area.Code}，名称: {area.Name}";
            }
            else
            {
                label4.Text = "";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoadAreaTree();
        }

        // 修改 DrawFishboneDiagram 方法，支持每个一级鱼骨分支有2或3个二级鱼骨
        private void DrawFishboneDiagram(Graphics g, Rectangle bounds)
        {
            int centerY = bounds.Top + bounds.Height / 2;
            int startX = bounds.Left + 40;
            int endX = bounds.Right - 40;
            Pen mainPen = new Pen(Color.Black, 3);
            g.DrawLine(mainPen, startX, centerY, endX, centerY);

            // 鱼头
            Point[] head = new Point[]
            {
                new Point(endX, centerY),
                new Point(endX - 30, centerY - 20),
                new Point(endX - 30, centerY + 20)
            };
            g.FillPolygon(Brushes.Gray, head);

            int branchCount = 6;
            int branchLength = 60;
            int branchGap = (endX - startX - 40) / (branchCount - 1);
            Font font = new Font("微软雅黑", 10, FontStyle.Bold);
            Brush textBrush = Brushes.Blue;

            // 每个一级分支的二级分支数量（2或3个）
            int[] subBranchCounts = new int[] { 2, 3, 2, 3, 2, 3 };

            for (int i = 0; i < branchCount; i++)
            {
                int x = startX + i * branchGap;
                // 上分支
                Point upStart = new Point(x, centerY);
                Point upEnd = new Point(x - branchLength / 2, centerY - branchLength);
                g.DrawLine(Pens.Black, upStart, upEnd);
                // 下分支
                Point downStart = new Point(x, centerY);
                Point downEnd = new Point(x - branchLength / 2, centerY + branchLength);
                g.DrawLine(Pens.Black, downStart, downEnd);

                // 一级分支序号
                string upLabel = $"上{i + 1}";
                string downLabel = $"下{i + 1}";
                SizeF upSize = g.MeasureString(upLabel, font);
                SizeF downSize = g.MeasureString(downLabel, font);
                g.DrawString(upLabel, font, textBrush, upEnd.X - upSize.Width - 5, upEnd.Y - upSize.Height / 2);
                g.DrawString(downLabel, font, textBrush, downEnd.X - downSize.Width - 5, downEnd.Y - downSize.Height / 2);

                // 二级鱼骨（每个一级分支上画2或3个二级分支）
                int subBranchCount = subBranchCounts[i % subBranchCounts.Length];
                int subBranchLength = 30;
                int subBranchGap = branchLength / (subBranchCount + 1);
                for (int j = 0; j < subBranchCount; j++)
                {
                    // 上二级分支
                    int subUpX = upEnd.X - (j + 1) * subBranchGap;
                    int subUpY = upEnd.Y - (j + 1) * subBranchGap;
                    Point subUpStart = new Point(upEnd.X, upEnd.Y);
                    Point subUpEnd = new Point(subUpX, subUpY);
                    g.DrawLine(Pens.Gray, subUpStart, subUpEnd);
                    string subUpLabel = $"上{i + 1}-{j + 1}";
                    SizeF subUpSize = g.MeasureString(subUpLabel, font);
                    g.DrawString(subUpLabel, font, Brushes.DarkGreen, subUpEnd.X - subUpSize.Width - 2, subUpEnd.Y - subUpSize.Height / 2);

                    // 下二级分支
                    int subDownX = downEnd.X - (j + 1) * subBranchGap;
                    int subDownY = downEnd.Y + (j + 1) * subBranchGap;
                    Point subDownStart = new Point(downEnd.X, downEnd.Y);
                    Point subDownEnd = new Point(subDownX, subDownY);
                    g.DrawLine(Pens.Gray, subDownStart, subDownEnd);
                    string subDownLabel = $"下{i + 1}-{j + 1}";
                    SizeF subDownSize = g.MeasureString(subDownLabel, font);
                    g.DrawString(subDownLabel, font, Brushes.DarkGreen, subDownEnd.X - subDownSize.Width - 2, subDownEnd.Y - subDownSize.Height / 2);
                }
            }
        }

        // 2. 订阅 panel1 的 Paint 事件
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            DrawFishboneDiagram(e.Graphics, panel1.ClientRectangle);
        }

        // 1. 移除构造函数中的 panel1.Paint 事件绑定
      

        // 2. 添加 button3_Click 事件处理，点击时绑定 Paint 并刷新 panel1
        private void button3_Click(object sender, EventArgs e)
        {
            panel1.Paint -= panel1_Paint; // 防止重复绑定
            panel1.Paint += panel1_Paint;
            panel1.Invalidate(); // 触发重绘

        }
      
    }
}
