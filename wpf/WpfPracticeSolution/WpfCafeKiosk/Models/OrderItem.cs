namespace WpfCafeKiosk.Models
{
    public class OrderItem
    {
        // menu_id, menu_name, price 컬럼명
        // MenuId, MenuName, Price 클래스 속성명
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public int Price { get; set; }
        public int Count { get; set; }

        // get { return Price * Count; }
        public int TotalPrice { get; set; }
    }
}
