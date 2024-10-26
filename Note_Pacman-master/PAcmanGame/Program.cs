using System; // Khai báo thư viện cơ bản cho các chức năng như Console
using System.Collections.Generic; // Khai báo thư viện cho các cấu trúc dữ liệu như List
using System.Threading; // Khai báo thư viện cho xử lý đa luồng

namespace PacmanGame // Định nghĩa không gian tên cho trò chơi Pacman
{
    class Program // Định nghĩa lớp chính cho chương trình
    {
        public static bool gameOver = false; // Biến toàn cục để xác định trạng thái trò chơi (kết thúc hay không)
        public static int score = 0; // Biến toàn cục để lưu điểm số của người chơi
        static int speed = 250; // Biến tốc độ, quyết định thời gian ngủ giữa các vòng lặp
        public enum direction { left, up, right, down } // Định nghĩa hướng di chuyển

        static Thread background = new Thread(BackgroundGame); // Tạo một luồng mới cho quá trình trò chơi
        public static Map map = new Map(); // Khởi tạo bản đồ trò chơi
        public static Pacman pacman; // Khai báo đối tượng Pacman
        public static List<StupidGhost> stupidGhosts = new List<StupidGhost>(); // Danh sách các ma ngu
        public static List<SmartGhost> smartGhosts = new List<SmartGhost>(); // Danh sách các ma thông minh

        static void Main(string[] args) // Phương thức chính của chương trình
        {
            // Hình ảnh giới thiệu
            Console.CursorVisible = false; // Ẩn con trỏ chuột
            Console.ForegroundColor = ConsoleColor.Cyan; // Đặt màu chữ là Cyan
            Console.SetCursorPosition(12, 15); // Đặt vị trí con trỏ để in ra
            Console.WriteLine("PACMAN"); // In ra tiêu đề trò chơi
            Thread.Sleep(1500); // Tạm dừng 1.5 giây để người chơi xem tiêu đề

            // Bắt đầu trò chơi
            map.RenderMap(); // Vẽ bản đồ trò chơi
            map.RenderJewels(); // Vẽ các viên ngọc trên bản đồ

            Program.pacman = new Pacman(13, 23); // Khởi tạo đối tượng Pacman tại vị trí (13, 23)
            Program.stupidGhosts.Add(new StupidGhost(13, 11, Object.direction.left)); // Thêm một ma ngu vào danh sách
            Program.smartGhosts.Add(new SmartGhost(15, 11, Object.direction.right)); // Thêm một ma thông minh vào danh sách

            background.Start(); // Bắt đầu luồng trò chơi
            background.IsBackground = true; // Đặt luồng này là luồng nền

            pacman.Control(background); // Gọi phương thức điều khiển Pacman

            Console.Clear(); // Xóa màn hình console

            // Kết thúc trò chơi
            // Bạn thua
            if (score < 2430) // Kiểm tra nếu điểm số nhỏ hơn 2430
            {
                Console.SetCursorPosition(12, 6); // Đặt vị trí con trỏ
                Console.ForegroundColor = ConsoleColor.Red; // Đặt màu chữ là đỏ
                Console.Write("Game Over"); // In ra thông báo "Game Over"
                Console.SetCursorPosition(12, 8); // Đặt lại vị trí con trỏ
                Console.ForegroundColor = ConsoleColor.Cyan; // Đặt màu chữ là Cyan
                Console.Write("Điểm của bạn: {0}", score); // In ra điểm số của người chơi
                Thread.Sleep(500); // Tạm dừng 0.5 giây
                Console.SetCursorPosition(12, 10); // Đặt lại vị trí con trỏ
                Console.ForegroundColor = ConsoleColor.Yellow; // Đặt màu chữ là vàng
            }
            // Bạn thắng
            else // Nếu điểm số lớn hơn hoặc bằng 2430
            {
                Console.SetCursorPosition(12, 6); // Đặt vị trí con trỏ
                Console.ForegroundColor = ConsoleColor.Cyan; // Đặt màu chữ là Cyan
                Console.Write("Bạn thắng!"); // In ra thông báo "Bạn thắng!"
                Console.SetCursorPosition(12, 8); // Đặt lại vị trí con trỏ
                Console.ForegroundColor = ConsoleColor.Cyan; // Đặt màu chữ là Cyan
                Console.Write("Điểm của bạn: {0}", score); // In ra điểm số của người chơi
                Thread.Sleep(500); // Tạm dừng 0.5 giây
                Console.SetCursorPosition(12, 10); // Đặt lại vị trí con trỏ
                Console.ForegroundColor = ConsoleColor.Yellow; // Đặt màu chữ là vàng
            }
        }

        // Quá trình trò chơi
        public static void BackgroundGame() // Phương thức chạy nền cho trò chơi
        {
            while (!Program.gameOver) // Vòng lặp cho đến khi trò chơi kết thúc
            {
                if (score == 2430) gameOver = true; // Kiểm tra nếu điểm số đạt 2430, kết thúc trò chơi

                // Hành động của Pacman
                pacman.Step(); // Gọi phương thức di chuyển của Pacman

                // Hành động của ma thông minh
                foreach (SmartGhost ghost in Program.smartGhosts) ghost.Step(); // Gọi phương thức di chuyển của các ma thông minh

                // Sau 30 viên ngọc bắt đầu hành động của ma ngu
                if (Program.score > 300) // Kiểm tra nếu điểm số lớn hơn 300
                {
                    foreach (StupidGhost ghost in Program.stupidGhosts) ghost.Step(); // Gọi phương thức di chuyển của các ma ngu
                }

                // Hiển thị điểm số
                Console.ForegroundColor = ConsoleColor.Yellow; // Đặt màu chữ là vàng
                Console.SetCursorPosition(31, 1); // Đặt vị trí con trỏ
                Console.Write("Điểm: {0}", score); // In ra điểm số của người chơi

                Thread.Sleep(speed); // Tạm dừng một khoảng thời gian trước khi tiếp tục vòng lặp
            }
        }
    }
}