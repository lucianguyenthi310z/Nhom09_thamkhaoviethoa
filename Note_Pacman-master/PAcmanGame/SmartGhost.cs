using System;
using System.Collections.Generic; 

namespace PacmanGame
{
    class SmartGhost : Object
    {
        // Xây dựng ma thông minh
        public SmartGhost(int x, int y, direction Direction) // Hàm khởi tạo với tọa độ x, y và hướng di chuyển
        {
            this.X = x; // Gán tọa độ X
            this.Y = y; // Gán tọa độ Y
            currentStatePlace = Program.map.Jewel; // Thiết lập trạng thái hiện tại là viên ngọc
            objectDirection = Direction; // Đặt hướng di chuyển
            Program.map.RenderChar(x, y, GetSymbol()); // Vẽ ký hiệu của ma tại vị trí khởi tạo
        }

        // Biểu tượng của ma
        public override char GetSymbol() // Phương thức trả về ký hiệu của ma
        {
            return Map.smartGhostSymbol; // Ký hiệu đại diện cho ma thông minh
        }

        // Phát hiện hướng có thể
        public void DetectDirection() // Phương thức phát hiện hướng di chuyển
        {
            List<direction> variantsOfDirection = new List<direction>(); // Danh sách các hướng khả thi

            // Kiểm tra hướng di chuyển hiện tại và thêm hướng khả thi vào danh sách
            if (objectDirection == direction.up)
            {
                if (Program.map[x, y - 1] != Map.wall) // Nếu không phải là tường phía trên
                {
                    variantsOfDirection.Add(direction.up); // Thêm hướng lên
                }
                if (Program.map[x - 1, y] != Map.wall) // Nếu không phải là tường bên trái
                {
                    variantsOfDirection.Add(direction.left); // Thêm hướng trái
                }
                if (Program.map[x + 1, y] != Map.wall) // Nếu không phải là tường bên phải
                {
                    variantsOfDirection.Add(direction.right); // Thêm hướng phải
                }
            }
            else if (objectDirection == direction.down)
            {
                if (Program.map[x, y + 1] != Map.wall) // Nếu không phải là tường phía dưới
                {
                    variantsOfDirection.Add(direction.down); // Thêm hướng xuống
                }
                if (Program.map[x - 1, y] != Map.wall) // Nếu không phải là tường bên trái
                {
                    variantsOfDirection.Add(direction.left); // Thêm hướng trái
                }
                if (Program.map[x + 1, y] != Map.wall) // Nếu không phải là tường bên phải
                {
                    variantsOfDirection.Add(direction.right); // Thêm hướng phải
                }
            }
            else if (objectDirection == direction.left)
            {
                if (Program.map[x, y - 1] != Map.wall) // Nếu không phải là tường phía trên
                {
                    variantsOfDirection.Add(direction.up); // Thêm hướng lên
                }
                if (Program.map[x - 1, y] != Map.wall) // Nếu không phải là tường bên trái
                {
                    variantsOfDirection.Add(direction.left); // Thêm hướng trái
                }
                if (Program.map[x, y + 1] != Map.wall) // Nếu không phải là tường phía dưới
                {
                    variantsOfDirection.Add(direction.down); // Thêm hướng xuống
                }
            }
            else // Nếu hướng di chuyển là phải
            {
                if (Program.map[x, y - 1] != Map.wall) // Nếu không phải là tường phía trên
                {
                    variantsOfDirection.Add(direction.up); // Thêm hướng lên
                }
                if (Program.map[x, y + 1] != Map.wall) // Nếu không phải là tường phía dưới
                {
                    variantsOfDirection.Add(direction.down); // Thêm hướng xuống
                }
                if (Program.map[x + 1, y] != Map.wall) // Nếu không phải là tường bên phải
                {
                    variantsOfDirection.Add(direction.right); // Thêm hướng phải
                }
            }

            // Chọn hướng dựa trên vị trí của Pacman
            Pacman pacman = Program.pacman; // Lấy đối tượng Pacman

            // Định hướng di chuyển dựa trên vị trí của Pacman
            if (x < pacman.x && objectDirection != direction.left && variantsOfDirection.Contains(direction.right))
            {
                objectDirection = direction.right; // Chọn hướng phải
            }
            else if (x > pacman.x && objectDirection != direction.right && variantsOfDirection.Contains(direction.left))
            {
                objectDirection = direction.left; // Chọn hướng trái
            }
            else if (y > pacman.y && objectDirection != direction.down && variantsOfDirection.Contains(direction.up))
            {
                objectDirection = direction.up; // Chọn hướng lên
            }
            else if (y < pacman.y && objectDirection != direction.up && variantsOfDirection.Contains(direction.down))
            {
                objectDirection = direction.down; // Chọn hướng xuống
            }
            else // Nếu không có hướng khả thi nào
            {
                Random random = new Random(); // Tạo đối tượng ngẫu nhiên
                int index = random.Next(variantsOfDirection.Count); // Chọn ngẫu nhiên một hướng khả thi
                objectDirection = variantsOfDirection[index]; // Đặt hướng di chuyển
            }
        }

        // Bước tiếp theo
        public override void Step() // Phương thức thực hiện bước di chuyển
        {
            KillPacman(); // Giết Pacman nếu có thể
            DetectDirection(); // Phát hiện hướng di chuyển
            ChangePositionByDirection(objectDirection); // Di chuyển ma thông minh
            KillPacman(); // Giết Pacman nếu có thể
        }
    }
}