using Entities.Entities;
using Foms.Interfaces;
using Foms.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Foms.Forms
{
    public partial class EstoqueForm : Form
    {
        private MovimentoEstoque movimento;
        private readonly IEstoque_FormRepository MovimentoRepository;
        public EstoqueForm(IEstoque_FormRepository mov)
        {
            MovimentoRepository = mov;
            InitializeComponent();
            grid();
            textBox1.KeyPress += textBox1_KeyPress;
            textBox2.KeyPress += textBox1_KeyPress;
            textBox3.KeyPress += textBox1_KeyPress;
        }

        public async void grid()
        {
            DataTable tabela = new DataTable();
            tabela=await MovimentoRepository.RetornarTabela();

            dataGridView.DataSource = tabela;
        }

        private async void button1_Click(object sender, EventArgs e)
        {

            MessageBox.Show(await MovimentoRepository.AdcionarNovoMovimentoNoEstoque(movimento = new MovimentoEstoque
            {
                FkFornecedor = 1,
                Fk_tipomovimentacao = 1,
                FkProduto = 8,
                DataMovimentacao = DateTime.Now,
                Quantidade = 20,
                ValorUnitario = 10
            }));
            grid();

        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

       
    }
}
