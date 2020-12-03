using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Telegram.Bot.Types.Enums;//нужны для работы телеграмм-бота
using Telegram.Bot.Args;//нужны для работы телеграмм-бота
using Telegram.Bot;
using System.Net;

namespace TelegramBot
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //переменная для бота
        private static TelegramBotClient MyBot;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnStartBot_Click(object sender, RoutedEventArgs e)
        {
            //создаем бота
            //устанавливаем клиенту токен
            MyBot = new TelegramBotClient("437941930:AAGzt9uHjMVFhnI1d5PrWXNIS9-WgmS3Cdc");

			ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

			var me = MyBot.GetMeAsync().Result;
            //назначаем обработчик
            MyBot.OnMessage += BotOnMessageReceived;
            //запускаем прием сообщений
            MyBot.StartReceiving(new UpdateType[] { UpdateType.Message });
        }
        //обработчик бота(обработчик получения/чтения сообщений)
        private static async void BotOnMessageReceived(object sender, MessageEventArgs messageEventArgs)
        {
            Telegram.Bot.Types.Message msg = messageEventArgs.Message;
            //проверяем сообщение, чтобы было не пустое и было текстовое
            if(msg == null || msg.Type != MessageType.Text)
                return;
            //переменная для хранения ответа
            String answ_msg = "";
            switch (msg.Text)
            {
				case "/info": answ_msg = "Команды:\r\n/start\r\n/test\r\n/stop"; break;
                case "/start": answ_msg = $"Здравствуй, дорогой друг!\r\nСын маминой подруги?"; break;
                case "/test": answ_msg = "Здравствуйте, это тест на работу бота"; break;
                case "/kontakt": answ_msg = "Контакты для связи с программистом:\r\nskype: psacode\r\nwww: http://про-кодер.рф"; break;
                case "/stop": answ_msg = "Досвидания, бот прощается с вами!"; break;
                default: answ_msg = "Не верная команда"; break;
            }
            await MyBot.SendTextMessageAsync(msg.Chat.Id, answ_msg);
        }

    }
}
