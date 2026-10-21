using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MinhDucWeb.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn danh mục")]
        [Display(Name = "Danh Mục")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
        [StringLength(200, ErrorMessage = "Tên sản phẩm tối đa 200 ký tự")]
        [Display(Name = "Tên Sản Phẩm")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Thương hiệu không được để trống")]
        [StringLength(100)]
        [Display(Name = "Thương Hiệu / Hãng")]
        public string Brand { get; set; } = "Adidas";

        [Required(ErrorMessage = "Giá bán không được để trống")]
        [Range(0.01, 100000000, ErrorMessage = "Giá bán phải lớn hơn 0")]
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Giá Bán ($)")]
        public decimal Price { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Giá Cũ ($)")]
        public decimal? OldPrice { get; set; }

        [Required(ErrorMessage = "Đường dẫn hình ảnh không được để trống")]
        [StringLength(255)]
        [Display(Name = "Đường Dẫn Hình Ảnh")]
        public string ImageUrl { get; set; } = "img/products/f1.jpg";

        [Display(Name = "Mô Tả Sản Phẩm")]
        public string? Description { get; set; }

        [Range(0, 100000, ErrorMessage = "Số lượng tồn kho không hợp lệ")]
        [Display(Name = "Số Lượng Tồn Kho")]
        public int Stock { get; set; } = 50;

        [Display(Name = "Sản Phẩm Nổi Bật")]
        public bool IsFeatured { get; set; } = false;

        [Display(Name = "Hàng Mới Về")]
        public bool IsNewArrival { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Khóa ngoại
        [ForeignKey("CategoryId")]
        public virtual Category? Category { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}
