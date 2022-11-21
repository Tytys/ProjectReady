using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Project.ClassFolder
{
    class MBClass
    {
        public static void ErrorMB(string message)
        {
            MessageBox.Show(message, "Ошибка",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public static void ErrorMB(Exception message)
        {
            MessageBox.Show(message.Message, "Ошибка",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public static void InfoMB(string message)
        {
            MessageBox.Show(message, "Информация",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public static bool QuestionMB(string question)
        {
            return MessageBoxResult.Yes ==
                MessageBox.Show(question, "Вопрос",
                MessageBoxButton.YesNo, MessageBoxImage.Question);
        }

        public static void ExitMB()
        {
            if (QuestionMB("Вы действительно хотите выйти"))
            {
                App.Current.Shutdown();
            }
        }
    }
}
