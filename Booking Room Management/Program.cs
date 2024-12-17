namespace Booking_Room_Management
{
    #region Đề bài
    /* 
    Khách sạn X có N phòng.
    Để quản lý việc đặt phòng và check phòng trống thì cần lưu dữ liệu đặt phòng như thế nào tối ưu nhất để:
    1. Có thể check phòng trống theo ngày
    2. Có thể check phòng trống theo giờ trong ngày.

    Ví dụ: Ngày 1/1/2024 đã có đặt phòng A vào lúc 13h-15h.
    1. Làm sao để check nhanh nhất phòng A có trống ngày đó vào lúc 16-18h
    2. Check phòng đó có trống ngày 1/1 hay không
    Phương án tối ưu là phương án mà việc kiểm tra đơn giản nhất và dữ liệu lưu ít nhất
    */
    #endregion

    #region Cấu trúc dữ liệu và phương pháp lưu trữ
    /*
     1. Lưu trữ thông tin đặt phòng:
        Tạo một dictionary với khóa là mã phòng và giá trị là danh sách các khoảng thời gian đã được đặt phòng trong một ngày cụ thể.
        Mỗi khoảng thời gian được lưu trữ dưới dạng cặp (startTime, endTime).
     2. Cách kiểm tra phòng trống:
        - Kiểm tra phòng trống theo giờ trong ngày:
            + Khi cần kiểm tra phòng trống theo giờ, ta lấy danh sách các khoảng thời gian đã được đặt phòng của phòng đó.
            + Duyệt qua danh sách và kiểm tra xem khoảng thời gian mới có giao với bất kỳ khoảng thời gian 
            nào trong danh sách không.
            + Nếu không có khoảng thời gian nào giao nhau, phòng đó trống trong khoảng thời gian yêu cầu.
        - Kiểm tra phòng trống theo ngày:
            + Để kiểm tra xem phòng có trống trong một ngày hay không, chỉ cần kiểm tra xem danh sách các khoảng 
            thời gian đã đặt phòng của phòng đó có rỗng hay không.
    */
    #endregion
    public class HotelBooking
    {
        private readonly Dictionary<(string, string), List<(int, int)>> bookingData = [];

        // Hàm kiểm tra phòng trống theo giờ trong ngày
        public bool IsRoomAvailable(string roomId, string date, int startTime, int endTime)
        {
            var key = (roomId, date);
            if (bookingData.TryGetValue(key, out List<(int, int)>? value))
            {
                foreach (var booking in value)
                {
                    if (!(endTime <= booking.Item1 || startTime >= booking.Item2))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        // Hàm kiểm tra phòng trống theo ngày
        public bool IsRoomAvailableDay(string roomId, string date)
        {
            var key = (roomId, date);
            return !bookingData.ContainsKey(key) || bookingData[key].Count == 0;
        }

        // Thêm đặt phòng
        public void AddBooking(string roomId, string date, int startTime, int endTime)
        {
            var key = (roomId, date);
            if (!bookingData.TryGetValue(key, out List<(int, int)>? value))
            {
                value = [];
                bookingData[key] = value;
            }

            value.Add((startTime, endTime));
        }
    }

    class Program
    {
        static void Main()
        {
            var hotelBooking = new HotelBooking();
            hotelBooking.AddBooking("A", "01/01/2024", 13, 15);

            // Kiểm tra phòng A có trống vào lúc 16-18h ngày 01/01/2024
            Console.WriteLine(hotelBooking.IsRoomAvailable("A", "01/01/2024", 16, 18));

            // Kiểm tra phòng A có trống cả ngày 01/01/2024 hay không
            Console.WriteLine(hotelBooking.IsRoomAvailableDay("A", "01/01/2024"));
            Console.ReadKey();
        }
    }
}
