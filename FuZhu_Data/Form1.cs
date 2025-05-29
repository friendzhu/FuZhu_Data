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
            // Ŀ��URL
            string url = "https://book.heao.com.cn/prod-api/school/plan/selectSchoolPlan?pcdm=I&kldm=1&pageSize=5&pageNum=" + pageNum;
            // ʹ�� HttpClient ���� GET ����
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // ��� Authorization ͷ
                    string token = textBox1.Text; // �滻Ϊʵ�ʵ� token
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                    // ��ȡ��Ӧ
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode(); // ȷ������ɹ�
                    // ��ȡ��Ӧ����
                    string jsonResponse = await response.Content.ReadAsStringAsync();

                    // ���ַ���ת��Ϊ JObject
                    JObject jsonObject = JObject.Parse(jsonResponse);
                    // ��ȡ rows ����
                    JArray rows = (JArray)jsonObject["rows"];
                    // ���� rows
                    Console.WriteLine("Rows:");
                    foreach (var row in rows)
                    {
                        //  string id = (string)row["yxdh"];
                        string yxdh = (string)row["yxdh"];   //ԺУ����
                        string name = (string)row["schoolName"];  //ԺУ����
                        string schoolJhrs = (string)row["schoolJhrs"];//��������

                        string kskmyqzw = (string)row["kskmyqzw"];  //ѡ����ĿҪ��רҵ��ע

                        string zyfx = (string)row["zyfx"];    //רҵ����
                        string zymc = (string)row["zymc"];   //רҵ����
                        string jhrs = (string)row["jhrs"];   //�ƻ�����

                        string zyzhmc = (string)row["zyzhmc"];
                        string xznx = (string)row["xznx"];    //ѧ��/��

                        string zyzh = (string)row["zyzh"];  //רҵ�����
                        string zyzJhrs = (string)row["zyzJhrs"];  //�ƻ���
                        string zydh = (string)row["zydh"];   //רҵ����

                        string sfbz = (string)row["sfbz"];   //ѧ�ѱ�׼
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


        // ʡ�������ݽṹ


        // 1. �޸� AreaNode������ Code ����
        public class AreaNode
        {
            public string Name { get; set; }
            public string Code { get; set; }
            public List<AreaNode> Children { get; set; } = new List<AreaNode>();
        }

        // 2. �޸� LoadAreaTree����ÿ���ڵ㸳���������룬����������� TreeNode �� Tag ����
        private void LoadAreaTree()
        {
            // ʾ�����ݣ����ϡ��ӱ���ʡ-��-������������������
            var rootAreas = new List<AreaNode>
            {
new AreaNode
{
    Name = "����ʡ",
    Code = "41",
    Children = new List<AreaNode>
    {
        new AreaNode
        {
            Name = "֣����",
            Code = "4101",
            Children = new List<AreaNode>
            {
                new AreaNode { Name = "��ԭ��", Code = "410102" },
                new AreaNode { Name = "��ˮ��", Code = "410105" },
                new AreaNode { Name = "������", Code = "410103" }
            }
        },
        new AreaNode
        {
            Name = "������",
            Code = "4103",
            Children = new List<AreaNode>
            {
                new AreaNode { Name = "������", Code = "410305" },
                new AreaNode { Name = "�ϳ���", Code = "410302" }
            }
        },
        new AreaNode
        {
            Name = "������",
            Code = "4108",
            Children = new List<AreaNode>
            {
                new AreaNode { Name = "�����", Code = "410802" },
                new AreaNode { Name = "��վ��", Code = "410803" },
                new AreaNode { Name = "�����", Code = "410804" },
                new AreaNode { Name = "ɽ����", Code = "410811" },
                new AreaNode { Name = "������", Code = "410821" },
                new AreaNode { Name = "������", Code = "410822" },
                new AreaNode { Name = "������", Code = "410823" },
                new AreaNode { Name = "����", Code = "410825" },
                new AreaNode { Name = "������", Code = "410882" },
                new AreaNode { Name = "������", Code = "410883" }
            }
        }
    }
},
                new AreaNode
                {
                    Name = "�ӱ�ʡ",
                    Code = "13",
                    Children = new List<AreaNode>
                    {
                        new AreaNode
                        {
                            Name = "ʯ��ׯ��",
                            Code = "1301",
                            Children = new List<AreaNode>
                            {
                                new AreaNode { Name = "������", Code = "130102" },
                                new AreaNode { Name = "������", Code = "130104" }
                            }
                        },
                        new AreaNode
                        {
                            Name = "��ɽ��",
                            Code = "1302",
                            Children = new List<AreaNode>
                            {
                                new AreaNode { Name = "·����", Code = "130202" },
                                new AreaNode { Name = "·����", Code = "130203" }
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
                provinceNode.Tag = province; // �洢 AreaNode ����
                AddChildNodes(provinceNode, province.Children);
                treeView1.Nodes.Add(provinceNode);
            }
            treeView1.ExpandAll();
        }

        // 3. �޸� AddChildNodes������ Tag
        private void AddChildNodes(TreeNode parentNode, List<AreaNode> children)
        {
            foreach (var child in children)
            {
                TreeNode childNode = new TreeNode(child.Name);
                childNode.Tag = child; // �洢 AreaNode ����
                parentNode.Nodes.Add(childNode);
                if (child.Children != null && child.Children.Count > 0)
                {
                    AddChildNodes(childNode, child.Children);
                }
            }
        }

        // 4. ���� treeView1 �� AfterSelect �¼�
        private void Form1_Load(object sender, EventArgs e)
        {
            treeView1.AfterSelect += treeView1_AfterSelect;
        }

        // 5. ʵ�� treeView1_AfterSelect �¼�������ʾ���������
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag is AreaNode area)
            {
                label4.Text = $"����: {area.Code}������: {area.Name}";
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

        // �޸� DrawFishboneDiagram ������֧��ÿ��һ����Ƿ�֧��2��3���������
        private void DrawFishboneDiagram(Graphics g, Rectangle bounds)
        {
            int centerY = bounds.Top + bounds.Height / 2;
            int startX = bounds.Left + 40;
            int endX = bounds.Right - 40;
            Pen mainPen = new Pen(Color.Black, 3);
            g.DrawLine(mainPen, startX, centerY, endX, centerY);

            // ��ͷ
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
            Font font = new Font("΢���ź�", 10, FontStyle.Bold);
            Brush textBrush = Brushes.Blue;

            // ÿ��һ����֧�Ķ�����֧������2��3����
            int[] subBranchCounts = new int[] { 2, 3, 2, 3, 2, 3 };

            for (int i = 0; i < branchCount; i++)
            {
                int x = startX + i * branchGap;
                // �Ϸ�֧
                Point upStart = new Point(x, centerY);
                Point upEnd = new Point(x - branchLength / 2, centerY - branchLength);
                g.DrawLine(Pens.Black, upStart, upEnd);
                // �·�֧
                Point downStart = new Point(x, centerY);
                Point downEnd = new Point(x - branchLength / 2, centerY + branchLength);
                g.DrawLine(Pens.Black, downStart, downEnd);

                // һ����֧���
                string upLabel = $"��{i + 1}";
                string downLabel = $"��{i + 1}";
                SizeF upSize = g.MeasureString(upLabel, font);
                SizeF downSize = g.MeasureString(downLabel, font);
                g.DrawString(upLabel, font, textBrush, upEnd.X - upSize.Width - 5, upEnd.Y - upSize.Height / 2);
                g.DrawString(downLabel, font, textBrush, downEnd.X - downSize.Width - 5, downEnd.Y - downSize.Height / 2);

                // ������ǣ�ÿ��һ����֧�ϻ�2��3��������֧��
                int subBranchCount = subBranchCounts[i % subBranchCounts.Length];
                int subBranchLength = 30;
                int subBranchGap = branchLength / (subBranchCount + 1);
                for (int j = 0; j < subBranchCount; j++)
                {
                    // �϶�����֧
                    int subUpX = upEnd.X - (j + 1) * subBranchGap;
                    int subUpY = upEnd.Y - (j + 1) * subBranchGap;
                    Point subUpStart = new Point(upEnd.X, upEnd.Y);
                    Point subUpEnd = new Point(subUpX, subUpY);
                    g.DrawLine(Pens.Gray, subUpStart, subUpEnd);
                    string subUpLabel = $"��{i + 1}-{j + 1}";
                    SizeF subUpSize = g.MeasureString(subUpLabel, font);
                    g.DrawString(subUpLabel, font, Brushes.DarkGreen, subUpEnd.X - subUpSize.Width - 2, subUpEnd.Y - subUpSize.Height / 2);

                    // �¶�����֧
                    int subDownX = downEnd.X - (j + 1) * subBranchGap;
                    int subDownY = downEnd.Y + (j + 1) * subBranchGap;
                    Point subDownStart = new Point(downEnd.X, downEnd.Y);
                    Point subDownEnd = new Point(subDownX, subDownY);
                    g.DrawLine(Pens.Gray, subDownStart, subDownEnd);
                    string subDownLabel = $"��{i + 1}-{j + 1}";
                    SizeF subDownSize = g.MeasureString(subDownLabel, font);
                    g.DrawString(subDownLabel, font, Brushes.DarkGreen, subDownEnd.X - subDownSize.Width - 2, subDownEnd.Y - subDownSize.Height / 2);
                }
            }
        }

        // 2. ���� panel1 �� Paint �¼�
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            DrawFishboneDiagram(e.Graphics, panel1.ClientRectangle);
        }

        // 1. �Ƴ����캯���е� panel1.Paint �¼���
      

        // 2. ��� button3_Click �¼��������ʱ�� Paint ��ˢ�� panel1
        private void button3_Click(object sender, EventArgs e)
        {
            panel1.Paint -= panel1_Paint; // ��ֹ�ظ���
            panel1.Paint += panel1_Paint;
            panel1.Invalidate(); // �����ػ�

        }
      
    }
}
