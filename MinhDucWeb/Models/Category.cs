using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MinhDucWeb.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên danh mục không được để trống")]
        [StringLength(100, ErrorMessage = "Tên danh mục tối đa 100 ký tự")]
        [Display(Name = "Tên Danh Mục")]
        public string Name { get; set; } = string.Empty;

        [StringLength(255)]
        [Display(Name = "Mô Tả")]
        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Quan hệ 1 - N với Products
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
