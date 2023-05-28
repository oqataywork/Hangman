// выводить все испробованные буквы
// сделать так, чтобы надпись про успех и повтор выводилась только один раз
// сделать вывод слова



namespace Hangman
{
    public class Program
    {
        static void Main(string[] args)
        {
            List<string> allWords = GetWordsInList();
            string randomWord = GetRandomWordFromList(allWords);
            char[] encryptedWord = EncryptWord(randomWord);
            StartGame(randomWord, encryptedWord);


        }


        static List<string> GetWordsInList()
        {
            string filePath = @"C:\Users\oqata\source\repos\Hangman\WordsStockRus.txt";
            IEnumerable<string> allWords = File.ReadLines(filePath);
            List<string> wordsInList = allWords.ToList();

            return wordsInList;
        }

        static string GetRandomWordFromList(List<string> allWords)
        {
            int listSize = allWords.Count();
            Random random = new Random();
            int randomNumber = random.Next(0, listSize);
            string randomWord = allWords[randomNumber];

            return randomWord;
        }

        static char[] EncryptWord(string word)
        {
            char[] underscores = Enumerable.Repeat('_', word.Length).ToArray();
            return underscores;
            
        }

        static int SelectDifficultMode()
        {
            Console.WriteLine("Выбери режим сложности:" +
                "\n1 - Easy" +
                "\n2 - Medium" +
                "\n3 - Hard");

            string userAnswer = Console.ReadLine();
            int numberOfAttempts = 10;

            while (numberOfAttempts != 10 || numberOfAttempts != 8 || numberOfAttempts != 6)
            {
                switch (userAnswer)
                {
                    case "1":
                        numberOfAttempts = 10;
                        return numberOfAttempts;
                        
                    case "Easy":
                        numberOfAttempts = 10;
                        return numberOfAttempts;
                        
                    case "2":
                        numberOfAttempts = 8;
                        return numberOfAttempts;
                        
                    case "Medium":
                        numberOfAttempts = 8;
                        return numberOfAttempts;
                        
                    case "3":
                        numberOfAttempts = 6;
                        return numberOfAttempts;
                        
                    case "Hard":
                        numberOfAttempts = 6;
                        return numberOfAttempts;
                        
                    default:
                        Console.WriteLine("Неправильная форма ответа. Попробуйте 1,2,3 или используйте названия режимов");
                        userAnswer = Console.ReadLine();
                        break;
                }
            }
            return numberOfAttempts;
        }

       
        static char GetUserInputAndVerifyIt()
        {
            string userAnswer;
            bool check = false;
            char answer = ' '; 

            while (check != true)
            {

                userAnswer = Console.ReadLine();

                if (string.IsNullOrEmpty(userAnswer))
                {
                    Console.WriteLine("Ответ не может быть пустым. Попробуйте снова");
                    continue;
                }

                char[] answerInChar = userAnswer.ToCharArray();

                check = userAnswer.Length == 1
                    // checking if char belongs to russian alphabet
                    && answerInChar[0] >= '\u0410' && answerInChar[0] <= '\u044F';

                answer = answerInChar[0];
                if (!check)
                {
                    Console.WriteLine("Неподходящий символ. Попробуйте ввести снова");
                }
            }
            
            return answer;
        }


  

        static void StartGame(string randomWord, char[] encryptedWord)
        {
            char[] charRandomWord = randomWord.ToCharArray();

            Console.WriteLine("Привет! Добро пожаловать в игру \"Виселица\"!");
            int numberOfAttempts = SelectDifficultMode();
            Console.WriteLine($"Отлично! Игра начинается. Количество попыток: {numberOfAttempts}");

            Console.WriteLine($"Слово = {string.Join("", encryptedWord)}" +
                $"\nКоличество букв в слове = {encryptedWord.Length}");
            Console.WriteLine("Введите букву:");


           



            bool gameEnd = false;
            int lives = numberOfAttempts;
            char[] guessedLetters = new char[randomWord.Length];


            while (gameEnd || lives >= 0)
            {

                char userAnswer = GetUserInputAndVerifyIt();


                // check for repeat
                bool notCopy = true;
                for (int i = 0; i < guessedLetters.Length; i++)
                {
                    if (userAnswer.Equals(guessedLetters[i])) {
                        Console.WriteLine("Буква повторилась");
                        notCopy = false;
                    }
                }


                string encryptedWordCopy = string.Join("", encryptedWord);
                // check for success
                int counter = 0;
                for (int i = 0; i < encryptedWord.Length; i++)
                {
                    if (userAnswer.Equals(randomWord[i]) && notCopy)
                    {
                        encryptedWord[i] = userAnswer;
                        guessedLetters[counter] = userAnswer;
                        counter++;
                        Console.WriteLine("Буква отгадана");
                    }
                }

                // check for fail
                string compareWord = string.Join("", encryptedWord);
                if (Equals(compareWord, encryptedWordCopy) && notCopy) {
                    lives--;
                    Console.WriteLine("Буква была не отгадана" +
                        $"\n Количество жизней: {lives}"); ;
                    
                }
                Console.WriteLine(string.Join("", encryptedWord));

                // добавить счетчик побед подряд
                // добавить интерфейс для начала новой игры
                //checking for lose
                if (lives == 0)
                {
                    Console.WriteLine("Вы проиграли." +
                        $"\nЗагаданное слово: {randomWord}");

                }


                // checking for win
                int endGameChecker;
                endGameChecker = (Array.IndexOf(encryptedWord, '_'));
                if (endGameChecker == -1)
                {
                    Console.WriteLine("Игра закончена. Поздравляю, вы победили!!!");
                }

            }


        }
    }
}