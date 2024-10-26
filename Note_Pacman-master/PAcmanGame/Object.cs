using System;

namespace PacmanGame
{
    // Lớp trừu tượng cho các nhân vật trong trò chơi
    abstract class Object
    {
        public char currentStatePlace; // Biến lưu trạng thái hiện tại của nhân vật
        public int x, y; // Tọa độ của nhân vật trên bản đồ
        char objectSymbol; // Ký hiệu đại diện cho nhân vật
        public enum direction { left, up, right, down } // Định nghĩa hướng di chuyển
        public direction objectDirection = direction.right; // Hướng di chuyển hiện tại
        public direction previousObjectDirection = direction.right; // Hướng di chuyển trước đó
        public int X // Thuộc tính cho tọa độ X
        {
            set // Thiết lập giá trị cho tọa độ X
            {
                x = value; // Gán giá trị cho biến x
            }
            get // Lấy giá trị của tọa độ X
            {
                return x; // Trả về giá trị của biến x
            }
        }
        public int Y // Thuộc tính cho tọa độ Y
        {
            set // Thiết lập giá trị cho tọa độ Y
            {
                y = value; // Gán giá trị cho biến y
            }
            get // Lấy giá trị của tọa độ Y
            {
                return y; // Trả về giá trị của biến y
            }
        }
        public Random randomize = new Random(); // Tạo một đối tượng Random để sinh số ngẫu nhiên

        public abstract char GetSymbol(); // Phương thức trừu tượng để lấy ký hiệu của đối tượng

        public abstract void Step(); // Phương thức trừu tượng để thực hiện bước di chuyển

        public char GetSymbolByDirection(direction Direction) // Lấy ký hiệu dựa trên hướng di chuyển
        {
            if (Direction == direction.left) return Program.map[x - 1, y]; // Nếu hướng là trái, trả về ký hiệu bên trái
            if (Direction == direction.right) return Program.map[x + 1, y]; // Nếu hướng là phải, trả về ký hiệu bên phải
            if (Direction == direction.up) return Program.map[x, y - 1]; // Nếu hướng là lên, trả về ký hiệu phía trên
            return Program.map[x, y + 1]; // Ngược lại, trả về ký hiệu phía dưới
        }

        public void KillPacman() // Phương thức kiểm tra xem nhân vật có giết Pacman không
        {
            if (Program.pacman.x == x && Program.pacman.y == y) // Nếu tọa độ của Pacman trùng với tọa độ của đối tượng
            {
                Program.gameOver = true; // Đặt trạng thái trò chơi là kết thúc
            }
        }

        public virtual void ChangePositionByDirection(direction Direction) // Thay đổi vị trí của đối tượng theo hướng di chuyển
        {
            // Biên giới
            if (x > 27) x = 0; // Nếu x vượt quá 27, quay lại 0 (quay vòng)
            else if (x < 0) x = 27; // Nếu x nhỏ hơn 0, đặt lại thành 27 (quay vòng)

            Program.map.RenderChar(x, y, currentStatePlace); // Vẽ ký hiệu hiện tại tại vị trí cũ
            if (Direction == direction.left) x--; // Nếu hướng là trái, giảm x
            if (Direction == direction.right) x++; // Nếu hướng là phải, tăng x
            if (Direction == direction.up) y--; // Nếu hướng là lên, giảm y
            if (Direction == direction.down) y++; // Nếu hướng là xuống, tăng y

            // Khi ma gặp nhau
            if (Program.map[x, y] != Map.stupidGhostSymbol && Program.map[x, y] != Map.smartGhostSymbol) // Nếu vị trí không phải là của ma
            {
                currentStatePlace = Program.map[x, y]; // Cập nhật trạng thái hiện tại
            }
            // Vẽ nhân vật
            Program.map.RenderChar(x, y, GetSymbol()); // Vẽ ký hiệu của nhân vật tại vị trí mới
        }
    }
}