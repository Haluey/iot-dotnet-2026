namespace Prac06Arrange
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 01. 입출력
            Console.Write("이름 > ");
            string name = Console.ReadLine();


            Console.WriteLine("안녕하세요, " + name);
            Console.WriteLine($"안녕하세요, {name}님!!");     // 스트링 포맷이 훨씩 효율적

            // 02. 형변환
            Console.Write("나이(정수) > ");
            string input = Console.ReadLine();
            int age = int.Parse(input); // 정수가 아니면 예외발생!

            Console.WriteLine(age.GetType());

            // 03. 클래스
            Person p1 = new Person();   
            p1.Name = "홍길동";    
            p1.Walk();
        }
    }

    // 03. 클래스
    class Person
    {
        public string Name; // 속성
        public int Age; // 멤버 변수(필드)

        public void Walk()
        {
            Console.WriteLine($"{Name}(이)가 걷습니다.");
        }
    }

    // 04. 인터페이스 - 추상 클래스와 유사
    public interface IMovable
    {
        void Move();    // 메서드 구현이 없음. 정의만
    }

    class Car : IMovable
    {
        // 인터페이스를 가져다 쓰면 반드시 인터페이스에 있는 메서드 정의를 구현해야 함
        public void Move()
        {
            Console.WriteLine("차가 굴러갑니다");
        }
    }
}
