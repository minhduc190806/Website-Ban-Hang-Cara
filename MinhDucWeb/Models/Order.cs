using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MinhDucWeb.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Mã Đơn Hàng")]
        public string OrderCode { get; set; } = string.Empty;

        public int? CustomerId { get; set; }

        [Required(ErrorMessage = "Tên khách hàng không được để trống")]
        [StringLength(150)]
        [Display(Name = "Tên Khách Hàng")]
        public string CustomerName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        [StringLength(20)]
        [Display(Name = "Số Điện Thoại")]
        public string CustomerPhone { get; set; } = string.Empty;

        [StringLength(150)]
        [Display(Name = "Email")]
        public string? CustomerEmail { get; set; }

        [Required(ErrorMessage = "Địa chỉ nhận hàng không được để trống")]
        [StringLength(255)]
        [Display(Name = "Địa Chỉ Nhận Hàng")]
        public string ShippingAddress { get; set; } = string.Empty;

        [StringLength(500)]
        [Display(Name = "Ghi Chú Đơn Hàng")]
        public string? OrderNotes { get; set; }

        [StringLength(50)]
        [Display(Name = "Phương Thức Thanh Toán")]
        public string PaymentMethod { get; set; } = "COD (Thanh toán khi nhận hàng)";

        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Tổng Tiền ($)")]
        public decimal TotalAmount { get; set; }

        [StringLength(50)]
        [Display(Name = "Trạng Thái")]
        public string Status { get; set; } = "Chờ xử lý"; // 'Chờ xử lý', 'Đã xác nhận', 'Đang giao', 'Đã hoàn thành', 'Đã hủy'

        [Display(Name = "Ngày Đặt")]
        public DateTime OrderDate { get; set; } = DateTime.Now;

        [ForeignKey("CustomerId")]
        public virtual Customer? Customer { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}
