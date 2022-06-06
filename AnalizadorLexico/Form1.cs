using System.Windows.Forms;

namespace AnalizadorLexico
{
    public partial class Form1 : Form
    {
        Lexico Analiza_Lexico = new Lexico();
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Analiza_Lexico.Inicia();
            Analiza_Lexico.Analyze(textBox_codigo.Text);
            TOKEN_LEXEMA.Rows.Clear();
            if(Analiza_Lexico.NoTokens > 0)
            {
                TOKEN_LEXEMA.Rows.Add(Analiza_Lexico.NoTokens);
                for(int i = 0; i < Analiza_Lexico.NoTokens; i++)
                {
                    TOKEN_LEXEMA.Rows[i].Cells[0].Value = Analiza_Lexico.Tokens[i];
                    TOKEN_LEXEMA.Rows[i].Cells[1].Value = Analiza_Lexico.Lexemas[i];
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            //Limpiar el Datagrid
            TOKEN_LEXEMA.Rows.Clear();
            //Limpiar el Textbox
            textBox_codigo.Text = "";
            textBox_codigo.Focus();
        }
    }
}