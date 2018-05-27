namespace NaiveGumballMachine
{
	public enum State
	{
		SoldOut,        // Жвачка закончилась
		NoQuarter,      // Нет монетки
		HasQuarter,     // Есть монетка
		Sold,           // Монетка выдана
	}
}
