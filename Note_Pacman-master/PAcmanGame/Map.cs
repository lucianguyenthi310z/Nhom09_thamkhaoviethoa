using System;
using System.Collections.Generic;
using System.Linq; // Khai báo không gian System.Linq để sử dụng các phương thức truy vấn và thao tác dữ liệu với các tập hợp.
using System.Text; // Khai báo không gian System.Text để sử dụng các lớp và phương thức liên quan đến xử lý chuỗi, 
using System.Threading.Tasks; // Khai báo không gian tên System.Threading.Tasks để sử dụng các lớp và phương thức hỗ trợ lập trình bất đồng bộ,

namespace PacmanGame
{
    public class Map
    {
        int x, y; // tọa độ
        public static char wall = '█'; // ký hiệu của tường
        public static char emptySpace = ' '; // ký hiệu của không gian trống
        public static char jewel = '·'; // ký hiệu của viên ngọc
        public static char smartGhostSymbol = 'X'; // ký hiệu của ma thông minh
        public static char stupidGhostSymbol = 'Y'; // ký hiệu của ma ngu
        public char[,] area = new char[31, 28]; // mảng bản đồ

        public char Wall { get; set; } // thuộc tính để lấy hoặc thiết lập ký hiệu tường
        public char EmptySpace { get; set; } // thuộc tính để lấy hoặc thiết lập ký hiệu không gian trống
        public char Jewel { get; set; } // thuộc tính để lấy hoặc thiết lập ký hiệu viên ngọc

        // Chỉ mục của bản đồ
        public char this[int x, int y]
        {
            get
            {
                if (x < 0) return area[y, 27]; // Nếu x nhỏ hơn 0, trả về ký hiệu của cột cuối
                else if (x > 27) return area[y, 0]; // Nếu x lớn hơn 27, trả về ký hiệu của cột đầu
                else return area[y, x]; // Trả về ký hiệu tại tọa độ (x, y)
            }
            set
            {
                area[y, x] = value; // Thiết lập ký hiệu tại tọa độ (x, y)
                Console.ForegroundColor = ConsoleColor.Green; // Đặt màu chữ là xanh
                Console.SetCursorPosition(x + 1, y + 1); // Đặt vị trí con trỏ trên màn hình
                Console.Write(value); // In ký hiệu ra màn hình
                Console.ForegroundColor = ConsoleColor.White; // Đặt màu chữ trở lại trắng
            }
        }

        // Vẽ ký hiệu
        public void RenderChar(int x, int y, char symbol)
        {
            if (x < 0) x = 27; // Nếu x nhỏ hơn 0, chuyển về cột cuối
            else if (x > 27) x = 0; // Nếu x lớn hơn 27, chuyển về cột đầu

            // Đặt màu chữ theo ký hiệu
            if (symbol == '@') Console.ForegroundColor = ConsoleColor.Green; // Ký hiệu người chơi
            else if (symbol == 'X') Console.ForegroundColor = ConsoleColor.Red; // Ký hiệu ma thông minh
            else if (symbol == 'Y') Console.ForegroundColor = ConsoleColor.Blue; // Ký hiệu ma ngu

            Console.SetCursorPosition(x + 1, y + 1); // Đặt vị trí con trỏ
            Console.Write(symbol); // In ký hiệu ra màn hình
            Console.ForegroundColor = ConsoleColor.White; // Đặt màu chữ trở lại trắng

            area[y, x] = symbol; // Cập nhật ký hiệu vào mảng bản đồ
        }

        // Phương thức để tạo bản đồ mới
        private void RenderWall(int x, int y)
        {
            Console.ForegroundColor = ConsoleColor.Cyan; // Đặt màu chữ là cyan
            Console.SetCursorPosition(x + 1, y + 1); // Đặt vị trí con trỏ
            Console.Write(wall);  
            Console.ForegroundColor = ConsoleColor.White; // Đặt màu chữ trở lại trắng
            area[y, x] = wall; // Cập nhật ký hiệu tường vào mảng bản đồ
        }

        public void RenderEmptySpace(int x, int y)
        {
            Console.SetCursorPosition(x + 1, y + 1); // Đặt vị trí con trỏ
            Console.Write(emptySpace); // In ký hiệu không gian trống ra màn hình
            area[y, x] = emptySpace; // Cập nhật ký hiệu không gian trống vào mảng bản đồ
        }

