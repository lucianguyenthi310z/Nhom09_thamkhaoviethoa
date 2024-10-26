using System;
using System.Threading;

namespace PacmanGame
{
    class Pacman : Object
    {
        // Xây dựng Pacman
        public Pacman(int x, int y) // Hàm khởi tạo với tọa độ x, y
        {
            this.X = x; // Gán tọa độ X
            this.Y = y; // Gán tọa độ Y
            currentStatePlace = Program.map.EmptySpace; // Thiết lập trạng thái hiện tại là không gian trống
            objectDirection = direction.left; // Đặt hướng di chuyển ban đầu là trái
            Program.map.RenderChar(x, y, GetSymbol()); // Vẽ ký hiệu của Pacman tại vị trí khởi tạo
        }

        // Xử lý các phím bấm
        static ConsoleKeyInfo KeyInfo = new ConsoleKeyInfo(); // Khai báo một biến để lưu thông tin phím bấm

        public void Control(Thread background) // Phương thức điều khiển Pacman
        {
            while (background.IsAlive) // Khi luồng trò chơi còn sống
            {
                KeyInfo = Console.ReadKey(true); // Đọc phím bấm từ người dùng

                // Kiểm tra các phím bấm và cập nhật hướng di chuyển tương ứng
                if (KeyInfo.Key == ConsoleKey.LeftArrow)
                {
                    objectDirection = direction.left; // Hướng trái
                }
                else if (KeyInfo.Key == ConsoleKey.RightArrow)
                {
                    objectDirection = direction.right; // Hướng phải
                }
                else if (KeyInfo.Key == ConsoleKey.UpArrow)
                {
                    objectDirection = direction.up; // Hướng lên
                }
                else if (KeyInfo.Key == ConsoleKey.DownArrow)
                {
                    objectDirection = direction.down; // Hướng xuống
                }
            }
        }

        // Biểu tượng của Pacman
        public override char GetSymbol() // Phương thức trả về ký hiệu của Pacman
        {
            return '@'; // Ký hiệu đại diện cho Pacman
        }

        // Di chuyển Pacman
        public override void ChangePositionByDirection(direction Direction) // Phương thức thay đổi vị trí của Pacman theo hướng di chuyển
        {
            // Biên giới
            if (x > 27) x = 0; // Nếu x vượt quá 27, quay lại 0 (quay vòng)
            else if (x < 0) x = 27; // Nếu x nhỏ hơn 0, đặt lại thành 27 (quay vòng)

            Program.map.RenderChar(x, y, currentStatePlace); // Vẽ ký hiệu hiện tại tại vị trí cũ
            // Cập nhật tọa độ dựa trên hướng di chuyển
            if (Direction == direction.left) x--; // Nếu hướng là trái, giảm x
            if (Direction == direction.right) x++; // Nếu hướng là phải, tăng x
            if (Direction == direction.up) y--; // Nếu hướng là lên, giảm y
            if (Direction == direction.down) y++; // Nếu hướng là xuống, tăng y

            CalcScore(); // Tính điểm nếu Pacman ăn viên ngọc
            Program.map.RenderChar(x, y, GetSymbol()); // Vẽ ký hiệu của Pacman tại vị trí mới
        }

        // Điểm cho viên ngọc
        public void CalcScore() // Phương thức tính điểm
        {
            if (Program.map[x, y] == Map.jewel) // Nếu vị trí hiện tại là viên ngọc
            {
                Program.score += 10; // Tăng điểm số lên 10
            }
        }

        // Bước tiếp theo
        public override void Step() // Phương thức thực hiện bước di chuyển
        {
            char newPosition = GetSymbolByDirection(objectDirection); // Lấy ký hiệu của vị trí mới dựa trên hướng di chuyển

            if (newPosition != Map.wall) // Nếu vị trí mới không phải là tường
            {
                ChangePositionByDirection(objectDirection); // Di chuyển Pacman tới vị trí mới
                previousObjectDirection = objectDirection; // Cập nhật hướng di chuyển trước đó
            }
            else // Nếu vị trí mới là tường
            {
                newPosition = GetSymbolByDirection(previousObjectDirection); // Lấy ký hiệu của vị trí mới dựa trên hướng trước đó
                if (newPosition != Map.wall) // Nếu vị trí mới không phải là tường
                {
                    ChangePositionByDirection(previousObjectDirection); // Di chuyển Pacman tới vị trí mới
                }
            }
        }
    }
}