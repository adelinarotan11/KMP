using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace КМП
{
    public partial class Form_1 : Form
    {
        public static string input;//исходная строка
        public static string pattern; //шаблон
        KMP t = new KMP(input, pattern);//экземпляр класса КМП для решение поставленной задачи(поиса подстрок)
        private bool dataReaded = false;//флаг успешного считывания с файла

        public Form_1()
        {
            InitializeComponent();
         button1.Enabled = false; button2.Enabled = false; 
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        //обработка нажатия на пункт Выход
        {
            this.Close();//закрые окна приложения
        }


        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)//обработка события открытия текстового файла
        {
          
            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                StreamReader fin1;//создание потока исходной строки
                
                try
                {
                    using (fin1 = new StreamReader(openFileDialog1.FileName))//окно_1 для выбора файла и присваевание fin1
                    {
                       
                        while (!fin1.EndOfStream)
                        {
                            input += fin1.ReadLine();
                        }
                        fin1.Close();//закрытие файла
                        richTextBox1.Text = input;
                    }
                    dataReaded = true;//флаг того, что всё прошло успешно
                }
                catch
                {
                    dataReaded = false;
                    MessageBox.Show("Read Error!");
                }
            }

            if (openFileDialog2.ShowDialog(this) == DialogResult.OK)//окно_2 для выбора файла и присваевание fin2
            {
                StreamReader fin2;//создание потока для образа
                try
                {
                   
                    using (fin2 = new StreamReader(openFileDialog2.FileName))
                    {
                        while (!fin2.EndOfStream)
                        {
                            pattern += fin2.ReadLine();
                        }
                        fin2.Close();//закрытие файла
                        richTextBox2.Text = pattern;
                    }
                    dataReaded = true;//флаг того, что всё прошло успешно
                
                }
                catch
                {
                    dataReaded = false;
                    MessageBox.Show("Read Error!");
                }
            }
            instruction_l.Visible = false;
            button1.Enabled= true;//клавиша "Найти подстроки" становится рабочей
        }

        private void button1_Click(object sender, EventArgs e)//обработка клавиши "Найти подстроки!"
        {
            KMP t1 = new KMP(input, pattern);
            List<int> rez = new List<int>();//список индексов с которых начинается образ
            int r = 0;//вспомогательная переменная
            int begin = 0;//индекс с которого начинаются 
            int Count = 0;//Счетчик массива целочисленного массива индексов начала подстрок
           
            if (dataReaded && richTextBox1.Text != "" && richTextBox2.Text != "")//ЕСЛИ :флаг что все успешно считано с файла равен 1(истина) 
                                                                                 //и два richTextBox заполнены текстом
            {
                try
                { 
                    while ((r = t1.FindSubstring(ref begin)) >= 0)//решение задачи КМП,пока не конец строки(значение >=0)
                    {
                        rez.Add(r);
                        Count++;//счетчик для кол-ва элементов в списке начала подстрок
                        begin = r + 1;
                    }
                    if (Count > 0)
                    {
                        foreach (var i in rez)//вывод результата
                        {
                            //    richTextBox2.Text += "\n";
                            //    richTextBox2.Text += $"begin_obraz= : {string.Join(", ", i)}";
                            //    richTextBox2.Text += "\t";
                            //}
                                                //Выделение другим шрифтом:
                            char ch;                                                                      
                            for (int j = 0; j < input.Length; j++)
                            {
                                // Установить значение по которой будет проверка,из какого регистра буква: 
                                ch = richTextBox2.Text[0];
                                //C какого символа и до какого выделить:
                                richTextBox1.Select(i, pattern.Length);
                                if (char.IsUpper(ch)) // isupper()-возвращает ненулевое значение, если аргумент ch является буквой верхнего регистра
                                {
                                    richTextBox1.SelectionFont = new Font("Verdana", 16, FontStyle.Regular);
                                    richTextBox1.SelectionColor = Color.Red;
                                }
                                else
                                {
                                    richTextBox1.SelectionFont = new Font("Arial", 16, FontStyle.Bold);
                                    richTextBox1.SelectionColor = Color.Pink;
                                }
                            }
                        }
                    }
                    else throw new Exception("Образ не был найден в текущем тексте!");
                   
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (richTextBox1.Text.Length < richTextBox2.Text.Length)
            {
                MessageBox.Show("Длина образа больше длины исходного текста поиска!");
                button2.Enabled = true;//клавиша "Очистить" становится рабочей
                return;

            }
            else MessageBox.Show("Read Error!");

           
            button2.Enabled = true;//клавиша "Очистить" становится рабочей
        }

        private void button2_Click(object sender, EventArgs e)//обработка нажатия клавиши Очистить
        {
            input = "";
            pattern = "";
            richTextBox1.Clear(); richTextBox2.Clear();
        }

       
    }
    }


