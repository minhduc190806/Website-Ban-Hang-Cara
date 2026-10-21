using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MinhDucWeb.Models
{
    public class OrderDetail
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int OrderId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Tên Sản Phẩm")]
        public string ProductName { get; set; } = string.Empty;

        [StringLength(255)]
        [Display(Name = "Hình Ảnh")]
        public string? ProductImage { get; set; }

        [StringLength(20)]
        [Display(Name = "Kích Cỡ (Size)")]
        public string Size { get; set; } = "L";

        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Đơn Giá ($)")]
        public decimal Price { get; set; }

        [Range(1, 10000)]
        [Display(Name = "Số Lượng")]
        public int Quantity { get; set; } = 1;

        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Thành Tiền ($)")]
        public decimal SubTotal { get; set; }

        [ForeignKey("OrderId")]
        public virtual Order? Order { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product? Product { get; set; }
    }
}
