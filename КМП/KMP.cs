using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace КМП
{
    class KMP
    {
        public string input;//input-исходная строка
        public string pattern; //pattern-шаблон

        public KMP(string stroka2, string pattern2)//конструктор
        {
            input = stroka2;
            pattern = pattern2;
        }

        public int FindSubstring(ref int begin)//функция поиска подстроки
                                               //begin-символ с которого образ в строке найден

        {
            int n = input.Length;
            int m = pattern.Length;
            if (n == 0) throw new ArgumentException("String mustn't be empty", "input");
            if (m == 0) throw new ArgumentException("String mustn't be empty", "pattern");

            int[] d = GetPrefixFunction();//массив пи
            int i, j;
            //i-указатель на текущий символ исходной строки. 
            //j -указатель на текущий символ образа.
            for (i = begin, j = 0; (i < n) && (j < m); i++, j++)
                while ((j >= 0) && (pattern[j] != input[i]))
                    j = d[j];

            if (j == m) return i - m;   //образ найден

            else
                return -1;//образ в строке отсутствует



        }
        private int[] GetPrefixFunction()//создание префикс функции
        {
            int length = pattern.Length;
            int[] result = new int[length];//массив пи

            int i = 0;
            int j = -1;
            result[0] = -1;
            while (i < length - 1)
            {
                while ((j >= 0) && (pattern[j] != pattern[i]))
                    j = result[j];
                i++;
                j++;
                if (pattern[i] == pattern[j])
                    result[i] = result[j];
                else
                    result[i] = j;
            }
            return result;
        }

    }
    }