        public void RenderJewel(int x, int y)
        {
            Console.ForegroundColor = ConsoleColor.Yellow; // Đặt màu chữ là vàng
            Console.SetCursorPosition(x + 1, y + 1); // Đặt vị trí con trỏ
            Console.Write(jewel); // In ký hiệu viên ngọc ra màn hình
            Console.ForegroundColor = ConsoleColor.White; // Đặt màu chữ trở lại trắng
            area[y, x] = jewel;  
        }

        // Vẽ bản đồ theo cách thủ công
        public void RenderMap()
        {
            // Biên giới
            Console.ForegroundColor = ConsoleColor.Cyan; // Đặt màu chữ là cyan
            Console.SetCursorPosition(1, 1); // Đặt vị trí con trỏ
            for (int i = 0; i < 28; i++)
            {
                Console.Write(wall);  // In ký hiệu tường ra màn hình
                area[0, i] = wall; // Cập nhật ký hiệu tường vào mảng bản đồ
            }
            Console.SetCursorPosition(1, 31);  
            for (int i = 0; i < 28; i++)
            {
                Console.Write(wall);  
                area[30, i] = wall;  
            }
            for (int i = 2; i < 15; i++)
            {
                Console.SetCursorPosition(1, i);  
                Console.Write(wall);  
                area[i - 1, 0] = wall;  
                Console.SetCursorPosition(28, i);  
                Console.Write(wall);  
                area[i - 1, 27] = wall;  
            }
            for (int i = 16; i < 31; i++)
            {
                Console.SetCursorPosition(1, i);  
                Console.Write(wall);  
                area[i - 1, 0] = wall;  
                Console.SetCursorPosition(28, i);  
                Console.Write(wall);  
                area[i - 1, 27] = wall;  
            }

            // Phần trái 1/3
            for (int i = 3; i < 6; i++)
            {
                Console.SetCursorPosition(3, i);  
                for (int j = 3; j < 7; j++)
                {
                    Console.Write(wall); 
                    area[i - 1, j - 1] = wall;  
                }
            }
            for (int i = 7; i < 9; i++)
            {
                Console.SetCursorPosition(3, i);
                for (int j = 3; j < 7; j++)
                {
                    Console.Write(wall); 
                    area[i - 1, j - 1] = wall; 
                }
            }
            for (int i = 10; i < 15; i++)
            {
                Console.SetCursorPosition(2, i); 
                for (int j = 2; j < 7; j++)
                {
                    Console.Write(wall); 
                    area[i - 1, j - 1] = wall; 
                }
            }
            for (int i = 16; i < 21; i++)
            {
                Console.SetCursorPosition(2, i);  
                for (int j = 2; j < 7; j++)
                {
                    Console.Write(wall);  
                    area[i - 1, j - 1] = wall;  
                }
            }
            for (int i = 25; i < 27; i++)
            {
                Console.SetCursorPosition(2, i);  
                for (int j = 2; j < 4; j++)
                {
                    Console.Write(wall);  
                    area[i - 1, j - 1] = wall;  
                }
            }
            for (int i = 22; i < 24; i++)
            {
                Console.SetCursorPosition(3, i);  
                for (int j = 3; j < 7; j++)
                {
                    Console.Write(wall);  
                    area[i - 1, j - 1] = wall;  
                }
            }
            for (int i = 24; i < 27; i++)
            {
                Console.SetCursorPosition(5, i);  
                for (int j = 5; j < 7; j++)
                {
                    Console.Write(wall);  
                    area[i - 1, j - 1] = wall;  
                }
            }

            // Phần trái 2/3
            for (int i = 3; i < 6; i++)
            {
                Console.SetCursorPosition(8, i);  
                for (int j = 8; j < 13; j++)
                {
                    Console.Write(wall);  
                    area[i - 1, j - 1] = wall;  
                }
            }
            for (int i = 7; i < 15; i++)
            {
                Console.SetCursorPosition(8, i);  
                for (int j = 8; j < 10; j++)
                {
                    Console.Write(wall);  
                    area[i - 1, j - 1] = wall;  
                }
            }
            for (int i = 10; i < 12; i++)
            {
                Console.SetCursorPosition(10, i);  
                for (int j = 10; j < 13; j++)
                {
                    Console.Write(wall);  
                    area[i - 1, j - 1] = wall;  
                }
            }
            for (int i = 16; i < 21; i++)
            {
                Console.SetCursorPosition(8, i);  
                for (int j = 8; j < 10; j++)
                {
                    Console.Write(wall);  
                    area[i - 1, j - 1] = wall;  
                }
            }
            for (int i = 22; i < 24; i++)
            {
                Console.SetCursorPosition(8, i);  
                for (int j = 8; j < 13; j++)
                {
                    Console.Write(wall);  
                    area[i - 1, j - 1] = wall;  
                }
            }
            for (int i = 25; i < 28; i++)
            {
                Console.SetCursorPosition(8, i);  
                for (int j = 8; j < 10; j++)
                {
                    Console.Write(wall);  
                    area[i - 1, j - 1] = wall;  
                }
            }
            for (int i = 28; i < 30; i++)
            {
                Console.SetCursorPosition(3, i);  
                for (int j = 3; j < 13; j++)
                {
                    Console.Write(wall);  
                    area[i - 1, j - 1] = wall;  
                }
            }

            // Phần trái 3/3
            for (int i = 2; i < 6; i++)
            {
                Console.SetCursorPosition(14, i);  
                for (int j = 14; j < 15; j++)
                {
                    Console.Write(wall);  
                    area[i - 1, j - 1] = wall;  
                }
            }
            for (int i = 7; i < 9; i++)
            {
                Console.SetCursorPosition(11, i);  
                for (int j = 11; j < 15; j++)
                {
                    Console.Write(wall);  
                    area[i - 1, j - 1] = wall;  
                }
            }
            for (int i = 7; i < 12; i++)
            {
                Console.SetCursorPosition(14, i);  
                for (int j = 14; j < 15; j++)
                {
                    Console.Write(wall);  
                    area[i - 1, j - 1] = wall;  
                }
            }
            for (int i = 13; i < 14; i++)
            {
                Console.SetCursorPosition(11, i);  
                for (int j = 11; j < 15; j++)
                {
                    Console.Write(wall);  
                    area[i - 1, j - 1] = wall;  
                }
            }
            for (int i = 14; i < 17; i++)
            {
                Console.SetCursorPosition(18, i);  
                for (int j = 18; j < 19; j++)
                {
                    Console.Write(wall);  
                    area[i - 1, j - 1] = wall;  
                }
            }
            for (int i = 17; i < 18; i++)
            {
                Console.SetCursorPosition(11, i);  
                for (int j = 11; j < 15; j++)
                {
                    Console.Write(wall);  
                    area[i - 1, j - 1] = wall;  
                }
            }
            for (int i = 19; i < 21; i++)
            {
                Console.SetCursorPosition(11, i);  
                for (int j = 11; j < 15; j++)
                {
                    Console.Write(wall);  
                    area[i - 1, j - 1] = wall;  
                }
            }
            for (int i = 21; i < 24; i++)
            {
                Console.SetCursorPosition(14, i);  
                for (int j = 14; j < 15; j++)
                {
                    Console.Write(wall);  
                    area[i - 1, j - 1] = wall;  
                }
            }
            for (int i = 25; i < 27; i++)
            {
                Console.SetCursorPosition(11, i);  
                for (int j = 11; j < 15; j++)
                {
                    Console.Write(wall);  
                    area[i - 1, j - 1] = wall;  
                }
            }
            for (int i = 27; i < 30; i++)
            {
                Console.SetCursorPosition(14, i);  
                for (int j = 14; j < 15; j++)
                {
                    Console.Write(wall);  
                    area[i - 1, j - 1] = wall;  
                }
            }

            // Phần phải 1/3
            for (int i = 3; i < 6; i++)
            {
                Console.SetCursorPosition(23, i);  
                for (int j = 23; j < 27; j++)
                {
                    Console.Write(wall);  
                    area[i - 1, j - 1] = wall;  
                }
            }
            for (int i = 7; i < 9; i++)
            {
                Console.SetCursorPosition(23, i);  
                for (int j = 23; j < 27; j++)
                {
                    Console.Write(wall);  
                    area[i - 1, j - 1] = wall;  
                }
            }
            for (int i = 10; i < 15; i++)
            {
                Console.SetCursorPosition(23, i);  
                for (int j = 23; j < 28; j++)
                {
                    Console.Write(wall);  
                    area[i - 1, j - 1] = wall;  
                }
            }
            for (int i = 16; i < 21; i++)
            {
                Console.SetCursorPosition(23, i);  
                for (int j = 23; j < 28; j++)
                {
                    Console.Write(wall);  
                    area[i - 1, j - 1] = wall;  
                }
            }
            for (int i = 25; i < 27; i++)
            {
                Console.SetCursorPosition(26, i);  
                for (int j = 26; j < 28; j++)
                {
                    Console.Write(wall);  
                    area[i - 1, j - 1] = wall;  
                }
            }
            for (int i = 22; i < 24; i++)
            {
                Console.SetCursorPosition(23, i);  
                for (int j = 23; j < 27; j++)
                {
                    Console.Write(wall);  
                    area[i - 1, j - 1] = wall;  
                }
            }
            for (int i = 24; i < 27; i++)
            {
                Console.SetCursorPosition(23, i);  
                for (int j = 23; j < 25; j++)
                {
                    Console.Write(wall);  
                    area[i - 1, j - 1] = wall;  
                }
            }

            // Phần phải 2/3
            for (int i = 3; i < 6; i++)
            {
                Console.SetCursorPosition(17, i);  
                for (int j = 17; j < 22; j++)
                {
                    Console.Write(wall);  
                    area[i - 1, j - 1] = wall;  
                }
            }
            for (int i = 7; i < 15; i++)
            {
                Console.SetCursorPosition(20, i);  
                for (int j = 20; j < 22; j++)
                {
                    Console.Write(wall);  
                    area[i - 1, j - 1] = wall;  
                }
            }
            for (int i = 10; i < 12; i++)
            {
                Console.SetCursorPosition(17, i);  
                for (int j = 17; j < 20; j++)
                {
                    Console.Write(wall);  
                    area[i - 1, j - 1] = wall;  
                }
            }
            for (int i = 16; i < 21; i++)
            {
                Console.SetCursorPosition(20, i);  
                for (int j = 20; j < 22; j++)
                {
                    Console.Write(wall);  
                    area[i - 1, j - 1] = wall;  
                }
            }
            for (int i = 22; i < 24; i++)
            {
                Console.SetCursorPosition(17, i);  
                for (int j = 17; j < 22; j++)
                {
                    Console.Write(wall);  
                    area[i - 1, j - 1] = wall;  
                }
            }
            for (int i = 25; i < 28; i++)
            {
                Console.SetCursorPosition(20, i);  
                for (int j = 20; j < 22; j++)
                {
                    Console.Write(wall);  
                    area[i - 1, j - 1] = wall;  
                }
            }
            for (int i = 28; i < 30; i++)
            {
                Console.SetCursorPosition(17, i);  
                for (int j = 17; j < 27; j++)
                {
                    Console.Write(wall);  
                    area[i - 1, j - 1] = wall;  
                }
            }

            // Phần phải 3/3
            for (int i = 2; i < 6; i++)
            {
                Console.SetCursorPosition(15, i);  
                for (int j = 15; j < 16; j++)
                {
                    Console.Write(wall);  
                    area[i - 1, j - 1] = wall;  
                }
            }
            for (int i = 7; i < 9; i++)
            {
                Console.SetCursorPosition(15, i);  
                for (int j = 15; j < 19; j++)
                {
                    Console.Write(wall);  
                    area[i - 1, j - 1] = wall;  
                }
            }
            for (int i = 7; i < 12; i++)
            {
                Console.SetCursorPosition(15, i);  
                for (int j = 15; j < 16; j++)
                {
                    Console.Write(wall);  
                    area[i - 1, j - 1] = wall;  
                }
            }
            for (int i = 13; i < 14; i++)
            {
                Console.SetCursorPosition(15, i);  
                for (int j = 15; j < 19; j++)
                {
                    Console.Write(wall);  
                    area[i - 1, j - 1] = wall;  
                }
            }
            for (int i = 14; i < 17; i++)
            {
                Console.SetCursorPosition(18, i);  
                for (int j = 18; j < 19; j++)
                {
                    Console.Write(wall);  
                    area[i - 1, j - 1] = wall;  
                }
            }
            for (int i = 17; i < 18; i++)
            {
                Console.SetCursorPosition(15, i);  
                for (int j = 15; j < 19; j++)
                {
                    Console.Write(wall);  
                    area[i - 1, j - 1] = wall;  
                }
            }
            for (int i = 19; i < 21; i++)
            {
                Console.SetCursorPosition(15, i);  
                for (int j = 15; j < 19; j++)
                {
                    Console.Write(wall);  
                    area[i - 1, j - 1] = wall;  
                }
            }
            for (int i = 21; i < 24; i++)
            {
                Console.SetCursorPosition(15, i); 
                for (int j = 15; j < 16; j++)
                {
                    Console.Write(wall);  
                    area[i - 1, j - 1] = wall;  
                }
            }
            for (int i = 25; i < 27; i++)
            {
                Console.SetCursorPosition(15, i);  
                for (int j = 15; j < 19; j++)
                {
                    Console.Write(wall);  
                    area[i - 1, j - 1] = wall;
                }
            }
            for (int i = 27; i < 30; i++)
            {
                Console.SetCursorPosition(15, i);  
                for (int j = 15; j < 16; j++)
                {
                    Console.Write(wall);  
                    area[i - 1, j - 1] = wall;  
                }
            }

            Console.ForegroundColor = ConsoleColor.White; // Đặt màu chữ trở lại trắng
        }

