

namespace LerXML_NaturezaTit
{
    public partial class Form1 : Form
    {
        //Teste de Commit no Git
        public Form1()
        {
            InitializeComponent();

          // button2.Image = Properties.Resources.adArquivo;

        }

        //private const int V = 100;

        private void Form1_Load(object sender, EventArgs e)
        {

            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);

            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 120, 215);
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.White;

            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.EnableHeadersVisualStyles = false;

            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 30, 30);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            dataGridView1.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dataGridView1.RowTemplate.Height = 30;

            // Remove linhas feias
            dataGridView1.RowHeadersVisible = false;


            textInstrucao.Text = @"INSTRUÇÕES:

 Segue instruções para que seja filtrado as 
informações sobre a Natureza da operação:

1. Adicione o Borderô desado.
2. Selecione os XML's desejados.
3. Basta gerar a planilha com as informações.
";


            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        public string[] selecaoArquivo()
        {
            try
            {
                OpenFileDialog selecaoXml = new OpenFileDialog();

                selecaoXml.InitialDirectory = @""; //alterar diretorio para um que inicialize na area de trabalho do user.
                selecaoXml.Filter = "Arquivos XML (*.xml)|*.xml|Todos os arquivos (*.*)|*.*"; //busca apenas arquivos com extenssão XML
                selecaoXml.FilterIndex = 1; // Define o filtro padrão (1 para XML)
                selecaoXml.RestoreDirectory = true; // Restaura o diretório anterior após fechar a caixa de diálogo
                selecaoXml.Multiselect = true;

                if (selecaoXml.ShowDialog() == DialogResult.OK)
                {
                    return selecaoXml.FileNames;
                }
                else
                {
                    string caminhoxmlVazio = "Vazio";
                    // Se o usuário cancelou o diálogo (não selecionou nenhum arquivo),
                    // exibe uma mensagem e para a execução do restante do método.
                    MessageBox.Show("Nenhum arquivo XML foi selecionado. Operação cancelada.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    // Sai do método se nenhum arquivo for selecionado
                    return new string[0];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
                return new string[0];
            }
        }

        List<LerXml> listaGlobal = new List<LerXml>();

        private void button2_Click(object sender, EventArgs e)
        {
            var caminhos = selecaoArquivo();

            LerXml xml1 = new LerXml();

            listaGlobal = caminhos
                .SelectMany(caminho => xml1.GerarList(caminho, textBox1))
                .ToList();

            dataGridView1.DataSource = listaGlobal;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listaGlobal == null || listaGlobal.Count == 0)
            {
                MessageBox.Show("Nenhum dado carregado!");
                return;
            }

            SaveFileDialog salvar = new SaveFileDialog();

            salvar.Filter = "Arquivo Excel (*.xlsx)|*.xlsx";
            salvar.Title = "Salvar arquivo Excel";
            salvar.FileName = "Relatorio.xlsx"; // nome padrão
            salvar.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            if (salvar.ShowDialog() == DialogResult.OK)
            {
                string caminhoEscolhido = salvar.FileName;

                GerarExcel.GerarPlanilha(listaGlobal, caminhoEscolhido);

                MessageBox.Show("Excel gerado com sucesso!");
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            textBox1.Text = null;
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
