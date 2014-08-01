using ServiceStack.DataAnnotations;

namespace ELearning.Models
{
    /// <summary>
    /// 菜单实体
    /// </summary>
    [Alias("EL_Menus")]
    public class Menu
    {
        /// <summary>
        /// 菜单编号
        /// </summary>
        [PrimaryKey]
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>
        [Required]
        [CustomField("NVARCHAR(200)")]
        public string Name { get; set; }

        /// <summary>
        /// 父菜单编号,顶层父菜单编号为0
        /// </summary>
        [Required]
        public int ParentId { get; set; }

        /// <summary>
        /// 菜单权重，值大的放在前面
        /// </summary>
        [Required]
        public int Weight { get; set; }

        /// <summary>
        /// 菜单位置
        /// </summary>
        [Required]
        [CustomField("NVARCHAR(20)")]
        public MenuPostion Postion { get; set; }
    }

    /// <summary>
    /// 菜单位置
    /// </summary>
    public enum MenuPostion
    {
        /// <summary>
        /// 顶部
        /// </summary>
        Top,

        /// <summary>
        /// 左侧
        /// </summary>
        Left
    }
}