        // Vẽ viên ngọc theo cách thủ công
        public void RenderJewels()
        {
            // Dòng ngang trên cùng
            Console.SetCursorPosition(2, 2);  // Đặt vị trí con trỏ
            for (int i = 1; i < 13; i++)
            {
                Console.Write(jewel); // In ký hiệu viên ngọc ra màn hình
                area[1, i] = jewel; // Cập nhật ký hiệu viên ngọc vào mảng bản đồ
            }
            Console.SetCursorPosition(16, 2);  
            for (int i = 15; i < 27; i++)
            {
                Console.Write(jewel);  
                area[1, i] = jewel; 
            }
            // Dòng ngang thứ hai trên cùng
            Console.SetCursorPosition(2, 6);  
            for (int i = 1; i < 27; i++)
            {
                Console.Write(jewel);  
                area[5, i] = jewel;  
            }
            // Dòng dọc trên cùng
            for (int i = 3; i < 10; i++)
            {
                Console.SetCursorPosition(2, i);  
                Console.Write(jewel);  
                area[i - 1, 1] = jewel;  
            }
            for (int i = 3; i < 28; i++)
            {
                Console.SetCursorPosition(7, i);  
                Console.Write(jewel);  
                area[i - 1, 6] = jewel;  
            }
            for (int i = 3; i < 6; i++)
            {
                Console.SetCursorPosition(13, i);  
                Console.Write(jewel);  
                area[i - 1, 12] = jewel;  
            }
            for (int i = 3; i < 6; i++)
            {
                Console.SetCursorPosition(16, i);  
                Console.Write(jewel);  
                area[i - 1, 15] = jewel;  
            }
            for (int i = 3; i < 28; i++)
            {
                Console.SetCursorPosition(22, i);  
                Console.Write(jewel);  
                area[i - 1, 21] = jewel;  
            }
            for (int i = 3; i < 10; i++)
            {
                Console.SetCursorPosition(27, i);  
                Console.Write(jewel);  
                area[i - 1, 26] = jewel;  
            }
            for (int i = 7; i < 9; i++)
            {
                Console.SetCursorPosition(10, i);  
                Console.Write(jewel);  
                area[i - 1, 9] = jewel;  
            }
            for (int i = 7; i < 9; i++)
            {
                Console.SetCursorPosition(19, i);  
                Console.Write(jewel);  
                area[i - 1, 18] = jewel;  
            }
            // Dòng ngang thứ ba trên cùng
            Console.SetCursorPosition(3, 9);  
            for (int i = 2; i < 6; i++)
            {
                Console.Write(jewel);  
                area[8, i] = jewel;  
            }
            Console.SetCursorPosition(23, 9);  
            for (int i = 22; i < 26; i++)
            {
                Console.Write(jewel);  
                area[8, i] = jewel;  
            }
            Console.SetCursorPosition(10, 9);  
            for (int i = 9; i < 13; i++)
            {
                Console.Write(jewel);  
                area[8, i] = jewel;  
            }
            Console.SetCursorPosition(16, 9);  
            for (int i = 15; i < 19; i++)
            {
                Console.Write(jewel);  
                area[8, i] = jewel;  
            }
            // Dòng ngang dưới cùng
            Console.SetCursorPosition(2, 30);  
            for (int i = 1; i < 27; i++)
            {
                Console.Write(jewel);  
                area[29, i] = jewel;  
            }
            // Dòng ngang thứ hai dưới cùng
            Console.SetCursorPosition(2, 27);  
            for (int i = 1; i < 7; i++)
            {
                Console.Write(jewel);  
                area[26, i] = jewel;  
            }
            Console.SetCursorPosition(22, 27);  
            for (int i = 21; i < 27; i++)
            {
                Console.Write(jewel);  
                area[26, i] = jewel;  
            }
            Console.SetCursorPosition(10, 27);  
            for (int i = 9; i < 13; i++)
            {
                Console.Write(jewel);  
                area[26, i] = jewel;  
            }
            Console.SetCursorPosition(16, 27);  
            for (int i = 15; i < 19; i++)
            {
                Console.Write(jewel);  
                area[26, i] = jewel;  
            }
            // Dòng dọc dưới cùng
            for (int i = 28; i < 30; i++)
            {
                Console.SetCursorPosition(2, i);  
                Console.Write(jewel);  
                area[i - 1, 1] = jewel;  
            }
            for (int i = 28; i < 30; i++)
            {
                Console.SetCursorPosition(13, i);  
                Console.Write(jewel);  
                area[i - 1, 12] = jewel;  
            }
            for (int i = 28; i < 30; i++)
            {
                Console.SetCursorPosition(16, i);  
                Console.Write(jewel);  
                area[i - 1, 15] = jewel;  
            }
            for (int i = 28; i < 30; i++)
            {
                Console.SetCursorPosition(27, i);  
                Console.Write(jewel);  
                area[i - 1, 26] = jewel;  
            }
            // Dòng dọc thứ hai dưới cùng
            for (int i = 25; i < 27; i++)
            {
                Console.SetCursorPosition(4, i);  
                Console.Write(jewel);  
                area[i - 1, 3] = jewel;  
            }
            for (int i = 25; i < 27; i++)
            {
                Console.SetCursorPosition(10, i);  
                Console.Write(jewel);  
                area[i - 1, 9] = jewel;  
            }
            for (int i = 25; i < 27; i++)
            {
                Console.SetCursorPosition(19, i);  
                Console.Write(jewel);  
                area[i - 1, 18] = jewel;  
            }
            for (int i = 25; i < 27; i++)
            {
                Console.SetCursorPosition(25, i);  
                Console.Write(jewel);  
                area[i - 1, 24] = jewel;  
            }

            // Dòng ngang thứ ba dưới cùng
            Console.SetCursorPosition(2, 24);  
            for (int i = 1; i < 4; i++)
            {
                Console.Write(jewel);  
                area[23, i] = jewel;  
            }
            Console.SetCursorPosition(25, 24);  
            for (int i = 24; i < 27; i++)
            {
                Console.Write(jewel);  
                area[23, i] = jewel;  
            }
            Console.SetCursorPosition(8, 24);  
            for (int i = 7; i < 13; i++)
            {
                Console.Write(jewel);  
                area[23, i] = jewel;  
            }
            Console.SetCursorPosition(16, 24);  
            for (int i = 15; i < 21; i++)
            {
                Console.Write(jewel);  
                area[23, i] = jewel;  
            }

            // Dòng dọc thứ ba dưới cùng
            for (int i = 22; i < 24; i++)
            {
                Console.SetCursorPosition(2, i);  
                Console.Write(jewel);  
                area[i - 1, 1] = jewel;  
            }
            for (int i = 22; i < 24; i++)
            {
                Console.SetCursorPosition(13, i);  
                Console.Write(jewel);  
                area[i - 1, 12] = jewel;  
            }
            for (int i = 22; i < 24; i++)
            {
                Console.SetCursorPosition(16, i);  
                Console.Write(jewel);  
                area[i - 1, 15] = jewel;  
            }
            for (int i = 22; i < 24; i++)
            {
                Console.SetCursorPosition(27, i);  
                Console.Write(jewel);  
                area[i - 1, 26] = jewel;  
            }

            // Dòng ngang thứ tư
            Console.SetCursorPosition(2, 21);  
            for (int i = 1; i < 13; i++)
            {
                Console.Write(jewel);  
                area[20, i] = jewel;  
            }
            Console.SetCursorPosition(16, 21);  
            for (int i = 15; i < 27; i++)
            {
                Console.Write(jewel);  
                area[20, i] = jewel;  
            }

            Console.ForegroundColor = ConsoleColor.White; // Đặt màu chữ trở lại trắng
        }
    }
}