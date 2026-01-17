namespace ZooWorld.Gameplay
{
	public class BattleHitInfo
	{
		public Animal Source { get; private set; }
		public Animal Target { get; private set; }

		public void UpdateData(Animal source, Animal target)
		{
			Source = source;
			Target = target;
		}
	}
}