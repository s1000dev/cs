using System;
using System.Threading;
using System.Text;

// Обновил стрельбу - не надо зажимать пробел, а нажать один раз и появится анимация стрельбы
// Также добавил новое оружие - калаш, но дал ему суперспособности

namespace MechWarrior
{
	class Program
	{
		static void Main(string[] args)
		{
			bool isWorking = false;
			int currentSlot = 0;
			bool isSingleShoot = false;

			// описать слоты меха
			IWeapon[] slots = {
				new MachineGun(),
								new MissileLauncher(),
								new AK()
			};
			// описать игровой цикол
			Console.WriteLine("Для продолжения нажмите Enter");
			var key = Console.ReadKey().Key;
			if (key == ConsoleKey.Enter)
			{
				isWorking = true;
				Console.WriteLine("Игра началась!");
				Console.WriteLine("Чтобы переключить режим оружия, нажмите t");
			}
			else
			{
				Console.WriteLine("Выход из приложения");
			}

			while (isWorking)
			{
				// ввод с клавиатуры
				key = Console.ReadKey().Key;

				if (key == ConsoleKey.Escape)
				{
					Console.WriteLine("Выход из приложения");
					Environment.Exit(0);
				}
				else if (key == ConsoleKey.D1)
				{
					currentSlot = 0;
					Console.WriteLine($"Выбрано оружие: {slots[currentSlot].Name}");
				}
				else if (key == ConsoleKey.D2)
				{
					currentSlot = 1;
					Console.WriteLine($"Выбрано оружие: {slots[currentSlot].Name}");
				}
				else if (key == ConsoleKey.D3)
				{
					currentSlot = 2;
					Console.WriteLine($"Выбрано оружие: {slots[currentSlot].Name}");
				}
				else if (key == ConsoleKey.Spacebar)
				{
					slots[currentSlot].Shoot();
				}
				else if (key == ConsoleKey.R)
				{
					slots[currentSlot].Reload();
				}
				else if (key == ConsoleKey.T)
				{
					if (isSingleShoot == false)
					{
						Console.WriteLine("Вы выбрали одиночный режим стрельбы.");
						isSingleShoot = true;
						slots[currentSlot].ChangeType();
					}
					else
					{
						Console.WriteLine("Вы выбрали автоматический режим стрельбы.");
						isSingleShoot = false;
						slots[currentSlot].ChangeType();
					}
				}
			}

			// завершение программы
		}
	}

	public class MissileLauncher : IWeapon
	{
		public string Name { get; } = "Ракетница";
		public int Damage { get; set; } = 30;
		public int MaxBullets { get; } = 10;
		public int CurrentBullets { get; set; } = 10;

		public bool isSingle { get; set; } = false;

	}

	public class MachineGun : IWeapon
	{
		public string Name { get; } = "Пулемет";
		public int Damage { get; set; } = 15;
		public int MaxBullets { get; } = 150;
		public int CurrentBullets { get; set; } = 150;
		public bool isSingle { get; set; } = false;

	}

	public class AK : IWeapon
	{
		public string Name { get; } = "Калашников";
		public int Damage { get; set; } = 15;
		public int MaxBullets { get; } = 30;
		public int CurrentBullets { get; set; } = 30;
		public bool isSingle { get; set; } = false;
		public int BulletsSpeed { get; set; } = 50;
		public bool isEvolved { get; set; } = false;
		public void Shoot()
		{
			if (isSingle == true)
			{
				if (CurrentBullets > 0)
				{
					CurrentBullets--;
					Console.WriteLine($"В оружии {Name} осталось {CurrentBullets} патронов");
				}
				else
				{
					Console.WriteLine("Товарищ Майор, кончились патроны!!!");
				}
			}
			else
			{
				Random rand = new Random();
				ConsoleKey tmpKey = ConsoleKey.Enter;
				if (isEvolved == true)
				{
					Console.WriteLine($"Чтобы выйти из партии, нажмите k");
					Thread.Sleep(2000);
				}
				while (CurrentBullets > 0)
				{
					CurrentBullets--;
					Thread.Sleep(BulletsSpeed);
					int pos = rand.Next(0, 13);

					if (Console.KeyAvailable)
					{
						tmpKey = Console.ReadKey(true).Key;
					}
					if (tmpKey == ConsoleKey.R)
					{
						Reload();
						break;
					}
					if (tmpKey == ConsoleKey.K && isEvolved == true)
					{
						Console.WriteLine($"Поздравляем вас, вы успешно эволюционировали!");
						isEvolved = true;
						break;
					}
					string str = "              ";
					char replacement = '|';
					StringBuilder sb = new StringBuilder(str);
					sb[pos] = replacement;
					str = sb.ToString();
					Console.WriteLine(str);
				}
				if (CurrentBullets == 0 && isEvolved == false)
				{
					CurrentBullets = 1917;
					BulletsSpeed = 15;
					isEvolved = true;

					Thread.Sleep(2000);
					Console.WriteLine("Товарищ Майор, кончились патроны!!!");
					Thread.Sleep(2000);
					Console.WriteLine("Иванов, ты же Коммунист!!!");
					Thread.Sleep(2000);
					Console.WriteLine("Так точно товарищ майор!");
					Thread.Sleep(2000);

					Shoot();
				}
				else
				{
					CurrentBullets = MaxBullets;
					BulletsSpeed = 50;
					isEvolved = false;
				}
			}
		}
		void Reload()
		{
			CurrentBullets = MaxBullets;
			Console.WriteLine($"Оружие {Name} перезаряжено");
		}

	}

	interface IWeapon
	{
		string Name { get; }
		int Damage { get; set; }
		int MaxBullets { get; }
		int CurrentBullets { get; set; }
		bool isSingle { get; set; }
		public void Shoot()
		{
			if (isSingle == true)
			{
				if (CurrentBullets > 0)
				{
					CurrentBullets--;
					Console.WriteLine($"В оружии {Name} осталось {CurrentBullets} патронов");
				}
				else
				{
					Console.WriteLine("Товарищ Майор, кончились патроны!!!");
				}
			}
			else
			{
				Random rand = new Random();
				ConsoleKey tmpKey = ConsoleKey.Enter;
				while (CurrentBullets > 0)
				{
					CurrentBullets--;
					Thread.Sleep(50);
					int pos = rand.Next(0, 13);

					if (Console.KeyAvailable)
					{
						tmpKey = Console.ReadKey(true).Key;
					}
					if (tmpKey == ConsoleKey.R)
					{
						Reload();
						break;
					}
					string str = "              ";
					char replacement = '|';
					StringBuilder sb = new StringBuilder(str);
					sb[pos] = replacement;
					str = sb.ToString();
					Console.WriteLine(str, CurrentBullets);
				}
				if (CurrentBullets == 0)
				{
					Console.WriteLine("Товарищ Майор, кончились патроны!!!");
				}
			}
		}
		void Reload()
		{
			CurrentBullets = MaxBullets;
			Console.WriteLine($"Оружие {Name} перезаряжено");
		}
		void ChangeType()
		{
			isSingle = !isSingle;
		}
	}

}
