using System.Text;

namespace Water_Bottle_Problem
{
    /*Công ty cocacola tổ chức 1 chương trình khuyến mại, khi người tiêu dùng uống 3 chai thì đổi 3 vỏ chai sẽ được 1 chai coca mới. 
     * Là nhân viên lập trình và là con nghiện coca bạn cần viết 1 chương trình tính toán số chai coca mình có thể uống được. 
     * Đầu vào là số nguyên N (N > 0 và < 10^5) là số chai bạn có, Đầu ra in ra số lượng chai bạn có thể uống được.*/
    public class Program
    {
        //Mở rộng bài toán khi nhập số nguyên k > 0 là số vỏ chai để đổi được 1 chai mới
        public static long TotalDrinks(long N, long k)
        {
            long total = N;
            while (N >= k)
            {
                long newDrinks = N / k;
                total += newDrinks;
                N = newDrinks + (N % k);
            }
            return total;
        }
        
        public static void Main()
        {
            long N;
            long k;
            Console.OutputEncoding = Encoding.UTF8;
            while (true)
            {
                Console.Write("Nhập số lượng chai: ");
                if (long.TryParse(Console.ReadLine(), out N) && N > 0 && N < 100000)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Đầu vào không hợp lệ! Hãy nhập số chai lớn hơn 0 và nhỏ hơn 100000!");
                    Console.ReadKey();
                }
            }

            while (true)
            {
                Console.Write("Nhập vào số vỏ chai cần thiết để đổi được một chai mới: ");
                if (long.TryParse(Console.ReadLine(), out k) && k > 0)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Đầu vào không hợp lệ! Hãy nhập vào số nguyên dương vỏ chai cần thiết để đổi lấy một chai mới!");
                    Console.ReadKey();
                }
            }

            Console.WriteLine("Tổng số chai bạn có khi nhập vỏ chai là: " + TotalDrinks(N, k));
            Console.WriteLine("Tổng số chai bạn có khi số vỏ chai cần thiết là 3: " + TotalDrinks(N, 3));
            Console.ReadKey();
        }
    }

}
