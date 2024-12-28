using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;
using System.Text.RegularExpressions;

namespace ReadFile
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var path = "C:\\pathToExpert\\ReadFile\\TextRead.txt";
            var read = File.ReadAllText(path);
            List<string> words= read.Split(new[] { "\r\n", "\r", "\n", ",", " ", ".", "?", "!", "-","*"}, StringSplitOptions.RemoveEmptyEntries).ToList();
            
            string[] splitSentences = Regex.Split(read, @"(?<=[.!?])\s+");

           
            List<string> sentenceList = splitSentences
                .Select(sentence => sentence.Trim())
                .Where(sentence => !string.IsNullOrEmpty(sentence))
                .ToList();
            var workString = new WorkString();



            Console.WriteLine("Enter the command number \n \n" +
                                    "0: Найти слова, содержащие максимальное количество цифр. \n" +
                                    "1: Найти самое длинное слово и определить, сколько раз оно встретилось в тексте.\n" +
                                    "2: Заменить цифры от 0 до 9 на слова «ноль», «один», ..., «девять». \n" +
                                    "3: Вывести на экран сначала вопросительные, а затем восклицательные предложения. \n" +
                                    "4: Вывести на экран только предложения, не содержащие запятых. \n" +
                                    "5: Вывести на экран только предложения, не содержащие запятых. \n" +
                                    "6: Найти слова, начинающиеся и заканчивающиеся на одну и ту же букву. \n\n");
            
             string MenuSelection = Console.ReadLine();
 
             bool flagBoolConverttoInt=int.TryParse(MenuSelection, out var numberMenu);
             if (flagBoolConverttoInt)
             {
                UserSelection(numberMenu, workString, sentenceList, words);
             }
             else
             {
                while(!flagBoolConverttoInt)
                {
                    Console.WriteLine("Please enter a number or end the program");
                    MenuSelection = Console.ReadLine();
                    flagBoolConverttoInt = int.TryParse(MenuSelection, out  numberMenu);
                }
                
                UserSelection(numberMenu, workString, sentenceList, words);

             }

        }

 

        private static void UserSelection(int numberMenu, WorkString workString, List<string> sentenceList, List<string> words)
        {
            switch (numberMenu)
            {
                case 1:
                    workString.TextKey = words;
                    workString.FindNumberWithSameFirstAndLastLetter(); //Найти слова, содержащие максимальное количество цифр.
                    Console.WriteLine($"Cлова, содержащие максимальное количество цифр \n");
                    foreach (var item in workString.NumberFirstAndLastLetter)
                    Console.WriteLine($"{item}");
                    break;
                case 2:
                    workString.TextKey = words;
                    workString.FindBigWords();  // Найти самое длинное слово и определить, сколько раз оно встретилось в тексте.
                    Console.WriteLine($" SizeWord = {workString.SizeWord}, {workString.CountWord}");
                    break;
                case 3:
                    workString.TextKey = words;
                    workString.ConvertNumberToString(); //- Заменить цифры от 0 до 9 на слова «ноль», «один», ..., «девять».
                    break;
                case 4:
                    workString.TextKey = sentenceList;
                    workString.SplitСollection(sentenceList);  //- Вывести на экран сначала вопросительные, а затем восклицательные предложения.
                    foreach (var item in workString.ExclamatorySentence)
                             Console.WriteLine(item);
                    foreach (var item in workString.InterrogativeSentence)
                            Console.WriteLine(item);
                    break;
                case 5:
                    //- Вывести на экран только предложения, не содержащие запятых.
                    break;
                case 6:
                    workString.TextKey = words;
                    workString.FindWordsWithSameFirstAndLastLetter();   //- Найти слова, начинающиеся и заканчивающиеся на одну и ту же букву.
                    break;
            };

        }
    }

}


//Домашнее задание:
//Visual studio - Console Application
//Считать строку текста из консоли (продвинутое задание: из файла).
//Строка содержит буквы латинского алфавита, знаки препинания и цифры.
//Реализовать меню выбора действий:
//- Найти слова, содержащие максимальное количество цифр.
//- Найти самое длинное слово и определить, сколько раз оно встретилось в тексте.
//- Заменить цифры от 0 до 9 на слова «ноль», «один», ..., «девять».
//- Вывести на экран сначала вопросительные, а затем восклицательные предложения.
//- Вывести на экран только предложения, не содержащие запятых.
//- Найти слова, начинающиеся и заканчивающиеся на одну и ту же букву.
//Приложение не должно падать ни при каких условиях.

