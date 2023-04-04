using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Contracts.ApiModels.Tegs
{
    public class AddTegApi
    {
        [Required(ErrorMessage = "Отсутсвует текст тега")]
        [DataType(DataType.Text)]       
        [StringLength(20, ErrorMessage = "Поле {0} должно иметь минимум {2} и максимум {1} символов.", MinimumLength = 3)]
        public string? Content { get; set; }
    }
}
