using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace app_notas
{
    public partial class Form1 : Form
    {
        private Data manager;
        private List<Nota> notas;
        private string title, content;
        private int id,contador;
        private Boolean editor;
       
        public Form1()
        {
            InitializeComponent();
            this.CenterToScreen();
            manager = new Data();
            //this.Icon = new Icon("resources/iconnotes.png");
            cargarLista(); //mandamos a mostrar en la lista las notas guardadas 
            this.MaximizeBox = true;
        }

        private void cargarLista()
        {
            editor = false;
            id = 0;
            title = "";
            content = "";
            notas = manager.getNotas();
            list_notas.Items.Clear();
            foreach (Nota note in notas)
            {
                list_notas.Items.Add(note.getTitulo());
            }
            
        }

        
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (list_notas.SelectedIndex >= 0)
            {
                if (editor)
                {

                    //update 
                    title = text_titulo.Text;
                    content = text_contenido.Text;
                    if (manager.updateNota(id, title, content))
                    {
                        int pos_temp = list_notas.SelectedIndex;
                        cargarLista();
                        editor = false;
                        list_notas.SetSelected(pos_temp, true);
                        //Console.WriteLine("update..");
                    }
                }
                contador = 0;
                Nota note = notas.ElementAt(list_notas.SelectedIndex);
                title = note.getTitulo();
                content = note.getContenido();
                id = note.getId();

                text_titulo.Text = title;
                text_contenido.Text = content;
            }

        }

        private void nuevaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            text_titulo.Text = "";
            text_contenido.Text = "";
            list_notas.ClearSelected();

        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void eliminarNotaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(list_notas.SelectedIndex >= 0 )
            {
               DialogResult result =  MessageBox.Show("¿Eliminar nota seleccionada?","Confirmación",MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    if (manager.deleteNota(id))
                    {
                        cargarLista();

                        text_contenido.Text = "";
                        text_titulo.Text = "";
                    }
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void list_notas_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (list_notas.SelectedIndex >= 0)
            {
                if (editor)
                {

                    //update 
                    title = text_titulo.Text;
                    content = text_contenido.Text;
                    if (manager.updateNota(id, title, content))
                    {
                        int pos_temp = list_notas.SelectedIndex;
                        cargarLista();
                        editor = false;
                        list_notas.SetSelected(pos_temp, true);
                        //Console.WriteLine("update..");
                    }
                }
                contador = 0;
                Nota note = notas.ElementAt(list_notas.SelectedIndex);
                title = note.getTitulo();
                content = note.getContenido();
                id = note.getId();

                text_titulo.Text = title;
                text_contenido.Text = content;
            }


        }

        private void text_contenido_TextChanged_1(object sender, EventArgs e)
        {
            //Console.WriteLine("Text ...");
            contador++;
            if (list_notas.SelectedIndex >= 0 && contador > 1)
            {
                editor = true; //queremos decir que si modifico la nota actualmente abierta
            }
            //sino hay un indice mayor o igual a -1 quiere decir que esta haciendo una nota nueva
        }

        private void text_contenido_TextChanged(object sender, EventArgs e)
        {
            //Console.WriteLine("Text ...");
            contador++;
            if (list_notas.SelectedIndex >= 0 && contador > 1)
            {
                editor = true; //queremos decir que si modifico la nota actualmente abierta
            }
            //sino hay un indice mayor o igual a -1 quiere decir que esta haciendo una nota nueva
        }

        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {

            string titulo = text_titulo.Text;
            string contenido = text_contenido.Text;

            if (titulo == "")
            {
                MessageBox.Show("Inserte un titulo", "Notificación");
            }else if(contenido == "")
            {
                MessageBox.Show("Inserte contenido", "Notificación");
            }
            else
            {
                if (manager.insertarNota(titulo, contenido))
                {
                    //MessageBox.Show("Nota guardada");
                    text_titulo.Text = "";
                    text_contenido.Text = "";
                    cargarLista();
                    list_notas.SetSelected(notas.Count-1,true);
                    
                }
                else
                {
                    MessageBox.Show("No se pudo guardar la nota","Notificación");
                }
            }

        }//btn guardar
    }//class form
}//namespace
