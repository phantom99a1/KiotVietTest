using System.Text;

namespace Booking_Room_Management_Optimize
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

    #region Tối ưu bằng Binary Search
    /*
     Ý tưởng là lưu trữ các khoảng thời gian đã đặt phòng trong một danh sách có thứ tự 
    và sử dụng tìm kiếm nhị phân để kiểm tra xem khoảng thời gian yêu cầu có trống hay không.    
    */
    #endregion
    public class RoomBooking
    {
        // Dictionary để lưu trữ thông tin đặt phòng
        private readonly Dictionary<string, List<(DateTime start, DateTime end)>> bookingData = [];

        // Hàm kiểm tra phòng trống theo giờ trong ngày
        public bool IsRoomAvailable(string roomId, DateTime startTime, DateTime endTime)
        {
            if (!bookingData.TryGetValue(roomId, out List<(DateTime start, DateTime end)>? value))
            {
                return true; // Phòng chưa từng được đặt
            }

            var bookings = value;
            int index = bookings.BinarySearch((startTime, endTime), Comparer<(DateTime start, DateTime end)>.Create((x, y) => x.start.CompareTo(y.start)));

            if (index < 0) index = ~index;

            if (index > 0 && bookings[index - 1].end > startTime) return false;
            if (index < bookings.Count && bookings[index].start < endTime) return false;

            return true;
        }

        // Hàm kiểm tra phòng trống theo ngày
        public bool IsRoomAvailableOnDay(string roomId, DateTime date)
        {
            if (!bookingData.TryGetValue(roomId, out List<(DateTime start, DateTime end)>? value))
            {
                return true; // Phòng chưa từng được đặt
            }

            var bookings = value;
            DateTime startOfDay = date.Date;
            _ = startOfDay.AddDays(1).AddTicks(-1);
            foreach (var (start, _) in bookings)
            {
                if (start.Date == date.Date)
                {
                    return false; // Có đặt phòng vào ngày đó
                }
            }
            return true;
        }

        // Thêm đặt phòng
        public void AddBooking(string roomId, DateTime startTime, DateTime endTime)
        {
            if (!bookingData.TryGetValue(roomId, out List<(DateTime start, DateTime end)>? value))
            {
                value = [];
                bookingData[roomId] = value;
            }

            var bookings = value;
            int index = bookings.BinarySearch((startTime, endTime), Comparer<(DateTime start, DateTime end)>.Create((x, y) => x.start.CompareTo(y.start)));

            if (index < 0) index = ~index;

            bookings.Insert(index, (startTime, endTime));
        }
    }

    public class Program
    {
        public static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            var roomBooking = new RoomBooking();

            // Thêm các đặt phòng ví dụ
            roomBooking.AddBooking("A", new DateTime(2024, 1, 1, 13, 0, 0), new DateTime(2024, 1, 1, 15, 0, 0));

            // Kiểm tra phòng A có trống vào lúc 16-18h ngày 01/01/2024
            DateTime startTime = new(2024, 1, 1, 16, 0, 0);
            DateTime endTime = new(2024, 1, 1, 18, 0, 0);
            bool isAvailable = roomBooking.IsRoomAvailable("A", startTime, endTime);
            Console.WriteLine($"Phòng A có trống từ 16h đến 18h ngày 01/01/2024: {isAvailable}");

            // Kiểm tra phòng A có trống cả ngày 01/01/2024 hay không
            DateTime checkDate = new(2024, 1, 1);
            isAvailable = roomBooking.IsRoomAvailableOnDay("A", checkDate);
            Console.WriteLine($"Phòng A có trống cả ngày 01/01/2024: {isAvailable}");
            Console.ReadKey();
        }
    }
}
