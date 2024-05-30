using BLL.Models;

namespace Web.Models;

public class StockItemViewModel(StockItem stockItem)
{
	public StockItem StockItem { get; set; } = stockItem;
}